namespace Memory_Boxes_WinForms.Menu
{
    partial class SizeDialogForm
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
            this.dialogTablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.confirmSizeChoiceButton = new System.Windows.Forms.Button();
            this.cancelSizeChoiceButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dialogTablePanel
            // 
            this.dialogTablePanel.ColumnCount = 8;
            this.dialogTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.dialogTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.dialogTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.dialogTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.dialogTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.dialogTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.dialogTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.dialogTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.dialogTablePanel.Location = new System.Drawing.Point(12, 12);
            this.dialogTablePanel.Name = "dialogTablePanel";
            this.dialogTablePanel.RowCount = 6;
            this.dialogTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.dialogTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.dialogTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.dialogTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.dialogTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.dialogTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.dialogTablePanel.Size = new System.Drawing.Size(320, 240);
            this.dialogTablePanel.TabIndex = 0;
            // 
            // confirmSizeChoiceButton
            // 
            this.confirmSizeChoiceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.confirmSizeChoiceButton.Location = new System.Drawing.Point(94, 267);
            this.confirmSizeChoiceButton.Name = "confirmSizeChoiceButton";
            this.confirmSizeChoiceButton.Size = new System.Drawing.Size(75, 23);
            this.confirmSizeChoiceButton.TabIndex = 1;
            this.confirmSizeChoiceButton.Text = "Confirm";
            this.confirmSizeChoiceButton.UseVisualStyleBackColor = true;
            this.confirmSizeChoiceButton.Click += new System.EventHandler(this.confirmSizeChoiceButton_Click);
            // 
            // cancelSizeChoiceButton
            // 
            this.cancelSizeChoiceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelSizeChoiceButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelSizeChoiceButton.Location = new System.Drawing.Point(175, 267);
            this.cancelSizeChoiceButton.Name = "cancelSizeChoiceButton";
            this.cancelSizeChoiceButton.Size = new System.Drawing.Size(75, 23);
            this.cancelSizeChoiceButton.TabIndex = 2;
            this.cancelSizeChoiceButton.Text = "Cancel";
            this.cancelSizeChoiceButton.UseVisualStyleBackColor = true;
            // 
            // SizeDialogForm
            // 
            this.AcceptButton = this.confirmSizeChoiceButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelSizeChoiceButton;
            this.ClientSize = new System.Drawing.Size(344, 302);
            this.ControlBox = false;
            this.Controls.Add(this.cancelSizeChoiceButton);
            this.Controls.Add(this.confirmSizeChoiceButton);
            this.Controls.Add(this.dialogTablePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SizeDialogForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  Choose the size of the playing field";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel dialogTablePanel;
        private System.Windows.Forms.Button confirmSizeChoiceButton;
        private System.Windows.Forms.Button cancelSizeChoiceButton;
    }
}