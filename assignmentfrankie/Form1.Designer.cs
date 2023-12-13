namespace assignmentfrankie
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            player = new PictureBox();
            txtScore = new Label();
            gameTimer = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
            SuspendLayout();
            // 
            // player
            // 
            player.Image = Properties.Resources.tank;
            player.Location = new Point(373, 579);
            player.Margin = new Padding(4, 3, 4, 3);
            player.Name = "player";
            player.Size = new Size(66, 58);
            player.SizeMode = PictureBoxSizeMode.StretchImage;
            player.TabIndex = 0;
            player.TabStop = false;
            // 
            // txtScore
            // 
            txtScore.AutoSize = true;
            txtScore.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            txtScore.ForeColor = Color.White;
            txtScore.Location = new Point(4, 609);
            txtScore.Margin = new Padding(4, 0, 4, 0);
            txtScore.Name = "txtScore";
            txtScore.Size = new Size(88, 24);
            txtScore.TabIndex = 1;
            txtScore.Text = "Score: 0";
            // 
            // gameTimer
            // 
            gameTimer.Interval = 20;
            gameTimer.Tick += mainGameTimerEvent;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(646, 9);
            label1.Name = "label1";
            label1.Size = new Size(198, 45);
            label1.TabIndex = 2;
            label1.Text = "Space to shoot\r\nLeft and Right arrows for movement\r\nEnter to restart\r\n";
            label1.Click += label1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(856, 647);
            Controls.Add(label1);
            Controls.Add(txtScore);
            Controls.Add(player);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Reaction Time Training";
            KeyDown += keyisdown;
            KeyUp += keyisup;
            ((System.ComponentModel.ISupportInitialize)player).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox player;
        private Label txtScore;
        private System.Windows.Forms.Timer gameTimer;
        private Label label1;
    }
}
