using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Assignment.Models;
using Assignment.Pages.DatabaseConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel;
using Section = MigraDoc.DocumentObjectModel.Section;
using Paragraph = MigraDoc.DocumentObjectModel.Paragraph;
using Color = MigraDoc.DocumentObjectModel.Color;
using Table = MigraDoc.DocumentObjectModel.Tables.Table;
using Microsoft.AspNetCore.Hosting;


namespace Assignment.Pages.AdminPages
{
    public class ViewEmployeesModel : PageModel
    {
        public List<Employee> Employee { get; set; }

        public string UserName;
        public const string SessionKeyName1 = "username";

        public string FirstName;
        public const string SessionKeyName2 = "fname";

        public string SessionID;
        public const string SessionKeyName3 = "sessionID";

        public User user { get; }



        IWebHostEnvironment _env;

        public ViewEmployeesModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        
        public IActionResult OnGet(string pdf)
        {
            //get the session first!
            UserName = HttpContext.Session.GetString(SessionKeyName1);
            FirstName = HttpContext.Session.GetString(SessionKeyName2);
            SessionID = HttpContext.Session.GetString(SessionKeyName3);


            DatabaseConnect dbstring = new DatabaseConnect(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT EmployeeID,
                                        EmpFName + ' ' + EmpLName AS EmployeeName,
                                        EmpDoB AS EmployeeBornOn,
                                        EmpHireDate AS EmployedOn
                                        FROM EmployeesTable;";

                var reader = command.ExecuteReader();

                Employee = new List<Employee>();
                while (reader.Read())
                {
                    Employee Row = new Employee(); //each record found from the table
                    Row.EmployeeId = reader.GetInt32(0);
                    Row.EmployeeFirstName = reader.GetString(1);
                    Row.EmployeeDoB = reader.GetDateTime(2) ;
                    Row.EmployeeDateHired = reader.GetDateTime(3);
                    Employee.Add(Row);
                }

            }


            //PDF code here!
            if (pdf == "1")
            {
                //Create an object for pdf document
                Document doc = new Document();
                Section sec = doc.AddSection();
                Paragraph para = sec.AddParagraph();

                para.Format.Font.Name = "Arial";
                para.Format.Font.Size = 14;
                para.Format.Font.Color = Color.FromCmyk(0, 0, 0, 100); //black colour
                para.AddFormattedText("List of Employees", TextFormat.Bold);
                para.Format.SpaceAfter = "1.0cm";

                para.AddFormattedText();

                //Table
                Table tab = new Table();
                tab.Borders.Width = 0.75;
                tab.TopPadding = 5;
                tab.BottomPadding = 5;

                //Column
                Column col = tab.AddColumn(Unit.FromCentimeter(3));
                col.Format.Alignment = ParagraphAlignment.Center;
                tab.AddColumn(Unit.FromCentimeter(3));
                tab.AddColumn(Unit.FromCentimeter(3));
                tab.AddColumn(Unit.FromCentimeter(3));
                //tab.AddColumn(Unit.FromCentimeter(3));


                //Row
                Row row = tab.AddRow();
                row.Shading.Color = Colors.Coral;//select your preference colour!

                //Cell for header
                Cell cell = new Cell();
                cell = row.Cells[0];
                cell.AddParagraph("EmployeeID");
                cell = row.Cells[1];
                cell.AddParagraph("Employee First Name");
                cell = row.Cells[2];
                cell.AddParagraph("Employee Date of Birth");
                cell = row.Cells[3];
                cell.AddParagraph("Employee Hire Date");

                //Add data to table 
                for (int i = 0; i < Employee.Count; i++)
                {
                    row = tab.AddRow();
                    cell = row.Cells[0];
                    cell.AddParagraph(Convert.ToString(i + 1));
                    cell = row.Cells[1];
                    cell.AddParagraph(Employee[i].EmployeeFirstName);
                    cell = row.Cells[2];
                    cell.AddParagraph(Convert.ToString(Employee[i].EmployeeDateHired));
                    cell = row.Cells[3];
                    cell.AddParagraph(Convert.ToString(Employee[i].EmployeeDoB));
                }

                tab.SetEdge(0, 0, 4, (Employee.Count + 1), Edge.Box, BorderStyle.Single, 1.5, Colors.Black);
                sec.Add(tab);

                //Rendering
                PdfDocumentRenderer pdfRen = new PdfDocumentRenderer();
                pdfRen.Document = doc;
                pdfRen.RenderDocument();

                //Create a memory stream
                MemoryStream stream = new MemoryStream();
                pdfRen.PdfDocument.Save(stream); //saving the file into the stream

                Response.Headers.Add("content-disposition", new[] { "inline; filename = ListofEmployees.pdf" });
                return File(stream, "application/pdf");
            }
            return Page();

        }
    }
}