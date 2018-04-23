Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
#Region "#usings"
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Forms
#End Region ' #usings

Namespace RemindersFormExample
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
			schedulerControl1.DayView.TopRowTime = New TimeSpan(DateTime.Now.Hour,0,0)
		End Sub

		Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
			schedulerStorage1.Appointments.Add(CreateAppointmentWithReminder())

			' Set time interval to check for active reminders to 3 seconds.
			schedulerStorage1.RemindersCheckInterval = 3000
			' Handle the RemindersFormShowing event.
			AddHandler schedulerControl1.RemindersFormShowing, AddressOf schedulerControl1_RemindersFormShowing
		End Sub
		#Region "#remindersformshowing"
		Private Sub schedulerControl1_RemindersFormShowing(ByVal sender As Object, ByVal e As RemindersFormEventArgs)
			Dim remindersForm As New RemindersForm(CType(sender, SchedulerControl))
			Dim args As New ReminderEventArgs(e.AlertNotifications)
			AddHandler remindersForm.FormClosed, AddressOf remindersForm_FormClosed
			remindersForm.OnReminderAlert(args)
			e.Handled = True
		End Sub
		#End Region ' #remindersformshowing

		Private Sub remindersForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
			' Your code here...
		End Sub

		Private Function CreateAppointmentWithReminder() As Appointment
			' Create a new appointment and set its properties.
			Dim app As New Appointment(DateTime.Now, TimeSpan.FromHours(1))
			app.Subject = "Test appointment"
			app.HasReminder = True
			Return app
		End Function
	End Class
End Namespace