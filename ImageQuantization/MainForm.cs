using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImageQuantization
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                long timeBefore = Environment.TickCount;

                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);

                List<RGBPixel> DistinctColours = new List<RGBPixel>(ImageOperations.DistinctColours);
                Graph Graph = new Graph(DistinctColours.Count, DistinctColours.Count * DistinctColours.Count);
                Graph.BuildAdjacencyList(DistinctColours);
                double MSTSum = MST.PrimMST(Graph.AdjacencyList, DistinctColours.Count);

                long timeAfter = Environment.TickCount;

                txtDistinctColours.Text = DistinctColours.Count.ToString();
                txtMSTSum.Text = MathUtilities.RoundUp(MSTSum, 1).ToString();
                txtTime.Text = (timeAfter - timeBefore).ToString();
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
        }

        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            double sigma = double.Parse(txtGaussSigma.Text);
            int maskSize = (int)nudMaskSize.Value ;
            ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }
    }
}