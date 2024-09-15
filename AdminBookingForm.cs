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
    public partial class AdminBookingForm : UserControl
    {
        public AdminBookingForm()
        {
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;
            button1_Click(null, null);
        }

        private void AdminBookingForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = dateTimePicker1.Value;
            DateTime dt2 = dateTimePicker2.Value;

            List<string> cities = NpgSQLClass.Select("SELECT name FROM cities ORDER BY name");
            Column5.Items.Clear();
            Column4.Items.Clear();
            Column4.Items.AddRange(cities.ToArray());
            Column5.Items.AddRange(cities.ToArray());


            List<string> orders = NpgSQLClass.getOrdersByRuns(dt1.ToShortDateString(), dt2.ToShortDateString());


            for (int i = 0; i < orders.Count; i += 8)
            {
                string[] row = new string[8];
                for (int j = i; j < i + 8; j++)
                    row[j - i] = orders[j];

                //Города
                List<string> runData = NpgSQLClass.Select(
                    "SELECT trains.name, dt FROM runs JOIN trains ON trains.id = runs.trainId WHERE runs.id=" + row[2]);
                row[2] = runData[0];
                row[3] = runData[1];
                Console.WriteLine(row[4]);
                row[4] = NpgSQLClass.findCityById(int.Parse(row[4]));
                row[5] = NpgSQLClass.findCityById(int.Parse(row[5]));

                ordersDGV.Rows.Add(row);
            }
        }

    }
}
