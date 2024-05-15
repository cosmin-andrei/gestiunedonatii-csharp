using System.Diagnostics;
using GDClient;
using GDNetworking.dto;
using GDServices;
using GestiuneDonatii.model;


namespace GestiuneDonatii;

public partial class userForm : Form
{
    
    
    
    private Voluntar _voluntar;
    private Dictionary<String, float> causes;
    
    private UserController ctrl;
    
    public userForm(UserController ctrl)
    {
        InitializeComponent();
        this.ctrl = ctrl;
        this.causes = ctrl.getDonatii();
        foreach (var cause in causes)
        {
            dataGridView1.Rows.Add(cause.Key, cause.Value);
        }
        ctrl.updateEvent += userUpdate;


    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count == 1)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            
            string nume = row.Cells["numeCauza"].Value.ToString();
            Cauza cauza = ctrl.findCauza(nume);

            donationForm donationForm = new donationForm(ctrl, cauza);
            donationForm.Show();
        }
        else
        {
            MessageBox.Show("Selectati o cauza!");
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        ctrl.logout();
        this.Close();
    }

    public void userUpdate(object sender, UserEventArgs e)
    {
        if (e.UserEventType == UserEvent.newDonatie)
        {
            Donatie donatie = e.Data as Donatie;
            causes[donatie.Cauza.Nume] += donatie.Suma;
        }

        dataGridView1.Rows.Clear();
        foreach (var cause in causes)
        {
            dataGridView1.Rows.Add(cause.Key, cause.Value);
        }
    }
}