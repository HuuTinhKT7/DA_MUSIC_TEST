using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DA_MusicApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        string? username;
        string? email;


        public MainForm(string username,string email)
        {
            InitializeComponent();
            this.username = username;
            this.email = email;
            LoadData();
        }


        

        private void LoadData()
        {
            txtUsername.Text = username;   
            txtEmail.Text = email;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            
            signin signin = new signin();
            signin.Show();
            this.Close();
        }
    }
}
