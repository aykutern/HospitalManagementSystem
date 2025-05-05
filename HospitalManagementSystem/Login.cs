using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagementSystem
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                string query = $"SELECT * FROM Users WHERE Username = '{username}' and Password = '{password}'";
                DataTable dt = DatabaseHandler.ExecuteSelect(query);
                if (dt.Rows.Count > 0)
                {
                    // User exists, proceed to the next form
                    String role = dt.Rows[0]["Role"].ToString();
                    this.Hide();
                    frmDashboard dashboard = new frmDashboard(role);
                    dashboard.Show();
                }
                else
                {
                    // User does not exist, show error message
                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmRegister registerForm = new frmRegister();
            registerForm.Show();
        }

        private void chcShow_CheckedChanged(object sender, EventArgs e)
        {
            if (chcShow.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
