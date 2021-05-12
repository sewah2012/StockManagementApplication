using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

public class Cnx
{
	public Cnx()
	{
	}

   public static SQLiteConnection CreateConnection()
    {

        SQLiteConnection sqlite_conn;
        // Create a new database connection:
        sqlite_conn = new SQLiteConnection("Data Source=database.db");
         // Open the connection:
         try
        {
            
            if (!File.Exists("./database.db"))
            {
                SQLiteConnection.CreateFile("database.db");
                Debug.WriteLine("Database created successfuly");
            }
                sqlite_conn.Open();
                Debug.WriteLine("Connection created successfuly");
            }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
        return sqlite_conn;
    }

    //inserting data or saving data

    public static void InsertData(SQLiteConnection conn, String commandText)
    {
        try
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = commandText;
            sqlite_cmd.ExecuteNonQuery();
        }catch(Exception ex)
        {
            Debug.WriteLine(ex);
        }

    }

    public static void populateTable(SQLiteConnection conn, String query, DataGridView dv)
    {
        SQLiteCommand cmd = new SQLiteCommand(query, conn);
        DataTable dt = new DataTable();
        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
        adapter.Fill(dt);

        dv.DataSource= dt;
    }

    public static SQLiteDataReader getData(SQLiteConnection conn, String query)
    {
        SQLiteCommand cmd = new SQLiteCommand(query, conn);
        SQLiteDataReader rdr = cmd.ExecuteReader();

        return rdr;
    }



}
