using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region TempVariable&Arrey
        public int Fig = 1;
        public int NewFig = 0;
        public int Score = 0;
        public int Level = 1;
        public int Lines = 0;
        public int ii = 0;
        public int ConFig = 0;
        Block[] Blk = new Block[4];
        Block[] NewBlk = new Block[4];
        List<Block> Broken = new List<Block>();
        bool[,] space = new bool[21, 14];
        int[] temparr = new int[8];
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 14; i++)
                space[20, i] = true;
            for (int i = 0; i < 21; i++)
                space[i, 13] = true;
            for (int i = 0; i < 21; i++)
                space[i, 0] = true;
            Creat();
            CreatNew();
            PaintNew();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            LevelUp();
            pictureBox1.Image = new Bitmap(300, 450);
            System.Drawing.Graphics G = Graphics.FromImage(pictureBox1.Image);
            if (FreeSpaceDown())
            {
                Blk[0].posY += 15;
                Blk[1].posY += 15;
                Blk[2].posY += 15;
                Blk[3].posY += 15;
            }
            else
            {
                Broken.Add(Blk[0]);
                Broken.Add(Blk[1]);
                Broken.Add(Blk[2]);
                Broken.Add(Blk[3]);
                SpaceBrok();
                Fig = NewFig;
                CreatNew();
                PaintNew();
                Score++;
                if (FallTrue())
                    Delete();
                Creat();
                Loser();
            }
            foreach (Block i in Blk)
                i.Paint(G);
            foreach (Block i in Broken)
                i.Paint(G);
            SpaceBrok();
            PaintNew();
            label1.Text = Score.ToString();
            label6.Text = Lines.ToString();
            label7.Text = Level.ToString();
        }
        #region SomeMethods
        private Color color()
        {
            Random D = new Random();
            int a = D.Next(1, 12);
            if (a == 1)
                return Color.Aqua;
            if (a == 2)
                return Color.MediumBlue;
            if (a == 3)
                return Color.DeepPink;
            if (a == 4)
                return Color.Gold;
            if (a == 5)
                return Color.Red;
            if (a == 6)
                return Color.Silver;
            if (a == 7)
                return Color.Violet;
            if (a == 8)
                return Color.SpringGreen;
            if (a == 9)
                return Color.Magenta;
            if (a == 10)
                return Color.Gray;

            return Color.White;
        }
        private void SpaceBrok()
        {
            for (int i = 0; i < 20; i++)
                for (int j = 1; j < 13; j++)
                    space[i, j] = false;
            foreach (Block a in Broken)
                space[a.posY / 15, a.posX / 15] = true;
        }
        private void Creat()
        {
            if (Fig == 1)
            {
                Blk[0] = new Block(75, -15, color());
                Blk[1] = new Block(Blk[0].posX, Blk[0].posY + 15, color());
                Blk[2] = new Block(Blk[0].posX + 15, Blk[0].posY + 15, color());
                Blk[3] = new Block(Blk[0].posX, Blk[0].posY + 30, color());
                ConFig = 1;
            }
            if (Fig == 2)
            {
                Blk[0] = new Block(75, -15, color());
                Blk[1] = new Block(Blk[0].posX, Blk[0].posY + 15, color());
                Blk[2] = new Block(Blk[0].posX, Blk[0].posY + 30, color());
                Blk[3] = new Block(Blk[0].posX, Blk[0].posY + 45, color());
                ConFig = 1;
            }
            if (Fig == 3)
            {
                Blk[0] = new Block(75, -15, color());
                Blk[1] = new Block(Blk[0].posX + 15, Blk[0].posY, color());
                Blk[2] = new Block(Blk[0].posX, Blk[0].posY + 15, color());
                Blk[3] = new Block(Blk[0].posX + 15, Blk[0].posY + 15, color());
            }
            if (Fig == 4)
            {
                Blk[0] = new Block(75, -15, color());
                Blk[1] = new Block(Blk[0].posX + 15, Blk[0].posY, color());
                Blk[2] = new Block(Blk[0].posX + 15, Blk[0].posY + 15, color());
                Blk[3] = new Block(Blk[0].posX + 30, Blk[0].posY + 15, color());
                ConFig = 1;
            }
            if (Fig == 5)
            {
                Blk[0] = new Block(75, -15, color());
                Blk[1] = new Block(Blk[0].posX + 15, Blk[0].posY, color());
                Blk[2] = new Block(Blk[0].posX, Blk[0].posY + 15, color());
                Blk[3] = new Block(Blk[0].posX - 15, Blk[0].posY + 15, color());
                ConFig = 1;
            }
            if (Fig == 6)
            {
                Blk[0] = new Block(75, -15, color());
                Blk[1] = new Block(Blk[0].posX, Blk[0].posY+15, color());
                Blk[2] = new Block(Blk[0].posX+15, Blk[0].posY + 15, color());
                Blk[3] = new Block(Blk[0].posX + 30, Blk[0].posY+15, color());
                ConFig = 1;
            }
            if (Fig == 7)
            {
                Blk[0] = new Block(75, -15, color());
                Blk[1] = new Block(Blk[0].posX + 15, Blk[0].posY, color());
                Blk[2] = new Block(Blk[0].posX - 15, Blk[0].posY, color());
                Blk[3] = new Block(Blk[0].posX + 15, Blk[0].posY - 15, color());
                ConFig = 1;
            }
        }
        private void Loser()
        {
            foreach (Block a in Broken)
            {
                if (a.posY / 15 == 1)
                {
                    timer1.Enabled = false;
                    Score = 0;
                    Level = 1;
                    Lines = 0;
                    panel1.Visible = false;
                    label1.Text = Score.ToString();
                    label6.Text = Lines.ToString();
                    Broken.Clear();
                    Creat();
                    pictureBox1.Image = null;
                    pictureBox1.Invalidate();
                    for (int i = 0; i < 20; i++)
                        for (int j = 1; j < 13; j++)
                            space[i, j] = false;
                    MessageBox.Show ("Game Over! You Loose!");
                    break;
                }
            }
        }
        private void LevelUp()
        {
            if (Level==1)
            if (Score > 100)
            {
                Level++;
                timer1.Interval = 400;
            }
            if (Level == 2)
                if (Score > 1000)
                {
                    Level++;
                    timer1.Interval = 300;
                }
            if (Level == 3)
                if (Score > 2500)
                {
                    Level++;
                    timer1.Interval = 200;
                }
            if (Level == 4)
                if (Score > 5000)
                {
                    panel1.Visible = true;
                    Level++;
                    timer1.Interval = 150;
                }

        }
        private void CreatNew()
        {
            Random D = new Random();
            NewFig = D.Next(1, 8);
            if (NewFig == 1)
            {
                NewBlk[0] = new Block(15, 15, Color.Orange);
                NewBlk[1] = new Block(NewBlk[0].posX, NewBlk[0].posY - 15, Color.Orange);
                NewBlk[2] = new Block(NewBlk[0].posX, NewBlk[0].posY + 15, Color.Orange);
                NewBlk[3] = new Block(NewBlk[0].posX+15, NewBlk[0].posY, Color.Orange);
            }
            if (NewFig == 2)
            {
                NewBlk[0] = new Block(15, 15, Color.Orange);
                NewBlk[1] = new Block(NewBlk[0].posX, NewBlk[0].posY - 15, Color.Orange);
                NewBlk[2] = new Block(NewBlk[0].posX, NewBlk[0].posY + 15, Color.Orange);
                NewBlk[3] = new Block(NewBlk[0].posX, NewBlk[0].posY + 30, Color.Orange);
            }
            if (NewFig == 3)
            {
                NewBlk[0] = new Block(15, 15, Color.Orange);
                NewBlk[1] = new Block(NewBlk[0].posX + 15, NewBlk[0].posY, Color.Orange);
                NewBlk[2] = new Block(NewBlk[0].posX, NewBlk[0].posY + 15, Color.Orange);
                NewBlk[3] = new Block(NewBlk[0].posX + 15, NewBlk[0].posY + 15, Color.Orange);
            }
            if (NewFig == 4)
            {
                NewBlk[0] = new Block(15, 15, Color.Orange);
                NewBlk[1] = new Block(NewBlk[0].posX - 15, NewBlk[0].posY, Color.Orange);
                NewBlk[2] = new Block(NewBlk[0].posX, NewBlk[0].posY + 15, Color.Orange);
                NewBlk[3] = new Block(NewBlk[0].posX + 15, NewBlk[0].posY + 15, Color.Orange);
            }
            if (NewFig == 5)
            {
                NewBlk[0] = new Block(15, 15, Color.Orange);
                NewBlk[1] = new Block(NewBlk[0].posX + 15, NewBlk[0].posY, Color.Orange);
                NewBlk[2] = new Block(NewBlk[0].posX, NewBlk[0].posY + 15, Color.Orange);
                NewBlk[3] = new Block(NewBlk[0].posX - 15, NewBlk[0].posY + 15, Color.Orange);
            }
            if (NewFig == 6)
            {
                NewBlk[0] = new Block(15, 15, Color.Orange);
                NewBlk[1] = new Block(NewBlk[0].posX - 15, NewBlk[0].posY, Color.Orange);
                NewBlk[2] = new Block(NewBlk[0].posX-15, NewBlk[0].posY - 15, Color.Orange);
                NewBlk[3] = new Block(NewBlk[0].posX + 15, NewBlk[0].posY, Color.Orange);
            }
            if (NewFig == 7)
            {
                NewBlk[0] = new Block(15, 15, Color.Orange);
                NewBlk[1] = new Block(NewBlk[0].posX + 15, NewBlk[0].posY, Color.Orange);
                NewBlk[2] = new Block(NewBlk[0].posX - 15, NewBlk[0].posY, Color.Orange);
                NewBlk[3] = new Block(NewBlk[0].posX + 15, NewBlk[0].posY - 15, Color.Orange);
            }
        }
        private bool FreeSpaceDown()
        {
            foreach (Block a in Blk)
            {
                if (a.posY >= 0)
                {
                    if (space[(a.posY + 15) / 15, a.posX / 15] == true)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool FallTrue()
        {
            for (int i = 19; i > 0; i--)
            {
                if (space[i, 1] == true)
                    if (space[i, 2] == true)
                        if (space[i, 3] == true)
                            if (space[i, 4] == true)
                                if (space[i, 5] == true)
                                    if (space[i, 6] == true)
                                        if (space[i, 7] == true)
                                            if (space[i, 8] == true)
                                                if (space[i, 9] == true)
                                                    if (space[i, 10] == true)
                                                        if (space[i, 11] == true)
                                                            if (space[i, 12] == true)
                                                            {
                                                                ii = i * 15;
                                                                return true;
                                                            }
                continue;
            }
            return false;

        }
        private void PaintNew()
        {
            pictureBox3.Image = new Bitmap(300, 450);
            System.Drawing.Graphics Z = Graphics.FromImage(pictureBox3.Image);
            foreach (Block a in NewBlk)
                a.Paint(Z);
        }
        private bool FreeSpaceLeft()
        {
            foreach (Block a in Blk)
            {
                if (a.posY > 0)
                {
                    if (space[(a.posY) / 15, (a.posX - 15) / 15] == true)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool FreeSpaceRight()
        {
            foreach (Block a in Blk)
            {
                if (a.posY > 0)
                {
                    if (space[(a.posY) / 15, (a.posX + 15) / 15] == true)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void Delete()
        {
            for (int v = 0; v < 12; v++ )
                for (int i = 0; i < Broken.Count(); i++)
                {
                    if (Broken[i].posY == ii)
                    {
                        Broken.RemoveAt(i);
                        break;
                    }
                }
            for (int i = 1; i < 13; i++)
                space[ii/15, i] = false;
            foreach (Block a in Broken)
            {
                if (a.posY < ii)
                    a.posY += 15;
            }
            SpaceBrok();
            Lines += 1;
            Score+=10;
            if (FallTrue())
            {
                Score += 100;
                Delete();
            }
        }
#endregion
        #region Event
        private void label8_MouseDown(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }
        private void label9_MouseDown(object sender, MouseEventArgs e)
        {
            if (timer1.Enabled == false)
                timer1.Enabled = true;
            else
                timer1.Enabled = false;
        }
        private void label10_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
            Score = 0;
            Level = 1;
            Lines = 0;
            panel1.Visible = false;
            label1.Text = Score.ToString();
            label6.Text = Lines.ToString();
            Broken.Clear();
            Creat();
            pictureBox1.Image = null;
            pictureBox1.Invalidate();
            for (int i = 0; i < 20; i++)
                for (int j = 1; j < 13; j++)
                    space[i, j] = false;
        }
        private void label11_MouseDown(object sender, MouseEventArgs e)
        {
            if (timer1.Enabled == true)
            {
                timer1.Enabled = false;
                Form2 f = new Form2();
                f.Owner = this;
                f.ShowDialog();
                timer1.Enabled = true;
            }
            else
            {
                Form2 f = new Form2();
                f.Owner = this;
                f.ShowDialog();
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (timer1.Enabled == false)
                    timer1.Enabled = true;
                else
                    timer1.Enabled = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                int f = 0;
                int[] z = new int[9];
                foreach (Block a in Blk)
                {
                    z[f] = a.posX; z[f+1] = a.posY;
                    f += 2;
                }
                z[8] = ConFig;
                Rotate();
                foreach (Block a in Blk)
                {
                    if (a.posY >= 0)
                    if (space[a.posY / 15, a.posX / 15] == true)
                    {
                        Blk[0].posX = z[0];
                        Blk[0].posY = z[1];
                        Blk[1].posX = z[2];
                        Blk[1].posY = z[3];
                        Blk[2].posX = z[4];
                        Blk[2].posY = z[5];
                        Blk[3].posX = z[6];
                        Blk[3].posY = z[7];
                        ConFig = z[8];
                        break;
                    }
                }
                pictureBox1.Image = new Bitmap(300, 450);
                System.Drawing.Graphics G = Graphics.FromImage(pictureBox1.Image);
                foreach (Block i in Blk)
                    i.Paint(G);
                foreach (Block i in Broken)
                    i.Paint(G);
            }
            if (e.KeyCode == Keys.Left)
            {
                if (FreeSpaceLeft()&& Blk[0].posY>=0)
                {
                    Blk[0].posX -= 15;
                    Blk[1].posX -= 15;
                    Blk[2].posX -= 15;
                    Blk[3].posX -= 15;
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                if (FreeSpaceRight() && Blk[0].posY >= 0)
                {
                    Blk[0].posX += 15;
                    Blk[1].posX += 15;
                    Blk[2].posX += 15;
                    Blk[3].posX += 15;
                }
            }
            if (e.KeyCode == Keys.Down)
                timer1.Interval = 50;
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (Level == 1)
                    timer1.Interval = 500;
                if (Level == 2)
                    timer1.Interval = 400;
                if (Level == 3)
                    timer1.Interval = 300;
                if (Level == 4)
                    timer1.Interval = 200;
                if (Level == 5)
                    timer1.Interval = 150;
            }
        }
        #endregion
        # region RotateMethods
        private void Rotate()
        {
            if (Fig == 1)
                Rotate1();
            if (Fig == 2)
                Rotate2();
            if (Fig == 4)
                Rotate4();
            if (Fig == 5)
                Rotate5();
            if (Fig == 6)
                Rotate6();
            if (Fig == 7)
                Rotate7();
        }
        private void Rotate1()
        {
            if (ConFig == 1)
            {
                Blk[1].posX = Blk[0].posX - 15;
                Blk[1].posY = Blk[0].posY;
                Blk[2].posX = Blk[0].posX + 15;
                Blk[2].posY = Blk[0].posY;
                Blk[3].posX = Blk[0].posX;
                Blk[3].posY = Blk[0].posY + 15;
                ConFig = 2;
            }
            else if (ConFig == 2)
            {
                Blk[1].posX = Blk[0].posX;
                Blk[1].posY = Blk[0].posY + 15;
                Blk[2].posX = Blk[0].posX - 15;
                Blk[2].posY = Blk[0].posY + 15;
                Blk[3].posX = Blk[0].posX;
                Blk[3].posY = Blk[0].posY + 30;
                ConFig = 3;
            }
            else if (ConFig == 3)
            {
                Blk[1].posX = Blk[0].posX - 15;
                Blk[1].posY = Blk[0].posY + 15;
                Blk[2].posX = Blk[0].posX;
                Blk[2].posY = Blk[0].posY + 15;
                Blk[3].posX = Blk[0].posX + 15;
                Blk[3].posY = Blk[0].posY + 15;
                ConFig = 4;
            }
            else if (ConFig == 4)
            {
                Blk[1].posX = Blk[0].posX;
                Blk[1].posY = Blk[0].posY + 15;
                Blk[2].posX = Blk[0].posX + 15;
                Blk[2].posY = Blk[0].posY + 15;
                Blk[3].posX = Blk[0].posX;
                Blk[3].posY = Blk[0].posY + 30;
                ConFig = 1;
            }
        }
        private void Rotate2()
        {
            if (ConFig == 1)
            {
                Blk[1].posX = Blk[0].posX - 15;
                Blk[1].posY = Blk[0].posY;
                Blk[2].posX = Blk[0].posX + 15;
                Blk[2].posY = Blk[0].posY;
                Blk[3].posX = Blk[0].posX + 30;
                Blk[3].posY = Blk[0].posY;
                ConFig = 2;
            }
            else if (ConFig == 2)
            {
                Blk[1].posX = Blk[0].posX;
                Blk[1].posY = Blk[0].posY + 15;
                Blk[2].posX = Blk[0].posX;
                Blk[2].posY = Blk[0].posY + 30;
                Blk[3].posX = Blk[0].posX;
                Blk[3].posY = Blk[0].posY + 45;
                ConFig = 1;
            }

        }
        private void Rotate4()
        {
            if (ConFig == 1)
            {
                Blk[0].posX += 30; ;
                Blk[1].posY += 30;
                ConFig = 2;
            }
            else if (ConFig == 2)
            {
                Blk[0].posX -= 30; ;
                Blk[1].posY -= 30;
                ConFig = 1;
            }

        }
        private void Rotate5()
        {
            if (ConFig == 1)
            {
                Blk[0].posY += 30; ;
                Blk[1].posX -= 30;
                ConFig = 2;
            }
            else if (ConFig == 2)
            {
                Blk[0].posY -= 30; ;
                Blk[1].posX += 30;
                ConFig = 1;
            }

        }
        private void Rotate6()
        {
            if (ConFig == 1)
            {
                Blk[1].posX = Blk[0].posX+15;
                Blk[1].posY = Blk[0].posY;
                Blk[2].posX = Blk[0].posX;
                Blk[2].posY = Blk[0].posY+15;
                Blk[3].posX = Blk[0].posX;
                Blk[3].posY = Blk[0].posY + 30;
                ConFig = 2;
            }
            else if (ConFig == 2)
            {
                Blk[1].posX = Blk[0].posX;
                Blk[1].posY = Blk[0].posY+15;
                Blk[2].posX = Blk[0].posX-15;
                Blk[2].posY = Blk[0].posY;
                Blk[3].posX = Blk[0].posX-30;
                Blk[3].posY = Blk[0].posY;
                ConFig = 3;
            }
            else if (ConFig == 3)
            {
                Blk[1].posX = Blk[0].posX+15;
                Blk[1].posY = Blk[0].posY+15;
                Blk[2].posX = Blk[0].posX;
                Blk[2].posY = Blk[0].posY+15;
                Blk[3].posX = Blk[0].posX;
                Blk[3].posY = Blk[0].posY-15;
                ConFig = 4;
            }
            else if (ConFig == 4)
            {
                Blk[1].posX = Blk[0].posX;
                Blk[1].posY = Blk[0].posY + 15;
                Blk[2].posX = Blk[0].posX+15;
                Blk[2].posY = Blk[0].posY + 15;
                Blk[3].posX = Blk[0].posX+30;
                Blk[3].posY = Blk[0].posY + 15;
                ConFig = 1;
            }

        }
        private void Rotate7()
        {
            if (ConFig == 1)
            {
                Blk[1].posX = Blk[0].posX;
                Blk[1].posY = Blk[0].posY+15;
                Blk[2].posX = Blk[0].posX;
                Blk[2].posY = Blk[0].posY - 15;
                Blk[3].posX = Blk[0].posX+15;
                Blk[3].posY = Blk[0].posY + 15;
                ConFig = 2;
            }
            else if (ConFig == 2)
            {
                Blk[1].posX = Blk[0].posX-15;
                Blk[1].posY = Blk[0].posY;
                Blk[2].posX = Blk[0].posX - 15;
                Blk[2].posY = Blk[0].posY+15;
                Blk[3].posX = Blk[0].posX +15;
                Blk[3].posY = Blk[0].posY;
                ConFig = 3;
            }
            else if (ConFig == 3)
            {
                Blk[1].posX = Blk[0].posX - 15;
                Blk[1].posY = Blk[0].posY - 15;
                Blk[2].posX = Blk[0].posX;
                Blk[2].posY = Blk[0].posY - 15;
                Blk[3].posX = Blk[0].posX;
                Blk[3].posY = Blk[0].posY + 15;
                ConFig = 4;
            }
            else if (ConFig == 4)
            {
                Blk[1].posX = Blk[0].posX+15;
                Blk[1].posY = Blk[0].posY;
                Blk[2].posX = Blk[0].posX -15;
                Blk[2].posY = Blk[0].posY;
                Blk[3].posX = Blk[0].posX+15;
                Blk[3].posY = Blk[0].posY - 15;
                ConFig = 1;
            }
        }
        #endregion


    }
    public class Block
    {
        public Block(int X, int Y, Color C)
        {
            posX = X;
            posY = Y;
            Col = C;
        }
        public int posX;
        public int posY;
        public Color Col;
        public void Paint(Graphics g)
        {
            Point q = new Point(posX+3, posY);
            Point w = new Point(posX + 12, posY);
            Point r = new Point(posX + 15, posY+3);
            Point y = new Point(posX + 15, posY + 12);
            Point t = new Point(posX + 12, posY + 15);
            Point u = new Point(posX + 3, posY + 15);
            Point i = new Point(posX, posY + 12);
            Point p = new Point(posX, posY + 3);
            Point o = new Point(posX + 3, posY);
            Point[] z = { q, w, r, y, t, u, i, p, o };
            SolidBrush one = new SolidBrush(Color.Black);
            SolidBrush two = new SolidBrush(Col);
            g.FillPolygon(one, z);
            g.FillRectangle(two, posX + 1.82F, posY + 1.82F, 11.2F, 11.2F);
        }
    }
}