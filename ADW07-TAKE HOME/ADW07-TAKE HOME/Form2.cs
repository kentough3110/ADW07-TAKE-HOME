using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADW07_TAKE_HOME
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public int size = 0;
        public string answer;
        Button[,] arrButton;
        string[] arrButtons = { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", "Z", "X", "C", "V", "B", "N", "M" };
        string[] arrWords = File.ReadAllText("Wordle Word List.txt").Split(',');
        public int x = 10;
        public int y = 10;
        public int left, top;
        public int tbj = 0, tbi = 0;
        List<string> listofwords = new List<string>();
        private void Form2_Load(object sender, EventArgs e)
        {
            boxMaker();
            result();
        }

        public void boxMaker()
        {
            arrButton = new Button[5, size];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    arrButton[i, j] = new Button();
                    arrButton[i, j].Location = new Point(x,y);
                    arrButton[i, j].Size = new Size(50, 50);
                    y += 60;
                    arrButton[i, j].Tag = i.ToString() + "," + j.ToString();
                    this.Controls.Add(arrButton[i, j]);
                }
                x += 60;
                y = 10;
                
            }

            left = 310;
            top = 40;

            foreach (string word in arrButtons)
            {
                if (word == "A")
                {
                    left = 345;
                    top = 91;
                }
                if (word == "Z")
                {
                    left = 394;
                    top = 142;
                }

                Button button = new Button();
                button.Text = word;
                button.Location = new Point(left, top);
                button.Size = new Size(45, 45);
                button.Click += button_Click;
                this.Controls.Add(button);
                left += 52;
            }

            Button btnEnter = new Button();
            btnEnter.Location = new Point(315, 142);
            btnEnter.Size = new Size(71, 45);
            btnEnter.Text = "Enter";
            btnEnter.Click += btnEnter_Click;
            this.Controls.Add(btnEnter);

            Button btnDelete = new Button();
            btnDelete.Location = new Point(758, 142);
            btnDelete.Size = new Size(70, 45);
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            this.Controls.Add(btnDelete);
        }

        public void result()
        {
            bool isTrue = true;
            Random rnd = new Random();
            while (isTrue)
            {
                
                listofwords = new List<string>();
                foreach (string wordLine in arrWords)
                {
                    listofwords.Add(wordLine);
                }
                int answNum = rnd.Next(0, listofwords.Count - 1);
                answer = listofwords[answNum].ToUpper();
                MessageBox.Show("Jawaban: " + answer, "CHEAT", MessageBoxButtons.OK);
                if (answer.Length == 5)
                {
                    break;
                }
            }
            
        }
        
        private void btnEnter_Click(object sender, EventArgs e)
        {
            int countGreen = 0;
            if (tbi != 5)
            {
                MessageBox.Show("Word must be 5 letters long", "Not Enough", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string kata = "";
                for (int i = 0; i < tbi; i++)
                {
                    kata += arrButton[i, tbj].Text;
                }
                if (listofwords.Contains(kata.ToLower()))
                {
                    for (int i = 0; i < tbi; i++)
                    {
                        if (answer.Contains(arrButton[i, tbj].Text))
                        {
                            arrButton[i, tbj].BackColor = Color.Yellow;
                        }
                        if (answer[i].ToString() == arrButton[i, tbj].Text)
                        {
                            arrButton[i, tbj].BackColor = Color.Green;
                            countGreen++;
                        }
                    }
                    tbj++;

                    if (countGreen == 5)
                    {
                        MessageBox.Show("You won!", "WINNER");
                        this.Close();
                    }
                    else if (countGreen < 5 && tbj == size || tbj == size)
                    {
                        MessageBox.Show("Game over! correct word is " + answer, "GAME OVER");
                        this.Close();
                    }
                    else
                    {
                        tbi = 0;
                    }
                }
                else
                {
                    MessageBox.Show("Word not found in word list", "NOT FOUND", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbi != 0)
            {
                tbi--;
                arrButton[tbi, tbj].Text = "";
            }
        }
        
        private void button_Click(object sender, EventArgs e)
        {
            Button sd = sender as Button;
            if (tbi != 5)
            {
                arrButton[tbi, tbj].Text = sd.Text;
                tbi++;
            }
        }
    }
} 