using NumeroTextBox;

namespace Treenilaskuri
{
    partial class Treenilaskuri
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
            this.button1 = new System.Windows.Forms.Button();
            this.timeTextBox1 = new AikaTextBox.TimeTextBox();
            this.numberTextBox1 = new NumeroTextBox.NumberTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.paceLabel1 = new TahtiLabel.PaceLabel();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(138, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Laske";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timeTextBox1
            // 
            this.timeTextBox1.Location = new System.Drawing.Point(19, 19);
            this.timeTextBox1.Name = "timeTextBox1";
            this.timeTextBox1.Size = new System.Drawing.Size(100, 22);
            this.timeTextBox1.TabIndex = 0;
            this.timeTextBox1.Text = "00:00:00";
            this.timeTextBox1.TextChanged += new System.EventHandler(this.timeTextBox1_TextChanged);
            // 
            // numberTextBox1
            // 
            this.numberTextBox1.Location = new System.Drawing.Point(19, 64);
            this.numberTextBox1.Name = "numberTextBox1";
            this.numberTextBox1.Size = new System.Drawing.Size(100, 22);
            this.numberTextBox1.TabIndex = 2;
            this.numberTextBox1.Text = "0";
            this.numberTextBox1.TextChanged += new System.EventHandler(this.numberTextBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "aika (hh:mm:ss)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "matka (km)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "vauhti:";
            // 
            // paceLabel1
            // 
            this.paceLabel1.AutoSize = true;
            this.paceLabel1.distance = 0D;
            this.paceLabel1.Location = new System.Drawing.Point(16, 157);
            this.paceLabel1.Name = "paceLabel1";
            this.paceLabel1.Size = new System.Drawing.Size(64, 17);
            this.paceLabel1.TabIndex = 3;
            this.paceLabel1.Text = "0 min/km";
            this.paceLabel1.time = System.TimeSpan.Parse("00:00:00");
            this.paceLabel1.Fast += new TahtiLabel.PaceLabel.GoFast(this.paceLabel1_Fast);
            this.paceLabel1.Average += new TahtiLabel.PaceLabel.GoAverage(this.paceLabel1_Average);
            this.paceLabel1.Slow += new TahtiLabel.PaceLabel.GoSlow(this.paceLabel1_Slow);
            // 
            // Treenilaskuri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(282, 255);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.paceLabel1);
            this.Controls.Add(this.numberTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.timeTextBox1);
            this.Name = "Treenilaskuri";
            this.Text = this.Name;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AikaTextBox.TimeTextBox timeTextBox1;
        private System.Windows.Forms.Button button1;
        private NumeroTextBox.NumberTextBox numberTextBox1;
        private TahtiLabel.PaceLabel paceLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

