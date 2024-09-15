using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tickets
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            NpgSQLClass.conn = new NpgsqlConnection();
            NpgSQLClass.conn.ConnectionString =
                "Server = localhost; Port = 5432; Database = Trains; User ID = postgres; Password= hgij7792kj;";
            NpgSQLClass.conn.Open();
            Application.Run(new MainForm());

           // SQLClass.conn.Close();
        }

        public static string Login = "'andre@gmail.com'";
    }
}
