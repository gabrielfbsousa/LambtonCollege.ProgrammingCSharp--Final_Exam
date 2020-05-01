using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reservations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public bool IsValidData()
        {
            
            if(IsPresent(txtArrivalDate, "Arrival Date") && IsPresent(txtDepartureDate, "Departure Date") 
                && IsDateTime(txtArrivalDate, "Arrival Date") && IsDateTime(txtDepartureDate, "Departure Date")
                && IsWithinRange(txtArrivalDate,"ArrivalDate", DateTime.Today, DateTime.Today.AddYears(5))
                && IsWithinRange(txtDepartureDate, "Departure Date", DateTime.Today, DateTime.Today.AddYears(5)))
            {
                return true;
            } else
            {
                return false;
            }
 
        }

        public bool IsPresent(TextBox textBox, string name)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(name + " is a required field.", "Entry Error");
                textBox.Focus();
                return false;
            }
            return true;
        }

        public bool IsDateTime(TextBox textBox, string name)
        {            
            DateTime outputDate;
            if(DateTime.TryParse(textBox.Text, out outputDate))
            {
                return true;
            } else
            {
                MessageBox.Show(name + " format is Wrong. Please, enter a valid Date Time", "Entry Error");
                textBox.Focus();
                return false;
            }
        }

        public bool IsWithinRange(TextBox textBox, string name,
            DateTime min, DateTime max)
        {
            DateTime date = DateTime.Parse(textBox.Text);
            if(date <= max && date >= min)
            {
                return true;
            } else
            {
                MessageBox.Show(name + " range is wrong. Please, enter a date between " 
                    + min + " and "+ max, "Entry Error");
                textBox.Focus();
                return false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            //Get the arrival and departure dates
            

            //Checking if the dates entered are valid
            if (IsValidData())
            {
                DateTime arrivalDate = DateTime.Parse(txtArrivalDate.Text);
                DateTime departureDate = DateTime.Parse(txtDepartureDate.Text);

                //Calculate how many dates have passed since arrival and departure
                TimeSpan datesBetween = departureDate - arrivalDate;
                double daysBetween = datesBetween.Days;

                //Calculating and assigning value if Friday/Saturday or other days
                double daysCounter = 0;
                double totalPrice = 0;

                while(daysCounter < daysBetween)
                {
                    DateTime newDate = arrivalDate.AddDays(daysCounter);
                    DayOfWeek dayOfWeek = newDate.DayOfWeek;

                    if (dayOfWeek == DayOfWeek.Friday || dayOfWeek == DayOfWeek.Saturday)
                    {
                        totalPrice = totalPrice + 150;
                    }
                    else
                    {
                        totalPrice = totalPrice + 120;
                    }


                    daysCounter++;
                }

               // double totalPrice = 120 * daysBetween;
                //Calculate the average price per night
                double averagePricePerNight = totalPrice / daysBetween;

                //Displaying the results
                txtNights.Text = daysCounter.ToString();
                txtTotalPrice.Text = totalPrice.ToString("c");
                txtAvgPrice.Text = averagePricePerNight.ToString("c");

                txtArrivalDate.Focus();
            }         

        }

        //Creating load event
        private void loadEvent(object sender, EventArgs e)
        {
            //Getting the current date and 3 dates from now 
            DateTime currentDate = DateTime.Today;
            DateTime threeDaysFromNow = currentDate.AddDays(3);

            //Showing dates as default on textboxes
            txtArrivalDate.Text = currentDate.ToString("M/d/yyyy");
            txtDepartureDate.Text = threeDaysFromNow.ToString("M/d/yyyy");
        }
    }
}