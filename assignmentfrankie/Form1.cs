using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignmentfrankie
{
    public partial class Form1 : Form
    {
        // Player movement variables
        bool goLeft, goRight;

        // Speed variables
        int playerSpeed = 6;
        int enemySpeed = 5;

        // Scoring variables
        int score = 0;

        // Timers and intervals
        int enemyBulletTimer = 300;
        int spawnTimer = 20000;  // New row spawn timer

        // Arrays for game elements
        PictureBox[] enemyArray;

        // Lists to keep track of bullets
        private List<PictureBox> enemyBulletsList = new List<PictureBox>();

        // Game state variables
        bool shooting;
        bool isGameOver;

        public Form1()
        {
            InitializeComponent();
            gameSetup();
            gameTimer.Tick += new EventHandler(mainGameTimerEvent);
        }

        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            // Update the score display
            txtScore.Text = "Score: " + score;

            // Player movement
            if (goLeft)
            {
                player.Left -= playerSpeed;
            }

            if (goRight)
            {
                player.Left += playerSpeed;
            }

            // Decrease timer for enemy bullets
            enemyBulletTimer -= 10;

            // Check and spawn new enemy bullets
            if (enemyBulletTimer < 1)
            {
                enemyBulletTimer = 300;  // Adjust the frequency here, increase the value for less frequent shooting

                // Make sure there are fewer than 5 enemy bullets before adding a new one
                if (enemyBulletsList.Count < 5)
                {
                    makeBullet("enemyBullet");
                }
            }

            // Increase the number of enemy bullets and spawn new row of invaders every 20 seconds
            if (spawnTimer <= 0)
            {
                spawnTimer = 20000;  // Reset the timer

                // Make sure there are fewer than 5 enemy bullets before adding a new one
                if (enemyBulletsList.Count < 5)
                {
                    makeBullet("enemyBullet");
                }

                // Spawn a new row of invaders
                spawnEnemies();
            }
            else
            {
                spawnTimer -= gameTimer.Interval;  // Decrease the timer
            }

            // Remove enemy bullets that go out of bounds
            enemyBulletsList.RemoveAll(bullet => bullet.Top > 620);

            // Iterate through game elements
            foreach (Control x in this.Controls)
            {
                // Update position of enemies
                if (x is PictureBox && (string)x.Tag == "Enemy")
                {
                    x.Left += enemySpeed;

                    // Move enemy to new row if it reaches the right edge
                    if (x.Left > 730)
                    {
                        x.Top += 65;
                        x.Left = -80;

                        // Randomly shoot new enemy bullets
                        if (new Random().Next(0, 10) < 3)
                        {
                            // Make sure there are fewer than 5 enemy bullets before adding a new one
                            if (enemyBulletsList.Count < 5)
                            {
                                makeBullet("enemyBullet");
                            }
                        }
                    }

                    // Check for collision with player
                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        // Player is hit by enemy, trigger game over
                        gameOver("Game Over! You've been hit by an enemy.");
                    }

                    // Check for collision with player bullets
                    foreach (Control y in this.Controls)
                    {
                        if (y is PictureBox && (string)y.Tag == "bullet")
                        {
                            if (y.Bounds.IntersectsWith(x.Bounds))
                            {
                                // Remove enemy and player bullet, increase score, and reset shooting flag
                                this.Controls.Remove(x);
                                this.Controls.Remove(y);
                                score += 1;
                                shooting = false;
                            }
                        }
                    }
                }

                // Update position of player bullets
                if (x is PictureBox && (string)x.Tag == "bullet")
                {
                    x.Top -= 20;

                    // Remove player bullet if it goes out of bounds
                    if (x.Top < 15)
                    {
                        this.Controls.Remove(x);
                        shooting = false;
                    }
                }

                // Update position of enemy bullets
                if (x is PictureBox && (string)x.Tag == "enemyBullet")
                {
                    PictureBox enemyBullet = (PictureBox)x;

                    // Make enemy bullets faster
                    enemyBullet.Top += 5;

                    // Check for collision with player
                    if (enemyBullet.Bounds.IntersectsWith(player.Bounds))
                    {
                        // Player is hit by enemy bullet, trigger game over
                        this.Controls.Remove(enemyBullet);
                        gameOver("Game Over! You've been hit by an enemy bullet.");
                        return;
                    }
                }
            }

            // Player wins the game when the score reaches 30
            if (score >= 30)
            {
                gameOver("Congratulations! You've won the game!");
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            // Handle key down events
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            // Handle key up events
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Space && shooting == false)
            {
                // Shoot player bullet
                shooting = true;
                makeBullet("bullet");
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                // Restart the game
                removeAll();
                gameSetup();
            }
        }

        private void makeEnemies()
        {
            // Initialize the array of enemies
            enemyArray = new PictureBox[50];

            int left = 0;

            // Create and position enemy PictureBoxes
            for (int i = 0; i < enemyArray.Length; i++)
            {
                enemyArray[i] = new PictureBox();
                enemyArray[i].Size = new Size(60, 50);
                enemyArray[i].Image = Properties.Resources.inavders;
                enemyArray[i].Top = 5;
                enemyArray[i].Tag = "Enemy";
                enemyArray[i].Left = left;
                enemyArray[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(enemyArray[i]);
                left = left - 80;
            }
        }

        private void spawnEnemies()
        {
            // Spawn a new row of enemies
            int left = 0;

            for (int i = 0; i < enemyArray.Length; i++)
            {
                PictureBox enemy = new PictureBox();
                enemy.Size = new Size(60, 50);
                enemy.Image = Properties.Resources.inavders;
                enemy.Top = 5;
                enemy.Tag = "Enemy";
                enemy.Left = left;
                enemy.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(enemy);
                left = left - 80;
            }
        }

        private void gameSetup()
        {
            // Set up the initial game state
            txtScore.Text = "Score: 0";
            score = 0;
            isGameOver = false;

            enemyBulletTimer = 300;
            shooting = false;

            makeEnemies();
            gameTimer.Start();
        }

        private void gameOver(string message)
        {
            // Handle the game over condition
            isGameOver = true;
            gameTimer.Stop();
            txtScore.Text = "Score: " + score + " " + message;
        }

        private void removeAll()
        {
            // Remove all PictureBoxes from the form
            foreach (PictureBox i in enemyArray)
            {
                this.Controls.Remove(i);
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "bullet" || (string)x.Tag == "enemyBullet")
                    {
                        this.Controls.Remove(x);
                    }
                }
            }
        }

        private void makeBullet(string bulletTag)
        {
            // Create and position a new bullet
            PictureBox bullet = new PictureBox();
            bullet.Image = Properties.Resources.bullet;
            bullet.Size = new Size(5, 20);
            bullet.Tag = bulletTag;
            bullet.Left = player.Left + player.Width / 2;

            if ((string)bullet.Tag == "bullet")
            {
                bullet.Top = player.Top - 20;
            }
            else if ((string)bullet.Tag == "enemyBullet")
            {
                bullet.Top = -100;
            }

            this.Controls.Add(bullet);
            bullet.BringToFront();

            // Add the new bullet to the appropriate list
            if ((string)bullet.Tag == "enemyBullet")
            {
                enemyBulletsList.Add(bullet);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
