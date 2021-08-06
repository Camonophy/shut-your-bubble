using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gui
{
    public partial class MainWindow : Form
    {

        private readonly string BACKUPPATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "\\..\\..\\Resources\\Backup\\");

        private int stepCount, index = 0;
        private string path;
        private DirectoryInfo d;

        public MainWindow()
        {
            InitializeComponent();
            d = new DirectoryInfo(this.BACKUPPATH);
        }

        private void ImageBox_MouseDown(object sender, MouseEventArgs e)
        {
            this.StartXBox.Text = e.X.ToString();
            this.StartYBox.Text = e.Y.ToString();
        }

        private void ImageBox_MouseUp(object sender, MouseEventArgs e)
        {
            this.EndXBox.Text = e.X.ToString();
            this.EndYBox.Text = e.Y.ToString();

            for(int i = this.index + 1; i <= this.stepCount;  i++)
            {
                File.Delete(this.BACKUPPATH + i);
            }

            if(this.stepCount == 100)
            {
                File.Delete(this.BACKUPPATH + 0);
                this.reNumerade_files();

            } else {
                this.stepCount = ++this.index;
            }
            
            Image image = Image.FromFile(this.path);
            image.Save(this.BACKUPPATH + this.stepCount);
        }

        private void ImageBox_MouseMove(object sender, MouseEventArgs e)
        {
            this.CurrentXBox.Text = e.X.ToString();
            this.CurrentYBox.Text = e.Y.ToString();
        }

        //////// Not sure abt these paths ///////
        private void backButton_Click(object sender, EventArgs e)
        {
            if(this.index > 0) 
            {
                this.ImageBox.Load(AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\..\\Resources\\Backup\\" + --this.index); 
            }
        }

        private void ForthButton_Click(object sender, EventArgs e)
        {
            if (this.index != this.stepCount)
            {
                this.ImageBox.Load(AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\..\\Resources\\Backup\\" + ++this.index);
            }
        }

        private void load_Click(object sender, EventArgs e)
        {
            OpenFileDialog findImageDialog = new OpenFileDialog();
            findImageDialog.Title = "Please select an image";
            findImageDialog.Filter = "Bitmap (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg |PNG (*.png)|*.png";
            findImageDialog.FilterIndex = 3;
            findImageDialog.ShowDialog();
            this.path = findImageDialog.FileName.ToString();
            this.ImageBox.ImageLocation = this.path;
            this.PathBox.Text = this.path;
            Image image = Image.FromFile(this.path);
            image.Save(Path.Combine(this.BACKUPPATH, "0"));
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveImageDialog = new SaveFileDialog();
            saveImageDialog.Title = "Save the image";
            saveImageDialog.Filter = "Bitmap (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg |PNG (*.png)|*.png";
            saveImageDialog.FilterIndex = 3;
            saveImageDialog.ShowDialog();
        }

        private void reNumerade_files()
        {
            try
            {
                for(int i = 1; i <= 100; i++)
                {
                    System.IO.File.Move(this.BACKUPPATH + i.ToString(), this.BACKUPPATH + (i - 1).ToString());
                }
            } catch (Exception e)
            {
                // One file is missing
            }
        }
    }
}
