using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuizWF
{
    public partial class Form1 : Form
    {
        // create Random object for pulling random numbers for quiz
        Random randomizer = new Random();

        // these integer variables store the values for the addition problem
        int addend1;
        int addend2;

        // these integer variables store the values for the subtraction problem
        int minuend;
        int subtrahend;

        // these integer variables store the values for the multiplication problem
        int multiplicand;
        int multiplier;

        // these integer variables store the values for the division problem
        int dividend;
        int divisor;

        // this integer variable keeps track of the time left for quiz
        int timeLeft;

        public void StartTheQuiz()
        {
            // get two random number and fill two fields addition problem
            // by picking 51 it will supply a number between 0 and 50, meaning sum can be 100 max
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            sum.Value = 0;

            // fill in the subtraction problem
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);

            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();

            difference.Value = 0;

            // fill in the multiplication problem
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);

            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();

            product.Value = 0;

            // fill in the division problem
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;

            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();

            quotient.Value = 0;

            // start the timer
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timeLabel.BackColor = Color.Transparent;
            timer1.Start();
        }

        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value) 
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value) 
                && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // if CheckTheAnswer returns True it means the right answer(s) have been entered
                // stop the timer and celebrate this glorious moment with a message box
                timer1.Stop();
                MessageBox.Show("You got all the answers right!", "Congratulations!");
                startButton.Enabled = true;
            }
            else if ((timeLeft > 0) && (timeLeft < 6))
            {
                // update Time Left label with new time left value
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
                timeLabel.BackColor = Color.Red;
            }
            else if (timeLeft > 0)
            {
                // update Time Left label with new time left value
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
            }
            
            else
            {
                // means time ran out, stop the timer and show a message box, fill answers
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time", "Sorry basterd(suiker)!");

                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;

                startButton.Enabled = true;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // select the whole answer in the NumericUpDown control, to avoid it leaves 
            // the 0 in place when typing in  new number
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
    }
}
