using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Final.Controller;

namespace Final
{
    public partial class Login : Form
    {
        AccountDAO aDAO = new AccountDAO();
        public Login()
        {
            InitializeComponent();
        }


        private void btlogin_Click(object sender, EventArgs e)
        {
            string user = tbuser.Text;
            string pass = tbpass.Text;
            if (aDAO.acc(user, pass) != null)
            {
                Setting.acc = aDAO.acc(user, pass);
                Setting.home = new Home();
                this.Hide();
                Setting.home.ShowDialog();
                this.Show();
            }
            else MessageBox.Show("login that bai");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = tbuser.Text;
            string pass = tbpass.Text;
            aDAO.ChangeInfo(user, "dep trai","áđâsđâsđấ");
        }
    }
}
