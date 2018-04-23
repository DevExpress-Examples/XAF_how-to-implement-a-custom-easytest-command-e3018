Imports Microsoft.VisualBasic
Imports System
Imports System.Configuration
Imports System.Windows.Forms

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Win
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl

Namespace AdditionalCommands.Win
	Friend NotInheritable Class Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		Private Sub New()
		End Sub
		<STAThread> _
		Shared Sub Main()
#If EASYTEST Then
			DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register()
#End If
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			'EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
			Dim winApplication As New AdditionalCommandsWindowsFormsApplication()
#If EASYTEST Then
			AddHandler winApplication.CustomizeFormattingCulture, AddressOf winApplication_CustomizeFormattingCulture
#End If
			If ConfigurationManager.ConnectionStrings("EasyTestConnectionString") IsNot Nothing Then
				winApplication.ConnectionString = ConfigurationManager.ConnectionStrings("EasyTestConnectionString").ConnectionString
			End If
			If ConfigurationManager.ConnectionStrings("ConnectionString") IsNot Nothing Then
				winApplication.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
			End If
			Try
				DevExpress.ExpressApp.Xpo.InMemoryDataStoreProvider.Register()
								winApplication.ConnectionString = DevExpress.ExpressApp.Xpo.InMemoryDataStoreProvider.ConnectionString
				winApplication.Setup()
				winApplication.Start()
			Catch e As Exception
				winApplication.HandleException(e)
			End Try
		End Sub
#If EASYTEST Then
		Private Shared Sub winApplication_CustomizeFormattingCulture(ByVal sender As Object, ByVal e As CustomizeFormattingCultureEventArgs)
			e.FormattingCulture = New System.Globalization.CultureInfo("en-US")
		End Sub
#End If
	End Class
End Namespace