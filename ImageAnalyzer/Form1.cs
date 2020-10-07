using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageAnalyzer
{
    public partial class Form1 : Form
    {
        Bitmap image, newImage;
        Color[,] pixel;
        String imagePath = "Empty";
        bool openImageSuccess = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            imagePath = "Empty";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            
            if (dlg.FileName != null && dlg.FileName != "")
                imagePath = dlg.FileName;
            
            OpenImage(imagePath);
            textBox1.Text = imagePath;
        }

        private void OpenImage(string path)
        {
            if (path == "Empty")
                throw new Exception("Podaj ścieżkę pliku");
            try
            {
                image = new Bitmap(path);
                pixel = new Color[image.Width, image.Height];
                for (int i = 0; i < image.Width; i++)
                    for (int j = 0; j < image.Height; j++)
                        pixel[i, j] = image.GetPixel(i, j);
                openImageSuccess = true;
            }
            catch
            {
                Exception ex = new Exception("Podany plik nie jest prawidłowym plikiem obrazu");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openImageSuccess)
                Convert();
        }

        private void Convert()
        {
            newImage = (Bitmap)image.Clone();
            //newImage = new Bitmap(image.Width, image.Height);
            int y = pixel.GetLength(1)/2;
            for (int i = 0; i < pixel.GetLength(0); i++)
            {
                if (PixelIsAir(i, y))
                    SetPixelColor(i, y);
            }
            newImage.Save("F://Paweł/img2.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
        }

        private bool PixelIsAir(int x, int y)
        {
            int r, g, b;
            r = pixel[x, y].R;
            g = pixel[x, y].G;
            b = pixel[x, y].B;

            if (r >= 125 && g >= 125 && b <= 100)
                return true;
            return false;
        }

        private void SetPixelColor(int x, int y)
        {
            newImage.SetPixel(x,y,Color.FromArgb(100,255,0,0));
        }
    }
}
