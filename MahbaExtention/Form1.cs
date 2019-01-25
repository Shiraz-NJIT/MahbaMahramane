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
    public partial class Form1 : Form
    {
        DossierService _DossierService;
        DocumentService _DocumentService;
        Dossier _Dossier;
        string tempDirectory;
        string[] Reshte = new string[5];
        string[] Maghta = new string[3];
        public Form1()
        {
            InitializeComponent();
            _DossierService = new DossierService();
            _DocumentService = new DocumentService();

            Reshte[0] = "مدیریت خدمات بهداشتی و درمانی";
            Reshte[1] = "فن آوری اطلاعات سلامت";
            Reshte[2] = "مدیریت اطلاعات سلامت";
            Reshte[3] = "انفورماتیک پزشکی";
            Reshte[4] = "اقتصاد بهداشت";
            Maghta[0] = "کارشناسی ";
            Maghta[1] = "کارشناسی ارشد";
            Maghta[2] = "دکترای تخصصی ";
            //Reshte
            AutoCompleteStringCollection autoCompleteStringCollectionr = new AutoCompleteStringCollection();
            autoCompleteStringCollectionr.AddRange(Reshte);
            txtReshte.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtReshte.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtReshte.AutoCompleteCustomSource = autoCompleteStringCollectionr;
            //Maghta
            AutoCompleteStringCollection autoCompleteStringCollectionM = new AutoCompleteStringCollection();
            autoCompleteStringCollectionM.AddRange(Maghta);
            txtMaghta.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtMaghta.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMaghta.AutoCompleteCustomSource = autoCompleteStringCollectionM;
        }
        private void SetPNAutoComplate()
        {
            AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();
            autoCompleteStringCollection.AddRange(_DossierService.GetAllPN());
            txtPN.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPN.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPN.AutoCompleteCustomSource = autoCompleteStringCollection;
       
           
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F1))
            {
                btnSave_Click(this.btnSave, EventArgs.Empty);
                return true;
            }
            if (keyData == (Keys.F5))
            {
                btnScans_Click(this.btnScans, EventArgs.Empty);
                return true;
            }
            if (keyData == (Keys.F9))
            {
                btnAddDocs_Click(this.btnAddDocs, EventArgs.Empty);
                return true;
            }
            if (keyData == (Keys.Control | Keys.Q))
            {
                Application.Exit();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        //الگوریتم چک کردن کد ملی
        public bool CheckNN(string NN)
        {
            try
            {
                string chArray = NN;
                int[] numArray = new int[chArray.Length];
                for (int i = 0; i < chArray.Length; i++)
                {
                    numArray[i] = (int)char.GetNumericValue(chArray[i]);
                }
                int num2 = numArray[9];
                switch (chArray)
                {
                    case "0000000000":
                    case "1111111111":
                    case "22222222222":
                    case "33333333333":
                    case "4444444444":
                    case "5555555555":
                    case "6666666666":
                    case "7777777777":
                    case "8888888888":
                    case "9999999999":

                        return false;

                }
                int num3 = ((((((((numArray[0] * 10) + (numArray[1] * 9)) + (numArray[2] * 8)) + (numArray[3] * 7)) + (numArray[4] * 6)) + (numArray[5] * 5)) + (numArray[6] * 4)) + (numArray[7] * 3)) + (numArray[8] * 2);
                int num4 = num3 - ((num3 / 11) * 11);
                if ((((num4 == 0) && (num2 == num4)) || ((num4 == 1) && (num2 == 1))) || ((num4 > 1) && (num2 == Math.Abs((int)(num4 - 11)))))
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch
            {
                return false;
            }

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


        protected virtual string SaveFile(string file)
        {
            if (_Dossier != null)
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
                this.SelectedFolderForSaveFiles = text + "\\" + _Dossier.PN;
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder Error = new StringBuilder();
                if (!string.IsNullOrEmpty(txtNN.Text))
                {
                    if (CheckNN(txtNN.Text) == false)
                    {
                        Error.Append("کد ملی نامعتبر است.");
                    }

                    if (_DossierService.isExist(txtNN.Text))
                    {
                        if (_Dossier == null)
                            Error.Append("کد ملی در بانک اطلاعاتی وجود دارد.");
                    }
                }

                if (string.IsNullOrEmpty(txtPN.Text))
                {
                    Error.Append(" .شماره پرونده وارد نشده است");
                }
                if (_DossierService.isExistPN(txtPN.Text))
                {
                    if (_Dossier == null)
                        Error.Append("شماره پرونده در بانک اطلاعاتی وجود دارد.");
                }
                //if (txtPN.Text.Length != 10)
                //{
                //    Error.Append("شماره پرونده باید 10 رقمی باشد.");
                //}
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    Error.Append(" .نام وارد نشده است");
                }
                if (string.IsNullOrEmpty(txtFamily.Text))
                {
                    Error.Append(".نام خانوادگی وارد نشده است");
                }
                if (string.IsNullOrEmpty(txtFatherName.Text))
                {
                    Error.Append(" .نام پدر وارد نشده است");
                }
                if (string.IsNullOrEmpty(txtReshte.Text))
                {
                    Error.Append(" .شماره شناسنامه وارد نشده است");
                }
                if (Error.Length != 0)
                {
                    throw new Exception(Error.ToString());
                }
                else
                {
                    if (_Dossier == null)
                    {
                        Dossier d = new Dossier();
                        d.NN = txtNN.Text;
                        d.Reshte = txtReshte.Text;
                        d.Maghta = txtMaghta.Text;
                        d.Name = txtName.Text;
                        d.Family = txtFamily.Text;
                        d.FatherName = txtFatherName.Text;
                        d.PN = txtPN.Text;

                        _DossierService.Add(d);
                        MessageBox.Show("اطلاعات با موفقیت ذخیره شد.");
                        _Dossier = d;
                    }
                    else
                    {

                        _Dossier.Reshte = txtReshte.Text;
                        _Dossier.Maghta = txtMaghta.Text;
                        _Dossier.Name = txtName.Text;
                        _Dossier.Family = txtFamily.Text;
                        _Dossier.FatherName = txtFatherName.Text;
                        _Dossier.PN = txtPN.Text;

                        _DossierService.Update(_Dossier);
                        MessageBox.Show("اطلاعات با موفقیت ویرایش شد.");
                    }
                    SetPNAutoComplate();
                    btnAddDocs.Enabled = true;
                    btnScans.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtNN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtName.Focus();
            }
        }

        private void txtPN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNN.Focus();
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFamily.Focus();
            }
        }

        private void txtFamily_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFatherName.Focus();
            }
        }

        private void txtFatherName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtReshte.Focus();
            }
        }

        private void txtCN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMaghta.Focus();
            }
        }

        private void txtNN_TextChanged(object sender, EventArgs e)
        {
            btnAddDocs.Enabled = false;
            btnSave.Text = " (F1) ذخیره پرونده";
            _Dossier = new Dossier();
            _Dossier = _DossierService.getByPN(txtPN.Text);
            if (_Dossier != null)
            {
                txtReshte.Text = _Dossier.Reshte;
                txtMaghta.Text = _Dossier.Maghta;
                txtName.Text = _Dossier.Name;
                txtFamily.Text = _Dossier.Family;
                txtNN.Text = _Dossier.NN;
                txtFatherName.Text = _Dossier.FatherName;
                showImage();
                btnAddDocs.Enabled = true;
                btnSave.Text = "(F1) ویرایش پرونده";
            }
            else
            {
                txtReshte.Text = "";
                txtMaghta.Text = "";
                txtReshte.Text = "";
                txtName.Text = "";
                txtFamily.Text = "";
                txtNN.Text = "";
                txtFatherName.Text = "";
            }
        }

        private void showImage()
        {
            imageListView1.Items.Clear();
            foreach (var item in _DocumentService.getAll(_Dossier.PN))
            {
                imageListView1.Items.Add(item.FileName);
            }
        }
        private void btnAddDocs_Click(object sender, EventArgs e)
        {
            if (_Dossier == null)
            {
                MessageBox.Show("لطفا اطلاعات پرونده را وارد کنید.");
            }
            else
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    foreach (string file in openFileDialog1.FileNames)
                    {

                        imageListView1.Items.Add(file);
                        try
                        {
                            string path = SaveFile(file);
                            _DocumentService.Add(new Document { PN = _Dossier.PN, ParentDocumentID = null, FileName = path, AttachedToDossier = false });
                        }
                        catch
                        {
                            imageListView1.Items.Clear();
                        }
                    }
                    showImage();
                }
            }


        }
        private void btnScans_Click(object sender, EventArgs e)
        {


            if (_Dossier != null)
            {
                ScanImage si = new ScanImage(_Dossier.PN);
                si.ShowDialog();
            }
            else
            {
                MessageBox.Show("لطفا اطلاعات پرونده را وارد کنید.");
            }

        }

        private void txtNN_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            //txtNN.BackColor = System.Drawing.Color.LightYellow;
        }

        private void txtPN_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            //txtPN.BackColor = System.Drawing.Color.LightYellow;
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsDigit(e.KeyChar);
            //txtName.BackColor = System.Drawing.Color.LightYellow;
        }

        private void txtMaghta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    Update u = new MahbaExtention.Update();
        //    u.Show();
        //}
    }
}
