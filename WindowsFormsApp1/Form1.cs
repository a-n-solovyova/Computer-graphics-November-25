using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging; // namespace that contains PixelFormat

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap myBitmap;

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                myBitmap = new Bitmap(openFileDialog1.FileName);
                DrawBitmap(myBitmap, panel1);               

            }
            
        }

        private void DrawBitmap(Bitmap bmp, Control control)
        {
            Graphics myGraphics = control.CreateGraphics(); //creates a canvas to draw on

            RectangleF sourceRectangle =
                new RectangleF(0, 0, bmp.Width, bmp.Height);

            RectangleF destinationRectangle =
                new RectangleF(0, 0, control.Width, control.Height);

            myGraphics.DrawImage(bmp, destinationRectangle,
                sourceRectangle, GraphicsUnit.Pixel);

            myGraphics.Dispose(); // clear the memory allocated for the canvas
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap newBitmap = new Bitmap(myBitmap);

            //---Filtering myBitmap into newBitmap---

            float[][] filterKernelMatrix =
            {
                new float[] { 0.125f, 0.0625f, 0.125f },
                new float[] { 0.125f, 0.25f, 0.125f },
                new float[] { 0.125f, 0.0625f, 0.125f }
            };

            for (int i = 1; i < newBitmap.Width-1; i++)
            {
                for (int j = 1; j < newBitmap.Height-1; j++)
                {
                    float red =
                        myBitmap.GetPixel(i - 1, j - 1).R * filterKernelMatrix[0][0] +
                        myBitmap.GetPixel(i, j - 1).R * filterKernelMatrix[0][1] +
                        myBitmap.GetPixel(i + 1, j - 1).R * filterKernelMatrix[0][2] +
                        myBitmap.GetPixel(i - 1, j).R * filterKernelMatrix[1][0] +
                        myBitmap.GetPixel(i, j).R * filterKernelMatrix[1][1] +
                        myBitmap.GetPixel(i + 1, j).R * filterKernelMatrix[1][2] +
                        myBitmap.GetPixel(i - 1, j + 1).R * filterKernelMatrix[2][0] +
                        myBitmap.GetPixel(i, j + 1).R * filterKernelMatrix[2][1] +
                        myBitmap.GetPixel(i + 1, j + 1).R * filterKernelMatrix[2][2];

                    red = red < 0 ? 0 : red;   // if red < 0 then red = 0 else red = red
                    red = red > 255 ? 255 : red; // if red > 255 then red = 255 else red = red
                                    
                    // < Condition > ? < Action If True > : < Action If False >

                    float green =
                        myBitmap.GetPixel(i - 1, j - 1).G * filterKernelMatrix[0][0] +
                        myBitmap.GetPixel(i, j - 1).G * filterKernelMatrix[0][1] +
                        myBitmap.GetPixel(i + 1, j - 1).G * filterKernelMatrix[0][2] +
                        myBitmap.GetPixel(i - 1, j).G * filterKernelMatrix[1][0] +
                        myBitmap.GetPixel(i, j).G * filterKernelMatrix[1][1] +
                        myBitmap.GetPixel(i + 1, j).G * filterKernelMatrix[1][2] +
                        myBitmap.GetPixel(i - 1, j + 1).G * filterKernelMatrix[2][0] +
                        myBitmap.GetPixel(i, j + 1).G * filterKernelMatrix[2][1] +
                        myBitmap.GetPixel(i + 1, j + 1).G * filterKernelMatrix[2][2];

                    green = green < 0 ? 0 : green;
                    green = green > 255 ? 255 : green;

                    float blue =
                        myBitmap.GetPixel(i - 1, j - 1).B * filterKernelMatrix[0][0] +
                        myBitmap.GetPixel(i, j - 1).B * filterKernelMatrix[0][1] +
                        myBitmap.GetPixel(i + 1, j - 1).B * filterKernelMatrix[0][2] +
                        myBitmap.GetPixel(i - 1, j).B * filterKernelMatrix[1][0] +
                        myBitmap.GetPixel(i, j).B * filterKernelMatrix[1][1] +
                        myBitmap.GetPixel(i + 1, j).B * filterKernelMatrix[1][2] +
                        myBitmap.GetPixel(i - 1, j + 1).B * filterKernelMatrix[2][0] +
                        myBitmap.GetPixel(i, j + 1).B * filterKernelMatrix[2][1] +
                        myBitmap.GetPixel(i + 1, j + 1).B * filterKernelMatrix[2][2];

                    blue = blue < 0 ? 0 : blue;
                    blue = blue > 255 ? 255 : blue;

                    newBitmap.SetPixel(i, j, Color.FromArgb((int)red, (int)green, (int)blue));
                }
            }

            DrawBitmap(newBitmap, panel1);
        }
    }
}
