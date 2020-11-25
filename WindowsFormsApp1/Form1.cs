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
                Graphics myGraphics = panel1.CreateGraphics(); //creates a canvas to draw on
                
                myBitmap = new Bitmap(openFileDialog1.FileName);

                RectangleF sourceRectangle =
                    new RectangleF(0, 0, myBitmap.Width, myBitmap.Height);

                RectangleF destinationRectangle =
                    new RectangleF(0, 0, panel1.Width, panel1.Height);
                                
                myGraphics.DrawImage(myBitmap, destinationRectangle,
                    sourceRectangle, GraphicsUnit.Pixel);

                myGraphics.Dispose(); // clear the memory allocated for the canvas

            }
            
        }


    }
}
