namespace JabboServer
{
    partial class Interface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Interface));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.startButton = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.TextBox();
            this.informationButton = new System.Windows.Forms.Button();
            this.hotelalertButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::JabboServer.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(113, 44);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.DimGray;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.ForeColor = System.Drawing.Color.White;
            this.startButton.Location = new System.Drawing.Point(12, 62);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(113, 202);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // logBox
            // 
            this.logBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logBox.Enabled = false;
            this.logBox.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logBox.Location = new System.Drawing.Point(132, 12);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(350, 252);
            this.logBox.TabIndex = 5;
            // 
            // informationButton
            // 
            this.informationButton.BackColor = System.Drawing.Color.DimGray;
            this.informationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.informationButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.informationButton.ForeColor = System.Drawing.Color.White;
            this.informationButton.Location = new System.Drawing.Point(12, 272);
            this.informationButton.Name = "informationButton";
            this.informationButton.Size = new System.Drawing.Size(113, 23);
            this.informationButton.TabIndex = 7;
            this.informationButton.Text = "Information";
            this.informationButton.UseVisualStyleBackColor = false;
            this.informationButton.Click += new System.EventHandler(this.informationButton_Click);
            // 
            // hotelalertButton
            // 
            this.hotelalertButton.BackColor = System.Drawing.Color.DimGray;
            this.hotelalertButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hotelalertButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hotelalertButton.ForeColor = System.Drawing.Color.White;
            this.hotelalertButton.Location = new System.Drawing.Point(132, 272);
            this.hotelalertButton.Name = "hotelalertButton";
            this.hotelalertButton.Size = new System.Drawing.Size(350, 23);
            this.hotelalertButton.TabIndex = 8;
            this.hotelalertButton.Text = "Broadcast a hotelalert to all users online";
            this.hotelalertButton.UseVisualStyleBackColor = false;
            this.hotelalertButton.Click += new System.EventHandler(this.hotelalertButton_Click);
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::JabboServer.Properties.Resources.bg;
            this.ClientSize = new System.Drawing.Size(494, 307);
            this.Controls.Add(this.hotelalertButton);
            this.Controls.Add(this.informationButton);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Interface";
            this.Opacity = 0.9D;
            this.Text = "JabboServer [C#] Users Online: 0, Rooms loaded: 0";
            this.Load += new System.EventHandler(this.Interface_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.Button informationButton;
        private System.Windows.Forms.Button hotelalertButton;
    }
}