// Developer Express Code Central Example:
// How to allow ComboBoxEdit to select multiple items with the autocomplete feature
// 
// By default, ComboBoxEdit allows you to select one item using the autocomplete
// feature.
// This example provides a way to enable the behavior when the
// autocomplete feature works for multiple item selection. Firstly, it is necessary
// to create a ComboBoxEdit descendant and override the CreateEditStrategy method,
// which should return your own ComboBoxEditStrategy class descendant.
// Then,
// override and implement the ProcessChangeText method, and some more methods to
// enable item auto-searching selection.
// For unimpeded search and selection of
// items without losing input field focus, the set FocusPopupOnOpen property to
// False.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E4226

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CustomCheckComboBox {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            FillCombo();
        }

        public void FillCombo() {
            List<Person> persons = new List<Person>();
            persons.Add(new Person() { Id = 1, IsActive = false, Name = "Adam"});
            persons.Add(new Person() { Id = 2, IsActive = true, Name = "John" });
            persons.Add(new Person() { Id = 3, IsActive = false, Name = "Alexander" });
            persons.Add(new Person() { Id = 4, IsActive = true, Name = "Andrew" });
            persons.Add(new Person() { Id = 5, IsActive = true, Name = "Alexaey" });
            persons.Add(new Person() { Id = 6, IsActive = true, Name = "Michael" });

            multiSelectionCombo.ItemsSource = persons;
            multiSelectionCombo.DisplayMember = "Name";
            multiSelectionCombo.ValueMember = "Id";
        }
    }
}
