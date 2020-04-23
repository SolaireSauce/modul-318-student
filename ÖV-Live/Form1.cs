using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ÖV_Live
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dateTimePicker3.ShowUpDown = true;
            dateTimePicker4.ShowUpDown = true;
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.CustomFormat = "HH:mm";
            dateTimePicker4.Format = DateTimePickerFormat.Custom;
            dateTimePicker4.CustomFormat = "HH:mm";

            BackColor = Color.FromArgb(245, 245, 245);
        }

        SwissTransport.Transport transport = new SwissTransport.Transport();
        SwissTransport.Connections connections = new SwissTransport.Connections();
        SwissTransport.StationBoardRoot stationboard = new SwissTransport.StationBoardRoot();
        SwissTransport.Stations stations = new SwissTransport.Stations();
        SwissTransport.Station departure = new SwissTransport.Station();
        SwissTransport.Station destination = new SwissTransport.Station();

        //Funktionen

        void UpdateConnection()
        {
            listBox4.Items.Clear();

            connections = null;
            if(departure.Name != null && destination.Name != null)
            connections = transport.GetConnections(departure.Name, destination.Name, dateTimePicker1.Value.Date, dateTimePicker3.Value.TimeOfDay, false);
            
            if (connections == null)
            {
                MessageBox.Show("Keine Verbindung gefunden.");
                return;
            }
            foreach (SwissTransport.Connection connection in connections.ConnectionList) 
            {
                listBox4.Items.Add(connection.From.Departure + "\t" + connection.From.Station.Name + "\t" + connection.To.Arrival + "\t" + connection.To.Station.Name + "\t" + connection.Duration);
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            stations = transport.GetStations(textBox2.Text);
            listBox2.DataSource = stations.StationList;
            listBox2.DisplayMember = "Name";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            stations = transport.GetStations(textBox1.Text);
            listBox1.DataSource = stations.StationList;
            listBox1.DisplayMember = "Name";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(51, 51, 51);
            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;
            label6.ForeColor = Color.White;
            groupBox1.ForeColor = Color.White;
            groupBox2.ForeColor = Color.White;
            button3.ForeColor = Color.Black;
            button3.BackColor = Color.White;
            button4.ForeColor = Color.Black;
            button4.BackColor = Color.White;
            button5.ForeColor = Color.Black;
            button5.BackColor = Color.White;
            button6.ForeColor = Color.Black;
            button6.BackColor = Color.White;
            button7.ForeColor = Color.Black;
            button7.BackColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(245, 245, 245);
            label1.ForeColor = Color.Black;
            label2.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;
            groupBox1.ForeColor = Color.Black;
            groupBox2.ForeColor = Color.Black;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            departure = (SwissTransport.Station)listBox1.SelectedItem;
            textBox1.Text = departure.Name;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            destination = (SwissTransport.Station)listBox2.SelectedItem;
            textBox2.Text = destination.Name;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            UpdateConnection();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_ControlRemoved(object sender, ControlEventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            stations = transport.GetStations(textBox4.Text);
            listBox3.DataSource = stations.StationList;
            listBox3.DisplayMember = "Name";
        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
