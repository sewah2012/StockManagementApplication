using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

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



}
