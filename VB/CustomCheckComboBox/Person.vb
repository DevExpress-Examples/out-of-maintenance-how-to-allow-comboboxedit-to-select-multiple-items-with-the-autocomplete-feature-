Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace CustomCheckComboBox
	Friend Class Person
		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property
		Private privateId As Integer
		Public Property Id() As Integer
			Get
				Return privateId
			End Get
			Set(ByVal value As Integer)
				privateId = value
			End Set
		End Property
		Private privateIsActive As Boolean
		Public Property IsActive() As Boolean
			Get
				Return privateIsActive
			End Get
			Set(ByVal value As Boolean)
				privateIsActive = value
			End Set
		End Property
		Public Overrides Function ToString() As String
			Return Name
		End Function
	End Class
End Namespace
