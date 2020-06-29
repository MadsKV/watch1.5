using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watch1._2
{
    public partial class Form2 : Form
    {

        //Variabler for Stopuret
        int timeHA, timeMA, timeSA, timeMiA;
        //Variabler for Counteren/Nedtælleren
        int counterHA, counterMA, counterSA, counterMSA;
        //Bool for stopur
        bool isActiveA;
        //Bool for Counter/Nedtælleren
        bool counterActiveA;

        public int CounterHA
        {
            get
            {
                return counterHA;
            }
            set
            {
                counterHA = value;
                if (counterHA >= 24)
                {
                    counterHA = 24;
                }
                if (counterHA <= 0)
                {
                    counterHA = 0;
                }
            }
        }

        public int CounterMA
        {
            get
            {
                return counterMA;
            }
            set
            {
                counterMA = value;
                if (counterMA >= 59)
                {
                    counterMA = 59;
                }
                if (counterMA <= 0)
                {
                    counterMA = 0;
                }
                if (CounterHA >= 1)
                {
                    if (CounterMA == 0)
                    {
                        CounterHA--;
                        CounterMA = 59;
                    }
                }
            }
        }

        public int CounterSA
        {
            get
            {
                return counterSA;
            }
            set
            {
                counterSA = value;
                if (counterSA >= 59)
                {
                    counterSA = 59;
                }
                if (counterSA <= 0)
                {
                    counterSA = 0;
                }

                if (CounterMA >= 1 || CounterHA >= 1)
                {
                    if (CounterSA == 0)
                    {
                        CounterMA--;
                        counterSA = 59;
                    }
                }
            }
        }

        private void TimerStartA_Click(object sender, EventArgs e)
        {
            counterActiveA = true;
        }

        private void TimerStopA_Click(object sender, EventArgs e)
        {
            counterActiveA = false;
        }

        private void TimerResetA_Click(object sender, EventArgs e)
        {
            StopTimerA();
            CounterResetA();
        }

        private void BtnAlarmsA_Click(object sender, EventArgs e)
        {
            Form1 win1 = new Form1();
            win1.Show();
            Form2 win2 = new Form2();
            win2.Close();
        }

        public int CounterMSA
        {
            get
            {
                return counterMSA;
            }
            set
            {
                counterMSA = value;
                if (counterMSA >= 99)
                {
                    counterMSA = 99;
                }
                if (counterMSA <= 0)
                {
                    counterMSA = 0;
                }
                if (CounterMA >= 1 || CounterHA >= 1 || CounterSA >= 1)
                {

                    if (counterMSA == 0)
                    {
                        CounterSA--;
                        counterMSA = 99;
                    }
                }
            }
        }

        private void CounterResetA()
        {
            CounterHA = 0;
            CounterMA = 0;
            CounterSA = 0;
            CounterMSA = 0;
        }

        private void StopTimerA()
        {
            counterActiveA = false;
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void TimerWatchA_Tick(object sender, EventArgs e)
        {
            WatchHA.Text = DateTime.Now.ToString("HH");
            WatchMA.Text = DateTime.Now.ToString("mm");
            WatchSA.Text = DateTime.Now.ToString("ss");
            LblDateA.Text = DateTime.Now.ToString("MMM dd yyyy");
            LblDayA.Text = DateTime.Now.ToString("dddd");
        }

        private void CounterTimerA_Tick(object sender, EventArgs e)
        {
            //Checker om den er aktiv, hvis den er, gå ind i if...
            if (counterActiveA)
            {
                //Trækker fra counterMS og tæller ned
                CounterMSA--;
                if (CounterHA == 0 && CounterMA == 0 && CounterSA == 0)
                {
                    StopTimerA();
                    DialogResult result = MessageBox.Show("Snooze? Add 5 min", "ALARM", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    {
                        switch (result)
                        {
                            case DialogResult.Yes:
                                CounterHA = 0;
                                CounterMA = 5;
                                CounterSA = 0;
                                counterActiveA = true;
                                MessageBox.Show("5 Minutes added");
                                break;
                            case DialogResult.No:
                                break;
                        }
                    }
                }
            }
        }
    }
}
