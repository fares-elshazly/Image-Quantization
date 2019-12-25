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

        int K;
        RGBPixel[,] ImageMatrix;
        int Distinct_Colors;
        EagerPrimMST MST;
        Edge[] edges;
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);

                long timeBefore = Environment.TickCount;

                Distinct_Colors = ImageUtilities.GetDistinctColors(ImageMatrix);
                MST = new EagerPrimMST(Distinct_Colors);
                double MST_SUM = MST.GetMst();
                edges = MST.Edge_To;
                txtDistinctColours.Text = Distinct_Colors.ToString();
                txtMSTSum.Text = MathUtilities.RoundUp(MST_SUM, 1).ToString();
                long timeAfter = Environment.TickCount;

                txtTime.Text = (timeAfter - timeBefore).ToString();
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();

        }

        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            double sigma = double.Parse(txtGaussSigma.Text);
            int maskSize = (int)nudMaskSize.Value;
           
            if (K_Clusters.Text.ToString().Length > 0)
            {

                K = int.Parse(K_Clusters.Text);
                KClustring Clusters = new KClustring(K, MST.Nodes, MST.Edge_To);
                Clusters.Clustering();
                Clusters.QuantizeImage(Clusters.Palette, ImageMatrix);
                //ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
                ImageOperations.DisplayImage(Clusters.Quantized_Image, pictureBox2);

            }
            else
            {
                MathUtilities m = new MathUtilities();
                K = m.AutoKdetection(edges);
                K = m.K;
                MessageBox.Show(K.ToString());
                KClustring Clusters = new KClustring(K, MST.Nodes, MST.Edge_To);
                Clusters.Clustering();
                Clusters.QuantizeImage(Clusters.Palette, ImageMatrix);
                //ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
                ImageOperations.DisplayImage(Clusters.Quantized_Image, pictureBox2);

            }

        }



    }
}