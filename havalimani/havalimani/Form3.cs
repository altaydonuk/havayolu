using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace havalimani
{
    public partial class Form3 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server= localHost; port=5432; Database= havalimani; user ID = postgres; password=123456");
        public Form3()
        {
            InitializeComponent();
            FromCity();
            WhereCity();
            SeatClass();
            Baggage();


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void FromCity()
        {
            comboBox2.Items.Clear();

            baglanti.Open();
            NpgsqlCommand sorguc = new NpgsqlCommand("Select fromcity_name from fromcity order by fromcity_country_id", baglanti);
            sorguc.ExecuteNonQuery();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorguc);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(dr["fromcity_name"].ToString());
            }
            baglanti.Close();
            sorguc.Parameters.Clear();

        }

        public void WhereCity()
        {
            comboBox3.Items.Clear();

            baglanti.Open();
            NpgsqlCommand sorguc = new NpgsqlCommand("Select wherecity_name from wherecity order by wherecity_country_id", baglanti);
            sorguc.ExecuteNonQuery();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorguc);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                comboBox3.Items.Add(dr["wherecity_name"].ToString());
            }
            baglanti.Close();
            sorguc.Parameters.Clear();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlCommand sorgu2 = new NpgsqlCommand("Select fromcity_id from fromcity where fromcity_name=@c1", baglanti);
            sorgu2.Parameters.AddWithValue("@c1", comboBox2.SelectedItem);
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorgu2);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                label12.Text = (dr["fromcity_id"].ToString());
            }
            baglanti.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlCommand sorgu2 = new NpgsqlCommand("Select wherecity_id from wherecity where wherecity_name=@c1", baglanti);
            sorgu2.Parameters.AddWithValue("@c1", comboBox3.SelectedItem);
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorgu2);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                label13.Text = (dr["wherecity_id"].ToString());
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand sorgu = new NpgsqlCommand("SELECT *FROM flight WHERE flight_fromcity_id=@p1 AND flight_wherecity_id=@p2 ", baglanti);
            sorgu.Parameters.AddWithValue("@p1", int.Parse(label12.Text));
            sorgu.Parameters.AddWithValue("@p2", int.Parse(label13.Text));
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorgu);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                label14.Text = (dr["flight_id"].ToString());
            }
            Searchflight();

            baglanti.Close();

        }

        public void Searchflight()
        {
            NpgsqlCommand komut1 = new NpgsqlCommand  ("select * from ucuslar where flightid =@p1", baglanti);
            komut1.Parameters.AddWithValue("@p1", int.Parse(label14.Text));
            komut1.ExecuteNonQuery();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut1);
            DataSet ds = new DataSet();
            da.Fill(ds);
            baglanti.Close();
            dataGridView1.DataSource = ds.Tables[0];
            
            this.dataGridView1.Columns["flightid"].Visible = false;

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }
        public void SeatClass()
        {
            comboBox4.Items.Clear();

            baglanti.Open();
            NpgsqlCommand sorgub = new NpgsqlCommand("Select seat_type from seat order by seat_id", baglanti);
            sorgub.ExecuteNonQuery();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorgub);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                comboBox4.Items.Add(dr["seat_type"].ToString());
            }
            baglanti.Close();
            sorgub.Parameters.Clear();

        }

        public void Baggage()
        {
            comboBox5.Items.Clear();

            baglanti.Open();
            NpgsqlCommand sorgub = new NpgsqlCommand("Select baggage_weight from baggage order by baggage_id", baglanti);
            sorgub.ExecuteNonQuery();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorgub);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                comboBox5.Items.Add(dr["baggage_weight"].ToString());
            }
            baglanti.Close();
            sorgub.Parameters.Clear();

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlCommand sorgu2 = new NpgsqlCommand("Select seat_id from seat where seat_type=@c1", baglanti);
            sorgu2.Parameters.AddWithValue("@c1", comboBox4.SelectedItem);
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorgu2);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                label17.Text = (dr["seat_id"].ToString());
            }
            baglanti.Close();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            NpgsqlCommand sorgu2 = new NpgsqlCommand("Select baggage_id from baggage where baggage_weight=@c1", baglanti);
            sorgu2.Parameters.AddWithValue("@c1", comboBox5.SelectedItem);
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorgu2);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                label18.Text = (dr["baggage_id"].ToString());
            }
            baglanti.Close();
        }

        private void Ticket()
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into ticket(ticket_seat_id, ticket_passenger_id,ticket_flight_id,ticket_baggage_id) values(@p1,@p2,@p3,@p4)", baglanti);

            komut1.Parameters.AddWithValue("@p1", int.Parse(label17.Text));
            komut1.Parameters.AddWithValue("@p2", int.Parse(labelpassenger.Text));
            komut1.Parameters.AddWithValue("@p3", int.Parse(flightidlabel.Text));
            komut1.Parameters.AddWithValue("@p4", int.Parse(label18.Text));


            komut1.ExecuteNonQuery();
            PassengerID();
            baglanti.Close();

            MessageBox.Show("Bilet Alındı.");
        }

        private void PassengerID()
        {
            NpgsqlCommand sorgu2 = new NpgsqlCommand("Select passenger_id from passenger where passenger_identity=@p1", baglanti);
            sorgu2.Parameters.AddWithValue("@p1", textBox5.Text);
            DataTable dt = new DataTable();
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sorgu2);
            adp.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                labelpassenger.Text = (dr["passenger_id"].ToString());
            }
            baglanti.Close();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into passenger(passenger_name, passenger_surname, passenger_identity, passenger_phonenumber,passenger_age, passenger_sex, passenger_mail, passenger_baggage_id) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", baglanti);

            komut1.Parameters.AddWithValue("@p1", textBox3.Text);
            komut1.Parameters.AddWithValue("@p2", textBox4.Text);
            komut1.Parameters.AddWithValue("@p3", textBox5.Text);
            komut1.Parameters.AddWithValue("@p4", textBox6.Text);
            komut1.Parameters.AddWithValue("@p5", int.Parse(textBox7.Text));
            komut1.Parameters.AddWithValue("@p6", comboBox1.Text);
            komut1.Parameters.AddWithValue("@p7", textBox9.Text);
            komut1.Parameters.AddWithValue("@p8", int.Parse(label18.Text));

            komut1.ExecuteNonQuery();
            PassengerID();
            baglanti.Close();
           

            MessageBox.Show("Yolcu eklendi.");


        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            flightidlabel.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Ticket();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

     
    }
}
