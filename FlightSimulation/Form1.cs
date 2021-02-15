using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightSimulation
{
    public partial class Form1 : Form
    {
        BusinessModel model = new BusinessModel();
        bool isPaused = false;
        double seconds = 0;
        int minutes = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button_pause_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled && !isPaused)
            {
                isPaused = true;
                timer1.Stop();
            }
            else if (isPaused)
            {
                isPaused = false;
                timer1.Start();
            }
        }

        private void button_launch_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled && !isPaused)
            {
                chart1.Series[0].Points.Clear();               
                chart1.Series[0].Points.AddXY(0, numeric_height.Value);
                model.GetStarted(numeric_height.Value, numeric_speed.Value,
                    numeric_angle.Value, numeric_weight.Value, numeric_size.Value);
                timer1.Start();
            }
            
        }

        private void UpdateTime()
        {
            if(seconds >= 60)
            {
                seconds = 0;
                minutes++;
                label_minutes.Text = minutes.ToString();
            }
            else
            {
                int secToInt = Convert.ToInt32(seconds);
                label_seconds.Text = secToInt.ToString();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            model.NextTick();
            decimal x = model.GetX();
            decimal y = model.GetY();
            if(y <= 0)
            {
                timer1.Stop();
                chart1.Series[0].Points.AddXY(x, 0);
                seconds = 0;
                minutes = 0;
            }
            else
            {
                seconds += 0.1;
                chart1.Series[0].Points.AddXY(x, y);
            }
            UpdateTime();

        }
    }
}
