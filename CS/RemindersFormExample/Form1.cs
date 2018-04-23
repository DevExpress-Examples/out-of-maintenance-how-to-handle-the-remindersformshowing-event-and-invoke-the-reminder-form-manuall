using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
#region #usings
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Forms;
#endregion #usings

namespace RemindersFormExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            schedulerControl1.Start = DateTime.Today;
            schedulerControl1.DayView.TopRowTime = new TimeSpan(DateTime.Now.Hour-3,0,0);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            schedulerStorage1.Appointments.Items.AddRange(CreateAppointmentsWithReminder());

            // Set time interval to check for active reminders to 3 seconds.
            schedulerStorage1.RemindersCheckInterval = 3000;
            // Handle the RemindersFormShowing event.
            schedulerControl1.RemindersFormShowing += new DevExpress.XtraScheduler.RemindersFormEventHandler(schedulerControl1_RemindersFormShowing);
        }
        #region #remindersformshowing
        void schedulerControl1_RemindersFormShowing(object sender, RemindersFormEventArgs e)
        {
            ReminderAlertNotificationCollection alerts = new ReminderAlertNotificationCollection();
            foreach (ReminderAlertNotification alert in e.AlertNotifications) {
                if (alert.ActualAppointment.StatusKey.ToString() == "1")
                    alerts.Add(alert);
            }
            if (alerts.Count > 0) {
                RemindersForm remindersForm = new RemindersForm((SchedulerControl)sender);
                ReminderEventArgs args = new ReminderEventArgs(alerts);
                remindersForm.FormClosed += new FormClosedEventHandler(remindersForm_FormClosed);
                remindersForm.OnReminderAlert(args);
            }
            e.Handled = true;
        }
        #endregion #remindersformshowing

        void remindersForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Your code here...
        }

        private AppointmentCollection CreateAppointmentsWithReminder()
        {
            AppointmentCollection apts = new AppointmentCollection();
            Appointment apt1 = this.schedulerStorage1.CreateAppointment( AppointmentType.Normal, DateTime.Now.AddHours(-2), TimeSpan.FromHours(1));
            apt1.Subject = "Appointment 1";
            apt1.StatusKey = "0";
            apt1.HasReminder = true;
            apts.Add(apt1);
            Appointment apt2 = this.schedulerStorage1.CreateAppointment(AppointmentType.Normal, DateTime.Now.AddHours(-1), TimeSpan.FromHours(1));
            apt2.Subject = "Appointment 2";
            apt2.StatusKey = "1";
            apt2.HasReminder = true;
            apts.Add(apt2);
            return apts;
        }
    }
}