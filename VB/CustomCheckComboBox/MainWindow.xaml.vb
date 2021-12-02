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
            persons.Add(New Person() With {.Id = 5, .IsActive = True, .Name = "Alexaey"})
            persons.Add(New Person() With {.Id = 6, .IsActive = True, .Name = "Michael"})
            Me.multiSelectionCombo.ItemsSource = persons
            Me.multiSelectionCombo.DisplayMember = "Name"
            Me.multiSelectionCombo.ValueMember = "Id"
        End Sub
    End Class
End Namespace
