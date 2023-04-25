using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtoparkDeneme_1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        int Id;
        Form1 f1 = new Form1();
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=OTOPARK_1;Integrated Security=True");
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();
                    SqlCommand cmd = new SqlCommand("EkleveyaGuncelle", connect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if(txtPlaka.Text =="" || txtAciklama.Text == "")
                    {
                        MessageBox.Show("Lütfen boş alanı/alanları doldurun!");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@mode", "Ekle");
                        cmd.Parameters.AddWithValue("@KNO", 0);
                        cmd.Parameters.AddWithValue("@TARIHGIRIS", dateTimePicker1.Value.ToString("yyyy-MM-dd "));
                        //dateTimePicker kullanırken, tarih değerini yukarıdaki gibi almamız gereklidir.
                        //Aksi halde bir başka programda çalıştırdığımızda kod patlar.
                        //cmd.Parameters.AddWithValue("@TARIHGIRIS", DateTime.Parse(txtDate.Text));
                        cmd.Parameters.AddWithValue("@PLAKA", txtPlaka.Text.Trim());
                        cmd.Parameters.AddWithValue("@ACIKLAMA", txtAciklama.Text.Trim());


                    }


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Araç kayıt başarılı bir şekilde yapılmıştır!");

                    Temizle();
                    this.Close();
                    f1.gridDoldur();
                }


            }
            catch (Exception )
            {
                MessageBox.Show("Kayıt düzgün yapılmadı mal çocuk!");
            }
            finally
            {
                connect.Close();
            }
        }

        private void Temizle() //Veriyi kaydettikten sonra sayfayı temizler.
        {
            //txtDate.Text = "";
            txtPlaka.Text = "";
            txtAciklama.Text = "";
            Id = 0;
        }

    }
}
