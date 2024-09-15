using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tickets
{
    public partial class RegisterForm : UserControl
    {
        public RegisterForm()
        {
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string registeredUsers = NpgSQLClass.Count(
                "SELECT COUNT(*) FROM Users WHERE Email = '" + emailTB.Text + "'");
            string registeredEmploy = NpgSQLClass.Count(
                "SELECT COUNT(*) FROM Employee  WHERE Email = '" + emailTB.Text + "'");

            if (registeredUsers != "0")
            {
                MessageBox.Show("Вы уже зарегистрированы!");
                return;
            }
            
            if (registeredEmploy != "0" && AdminCheck.Checked) {
                MessageBox.Show("Вы уже зарегистрированы!");
                return;
            }

            if (AdminCheck.Checked)
            {
                NpgSQLClass.createEmployee(int.Parse(tabTB.Text), nameTB.Text, famTB.Text, passTB.Text, emailTB.Text);
                MessageBox.Show("Теперь можно входить в систему");
            }
            else
            {
                NpgSQLClass.createUser(nameTB.Text, famTB.Text, passTB.Text, emailTB.Text);
                MessageBox.Show("Теперь можно входить в систему");
            }
            

            //Close();
        }

        private void AdminCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (AdminCheck.Checked) {
                label5.Visible = true;
                tabTB.Visible = true;
            }

            if (!AdminCheck.Checked)
            {
                label5.Visible = false;
                tabTB.Visible = false;
            }
        }
    }
}
