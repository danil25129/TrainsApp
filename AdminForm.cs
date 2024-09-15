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
    public partial class AdminForm : UserControl
    {
        public AdminForm()
        {
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminTrainsForm atf = new AdminTrainsForm();
            MainForm.mainPanel.Controls.Clear();
            MainForm.mainPanel.Controls.Add(atf);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminBookingForm atf = new AdminBookingForm();
            MainForm.mainPanel.Controls.Clear();
            MainForm.mainPanel.Controls.Add(atf);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminRunsForm atf = new AdminRunsForm();
            MainForm.mainPanel.Controls.Clear();
            MainForm.mainPanel.Controls.Add(atf);
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminUsersForm atf = new AdminUsersForm();
            MainForm.mainPanel.Controls.Clear();
            MainForm.mainPanel.Controls.Add(atf);
        }
    }
}
