using System;
using System.Net;
using MySql.Data;
using System.Net.Mail;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Globalization;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace PlayRoomForm
{
    public partial class Form1 : Form
    {
        
        OpenFileDialog file = new OpenFileDialog();   //İŞLENECEK DOSYANIN LOKASYONUNU BELİRLENİR
        int which;

        MySqlConnection baglan = new MySqlConnection("Server=127.0.0.1; Database=helloworld;Uid='root';Pwd='';");
        
        public Form1()
        {


            InitializeComponent();

            baglan.Open();
           

            fill1();


            MySqlCommand command2 = new MySqlCommand("SELECT * FROM helloworld.sertifikaalan;", baglan);
            MySqlDataAdapter dtb2 = new MySqlDataAdapter();
            dtb2.SelectCommand = command2;
            DataTable dtable2 = new DataTable();                                                                            //KULLANICILARIN OLDUĞU VERİTABANI ÇEKİLDİ VE DATAGRİDVİEW2 DOLDURULDU
            dtb2.Fill(dtable2);
            dataGridView2.DataSource = dtable2;

            



            foreach (FontFamily font in System.Drawing.FontFamily.Families)             
            {
               comboBox1.Items.Add(font.Name);                                           //SİSTEM ÜZERİNDEKİ FONTLARIN EKLEME
            }
            whichone();
            
        }
      
        void whichone() 
        {
            int i = 0;
            try
            {
                while (dataGridView1.Rows[i].Cells[0].Value.ToString() == "True")
                {
                    i++;
                }
                which = i;          //BANA HANGİ SATIRI İŞLEMEM GEREKTİĞİNİ GÖSTERECEK
            }
            catch 
            {
                MessageBox.Show("Mevcut İş yok","Tüh BE");
            }
        }
        void fill1() 
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM helloworld.sertifikagonderilen;", baglan);
            MySqlDataAdapter dtb = new MySqlDataAdapter();                                                                  //SERTİFİKALARIN OLDUĞU VERİTABANI ÇEKİLDİ VE DATAGRİDVİEW1 DOLDURULDU
            dtb.SelectCommand = command;
            DataTable dtable = new DataTable();
            dtb.Fill(dtable);
       
   
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = dtable;
            izle();
        }


        void izle()
        {
            Font font = new Font(comboBox1.Text, Convert.ToInt32(numericUpDown1.Value));
            StringFormat format = new StringFormat();           //
            format.LineAlignment = StringAlignment.Center;      // CENTER POZİSYONUNDA ÇIKTI VERMEK İÇİN FORMAT TANIMLADIM
            format.Alignment = StringAlignment.Center;          //
          
            Brush pen = new LinearGradientBrush(new Point(1, 1), new Point(100, 100), button4.BackColor, button4.BackColor);
            Point point = new Point(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox1.Text));
            StreamReader Oku = new StreamReader("D:\\Metin.txt");
            string Okunan = Oku.ReadToEnd();
            Oku.Close();
            string[] Personel = Okunan.Split('|');

            Random rasgele = new Random();
            int random = rasgele.Next(13);

            //byte[] imageBytes = Convert.FromBase64String(dataGridView1.Rows[which].Cells[5].Value.ToString().Substring(22));
            //MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);                                               //BASE64 
            //Image image = Image.FromStream(ms, true);
            //Bitmap srt = new Bitmap(image);



            var data = (Byte[])(dataGridView1.Rows[which].Cells[5].Value);
            var stream = new MemoryStream(data);                                                        //BLOB
            picboxview.Image = Image.FromStream(stream);



           // picboxview.Image = srt;
           //picboxview.Image = image;







            //Bitmap preview = new Bitmap(file.FileName);
            //picboxview.Image = preview;
            Graphics gr = Graphics.FromImage(picboxview.Image);

            gr.DrawString(Personel[random], font, pen, point, format);

        }
        void Button1_Click(object sender, EventArgs e)   //RESİM SEÇ
        {
            
         
            file.Filter = "Picture (*.jpg)|*.jpg";
            file.ShowDialog();
            
               Bitmap pic = new Bitmap(file.FileName);  //İŞLENECEK RESMİ BİTMAP A DÖNÜŞTÜR
                pictureBox1.Image = pic;                //PİCTUREBOX1 E GÖSTER
                 pictureBox2.Image = pic;               //OBJENİN İÇİNİ DOLDUR
         
            
        }
         void Button3_Click(object sender, EventArgs e)   //PREVİEW
            
        {
            izle();

        }

        void Button2_Click(object sender, EventArgs e)    //İŞLE
        {
            StringFormat format = new StringFormat();           //
            format.LineAlignment = StringAlignment.Center;      // CENTER POZİSYONUNDA ÇIKTI VERMEK İÇİN FORMAT
            format.Alignment = StringAlignment.Center;          //

            SaveFileDialog save = new SaveFileDialog();  //SAVE ALINACAK KONUMU BELİRLE
           
            save.Filter = "(*.jpg)|*.jpg";
            Font font = new Font(comboBox1.Text,Convert.ToInt32(numericUpDown1.Value));           //YAZININ YAZILACALIĞI FONT VE PUNTO

            Random password = new Random();             //RANDOM TANIMLA
            string final = "";
            for (int i = 0; i <= 8; i++)
            {
                int rasgele = password.Next(34, 126);   //RANDOM SEÇ
                if(rasgele==47|| rasgele==58||rasgele==92||rasgele ==42||rasgele== 63  || rasgele == 34  || rasgele == 60|| rasgele == 62|| rasgele == 124)
                {
                    rasgele = rasgele + 6;
                }
                char c;
                c = Convert.ToChar(rasgele);
                final = Convert.ToString(c) + final; ;

            }           //DOSYA ADI ŞİFRELEME
            save.ShowDialog();
            save.FileName = save.FileName+final;
            

            Brush pen = new LinearGradientBrush(new Point(1, 1), new Point(100, 100), button4.BackColor, button4.BackColor);  //BASILAN STRİNG İFADE İÇİNE DOLGU
            Point point = new Point(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox1.Text));                          //BASLAN STRİNG İFADE İÇİN PİVOT NOKTASI

            SmtpClient sc = new SmtpClient();                                                                 //                             
            sc.Port = 587;                                                                                    //GMAİL İŞLEMLERİ
            sc.Host = "smtp.gmail.com";                                                                       //
            sc.EnableSsl = true;

            int personel = 0;

         //   byte[] imageBytes = Convert.FromBase64String(dataGridView1.Rows[which].Cells[5].Value.ToString().Substring(22));

            //MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);         //BASE64 
            //Image image = Image.FromStream(ms, true);
            //Bitmap srt = new Bitmap(image);
           
            var data = (Byte[])(dataGridView1.Rows[which].Cells[5].Value);
            var stream = new MemoryStream(data);                                                        //BLOB
            

            pictureBox1.Image = Image.FromStream(stream);
            pictureBox2.Image = Image.FromStream(stream);

            for (int i=1; i < dataGridView2.Rows.Count;i++)                                   
            {
                Bitmap root = new Bitmap(pictureBox1.Image);                           //DİĞER DOSYA ÜZERİNDE İŞLEM YAPINCA ANA DOSYANIN KAYBOLMAMASI İÇİN


                if (dataGridView1.Rows[which].Cells[1].Value.ToString() == dataGridView2.Rows[personel].Cells[3].Value.ToString())
                {
                    
                    //MessageBox.Show("girdim");                                   

                    Graphics gr = Graphics.FromImage(pictureBox1.Image);            //ÇİZİM İŞLEMİ İÇİN KAYNAK

                    gr.DrawString(dataGridView2.Rows[personel].Cells[1].Value.ToString(), font, pen, point, format);             //ÇİZİM İŞLEMİ


                    pictureBox1.Image.Save(save.FileName + (personel + 1) + ".jpg", ImageFormat.Jpeg);        //SAVE İŞLEMİ
                   

                    pictureBox1.Image = root;            //DİĞER DOSYA ÜZERİNDE İŞLEM YAPINCA ANA DOSYANIN KAYBOLMAMASI İÇİN


                    sc.Credentials = new NetworkCredential("yourmail@mail.com", "pwd");
                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress("receiver@mail.com", "Deneme");

                    mail.To.Add(dataGridView2.Rows[personel].Cells[2].Value.ToString());
                    mail.Subject = "Sertifikanız";
                    mail.IsBodyHtml = true;
                    mail.Body = "Sertifikanız Ektedir.Bu otomatik bir mesajdır.";
              
                    mail.Attachments.Add(new Attachment(save.FileName + (personel+1) + ".jpg"));

                    sc.Send(mail);                                                              //MAİL GÖNDER
                    
                    System.Threading.Thread.Sleep(5000);
                    MessageBox.Show("gönderildi");

                }
                personel++;
               
            }
            MessageBox.Show("İşlem Bitti");
            MySqlCommand done = new MySqlCommand("UPDATE `sertifikagonderilen` SET `isDone` = '1' WHERE `sertifikagonderilen`.`id` = '"+(which+1)+"';", baglan);
            done.ExecuteNonQuery();
            fill1();
        
        }



        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            izle();

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            color.ShowDialog();
            button4.BackColor = color.Color;
            izle();
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            izle();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                izle();
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                izle();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void picboxview_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            whichone();
            StringFormat format = new StringFormat();           //
            format.LineAlignment = StringAlignment.Center;      // CENTER POZİSYONUNDA ÇIKTI VERMEK İÇİN FORMAT
            format.Alignment = StringAlignment.Center;          //

            SaveFileDialog save = new SaveFileDialog();  //SAVE ALINACAK KONUMU BELİRLE

            save.Filter = "(*.jpg)|*.jpg";
            Font font = new Font(comboBox1.Text, Convert.ToInt32(numericUpDown1.Value));           //YAZININ YAZILACALIĞI FONT VE PUNTO

            Random password = new Random();             //RANDOM TANIMLA
            string final = "";
            for (int i = 0; i <= 8; i++)
            {
                int rasgele = password.Next(34, 126);   //RANDOM SEÇ
                if (rasgele == 47 || rasgele == 58 || rasgele == 92 || rasgele == 42 || rasgele == 63 || rasgele == 34 || rasgele == 60 || rasgele == 62 || rasgele == 124)
                {
                    rasgele = rasgele + 6;
                }
                char c;
                c = Convert.ToChar(rasgele);
                final = Convert.ToString(c) + final; ;

            }           //DOSYA ADI ŞİFRELEME
            save.ShowDialog();
            save.FileName = save.FileName + final;


            Brush pen = new LinearGradientBrush(new Point(1, 1), new Point(100, 100), button4.BackColor, button4.BackColor);  //BASILAN STRİNG İFADE İÇİNE DOLGU
            Point point = new Point(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox1.Text));                          //BASLAN STRİNG İFADE İÇİN PİVOT NOKTASI

            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.gmail.com";
            sc.EnableSsl = true;

            int personel = 0;

            //byte[] imageBytes = Convert.FromBase64String(dataGridView1.Rows[which].Cells[5].Value.ToString().Substring(22));

            //MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);         //BASE64 
            //Image image = Image.FromStream(ms, true);
            //Bitmap srt = new Bitmap(image);


            var data = (Byte[])(dataGridView1.Rows[which].Cells[5].Value);
            var stream = new MemoryStream(data);                                                        //BLOB


            pictureBox1.Image = Image.FromStream(stream);
            pictureBox2.Image = Image.FromStream(stream);
          

            for (int i = 1; i < dataGridView2.Rows.Count; i++)
            {
                Bitmap root = new Bitmap(pictureBox1.Image);                           //DİĞER DOSYA ÜZERİNDE İŞLEM YAPINCA ANA DOSYANIN KAYBOLMAMASI İÇİN


                if (dataGridView1.Rows[which].Cells[1].Value.ToString() == dataGridView2.Rows[personel].Cells[3].Value.ToString())
                {

                    //MessageBox.Show("girdim");

                    Graphics gr = Graphics.FromImage(pictureBox1.Image);            //ÇİZİM İŞLEMİ İÇİN KAYNAK

                    gr.DrawString(dataGridView2.Rows[personel].Cells[1].Value.ToString(), font, pen, point, format);             //ÇİZİM İŞLEMİ


                    pictureBox1.Image.Save(save.FileName + (personel + 1) + ".jpg", ImageFormat.Jpeg);        //SAVE İŞLEMİ


                    pictureBox1.Image = root;            //DİĞER DOSYA ÜZERİNDE İŞLEM YAPINCA ANA DOSYANIN KAYBOLMAMASI İÇİN


                    sc.Credentials = new NetworkCredential("denemee", "fewefees");
                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress("mail adress", "Deneme");

                    mail.To.Add(dataGridView2.Rows[personel].Cells[2].Value.ToString());
                    mail.Subject = "Sertifikanız";
                    mail.IsBodyHtml = true;
                    mail.Body = "Sertifikanız Ektedir.Bu otomatik bir mesajdır.";

                    mail.Attachments.Add(new Attachment(save.FileName + (personel + 1) + ".jpg"));

                     sc.Send(mail);
                    
                    System.Threading.Thread.Sleep(5000);
                    MessageBox.Show("Sertifika  Gönderildi ");
                }
                personel++;

            }
            MessageBox.Show("İşlem Bitti");
            MySqlCommand done = new MySqlCommand("UPDATE `sertifikagonderilen` SET `isDone` = '1' WHERE `sertifikagonderilen`.`id` = '" + (which + 1) + "';", baglan);
            done.ExecuteNonQuery();
            fill1();

        }
    }
}
