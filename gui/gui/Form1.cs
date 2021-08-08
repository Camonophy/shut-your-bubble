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

        // Path to several copies of the image
        private readonly string CACHEPATH = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\..\..\Resources\Cache\";

        // Path to the Python-Script to remove the text in an image
        private readonly string SCRIPTPATH = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\..\..\..\..\src\Textinguisher.py";

        // Used to manage through back and forth option
        private int stepCount, index = 0;

        // Original image path and name
        private string path;
        private string fileName;

        private DirectoryInfo cachDirectory;

        public MainWindow()
        {
            InitializeComponent();
            cachDirectory = new DirectoryInfo(this.CACHEPATH);
        }

        /**
         * Get the X and Y coordinates as the top left corner of the rectangle, in which 
         * the user wants remove the text.
         */
        private void ImageBox_MouseDown(object sender, MouseEventArgs e)
        {
            this.StartXBox.Text = e.X.ToString();
            this.StartYBox.Text = e.Y.ToString();
        }

        /**
         * Get the X and Y coordinates as the bottom right corner of the rectangle, in which 
         * the user wants remove the text.
         * 
         * Safe a copy of the current image in the gui/gui/Resources/Cache/ folder, 
         * process the image with the Python-Script and show the new image in the ImageBox
         */
        private void ImageBox_MouseUp(object sender, MouseEventArgs e)
        {
            this.ImageBox.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            this.EndXBox.Text = e.X.ToString();
            this.EndYBox.Text = e.Y.ToString();

            // If the user goes x images back and creates a new one, delete all x images
            for(int i = this.index + 1; i <= this.stepCount;  i++)
            {
                File.Delete(this.CACHEPATH + i);
            }

            // After 100 backup copies, delete the first, sort and append a new
            if (this.stepCount == 100)
            {
                File.Delete(this.CACHEPATH + 0);
                this.ReNumerade_files();

            } else {
                this.stepCount = ++this.index;
            }

            // Run the Python-Script
            System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo();
            start.FileName = "python.exe";
            start.Arguments = this.SCRIPTPATH +                              
                              String.Format(" {0} {1} {2} {3} ", 550, 123, 446, 49) +
                              this.CACHEPATH + this.fileName;
            start.UseShellExecute = false;
            start.CreateNoWindow = true; 
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true; 
            start.LoadUserProfile = true;
            using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stderr = process.StandardError.ReadToEnd(); 
                    string result = reader.ReadToEnd(); 
                    Console.WriteLine("From System Diagnostics");
                    Console.WriteLine(stderr);
                    Console.WriteLine(result);
                }
            }

            // Load the Python output file
            this.ImageBox.Image = LoadBitMap(this.CACHEPATH + this.fileName);

            // Safe the new picture as a backup copy
            try
            {
                Image image = this.ImageBox.Image;
                image.Save(this.CACHEPATH + this.stepCount);
            } catch (NullReferenceException) {
                // No image is loaded and therefore no image can be saved
            }

            this.ImageBox.Cursor = System.Windows.Forms.Cursors.Default;
        }

        /**
         * Coordinates of the current mouse position in the ImageBox
         */
        private void ImageBox_MouseMove(object sender, MouseEventArgs e)
        {
            this.CurrentXBox.Text = e.X.ToString();
            this.CurrentYBox.Text = e.Y.ToString();
        }

        /**
         * If the user wants to undo his the last actions, load the previous image
         * from the Cache folder
         */
        private void backButton_Click(object sender, EventArgs e)
        {
            if(this.index > 0 && this.ImageBox.Image != null) 
            {
                //////// Not sure abt these paths ///////
                try
                {
                    this.ImageBox.Load(AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\Resources\Cache\" + --this.index);
                } catch(FileNotFoundException)
                {
                    // No file is loaded and therefore can not be replaced by another one
                }
            }
        }

        /**
         * If the user wants to redo one of his lasta ctions, load his next image
         * from the Cache folder
         */
        private void ForthButton_Click(object sender, EventArgs e)
        {
            if (this.index != this.stepCount && this.ImageBox.Image != null)
            {
                try
                {
                    this.ImageBox.Load(AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\Resources\Cache\" + ++this.index);
                }
                catch (FileNotFoundException)
                {
                    // No file is loaded and therefore can not be replaced by another one
                }
            }
        }

        /**
         * Clear the Cache directory and load an image from the 
         * file dialog into the ImageBox
         */
        private void load_Click(object sender, EventArgs e)
        {
            foreach (FileInfo file in cachDirectory.GetFiles())
            {
                file.Delete();
            }

            OpenFileDialog findImageDialog = new OpenFileDialog();
            findImageDialog.Title = "Please select an image";
            findImageDialog.Filter = "Bitmap (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png";
            findImageDialog.FilterIndex = 3;
            findImageDialog.ShowDialog();
            this.path = findImageDialog.FileName.ToString();
            this.fileName = findImageDialog.SafeFileName.ToString();
            try
            {
                System.IO.File.Copy(this.path, this.CACHEPATH + this.fileName, true);
                this.ImageBox.Image = LoadBitMap(this.CACHEPATH + this.fileName);
                this.PathBox.Text = this.path;
                Image image = this.ImageBox.Image;
                image.Save(Path.Combine(this.CACHEPATH, "0"));
                this.SaveButton.Enabled = true;
            } catch(ArgumentException) {
                // File dialog is closed and therefore no file is loaded
            }
        }

        /**
         * Safe the image from the Imagebox with the file dialog 
         */
        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveImageDialog = new SaveFileDialog();
            saveImageDialog.Title = "Save the image";
            saveImageDialog.Filter = "Bitmap (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png";
            saveImageDialog.FilterIndex = 3;
            saveImageDialog.ShowDialog();

            if (saveImageDialog.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveImageDialog.OpenFile();

                switch (saveImageDialog.FilterIndex)
                {
                    case 1:
                        this.ImageBox.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 2:
                        this.ImageBox.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 3:
                        this.ImageBox.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }
                fs.Close();
            }
        }

        /**
         * The user has 100 backup copies available to turn back to.
         * If 100 copies are reached, the 0th get deleted, image copies 1-100
         * are taking the position of their previous copy and image 100 can be saved.
         */
        private void ReNumerade_files()
        {
            try
            {
                for(int i = 1; i <= 100; i++)
                {
                    System.IO.File.Move(this.CACHEPATH + i.ToString(), this.CACHEPATH + (i - 1).ToString());
                }
            } catch (Exception e)
            {
                // One file is missing
            }
        }

        /**
         * Create a copy of the original file as bitmap, in order to open the original 
         * with Python and C#. (Permission errors would arise otherwise) 
         */
        private Bitmap LoadBitMap(string path)
        {
            if (File.Exists(path))
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    var memoryStream = new MemoryStream(reader.ReadBytes((int)stream.Length));
                    return new Bitmap(memoryStream);
                }
            } else {
                this.PathBox.Text = "Error: Can not load this file.";
                return null;
            }
        }
    }
}
