using LMS_PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using DTO;
using System.Globalization;

namespace BSEF20A032_H04
{
    class Program
    {
        static void Main(string[] args)
        {
            /*string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=My Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection sqlConn = new SqlConnection(connectionString: conString);
            sqlConn.Open();
            string query = $"INSERT INTO Book(title,author,description,ISBN,edition,totalCopies,availableCopies) OUTPUT inserted.id VALUES('C++','Saad','Learn with Saad','123','10th','10','10')";
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            int id = (int)cmd.ExecuteScalar();
            sqlConn.Close();*/
            /*BookPL pl = new BookPL();
            pl.viewReport();*/
            /*BookDTO obj = new BookDTO();
            obj.BookTitle = Console.ReadLine();
            if (obj.BookTitle.Length == 0)
                Console.Write("Pakistan");*/
            /*Console.Write("Current Date and Time is : ");
            DateTime now = DateTime.Now;
            Console.WriteLine(now.Date.ToString().Split(' ')[0]);
            Console.ReadLine();*/
            /*BorrowedRecordPL pl1 = new BorrowedRecordPL();
            
            pl1.borrowBook();*/
            /*            pl1.borrowBook();
            */
            /*DateTime date1 = new DateTime(2022,5,2);
            DateTime date2 = new DateTime(2023,5,10);

            TimeSpan diff = date2 - date1;
            int days = (int)diff.TotalDays;

            Console.WriteLine("There are {0} days between {1:d} and {2:d}", days, date1, date2);
            DateTime date = new DateTime(2023, 5, 2);
            string formattedDate = date.ToString("M/d/yyyy");
            Console.WriteLine(formattedDate);
            date = DateTime.Parse(formattedDate);
            formattedDate = date.ToString("yyyy/M/d");
            Console.WriteLine(formattedDate);*/
            /*string s1 = "05/06/2023";
            if (DateTime.TryParseExact(s1, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateValue))
            {
                Console.WriteLine("Yes its a date");
            }
            else
                Console.WriteLine("No its not a date");*/
            bool status = true;
            while (status == true)
            {
                Console.Write(""+
                "--------------------------------------------WELCOME TO LMS ---------------------------------------------\n"+
                "1------ Manage Books\n"+
                "2------ Manage Book Borrowings\n"+
                "3------ Exit   ");
                Console.ForegroundColor = ConsoleColor.Green;
                int choice = int.Parse(Console.ReadLine());
                Console.ResetColor();
                while (choice != 1 && choice !=2 && choice !=3)
                {
                    Console.Write("Entered Wrong choice enter it again: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    choice = int.Parse(Console.ReadLine());
                    Console.ResetColor();
                }
                if (choice == 1)
                {
                    bool bookStatus = true;
                    BookPL pl = new BookPL();
                    Console.Write("" +
                    "---------------------------------------------- Book Menu -----------------------------------------------\n");
                    while (bookStatus == true)
                    {
                        Console.Write("" +
                        "1----Add New Book\n" +
                        "2----Delete Existing Book\n" +
                        "3----Update Book Information\n" +
                        "4----Search for Books\n" +
                        "5----View Reports\n" +
                        "6----Exit ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        choice = int.Parse(Console.ReadLine());
                        Console.ResetColor();
                        while (choice != 1 && choice != 2 && choice != 3 && choice != 4 && choice != 5 && choice != 6)
                        {
                            Console.Write("Entered Wrong choice enter it again: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            choice = int.Parse(Console.ReadLine());
                            Console.ResetColor();
                        }
                        if (choice == 1)
                            pl.insertBook();
                        else if (choice == 2)
                            pl.removeBook();
                        else if (choice == 3)
                            pl.updateBookRecord();
                        else if (choice == 4)
                            pl.searchBook();
                        else if (choice == 5)
                            pl.viewReport();
                        else
                            bookStatus = false;
                    }
                }
                else if (choice == 2)
                {
                    bool borrowingStatus = true;
                    BorrowedRecordPL brBook = new BorrowedRecordPL();
                    Console.Write("" +
                    "----------------------------------------- Book Borrowings Menu ------------------------------------------\n");
                    while (borrowingStatus == true)
                    {
                        Console.Write("" +
                        "1----Borrow Book\n" +
                        "2----Return Book\n" +
                        "3----View Borrower History\n" +
                        "4----Exit ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        choice = int.Parse(Console.ReadLine());
                        Console.ResetColor();
                        while (choice != 1 && choice != 2 && choice != 3 && choice != 4)
                        {
                            Console.Write("Entered Wrong choice enter it again: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            choice = int.Parse(Console.ReadLine());
                            Console.ResetColor();
                        }
                        if (choice == 1)
                            brBook.borrowBook();
                        else if (choice == 2)
                            brBook.returnBook();
                        else if (choice == 3)
                            brBook.viewBorrowerHistory();
                        else
                            borrowingStatus = false;
                    }
                }
                else
                    status = false;
            }

        }
    }
}
