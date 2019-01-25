using MahbaExtention.Model;
using MahbaExtention.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MahbaExtention
{
    public partial class ScanImage : Form
    {
        string tempDirectory;

        private string p;
        DocumentService _DocumentService;
        public ScanImage(string p)
        {
            InitializeComponent();
            _DocumentService = new DocumentService();
            tempDirectory = Path.Combine(Path.GetTempPath(), "~Njit");
            // TODO: Complete member initialization
            this.p = p;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
           
            if (keyData == (Keys.F5))
            {
                btnScan_Click(this.button1, EventArgs.Empty);
                return true;
            }
           
           
            return base.ProcessCmdKey(ref msg, keyData);
        }

        enum SaveFormats
        {
            None = 0,
            OnePdf = 1,
            Pdf = 2,
            OneMultiTiff = 3,
            Tiff = 4,
            JPEG = 5,
            PNG = 6,
            BMP = 7
        }
        [DefaultValue(typeof(SaveFormats), "None")]

        SaveFormats SelectedSaveFormat;
        [DefaultValue(null)]
        protected string SelectedFolderForSaveFiles { get; private set; }
        string[] imageExtensions = new string[] { ".bmp", ".jpg", ".jpeg", ".png", ".gif", ".tif", ".tiff" };
        Vintasoft.Twain.VSTwain _VsTwain;
        private Vintasoft.Twain.VSTwain VsTwain
        {
            get
            {
                if (_VsTwain == null)
                {
                    _VsTwain = new Vintasoft.Twain.VSTwain();
                    _VsTwain.AppProductName = "VintaSoftTwain.NET";
                    _VsTwain.AutoCleanBuffer = true;
                    _VsTwain.FileFormat = Vintasoft.Twain.FileFormat.TiffMulti;
                    _VsTwain.FileName = Path.Combine(tempDirectory, "temp.tiff");
                    _VsTwain.JpegQuality = 90;
                    _VsTwain.MaxImages = 1000;
                    _VsTwain.ModalUI = false;
                    _VsTwain.Parent = this;
                    _VsTwain.PdfImageCompression = Vintasoft.Twain.PdfImageCompression.JPEG;
                    _VsTwain.PdfMultiPage = true;
                    _VsTwain.TiffCompression = Vintasoft.Twain.TiffCompression.Auto;
                    _VsTwain.TiffMultiPage = true;
                    _VsTwain.TransferMode = Vintasoft.Twain.TransferMode.Memory;
                    _VsTwain.ImageAcquired += new Vintasoft.Twain.VSTwain.ImageAcquiredEventHandler(VsTwain_ImageAcquired);
                    _VsTwain.ScanCompleted += new Vintasoft.Twain.VSTwain.ScanCompletedEventHandler(VsTwain_ScanCompleted);
                    _VsTwain.ProgressChanged += VsTwain_ProgressChanged;
                }
                return _VsTwain;
            }
        }
        private void VsTwain_ScanCompleted(object sender, EventArgs e)
        {
            lblStatus.Text = "اسکن به پایان رسید";
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        private void VsTwain_ImageAcquired(object sender, EventArgs e)
        {
            lblStatus.Text = "درحال ذخیره تصویر...";
            string fileName = PersianCalendar.GetDate(DateTime.Now, "-") + " " + PersianCalendar.GetTime(DateTime.Now, "-", true, true);
            string documentPath = Path.Combine(tempDirectory, fileName + ".tiff").ToString();
            int i = 0;
            while (System.IO.File.Exists(documentPath))
            {
                documentPath = Path.Combine(tempDirectory, fileName + "(" + (++i).ToString() + ")" + ".tiff").ToString();
            }
            try
            {
                VsTwain.FileFormat = Vintasoft.Twain.FileFormat.TiffMulti;
                VsTwain.SaveImage(0, documentPath);
                VsTwain.DeleteImage(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "خطا در ذخیره فایل اسکن شده" + "\r\n\r\n" + ex.Message);
                return;
            }

            imageListView1.Items.Add(documentPath);
            string path = SaveFile(documentPath);
            _DocumentService.Add(new Document { PN = this.p, ParentDocumentID = null, FileName = path, AttachedToDossier = false });
        }
        protected virtual string SaveFile(string file)
        {
            if (this.p != null)
            {
                string text = Directory.GetCurrentDirectory() + @"\FileName.txt";
                var fileStream = new FileStream(text, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    text = streamReader.ReadToEnd();
                }
                tempDirectory = Path.Combine(Path.GetTempPath(), "~Njit");
                try
                {
                    if (!System.IO.Directory.Exists(tempDirectory))
                        System.IO.Directory.CreateDirectory(tempDirectory);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "خطا در دسترسی به مسیر ذخیره فایل ها" + "\r\n\r\n" + ex.Message);
                }
                this.SelectedFolderForSaveFiles = text + "\\" + this.p;
                SelectedSaveFormat = SaveFormats.JPEG;
                string fileExtension = Path.GetExtension(file).ToLower();
                if (imageExtensions.Contains(fileExtension))
                {
                    switch (SelectedSaveFormat)
                    {
                        case SaveFormats.None:
                            break;

                        case SaveFormats.JPEG:
                            if (fileExtension != ".jpg" && fileExtension != ".jpeg")
                            {
                                fileExtension = ".jpg";
                                string newfile = GetUniqFileName(fileExtension);
                                Image image = Image.FromFile(file);
                                ImageHelper.ConvertImage(image, System.Drawing.Imaging.ImageFormat.Jpeg, 0, newfile);
                                image.Dispose();
                                file = newfile;
                            }
                            break;
                        case SaveFormats.PNG:
                            if (fileExtension != ".png")
                            {
                                fileExtension = ".png";
                                string newfile = GetUniqFileName(fileExtension);
                                Image image = Image.FromFile(file);
                                ImageHelper.ConvertImage(image, System.Drawing.Imaging.ImageFormat.Png, 0, newfile);
                                image.Dispose();
                                file = newfile;
                            }
                            break;
                        case SaveFormats.BMP:
                            if (fileExtension != ".bmp")
                            {
                                fileExtension = ".bmp";
                                string newfile = GetUniqFileName(fileExtension);
                                Image image = Image.FromFile(file);
                                ImageHelper.ConvertImage(image, System.Drawing.Imaging.ImageFormat.Bmp, 0, newfile);
                                image.Dispose();
                                file = newfile;
                            }
                            break;
                        default:
                            throw new Exception();
                    }
                }
                string fileName = System.IO.Path.GetFileNameWithoutExtension(file) + fileExtension; //Njit.Common.PersianCalendar.GetDate(DateTime.Now, "-") + " " + Njit.Common.PersianCalendar.GetTime(DateTime.Now, "-", true, true) + fileExtension;
                string destinationFile = null;
                destinationFile = Path.Combine(this.SelectedFolderForSaveFiles, fileName);
                int i = 1;
                while (System.IO.File.Exists(destinationFile))
                {
                    fileName = System.IO.Path.GetFileNameWithoutExtension(file) + " (" + i.ToString() + ")" + fileExtension;//Njit.Common.PersianCalendar.GetDate(DateTime.Now, "-") + " " + Njit.Common.PersianCalendar.GetTime(DateTime.Now, "-", true, true) + fileExtension;
                    destinationFile = Path.Combine(this.SelectedFolderForSaveFiles, fileName);
                    i++;
                }

                FileStream serverFileStream = null;
                FileStream clientFileStream = null;
                try
                {
                    if (!System.IO.Directory.Exists(this.SelectedFolderForSaveFiles))
                        System.IO.Directory.CreateDirectory(this.SelectedFolderForSaveFiles);
                    serverFileStream = System.IO.File.Create(destinationFile);
                    clientFileStream = System.IO.File.OpenRead(file);
                    byte[] buffre = new byte[1 * 1024 * 1024];
                    int readCount = 0;
                    do
                    {
                        readCount = clientFileStream.Read(buffre, 0, buffre.Length);
                        serverFileStream.Write(buffre, 0, readCount);
                    }
                    while (readCount > 0);
                    clientFileStream.Close();
                    serverFileStream.Close();
                    clientFileStream.Dispose();
                    serverFileStream.Dispose();
                    return destinationFile;
                }
                catch (Exception ex)
                {
                    if (clientFileStream != null)
                        clientFileStream.Dispose();
                    if (serverFileStream != null)
                        serverFileStream.Dispose();
                    throw new Exception("خطا در ذخیره فایل" + "\r\n" + file + "\r\n\r\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("پرونده ای که می خواهید سند به آن اضافه کنید را مشخص کنید");
                return "";
            }
        }

        public string GetUniqFileName(string extension)
        {
            string fileName = PersianCalendar.GetDate(DateTime.Now, "-") + " " + PersianCalendar.GetTime(DateTime.Now, "-", true, true);
            string documentPath = Path.Combine(tempDirectory, fileName + extension).ToString();
            int i = 0;
            while (System.IO.File.Exists(documentPath))
            {
                documentPath = Path.Combine(tempDirectory, fileName + "(" + (++i).ToString() + ")" + extension).ToString();
            }
            return documentPath;

        }
        void VsTwain_ProgressChanged(object sender, Vintasoft.Twain.ProgressChangedEventArgs e)
        {
            progressBar1.Value = (int)e.PercentComplete;
        }
        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                VsTwain.StartDevice();
                if (this.SelectScannerBeforScan && this.ScanSource == null)
                    if (!VsTwain.SelectSource())
                    {
                        MessageBox.Show(this, "هیچ اسکنری انتخاب نشده است");
                        return;
                    }
                    else
                    {
                        this.ScanSource = VsTwain.GetSourceProductName(VsTwain.SourceIndex);
                    }
                VsTwain.Acquire();
                lblStatus.Text = "درحال اسکن تصویر...";
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا" + "\r\n\r\n" + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private bool FastSaveMode { get; set; }

        private string _ScanSource = null;
        [DefaultValue(null)]
        public string ScanSource
        {
            get
            {
                return _ScanSource;
            }
            set
            {
                _ScanSource = value;
            }
        }

        private bool _SelectScannerBeforScan = false;
        [DefaultValue(false)]
        protected bool SelectScannerBeforScan
        {
            get
            {
                return _SelectScannerBeforScan;
            }
            set
            {
                _SelectScannerBeforScan = value;
            }
        }

        public event EventHandler SaveComplete;

        protected virtual void OnSaveComplete()
        {
            if (SaveComplete != null)
                SaveComplete(this, EventArgs.Empty);
        }
    }
}
