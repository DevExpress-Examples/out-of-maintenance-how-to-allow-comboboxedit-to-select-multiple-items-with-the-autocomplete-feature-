Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows

Namespace CustomCheckComboBox
	Partial Public Class MainWindow
		Inherits Window
		Public Sub New()
			InitializeComponent()
			FillCombo()
		End Sub

		Public Sub FillCombo()
			Dim persons As New List(Of Person)()
			persons.Add(New Person() With {.Id = 1, .IsActive = False, .Name = "Adam"})
			persons.Add(New Person() With {.Id = 2, .IsActive = True, .Name = "John"})
			persons.Add(New Person() With {.Id = 3, .IsActive = False, .Name = "Alexander"})
			persons.Add(New Person() With {.Id = 4, .IsActive = True, .Name = "Andrew"})
			persons.Add(New Person() With {.Id = 5, .IsActive = True, .Name = "Alexaey"})
			persons.Add(New Person() With {.Id = 6, .IsActive = True, .Name = "Michael"})

			multiSelectionCombo.ItemsSource = persons
			multiSelectionCombo.DisplayMember = "Name"
			multiSelectionCombo.ValueMember = "Id"
		End Sub
	End Class
End Namespace
