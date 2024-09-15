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
    public partial class AdminRunsForm : UserControl
    {
        public AdminRunsForm()
        {
            InitializeComponent();
            if (MainForm.pages.Count > MainForm.pagePos + 1)
                MainForm.pages.RemoveRange(MainForm.pagePos + 1, MainForm.pages.Count - MainForm.pagePos - 1);
            MainForm.pages.Add(this);
            MainForm.pagePos++;
        }

        private void AdminRunsForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить предыдущие рейсы за эти даты и заменить новыми?", "Предупреждение", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            DateTime dt1 = dateTimePicker1.Value;
            DateTime dt2 = dateTimePicker2.Value;
            List<string> trains = NpgSQLClass.Select("SELECT Id, Days FROM Trains");

            NpgSQLClass.deleteOrderByDate(dt1.ToShortDateString(), dt2.ToShortDateString());

            NpgSQLClass.deleteRunByDate(dt1.ToShortDateString(), dt2.ToShortDateString());

            while (dt1 < dt2)
            {
                int day = (int)dt1.DayOfWeek;
                if (day == 0) day = 7;//Воскресенье

                for (int i = 0; i < trains.Count; i += 2)
                {
                    //В этот день есть поезд
                    if (trains[i + 1].Contains(day.ToString()))
                    {
                        String runId = NpgSQLClass.Select("SELECT MAX(id) FROM runs")[0];
                        NpgSQLClass.createRun(int.Parse(runId) + 1, int.Parse(trains[i]), dt1.ToShortDateString());
                    }
                }

                dt1 = dt1.AddDays(1);
            }

            MessageBox.Show("Случилось");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = dateTimePicker1.Value;
            DateTime dt2 = dateTimePicker2.Value;
            runsDGV.Rows.Clear();

            List<string> trains = NpgSQLClass.Select("SELECT Concat(Name, ' ('," +
                "(SELECT name FROM cities WHERE id = trains.cityfrom), ' - ', " +
                "(SELECT name FROM cities WHERE id = trains.cityto), ')')" +
                "FROM trains ORDER BY name");
            Column2.Items.Clear();
            Column2.Items.AddRange(trains.ToArray());
            
            List<string> runs = NpgSQLClass.Select(
                "SELECT runs.id, trains.places, Concat(name, ' ('," +
                "(SELECT name FROM cities WHERE id = trains.cityfrom), ' - ', " +
                "(SELECT name FROM cities WHERE id = trains.cityto), ')'), dt FROM runs JOIN trains ON trains.id = runs.trainid" +
                " WHERE dt BETWEEN TO_DATE('" + dt1.ToShortDateString() + "', 'DD.MM.YYYY') AND TO_DATE('" + dt2.ToShortDateString() + "', 'DD.MM.YYYY')" +
                " ORDER BY dt");


            for (int  i = 0; i < runs.Count; i += 4)
            {
                string[] row = new string[4];
                row[0] = runs[i];
                row[1] = runs[i + 2];
                row[2] = runs[i + 3];
                String total = runs[i + 1];
                String booked = NpgSQLClass.Select("SELECT COUNT(*) FROM orders WHERE runid = " + runs[i])[0];

                row[3] = booked + " / " + total;

                runsDGV.Rows.Add(row);
            }
        }

        private void runsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string runId = runsDGV.Rows[e.RowIndex].Cells[0].Value.ToString();
            string runInfo = runsDGV.Rows[e.RowIndex].Cells[1].Value.ToString();

            BookedForRun atf = new BookedForRun(runId, runInfo);
            atf.Show();
        }
    }
}
