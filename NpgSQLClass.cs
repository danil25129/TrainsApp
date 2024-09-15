using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Npgsql;
using System.Numerics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace Tickets
{
    public static class NpgSQLClass
    {
        public static NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = Trains; User ID = postgres; Password= hgij7792kj;");
       public static List<string> Select(string Text)
        {
            List<string> results = new List<string>();

            NpgsqlCommand command = new NpgsqlCommand(Text, conn);
            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    results.Add(reader.GetValue(i).ToString());
            }

            reader.Close();
            command.Dispose();
            return results;
        }

        public static void Insert(string Text)
        {
            NpgsqlCommand command = new NpgsqlCommand(Text, conn);
            DbDataReader reader = command.ExecuteReader();
            reader.Close();
            command.Dispose();
        }

        public static string Count(string Text)
        {
            NpgsqlCommand command = new NpgsqlCommand(Text, conn);

            String result = Convert.ToString(command.ExecuteScalar());

            return result;
        }

        public static void createUser(String name, String family, String password, String email)
        {
            NpgsqlCommand command = new NpgsqlCommand("select add_user(:value1, :value2, :value3, :value4)",conn);
            command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters.Add(new NpgsqlParameter("value2", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters.Add(new NpgsqlParameter("value3", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters.Add(new NpgsqlParameter("value4", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters[0].Value = name;
            command.Parameters[1].Value = family;
            command.Parameters[2].Value = password;
            command.Parameters[3].Value = email;
            command.ExecuteNonQuery();

        }

        public static void createEmployee(int tabNum, String name, String family, String password, String email)
        {
            NpgsqlCommand command = new NpgsqlCommand("select add_employee(:value1, :value2, :value3, :value4, :value5)", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value2", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters.Add(new NpgsqlParameter("value3", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters.Add(new NpgsqlParameter("value4", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters.Add(new NpgsqlParameter("value5", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters[0].Value = tabNum;
            command.Parameters[1].Value = name;
            command.Parameters[2].Value = family;
            command.Parameters[3].Value = password;
            command.Parameters[4].Value = email;
            command.ExecuteNonQuery();
        }

        public static String findCityById(int id)
        {
            NpgsqlCommand command = new NpgsqlCommand("select get_city_by_id(:value1)", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters[0].Value = id;
            String result = Convert.ToString(command.ExecuteScalar());

            return result;
        }

        public static void createTrain(int id, int cityFrom, int cityTo, String name, String days, int places) 
        {
            NpgsqlCommand command = new NpgsqlCommand("select add_train(:value1, :value2, :value3, :value4, :value5, :value6)", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value2", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value3", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value4", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters.Add(new NpgsqlParameter("value5", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters.Add(new NpgsqlParameter("value6", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters[0].Value = id;
            command.Parameters[1].Value = cityFrom;
            command.Parameters[2].Value = cityTo;
            command.Parameters[3].Value = name;
            command.Parameters[4].Value = days;
            command.Parameters[5].Value = places;
            command.ExecuteNonQuery();

        }

        public static void updateTrain(int id, int cityFrom, int cityTo, String name, String days, int places)
        {
            NpgsqlCommand command = new NpgsqlCommand("select update_train(:value1, :value2, :value3, :value4, :value5, :value6)", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value2", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value3", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value4", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters.Add(new NpgsqlParameter("value5", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters.Add(new NpgsqlParameter("value6", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters[0].Value = id;
            command.Parameters[1].Value = cityFrom;
            command.Parameters[2].Value = cityTo;
            command.Parameters[3].Value = name;
            command.Parameters[4].Value = days;
            command.Parameters[5].Value = places;
            command.ExecuteNonQuery();
        }

        public static void deleteRoute(int traiId)
        {
            NpgsqlCommand command = new NpgsqlCommand("select delete_route(:value1)", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters[0].Value = traiId;
            command.ExecuteNonQuery();
        }
        
        public static void createRoute(int traiId, String city, TimeSpan timeStart, TimeSpan timeFinish)
        {
            NpgsqlCommand command = new NpgsqlCommand("select add_route_from_cities(:value1, :value2, :value3, :value4)", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value2", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters.Add(new NpgsqlParameter("value3", NpgsqlTypes.NpgsqlDbType.Time));
            command.Parameters.Add(new NpgsqlParameter("value4", NpgsqlTypes.NpgsqlDbType.Time));
            command.Parameters[0].Value = traiId;
            command.Parameters[1].Value = city;
            command.Parameters[2].Value = timeStart;
            command.Parameters[3].Value = timeFinish;
            command.ExecuteNonQuery();

        }

        public static List<String> getOrdersByRuns(String dateFrom,  String dateTo)
        {
            List<String> result = new List<String>();

            NpgsqlCommand command = new NpgsqlCommand(" SELECT * from get_orders_by_runs(:value1, :value2)", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters.Add(new NpgsqlParameter("value2", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters[0].Value = dateFrom;
            command.Parameters[1].Value = dateTo;   
            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    result.Add(reader.GetValue(i).ToString());
            }

            reader.Close();
            command.Dispose();

            return result;
        }

        public static void deleteOrderByDate(String dateFrom, String dateTo)
        {
            NpgsqlCommand command = new NpgsqlCommand("select delete_order_by_date(:value1, :value2)", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters.Add(new NpgsqlParameter("value2", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters[0].Value = dateFrom;
            command.Parameters[1].Value=  dateTo;

            command.ExecuteNonQuery();
        }
        public static void deleteRunByDate(String dateFrom, String dateTo)
        {
            NpgsqlCommand command = new NpgsqlCommand("select delete_run_by_date(:value1, :value2)", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters.Add(new NpgsqlParameter("value2", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters[0].Value = dateFrom;
            command.Parameters[1].Value = dateTo;

            command.ExecuteNonQuery();
        }
        public static void createRun(int id, int trainId, String dt)
        {
            NpgsqlCommand command = new NpgsqlCommand("select add_run(:value1, :value2, :value3)", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value2", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value3", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters[0].Value = id;
            command.Parameters[1].Value = trainId;
            command.Parameters[2].Value = dt;
            command.ExecuteNonQuery();

        }

        public static List<String> getActiveRoutes(int cityFrom, int cityTo, String dateFrom, String dateTo)
        {

            List<String> result = new List<String>();

            NpgsqlCommand command = new NpgsqlCommand("select * from get_active_routes(:value1, :value2, cast(:value3 as date), cast(:value4 as date))", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value2", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value3", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters.Add(new NpgsqlParameter("value4", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters[0].Value = cityFrom;
            command.Parameters[1].Value = cityTo;
            command.Parameters[2].Value = dateFrom;
            command.Parameters[3].Value = dateTo;

            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    result.Add(reader.GetValue(i).ToString());
            }

            reader.Close();
            command.Dispose();

            return result;
        }

        public static void createOrder(int id, int login, int runid, int cityFrom, int employee, int cityTo, int wagon, int place, String status)
        {
            NpgsqlCommand command = new NpgsqlCommand("select add_order(:value1, :value2, :value3, :value4, :value5, :value6, :value7, :value8, :value9)", conn);
            command.Parameters.Add(new NpgsqlParameter("value1", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value2", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value3", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value4", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value5", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value6", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value7", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value8", NpgsqlTypes.NpgsqlDbType.Integer));
            command.Parameters.Add(new NpgsqlParameter("value9", NpgsqlTypes.NpgsqlDbType.Text));
            command.Parameters[0].Value = id;
            command.Parameters[1].Value = login;
            command.Parameters[2].Value = runid;
            command.Parameters[3].Value = cityFrom;
            command.Parameters[4].Value = employee;
            command.Parameters[5].Value = cityTo;
            command.Parameters[6].Value = wagon;
            command.Parameters[7].Value = place;
            command.Parameters[8].Value = status;

            command.ExecuteNonQuery();
        }
    }
}
