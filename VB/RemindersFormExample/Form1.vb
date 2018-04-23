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
            schedulerControl1.Start = Date.Today
            schedulerControl1.DayView.TopRowTime = New TimeSpan(Date.Now.Hour-3,0,0)
        End Sub

        Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
            schedulerStorage1.Appointments.Items.AddRange(CreateAppointmentsWithReminder())

            ' Set time interval to check for active reminders to 3 seconds.
            schedulerStorage1.RemindersCheckInterval = 3000
            ' Handle the RemindersFormShowing event.
            AddHandler schedulerControl1.RemindersFormShowing, AddressOf schedulerControl1_RemindersFormShowing
        End Sub
        #Region "#remindersformshowing"
        Private Sub schedulerControl1_RemindersFormShowing(ByVal sender As Object, ByVal e As RemindersFormEventArgs)
            Dim alerts As New ReminderAlertNotificationCollection()
            For Each alert As ReminderAlertNotification In e.AlertNotifications
                If alert.ActualAppointment.StatusKey.ToString() = "1" Then
                    alerts.Add(alert)
                End If
            Next alert
            If alerts.Count > 0 Then
                Dim remindersForm As New RemindersForm(DirectCast(sender, SchedulerControl))
                Dim args As New ReminderEventArgs(alerts)
                AddHandler remindersForm.FormClosed, AddressOf remindersForm_FormClosed
                remindersForm.OnReminderAlert(args)
            End If
            e.Handled = True
        End Sub
        #End Region ' #remindersformshowing

        Private Sub remindersForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
            ' Your code here...
        End Sub

        Private Function CreateAppointmentsWithReminder() As AppointmentCollection
            Dim apts As New AppointmentCollection()
            Dim apt1 As Appointment = Me.schedulerStorage1.CreateAppointment(AppointmentType.Normal, Date.Now.AddHours(-2), TimeSpan.FromHours(1))
            apt1.Subject = "Appointment 1"
            apt1.StatusKey = "0"
            apt1.HasReminder = True
            apts.Add(apt1)
            Dim apt2 As Appointment = Me.schedulerStorage1.CreateAppointment(AppointmentType.Normal, Date.Now.AddHours(-1), TimeSpan.FromHours(1))
            apt2.Subject = "Appointment 2"
            apt2.StatusKey = "1"
            apt2.HasReminder = True
            apts.Add(apt2)
            Return apts
        End Function
    End Class
End Namespace