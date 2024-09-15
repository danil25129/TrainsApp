namespace Tickets
{
    partial class RegisterForm
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
            this.emailTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.passTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nameTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.famTB = new System.Windows.Forms.TextBox();
            this.AdminCheck = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 257);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(451, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // emailTB
            // 
            this.emailTB.Location = new System.Drawing.Point(120, 142);
            this.emailTB.Margin = new System.Windows.Forms.Padding(5);
            this.emailTB.Name = "emailTB";
            this.emailTB.Size = new System.Drawing.Size(350, 28);
            this.emailTB.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Почта";
            // 
            // passTB
            // 
            this.passTB.Location = new System.Drawing.Point(120, 104);
            this.passTB.Margin = new System.Windows.Forms.Padding(5);
            this.passTB.Name = "passTB";
            this.passTB.Size = new System.Drawing.Size(350, 28);
            this.passTB.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "Пароль";
            // 
            // nameTB
            // 
            this.nameTB.Location = new System.Drawing.Point(120, 66);
            this.nameTB.Margin = new System.Windows.Forms.Padding(5);
            this.nameTB.Name = "nameTB";
            this.nameTB.Size = new System.Drawing.Size(350, 28);
            this.nameTB.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 24);
            this.label2.TabIndex = 9;
            this.label2.Text = "Имя";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 24);
            this.label3.TabIndex = 10;
            this.label3.Text = "Фамилия";
            // 
            // famTB
            // 
            this.famTB.Location = new System.Drawing.Point(120, 24);
            this.famTB.Name = "famTB";
            this.famTB.Size = new System.Drawing.Size(350, 28);
            this.famTB.TabIndex = 11;
            // 
            // AdminCheck
            // 
            this.AdminCheck.AutoSize = true;
            this.AdminCheck.Location = new System.Drawing.Point(19, 198);
            this.AdminCheck.Name = "AdminCheck";
            this.AdminCheck.Size = new System.Drawing.Size(143, 28);
            this.AdminCheck.TabIndex = 12;
            this.AdminCheck.Text = "Я сотрудник";
            this.AdminCheck.UseVisualStyleBackColor = true;
            this.AdminCheck.CheckedChanged += new System.EventHandler(this.AdminCheck_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(168, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(172, 24);
            this.label5.TabIndex = 13;
            this.label5.Text = "Табельный номер";
            this.label5.Visible = false;
            // 
            // tabTB
            // 
            this.tabTB.Location = new System.Drawing.Point(345, 200);
            this.tabTB.Name = "tabTB";
            this.tabTB.Size = new System.Drawing.Size(113, 28);
            this.tabTB.TabIndex = 14;
            this.tabTB.Visible = false;
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.tabTB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.AdminCheck);
            this.Controls.Add(this.famTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nameTB);
            this.Controls.Add(this.passTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.emailTB);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "RegisterForm";
            this.Size = new System.Drawing.Size(492, 322);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox emailTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox famTB;
        private System.Windows.Forms.CheckBox AdminCheck;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tabTB;
    }
}