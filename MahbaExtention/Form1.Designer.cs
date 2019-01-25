namespace MahbaExtention
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imageListView1 = new Njit.ImageListView.ImageListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMaghta = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAddDocs = new Glass.GlassButton();
            this.btnScans = new Glass.GlassButton();
            this.btnSave = new Glass.GlassButton();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFatherName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReshte = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFamily = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNN = new System.Windows.Forms.TextBox();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "همه فایل ها|*.*";
            this.openFileDialog1.Multiselect = true;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.Controls.Add(this.imageListView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1029, 733);
            this.panel2.TabIndex = 4;
            // 
            // imageListView1
            // 
            this.imageListView1.BackgroundImage = global::MahbaExtention.Properties.Resources.background;
            this.imageListView1.DefaultImage = ((System.Drawing.Image)(resources.GetObject("imageListView1.DefaultImage")));
            this.imageListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageListView1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imageListView1.ErrorImage")));
            this.imageListView1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageListView1.Location = new System.Drawing.Point(0, 0);
            this.imageListView1.Margin = new System.Windows.Forms.Padding(4);
            this.imageListView1.Name = "imageListView1";
            this.imageListView1.Size = new System.Drawing.Size(1029, 733);
            this.imageListView1.TabIndex = 0;
            this.imageListView1.Text = "";
            this.imageListView1.ThumbnailSize = new System.Drawing.Size(450, 450);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtMaghta);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnAddDocs);
            this.panel1.Controls.Add(this.btnScans);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtFatherName);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtReshte);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtFamily);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtPN);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtNN);
            this.panel1.Controls.Add(this.shapeContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1029, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(325, 733);
            this.panel1.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label7.Location = new System.Drawing.Point(262, 343);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(45, 19);
            this.label7.TabIndex = 25;
            this.label7.Text = "مقطع";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtMaghta
            // 
            this.txtMaghta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtMaghta.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtMaghta.Location = new System.Drawing.Point(17, 362);
            this.txtMaghta.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaghta.Name = "txtMaghta";
            this.txtMaghta.Size = new System.Drawing.Size(286, 27);
            this.txtMaghta.TabIndex = 13;
            this.txtMaghta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMaghta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaghta_KeyDown);
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::MahbaExtention.Properties.Resources.Mahba;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.Location = new System.Drawing.Point(34, 638);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 72);
            this.button1.TabIndex = 22;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnAddDocs
            // 
            this.btnAddDocs.BackColor = System.Drawing.Color.Red;
            this.btnAddDocs.Enabled = false;
            this.btnAddDocs.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDocs.ForeColor = System.Drawing.Color.Black;
            this.btnAddDocs.Location = new System.Drawing.Point(18, 566);
            this.btnAddDocs.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddDocs.Name = "btnAddDocs";
            this.btnAddDocs.Size = new System.Drawing.Size(288, 54);
            this.btnAddDocs.TabIndex = 19;
            this.btnAddDocs.Text = " (F9)  افزودن اسناد پرونده ";
            this.btnAddDocs.Click += new System.EventHandler(this.btnAddDocs_Click);
            // 
            // btnScans
            // 
            this.btnScans.BackColor = System.Drawing.Color.Red;
            this.btnScans.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScans.ForeColor = System.Drawing.Color.Black;
            this.btnScans.Location = new System.Drawing.Point(18, 504);
            this.btnScans.Margin = new System.Windows.Forms.Padding(4);
            this.btnScans.Name = "btnScans";
            this.btnScans.Size = new System.Drawing.Size(288, 54);
            this.btnScans.TabIndex = 20;
            this.btnScans.Text = "(F5)  اسکن اسناد پرونده  ";
            this.btnScans.Click += new System.EventHandler(this.btnScans_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SaddleBrown;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(18, 442);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(288, 54);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = " (F1)  ذخیره پرونده";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label6.Location = new System.Drawing.Point(253, 225);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label6.Size = new System.Drawing.Size(57, 19);
            this.label6.TabIndex = 17;
            this.label6.Text = "نام پدر:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtFatherName
            // 
            this.txtFatherName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtFatherName.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtFatherName.Location = new System.Drawing.Point(20, 248);
            this.txtFatherName.Margin = new System.Windows.Forms.Padding(4);
            this.txtFatherName.Name = "txtFatherName";
            this.txtFatherName.Size = new System.Drawing.Size(286, 27);
            this.txtFatherName.TabIndex = 11;
            this.txtFatherName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFatherName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFatherName_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label5.Location = new System.Drawing.Point(253, 289);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(52, 19);
            this.label5.TabIndex = 13;
            this.label5.Text = "رشته:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtReshte
            // 
            this.txtReshte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtReshte.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtReshte.Location = new System.Drawing.Point(20, 312);
            this.txtReshte.Margin = new System.Windows.Forms.Padding(4);
            this.txtReshte.Name = "txtReshte";
            this.txtReshte.Size = new System.Drawing.Size(286, 27);
            this.txtReshte.TabIndex = 12;
            this.txtReshte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtReshte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCN_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label4.Location = new System.Drawing.Point(210, 169);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(100, 19);
            this.label4.TabIndex = 11;
            this.label4.Text = "نام خانوادگی:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtFamily
            // 
            this.txtFamily.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtFamily.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtFamily.Location = new System.Drawing.Point(20, 197);
            this.txtFamily.Margin = new System.Windows.Forms.Padding(4);
            this.txtFamily.Name = "txtFamily";
            this.txtFamily.Size = new System.Drawing.Size(286, 27);
            this.txtFamily.TabIndex = 10;
            this.txtFamily.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFamily.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFamily_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label3.Location = new System.Drawing.Point(277, 115);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(33, 19);
            this.label3.TabIndex = 9;
            this.label3.Text = "نام:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtName.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtName.Location = new System.Drawing.Point(20, 138);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(286, 27);
            this.txtName.TabIndex = 8;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label2.Location = new System.Drawing.Point(203, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(107, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "کد دانشجویی:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPN
            // 
            this.txtPN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtPN.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPN.Location = new System.Drawing.Point(20, 36);
            this.txtPN.Margin = new System.Windows.Forms.Padding(4);
            this.txtPN.Name = "txtPN";
            this.txtPN.Size = new System.Drawing.Size(286, 27);
            this.txtPN.TabIndex = 1;
            this.txtPN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPN.TextChanged += new System.EventHandler(this.txtNN_TextChanged);
            this.txtPN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPN_KeyDown);
            this.txtPN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPN_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label1.Location = new System.Drawing.Point(243, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(67, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "کد ملی:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtNN
            // 
            this.txtNN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtNN.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNN.Location = new System.Drawing.Point(20, 88);
            this.txtNN.Margin = new System.Windows.Forms.Padding(4);
            this.txtNN.MaxLength = 10;
            this.txtNN.Name = "txtNN";
            this.txtNN.Size = new System.Drawing.Size(286, 27);
            this.txtNN.TabIndex = 2;
            this.txtNN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNN_KeyDown);
            this.txtNN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNN_KeyPress);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(321, 729);
            this.shapeContainer1.TabIndex = 21;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.Color.DarkGray;
            this.lineShape1.BorderWidth = 2;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = -3;
            this.lineShape1.X2 = 324;
            this.lineShape1.Y1 = 401;
            this.lineShape1.Y2 = 401;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1354, 733);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "مدیریت اسناد";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFatherName;
        private System.Windows.Forms.Panel panel1;
        private Glass.GlassButton btnAddDocs;
        private Glass.GlassButton btnScans;
        private Glass.GlassButton btnSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtReshte;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFamily;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNN;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel2;
        private Njit.ImageListView.ImageListView imageListView1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMaghta;
    }
}

