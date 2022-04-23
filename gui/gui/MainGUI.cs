using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace gui
{
    public partial class MainWindow : Form
    {
        private readonly String CACHEPATH  = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"../../Resources/Cache/";
        private readonly String SCRIPTPATH = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"../../../../src/Textinguisher.py";
        private String imagePath;
        private String imageName;

        private Bitmap bmp;

        private DirectoryInfo cachDirectory;

        private ColorDialog colDialog = new ColorDialog();

        private int index, stepCount = 0;

        System.Collections.Generic.Dictionary<String, String> languages = 
            new System.Collections.Generic.Dictionary<String, String>(){
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

        private bool pickColorMode = false;


        public MainWindow()
        {
            InitializeComponent();
            cachDirectory = new DirectoryInfo(this.CACHEPATH);
            String[] installed_languages = this.Get_Installed_Tessarect_Languages();

            foreach (String lan in installed_languages)
            {
                try { this.LanguageSelect.Items.Add(this.languages[lan]); } 
                catch(Exception) { } // Language not supported
            }

            if(this.LanguageSelect.Items.Count == 0)
            {
                this.LanguageSelect.Items.Add("No language found");
            }

            this.LanguageSelect.SelectedIndex = 0;
        }


        /// <summary>
        /// Undo the last actions taken and load the previous image
        /// from the Cache folder.
        /// </summary>
        private void BackButton_Click(object sender, EventArgs e)
        {
            this.ImageBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.pickColorMode = false;

            if (this.index > 0 && this.ImageBox.Image != null) 
            {
                try
                {
                    this.bmp = Load_BitMap(this.CACHEPATH + --this.index);
                    this.ImageBox.Image = this.bmp;
                    System.IO.File.Copy(this.CACHEPATH + this.index, this.CACHEPATH + this.imageName, true);
                    this.ForthButton.Enabled = true;
                } catch(FileNotFoundException) { } // No file is loaded and therefore can not be replaced by another one
            }

            // There is yet another image to load 
            this.BackButton.Enabled = System.IO.File.Exists(this.CACHEPATH + (this.index - 1));
        }


        private String Color_To_Hex(Color actColor)
        {
            return "#" + actColor.R.ToString("X2") + actColor.G.ToString("X2") + actColor.B.ToString("X2");
        }


        /// <summary>
        /// Read the content of the ColorBox, convert the hex number into
        /// a color and show it on the ColorButton.
        /// </summary>
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


        /// <summary>
        /// Open a color dialog to change the color of the
        /// painted rectangle.
        /// </summary>
        private void ColorButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.ImageBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.pickColorMode = false;

            // Sets the initial color select to the current text color.
            this.colDialog.Color = this.ColorButton.ForeColor;

            // Update the text box color if the user clicks OK 
            if (this.colDialog.ShowDialog() == DialogResult.OK)
                this.ColorButton.BackColor = this.colDialog.Color;
                this.ColorBox.Text = Color_To_Hex(this.ColorButton.BackColor);
        }


        /// <summary>
        /// Redo one step by loading the next image from the cache folder.
        /// </summary>
        private void ForthButton_Click(object sender, EventArgs e)
        {
            this.ImageBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.pickColorMode = false;

            if (this.index != this.stepCount && this.ImageBox.Image != null)
            {
                try
                {
                    this.bmp = Load_BitMap(this.CACHEPATH + ++this.index);
                    this.ImageBox.Image = this.bmp;
                    System.IO.File.Copy(this.CACHEPATH + this.index, this.CACHEPATH + this.imageName, true);
                    this.BackButton.Enabled = true;
                }
                catch (FileNotFoundException) { } // No file is loaded and therefore can not be replaced by another one
            }

            // There is yet another image to load 
            this.ForthButton.Enabled = System.IO.File.Exists(this.CACHEPATH + (this.index + 1));

        }


        private String[] Get_Installed_Tessarect_Languages()
        {
            String[] languages = new String[] { };

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
                    String stderr = process.StandardError.ReadToEnd();
                    String result = reader.ReadToEnd();
                    Console.Write(stderr == "" ? "" : stderr + "\n");
                    Console.Write(result == "" ? "" : result + "\n");
                    languages = result.Substring(2, result.Length - 5)
                                      .Replace(" ", "")
                                      .Replace("\'", "")
                                      .Split(',');
                }
            }

            return languages;
        }


        /// <summary>
        /// Track the X and Y coordinates in the ImageBox when the mouse button
        /// is pressed.
        /// </summary>
        private void ImageBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.pickColorMode)
            {
                this.StartXBox.Text = e.X.ToString();
                this.StartYBox.Text = e.Y.ToString();
            }
        }


        /// <summary>
        /// Track the X and Y coordinates in the ImageBox when the mouse
        /// is moved.
        /// </summary>
        private void ImageBox_MouseMove(object sender, MouseEventArgs e)
        {
            this.CurrentXBox.Text = e.X.ToString();
            this.CurrentYBox.Text = e.Y.ToString();
        }


        /// <summary>        
        /// Track the X and Y coordinates in the ImageBox when the mouse button
        /// is released.
        /// 
        /// Save an image backup, run the Python script on the defined area in the image
        /// and load the new image into the ImageBox.
        /// </summary>
        private void ImageBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!this.pickColorMode)
            {
                this.ImageBox.Cursor    = System.Windows.Forms.Cursors.WaitCursor;
                this.ImageBox.Enabled   = false;
                this.SaveButton.Enabled = false;
                this.ForthButton.Enabled = false;
                this.BackButton.Enabled = false;
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
                int[] xVals = new int[2] { int.Parse(this.StartXBox.Text.ToString()), int.Parse(this.EndXBox.Text.ToString()) };
                int[] yVals = new int[2] { int.Parse(this.StartYBox.Text.ToString()), int.Parse(this.EndYBox.Text.ToString()) };
                start.Arguments = this.SCRIPTPATH +                              
                                  String.Format(" {0} {1} {2} {3} {4} {5} {6} ", 
                                                 xVals.Min(),
                                                 yVals.Min(), 
                                                 xVals.Max() - xVals.Min(),
                                                 yVals.Max() - yVals.Min(),
                                                 this.ColorBox.Text,
                                                 this.languages.FirstOrDefault(x => x.Value == this.LanguageSelect.Text).Key,
                                                 this.CACHEPATH + this.imageName);
                start.UseShellExecute        = false;
                start.CreateNoWindow         = true; 
                start.RedirectStandardOutput = true;
                start.RedirectStandardError  = true; 
                start.LoadUserProfile        = true;
                using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        String stderr = process.StandardError.ReadToEnd(); 
                        String result = reader.ReadToEnd(); 
                        Console.Write(stderr == "" ? "" : stderr + "\n");
                        Console.Write(result == "" ? "" : result + "\n");
                    }
                }

                // Load the Python output file
                this.ImageBox.Image = Load_BitMap(this.CACHEPATH + this.imageName);

                // Save the new picture as a backup copy
                try
                {
                    Image image = this.ImageBox.Image;
                    image.Save(this.CACHEPATH + this.stepCount);

                } catch (NullReferenceException) { } // No image is loaded and therefore no image can be saved

                this.ImageBox.Cursor    = System.Windows.Forms.Cursors.Default;
                this.ImageBox.Enabled   = true;
                this.SaveButton.Enabled = true;
                this.BackButton.Enabled = true;
            } else {
                Color pixelColor   = this.bmp.GetPixel(e.X, e.Y);
                this.ColorBox.Text = Color_To_Hex(pixelColor);
                this.ColorButton.BackColor = pixelColor;
                this.ImageBox.Cursor = System.Windows.Forms.Cursors.Default;
                this.pickColorMode = false;
            }
        }
        

        private Bitmap Load_BitMap(String path)
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
                return null;
            }
        }


        /// <summary>        
        /// Load an image from file dialog into the ImageBox.
        /// </summary>
        private void Load_Click(object sender, EventArgs e)
        {
            this.ImageBox.Cursor    = System.Windows.Forms.Cursors.Default;
            bool forth = this.ForthButton.Enabled;
            bool back = this.BackButton.Enabled;

            this.pickColorMode      = false;
            this.ImageBox.Enabled   = false;
            this.SaveButton.Enabled = false;
            this.ForthButton.Enabled = false;
            this.BackButton.Enabled = false;

            OpenFileDialog findImageDialog = new OpenFileDialog();
            findImageDialog.Title  = "Please select an image";
            findImageDialog.Filter = "Bitmap (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg|PNG (*.png)|*.png";
            findImageDialog.FilterIndex = 3;
            findImageDialog.ShowDialog();
            String path     = findImageDialog.FileName.ToString();
            String name     = findImageDialog.SafeFileName.ToString();
            Bitmap newImage = Load_BitMap(path);

            if (newImage == null)
            {
                this.SaveButton.Enabled = true;
                this.ImageBox.Enabled = true;
                this.PipetteButton.Enabled = true;
                return;
            }

            try
            {
                foreach (FileInfo file in cachDirectory.GetFiles())
                {
                    try { file.Delete(); }
                    catch (IOException) { } // This file is still opened somewhere
                }
                this.imagePath = path;
                this.imageName = name;
                this.bmp = newImage;
                this.ImageBox.Image = this.bmp;
                this.PathBox.Text   = this.imagePath;
                Image image = this.ImageBox.Image;
                image.Save(Path.Combine(this.CACHEPATH, "0"));
                this.SaveButton.Enabled    = true;
                this.ImageBox.Enabled      = true;
                this.PipetteButton.Enabled = true;
                this.stepCount             = 0;
                this.index                 = 0;
                System.IO.File.Copy(this.imagePath, this.CACHEPATH + this.imageName, true);

            } catch(Exception) {
                // File dialog is closed, hence no file is loaded
                if (this.ImageBox.Image != null)
                {
                    this.SaveButton.Enabled    = true;
                    this.ImageBox.Enabled      = true;
                    this.PipetteButton.Enabled = true;
                    this.ForthButton.Enabled   = forth;
                    this.BackButton.Enabled    = back;
                }
            }
        }


        /// <summary>        
        /// Cancel the color pick mode.
        /// </summary>
        private void MainWindow_MouseClick(object sender, MouseEventArgs e)
        {
            this.ImageBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.pickColorMode = false;
        }


        /// <summary>        
        /// Activate the color pick mode.
        /// 
        /// Load the RGB value of a pixel into the color related elements in the GUI.
        /// </summary>
        private void PipetteButton_Click(object sender, EventArgs e)
        {
            if(this.pickColorMode)
            {
                this.ImageBox.Cursor = System.Windows.Forms.Cursors.Default;
                this.pickColorMode = false;
            } else {
                this.ImageBox.Cursor = System.Windows.Forms.Cursors.Cross;
                this.pickColorMode = true;
            }
        }


        /// <summary>        
        /// After 100 backup copies, file 0 gets deleted, the remaining files are getting
        /// pulled down by one position and the new file takes the position 100.
        /// </summary>
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


        /// <summary>        
        /// Bring up the save file dialog to save the file from the ImageBox.
        /// </summary>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            this.ImageBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.pickColorMode = false;
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
