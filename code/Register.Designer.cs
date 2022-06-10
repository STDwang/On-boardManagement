namespace code
{
    partial class Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            this.label1 = new System.Windows.Forms.Label();
            this.st_name = new System.Windows.Forms.TextBox();
            this.st_class = new System.Windows.Forms.TextBox();
            this.st_pay = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.st_register = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(168)))), ((int)(((byte)(180)))));
            this.label1.Location = new System.Drawing.Point(246, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "学生注册";
            // 
            // st_name
            // 
            this.st_name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.st_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.st_name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(217)))), ((int)(((byte)(229)))));
            this.st_name.Location = new System.Drawing.Point(223, 114);
            this.st_name.Margin = new System.Windows.Forms.Padding(2);
            this.st_name.Name = "st_name";
            this.st_name.Size = new System.Drawing.Size(189, 21);
            this.st_name.TabIndex = 1;
            // 
            // st_class
            // 
            this.st_class.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.st_class.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.st_class.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(217)))), ((int)(((byte)(229)))));
            this.st_class.Location = new System.Drawing.Point(223, 181);
            this.st_class.Margin = new System.Windows.Forms.Padding(2);
            this.st_class.Name = "st_class";
            this.st_class.Size = new System.Drawing.Size(189, 21);
            this.st_class.TabIndex = 2;
            // 
            // st_pay
            // 
            this.st_pay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.st_pay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.st_pay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(217)))), ((int)(((byte)(229)))));
            this.st_pay.Location = new System.Drawing.Point(223, 246);
            this.st_pay.Margin = new System.Windows.Forms.Padding(2);
            this.st_pay.Name = "st_pay";
            this.st_pay.Size = new System.Drawing.Size(189, 21);
            this.st_pay.TabIndex = 3;
            this.st_pay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.st_pay_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label2.Location = new System.Drawing.Point(141, 118);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "姓名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label3.Location = new System.Drawing.Point(141, 185);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "专业班级：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(186)))), ((int)(((byte)(199)))));
            this.label4.Location = new System.Drawing.Point(141, 250);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "首冲金额：";
            // 
            // st_register
            // 
            this.st_register.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(71)))));
            this.st_register.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.st_register.ForeColor = System.Drawing.Color.SteelBlue;
            this.st_register.Location = new System.Drawing.Point(267, 311);
            this.st_register.Margin = new System.Windows.Forms.Padding(2);
            this.st_register.Name = "st_register";
            this.st_register.Size = new System.Drawing.Size(100, 35);
            this.st_register.TabIndex = 7;
            this.st_register.Text = "注册";
            this.st_register.UseVisualStyleBackColor = false;
            this.st_register.Click += new System.EventHandler(this.st_register_Click);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(600, 385);
            this.Controls.Add(this.st_register);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.st_pay);
            this.Controls.Add(this.st_class);
            this.Controls.Add(this.st_name);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Register";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox st_name;
        private System.Windows.Forms.TextBox st_class;
        private System.Windows.Forms.TextBox st_pay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button st_register;
    }
}