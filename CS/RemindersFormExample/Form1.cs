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
            schedulerControl1.DayView.TopRowTime = new TimeSpan(DateTime.Now.Hour,0,0);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            schedulerStorage1.Appointments.Add(CreateAppointmentWithReminder());

            // Set time interval to check for active reminders to 3 seconds.
            schedulerStorage1.RemindersCheckInterval = 3000;
            // Handle the RemindersFormShowing event.
            schedulerControl1.RemindersFormShowing += new DevExpress.XtraScheduler.RemindersFormEventHandler(schedulerControl1_RemindersFormShowing);
        }
        #region #remindersformshowing
        void schedulerControl1_RemindersFormShowing(object sender, RemindersFormEventArgs e)
        {
            RemindersForm remindersForm = new RemindersForm((SchedulerControl)sender);
            ReminderEventArgs args = new ReminderEventArgs(e.AlertNotifications);
            remindersForm.FormClosed += new FormClosedEventHandler(remindersForm_FormClosed);
            remindersForm.OnReminderAlert(args);
            e.Handled = true;
        }
        #endregion #remindersformshowing

        void remindersForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Your code here...
        }

        private Appointment CreateAppointmentWithReminder()
        {
            // Create a new appointment and set its properties.
            Appointment app = new Appointment(DateTime.Now, TimeSpan.FromHours(1));
            app.Subject = "Test appointment";
            app.HasReminder = true;
            return app;
        }
    }
}