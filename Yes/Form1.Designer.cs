namespace Yes
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
            TimerLair1 = new System.Windows.Forms.Timer(components);
            TimerLair2 = new System.Windows.Forms.Timer(components);
            TimerLair3 = new System.Windows.Forms.Timer(components);
            make1 = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            label2 = new Label();
            moving = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // TimerLair1
            // 
            TimerLair1.Tick += timer1_Tick;
            // 
            // TimerLair2
            // 
            TimerLair2.Tick += timer1_Tick_1;
            // 
            // TimerLair3
            // 
            TimerLair3.Tick += timer2_Tick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(711, 378);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(714, 349);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 1;
            label2.Text = "label2";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 800);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            KeyPress += Form1_KeyPress;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer TimerLair1;
        private System.Windows.Forms.Timer TimerLair2;
        private System.Windows.Forms.Timer TimerLair3;
        private System.Windows.Forms.Timer make1;
        private Label label1;
        private Label label2;
        private System.Windows.Forms.Timer moving;
    }
}