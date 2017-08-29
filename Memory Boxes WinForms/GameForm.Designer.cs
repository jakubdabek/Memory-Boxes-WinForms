namespace Memory_Boxes_WinForms.Game
{
    partial class GameForm
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
            if(disposing && (components != null))
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.mainLoopTimer = new System.Windows.Forms.Timer(this.components);
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.timeDisplayLabel = new System.Windows.Forms.Label();
            this.pausePlayImageList = new System.Windows.Forms.ImageList(this.components);
            this.pausePlayButton = new System.Windows.Forms.Button();
            this.timeDisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // mainLoopTimer
            // 
            this.mainLoopTimer.Tick += new System.EventHandler(this.mainLoopTimer_Tick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "bison.png");
            this.imageList.Images.SetKeyName(1, "camel.png");
            this.imageList.Images.SetKeyName(2, "owl.png");
            this.imageList.Images.SetKeyName(3, "puma.png");
            this.imageList.Images.SetKeyName(4, "rhino.png");
            this.imageList.Images.SetKeyName(5, "toucan.png");
            this.imageList.Images.SetKeyName(6, "moose.png");
            // 
            // timeDisplayLabel
            // 
            this.timeDisplayLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeDisplayLabel.AutoSize = true;
            this.timeDisplayLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.timeDisplayLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.timeDisplayLabel.Location = new System.Drawing.Point(243, 17);
            this.timeDisplayLabel.Name = "timeDisplayLabel";
            this.timeDisplayLabel.Size = new System.Drawing.Size(39, 15);
            this.timeDisplayLabel.TabIndex = 1;
            this.timeDisplayLabel.Text = "0:00.0";
            // 
            // pausePlayImageList
            // 
            this.pausePlayImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("pausePlayImageList.ImageStream")));
            this.pausePlayImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.pausePlayImageList.Images.SetKeyName(0, "Pause.png");
            this.pausePlayImageList.Images.SetKeyName(1, "Play.png");
            // 
            // pausePlayButton
            // 
            this.pausePlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pausePlayButton.ImageKey = "Pause.png";
            this.pausePlayButton.ImageList = this.pausePlayImageList;
            this.pausePlayButton.Location = new System.Drawing.Point(189, 12);
            this.pausePlayButton.Name = "pausePlayButton";
            this.pausePlayButton.Size = new System.Drawing.Size(41, 23);
            this.pausePlayButton.TabIndex = 0;
            this.pausePlayButton.UseVisualStyleBackColor = true;
            // 
            // timeDisplayTimer
            // 
            this.timeDisplayTimer.Tick += new System.EventHandler(this.timeTimer_Tick);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 320);
            this.Controls.Add(this.timeDisplayLabel);
            this.Controls.Add(this.pausePlayButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameForm_FormClosed);
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer mainLoopTimer;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label timeDisplayLabel;
        private System.Windows.Forms.ImageList pausePlayImageList;
        private System.Windows.Forms.Button pausePlayButton;
        private System.Windows.Forms.Timer timeDisplayTimer;
    }
}