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
    public partial class OrderForm : UserControl
    {
        string RunId;
        string CityFrom;
        string CityTo;
        public OrderForm(string runId, string cityFrom, string cityTo)
        {
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;

            RunId = runId;
            CityFrom = cityFrom;
            CityTo = cityTo;

            String[] range = new String[10] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            List<string> nameEmployee = NpgSQLClass.Select(
             "SELECT name FROM employee ORDER BY name");
            comboBox1.Items.AddRange(nameEmployee.ToArray());
            comboBox2.Items.AddRange(range.ToArray());

            int x = 50;
            int y = 80;
            List<string> trainData = NpgSQLClass.Select("SELECT places FROM trains" +
                " WHERE id = (SELECT trainid FROM runs WHERE id = " + RunId + ")");
            for (int i = 1; i <= Convert.ToInt32(trainData[0]); i++)
            {
                Button btn = new Button();
                btn.Location = new Point(x, y);
                btn.Size = new Size(50, 30);
                btn.Text = i.ToString();
                string disabled = NpgSQLClass.Select("SELECT COUNT(*) FROM orders" +
                    " WHERE runid = " + RunId + " AND place = " + i.ToString())[0];
                btn.Enabled = (disabled == "0");
                btn.Click += new EventHandler(MakeOrder);

                Controls.Add(btn);

                x += 100;
                if (x + 100 >= Width)
                {
                    x = 50;
                    y += 50;
                }
            }
        }

        void MakeOrder(object sender, EventArgs e)
        {
            if (Program.Login == "")
            {
                MessageBox.Show("Вы не вошли в систему!");
                return;
            }

            Button btn = (Button)sender;
            String OrderId = NpgSQLClass.Select(
            "SELECT Max(id) FROM orders")[0];
            String loginId = NpgSQLClass.Select(
            "SELECT id FROM users where email = " + Program.Login.ToString())[0];
            String employeeId = NpgSQLClass.Select(
            "SELECT tabnum FROM employee where name = " + "'" + comboBox1.Text + "'")[0];
            NpgSQLClass.createOrder(int.Parse(OrderId) + 1, int.Parse(loginId), int.Parse(RunId),
                int.Parse(CityFrom), int.Parse(employeeId), int.Parse(CityTo), int.Parse(comboBox2.Text), int.Parse(btn.Text), "");

               // Insert("INSERT INTO orders(login, RunId, Place, CityFrom, CityTo)" +
             //   " VALUES('" + Program.Login + "', " + RunId + ", " + btn.Text + ", " + CityFrom + ", " + CityTo + ")");
            MessageBox.Show("Сделано");
            btn.Enabled = false;
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
