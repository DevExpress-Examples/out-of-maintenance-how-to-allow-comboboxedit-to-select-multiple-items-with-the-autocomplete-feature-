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
