using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace gui
{
    public partial class MainWindow : Form
    {

        // Path to several copies of the image
        private readonly string CACHEPATH = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\..\..\Resources\Cache\";

        // Path to the Python-Script to remove the text in an image
        private readonly string SCRIPTPATH = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\..\..\..\..\src\Textinguisher.py";

        // Bitmap to figure out the color of a specific pixel in read-color-mode
        private Bitmap bmp;

        // Directory of the image backup files
        private DirectoryInfo cachDirectory;

        // Color dialog window to change the color of the replacing rectangles
        private ColorDialog CDialog = new ColorDialog();

        // Used to manage through back and forth option
        private int index, stepCount = 0;

        // Map the installed Tesseract languages to their full notation
        System.Collections.Generic.Dictionary<string, string> languages = 
            new System.Collections.Generic.Dictionary<string, string>(){
	            {"chi_sim",      "Chinese (Simplified)"},
                {"chi_sim_vert", "Chinese Vertical (Simplified)"},
                {"chi_tra",      "Chinese (Traditional)"},
	            {"chi_tra_vert", "Chinese Vertical (Traditional)"},
                {"deu",          "German"},
                {"eng",          "English"},
                {"fra",          "French"},
                {"ita",          "Italian"},
                {"jpn",          "Japanese"},
                {"jpn_vert",     "Japanese Vertical"},
                {"kor",          "Korean"},
                {"rus",          "Russian"},
                {"spa",          "Spanish"}
            };

        // Original image path and name
        private string path;
        private string fileName;

        // Distinguish between "the user wants wo pick a color from an image" or "the user wants to remove text from an image"
        private bool readColorMode = false;


        public MainWindow()
        {
            InitializeComponent();
            cachDirectory = new DirectoryInfo(this.CACHEPATH);
            String[] installed_languages = this.Get_Installed_Tessarect_Languages();

            foreach (String lan in installed_languages)
            {
                try { this.LanguageSelect.Items.Add(languages[lan]); } 
                catch(Exception) { } // Language not supported
            }

            if(this.LanguageSelect.Items.Count == 0)
            {
                this.LanguageSelect.Items.Add("No language found");
            }

            this.LanguageSelect.SelectedIndex = 0;
        }


        /**
         * If the user wants to undo his the last actions, load the previous image
         * from the Cache folder
         */
        private void BackButton_Click(object sender, EventArgs e)
        {
            this.ImageBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.readColorMode = false;

            if (this.index > 0 && this.ImageBox.Image != null) 
            {
                //////// Not sure abt these paths ///////
                try
                {
                    this.ImageBox.Load(AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\Resources\Cache\" + --this.index);
                    System.IO.File.Copy(this.CACHEPATH + this.index, this.CACHEPATH + this.fileName, true);
                } catch(FileNotFoundException) { } // No file is loaded and therefore can not be replaced by another one
            }
        }

        /**
         * Convert a color into a string, that represents the corresponding hex value
         */
        private String Color_To_Hex(Color actColor)
        {
            return "#" + actColor.R.ToString("X2") + actColor.G.ToString("X2") + actColor.B.ToString("X2");
        }


        /**
         * Try to read the content of the ColorBox, convert the hex number into
         * an actual color if possible and show the color on the ColorButton.
         */
        private void ColorBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Color colorFromTextBox = (Color)(new ColorConverter()).ConvertFromString(this.ColorBox.Text);
                if(colorFromTextBox.ToArgb() < 0)
                {
                    this.ColorButton.BackColor = colorFromTextBox;
                }
            } catch (Exception) { } // No valid hex number found to convert into a color
        }


        /**
         * Open a color dialog, where the user can choose between various default
         * colors to change the one that will be used to fill the next rectangle
         */
        private void ColorButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.ImageBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.readColorMode = false;

            // Sets the initial color select to the current text color.
            this.CDialog.Color = this.ColorButton.ForeColor;

            // Update the text box color if the user clicks OK 
            if (this.CDialog.ShowDialog() == DialogResult.OK)
                this.ColorButton.BackColor = this.CDialog.Color;
                this.ColorBox.Text = Color_To_Hex(this.ColorButton.BackColor);
        }


        /**
         * If the user wants to redo one of his lasta ctions, load his next image
         * from the Cache folder
         */
        private void ForthButton_Click(object sender, EventArgs e)
        {
            this.ImageBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.readColorMode = false;

            if (this.index != this.stepCount && this.ImageBox.Image != null)
            {
                try
                {
                    this.ImageBox.Load(AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\Resources\Cache\" + ++this.index);
                    System.IO.File.Copy(this.CACHEPATH + this.index, this.CACHEPATH + this.fileName, true);
                }
                catch (FileNotFoundException) { } // No file is loaded and therefore can not be replaced by another one
            }
        }


        /**
        * Ask Tesseract for all available languages that has been installed
        */
        private String[] Get_Installed_Tessarect_Languages()
        {
            String[] languages = new string[] { };

            System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo();
            start.FileName = "python";
            start.Arguments = "-c \"import pytesseract; print(pytesseract.get_languages())\"";
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
                    languages = result.Substring(2, result.Length - 4)
                                      .Replace(" ", "")
                                      .Replace("\'", "")
                                      .Split(',');
                }
            }

            return languages;
        }


        /**
         * Get the X and Y coordinates as the top left corner of the rectangle, in which 
         * the user wants remove the text.
         */
        private void ImageBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.readColorMode)
            {
                this.StartXBox.Text = e.X.ToString();
                this.StartYBox.Text = e.Y.ToString();
            }
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
         * Get the X and Y coordinates as the bottom right corner of the rectangle, in which 
         * the user wants remove the text.
         * 
         * Safe a copy of the current image in the gui/gui/Resources/Cache/ folder, 
         * process the image with the Python-Script and show the new image in the ImageBox
         */
        private void ImageBox_MouseUp(object sender, MouseEventArgs e)
        {
            if(!this.readColorMode)
            {
                this.ImageBox.Cursor    = System.Windows.Forms.Cursors.WaitCursor;
                this.ImageBox.Enabled   = false;
                this.SaveButton.Enabled = false;
                this.EndXBox.Text       = e.X.ToString();
                this.EndYBox.Text       = e.Y.ToString();

                // If the user goes x images back and creates a new one, delete all x images
                for(int i = this.index + 1; i <= this.stepCount;  i++)
                {
                    File.Delete(this.CACHEPATH + i);
                }

                // After 100 backup copies, delete the first, sort and append a new
                if (this.stepCount == 100)
                {
                    File.Delete(this.CACHEPATH + 0);
                    this.Renumerade_Files();

                } else {
                    this.stepCount = ++this.index;
                }

                // Run the Python-Script
                System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo();
                start.FileName  = "python";
                start.Arguments = this.SCRIPTPATH +                              
                                  String.Format(" {0} {1} {2} {3} {4} {5} ", 
                                                 int.Parse(this.StartXBox.Text.ToString()),
                                                 int.Parse(this.StartYBox.Text.ToString()), 
                                                 int.Parse(this.EndXBox.Text.ToString()) - int.Parse(this.StartXBox.Text.ToString()),
                                                 int.Parse(this.EndYBox.Text.ToString()) - int.Parse(this.StartYBox.Text.ToString()),
                                                 this.ColorBox.Text,
                                                 this.CACHEPATH + this.fileName);
                start.UseShellExecute        = false;
                start.CreateNoWindow         = true; 
                start.RedirectStandardOutput = true;
                start.RedirectStandardError  = true; 
                start.LoadUserProfile        = true;
                using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string stderr = process.StandardError.ReadToEnd(); 
                        string result = reader.ReadToEnd(); 
                        Console.WriteLine(stderr);
                        Console.WriteLine(result);
                    }
                }

                // Load the Python output file
                this.ImageBox.Image = Load_BitMap(this.CACHEPATH + this.fileName);

                // Safe the new picture as a backup copy
                try
                {
                    Image image = this.ImageBox.Image;
                    image.Save(this.CACHEPATH + this.stepCount);
                } catch (NullReferenceException) { } // No image is loaded and therefore no image can be saved

                this.ImageBox.Cursor    = System.Windows.Forms.Cursors.Default;
                this.ImageBox.Enabled   = true;
                this.SaveButton.Enabled = true;
            } else {
                Color pixelColor = bmp.GetPixel(e.X, e.Y);
                this.ColorBox.Text = Color_To_Hex(pixelColor);
                this.ColorButton.BackColor = pixelColor;
                this.ImageBox.Cursor = System.Windows.Forms.Cursors.Default;
                this.readColorMode = false;
            }
        }


        /**
         * Create a copy of the original file as bitmap, in order to open the original 
         * with Python and C#. (Permission errors would arise otherwise) 
         */
        private Bitmap Load_BitMap(string path)
        {
            if (File.Exists(path))
            {
                using (FileStream stream   = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    var memoryStream = new MemoryStream(reader.ReadBytes((int)stream.Length));
                    return new Bitmap(memoryStream);
                }
            } else {
                this.PathBox.Text = "Error: No file loaded";
                return null;
            }
        }


        /**
         * Clear the Cache directory and load an image from the 
         * file dialog into the ImageBox
         */
        private void Load_Click(object sender, EventArgs e)
        {
            this.ImageBox.Cursor    = System.Windows.Forms.Cursors.Default;
            this.readColorMode      = false;
            this.ImageBox.Enabled   = false;
            this.SaveButton.Enabled = false;
            foreach (FileInfo file in cachDirectory.GetFiles())
            {
                try { file.Delete();  } 
                catch (IOException) { } // This file is still open

            }

            OpenFileDialog findImageDialog = new OpenFileDialog();
            findImageDialog.Title  = "Please select an image";
            findImageDialog.Filter = "Bitmap (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png";
            findImageDialog.FilterIndex = 3;
            findImageDialog.ShowDialog();
            this.path     = findImageDialog.FileName.ToString();
            this.fileName = findImageDialog.SafeFileName.ToString();
            try
            {
                System.IO.File.Copy(this.path, this.CACHEPATH + this.fileName, true);
                this.bmp = Load_BitMap(this.CACHEPATH + this.fileName);
                this.ImageBox.Image = bmp;
                this.PathBox.Text   = this.path;
                Image image = this.ImageBox.Image;
                image.Save(Path.Combine(this.CACHEPATH, "0"));
                this.SaveButton.Enabled    = true;
                this.ImageBox.Enabled      = true;
                this.PipetteButton.Enabled = true;
            } catch(Exception) {
                // File dialog is closed and therefore no file is loaded
                this.SaveButton.Enabled = true;
                this.ImageBox.Enabled = true;
                this.PipetteButton.Enabled = true;
            }
        }


        /**
         * Cancel the color pick mode.
         */
        private void MainWindow_MouseClick(object sender, MouseEventArgs e)
        {
            this.ImageBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.readColorMode = false;
        }


        /**
         * Activate the color pick mode, where the user can click on a pixel in the image,
         * read its RGB values and use this color to determine the color of the rectangle, 
         * which will cover the texts in the image.
         */
        private void PipetteButton_Click(object sender, EventArgs e)
        {
            if(this.readColorMode)
            {
                this.ImageBox.Cursor = System.Windows.Forms.Cursors.Default;
                this.readColorMode = false;
            } else {
                this.ImageBox.Cursor = System.Windows.Forms.Cursors.Cross;
                this.readColorMode = true;
            }
        }


        /**
         * The user has 100 backup copies available to turn back to.
         * If 100 copies are reached, the 0th get deleted, image copies 1-100
         * are taking the position of their previous copy and image 100 can be saved.
         */
        private void Renumerade_Files()
        {
            try
            {
                for(int i = 1; i <= 100; i++)
                {
                    System.IO.File.Move(this.CACHEPATH + i.ToString(), this.CACHEPATH + (i - 1).ToString());
                }
            } catch (Exception) { } // One file is missing
        }


        /**
         * Safe the image from the Imagebox with the file dialog 
         */
        private void SaveButton_Click(object sender, EventArgs e)
        {
            this.ImageBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.readColorMode = false;
            SaveFileDialog saveImageDialog = new SaveFileDialog();
            saveImageDialog.Title  = "Save the image";
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
    }
}
