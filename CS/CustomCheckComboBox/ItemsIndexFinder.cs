using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors.Helpers;

namespace CustomCheckComboBox {
    public class ItemsIndexFinder : ItemsProvider {
        
        public ItemsIndexFinder(IItemsProviderOwner owner)
            : base(owner) {

        }
        public List<int> FindItemIndexByText(List<string> items, bool isCaseSensitive, bool autoComplete) {
            if (items.Count == 1) {
                string text = items.First();
                if (text == null)
                    return new List<int>() { -1 };
                string findText = isCaseSensitive ? text : text.ToLower();
                return FindItemIndexByTextInternal(text, isCaseSensitive, autoComplete);
            }
            else {
                List<int> findedItems = new List<int>();
                foreach (string item in items) {
                    findedItems.AddRange(FindItemIndexByTextInternal(item, isCaseSensitive, autoComplete));
                }
                return findedItems;
            
            }
        }
        List<int> FindItemIndexByTextInternal(string text, bool isCaseSensitive, bool autoComplete) {
            if (text.Length == 0) {
                for (int i = 0; i < Count; ++i) {
                    if (string.Empty == GetPrimaryText(this[i])) {
                        return new List<int>() { i };
                    }
                }
            }
            else {
                if (!isCaseSensitive)
                    text = text.ToLower();
                List<int> itemsIndex = new List<int>();
                for (int i = 0; i < Count; i++) {
                    string itemText = String.Empty;
                    if (!isCaseSensitive)
                        itemText = GetPrimaryText(this[i]).ToLower();
                    else
                        itemText = GetPrimaryText(this[i]);
                    if (autoComplete)
                        itemText = itemText.Substring(0, Math.Min(itemText.Length, text.Length));
                    if (text == itemText) {
                        int listSourceIndex = DataController.GetListSourceRowIndex(i);
                        itemsIndex.Add(listSourceIndex);
                    }
                }
                return itemsIndex;
            }
            return new List<int>() { -1 };
        }

        string GetPrimaryText(object item) {
            object value = GetDisplayValueFromItem(item);
            return value == null ? string.Empty : value.ToString();
        }
    }
}
