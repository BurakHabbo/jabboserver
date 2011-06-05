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
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.logBox = new System.Windows.Forms.TextBox();
            this.informationButton = new System.Windows.Forms.Button();
            this.hotelalertButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // logoBox
            // 
            this.logoBox.BackColor = System.Drawing.Color.Transparent;
            this.logoBox.Image = global::JabboServer.Properties.Resources.logo;
            this.logoBox.Location = new System.Drawing.Point(12, 12);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(113, 44);
            this.logoBox.TabIndex = 0;
            this.logoBox.TabStop = false;
            // 
            // logBox
            // 
            this.logBox.BackColor = System.Drawing.SystemColors.Control;
            this.logBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logBox.Enabled = false;
            this.logBox.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logBox.Location = new System.Drawing.Point(288, 12);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(350, 252);
            this.logBox.TabIndex = 5;
            this.logBox.MouseLeave += new System.EventHandler(this.logBox_MouseLeave);
            this.logBox.MouseHover += new System.EventHandler(this.logBox_MouseHover);
            // 
            // informationButton
            // 
            this.informationButton.BackColor = System.Drawing.Color.DimGray;
            this.informationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.informationButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.informationButton.ForeColor = System.Drawing.Color.White;
            this.informationButton.Location = new System.Drawing.Point(288, 299);
            this.informationButton.Name = "informationButton";
            this.informationButton.Size = new System.Drawing.Size(350, 23);
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
            this.hotelalertButton.Location = new System.Drawing.Point(288, 270);
            this.hotelalertButton.Name = "hotelalertButton";
            this.hotelalertButton.Size = new System.Drawing.Size(350, 23);
            this.hotelalertButton.TabIndex = 8;
            this.hotelalertButton.Text = "Broadcast a hotelalert to all users online";
            this.hotelalertButton.UseVisualStyleBackColor = false;
            this.hotelalertButton.Click += new System.EventHandler(this.hotelalertButton_Click);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.Transparent;
            this.startButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("startButton.BackgroundImage")));
            this.startButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.startButton.FlatAppearance.BorderSize = 0;
            this.startButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.startButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("Verdana", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.ForeColor = System.Drawing.Color.White;
            this.startButton.Location = new System.Drawing.Point(12, 90);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(180, 120);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            this.startButton.MouseLeave += new System.EventHandler(this.startButton_MouseLeave);
            this.startButton.MouseHover += new System.EventHandler(this.startButton_MouseHover);
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::JabboServer.Properties.Resources.panel;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(650, 354);
            this.ControlBox = false;
            this.Controls.Add(this.hotelalertButton);
            this.Controls.Add(this.informationButton);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.logoBox);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Interface";
            this.Opacity = 0.9D;
            this.Text = "JabboServer [C#] - Users online: 0 - Rooms loaded: 0";
            this.Load += new System.EventHandler(this.Interface_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.Button informationButton;
        private System.Windows.Forms.Button hotelalertButton;
        private System.Windows.Forms.Button startButton;
    }
}