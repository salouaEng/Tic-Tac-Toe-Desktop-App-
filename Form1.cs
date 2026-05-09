using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_Tac_Toe.Properties;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
           
        }
        enum eWinners {Player1=1,Player2=2,Draw=3, GameInProgress = 4};
        enum ePlayer { Player1,Player2};
         struct stGameStatus {
            public bool GameOver;
            public eWinners Winner;
            public  short PlayCounte;
        };
        ePlayer PlayerTurn = ePlayer.Player1;
        stGameStatus gameStatus;

        bool CheckValue(PictureBox pb1,PictureBox pb2, PictureBox pb3)
        {
            if (pb1.Tag.ToString() != "empty" && pb1.Tag.ToString() == pb2.Tag.ToString() &&  pb3.Tag.ToString()==pb2.Tag.ToString())
            {
                pb1.BackColor = Color.Green;
                pb2.BackColor = Color.Green;
                pb3.BackColor = Color.Green;
                if (pb1.Tag.ToString() == "X")
                {
                    gameStatus.GameOver = true;
                    gameStatus.Winner=eWinners.Player1;
                    EndGame();
                    return true;
                }
                else 
                {
                    gameStatus.GameOver = true;
                    gameStatus.Winner = eWinners.Player2;
                    EndGame();
                    return true;
                }
            }
            else
            {
                gameStatus.GameOver = false;
                return false;
            }
        }

        void EndGame()
        {
            lblTurn.Text = "Game Over";
            gameStatus.GameOver = true;

            switch (gameStatus.Winner)
            {
                case eWinners.Player1:
                    {
                        lblWinner.Text= "Player 1";
                        break;
                    }
                case eWinners.Player2:
                    {
                        lblWinner.Text = "Player 2";
                        break;
                    }
                default:
                    {
                        lblWinner.Text = "Draw";
                        break;
                    }
            }
            MessageBox.Show("GameOver", "GameOver", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        void CheckWinner()
        {
            if (CheckValue(pictureBox1, pictureBox2, pictureBox3))
                return;
            if (CheckValue(pictureBox4, pictureBox5, pictureBox6))
                return;
            if (CheckValue(pictureBox7, pictureBox8, pictureBox9))
                return;
            if (CheckValue(pictureBox1, pictureBox4, pictureBox7))
                return;
            if (CheckValue(pictureBox2, pictureBox5, pictureBox8))
                return;
            if (CheckValue(pictureBox3, pictureBox6, pictureBox9))
                return;
            if (CheckValue(pictureBox1, pictureBox5, pictureBox9))
                return;
            if (CheckValue(pictureBox3, pictureBox5, pictureBox7))
                return;
        }

        void ChangeImage(PictureBox pb)
        {
           if(pb.Tag.ToString() == "empty")
            {
                switch (PlayerTurn)
                {
                    case ePlayer.Player1:
                        {
                            pb.Tag = "X";
                            pb.Image = Resources.X;
                            lblTurn.Text ="Player 2";
                            PlayerTurn = ePlayer.Player2;
                            gameStatus.PlayCounte++;
                            CheckWinner();
                            break;

                        }
                    case ePlayer.Player2:
                        {
                            pb.Tag = "O";
                            pb.Image = Resources.O;
                            lblTurn.Text = "Player 1";
                            PlayerTurn = ePlayer.Player1;
                            gameStatus.PlayCounte++;
                            CheckWinner();

                            break;
                        }
                }
            }
           else
            {
                MessageBox.Show("this box is fall","this box is fall",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (gameStatus.PlayCounte == 9)
            {
                gameStatus.GameOver = true;
                gameStatus.Winner = eWinners.Draw;
                EndGame();
            }
        }

       
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color White = Color.FromArgb(255, 255, 255, 255);

            Pen pen = new Pen(White);
            pen.Width = 10;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(pen, 70, 158, 425, 158);
            e.Graphics.DrawLine(pen, 70, 285, 425, 285);
            e.Graphics.DrawLine(pen, 188, 48, 188, 390);
            e.Graphics.DrawLine(pen, 308, 48, 308, 390);
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            ChangeImage((PictureBox) sender);
        }

       
        private void RestButton(PictureBox btn)
        {
            btn.Image = Resources.QM;
            btn.Tag = "empty";
            btn.BackColor = Color.Black;
            PlayerTurn = ePlayer.Player1;

            lblTurn.Text = "Player 1";
            gameStatus.PlayCounte = 0;
            gameStatus.GameOver = false;
            gameStatus.Winner = eWinners.GameInProgress;
            lblWinner.Text = "In Progress";

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            RestButton(pictureBox1);
            RestButton(pictureBox2);
            RestButton(pictureBox3);
            RestButton(pictureBox4);
            RestButton(pictureBox5);
            RestButton(pictureBox6);
            RestButton(pictureBox7);
            RestButton(pictureBox8);
            RestButton(pictureBox9);

        }

       
    }
}
