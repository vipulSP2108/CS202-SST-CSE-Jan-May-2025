Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Timers

Public Class Form1
    Private timer As System.Timers.Timer
    Private targetTime As DateTime
    Private random As New Random()
    Private isAlarmSet As Boolean = False
    Private Const PLACEHOLDER_TEXT As String = "Enter time (HH:MM:SS, 24-hour format)"

    Private Sub TextBox1_Enter(sender As Object, e As EventArgs)
        If TextBox1.Text = PLACEHOLDER_TEXT Then
            TextBox1.Text = ""
            TextBox1.ForeColor = SystemColors.WindowText ' Restore normal text color
        End If
    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            TextBox1.Text = PLACEHOLDER_TEXT
            TextBox1.ForeColor = SystemColors.GrayText ' Gray text for placeholder
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not isAlarmSet Then
            ' Check if the text is the placeholder before parsing
            If TextBox1.Text = PLACEHOLDER_TEXT Then
                MessageBox.Show("Please enter a valid time.", "Error")
                Exit Sub
            End If

            ' Validate and parse input time
            If DateTime.TryParse(TextBox1.Text, targetTime) Then
                targetTime = DateTime.Today.Add(targetTime.TimeOfDay)
                If targetTime < DateTime.Now Then ' If time is in the past, set for tomorrow
                    targetTime = targetTime.AddDays(1)
                End If

                ' Disable controls to prevent further input
                TextBox1.Enabled = False
                Button1.Text = "Alarm Set"
                isAlarmSet = True

                ' Start timer to check time and change color
                timer = New System.Timers.Timer(1000)
                AddHandler timer.Elapsed, AddressOf Timer_Tick
                timer.AutoReset = True
                timer.Start()
            Else
                MessageBox.Show("Invalid time format. Please use HH:MM:SS.", "Error")
            End If
        End If
    End Sub

    Private Sub Timer_Tick(sender As Object, e As ElapsedEventArgs)
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(Sub()
                                            Me.BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256))

                                            ' Check if current time matches target time
                                            If DateTime.Now.Hour = targetTime.Hour AndAlso
                                               DateTime.Now.Minute = targetTime.Minute AndAlso
                                               DateTime.Now.Second = targetTime.Second Then
                                                timer.Stop()
                                                MessageBox.Show("Alarm! Time's up!", "Alarm")
                                                Me.BackColor = SystemColors.Control ' Reset color
                                                TextBox1.Enabled = True
                                                Button1.Text = "Start"
                                                isAlarmSet = False
                                                TextBox1.Text = PLACEHOLDER_TEXT ' Restore placeholder
                                                TextBox1.ForeColor = SystemColors.GrayText ' Restore placeholder color
                                            End If
                                        End Sub))
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class