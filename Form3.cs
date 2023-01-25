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
    }
}
