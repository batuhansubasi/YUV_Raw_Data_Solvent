using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Threading;
using System.Drawing.Imaging;

namespace YazLab3
{
    public partial class Form1 : Form
    {
        Boolean kontrol = true;
        int a = 1, sayac = 0, y_ct = 0, i = 0, deneme = 0, katsayi = 0, bir_pencere_y_byte_sayisi;
        double pencere_sayisi;
        String dosya_ismi;
        string[] y_dizim;
        byte[] b;

        public Form1()
        {
            InitializeComponent();
            //groupBox3.Visible = false;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "BMP KAYDET";
            save.CreatePrompt = true;
            save.ShowDialog();


                DirectoryInfo dir = new DirectoryInfo("C:\\Users\\batuhan.subasi\\Desktop\\YazLab3\\frames");
                dir.MoveTo(save.FileName);
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public Boolean KKK()
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Uzunluk bilgisini girmediniz.");
                kontrol = false;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("En bilgisini girmediniz.");
                kontrol = false;
            }
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Format bilgisini girmediniz.");
                kontrol = false;
            }

            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9]"))
            {
                MessageBox.Show("Boy bilgisi için lütfen sadece sayi giriniz.");
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
                kontrol = false;
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, "[^0-9]"))
            {
                MessageBox.Show("En bilgisi için lütfen sadece sayi giriniz.");
                textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
                kontrol = false;
            }
            if (a == 1)
            {
                MessageBox.Show(".yuv Dosyası girmediniz, onay için lütfen .yuv dosyasi giriniz.");
                kontrol = false;
            }
            return kontrol;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Width  = Convert.ToInt32(textBox1.Text);
            pictureBox1.Height = Convert.ToInt32(textBox2.Text);

            kontrol = KKK();

            if (kontrol == true)
            {
                groupBox3.Visible = true;
                FileStream fs = new FileStream(dosya_ismi, FileMode.Open);
                using (BinaryReader br = new BinaryReader(fs))
                {
                    b = br.ReadBytes((int)fs.Length);
                }
                int a = comboBox1.SelectedIndex;
                if (a == 0) //4-4-4
                {
                    pencere_sayisi = (int)b.Length / (Convert.ToInt32(textBox1.Text) * Convert.ToInt32(textBox2.Text) * 3);
                }
                else if (a == 1) //4-2-2
                {
                    pencere_sayisi = (int)b.Length / (Convert.ToInt32(textBox1.Text) * Convert.ToInt32(textBox2.Text) * 2);
                }
                else if (a == 2) //4-2-0
                {
                    pencere_sayisi = (int)b.Length / (Convert.ToInt32(textBox1.Text) * Convert.ToInt32(textBox2.Text) * 1.5);
                }
                double bir_pencerelik_byte = b.Length / pencere_sayisi; //1 pencerenin alması gereken byte array sayisi
                System.IO.Directory.CreateDirectory("C:\\Users\\batuhan.subasi\\Desktop\\YazLab3\\frames");
                System.IO.Directory.CreateDirectory("C:\\Users\\batuhan.subasi\\Desktop\\YazLab3\\frames\\d");

                if (a == 0)
                {
                    double y_sayisi = bir_pencerelik_byte / 3; 
                    bir_pencere_y_byte_sayisi = Convert.ToInt32(y_sayisi);                
                    y_dizim = new string[b.Length / 3];
                    while (i < b.Length)
                    {
                        y_dizim[sayac] = b[i].ToString();
                        sayac++;
                        y_ct++;
                        i++;
                        if (y_ct == bir_pencere_y_byte_sayisi)
                        {
                            y_ct = 0;
                            i = i + (bir_pencere_y_byte_sayisi * 2);
                        }
                    }
                    while (katsayi != pencere_sayisi - 1)
                    {
                        Bitmap resim = new Bitmap(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
                        deneme = bir_pencere_y_byte_sayisi * katsayi;
                        for (int z = 0; z < Convert.ToInt32(textBox2.Text); z++)
                        {
                            for (int y = 0; y < Convert.ToInt32(textBox1.Text); y++)
                            {
                                deneme++;
                                resim.SetPixel(y, z, Color.FromArgb(Int32.Parse(y_dizim[deneme]), Int32.Parse(y_dizim[deneme]), Int32.Parse(y_dizim[deneme])));
                            }
                        }
                        pictureBox1.Image = resim;
                        pictureBox1.Refresh();
                        katsayi++;
                        resim.Save(@"C:\Users\batuhan.subasi\Desktop\YazLab3\frames\\d" + (katsayi) + "Frame.bmp");
                    }
                }

                else if (a == 1)
                {
                    double y_sayisi = bir_pencerelik_byte / 2; 
                    int bir_pencere_y_byte_sayisi = Convert.ToInt32(y_sayisi);                
                    y_dizim = new string[b.Length / 2];
                    while (i < b.Length)
                    {
                        y_dizim[sayac] = b[i].ToString();
                        sayac++;
                        y_ct++;
                        i++;
                        if (y_ct == bir_pencere_y_byte_sayisi)
                        {
                            y_ct = 0;
                            i = i + (bir_pencere_y_byte_sayisi * 1);
                        }
                    }
                    while (katsayi != pencere_sayisi - 1)
                    {
                        Bitmap resim = new Bitmap(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
                        deneme = bir_pencere_y_byte_sayisi * katsayi;
                        for (int z = 0; z < Convert.ToInt32(textBox2.Text); z++)
                        {
                            for (int y = 0; y < Convert.ToInt32(textBox1.Text); y++)
                            {
                                deneme++;
                                resim.SetPixel(y, z, Color.FromArgb(Int32.Parse(y_dizim[deneme]), Int32.Parse(y_dizim[deneme]), Int32.Parse(y_dizim[deneme])));
                            }
                        }
                        pictureBox1.Image = resim;
                        pictureBox1.Refresh();
                        katsayi++;
                        resim.Save(@"C:\Users\batuhan.subasi\Desktop\YazLab3\frames\\d" + (katsayi) + "Frame.bmp");
                    }
                }

                else if (a == 2)
                {
                    double y_sayisi = (bir_pencerelik_byte / 3) * 2; 
                    int bir_pencere_y_byte_sayisi = Convert.ToInt32(y_sayisi);              
                    y_dizim = new string[(b.Length / 3) * 2];
                    while (i < b.Length)
                    {
                        y_dizim[sayac] = b[i].ToString();
                        sayac++;
                        y_ct++;
                        i++;
                        if (y_ct == bir_pencere_y_byte_sayisi)
                        {
                            y_ct = 0;
                            i = i + (bir_pencere_y_byte_sayisi / 2);
                        }
                    }
                    while (katsayi != pencere_sayisi - 1)
                    {
                        Bitmap resim = new Bitmap(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
                        deneme = bir_pencere_y_byte_sayisi * katsayi;
                        for (int z = 0; z < Convert.ToInt32(textBox2.Text); z++)
                        {
                            for (int y = 0; y < Convert.ToInt32(textBox1.Text); y++)
                            {
                                deneme++;
                                resim.SetPixel(y, z, Color.FromArgb(Int32.Parse(y_dizim[deneme]), Int32.Parse(y_dizim[deneme]), Int32.Parse(y_dizim[deneme])));
                            }
                        }
                        pictureBox1.Image = resim;
                        pictureBox1.Refresh();
                        katsayi++;
                        resim.Save(@"C:\Users\batuhan.subasi\Desktop\YazLab3\frames\\d" + (katsayi) + "Frame.bmp");
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog files = new OpenFileDialog();
            files.Filter = "YUV Dosyası |*.yuv";
            files.ShowDialog();
            dosya_ismi = files.FileName;
            
            if(string.IsNullOrEmpty(files.FileName))
            {
                a = 1;
            }
            else
            {
                a = 2;
            }

        }

    }
}
