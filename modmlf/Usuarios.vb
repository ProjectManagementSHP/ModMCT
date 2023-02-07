Imports System.Data.SqlClient

Public Class Usuarios
	Public execQuery As Func(Of String, SqlCommand) = Function(sentence)
														  If cnn.State.ToString = "Open" Then cnn.Close()
														  Dim cmd As SqlCommand
														  cmd = New SqlCommand(sentence, cnn)
														  cmd.CommandType = CommandType.Text
														  cmd.CommandTimeout = 18000000
														  If cnn.State.ToString = "Closed" Then cnn.Open()
														  Return cmd
													  End Function
	Dim iduser As Integer
	Private Sub LoadUsers()
		Dim read As SqlDataReader
		Dim tb As New DataTable
		read = execQuery("SELECT IDUsuario,UserID,Position,Active,Module,Department FROM tblItemsPOUserIDAuthorizations WHERE Module='MLF' and Active=1").ExecuteReader
		tb.Load(read)
		With dgvUsers
			.DataSource = tb
			.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
			.AutoResizeColumns()
			.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
		End With
		cnn.Close()
	End Sub
	Private Sub FillItems(e As DataGridViewCellEventArgs)
		Try
			TextBox1.Text = $"{dgvUsers.Rows(e.RowIndex).Cells("UserID").Value}"
			TextBox2.Text = $"{dgvUsers.Rows(e.RowIndex).Cells("Position").Value}"
			CheckBox8.Checked = CBool(dgvUsers.Rows(e.RowIndex).Cells("Active").Value)
			iduser = CInt(dgvUsers.Rows(e.RowIndex).Cells("IDUsuario").Value)
			Dim dept As String = $"{dgvUsers.Rows(e.RowIndex).Cells("Department").Value}"

			If dept = "PlanCorte" Then
				CheckBox1.Checked = True
			ElseIf dept = "PlanXP" Then
				CheckBox2.Checked = True
			ElseIf dept = "PlanPWO" Then
				CheckBox3.Checked = True
			ElseIf dept = "Compras" Then
				CheckBox6.Checked = True
			ElseIf dept = "Aplicadores" Then
				CheckBox5.Checked = True
			ElseIf dept = "Almacen" Then
				CheckBox4.Checked = True
			ElseIf dept = "Desarrollo" Then
				CheckBox7.Checked = True
			Else
				Dim opciones As String() = dept.Split(",")
				If opciones IsNot Nothing Then
					For Each _Option In opciones
						If _Option = "3" Then 'Aplicadores
							CheckBox5.Checked = True
						ElseIf _Option = "2" Then 'Almacen
							CheckBox4.Checked = True
						ElseIf _Option = "5" Then 'Compras
							CheckBox6.Checked = True
						ElseIf _Option = "6" Then 'PlanCorte
							CheckBox1.Checked = True
						ElseIf _Option = "7" Then 'PlanXP
							CheckBox2.Checked = True
						ElseIf _Option = "8" Then 'PlanPWO
							CheckBox3.Checked = True
						End If
					Next
				End If
			End If
			cnn.Close()
		Catch ex As Exception
			cnn.Close()
		End Try
	End Sub
	Private Function GetDepts()
		Dim DeptsUnion As String = ""
		If CheckBox7.Checked And CheckBox6.Checked = False And CheckBox5.Checked = False And CheckBox4.Checked = False And CheckBox3.Checked = False And CheckBox2.Checked = False And CheckBox1.Checked = False Then
			DeptsUnion = "Desarrollo"
		ElseIf CheckBox7.Checked = False And CheckBox6.Checked And CheckBox5.Checked = False And CheckBox4.Checked = False And CheckBox3.Checked = False And CheckBox2.Checked = False And CheckBox1.Checked = False Then
			DeptsUnion = "Compras"
		ElseIf CheckBox7.Checked = False And CheckBox6.Checked = False And CheckBox5.Checked And CheckBox4.Checked = False And CheckBox3.Checked = False And CheckBox2.Checked = False And CheckBox1.Checked = False Then
			DeptsUnion = "Aplicadores"
		ElseIf CheckBox7.Checked = False And CheckBox6.Checked = False And CheckBox5.Checked = False And CheckBox4.Checked And CheckBox3.Checked = False And CheckBox2.Checked = False And CheckBox1.Checked = False Then
			DeptsUnion = "Almacen"
		ElseIf CheckBox7.Checked = False And CheckBox6.Checked = False And CheckBox5.Checked = False And CheckBox4.Checked = False And CheckBox3.Checked And CheckBox2.Checked = False And CheckBox1.Checked = False Then
			DeptsUnion = "PlanPWO"
		ElseIf CheckBox7.Checked = False And CheckBox6.Checked = False And CheckBox5.Checked = False And CheckBox4.Checked = False And CheckBox3.Checked = False And CheckBox2.Checked And CheckBox1.Checked = False Then
			DeptsUnion = "PlanXP"
		ElseIf CheckBox7.Checked = False And CheckBox6.Checked = False And CheckBox5.Checked = False And CheckBox4.Checked = False And CheckBox3.Checked = False And CheckBox2.Checked = False And CheckBox1.Checked Then
			DeptsUnion = "PlanCorte"
		Else

			DeptsUnion = $"{If(CheckBox1.Checked, "6", "")},{If(CheckBox2.Checked, "7", "")},{If(CheckBox3.Checked, "8", "")},{If(CheckBox4.Checked, "2", "")},{If(CheckBox5.Checked, "3", "")},{If(CheckBox6.Checked, "5", "")}"

		End If
		DeptsUnion = DeptsUnion.ToString.TrimEnd(",").Trim.TrimEnd(",").TrimStart(",").Trim.TrimStart(",")
		DeptsUnion = LTrim(RTrim(DeptsUnion)).Replace(",,", ",")
		DeptsUnion = LTrim(RTrim(DeptsUnion)).Replace(",,,", ",")
		DeptsUnion = LTrim(RTrim(DeptsUnion)).Replace(",,,,", ",")
		DeptsUnion = LTrim(RTrim(DeptsUnion)).Replace(",,,", ",")
		DeptsUnion = LTrim(RTrim(DeptsUnion)).Replace(",,", ",")

		Return DeptsUnion
	End Function
	Private Sub AddOrEditUser()
		Try
			Dim query As String = ""
			If Button1.Text = "Actualizar" Then
				query = $"update tblItemsPOUserIDAuthorizations set UserID='{TextBox1.Text}',Position='{TextBox2.Text}',Active='{CheckBox8.Checked}',Department='{GetDepts()}' where IDUsuario={iduser}"
			ElseIf Button1.Text = "Agregar" Then
				query = $"insert into tblItemsPOUserIDAuthorizations (UserID,Position,Active,Department,Module,Form)
                         values ('{TextBox1.Text}','{TextBox2.Text}','{CheckBox8.Checked}','{GetDepts()}','MLF','modmlf')
                         "
			End If
			If query <> "" Then
				If execQuery(query).ExecuteNonQuery > 0 Then
					cnn.Close()
					MessageBox.Show($"Se a {If(Button1.Text = "Actualizar", "actualizado", "agregado")} usuario con exito")
					iduser = 0
					ClearItems()
					LoadUsers()
					ChangeCaption(False)
				End If
			End If
			cnn.Close()
		Catch ex As Exception
			cnn.Close()
		End Try
	End Sub
	Private ClearItems As Action = Function()
									   CheckBox8.Checked = False
									   TextBox2.Text = ""
									   TextBox1.Text = ""
									   CheckBox7.Checked = False
									   CheckBox6.Checked = False
									   CheckBox5.Checked = False
									   CheckBox4.Checked = False
									   CheckBox3.Checked = False
									   CheckBox2.Checked = False
									   CheckBox1.Checked = False
									   Return Nothing
								   End Function
	Private ChangeCaption As Action(Of Boolean) = Function(resp)
													  Button1.Text = If(resp, "Actualizar", "Agregar")
													  Return Nothing
												  End Function
	Private Sub Usuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		LoadUsers()
		ChangeCaption(False)
	End Sub
	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		iduser = 0
		LoadUsers()
	End Sub
	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		iduser = 0
		ChangeCaption(False)
		ClearItems()
	End Sub
	Private Sub dgvUsers_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUsers.CellContentClick
		If e.RowIndex >= -1 Then
			ClearItems()
			ChangeCaption(True)
			FillItems(e)
		End If
	End Sub
	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If CheckBox7.Checked = False And CheckBox6.Checked = False And CheckBox5.Checked = False And CheckBox4.Checked = False And CheckBox3.Checked = False And CheckBox2.Checked = False And CheckBox1.Checked = False Then
			MessageBox.Show("Debe seleccionar minimo 1 departamento a asignar")
		Else
			If TextBox1.Text = "" And TextBox2.Text = "" Then
				MessageBox.Show("Debe agregar nombre de usuario y puesto")
			Else
				AddOrEditUser()
			End If
		End If
	End Sub
	Private Sub CheckBox7_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox7.CheckedChanged
		If CheckBox7.Checked Then
			CheckBox6.Checked = False
			CheckBox5.Checked = False
			CheckBox4.Checked = False
			CheckBox3.Checked = False
			CheckBox2.Checked = False
			CheckBox1.Checked = False
		End If
	End Sub
End Class