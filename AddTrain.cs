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
    public partial class AddTrain : UserControl
    {
        string trainId;
        int comboY = 0;
        List<string> cities = new List<string>();
        public AddTrain(string TrainId)
        {
            trainId = TrainId;
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;

            comboY = comboBox1.Location.Y + 50;
            cities = NpgSQLClass.Select("SELECT name FROM cities ORDER BY name");
            comboBox1.Items.AddRange(cities.ToArray());
            toComboBox.Items.AddRange(cities.ToArray());
            fromComboBox.Items.AddRange(cities.ToArray());

            if (trainId != "")
            {
                Text = "Изменить маршрут поезда";
                button3_Click(button3, null);

                List<string> trainData = 
                    NpgSQLClass.Select("SELECT id, name, cityfrom, cityto, days, places FROM trains WHERE id = " + trainId);

                if (trainData.Count > 0)
                {
                    nameTextBox.Text = trainData[1];
                    placeTextBox.Text = trainData[5];
                    string[] days = trainData[4].Split(new string[] { ", " }, StringSplitOptions.None);
                    for (int i = 0; i < days.Length; i++)
                    {
                        int j = Convert.ToInt32(days[i]);
                        checkedListBox1.SetItemChecked(j - 1, true);                            
                    }
                    fromComboBox.Text = NpgSQLClass.findCityById(int.Parse(trainData[2]));
                    toComboBox.Text = NpgSQLClass.findCityById(int.Parse(trainData[3]));
                }

                List<string> citiesData =
                    NpgSQLClass.Select("SELECT cities.name, timestart FROM routes JOIN cities ON routes.city = cities.id WHERE trainid = " + trainId);

                for (int i = 0; i < citiesData.Count; i += 2)
                {
                    ComboBox cb1 = new ComboBox();
                    cb1.Items.AddRange(cities.ToArray());
                    cb1.Text = citiesData[i];
                    cb1.Location = new Point(comboBox1.Location.X, comboY);
                    cb1.Size = new Size(255, 37);

                    Button btn = new Button();
                    btn.Location = new Point(button2.Location.X, comboY);
                    btn.Size = new Size(75, 34);
                    btn.Text = "+";
                    btn.Click += new EventHandler(button2_Click);

                    Button btn3 = new Button();
                    btn3.Location = new Point(button3.Location.X, comboY);
                    btn3.Size = new Size(75, 34);
                    btn3.Text = "-";
                    btn3.Click += new EventHandler(button3_Click);

                    DateTimePicker dp1 = new DateTimePicker();
                    dp1.Format = DateTimePickerFormat.Time;
                    dp1.Location = new Point(dateTimePicker1.Location.X, comboY);
                    dp1.Size = new Size(131, 34);

                    Controls.Add(cb1);
                    Controls.Add(btn);
                    Controls.Add(dp1);
                    Controls.Add(btn3);

                    comboY += 50;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ComboBox cb1 = new ComboBox();
            cb1.Items.AddRange(cities.ToArray());
            cb1.Location = new Point(comboBox1.Location.X, comboY);
            cb1.Size = new Size(255, 37);

            Button btn = new Button();
            btn.Location = new Point(button2.Location.X, comboY);
            btn.Size = new Size(75, 34);
            btn.Text = "+";
            btn.Click += new EventHandler(button2_Click);

            Button btn3 = new Button();
            btn3.Location = new Point(button3.Location.X, comboY);
            btn3.Size = new Size(75, 34);
            btn3.Text = "-";
            btn3.Click += new EventHandler(button3_Click);

            DateTimePicker dp1 = new DateTimePicker();
            dp1.Format = DateTimePickerFormat.Time;
            dp1.Location = new Point(dateTimePicker1.Location.X, comboY);
            dp1.Size = new Size(131, 34);

            Controls.Add(cb1);
            Controls.Add(btn);
            Controls.Add(btn3);
            Controls.Add(dp1);

            comboY += 50;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> days = new List<string>();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                    days.Add((i + 1).ToString());
            }
            string d = String.Join(", ", days);

            string CityFrom = NpgSQLClass.Select("SELECT id FROM cities" +
                " WHERE name = '" + fromComboBox.Text + "'")[0];
            string CityTo = NpgSQLClass.Select("SELECT id FROM cities" +
                " WHERE name  = '" + toComboBox.Text + "'")[0];

            if (trainId == "")
            {
                trainId = NpgSQLClass.Select("SELECT MAX(id) FROM trains")[0];
                NpgSQLClass.createTrain(int.Parse(trainId) + 1, int.Parse(CityFrom), int.Parse(CityTo),
                    nameTextBox.Text, d, int.Parse(placeTextBox.Text));
                
            }
            else
            {
                NpgSQLClass.updateTrain(int.Parse(trainId), int.Parse(CityFrom), int.Parse(CityTo),
                    nameTextBox.Text, d, int.Parse(placeTextBox.Text));

                NpgSQLClass.deleteRoute(int.Parse(trainId));
            }


            //Город
            foreach (Control ctrl in Controls)
            {
                if (ctrl is ComboBox)
                {
                    string city = ctrl.Text;

                    //Время, когда поезд там
                    foreach (Control ctrl2 in Controls)
                    {
                        if (ctrl2 is DateTimePicker && ctrl2.Location.Y == ctrl.Location.Y)
                        {
                            //string cityTime = ((DateTimePicker)ctrl2).Value.ToLongTimeString();

                            NpgSQLClass.createRoute(int.Parse(trainId), city, ((DateTimePicker)ctrl2).Value.TimeOfDay, ((DateTimePicker)ctrl2).Value.TimeOfDay);
                                
                            break;
                        }
                    }
                }    
            }

            MessageBox.Show("Успешно сохранено");
        }

        private void AddTrain_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int y = btn.Location.Y;
            //Город
            foreach (Control ctrl in Controls)
            {
                if (Math.Abs(ctrl.Location.Y - y) < 3 && ctrl.Location.X > 200)
                {
                    ctrl.Parent = null;
                }
                else if (ctrl.Location.Y > y + 10 && ctrl.Location.X > 200)
                {
                    ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y - 50);
                }
            }

            comboY -= 50;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
