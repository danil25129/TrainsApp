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
    public partial class MainForm : Form
    {
        public static Panel mainPanel;

        public static List<UserControl> pages = new List<UserControl>();
        public static int pagePos = -1;

        public MainForm()
        {
            InitializeComponent();
            mainPanel = panel1;

            TicketsList rf = new TicketsList();
            panel1.Controls.Clear();
            panel1.Controls.Add(rf);

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegisterForm rf = new RegisterForm();
            panel1.Controls.Clear();
            panel1.Controls.Add(rf);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminForm rf = new AdminForm();
            rf.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(rf);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Выберите пункт отправления");
                return;
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Выберите пункт назначения");
                return;
            }

            string dateFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string dateTo = dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd");

            string CityFrom = 
                NpgSQLClass.Select("SELECT id FROM cities WHERE name = '" + comboBox1.Text + "'")[0];
            string CityTo = 
                NpgSQLClass.Select("SELECT id FROM cities WHERE name = '" + comboBox2.Text + "'")[0];

            List<string> trains = NpgSQLClass.getActiveRoutes(int.Parse(CityFrom), int.Parse(CityTo), dateFrom, dateTo);
            /*"SELECT runs.trainid, trains.name, " +
                "'" + comboBox1.Text + "', " +
                "'" + comboBox2.Text + "', " +
                " (SELECT DATE dt + Interval timestart FROM routes WHERE city = " + CityFrom + " AND trainid = runs.trainid) timecity1," +
                " (SELECT DATE dt + Interval timestart FROM routes WHERE city = " + CityTo + "  AND trainid = runs.trainid) timecity2, " +
                " runs.id" +
                " FROM runs" +
                " JOIN trains ON trains.id = runs.trainid" +
                " HAVING timecity1<timecity2 AND" +
                " timecity1 BETWEEN '" + dateFrom + "' AND '" + dateTo + "'");*/

            Image img = Image.FromFile("../../Pictures/TrainBtn.png");
            int x = 10;
            int y = 10;
            TrainsPanel.Controls.Clear();
            for (int i = 0; i < trains.Count; i += 7)
            {
                Label lbl = new Label();
                lbl.Text =
                    Environment.NewLine +
                    Environment.NewLine +
                    Environment.NewLine +
                    "  " + "Поезд № " + trains[i + 1] + Environment.NewLine +
                    "  " + trains[i + 2] + " - " + trains[i + 3] + Environment.NewLine +
                    "  " + "Отправление: " + Environment.NewLine +
                    "  " + trains[i + 4] + Environment.NewLine +
                    "  " + "Прибытие: " + Environment.NewLine +
                    "  " + trains[i + 5];
                lbl.Location = new Point(x, y);
                lbl.Size = new Size(200, 160);
                lbl.Font = new Font("Arial", 11);
                lbl.Tag = trains[i + 6];
                lbl.Image = img;
                lbl.Click += new EventHandler(TrainClick);
                TrainsPanel.Controls.Add(lbl);



                x += 250;
                if (x + 200 > Width)
                {
                    x = 10;
                    y += 180;
                }
            }
        }

        private void TrainClick(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            string CityFrom = NpgSQLClass.Select("SELECT id FROM cities" +
                " WHERE name = '" + comboBox1.Text + "'")[0];
            string CityTo = NpgSQLClass.Select("SELECT id FROM cities" +
                " WHERE name = '" + comboBox2.Text + "'")[0];

            OrderForm of = new OrderForm(lbl.Tag.ToString(), CityFrom, CityTo);
            of.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> cities = NpgSQLClass.Select(
                "SELECT name FROM cities ORDER BY name");
            comboBox1.Items.AddRange(cities.ToArray());
            comboBox2.Items.AddRange(cities.ToArray());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string pass = textBox2.Text;

            string existUser = NpgSQLClass.Count(
                "SELECT COUNT(*) FROM users WHERE email ='" + email + "'");
            string existUserWithPass = 
                NpgSQLClass.Count("SELECT COUNT(*) FROM Users WHERE email = '" + email + 
                "' AND Password = '" + pass + "'");

            if (existUser == "0")
            {
                MessageBox.Show("Вы не зарегистрированы!");
                return;
            }
            else if (existUserWithPass == "0")
            {
                MessageBox.Show("Неверный пароль!");
                return;
            }
            else
            {
                textBox1.Visible = false;
                textBox2.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                button4.Visible = true;
                button3.Visible = true;
            }


            Program.Login = email;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pagePos--;
            panel1.Controls.Clear();
            panel1.Controls.Add(pages[pagePos]);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pagePos++;
            panel1.Controls.Clear();
            panel1.Controls.Add(pages[pagePos]);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Visible = (pagePos > 0);
            pictureBox2.Visible = (pagePos < pages.Count - 1);
        }
    }
}
