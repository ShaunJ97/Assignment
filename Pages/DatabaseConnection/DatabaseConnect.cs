using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Pages.DatabaseConnection
{
    public class DatabaseConnect
    {
        public string DatabaseString()
        {
            string DbString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\USERS\LEAHF\ONEDRIVE\DOCUMENTS\SHAUN\ASSIGNMENT\DATA\ASSIGNMENT-DATABASE.MDF;Integrated Security=True;Connect Timeout=30;";
            return DbString;
        }

        //@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\USERS\LEAHF\ONEDRIVE\DOCUMENTS\SHAUN\ASSIGNMENT\DATA\ASSIGNMENT-DATABASE.MDF;Integrated Security=True;Connect Timeout=30;";
    }
}
