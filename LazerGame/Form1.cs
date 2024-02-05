using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LazerGame
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        List<PictureBox> lazers = new List<PictureBox>();
        List<PictureBox> enemies = new List<PictureBox>();
        string yon;
        int _hiz = 20;
        int _lazerHiz = 10;
        int _enemySpeed = 6;
        int score = 0;
        int hak = 3;
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.BackgroundImage = Properties.Resources.backSpace;
            this.Icon = Properties.Resources.Space_Fighter;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (yon == "sag")
            {
                if (shuttlePlayer.Right <= 1830)
                    shuttlePlayer.Left += _hiz;
            }
            if (yon == "sol")
            {
                if (shuttlePlayer.Left >= 10)
                    shuttlePlayer.Left -= _hiz;
            }

            for (int i = 0; i < lazers.Count; i++)
            {
                lazers[i].Top -= _lazerHiz;
                if (lazers[i].Top == -100) { lazers[i].Dispose(); lazers.RemoveAt(i); }
            }

            for (int i = 0; i < enemies.Count; i++) enemies[i].Top += _enemySpeed;

            for (int i = 0; i < lazers.Count; i++)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    if (lazers[i].Bounds.IntersectsWith(enemies[j].Bounds))
                    {
                        lazers[i].Dispose();
                        enemies[j].Dispose();
                        lazers.RemoveAt(i);
                        enemies.RemoveAt(j);

                        score++;
                        labelScore.Text = string.Format("Score: {0}", score);
                        if (score == 100)
                        {
                            timer1.Enabled = false;
                            timer2.Enabled = false;
                            MessageBox.Show("YOU WIN !");
                            Application.Exit();
                        }
                        break;
                    }
                }
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].Top >= 1020)
                {
                    hak--;
                    labelHak.Text = string.Format("HP: {0}", hak);
                    enemies[i].Dispose();
                    enemies.RemoveAt(i);

                    if (hak == 0)
                    {
                        timer1.Enabled = false;
                        timer2.Enabled = false;
                        MessageBox.Show("YOU LOSE !");
                        Application.Exit();
                    }
                }
            }

        }

        private void Fire()
        {
            PictureBox lazer = new PictureBox();
            lazer.Image = Properties.Resources.lazerPNG;
            lazer.SizeMode = PictureBoxSizeMode.StretchImage;
            lazer.Size = new Size(12, 100);
            lazer.Location = new Point(shuttlePlayer.Location.X + 25, shuttlePlayer.Location.Y - 80);


            lazers.Add(lazer);
            this.Controls.Add(lazer);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D) yon = "sag";
            if (e.KeyCode == Keys.A) yon = "sol";
            if (e.KeyCode == Keys.Space) Fire();
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D) yon = string.Empty;
            if (e.KeyCode == Keys.A) yon = string.Empty;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void CreateEnemy()
        {
            PictureBox enemy = new PictureBox();
            enemy.Size = new Size(60, 68);
            enemy.Image = Properties.Resources.enemyShuttlePNG;
            enemy.SizeMode = PictureBoxSizeMode.StretchImage;
            enemy.Location = new Point(rnd.Next(1800), -80);
            enemies.Add(enemy);
            this.Controls.Add(enemy);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            CreateEnemy();
            timer2.Interval = rnd.Next(700, 1600);
        }
    }
}
