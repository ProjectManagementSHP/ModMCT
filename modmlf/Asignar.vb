Imports System.Reflection
Public Class Asignar
    Public re_confirmando As Integer
    Private Sub Asignar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button2.Visible = False
        Dim systemType As Type
        Dim propertyInfo As PropertyInfo
        systemType = dgvSort.GetType()
        propertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvSort, True, Nothing)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txbNotas.Text <> "" Then
            Principal.DetenerCWO(Label6.Text, txbNotas.Text, 1)
            Principal.filtros(4)
            Me.Dispose()
            Me.Close()
        Else
            MsgBox("Agrega una nota")
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim LongitudArray As Integer = dgvSort.Rows.Count()
        Dim auto As AutomaticSort = New AutomaticSort(MaskedTextBox1.Text, cola, LongitudArray, dgvSort, maq, lblcwoporsolicitar.Text)
        If auto.Compare() Then
            If Not auto.CheckZeros() Then
                auto.RemoveZeros()
                If Not auto.GetIdSort Then
                    dgvSort.DataSource = Principal.GetTable("select CWO,Id [Orden] from tblCWO where (Id > 0 or Id is not null) and Maq=" + maq.ToString + " and Status='OPEN' order by Id asc")
                    LongitudArray = dgvSort.Rows.Count()
                    auto = New AutomaticSort(MaskedTextBox1.Text, cola, LongitudArray, dgvSort, maq, lblcwoporsolicitar.Text)
                    auto.SetArray()
                End If
            Else
                auto.SetArray()
            End If
            MsgBox("Se han efectuado los cambios")
            If Principal.rbsolicitar.Checked = True Then Principal.filtros(1)
            If Principal.rbSolicitado.Checked = True Then Principal.filtros(2)
            If Principal.rbListosParaEntrar.Checked = True Then Principal.filtros(3)
            If Principal.rbYaempezados.Checked = True Then Principal.filtros(4)
            Me.Dispose()
            Me.Close()
        Else
            MsgBox("No es posible debido a que no puedes colocar un CWO en posiciones mayores o la misma posicion, solo posiciones menores a su lugar por entrar a corte", MessageBoxIcon.Warning)
        End If
    End Sub
    Private Sub MaskedTextBox1_TextChanged(sender As Object, e As EventArgs) Handles MaskedTextBox1.TextChanged
        If Not MaskedTextBox1.Text = "" Or MaskedTextBox1.TextLength = 0 Then
            Button2.Visible = True
        Else
            Button2.Visible = False
        End If
    End Sub
End Class