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
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
            this.AcceptButton = btnRegister;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsernameRgstr.Text.Trim();
            string password = txtPasswordRgstr.Text;
            string confirmPassword = txtUsernameRgstr.Text;
            string role = cbRole.SelectedItem.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = $"INSERT INTO Users (Username, Password, Role) VALUES ('{username}', '{password}', '{role}')";

            try
            {
                if (DatabaseHandler.ExecuteNonQuery(query))
                {
                    MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    frmLogin loginForm = new frmLogin();
                    loginForm.Show();
                }
                else
                {
                    MessageBox.Show("Registration failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
