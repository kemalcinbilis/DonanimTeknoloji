using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DonanımTeknoloji
{
    public partial class Form3 : Form
    {
        public static string verial;
        SqlCommand komut = new SqlCommand();
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=DonanımTeknoloji;Integrated Security=SSPI");
        SqlDataReader dr;
        SqlDataAdapter da;
        DataSet ds;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            lbl_DurumAdminPanel.Text = verial;
            ModulleriGetir();
            FirmalariGetir();
            KullaniciGetir();
            tabloDoldur();
        }
        private void KullaniciGetir()
        {
            komut = new SqlCommand("SELECT KullaniciAdi FROM Kullanici");
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;

            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cb_Danisman.Items.Add(dr["KullaniciAdi"]);
            }

            baglanti.Close();
        }
        private void ModulleriGetir()
        {
            komut = new SqlCommand("SELECT * FROM Modul");
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;

            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cb_Modül.Items.Add(dr["ModulAdi"]);
            }

            baglanti.Close();
        }

        private void FirmalariGetir()
        {
            komut = new SqlCommand("SELECT *FROM Firma");
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;

            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cb_FirmaAdi.Items.Add(dr["FirmaAdi"]);
                cb_FirmaSifre.Items.Add(dr["FirmaAdi"]);
            }

            baglanti.Close();
        }
        private void tabloDoldur()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select ID, FirmaAdi as Firma, ModulAdi as Modül, GorusulenKisi as Yetkili, Tarih, Aciklama as Açıklama, İslemDurumu as Durum from İslemEkrani", baglanti);
            DataSet ds = new DataSet();
            baglanti.Open();
            da.Fill(ds, "İslemEkrani");
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void btn_Arama_Click(object sender, EventArgs e)
        {
            da = new SqlDataAdapter("Select ID, KullaniciAdi as 'Kullanıcı Adı', FirmaAdi as Firma, ModulAdi as Modül, GorusulenKisi as Yetkili, Tarih as Tarih, Aciklama as Açıklama, İslemDurumu as Durum from İslemEkrani WHERE KullaniciAdi = '" + cb_Danisman.Text + "' and FirmaAdi = '" + cb_FirmaSifre.Text + "' and Tarih between '" + dtp_BasTarih.Value.ToString("yyyy-MM-dd") + "' and '" + dtp_BitTarih.Value.ToString("yyyy-MM-dd") + "'", baglanti);
            ds = new DataSet();
            baglanti.Open();
            da.Fill(ds, "İslemEkrani");
            dataGridView1.DataSource = ds.Tables["İslemEkrani"];
            baglanti.Close();
        }
    }
}
