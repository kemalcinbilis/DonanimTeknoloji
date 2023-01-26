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
using System.Threading;



namespace DonanımTeknoloji
{
    public partial class Form2 : Form
    {
        Form1 form1 = new Form1();
        Form3 form3 = new Form3();
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=DonanımTeknoloji;Integrated Security=SSPI");
        SqlDataReader dr;
        DataSet ds;
        SqlDataAdapter da;
        SqlCommand komut;
        Thread th;
        public static string verial;
        public Form2()
        {
            InitializeComponent();
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            if (verial == "admin")
            {
                form3.Show();
                this.Close();
                th = new Thread(opennewform);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }


            lbl_Durum.Text = verial;
            ModulleriGetir();
            FirmalariGetir();


            cb_FirmaAdi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cb_FirmaAdi.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb_FirmaAdi.AutoCompleteSource = AutoCompleteSource.ListItems;

            cb_Modül.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cb_Modül.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb_Modül.AutoCompleteSource = AutoCompleteSource.ListItems;

            cb_Firma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cb_Firma.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb_Firma.AutoCompleteSource = AutoCompleteSource.ListItems;

            cb_FirmaSifre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            cb_FirmaSifre.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb_FirmaSifre.AutoCompleteSource = AutoCompleteSource.ListItems;

            tabloDoldur();
            tabloDoldur2();
        }
        private void opennewform(object obj)
        {
            Application.Run(new Form3());
        }

        private void temizle()
        {
            cb_FirmaAdi.Text = null;
            tb_GörüsülenKisi.Text = "";
            cb_Modül.SelectedItem = null;
            dtp_Tarih.Value = DateTime.Now;
            tb_Aciklama.Text = null;
            cb_İslemDurumu.SelectedItem = null;
            lbl_ID.Text = "0";
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
                cb_Firma.Items.Add(dr["FirmaAdi"]);
                cb_FirmaSifre.Items.Add(dr["FirmaAdi"]);          
            }

