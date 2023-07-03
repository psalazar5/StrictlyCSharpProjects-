using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StopWatch
{
    public partial class StopWatch : Form
    {
        private DateTime startTime;
        private TimeSpan elapsedTime;
        public StopWatch()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            //// Set a value to start time 
            //startTime = DateTime.Now;

            ////Start the timer 
            //formTimer.Start();

            if (!formTimer.Enabled)
            {
                startTime = DateTime.Now - elapsedTime; //startTime is a variable type of "DateTime" that stores the starting time of the stopwatch when the clicking the start button.
                formTimer.Start(); 
            }

        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            //formTimer.Stop();

            if (formTimer.Enabled)
            {
                formTimer.Stop();
                elapsedTime = DateTime.Now - startTime;/*datetime.now returns the current date and time. 
                                    * line 32 calculates the duration of time that has elapsed since starting the sw, it subtracts 'startTime' from the current time ('DateTime.Now') to get the difference*/
            }
        }

        private void resetButton_Click(object sender, EventArgs e)  
        {
            formTimer.Stop();
            elapsedTime = TimeSpan.Zero;
            watchLabel.Text = "00:00.00";
        }

        private void formTimer_Tick(object sender, EventArgs e)
        {
            // Calculate how long its been since start 
            TimeSpan span = DateTime.Now - startTime;
            watchLabel.Text = span.ToString(@"mm\:ss\.ff");
            
            
        }
    }
}
