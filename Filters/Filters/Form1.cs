using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filters
{
    public partial class Form1 : Form
    {
        int buttonMode = 0;
        bool orientMode = true;
        Bitmap image;
        public Form1()
        {
            InitializeComponent();
        }

        private void ShowTrackBar(int buttonType)
        {
            trackBar1.Maximum = 10;
            trackBar1.Show();
            applyButton.Show();
            buttonMode = buttonType;
            closeButton.Show();
        }

        private void ShowTransformOptions(bool move)
        {
                if (move)
                {
                    labelHor.Text = "Horizontal";
                    labelVert.Text = "Vertikal";
                    buttonMode = 4;
                }
                else
                {
                    labelHor.Text = "Clockwise";
                    labelVert.Text = "Counter-clockwise";
                    buttonMode = 5;
                }
                labelHor.Show();
                labelVert.Show();
                textBoxHor.Text = "";
                textBoxVert.Text = "";
                textBoxHor.Show();
                textBoxVert.Show();
                applyButton.Show();
                closeButton.Show();
        }

        private void HideAll()
        {
            labelHor.Hide();
            labelVert.Hide();
            textBoxHor.Hide();
            textBoxVert.Hide();
            applyButton.Hide();
            closeButton.Hide();
            orientSwitchButton.Hide();
            trackBar1.Hide();
            trackBar1.Value = 0;
            buttonMode = 0;
            filterNameLabel.Visible = true;
        }

        private void inversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Inversion";
            applyButton.Visible = true;
            buttonMode = 14;
            closeButton.Visible = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void blurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Blur";
            applyButton.Visible = true;
            buttonMode = 15;
            closeButton.Visible = true;
        }

        private void gaussianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Gaussian";
            applyButton.Visible = true;
            buttonMode = 16;
            closeButton.Visible = true;
        }

        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "GreyScale";
            applyButton.Visible = true;
            buttonMode = 17;
            closeButton.Visible = true;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Sepia";
            ShowTrackBar(1);
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            Filters filter;
            switch (buttonMode)
            {
                case 1:
                    filter = new SepiaFilter(trackBar1.Value);
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 2:
                    filter = new BrightnessFilter(trackBar1.Value);
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 3:
                    filter = new SobelFilter(orientMode);
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 4:
                    if (textBoxHor.Text == "")
                        textBoxHor.Text = "0";
                    if (textBoxVert.Text == "")
                        textBoxVert.Text = "0";
                    filter = new Move(Convert.ToInt32(textBoxHor.Text), Convert.ToInt32(textBoxVert.Text));
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 5:
                    if (textBoxHor.Text == "")
                        textBoxHor.Text = "0";
                    if (textBoxVert.Text == "")
                        textBoxVert.Text = "0";
                    filter = new Turn(Convert.ToInt32(textBoxHor.Text) - Convert.ToInt32(textBoxVert.Text));
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 6:
                    filter = new Waves(trackBar1.Value, orientMode);
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 7:
                    filter = new Glass(trackBar1.Value);
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 8:
                    filter = new BorderSelection(trackBar1.Value, orientMode);
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 9:
                    filter = new MedianFilter();
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 10:
                    filter = new MaximumFilter();
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 11:
                    filter = new GlowingBorders(trackBar1.Value);
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 12:
                    filter = new Dilation(trackBar1.Value);
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 13:
                    filter = new Erosion(trackBar1.Value);
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 14:
                    filter = new InvertFilter();
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 15:
                    filter = new BlurFilter();
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 16:
                    filter = new GaussianFilter();
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 17:
                    filter = new GreyScaleFilter();
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 18:
                    filter = new SharpnessFilter();
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 19:
                    filter = new SharpnessFilter2();
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 20:
                    EmbossingFilter filter_ = new EmbossingFilter();
                    backgroundWorker1.RunWorkerAsync(filter_);
                    break;

                case 21:
                    GreyWorldFilter _filter_ = new GreyWorldFilter();
                    backgroundWorker1.RunWorkerAsync(_filter_);
                    break;

                case 22:
                    filter = new HistogramSretching();
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 23:
                    filter = new Opening(trackBar1.Value);
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 24:
                    filter = new Closing(trackBar1.Value);
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 25:
                    filter = new Grad(trackBar1.Value);
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 26:
                    filter = new TopHat(trackBar1.Value);
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

                case 27:
                    filter = new BlackHat(trackBar1.Value);
                    backgroundWorker1.RunWorkerAsync(filter);
                    break;

            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Visible = false;
        }

        private void brightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Brightness";
            ShowTrackBar(2);
            trackBar1.Value = 5;
        }

        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Sobel";
            orientSwitchButton.Show();
            applyButton.Show();
            buttonMode = 3;
            closeButton.Show();
        }

        private void sharpnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Sharpness";
            applyButton.Visible = true;
            buttonMode = 18;
            closeButton.Visible = true;
        }
        private void sharpness2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Sharpness";
            applyButton.Visible = true;
            buttonMode = 19;
            closeButton.Visible = true;
        }
        private void embossingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Embossing";
            applyButton.Visible = true;
            buttonMode = 20;
            closeButton.Visible = true;
        }

        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Move";
            ShowTransformOptions(true);
        }

        private void saveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        pictureBox1.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        pictureBox1.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        pictureBox1.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Show();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files |*.png;*.jpg;*.bmp|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }

        private void turnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Turn";
            ShowTransformOptions(false);
        }

        private void wavesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Waves";
            ShowTrackBar(6);
            orientSwitchButton.Show();
        }

        private void orientSwitchButton_Click(object sender, EventArgs e)
        {
            if (orientMode)
            {
                orientSwitchButton.Text = "Vertical";
                orientMode = !orientMode;
            }
            else
            {
                orientSwitchButton.Text = "Horizontal";
                orientMode = !orientMode;
            }
        }

        private void glassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Glass";
            ShowTrackBar(7);
        }

        private void borderSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "BorderSelection";
            orientSwitchButton.Show();
            ShowTrackBar(8);
        }

        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Median";
            applyButton.Visible = true;
            buttonMode = 9;
            closeButton.Visible = true;
        }

        private void maximumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Maximum";
            applyButton.Visible = true;
            buttonMode = 10;
            closeButton.Visible = true;
        }

        private void glowingBordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "GlowingBorders";
            ShowTrackBar(11);
        }

        private void greyWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "GreyWorld";
            applyButton.Visible = true;
            buttonMode = 21;
            closeButton.Visible = true;
        }

        private void histogramStretchingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "HistogramStretching";
            applyButton.Visible = true;
            buttonMode = 22;
            closeButton.Visible = true;
        }

        private void dilationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Dilation";
            ShowTrackBar(12);
            trackBar1.Maximum = 2;
        }

        private void erosionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Erosion";
            ShowTrackBar(13);
            trackBar1.Maximum = 2;
        }

        private void openingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Opening";
            ShowTrackBar(23);
            trackBar1.Maximum = 2;
        }

        private void closingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Closing";
            ShowTrackBar(24);
            trackBar1.Maximum = 2;
        }

        private void gradToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "Grad";
            ShowTrackBar(25);
            trackBar1.Maximum = 2;
        }

        private void topHatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "TopHat";
            ShowTrackBar(26);
            trackBar1.Maximum = 2;
        }

        private void blackHatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideAll();
            filterNameLabel.Text = "BlackHat";
            ShowTrackBar(26);
            trackBar1.Maximum = 2;
        }
    }
}
