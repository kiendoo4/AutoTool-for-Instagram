using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Timers;
using System.Security.Cryptography;

namespace SpaceShooter
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer spawn;
        private System.Windows.Forms.Timer obMove;
        private System.Windows.Forms.Timer shoot;
        private System.Windows.Forms.Timer impact;
        List<Meteor> meteors = new List<Meteor>();
        List<PictureBox> missiles = new List<PictureBox>();

        private void InitializeTimer()
        {
            spawn = new System.Windows.Forms.Timer();
            spawn.Interval = 2000;
            spawn.Tick += spawn_Tick;
            spawn.Start();

            obMove = new System.Windows.Forms.Timer();
            obMove.Interval = 25;
            obMove.Tick += Move_Tick;
            obMove.Start();

            shoot = new System.Windows.Forms.Timer();
            shoot.Interval = 1000;
            shoot.Tick += shoot_Tick;
            shoot.Start();

            impact = new System.Windows.Forms.Timer();
            impact.Interval = 25;
            impact.Tick += Impact_Tick;
            impact.Start();
        }

        private void spawn_Tick(object sender, EventArgs e)
        {
            meteors.Add(new Meteor());
            Controls.Add(meteors.Last().picMeteor);
        }

        private void Move_Tick(object sender, EventArgs e)
        {
            //Meteors part
            for (int i = meteors.Count - 1; i >= 0; i--)
            {
                meteors[i].picMeteor.Location = new Point(meteors[i].picMeteor.Location.X, meteors[i].picMeteor.Location.Y + 5);
                try
                {
                    if (meteors[i].picMeteor.Location.Y > 800)
                    {
                        meteors[i].picMeteor.Dispose();
                        meteors.RemoveAt(i);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //Missiles part
            for (int i = missiles.Count - 1; i >= 0; i--)
            {
                missiles[i].Location = new Point(missiles[i].Location.X, missiles[i].Location.Y - 7);
                try
                {
                    if (missiles[i].Location.Y < -40)
                    {
                        missiles[i].Dispose();
                        missiles.RemoveAt(i);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void shoot_Tick(object sender, EventArgs e)
        {
            PictureBox missile = new PictureBox();
            missile.SizeMode = PictureBoxSizeMode.AutoSize;
            missile.BackColor = Color.Transparent;
            missile.Image = Image.FromFile("./images/Missile.png");
            missile.Location = new Point(pictureShip.Location.X + pictureShip.Size.Width / 2 - missile.Width / 2, pictureShip.Location.Y);
            missiles.Add(missile);
            Controls.Add(missiles.Last());
        }

        private void Impact_Tick(object sender, EventArgs e)
        {
            try
            {
                for (int i = meteors.Count - 1; i >= 0; i--)
                {
                    Rectangle mete = new Rectangle(meteors[i].picMeteor.Location, meteors[i].picMeteor.Size);
                    Rectangle ship = new Rectangle(pictureShip.Location, pictureShip.Size);
                    if (mete.IntersectsWith(ship))
                    {
                        meteors[i].picMeteor.Dispose();
                        meteors.RemoveAt(i);
                        MessageBox.Show("You lose!");
                        LoadGame();
                        break;
                    }

                    for (int j = missiles.Count - 1; j >= 0; j--)
                    {
                        Rectangle rect1 = new Rectangle(meteors[i].picMeteor.Location, meteors[i].picMeteor.Size);
                        Rectangle rect2 = new Rectangle(missiles[j].Location, missiles[j].Size);

                        if (rect1.IntersectsWith(rect2))
                        {
                            meteors[i].picMeteor.Dispose();
                            meteors.RemoveAt(i);
                            missiles[j].Dispose();
                            missiles.RemoveAt(j);
                            labelPoint.Text = (Convert.ToInt32(labelPoint.Text) + 1).ToString();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Form1()
        {
            InitializeComponent();
            LoadGame();
            InitializeTimer();
        }

        private void LoadGame()
        {
            try
            {
                //Meteors clear part
                for (int i = meteors.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        meteors[i].picMeteor.Dispose();
                        meteors.RemoveAt(i);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                //Missiles clear part
                for (int i = missiles.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        missiles[i].Dispose();
                        missiles.RemoveAt(i);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                labelPoint.Text = "0";
                pictureShip.Location = new Point(535, 610);
                pictureShip.Image = Image.FromFile("./images/SpaceShip.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right && pictureShip.Right < 1150) pictureShip.Location = new System.Drawing.Point(pictureShip.Location.X + 10, pictureShip.Location.Y);
            if (e.KeyCode == Keys.Left && pictureShip.Left > 50) pictureShip.Location = new System.Drawing.Point(pictureShip.Location.X - 10, pictureShip.Location.Y);
        }
    }

    public class Meteor
    {
        public PictureBox picMeteor;
        public Point center;
        public Meteor()
        {
            Random random = new Random();
            picMeteor = new PictureBox();
            picMeteor.SizeMode = PictureBoxSizeMode.AutoSize;
            picMeteor.BackColor = Color.Transparent;
            picMeteor.Image = Image.FromFile($"./images/spaceMeteors_{random.Next(1, 4)}.png");
            center = new Point(random.Next(100, 1100), 100 - picMeteor.Height / 2);
            picMeteor.Location = new Point(center.X - picMeteor.Width / 2, center.Y - picMeteor.Height / 2);
        }
    }
}
