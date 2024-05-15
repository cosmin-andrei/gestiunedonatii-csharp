using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GDClient;
using GDServices; // Verifică dacă acesta este namespace-ul corect pentru IServices
using GestiuneDonatii.model;

namespace GestiuneDonatii
{
    public partial class donationForm : Form
    {
        private UserController ctrl;
        private readonly Cauza _cauza;
        private readonly List<Donator> _donators;

        public donationForm(UserController ctrl, Cauza cauza)
        {
            this.ctrl = ctrl;
            _cauza = cauza;
            InitializeComponent();
            _donators = ctrl.getDonatori();
            InitializeDataGridView();
            InitModel();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                string nume = row.Cells["Nume"].Value.ToString();
                string telefon = row.Cells["Telefon"].Value.ToString();
                string adresa = row.Cells["Adresa"].Value.ToString();
                textBox1.Text = nume;
                textBox2.Text = telefon;
                textBox3.Text = adresa;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox5.Text.ToLower();

            dataGridView1.Rows.Clear();
            foreach (var donator in _donators)
            {
                if (donator.Nume.ToLower().Contains(searchText))
                {
                    dataGridView1.Rows.Add(donator.Nume, donator.Telefon, donator.Adresa);
                }
            }
        }

        private void InitModel()
        {
            // dataGridView1.Rows.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _donators;
            // foreach (var donator in _donators)
            // {
            //     dataGridView1.Rows.Add(donator.Nume, donator.Telefon, donator.Adresa);
            // }
        }

        private void InitializeDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;

            // Adaugă coloanele corespunzătoare în DataGridView
            DataGridViewTextBoxColumn colNume = new DataGridViewTextBoxColumn();
            colNume.HeaderText = "Nume";
            colNume.DataPropertyName = "Nume";
            dataGridView1.Columns.Add(colNume);

            DataGridViewTextBoxColumn colAdresa = new DataGridViewTextBoxColumn();
            colAdresa.HeaderText = "Adresa";
            colAdresa.DataPropertyName = "Adresa";
            dataGridView1.Columns.Add(colAdresa);

            DataGridViewTextBoxColumn colTelefon = new DataGridViewTextBoxColumn();
            colTelefon.HeaderText = "Telefon";
            colTelefon.DataPropertyName = "Telefon";
            dataGridView1.Columns.Add(colTelefon);

            dataGridView1.DataSource = _donators;
        }

        private void handleRowSelection(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                Donator donator = (Donator)selectedRow.DataBoundItem;

                if (donator != null)
                {
                    textBox1.Text = donator.Nume;
                    textBox2.Text = donator.Telefon.ToString();
                    textBox3.Text = donator.Adresa;
                }
                else
                {
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) ||
               string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Completati toate campurile!");
            }
            else
            {
                try
                {
                    string nume = textBox1.Text;
                    long telefon = long.Parse(textBox2.Text);
                    string adresa = textBox3.Text;
                    float suma = float.Parse(textBox4.Text);
                    ctrl.addDonatie(nume, telefon, adresa, _cauza, suma);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }

}
