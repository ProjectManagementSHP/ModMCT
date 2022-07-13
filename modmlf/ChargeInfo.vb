Public Class ChargeInfo
    Private _Rows As Integer
    Private _PN As String
    Private _Cell As String

    Public Property PN As String
        Get
            Return _PN
        End Get
        Set
            _PN = Value
        End Set
    End Property

    Public Property Rows As Integer
        Get
            Return _Rows
        End Get
        Set
            _Rows = Value
        End Set
    End Property
    Public Property Cell As String
        Get
            Return _Cell
        End Get
        Set
            _Cell = Value
        End Set
    End Property
End Class
