Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Xpf.Editors.Settings
Imports DevExpress.Xpf.Editors.Helpers

Namespace CustomCheckComboBox
	Public Class CustomComboBoxEditSettings
		Inherits ComboBoxEditSettings

		Shared Sub New()
			RegisterCustomEdit()
		End Sub
		Protected Overrides Function CreateItemsProvider() As ItemsProvider
			Return New ItemsIndexFinder(Me)
		End Function
		Public Shared Sub RegisterCustomEdit()
			EditorSettingsProvider.Default.RegisterUserEditor(GetType(CustomComboBoxEdit), GetType(CustomComboBoxEditSettings), Function() New CustomComboBoxEdit(), Function() New CustomComboBoxEditSettings())
		End Sub
	End Class
End Namespace
