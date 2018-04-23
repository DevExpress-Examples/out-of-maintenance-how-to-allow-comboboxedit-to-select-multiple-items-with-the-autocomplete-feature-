Imports Microsoft.VisualBasic
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
