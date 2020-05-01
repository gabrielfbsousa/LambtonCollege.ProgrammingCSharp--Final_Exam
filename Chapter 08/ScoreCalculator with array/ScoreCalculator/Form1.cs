using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int total = 0;
        int count = 0;
        //Creating array of scores up to 20 scores
        int[] scores = new int[20];

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int score = Convert.ToInt32(txtScore.Text);
            total += score;
            //Adding the score entered to the Array
            scores[count] = score;
            count += 1;
            int average = total / count;
            txtScoreTotal.Text = total.ToString();
            txtScoreCount.Text = count.ToString();
            txtAverage.Text = average.ToString();
            txtScore.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Removing any scores assigned to the array
            int[] clearScores = new int[20];
            scores = clearScores;

            total = 0;
            count = 0;
            txtScore.Text = "";
            txtScoreTotal.Text = "";
            txtScoreCount.Text = "";
            txtAverage.Text = "";
            txtScore.Focus();

        }

        private void btnDisplayScores_Click(object sender, EventArgs e)
        {
            //Sort array
            Array.Sort(scores);

            //Create the String to display
            string stringScores = "";
            for(int i = 0; i < scores.Length; i++)
            {
                if (scores[i] != 0)
                {
                    stringScores += scores[i].ToString() + "\n";
                }
            }

            //Displaying the scores
            MessageBox.Show(stringScores, "Sorted Scores");

            //Move focus back to the Score textbox
            txtScore.Focus();

        }
    }
}