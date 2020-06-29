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
    public partial class Form1 : Form
    {
        //Variabler for Stopuret
        int timeH, timeM, timeS, timeMi;
        //Variabler for Counteren/Nedtælleren
        int counterH, counterM, counterS, counterMS;
        //Bool for stopur
        bool isActive;
        //Bool for Counter/Nedtælleren
        bool counterActive;

        public int CounterH
        {
            get 
            { 
                return counterH; 
            }
            set
            {
                counterH = value;
                if (counterH >= 24)
                {
                    counterH = 24;
                }
                if (counterH <= 0)
                {
                    counterH = 0;
                }
            }
        }

        public int CounterM
        {
            get 
            { 
                return counterM;
            }
            set
            {
                    counterM = value;
                if (counterM >= 59)
                {
                    counterM = 59;
                }
                if (counterM <= 0)
                {
                    counterM = 0;
                }
                if (CounterH >= 1)
                {
                    if (CounterM == 0)
                    {
                    CounterH--;
                    CounterM = 59;
                    }
                }
            }
        }

        public int CounterS
        {
            get 
            { 
                return counterS; 
            }
            set
            {
                    counterS = value;
                if(counterS >= 59)
                {
                    counterS = 59;
                }
                if (counterS <= 0)
                {
                    counterS = 0;
                }
                
                if (CounterM >= 1 || CounterH >= 1)
                {
                    if (CounterS == 0)
                    {
                    CounterM--;
                    counterS = 59;
                    }
                }
            }
        }

        public int CounterMS
        {
            get
            {
                return counterMS;
            }
            set
            {
                counterMS = value;
                if (counterMS >= 99)
                {
                    counterMS = 99;
                }
                if (counterMS <= 0)
                {
                    counterMS = 0;
                }
                if (CounterM >= 1 || CounterH >= 1 || CounterS >= 1)
                {
                    
                    if (counterMS == 0)
                    {   
                        CounterS--;
                        counterMS = 99; 
                    }
                }
            }
        }

        //Funktion til at restarte timerne på stopuret
        private void ResetTime()
        {
            timeH = 0;
            timeM = 0;
            timeS = 0;
            timeMi = 0;
        }
        //Funktion til at restarte timerne på Counteren/Nedtælleren
        private void CounterReset()
        {
            CounterH = 0;
            CounterM = 0;
            CounterS = 0;
            CounterMS = 0;
        }
        //Stopurets funktion
        private void TimerStopwatch_Tick(object sender, EventArgs e)
        {
            //Checker om den er aktiv, hvis den er, gå ind i If...
            if (isActive)
            {
                //Læg oven i timeMi
                timeMi++;

                //Hvis timeMI er 100 gå ind i if....
                if (timeMi >= 100)
                {
                    //Læg oven i timeS og reset timeMI
                    timeS++;
                    timeMi = 0;

                    //hvis timeS er 60 gå ind i if....
                    if (timeS >= 60)
                    {
                        //Læg oven i timeM og reset timeS
                        timeM++;
                        timeS = 0;

                        //hvis timeM er 60 gå ind i if...
                        if (timeM >= 60)
                        {
                            //Læg oven i timeH og reset timeM
                            timeH++;
                            timeM = 0;
                        }
                    }
                }
            }
            //Funktion til at vælge format af uret
            DrawTime();
        }

        //Stopurets startknap
        private void StartBtn_Click(object sender, EventArgs e)
        {
            isActive = true;
        }
        private void LapBtn_Click(object sender, EventArgs e)
        {
            LapBox.Items.Add(timeH + ":" + timeM + ":" + timeS + ":" + timeMi);

        }
        //Stopurets stopknap
        private void StopBtn_Click(object sender, EventArgs e)
        {
            isActive = false;
        }
        //Stopurets resetknap
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            isActive = false;
            //Funktion til at restarte timerne på stopuret
            ResetTime();
        }
        //Sætter det "Reel" ur til nuværene tid og dato.
        private void TimerWatch_Tick(object sender, EventArgs e)
        {
            WatchH.Text = DateTime.Now.ToString("HH");
            WatchM.Text = DateTime.Now.ToString("mm");
            WatchS.Text = DateTime.Now.ToString("ss");
            LblDate.Text = DateTime.Now.ToString("MMM dd yyyy");
            LblDay.Text = DateTime.Now.ToString("dddd");
        }
        //Counteren/Nedtællerens funktion
        private void CounterTimer_Tick(object sender, EventArgs e)
        {
            //Checker om den er aktiv, hvis den er, gå ind i if...
            if (counterActive)
            {
                //Trækker fra counterMS og tæller ned
                CounterMS--;
                if (CounterH == 0 && CounterM == 0 && CounterS == 0)
                {
                    StopTimer();
                    DialogResult result = MessageBox.Show("Snooze? Add 5 min", "ALARM", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    {
                        switch (result)
                        {
                            case DialogResult.Yes:
                                CounterH = 0;
                                CounterM = 5;
                                CounterS = 0;
                                counterActive = true;
                                MessageBox.Show("5 Minutes added");
                                break;
                            case DialogResult.No:
                                break;
                        }
                    }
                }
            }
            //Funktion til at resette Counteren/Nedtælleren
            CountTimer();
        }

        //Counteren/Nedtællerens startknap
        private void TimerStart_Click(object sender, EventArgs e)
        {
            counterActive = true;
        }

        //Lægger 1 til counterH
        private void AddH_Click(object sender, EventArgs e)
        {
            CounterH++;

        }

        //Trækker 1 fra counterH
        private void MinusH_Click(object sender, EventArgs e)
        {
            CounterH--;
        }

        //Lægger 1 til counterM
        private void AddM_Click(object sender, EventArgs e)
        {
            CounterM++;
            if (CounterM >= 60)
            {
                CounterH++;
            }
        }

        //Trækker 1 fra counterM
        private void MinusM_Click(object sender, EventArgs e)
        {
            CounterM--;
        }

        //Lægger 1 til counterS
        private void AddS_Click(object sender, EventArgs e)
        {
            CounterS++;
            if (CounterS >= 60)
            {
                CounterM++;
            }
        }

        //Trækker 1 fra counterS
        private void MinusS_Click(object sender, EventArgs e)
        {
            CounterS--;
        }

        //Counteren/Nedtællerens stopknap
        private void TimerStop_Click(object sender, EventArgs e)
        {
            StopTimer();
        }

        private void WatchM_Click(object sender, EventArgs e)
        {

        }

        private void LapBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnAlarm_Click(object sender, EventArgs e)
        {
            AlarmBox.Items.Add(CounterH + ":" + CounterM + ":" + CounterS + ":" + CounterMS);
        }

        private void BtnAlarms_Click(object sender, EventArgs e)
        {
            Form2 win2 = new Form2();
            win2.Show();
            Form1 win1 = new Form1();
            win1.Close();
        }

        private void AlarmBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ClearLap_Click(object sender, EventArgs e)
        {
            LapBox.Items.Clear();
        }

        private void ClearAlarm_Click(object sender, EventArgs e)
        {
            AlarmBox.Items.Clear();
        }

        private void WatchH_Click(object sender, EventArgs e)
        {

        }

        //Counteren/Nedtællerens resetknap
        private void TimerReset_Click(object sender, EventArgs e)
        {
            StopTimer();
            CounterReset();
        }

        private void StopTimer()
        {
            counterActive = false;
        }
        //Sætter stopurets decimaler og format
        private void DrawTime()
        {
            TimerMi.Text = String.Format("{0:00}", timeMi);
            TimerS.Text = String.Format("{0:00}", timeS);
            TimerM.Text = String.Format("{0:00}", timeM);
            TimerH.Text = String.Format("{0:00}", timeH);
        }

        //Sætter counteren/nedtællerens decimaler og format
        private void CountTimer()
        {
            CounterMili.Text = String.Format("{0:00}", CounterMS);
            CounterSeconds.Text = String.Format("{0:00}", CounterS);
            CounterMinutes.Text = String.Format("{0:00}", CounterM);
            CounterHours.Text = String.Format("{0:00}", CounterH);
        }

        //Selve Formen
        private void Form1_Load(object sender, EventArgs e)
        {
            //ResetTime();
            
            //isActive = false;
        }

        public Form1()

        {
            InitializeComponent();
        }
    }
}
