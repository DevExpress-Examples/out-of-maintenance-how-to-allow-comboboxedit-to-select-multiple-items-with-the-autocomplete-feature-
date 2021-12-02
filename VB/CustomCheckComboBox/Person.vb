Namespace CustomCheckComboBox

    Friend Class Person

        Public Property Name As String

        Public Property Id As Integer

        Public Property IsActive As Boolean

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
End Namespace