            baglanti.Close();
        }
        private void tabloDoldur()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select ID, FirmaAdi as Firma, ModulAdi as Modül, GorusulenKisi as Yetkili, Tarih, Aciklama as Açıklama, İslemDurumu as Durum from İslemEkrani  WHERE KullaniciAdi = '" + lbl_Durum.Text + "'", baglanti);
            DataSet ds = new DataSet();
            baglanti.Open();
            da.Fill(ds, "İslemEkrani");
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }
        private void tabloDoldur2()
        {
            SqlDataAdapter da = new SqlDataAdapter("select Firma.FirmaAdi as 'Firma Adı', SifreTable.AnyDesk as AnyDesk , SifreTable.AnyDeskSifre as 'AnyDesk Şifresi', SifreTable.Server, " +
                "SifreTable.ServerAdi as 'Server Adı', SifreTable.ServerSifre as 'Server Şifresi', SifreTable.SQLAdi as 'SQL Adı', SifreTable.SQLSifre as 'SQL Şifresi', SifreTable.SSO, " +
                "SifreTable.NetsisSifre as 'Netsis Şifresi' from SifreTable INNER JOIN Firma ON SifreTable.FirmaID = Firma.FirmaID ", baglanti);
            DataSet ds = new DataSet();
            baglanti.Open();
            da.Fill(ds, "SifreTable");
            dataGridView2.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void btn_Kaydet_Click(object sender, EventArgs e)
        {
            if (cb_FirmaAdi.SelectedItem != null && cb_Modül.SelectedItem != null)
            {
                komut = new SqlCommand("insert into İslemEkrani(KullaniciAdi ,FirmaAdi, ModulAdi, GorusulenKisi, Tarih, Aciklama, İslemDurumu) values (@KullaniciAdi ,@FirmaAdi, @ModulAdi, @GorusulenKisi, @Tarih, @Aciklama, @İslemDurumu)", baglanti);
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;

                komut.Parameters.AddWithValue("@KullaniciAdi", lbl_Durum.Text);
                komut.Parameters.AddWithValue("@FirmaAdi", cb_FirmaAdi.Text);
                komut.Parameters.AddWithValue("@ModulAdi", cb_Modül.Text);
                komut.Parameters.AddWithValue("@GorusulenKisi", tb_GörüsülenKisi.Text);
                komut.Parameters.AddWithValue("@Tarih", dtp_Tarih.Value);
                komut.Parameters.AddWithValue("@Aciklama", tb_Aciklama.Text);
                komut.Parameters.AddWithValue("@İslemDurumu", cb_İslemDurumu.Text);
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("Select * From İslemEkrani", baglanti);
                    DataSet ds = new DataSet();
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    temizle();
                    tabloDoldur();
                }
                catch
                {
                    MessageBox.Show("Bağlantı Hatası!");
                }
            }
            else
            {
                MessageBox.Show("Lütfen zorunlu alanları doldurunuz!");
            }

        }
        private void btn_YeniKayit_Click(object sender, EventArgs e)
        {
            temizle();
            tabloDoldur();
        }
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lbl_ID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            cb_FirmaAdi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            tb_GörüsülenKisi.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            cb_Modül.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            tb_Aciklama.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            cb_İslemDurumu.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }

        private void btn_Güncelle_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lbl_ID.Text) != 0)
            {
                //int i = Convert.ToInt32(lbl_ID.Text);
                SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=DonanımTeknoloji;Integrated Security=SSPI");
                komut = new SqlCommand("Update İslemEkrani set FirmaAdi = '" + cb_FirmaAdi.Text + "', ModulAdi = '" + cb_Modül.Text + "', GorusulenKisi = '" + tb_GörüsülenKisi.Text + "', Tarih = '" + dtp_Tarih.Value.ToString("yyyy-MM-dd") + "', Aciklama = '" + tb_Aciklama.Text + "', İslemDurumu = '" + cb_İslemDurumu.Text + "' WHERE ID = '" + lbl_ID.Text + "'", baglanti);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                tabloDoldur();
                temizle();
                MessageBox.Show("Güncelleme başarılı.");
            }
            else
            {
                MessageBox.Show("Güncellenecek görüşme kaydını tablodan seçiniz.");
            }

        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
                if (Convert.ToString(row.Cells[6].Value) == "TAMAMLANDI") 
                {
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                }
                else if (Convert.ToString(row.Cells[6].Value) == "TAMAMLANMADI")
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.YellowGreen;
                }
        }

        private void btn_Filtre_Click(object sender, EventArgs e)
        {
            if(cb_Firma.Text == "GENEL")
            {
                da = new SqlDataAdapter("Select ID, FirmaAdi as Firma, ModulAdi as Modül, GorusulenKisi as Yetkili, Tarih as Tarih, Aciklama as Açıklama, İslemDurumu as Durum from İslemEkrani WHERE KullaniciAdi = '"+lbl_Durum.Text+"' and Tarih between '"+dtp_BaslangicTarihi.Value.ToString("yyyy-MM-dd") + "' and '"+dtp_BitisTarihi.Value.ToString("yyyy-MM-dd") + "'", baglanti);
                ds = new DataSet();
                baglanti.Open();
                da.Fill(ds, "İslemEkrani");
                dataGridView1.DataSource = ds.Tables["İslemEkrani"];
                baglanti.Close();
            }
            else
            {
                da = new SqlDataAdapter("Select ID, FirmaAdi as Firma, ModulAdi as Modül, GorusulenKisi as Yetkili, Tarih as Tarih, Aciklama as Açıklama, İslemDurumu as Durum from İslemEkrani WHERE KullaniciAdi = '"+lbl_Durum.Text+"' and FirmaAdi = '"+cb_Firma.Text+"' and Tarih between '"+dtp_BaslangicTarihi.Value.ToString("yyyy-MM-dd") + "' and '"+dtp_BitisTarihi.Value.ToString("yyyy-MM-dd") + "'", baglanti);
                ds = new DataSet();
                baglanti.Open();
                da.Fill(ds, "İslemEkrani");
                dataGridView1.DataSource = ds.Tables["İslemEkrani"];
                baglanti.Close();
                //SqlDataAdapter da = new SqlDataAdapter("Select ID, FirmaAdi as Firma , ModulAdi as Modül , GorusulenKisi as Yetkili, Tarih as Tarih, Aciklama as Açıklama, İslemDurumu as Durum from İslemEkrani WHERE KullaniciAdi ='"+lbl_Durum+"' and FirmaAdi = '"+cb_Firma.Text+"' and Tarih between '"+dtp_BaslangicTarihi.Value.ToString("yyyy-MM-dd") + "' and '"+dtp_BitisTarihi.Value.ToString("yyyy-MM-dd") + "'", baglanti);
                //DataSet ds = new DataSet();
                //baglanti.Open();
                //da.Fill(ds, "İslemEkrani");
                //dataGridView1.DataSource = ds.Tables[0];
                //baglanti.Close();
            }

        }
        private void btn_Ara_Click_1(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select Firma.FirmaAdi as 'Firma Adı', SifreTable.AnyDesk as AnyDesk,SifreTable.AnyDeskSifre as 'AnyDesk Şifresi',SifreTable.Server,SifreTable.ServerAdi as 'Server Adı',SifreTable.ServerSifre as 'Server Şifresi',SifreTable.SQLAdi as 'SQL Adı', SifreTable.SQLSifre as'SQL Şifresi', SifreTable.SSO,SifreTable.NetsisSifre as 'Netsis Şifresi'from SifreTable INNER JOIN Firma ON SifreTable.FirmaID = Firma.FirmaID where Firma.FirmaAdi = '" + cb_FirmaSifre.Text + "'", baglanti);
            DataSet ds = new DataSet();
            baglanti.Open();
            da.Fill(ds, "SifreTable");
            dataGridView2.DataSource = ds.Tables[0];
            baglanti.Close();
            cb_FirmaSifre.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabloDoldur2();
        }
    }
}