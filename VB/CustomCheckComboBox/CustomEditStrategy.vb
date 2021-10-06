Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Xpf.Editors
Imports System.Windows.Input
Imports DevExpress.Xpf.Editors.Validation.Native
Imports DevExpress.Xpf.Editors.EditStrategy

Namespace CustomCheckComboBox

    Friend Class CustomEditStrategy
        Inherits ComboBoxEditStrategy

        Private searchItems As List(Of String)

        Private Property CurrentEditor As ComboBoxEdit

        Private Property InvalidTextInput As Boolean

        Public Sub New(ByVal editor As ComboBoxEdit)
            MyBase.New(editor)
            searchItems = New List(Of String)()
            CurrentEditor = editor
            InvalidTextInput = False
        End Sub

        Private Function CheckIsSeparatorInEndOfString(ByVal text As String) As Boolean
            Dim items As List(Of String) = text.Split(CurrentEditor.SeparatorString.Split(), StringSplitOptions.RemoveEmptyEntries).ToList(Of String)()
            If Enumerable.Last(items).Length = 1 AndAlso FindItemIndexByText(items.Last()) = -1 Then
                EditBox.Text = CurrentEditor.DisplayText
                CurrentEditor.SelectionStart = CurrentEditor.DisplayText.Length
                Return True
            End If

            If text.Length > CurrentEditor.SeparatorString.Length Then
                Dim lastString As String = text.Substring(text.Length - CurrentEditor.SeparatorString.Length, CurrentEditor.SeparatorString.Length)
                If lastString Is CurrentEditor.SeparatorString Then Return True
            End If

            Return False
        End Function

        Private Sub SearchItemsListRefreshing(ByVal editText As String)
            searchItems.Clear()
            searchItems = editText.Split(CurrentEditor.SeparatorString.Split(), StringSplitOptions.RemoveEmptyEntries).ToList(Of String)()
        End Sub

        Protected Overrides Sub ProcessChangeText(ByVal editText As String, ByVal updateAutoSearchSelection As Boolean)
            If Not Equals(editText, String.Empty) Then
                If CheckIsSeparatorInEndOfString(editText) Then Return
            Else
                ValueContainer.SetEditValue(Nothing, UpdateEditorSource.TextInput)
                UpdateDisplayText()
                Return
            End If

            UpdateAutoSearchBeforeValidate(editText)
            Dim values As List(Of Object) = New List(Of Object)()
            Dim indexesWithoutDups As List(Of Integer) = FindItemIndexByText(searchItems).Distinct().ToList()
            Dim loopCount As Integer = If(indexesWithoutDups.Count > searchItems.Count, searchItems.Count, indexesWithoutDups.Count)
            For i As Integer = 0 To loopCount - 1
                values.Add(CreateEditableItem(indexesWithoutDups(i), searchItems(i)))
            Next

            If EditValue IsNot Nothing AndAlso values.All(Function(item) CType(CurrentEditor.EditValue, List(Of Object)).Contains(item)) AndAlso CType(CurrentEditor.EditValue, List(Of Object)).All(Function(item) values.Contains(item)) Then
                UpdateDisplayText()
                Dim pr As LookUpEditBasePropertyProvider = TryCast(CurrentEditor.GetValue(ActualPropertyProvider.PropertiesProperty), LookUpEditBasePropertyProvider)
                If pr.SelectionViewModel.SelectAll Is Nothing OrElse pr.SelectionViewModel.SelectAll = False Then UpdateAutoSearchSelectionMultipleItems(InvalidTextInput)
                Return
            Else
                ValueContainer.SetEditValue(values, UpdateEditorSource.TextInput)
            End If

            UpdateAutoSearchAfterValidate(editText)
            UpdateDisplayText()
            Me.UpdateAutoSearchSelection(updateAutoSearchSelection)
            ShowIsImmediatePopup()
        End Sub

        Public Overridable Function FindItemIndexByText(ByVal items As List(Of String)) As List(Of Integer)
            Return CType(ItemsProvider, ItemsIndexFinder).FindItemIndexByText(items, CurrentEditor.IsCaseSensitiveSearch, CurrentEditor.AutoComplete)
        End Function

        Public Overrides Sub UpdateAutoSearchText(ByVal editText As String, ByVal reverse As Boolean)
            If Not CurrentEditor.AutoComplete OrElse IncrementalFiltering Then Return
            Dim editValuesIndexes As List(Of Integer) = New List(Of Integer)()
            If EditValue IsNot Nothing AndAlso EditValue.[GetType]() Is GetType(List(Of Object)) Then
                For i As Integer = 0 To CType(EditValue, List(Of Object)).Count - 1
                    editValuesIndexes.Add(FindItemIndexByText(GetItemDisplayValue(i)))
                Next

                If ValidateLastItemBeforeRefresh(editText, editValuesIndexes) = -1 Then
                    Return
                End If
            End If

            SearchItemsListRefreshing(editText)
            AutoSearchTextBuilding(editText, reverse, editValuesIndexes)
        End Sub

        Private Sub AutoSearchTextBuilding(ByVal editText As String, ByVal reverse As Boolean, ByVal editValuesIndexes As List(Of Integer))
            Dim autoText As String = String.Empty
            If searchItems.Count > 1 Then
                Dim ind As Integer = FindItemIndexByText(searchItems(searchItems.Count - 1))
                Dim indexesWithoutDups As List(Of Integer) = FindItemIndexByText(searchItems).Distinct().ToList()
                Dim exceptedList As List(Of Integer) = indexesWithoutDups.Except(editValuesIndexes).ToList()
                Dim index As Integer = If(exceptedList.Count = 0, editValuesIndexes.Last(), exceptedList.First())
                autoText = If(ind > -1, Convert.ToString(ItemsProvider.GetDisplayValueByIndex(index)), If(reverse, AutoSearchText, editText))
                AutoSearchText = autoText.Substring(0, Math.Min(autoText.Length, Enumerable.Last(searchItems).Length))
            Else
                Dim index As Integer = FindItemIndexByText(editText)
                autoText = If(index > -1, Convert.ToString(ItemsProvider.GetDisplayValueByIndex(index)), If(reverse, AutoSearchText, editText))
                AutoSearchText = autoText.Substring(0, Math.Min(autoText.Length, editText.Length))
            End If
        End Sub

        Private Function ValidateLastItemBeforeRefresh(ByVal editText As String, ByVal editValuesIndexes As List(Of Integer)) As Integer
            Dim lastSearchItem As String = String.Empty
            For i As Integer = editText.Length - 1 To 0 Step -1
                If editText(i).ToString() Is CurrentEditor.SeparatorString Then Exit For
                lastSearchItem += editText(i)
            Next

            Dim array As Char() = lastSearchItem.ToCharArray()
            System.Array.Reverse(array)
            Dim indexes As List(Of Integer) = FindItemIndexByText(New List(Of String)() From {New String(array)})
            If editValuesIndexes.Count > 1 AndAlso editValuesIndexes.Count = Enumerable.ToList(Enumerable.Distinct(indexes)).Count Then
                If Not(editValuesIndexes.Except(indexes.Distinct().ToList())).Any() Then
                    InvalidTextInput = True
                    Return -1
                End If
            End If

            If indexes.Count = 0 Then
                If CurrentEditor.SelectionStart Is CurrentEditor.DisplayText.Length + 1 Then InvalidTextInput = True
                Return -1
            End If

            If editValuesIndexes.Count > 1 Then
                If Not indexes.Distinct().ToList().Except(editValuesIndexes).Any() Then
                    AutoSearchText = New String(array)
                    If CurrentEditor.SelectionStart Is CurrentEditor.DisplayText.Length + 1 Then InvalidTextInput = True
                    Return -1
                End If
            End If

            Return indexes.First()
        End Function

        Private Sub UpdateAutoSearchSelection(ByVal updateSelection As Boolean)
            If Not CurrentEditor.AutoComplete Then Return
            If updateSelection Then
                If EditValue Is Nothing Then Return
                If EditValue.[GetType]() Is GetType(List(Of Object)) Then
                    UpdateAutoSearchSelectionMultipleItems(InvalidTextInput)
                    Return
                Else
                    Dim singleItemPrimaryText As String = CStr(GetDisplayValue(EditValue))
                    CurrentEditor.SelectionStart = AutoSearchText.Length
                    CurrentEditor.SelectionLength = Math.Max(0, singleItemPrimaryText.Length - AutoSearchText.Length)
                    Return
                End If
            End If
        End Sub

        Private Sub UpdateAutoSearchSelectionMultipleItems(ByVal input As Boolean)
            Dim editorDisplayText As String = String.Empty
            For i As Integer = 0 To CType(EditValue, List(Of Object)).Count - 1 - 1
                editorDisplayText += GetItemDisplayValue(i)
                If i <> (CType(EditValue, List(Of Object)).Count - 1) Then editorDisplayText += CurrentEditor.SeparatorString
            Next

            If input = False Then
                CurrentEditor.SelectionStart = editorDisplayText.Length + AutoSearchText.Length
                CurrentEditor.SelectionLength = Math.Max(CurrentEditor.SelectionStart, ((CStr(GetDisplayValue(Enumerable.Last(Of Object)(CType(EditValue, List(Of Object)))))).Length - AutoSearchText.Length))
            Else
                CurrentEditor.SelectionStart = CurrentEditor.DisplayText.Length
                CurrentEditor.SelectionLength = 0
                InvalidTextInput = False
            End If
        End Sub

        Private Function GetItemDisplayValue(ByVal currentCount As Integer) As String
            If CType(EditValue, List(Of Object))(currentCount).GetType() Is GetType(LookUpEditableItem) Then
                Return CType(CType(EditValue, List(Of Object))(currentCount), LookUpEditableItem).DisplayValue.ToString()
            Else
                Return(CStr(GetDisplayValue(CType(EditValue, List(Of Object))(currentCount))))
            End If
        End Function

        Public Overrides Sub ProcessAutoCompleteNavKey(ByVal e As KeyEventArgs)
            Dim text As String = GetDisplayText()
            If e.Key = Key.Back AndAlso searchItems.Count > 1 Then
                If CurrentEditor.SelectionStart = 0 Then
                    If CurrentEditor.SelectionLength = text.Length Then EditBox.Text = String.Empty
                Else
                    CurrentEditor.SelectionStart -= 1
                    CurrentEditor.SelectionLength = If(CurrentEditor.SelectionLength = 0, 1, CSharpImpl.__Assign(CurrentEditor.SelectionLength, 1))
                    AutoSearchText = text.Substring(0, text.Length - 1)
                End If

                e.Handled = True
            ElseIf e.Key = Key.Enter AndAlso searchItems.Count > 0 Then
                CurrentEditor.Text += CurrentEditor.SeparatorString
                CurrentEditor.SelectionStart = text.Length + 1
                e.Handled = True
            ElseIf e.Key = Key.Delete AndAlso searchItems.Count > 0 Then
                ProcessChangeText(ItemRemoving())
                e.Handled = True
            ElseIf e.Key = Key.Escape Then
                CurrentEditor.SelectionStart = CurrentEditor.Text.Length
                CurrentEditor.SelectionLength = 0
            Else
                MyBase.ProcessAutoCompleteNavKey(e)
            End If
        End Sub

        Private Function ItemRemoving() As String
            If CurrentEditor.SelectionStart > CurrentEditor.Text.Length Then Return CurrentEditor.Text
            If CurrentEditor.SelectionLength = 0 Then Return CurrentEditor.Text
            Dim length As Integer = If(EditBox.Text.Last().ToString() Is CurrentEditor.SeparatorString, CurrentEditor.SelectionLength - 1, CurrentEditor.SelectionLength)
            Dim selectedItems As List(Of String) = CurrentEditor.Text.Substring(CurrentEditor.SelectionStart, length).Split(CurrentEditor.SeparatorString.Split(), StringSplitOptions.RemoveEmptyEntries).ToList(Of String)()
            Dim deletedItems As List(Of String) = searchItems.Intersect(selectedItems).ToList()
            For Each item As String In deletedItems
                searchItems.Remove(item)
            Next

            If searchItems.Count = 0 Then Return String.Empty
            If(deletedItems.Count = 0 OrElse CurrentEditor.SelectionStart + CurrentEditor.SelectionLength Is CurrentEditor.Text.Length - 1) AndAlso selectedItems.Count <> 0 Then searchItems.Remove(searchItems.Last())
            Dim newText As String = String.Empty
            For i As Integer = 0 To searchItems.Count - 1
                newText += searchItems(i)
                If i <> searchItems.Count - 1 Then newText += CurrentEditor.SeparatorString
            Next

            Return newText
        End Function

        Private Class CSharpImpl

            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class
End Namespace
