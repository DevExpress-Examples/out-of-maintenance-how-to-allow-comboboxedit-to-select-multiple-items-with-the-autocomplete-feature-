using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpf.Editors;

namespace CustomCheckComboBox {
    public class CustomComboBoxEdit : ComboBoxEdit {
     
        static CustomComboBoxEdit() {
            CustomComboBoxEditSettings.RegisterCustomEdit();
        }
        
        protected override EditStrategyBase CreateEditStrategy() {
            return new CustomEditStrategy(this);
        }
    }
}
