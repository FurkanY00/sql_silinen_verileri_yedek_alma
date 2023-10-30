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

namespace sql_silinen_verileri_yedek_alma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti=new SqlConnection(@"Data Source=DESKTOP-KPC6PV7\SQLEXPRESS;Initial Catalog=yedek_proje;Integrated Security=True");

        void sayac()
        {
            baglanti.Open();
            SqlCommand kmt = new SqlCommand("select *from  tblsayac", baglanti);
            SqlDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                lblkitapsayisi.Text = dr[0].ToString();
            }
            baglanti.Close();
        }

       void listele()
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("select *from tblkıtaplar", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            listele();
            sayac();

        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into tblkıtaplar  (ad,yazar,sayfa,yayınevi,tur)values(@p1,@p2,@p3,@p4,@p5)", baglanti);
            komut.Parameters.AddWithValue("@p1",txtad.Text);
            komut.Parameters.AddWithValue("@p2",txtyazar.Text);
            komut.Parameters.AddWithValue("@p3",txtsayfa.Text);
            komut.Parameters.AddWithValue("@p4",txtyayinevi.Text);
            komut.Parameters.AddWithValue("@p5",txttur.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
            sayac();
            MessageBox.Show("kitap sisteme başarı ile eklendi");
            
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from tblkıtaplar where ıd=@p1",baglanti);
            komut.Parameters.AddWithValue("@p1",txtıd.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("kitap sistemden silindi");
            listele();
            sayac();

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtıd.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString(); ;
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString(); ;
            txtyazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString(); ;
            txtsayfa.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString(); ;
            txtyayinevi.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString(); ;
            txttur.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString(); ;

        }
    }
}
