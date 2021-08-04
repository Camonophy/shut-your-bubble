using System;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

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
        }

        private void ImageBox_DragDrop(object sender, DragEventArgs e)
        {
            
        }

        private void ImageBox_MouseMove(object sender, MouseEventArgs e)
        {
            this.CurrentXBox.Text = e.X.ToString();
            this.CurrentYBox.Text = e.Y.ToString();
        }
    }
}
