﻿namespace laba5
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pbMain = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            txtLog = new RichTextBox();
            txtCount = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)pbMain).BeginInit();
            SuspendLayout();
            // 
            // pbMain
            // 
            pbMain.Location = new Point(12, 12);
            pbMain.Name = "pbMain";
            pbMain.Size = new Size(530, 342);
            pbMain.TabIndex = 0;
            pbMain.TabStop = false;
            pbMain.Paint += pbMain_Paint;
            pbMain.MouseClick += pbMain_MouseClick;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 30;
            timer1.Tick += timer1_Tick;
            // 
            // txtLog
            // 
            txtLog.Location = new Point(561, 12);
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(177, 342);
            txtLog.TabIndex = 1;
            txtLog.Text = "";
            // 
            // txtCount
            // 
            txtCount.BorderStyle = BorderStyle.None;
            txtCount.Location = new Point(429, 23);
            txtCount.Name = "txtCount";
            txtCount.Size = new Size(101, 44);
            txtCount.TabIndex = 2;
            txtCount.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(750, 366);
            Controls.Add(txtCount);
            Controls.Add(txtLog);
            Controls.Add(pbMain);
            Name = "Form1";
            Text = "Лабораторная работа №5";
            ((System.ComponentModel.ISupportInitialize)pbMain).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbMain;
        private System.Windows.Forms.Timer timer1;
        private RichTextBox txtLog;
        private RichTextBox txtCount;
    }
}
