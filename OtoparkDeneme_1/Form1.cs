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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        SqlConnection connect = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=OTOPARK_1;Integrated Security=True");
        //int Id = 0;

        private void btnKayit_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Id = Convert.ToInt32(dgvListe.CurrentRow.Cells[0].Value.ToString());
            f3.dateTimePicker1.Text=dgvListe.CurrentRow.Cells[1].Value.ToString();
          //f3.txtDateUp.Text = dgvListe.CurrentRow.Cells[1].Value.ToString();
            f3.txtPlakaUp.Text = dgvListe.CurrentRow.Cells[2].Value.ToString();
            f3.txtAciklamaUp.Text = dgvListe.CurrentRow.Cells[3].Value.ToString();

            f3.ShowDialog();
        }

        public void gridDoldur()
        {
            dgvListe.ReadOnly = true;
            SqlDataAdapter adap = new SqlDataAdapter("SELECT * FROM ARACHAR", connect);
            DataSet ds = new DataSet();
            adap.Fill(ds,"ARACHAR");
            dgvListe.DataSource = ds.Tables[0];
            

        }

        DataTable TarihArama()
        {
            string sql = "SELECT * FROM ARACHAR WHERE TARIHGIRIS >= '" + dt1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' AND TARIHGIRIS <='" + dt2.Value.ToString("yyyy-MM-dd HH:mm:ss") +"'";
            DataTable dtb = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sql, connect);
            sda.SelectCommand.Parameters.Add("", SqlDbType.Date).Value = dt1.Value;
            sda.SelectCommand.Parameters.Add("", SqlDbType.Date).Value = dt2.Value;

            sda.Fill(dtb);
            dgvListe.DataSource = dtb;
            return dtb;
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            try
            {
                gridDoldur();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "KOD BUM BUM!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            TarihArama();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult cevap;
                cevap = MessageBox.Show("Kaydı silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    if (connect.State == ConnectionState.Closed)
                    {
                        connect.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM ARACHAR WHERE KNO =@KNO", connect);
                        //SqlCommand cmd = new SqlCommand("Silme", connect);
                        //cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@KNO", Convert.ToInt32(dgvListe.CurrentRow.Cells[0].Value.ToString()));
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Kayıt başarıyla silindi!");
                    gridDoldur();
                    connect.Close();
                }  
            }
            catch (Exception)
            {
                MessageBox.Show("Herhangi bir kayıt seçmedin!");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) //SEARCH
        {
            try
            {
                (dgvListe.DataSource as DataTable).DefaultView.RowFilter = string.Format("PLAKA like '%{0}%'", textBox1.Text.Trim());  
            }
            catch (Exception)
            {
                MessageBox.Show("asdfas");
            }
            
        }


    }
}
