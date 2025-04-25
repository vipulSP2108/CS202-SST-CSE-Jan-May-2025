using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

namespace WindowsFormsAlarmApp
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer timer;
        private DateTime targetTime;
        private Random random = new Random();
        private bool isAlarmSet = false;
        private TextBox textBoxTime;
        private Button buttonStart;

        public Form1()
        {
            // Manually set up the form
            this.Text = "Alarm App";
            this.Size = new Size(300, 200);

            // Create and configure controls
            textBoxTime = new TextBox
            {
                Name = "textBoxTime",
                Location = new Point(50, 50),
                Size = new Size(100, 20)
            };

            buttonStart = new Button
            {
                Name = "buttonStart",
                Text = "Start",
                Location = new Point(50, 80),
                Size = new Size(100, 30)
            };

            // Wire up the button click event
            buttonStart.Click += buttonStart_Click;

            // Add controls to the form
            this.Controls.Add(new Label
            {
                Text = "Enter time (HH:MM:SS):",
                Location = new Point(50, 20),
                AutoSize = true
            });
            this.Controls.Add(textBoxTime);
            this.Controls.Add(buttonStart);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!isAlarmSet)
            {
                // Validate and parse input time
                if (DateTime.TryParse(textBoxTime.Text, out targetTime))
                {
                    targetTime = DateTime.Today.Add(targetTime.TimeOfDay);
                    if (targetTime < DateTime.Now) // If time is in the past, set for tomorrow
                        targetTime = targetTime.AddDays(1);

                    // Disable controls to prevent further input
                    textBoxTime.Enabled = false;
                    buttonStart.Text = "Alarm Set";
                    isAlarmSet = true;

                    // Start timer to check time and change color
                    timer = new System.Timers.Timer(1000);
                    timer.Elapsed += Timer_Tick;
                    timer.AutoReset = true;
                    timer.Start();
                }
                else
                {
                    MessageBox.Show("Invalid time format. Please use HH:MM:SS.", "Error");
                }
            }
        }

        private void Timer_Tick(object sender, ElapsedEventArgs e)
        {
            // Run UI updates on the UI thread
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    this.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

                    // Check if current time matches target time
                    if (DateTime.Now.Hour == targetTime.Hour &&
                        DateTime.Now.Minute == targetTime.Minute &&
                        DateTime.Now.Second == targetTime.Second)
                    {
                        timer.Stop();
                        MessageBox.Show("Alarm! Time's up!", "Alarm");
                        this.BackColor = SystemColors.Control; // Reset color
                        textBoxTime.Enabled = true;
                        buttonStart.Text = "Start";
                        isAlarmSet = false;
                    }
                }));
            }
        }
    }
}