Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Editors.Helpers

Namespace CustomCheckComboBox
	Public Class ItemsIndexFinder
		Inherits ItemsProvider

		Public Sub New(ByVal owner As IItemsProviderOwner)
			MyBase.New(owner)

		End Sub
		Public Function FindItemIndexByText(ByVal items As List(Of String), ByVal isCaseSensitive As Boolean, ByVal autoComplete As Boolean) As List(Of Integer)
			If items.Count = 1 Then
				Dim text As String = items.First()
				If text Is Nothing Then
					Return New List(Of Integer) (New Integer() {-1})
				End If
				Dim findText As String = If(isCaseSensitive, text, text.ToLower())
				Return FindItemIndexByTextInternal(text, isCaseSensitive, autoComplete)
			Else
				Dim findedItems As New List(Of Integer)()
				For Each item As String In items
					findedItems.AddRange(FindItemIndexByTextInternal(item, isCaseSensitive, autoComplete))
				Next item
				Return findedItems

			End If
		End Function
		Private Function FindItemIndexByTextInternal(ByVal text As String, ByVal isCaseSensitive As Boolean, ByVal autoComplete As Boolean) As List(Of Integer)
			If text.Length = 0 Then
				For i As Integer = 0 To Count - 1
					If String.Empty Is GetPrimaryText(Me(i)) Then
						Return New List(Of Integer) (New Integer() {i})
					End If
				Next i
			Else
				If (Not isCaseSensitive) Then
					text = text.ToLower()
				End If
				Dim itemsIndex As New List(Of Integer)()
				For i As Integer = 0 To Count - 1
					Dim itemText As String = String.Empty
					If (Not isCaseSensitive) Then
						itemText = GetPrimaryText(Me(i)).ToLower()
					Else
						itemText = GetPrimaryText(Me(i))
					End If
					If autoComplete Then
						itemText = itemText.Substring(0, Math.Min(itemText.Length, text.Length))
					End If
					If text = itemText Then
						Dim listSourceIndex As Integer = DataController.GetListSourceRowIndex(i)
						itemsIndex.Add(listSourceIndex)
					End If
				Next i
				Return itemsIndex
			End If
			Return New List(Of Integer) (New Integer() {-1})
		End Function

		Private Function GetPrimaryText(ByVal item As Object) As String
			Dim value As Object = GetDisplayValueFromItem(item)
			Return If(value Is Nothing, String.Empty, value.ToString())
		End Function
	End Class
End Namespace
