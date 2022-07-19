Public Class ChargeInfo
    Private _Rows As Integer
    Private _PN As String
    Private _Cell As String
    Private _Balance As Integer
    Private _RunTime As Integer
    Private _WIP As String

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
    Public Property Balance As String
        Get
            Return _Balance
        End Get
        Set
            _Balance = Value
        End Set
    End Property
    Public Property RunTime As String
        Get
            Return _RunTime
        End Get
        Set
            _RunTime = Value
        End Set
    End Property
    Public Property WIP As String
        Get
            Return _WIP
        End Get
        Set
            _WIP = Value
        End Set
    End Property
End Class
