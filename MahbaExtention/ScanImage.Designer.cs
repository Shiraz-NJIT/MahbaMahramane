namespace MahbaExtention
{
    partial class ScanImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanImage));
            this.vsTwain1 = new Vintasoft.Twain.VSTwain();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.imageListView1 = new Njit.ImageListView.ImageListView();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // vsTwain1
            // 
            this.vsTwain1.AppProductName = "VintaSoftTwain.NET";
            this.vsTwain1.AutoCleanBuffer = true;
            this.vsTwain1.DisableAfterAcquire = false;
            this.vsTwain1.FileFormat = Vintasoft.Twain.FileFormat.Bmp;
            this.vsTwain1.FileName = "c:\\test.bmp";
            this.vsTwain1.JpegQuality = 90;
            this.vsTwain1.MaxImages = 1;
            this.vsTwain1.ModalUI = false;
            this.vsTwain1.Parent = this;
            this.vsTwain1.PdfImageCompression = Vintasoft.Twain.PdfImageCompression.JPEG;
            this.vsTwain1.PdfMultiPage = true;
            this.vsTwain1.ShowIndicators = true;
            this.vsTwain1.ShowUI = true;
            this.vsTwain1.TiffCompression = Vintasoft.Twain.TiffCompression.Auto;
            this.vsTwain1.TiffMultiPage = true;
            this.vsTwain1.TransferMode = Vintasoft.Twain.TransferMode.Memory;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(9, 81);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(179, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Location = new System.Drawing.Point(6, 55);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(182, 23);
            this.lblStatus.TabIndex = 1;
            // 
            // imageListView1
            // 
            this.imageListView1.BackgroundImage = global::MahbaExtention.Properties.Resources.background;
            this.imageListView1.DefaultImage = ((System.Drawing.Image)(resources.GetObject("imageListView1.DefaultImage")));
            this.imageListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageListView1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imageListView1.ErrorImage")));
            this.imageListView1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.imageListView1.Location = new System.Drawing.Point(0, 0);
            this.imageListView1.Name = "imageListView1";
            this.imageListView1.Size = new System.Drawing.Size(746, 427);
            this.imageListView1.TabIndex = 2;
            this.imageListView1.Text = "";
            this.imageListView1.ThumbnailSize = new System.Drawing.Size(450, 450);
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(6, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(182, 40);
            this.button1.TabIndex = 3;
            this.button1.Text = "Sacn (F5)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::MahbaExtention.Properties.Resources.background;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(746, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 427);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.imageListView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(746, 427);
            this.panel2.TabIndex = 5;
            // 
            // ScanImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 427);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ScanImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ScanImage";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Vintasoft.Twain.VSTwain vsTwain1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblStatus;
        private Njit.ImageListView.ImageListView imageListView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}