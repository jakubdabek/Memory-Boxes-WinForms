﻿namespace Memory_Boxes_WinForms
{
    partial class Form1
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.TitlePanel = new System.Windows.Forms.Panel();
            this.titleRainbowTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // TitlePanel
            // 
            this.TitlePanel.Location = new System.Drawing.Point(129, 28);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new System.Drawing.Size(269, 92);
            this.TitlePanel.TabIndex = 1;
            this.TitlePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // titleRainbowTimer
            // 
            this.titleRainbowTimer.Interval = 750;
            this.titleRainbowTimer.Tick += new System.EventHandler(this.titleRainbowTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 312);
            this.Controls.Add(this.TitlePanel);
            this.Name = "Form1";
            this.Text = "Memory boxes";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel TitlePanel;
        private System.Windows.Forms.Timer titleRainbowTimer;
    }
}

