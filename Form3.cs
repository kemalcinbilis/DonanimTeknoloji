using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            //ModulleriGetir();
            FirmalariGetir();
            KullaniciGetir();
            tabloDoldur();
            tabloDoldur2();

            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;

            cb_FirmaAdi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cb_FirmaAdi.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb_FirmaAdi.AutoCompleteSource = AutoCompleteSource.ListItems;

            cb_Danisman.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cb_Danisman.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb_Danisman.AutoCompleteSource = AutoCompleteSource.ListItems;

            cb_FirmaSifre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cb_FirmaSifre.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb_FirmaSifre.AutoCompleteSource = AutoCompleteSource.ListItems;
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
        //private void ModulleriGetir()
        //{
        //    komut = new SqlCommand("SELECT * FROM Modul");
        //    komut.Connection = baglanti;
        //    komut.CommandType = CommandType.Text;

        //    baglanti.Open();
        //    dr = komut.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        cb_Modül.Items.Add(dr["ModulAdi"]);
        //    }

        //    baglanti.Close();
        //}

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
            SqlDataAdapter da = new SqlDataAdapter("Select ID, KullaniciAdi as Danışman, FirmaAdi as Firma, ModulAdi as Modül, GorusulenKisi as Yetkili, Tarih, Aciklama as Açıklama, İslemDurumu as Durum from İslemEkrani", baglanti);
            DataSet ds = new DataSet();
            baglanti.Open();
            da.Fill(ds, "İslemEkrani");
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }
        private void tabloDoldur2()
        {
            SqlDataAdapter da = new SqlDataAdapter("select Firma.FirmaID as ID,Firma.FirmaAdi as 'Firma Adı', SifreTable.AnyDesk as AnyDesk , SifreTable.AnyDeskSifre as 'AnyDesk Şifresi', SifreTable.Server, " +
            "SifreTable.ServerAdi as 'Server Adı', SifreTable.ServerSifre as 'Server Şifresi', SifreTable.SQLAdi as 'SQL Adı', SifreTable.SQLSifre as 'SQL Şifresi', SifreTable.SSO, " +
            "SifreTable.NetsisSifre as 'Netsis Şifresi' from SifreTable INNER JOIN Firma ON SifreTable.FirmaID = Firma.FirmaID ", baglanti);
            DataSet ds = new DataSet();
            baglanti.Open();
            da.Fill(ds, "SifreTable");
            dataGridView2.DataSource = ds.Tables[0];
            baglanti.Close();
        }
        private void temizle()
        {
            cb_Danisman.Text = "";
            cb_FirmaAdi.Text = "";
            dtp_BasTarih.Value = DateTime.Now;
            dtp_BitTarih.Value = DateTime.Now;
            Kontrol.Checked = false;
            KontrolUser.Checked = false;
        }
        private void TextboxAktif()
        {
            tbAnyDesk.Enabled = true;
            tbAnyDeskSifre.Enabled = true;
            tbServer.Enabled = true;
            tbServerAdi.Enabled = true;
            tbServerSifresi.Enabled = true;
            tbSQLAdi.Enabled = true;
            tbSQLSifresi.Enabled = true;
            tbSSO.Enabled = true;
            tbNetsisSifresi.Enabled = true;
        }
        private void TextboxKapat()
        {
            tbAnyDesk.Enabled = false;
            tbAnyDeskSifre.Enabled = false;
            tbServer.Enabled = false;
            tbServerAdi.Enabled = false;
            tbServerSifresi.Enabled = false;
            tbSQLAdi.Enabled = false;
            tbSQLSifresi.Enabled = false;
            tbSSO.Enabled = false;
            tbNetsisSifresi.Enabled = false;
        }
        private void Doldur()
        {
            labelID.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            tbAnyDesk.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            tbAnyDeskSifre.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            tbServer.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            tbServerAdi.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
            tbServerSifresi.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
            tbSQLAdi.Text = dataGridView2.CurrentRow.Cells[7].Value.ToString();
            tbSQLSifresi.Text = dataGridView2.CurrentRow.Cells[8].Value.ToString();
            tbSSO.Text = dataGridView2.CurrentRow.Cells[9].Value.ToString();
            tbNetsisSifresi.Text = dataGridView2.CurrentRow.Cells[10].Value.ToString();
        }
        private void temizle2()
        {
            labelID.Text = "";
            tbAnyDesk.Clear();
            tbAnyDeskSifre.Clear();
            tbServer.Clear();
            tbServerAdi.Clear();
            tbServerSifresi.Clear();
            tbSQLAdi.Clear();
            tbSQLSifresi.Clear();
            tbSSO.Clear();
            tbNetsisSifresi.Clear();
        }
        private void YeniIslem()
        {
            temizle2();
            tabloDoldur2();
            cb_FirmaSifre.Text = "";
            cb_FirmaSifre.Enabled = true;
            TextboxKapat();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
        }
        private void btn_Arama_Click(object sender, EventArgs e)
        {
            if (Kontrol.Checked == true && KontrolUser.Checked == true)
            {
                da = new SqlDataAdapter("Select ID, KullaniciAdi as 'Kullanıcı Adı', FirmaAdi as Firma, ModulAdi as Modül, GorusulenKisi as Yetkili, Tarih as Tarih, Aciklama as Açıklama, İslemDurumu as Durum from İslemEkrani Where KullaniciAdi = '" + cb_Danisman.Text + "' and FirmaAdi = '" + cb_FirmaAdi.Text + "' and Tarih between '" + dtp_BasTarih.Value.ToString("yyyy-MM-dd") + "' and '" + dtp_BitTarih.Value.ToString("yyyy-MM-dd") + "'", baglanti);
                ds = new DataSet();
                baglanti.Open();
                da.Fill(ds, "İslemEkrani");
                dataGridView1.DataSource = ds.Tables["İslemEkrani"];
                baglanti.Close();
            }
            else if (Kontrol.Checked == true)
            {
                da = new SqlDataAdapter("Select ID, KullaniciAdi as 'Kullanıcı Adı', FirmaAdi as Firma, ModulAdi as Modül, GorusulenKisi as Yetkili, Tarih as Tarih, Aciklama as Açıklama, İslemDurumu as Durum from İslemEkrani Where FirmaAdi = '" + cb_FirmaAdi.Text + "' and Tarih between '" + dtp_BasTarih.Value.ToString("yyyy-MM-dd") + "' and '" + dtp_BitTarih.Value.ToString("yyyy-MM-dd") + "'", baglanti);
                ds = new DataSet();
                baglanti.Open();
                da.Fill(ds, "İslemEkrani");
                dataGridView1.DataSource = ds.Tables["İslemEkrani"];
                baglanti.Close();
            }
            else if (KontrolUser.Checked == true)
            {
                da = new SqlDataAdapter("Select ID, KullaniciAdi as 'Kullanıcı Adı', FirmaAdi as Firma, ModulAdi as Modül, GorusulenKisi as Yetkili, Tarih as Tarih, Aciklama as Açıklama, İslemDurumu as Durum from İslemEkrani Where KullaniciAdi = '" + cb_Danisman.Text + "' and Tarih between '" + dtp_BasTarih.Value.ToString("yyyy-MM-dd") + "' and '" + dtp_BitTarih.Value.ToString("yyyy-MM-dd") + "'", baglanti);
                ds = new DataSet();
                baglanti.Open();
                da.Fill(ds, "İslemEkrani");
                dataGridView1.DataSource = ds.Tables["İslemEkrani"];
                baglanti.Close();
            }
        }

        private void btn_Sifirla_Click(object sender, EventArgs e)
        {
            tabloDoldur();
        }

        private void Kontrol_CheckedChanged(object sender, EventArgs e)
        {
            if (Kontrol.Checked)
            {
                cb_FirmaAdi.Enabled = true;
            }
            else
            {
                cb_FirmaAdi.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void KontrolUser_CheckedChanged(object sender, EventArgs e)
        {
            if (KontrolUser.Checked)
            {
                cb_Danisman.Enabled = true;
            }
            else
            {
                cb_Danisman.Enabled = false;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
                if (Convert.ToString(row.Cells[7].Value) == "TAMAMLANDI")
                {
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                }
                else if (Convert.ToString(row.Cells[7].Value) == "TAMAMLANMADI")
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.YellowGreen;
                }
        }
        //private void btn_Ara_Click(object sender, EventArgs e)
        //{
        //    SqlDataAdapter da = new SqlDataAdapter("select Firma.FirmaID as ID, Firma.FirmaAdi as 'Firma Adı', SifreTable.AnyDesk as AnyDesk,SifreTable.AnyDeskSifre as 'AnyDesk Şifresi',SifreTable.Server,SifreTable.ServerAdi as 'Server Adı',SifreTable.ServerSifre as 'Server Şifresi',SifreTable.SQLAdi as 'SQL Adı', SifreTable.SQLSifre as 'SQL Şifresi', SifreTable.SSO,SifreTable.NetsisSifre as 'Netsis Şifresi'from SifreTable INNER JOIN Firma ON SifreTable.FirmaID = Firma.FirmaID where Firma.FirmaAdi = '" + cb_FirmaSifre.Text + "'", baglanti);
        //    DataSet ds = new DataSet();
        //    baglanti.Open();
        //    da.Fill(ds, "SifreTable");
        //    dataGridView2.DataSource = ds.Tables[0];
        //    baglanti.Close();
        //    temizle2();
        //}
        private void btn_Uygula_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                komut = new SqlCommand("INSERT INTO SifreTable(FirmaID, AnyDesk, AnyDeskSifre, Server, ServerAdi, ServerSifre, SQLAdi, SQLSifre, SSO, NetsisSifre) VALUES ('@FirmaID','@AnyDesk','@AnyDeskSifre','@Server','@ServerAdi','@ServerSifre','@SQLAdi','@SQLSifre','@SSO','@NetsisSifre')", baglanti);
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;

                komut.Parameters.AddWithValue("@FirmaID", Convert.ToInt32(labelID.Text));
                komut.Parameters.AddWithValue("@AnyDesk", tbAnyDesk.Text);
                komut.Parameters.AddWithValue("@AnyDeskSifre", tbAnyDeskSifre.Text);
                komut.Parameters.AddWithValue("@Server", tbServer.Text);
                komut.Parameters.AddWithValue("@ServerAdi", tbServerAdi.Text);
                komut.Parameters.AddWithValue("@ServerSifre", tbServerSifresi.Text);
                komut.Parameters.AddWithValue("@SQLAdi", tbSQLAdi.Text);
                komut.Parameters.AddWithValue("@SQLSifre", tbSQLSifresi.Text);
                komut.Parameters.AddWithValue("@SSO", tbSSO.Text);
                komut.Parameters.AddWithValue("@NetsisSifre", tbNetsisSifresi.Text);
                try
                {
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Kayıt başarılı!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(labelID.Text);
                    MessageBox.Show("Bağlantı Hatası: " + ex.Message);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabloDoldur2();
        }
        private void cb_FirmaSifre_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select Firma.FirmaID as ID, Firma.FirmaAdi as 'Firma Adı', SifreTable.AnyDesk as AnyDesk,SifreTable.AnyDeskSifre as 'AnyDesk Şifresi',SifreTable.Server,SifreTable.ServerAdi as 'Server Adı',SifreTable.ServerSifre as 'Server Şifresi',SifreTable.SQLAdi as 'SQL Adı', SifreTable.SQLSifre as 'SQL Şifresi', SifreTable.SSO,SifreTable.NetsisSifre as 'Netsis Şifresi'from SifreTable INNER JOIN Firma ON SifreTable.FirmaID = Firma.FirmaID where Firma.FirmaAdi = '" + cb_FirmaSifre.Text + "'", baglanti);
            DataSet ds = new DataSet();
            baglanti.Open();
            da.Fill(ds, "SifreTable");
            dataGridView2.DataSource = ds.Tables[0];
            baglanti.Close();

            temizle2();
            if (dataGridView2.FirstDisplayedCell.Value == null)
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
            }
            else
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;
            }
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            temizle2();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            temizle2();
            baglanti.Open();
            komut = new SqlCommand("select FirmaID From Firma Where FirmaAdi = '" + cb_FirmaSifre.Text + "'");
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;
            SqlDataReader reader = komut.ExecuteReader();

            if (reader.Read())
            {
                string firmaId = reader["FirmaID"].ToString();
                labelID.Text = firmaId;
            }
            reader.Close();
            baglanti.Close();
            TextboxAktif();
            cb_FirmaSifre.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            temizle2();
            TextboxKapat();
            Doldur();
            cb_FirmaSifre.Enabled = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            temizle2();
            TextboxAktif();
            Doldur();
            cb_FirmaSifre.Enabled = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            temizle2();
            tabloDoldur2();
            cb_FirmaSifre.Text = "";
            cb_FirmaSifre.Enabled = true;
            TextboxKapat();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
        }
    }
}