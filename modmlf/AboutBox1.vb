Public NotInheritable Class AboutBox1

	Private Sub AboutBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		' Set the title of the form.
		Dim ApplicationTitle As String
		If My.Application.Info.Title <> "" Then
			ApplicationTitle = My.Application.Info.Title
		Else
			ApplicationTitle = IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
		End If
		Me.Text = String.Format("About {0}", ApplicationTitle)
		' Initialize all of the text displayed on the About Box.
		' TODO: Customize the application's assembly information in the "Application" pane of the project 
		'    properties dialog (under the "Project" menu).
		Me.LabelProductName.Text = "MLF Software" 'My.Application.Info.ProductName
		If ApplicationDeployment.IsNetworkDeployed Then
			With ApplicationDeployment.CurrentDeployment.CurrentVersion
				Me.LabelVersion.Text = String.Format("Version {0}", .Major & "." & .Minor & "." & .Build & "." & .Revision)
			End With
		Else
			Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
		End If
		Me.LabelCopyright.Text = "© SPECIALIZED HARNESS PRODUCTS S DE RL DE CV All Rights Reserved. Oct 2021" 'My.Application.Info.Copyright
		Me.LabelCompanyName.Text = "SPECIALIZED HARNESS PRODUCTS S DE RL DE CV" 'My.Application.Info.CompanyName
		Me.TextBoxDescription.Text = "MLF is the software to request work orders in the cutting and MP area, following up from the creation of the work order until its completion. In the same way, follow up on short materials and workloads per machine using time as a standard measure for its KPI measurement." 'My.Application.Info.Description
	End Sub

	Private Sub OKButton_Click(sender As Object, e As EventArgs) Handles OKButton.Click
		Me.Close()
	End Sub
End Class
