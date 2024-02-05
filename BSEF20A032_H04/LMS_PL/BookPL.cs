using DTO;
using LMS_BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_PL
{
    public class BookPL
    {
        public void insertBook()
        {
            BookDTO bookDto = new BookDTO();
            Console.WriteLine("--------------------Enter.--------------------");
            Console.Write("\nTitle Of Book: ");
            bookDto.BookTitle = Console.ReadLine();
            Console.Write("Author Of Book: ");
            bookDto.BookAuthor = Console.ReadLine();
            Console.Write("Description Of Book: ");
            bookDto.BookDescription = Console.ReadLine();
            Console.Write("ISBN Of Book: ");
            bookDto.BookISBN = Console.ReadLine();
            Console.Write("Edition Of Book: ");
            bookDto.BookEdition = Console.ReadLine();
            Console.Write("Total Copies Of Book: ");
            bookDto.BookTotalCopies = int.Parse(Console.ReadLine());
            Console.Write("Available Copies Of Book: ");
            bookDto.BookAvailableCopies = int.Parse(Console.ReadLine());
            
            BookBL bookBl = new BookBL();
            int id  = bookBl.insertBook(bookDto);
            if (id != -1)
                Console.WriteLine ($"Book Successfully Added – the ID assigned is: {id} ");
            else
                Console.WriteLine($"This Book already exists in the system.");
        }
        public void removeBook()
        {
            Console.Write("Enter the ID of book which you want to delete: ");
            Console.ForegroundColor = ConsoleColor.Green;
            int id = int.Parse(Console.ReadLine());
            Console.ResetColor();
            BookBL bl = new BookBL();
            string status = bl.removeBook(id);
            if (status == "You have Entered Wrong Id.")
            {
                Console.WriteLine(status);
                return;
            }
            string[] tempArr = status.Split(',');
            
            Console.Write($"You wish to delete the {tempArr[5]} edition of {tempArr[1]} book by {tempArr[2]}.");
            Console.Write("If this information is correct, please re-enter the book ID: ");
            Console.ForegroundColor = ConsoleColor.Green;
            id = int.Parse(Console.ReadLine());
            Console.ResetColor();
            Console.WriteLine(bl.removeBook(id, true));
        }
        public void updateBookRecord()
        {
            Console.Write("Enter the ID of book which you want to update: ");
            Console.ForegroundColor = ConsoleColor.Green;
            int id = int.Parse(Console.ReadLine());
            Console.ResetColor();
            BookBL bl = new BookBL();
            string status = bl.removeBook(id);
            if (status == "You have Entered Wrong Id.")
            {
                Console.Write(status +'\n') ;
                return;
            }
            string[] tempArr = status.Split(',');
            Console.Write($"Id: {tempArr[0]}\nTitle: {tempArr[1]}\nAuthor: {tempArr[2]}\nDescription: {tempArr[3]}\nISBN: {tempArr[4]}\nEdition: {tempArr[5]}\nTotal Copies: {tempArr[6]}\nAvailable Copies: {tempArr[7]}");
            string newInfo = "";
            Console.WriteLine("\nPlease enter the fields you wish to update (leave blank otherwise):");
            Console.WriteLine("--------------------Enter.--------------------");
            Console.Write("\nTitle Of Book: ");
            Console.ForegroundColor = ConsoleColor.Green;
            newInfo = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Author Of Book: ");
            Console.ForegroundColor = ConsoleColor.Green;
            newInfo =newInfo +"," + Console.ReadLine();
            Console.ResetColor();
            Console.Write("Description Of Book: ");
            Console.ForegroundColor = ConsoleColor.Green;
            newInfo = newInfo + "," + Console.ReadLine();
            Console.ResetColor();
            Console.Write("ISBN Of Book: ");
            Console.ForegroundColor = ConsoleColor.Green;
            newInfo = newInfo + "," + Console.ReadLine();
            Console.ResetColor();
            Console.Write("Edition Of Book: ");
            Console.ForegroundColor = ConsoleColor.Green;
            newInfo = newInfo + "," + Console.ReadLine();
            Console.ResetColor();
            Console.Write("Total Copies Of Book: ");
            Console.ForegroundColor = ConsoleColor.Green;
            newInfo = newInfo + "," + Console.ReadLine();
            Console.ResetColor();
            Console.Write("Available Copies Of Book: ");
            Console.ForegroundColor = ConsoleColor.Green;
            newInfo = newInfo + "," + Console.ReadLine();
            Console.ResetColor();
            Console.WriteLine(bl.updateBookRecord(newInfo, int.Parse(tempArr[7]), int.Parse(tempArr[6]), id));
        }
        public void searchBook()
        {
            string searchInfo;
            Console.WriteLine("--------------------Enter.--------------------");
            Console.Write("\nID Of Book: ");
            Console.ForegroundColor = ConsoleColor.Green;
            searchInfo =  Console.ReadLine();
            Console.ResetColor();
            Console.Write("\nTitle Of Book: ");
            Console.ForegroundColor = ConsoleColor.Green;
            searchInfo = searchInfo + "," + Console.ReadLine();
            Console.ResetColor();
            Console.Write("Author Of Book: ");
            Console.ForegroundColor = ConsoleColor.Green;
            searchInfo = searchInfo + "," + Console.ReadLine();
            Console.ResetColor();
            Console.Write("Description Of Book: ");
            Console.ForegroundColor = ConsoleColor.Green;
            searchInfo = searchInfo + "," + Console.ReadLine();
            Console.ResetColor();
            Console.Write("ISBN Of Book: ");
            Console.ForegroundColor = ConsoleColor.Green;
            searchInfo = searchInfo + "," + Console.ReadLine();
            Console.ResetColor();
            Console.Write("Edition Of Book: ");
            Console.ForegroundColor = ConsoleColor.Green;
            searchInfo = searchInfo + "," + Console.ReadLine();
            Console.ResetColor();
            Console.Write("Total Copies Of Book: ");
            Console.ForegroundColor = ConsoleColor.Green;
            searchInfo = searchInfo + "," + Console.ReadLine();
            Console.ResetColor();
            Console.Write("Available Copies Of Book: ");
            Console.ForegroundColor = ConsoleColor.Green;
            searchInfo = searchInfo + "," + Console.ReadLine();
            Console.ResetColor();
            BookBL bl = new BookBL();
            List<BookDTO> listOfBook = bl.searchBook(searchInfo);
            if (listOfBook.Count == 0)
                Console.Write("Found Nothing.\n");
            else if (listOfBook[0].BookDescription == "You don't enter a single Field for search.")
                Console.Write("You don't enter a single Field for search.\n");
            else
            {
                Console.WriteLine("=====Search History===== \n");
                Console.WriteLine("{0,-23}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}", "BookID", "Title", "Author", "Edition", "TotalCopies", "AvailableCopies");
                foreach (var x in listOfBook)
                {
                    Console.WriteLine("{0,-23}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}", x.BookId, x.BookTitle, x.BookAuthor, x.BookEdition, x.BookTotalCopies, x.BookAvailableCopies);
                }
                Console.Write('\n');
            }

        }
        public void viewReport()
        {
            Console.Write("Enter starting date: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string stDate = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter ending date: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string endDate = Console.ReadLine();
            Console.ResetColor();
            BookBL bookBL = new BookBL();
            List <string>viewRep = bookBL.viewReport(stDate,endDate);
            if (viewRep.Count == 0)
                Console.WriteLine("No record found.");
            else if (viewRep.Count == 2 && viewRep[1] == "false")
                Console.WriteLine(viewRep[0]);
            else
            {
                Console.WriteLine("====== REPORT ======");
                Console.WriteLine("{0,-23}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}{6,-23}{7,-23}", "Title", "Author", "Edition", "Borrower", "Borrowed On", "Returned On", "Fine", "Status");
                foreach (var i in viewRep)
                {
                    string[] temp = i.Split(',');
                    foreach (var j in temp)
                        Console.Write("{0,-23}", j);
                    Console.Write('\n');
                }
                Console.Write('\n');
            }

        }
    }
    public class BorrowedRecordPL
    {
        public void borrowBook()
        {
            Console.WriteLine("--------------------Enter.--------------------");
            Console.Write("\nEnter borrower ID: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string borrower = Console.ReadLine();
            Console.ResetColor();
            BorrowedRecordBL brBl = new BorrowedRecordBL();
            string status = brBl.statusOfborrower(borrower);
            if (status != "true")
            {
                Console.WriteLine(status);
                return;
            }
            Console.Write("\nEnter book ID to be issued: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string id = Console.ReadLine();
            Console.ResetColor();
            status = brBl.borrowBookInfo(id);
            if(status == "No Book Found with this Identity")
            {
                Console.WriteLine(status);
                return;
            }
            Console.WriteLine(status);
            Console.Write("\nIf this information is correct, re-enter the book ID: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string id1 = Console.ReadLine();
            Console.ResetColor();
            BorrowedRecordDTO bbDto = new BorrowedRecordDTO
            {
                BorrowRecordBookId = id1,
                BorrowRecordBorrowedBy = borrower
            };
            Console.WriteLine(brBl.borrowBook(bbDto,int.Parse(id)));
        }
        public void returnBook()
        {
            Console.Write("\nEnter book ID to be returned: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string id = Console.ReadLine();
            Console.ResetColor();
            Console.Write("\nEnter returner identity: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string returner = Console.ReadLine();
            Console.ResetColor();
            BorrowedRecordBL brBl = new BorrowedRecordBL();
            Console.WriteLine(brBl.returnBook(returner, id));
        }
        public void viewBorrowerHistory()
        {
            Console.Write("Enter borrower identity: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string borrowerId = Console.ReadLine();
            Console.ResetColor();
            BorrowedRecordBL brBL = new BorrowedRecordBL();
            List<string> history = brBL.viewBorrowerHistory(borrowerId);
            if (history.Count == 0)
            {
                Console.WriteLine("No Record Found.");
                return;
            }
            Console.WriteLine("====== HISTORY ======");
            Console.WriteLine("{0,-23}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}{6,-23}{7,-23}", "Title", "Author", "Edition", "Borrowed By", "Borrowed On", "Returned On", "Fine", "Status");
            foreach (var i in history)
            {
                string[] temp = i.Split(',');
                foreach (var j in temp)
                    Console.Write("{0,-23}", j);
                Console.Write('\n');
            }
            Console.Write('\n');
        }
    }
}
