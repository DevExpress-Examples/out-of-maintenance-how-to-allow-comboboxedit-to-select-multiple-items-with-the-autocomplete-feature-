' Developer Express Code Central Example:
' How to allow ComboBoxEdit to select multiple items with the autocomplete feature
' 
' By default, ComboBoxEdit allows you to select one item using the autocomplete
' feature.
' This example provides a way to enable the behavior when the
' autocomplete feature works for multiple item selection. Firstly, it is necessary
' to create a ComboBoxEdit descendant and override the CreateEditStrategy method,
' which should return your own ComboBoxEditStrategy class descendant.
' Then,
' override and implement the ProcessChangeText method, and some more methods to
' enable item auto-searching selection.
' For unimpeded search and selection of
' items without losing input field focus, the set FocusPopupOnOpen property to
' False.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E4226
' Developer Express Code Central Example:
' How to allow ComboBoxEdit to select multiple items with the autocomplete feature
' 
' By default, ComboBoxEdit allows you to select one item using the autocomplete
' feature.
' This example provides a way to enable the behavior when the
' autocomplete feature works for multiple item selection. Firstly, it is necessary
' to create a ComboBoxEdit descendant and override the CreateEditStrategy method,
' which should return your own ComboBoxEditStrategy class descendant.
' Then,
' override and implement the ProcessChangeText method, and some more methods to
' enable item auto-searching selection.
' For unimpeded search and selection of
' items without losing input field focus, the set FocusPopupOnOpen property to
' False.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E4226
Imports System.Collections.Generic
Imports System.Windows

Namespace CustomCheckComboBox

    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            FillCombo()
        End Sub

        Public Sub FillCombo()
            Dim persons As List(Of Person) = New List(Of Person)()
            persons.Add(New Person() With {.Id = 1, .IsActive = False, .Name = "Adam"})
            persons.Add(New Person() With {.Id = 2, .IsActive = True, .Name = "John"})
            persons.Add(New Person() With {.Id = 3, .IsActive = False, .Name = "Alexander"})
            persons.Add(New Person() With {.Id = 4, .IsActive = True, .Name = "Andrew"})
            persons.Add(New Person() With {.Id = 5, .IsActive = True, .Name = "Alexey"})
            persons.Add(New Person() With {.Id = 6, .IsActive = True, .Name = "Michael"})
            Me.multiSelectionCombo.ItemsSource = persons
            Me.multiSelectionCombo.DisplayMember = "Name"
            Me.multiSelectionCombo.ValueMember = "Id"
        End Sub
    End Class
End Namespace
