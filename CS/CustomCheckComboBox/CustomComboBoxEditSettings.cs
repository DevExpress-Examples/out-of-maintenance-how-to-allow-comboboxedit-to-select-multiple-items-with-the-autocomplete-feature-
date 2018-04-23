using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors.Helpers;

namespace CustomCheckComboBox {
    public class CustomComboBoxEditSettings : ComboBoxEditSettings {

        static CustomComboBoxEditSettings() {
            RegisterCustomEdit();
        }
        protected override ItemsProvider CreateItemsProvider() {
            return new ItemsIndexFinder(this);
        }
        public static void RegisterCustomEdit() {
            EditorSettingsProvider.Default.RegisterUserEditor(typeof(CustomComboBoxEdit),
                typeof(CustomComboBoxEditSettings),
                () => new CustomComboBoxEdit(),
                () => new CustomComboBoxEditSettings());
        }
    }
}
