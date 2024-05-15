using System;
using System.Data;

using System.Collections.Generic;
using System.Data.SQLite;
using GestiuneDonatii.utils;

namespace ConnectionUtils
{
    public class SqliteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection createConnection(IDictionary<string,string> props)
        {
            //
            // //String connectionString = "URI=file:/Users/grigo/didactic/MPP/ExempleCurs/2017/database/tasks.db,Version=3";
            // String connectionString = props["ConnectionString"];
            // Console.WriteLine("SQLite ---Se deschide o conexiune la  ... {0}", connectionString);
            // return new SQLiteConnection(connectionString);

            // Windows SQLite Connection, fisierul .db ar trebuie sa fie in directorul debug/bin
            String connectionString = "Data Source=teledon.db;Version=3";
            // String connectionString = "URI=file:D:/Facultate/GestiuneDonatii-CSharp/teledon.db,Version=3";
            Console.WriteLine("SQLite ---Se deschide o conexiune la  ... {0}", connectionString);
            return new SQLiteConnection(connectionString);
        }
    }
}