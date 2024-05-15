using System;
using System.Windows.Forms;
using GDClient;
using GDServices;
using GestiuneDonatii.model;

namespace GestiuneDonatii
{
    public partial class login : Form
    {
        private UserController _userController;

        public login(UserController userController)
        {
            InitializeComponent(); // Inițializarea componentelor vizuale
            this._userController = userController;
        }

    
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            try
            {
                _userController.login(username, password);
                userForm userForm = new userForm(_userController);
                userForm.Text = "User form for " + username;
                userForm.Show();
            }
            catch (ServiceException ex)
            {
                MessageBox.Show("Eroare la autentificare: " + ex.Message);
                return;
            }
        }
    }
}