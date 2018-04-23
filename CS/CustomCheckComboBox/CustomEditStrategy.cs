using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpf.Editors;
using System.Windows.Input;
using DevExpress.Xpf.Editors.Validation.Native;
using DevExpress.Xpf.Editors.EditStrategy;

namespace CustomCheckComboBox {
    class CustomEditStrategy : ComboBoxEditStrategy {

        List<string> searchItems;
        ComboBoxEdit CurrentEditor { get; set; }
        bool InvalidTextInput { get; set; }

        public CustomEditStrategy(ComboBoxEdit editor)
            : base(editor) {
            searchItems = new List<string>();
            CurrentEditor = editor;
            InvalidTextInput = false;
        }

        private bool CheckIsSeparatorInEndOfString(string text) {
            List<string> items = text.Split(CurrentEditor.SeparatorString.Split(), StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            if(items.Last().Length == 1 && FindItemIndexByText(items.Last(), true) == -1) {
                EditBox.Text = CurrentEditor.DisplayText;
                CurrentEditor.SelectionStart = CurrentEditor.DisplayText.Length;
                return true;
            }
            if(text.Length > CurrentEditor.SeparatorString.Length) {
                string lastString = text.Substring(text.Length - CurrentEditor.SeparatorString.Length, CurrentEditor.SeparatorString.Length);
                if(lastString == CurrentEditor.SeparatorString)
                    return true;
            }
            return false;
        }

        private void SearchItemsListRefreshing(string editText) {
            searchItems.Clear();
            searchItems = editText.Split(CurrentEditor.SeparatorString.Split(), StringSplitOptions.RemoveEmptyEntries).ToList<string>();
        }
        protected override void ProcessChangeText(string editText, bool updateAutoSearchSelection) {
            if(editText != String.Empty) {
                if(CheckIsSeparatorInEndOfString(editText))
                    return;
            }
            else {
                ValueContainer.SetEditValue(null, UpdateEditorSource.TextInput);
                UpdateDisplayText();
                return;
            }
            UpdateAutoSearchBeforeValidate(editText);
            List<object> values = new List<object>();
            List<int> indexesWithoutDups = FindItemIndexByText(searchItems).Distinct().ToList();
            int loopCount = indexesWithoutDups.Count > searchItems.Count ? searchItems.Count : indexesWithoutDups.Count;
            for(int i = 0; i < loopCount; i++) {
                values.Add(CreateEditableItem(indexesWithoutDups[i], searchItems[i]));
            }
            if(EditValue != null && values.All(item => ((List<object>)CurrentEditor.EditValue).Contains(item)) && ((List<object>)CurrentEditor.EditValue).All(item => values.Contains(item))) {
                UpdateDisplayText();
                LookUpEditBasePropertyProvider pr = CurrentEditor.GetValue(ActualPropertyProvider.PropertiesProperty) as LookUpEditBasePropertyProvider;
                if(pr.SelectionViewModel.SelectAll == null || pr.SelectionViewModel.SelectAll == false)
                    UpdateAutoSearchSelectionMultipleItems(InvalidTextInput);
                return;
            }
            else
                ValueContainer.SetEditValue(values, UpdateEditorSource.TextInput);
            UpdateAutoSearchAfterValidate(editText);
            UpdateDisplayText();
            UpdateAutoSearchSelection(updateAutoSearchSelection);
            ShowIsImmediatePopup();
        }

        public virtual List<int> FindItemIndexByText(List<string> items) {
            return ((ItemsIndexFinder)ItemsProvider).FindItemIndexByText(items, CurrentEditor.IsCaseSensitiveSearch, CurrentEditor.AutoComplete);
        }

        public override void UpdateAutoSearchText(string editText, bool reverse) {
            if(!CurrentEditor.AutoComplete || IncrementalFiltering) return;
            List<int> editValuesIndexes = new List<int>();
            if(EditValue != null && EditValue.GetType() == typeof(List<object>)) {
                for(int i = 0; i < ((List<object>)EditValue).Count; i++) {
                    editValuesIndexes.Add(FindItemIndexByText(GetItemDisplayValue(i), true));
                }
                if(ValidateLastItemBeforeRefresh(editText, editValuesIndexes) == -1) {
                    return;
                }
            }
            SearchItemsListRefreshing(editText);
            AutoSearchTextBuilding(editText, reverse, editValuesIndexes);
        }

        private void AutoSearchTextBuilding(string editText, bool reverse, List<int> editValuesIndexes) {
            string autoText = string.Empty;
            if(searchItems.Count > 1) {
                int ind = FindItemIndexByText(searchItems[searchItems.Count - 1], true);

                List<int> indexesWithoutDups = FindItemIndexByText(searchItems).Distinct().ToList();
                List<int> exceptedList = indexesWithoutDups.Except(editValuesIndexes).ToList();

                int index = exceptedList.Count == 0 ? editValuesIndexes.Last() : exceptedList.First();
                autoText = ind > -1 ? Convert.ToString(ItemsProvider.GetDisplayValueByIndex(index)) : reverse ? AutoSearchText : editText;
                AutoSearchText = autoText.Substring(0, Math.Min(autoText.Length, searchItems.Last().Length));
            }
            else {
                int index = FindItemIndexByText(editText, true);
                autoText = index > -1 ? Convert.ToString(ItemsProvider.GetDisplayValueByIndex(index)) : reverse ? AutoSearchText : editText;
                AutoSearchText = autoText.Substring(0, Math.Min(autoText.Length, editText.Length));
            }
        }
        private int ValidateLastItemBeforeRefresh(string editText, List<int> editValuesIndexes) {
            string lastSearchItem = string.Empty;
            for(int i = editText.Length - 1; i >= 0; i--) {
                if(editText[i].ToString() == CurrentEditor.SeparatorString)
                    break;
                lastSearchItem += editText[i];
            }
            char[] array = lastSearchItem.ToCharArray();
            Array.Reverse(array);
            List<int> indexes = FindItemIndexByText(new List<string>() { new string(array) });
            if(editValuesIndexes.Count > 1 && editValuesIndexes.Count == indexes.Distinct().ToList().Count) {
                if(!(editValuesIndexes.Except(indexes.Distinct().ToList())).Any()) {
                    InvalidTextInput = true;
                    return -1;
                }
            }
            if(indexes.Count == 0) {
                if(CurrentEditor.SelectionStart == CurrentEditor.DisplayText.Length + 1)
                    InvalidTextInput = true;
                return -1;
            }
            if(editValuesIndexes.Count > 1)
                if(!indexes.Distinct().ToList().Except(editValuesIndexes).Any()) {
                    AutoSearchText = new string(array);
                    if(CurrentEditor.SelectionStart == CurrentEditor.DisplayText.Length + 1)
                        InvalidTextInput = true;
                    return -1;
                }
            return indexes.First();
        }
        void UpdateAutoSearchSelection(bool updateSelection) {
            if(!CurrentEditor.AutoComplete)
                return;
            if(updateSelection) {
                if(EditValue == null) return;
                if(EditValue.GetType() == typeof(List<object>)) {
                    UpdateAutoSearchSelectionMultipleItems(InvalidTextInput);
                    return;
                }
                else {
                    string singleItemPrimaryText = (String)(GetDisplayValue(EditValue));
                    CurrentEditor.SelectionStart = AutoSearchText.Length;
                    CurrentEditor.SelectionLength = Math.Max(0, singleItemPrimaryText.Length - AutoSearchText.Length);
                    return;
                }
            }
        }

        private void UpdateAutoSearchSelectionMultipleItems(bool input) {
            string editorDisplayText = String.Empty;
            for(int i = 0; i < ((List<object>)EditValue).Count - 1; i++) {
                editorDisplayText += GetItemDisplayValue(i);
                if(i != (((List<object>)EditValue).Count - 1))
                    editorDisplayText += CurrentEditor.SeparatorString;
            }
            if(input == false) {
                CurrentEditor.SelectionStart = editorDisplayText.Length + AutoSearchText.Length;
                CurrentEditor.SelectionLength = Math.Max(CurrentEditor.SelectionStart, (((String)GetDisplayValue(((List<object>)EditValue).Last())).Length - AutoSearchText.Length));
            }
            else {
                CurrentEditor.SelectionStart = CurrentEditor.DisplayText.Length;
                CurrentEditor.SelectionLength = 0;
                InvalidTextInput = false;
            }
        }

        private string GetItemDisplayValue(int currentCount) {
            if(((List<object>)EditValue)[currentCount].GetType() == typeof(LookUpEditableItem))
                return ((LookUpEditableItem)((List<object>)EditValue)[currentCount]).DisplayValue.ToString();
            else
                return ((String)GetDisplayValue(((List<object>)EditValue)[currentCount]));
        }

        public override void ProcessAutoCompleteNavKey(KeyEventArgs e) {
            string text = GetDisplayText();
            if(e.Key == Key.Back && searchItems.Count > 1) {
                if(CurrentEditor.SelectionStart == 0) {
                    if(CurrentEditor.SelectionLength == text.Length)
                        EditBox.Text = String.Empty;
                }
                else {
                    CurrentEditor.SelectionStart -= 1;
                    CurrentEditor.SelectionLength = CurrentEditor.SelectionLength == 0 ? 1 : CurrentEditor.SelectionLength += 1;
                    AutoSearchText = text.Substring(0, text.Length - 1);
                }
                e.Handled = true;
            }
            else if(e.Key == Key.Enter && searchItems.Count > 0) {
                CurrentEditor.Text += CurrentEditor.SeparatorString;
                CurrentEditor.SelectionStart = text.Length + 1;
                e.Handled = true;
            }
            else if(e.Key == Key.Delete && searchItems.Count > 0) {
                ProcessChangeText(ItemRemoving());
                e.Handled = true;
            }
            else if(e.Key == Key.Escape) { CurrentEditor.SelectionStart = CurrentEditor.Text.Length; CurrentEditor.SelectionLength = 0; }

            else
                base.ProcessAutoCompleteNavKey(e);
        }
        string ItemRemoving() {
            if(CurrentEditor.SelectionStart > CurrentEditor.Text.Length) return CurrentEditor.Text;
            if(CurrentEditor.SelectionLength == 0) return CurrentEditor.Text;
            int length = EditBox.Text.Last().ToString() == CurrentEditor.SeparatorString ? CurrentEditor.SelectionLength - 1 : CurrentEditor.SelectionLength;
            List<string> selectedItems = CurrentEditor.Text.
                                                       Substring(CurrentEditor.SelectionStart, length).
                                                       Split(CurrentEditor.SeparatorString.Split(), StringSplitOptions.RemoveEmptyEntries).
                                                       ToList<string>(); ;
            List<string> deletedItems = searchItems.Intersect(selectedItems).ToList();
            foreach(string item in deletedItems) {
                searchItems.Remove(item);
            }
            if(searchItems.Count == 0) return String.Empty;

            if((deletedItems.Count == 0 || CurrentEditor.SelectionStart + CurrentEditor.SelectionLength == CurrentEditor.Text.Length - 1) && selectedItems.Count != 0)
                searchItems.Remove(searchItems.Last());

            string newText = string.Empty;
            for(int i = 0; i < searchItems.Count; i++) {
                newText += searchItems[i];
                if(i != searchItems.Count - 1)
                    newText += CurrentEditor.SeparatorString;
            }
            return newText;
        }
    }
}