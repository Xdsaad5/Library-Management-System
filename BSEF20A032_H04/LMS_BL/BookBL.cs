using DTO;
using LMS_DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_BL
{
    public class BookBL
    {
        public int insertBook(BookDTO obj)
        {
            BookDAL dal = new BookDAL();
            return dal.insertBook(obj);

        }
        public string removeBook(int id, bool status=false)
        {
            BookDAL dal = new BookDAL();
            return dal.removeBook(id, status);
        }
        public string searchBook(int id)
        {
            BookDAL dal = new BookDAL();
            return dal.searchBook(id);
        }
        public string updateBookRecord(string newInfo,int oldAvailable, int oldTotalCopies,int id)
        {
            string[] tempArr = newInfo.Split(',');
            if (tempArr[5].Length != 0 && tempArr[6].Length == 0)
                tempArr[6] = (int.Parse(tempArr[5]) - oldTotalCopies + oldAvailable).ToString();
            BookDAL dal = new BookDAL();
            return dal.updateBookRecord(tempArr, id);
        }
        public List<BookDTO> searchBook(string searchInfo)
        {
            BookDAL dal = new BookDAL();
            return dal.searchBook(searchInfo);
        }
        public List<string> viewReport(string stDate, string endDate)
        {
            List<string> viewRep = new List<string>();
            if (!DateTime.TryParseExact(stDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime _))
            {
                viewRep.Add("Start date is not valid.");
                viewRep.Add("false");
                return viewRep;
            }
            else if (!DateTime.TryParseExact(endDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime _))
            {
                viewRep.Add("End date is not valid.");
                viewRep.Add("false");
                return viewRep;
            }
            BookDAL dal = new BookDAL();
            viewRep = dal.viewReport(stDate, endDate);
            return viewRep;
        }
    }
    public class BorrowedRecordBL
    {
        public string statusOfborrower(string borrower)
        {
            BorrowedRecordDAL brDal = new BorrowedRecordDAL();
            if (brDal.isLessthanThreeBooksBorrowed(borrower) == false)
                return "You already buy maximum limit of books.Please return old one to Borrow new Book.";
            string fine = brDal.checkFine(borrower);
            if (fine != "false")
                return fine;
            else if (brDal.isLessthanTwoBooksBorrowedInWeek(borrower) == false)
                return "You already Borrowed two Books in this week.";
          return "true";
        }
        public string borrowBookInfo(string id)
        {
            BorrowedRecordDAL brDal = new BorrowedRecordDAL();
            return brDal.borrowBookInfo(int.Parse(id));
        }
        public string borrowBook(BorrowedRecordDTO obj,int id)
        {
            if (id.ToString() != obj.BorrowRecordBookId)
                return "Now you have change book ID.";
            DateTime now = DateTime.Now;
            obj.BorrowRecordBorrowingDate = now.Date.ToString().Split(' ')[0];
            BorrowedRecordDAL brDal = new BorrowedRecordDAL();
            return brDal.borrowBook(obj);
        }
        public string returnBook(string returner,string id)
        {
            BorrowedRecordDAL brDal = new BorrowedRecordDAL();
            DateTime now = DateTime.Now;
            string returnDate = now.Date.ToString().Split(' ')[0];
            returnDate = "5/10/2023";
            return brDal.returnBook(returner,id,returnDate);
        }
        public List<string> viewBorrowerHistory(string borrowerId)
        {
            BorrowedRecordDAL brDal = new BorrowedRecordDAL();
            return brDal.viewBorrowerHistory(borrowerId);
        }
    }

}
