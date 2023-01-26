using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Threading;

namespace DonanımTeknoloji
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=DonanımTeknoloji;Integrated Security=SSPI");
        Thread th;
        public static string gonder;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            gonder = tb_KullaniciAdi.Text;
            tb_KullaniciAdi.CharacterCasing = CharacterCasing.Lower;
        }

        private void btn_Giris_Click(object sender, EventArgs e)
        {
            if (tb_KullaniciAdi.Text == "" || tb_Sifre.Text == "")
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifrenizi giriniz.");
                return;
            }
            try
            {
                SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=DonanımTeknoloji;Integrated Security=SSPI");
                SqlCommand komut = new SqlCommand("Select * from Kullanici where KullaniciAdi=@KullaniciAdi and Sifre=@Sifre", baglanti);
                komut.Parameters.AddWithValue("@KullaniciAdi", tb_KullaniciAdi.Text);
                komut.Parameters.AddWithValue("@Sifre", tb_Sifre.Text);
                baglanti.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(komut);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                baglanti.Close();
                int count = ds.Tables[0].Rows.Count;

                if (count == 1)
                {
                    lbl_Gonder.Text = tb_KullaniciAdi.Text;
                    gonder = lbl_Gonder.Text;
                    Form2.verial = gonder;
                    Form3.verial = gonder;
                    this.Close();
                    th = new Thread(opennewform);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
                else
                {
                    MessageBox.Show("Login Failed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void opennewform(object obj)
        {
            Application.Run(new Form2());
        }

        private void btn_GirisKeyDown(object sender, KeyEventArgs e)
        {
        
        }
    }
}
