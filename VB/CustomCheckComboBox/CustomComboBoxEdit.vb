﻿' Developer Express Code Central Example:
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

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Xpf.Editors

Namespace CustomCheckComboBox
    Public Class CustomComboBoxEdit
        Inherits ComboBoxEdit

        Shared Sub New()
            CustomComboBoxEditSettings.RegisterCustomEdit()
        End Sub

        Protected Overrides Function CreateEditStrategy() As EditStrategyBase
            Return New CustomEditStrategy(Me)
        End Function
    End Class
End Namespace
