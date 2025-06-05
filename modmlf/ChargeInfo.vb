Public Class ChargeInfo
    Implements IChargeInfo

    Private _Rows As Integer
    Private _PN As String
    Private _Cell As String
    Private _WIP As String
    Private _AU As String
    Private _Balance As Integer
    Private _RunTime As Integer

    Public Property PN As String Implements IChargeInfo.PN
        Get
            Return _PN
        End Get
        Set
            _PN = Value
        End Set
    End Property

    Public Property Rows As Integer Implements IChargeInfo.Rows
        Get
            Return _Rows
        End Get
        Set
            _Rows = Value
        End Set
    End Property
    Public Property Cell As String Implements IChargeInfo.Cell
        Get
            Return _Cell
        End Get
        Set
            _Cell = Value
        End Set
    End Property
    Public Property Balance As String Implements IChargeInfo.Balance
        Get
            Return _Balance
        End Get
        Set
            _Balance = Value
        End Set
    End Property
    Public Property RunTime As String Implements IChargeInfo.RunTime
        Get
            Return _RunTime
        End Get
        Set
            _RunTime = Value
        End Set
    End Property
    Public Property WIP As String Implements IChargeInfo.WIP
        Get
            Return _WIP
        End Get
        Set
            _WIP = Value
        End Set
    End Property
    Public Property AU As String Implements IChargeInfo.AU
        Get
            Return _AU
        End Get
        Set
            _AU = Value
        End Set
    End Property
End Class
