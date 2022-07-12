Public Class ChargeInfo
    Private _Rows As Integer
    Public Property PN As String
    Public Property Rows As Integer
        Get
            Return _Rows
        End Get
        Set
            _Rows = Value
        End Set
    End Property
End Class
