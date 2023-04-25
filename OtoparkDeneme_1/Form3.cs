using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OtoparkDeneme_1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        Form1 f1 = new Form1();
        public int Id = 0;
        SqlConnection conn = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=OTOPARK_1;Integrated Security=True");
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE ARACHAR SET " +
                        "TARIHGIRIS = @TARIHGIRIS," +
                        "PLAKA = @PLAKA," +
                        "ACIKLAMA = @ACIKLAMA " +
                        "WHERE KNO = @KNO", conn);
                    //SqlCommand cmd = new SqlCommand("EkleveyaGuncelle", conn);
                    //cmd.CommandType = CommandType.StoredProcedure;

                    if (txtPlakaUp.Text == "" || txtAciklamaUp.Text == "")
                    {
                        MessageBox.Show("Lütfen boş alanı/alanları doldurun!");
                    }
                    else
                    {
                        //cmd.Parameters.AddWithValue("@mode", "Guncelle");

                        cmd.Parameters.AddWithValue("@KNO", Id);
                        cmd.Parameters.AddWithValue("@TARIHGIRIS", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                      //cmd.Parameters.AddWithValue("@TARIHGIRIS", DateTime.Parse(txtDateUp.Text));
                        cmd.Parameters.AddWithValue("@PLAKA", txtPlakaUp.Text.Trim());
                        cmd.Parameters.AddWithValue("@ACIKLAMA", txtAciklamaUp.Text.Trim());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Aferin kaydı güncelledin");
                        
                    }
                    Temizle();
                    this.Close();
                    f1.gridDoldur();
                    conn.Close();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Olum naptın lan sen? Kaydı düzgün yapamıyorsun bir de üstüne gitmiş burada da hatalı şeyler yazmış. SALAK!");
            }
        }

        private void Temizle() //Veriyi kaydettikten sonra sayfayı temizler.
        {
          //txtDateUp.Text = "";
            txtPlakaUp.Text = "";
            txtAciklamaUp.Text = "";
            Id = 0;
        }
    }
}
