using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Collections;

namespace LMS_DAL
{
    public class BookDAL
    {
        SqlConnection sqlConn;
        public BookDAL()
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=My Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            sqlConn = new SqlConnection(connectionString: conString);
        }
        public int insertBook(BookDTO obj)
        {
            sqlConn.Open();
            {
                string query = $"select ISBN from Book where ISBN='{obj.BookISBN}'";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    dr.Close();
                    sqlConn.Close();
                    return -1;
                }
                dr.Close();
            }
            int id = 0;
            {
                string query = $"INSERT INTO Book(title,author,description,ISBN,edition,totalCopies,availableCopies) OUTPUT inserted.id VALUES('{obj.BookTitle}',' {obj.BookAuthor}','{obj.BookDescription}','{obj.BookISBN}','{obj.BookEdition}','{obj.BookTotalCopies}','{obj.BookAvailableCopies}')";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                id = (int)cmd.ExecuteScalar();
            }
            sqlConn.Close();
            return id;
        }
        public string searchBook(int id)
        {
            sqlConn.Open();
            string query = $"select * from Book where Id='{id}'";
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataReader dr = cmd.ExecuteReader();
            string bookInfo = "";
            if (dr.HasRows == false)
                bookInfo = $"You have Entered Wrong Id.";
            else
                while (dr.Read())
                    bookInfo = $"{dr[0].ToString()},{dr[1].ToString()},{dr[2].ToString()},{dr[3].ToString()},{dr[4].ToString()},{dr[5].ToString()},{dr[6].ToString()},{dr[7].ToString()}";
            dr.Close();
            sqlConn.Close();
            return bookInfo;
        }
        public string removeBook(int id, bool status = false)
        {
            string bookInfo = searchBook(id);
            if (status == true)
            {
                if (bookInfo == "You have Entered Wrong Id.")
                    return bookInfo;
                sqlConn.Open();
                string query = $"delete from BorrowingRecord  where bookId='{id}'";
                SqlCommand cmd1 = new SqlCommand(query, sqlConn);
                int st = cmd1.ExecuteNonQuery();
                query = $"delete from Book where Id='{id}'";
                SqlCommand cmd2 = new SqlCommand(query, sqlConn);
                st = cmd2.ExecuteNonQuery();
                bookInfo = "Deleted Successfully.";
                sqlConn.Close();
            }
            return bookInfo;
        }
        public string updateBookRecord(string[] updatedRec, int id)
        {
            string query = "UPDATE Book SET ";
            Console.WriteLine(query.Length);
            if (updatedRec[0].Length != 0)
            {
                query = query + $"title='{updatedRec[0]}'";
                if (updatedRec[1].Length != 0 || updatedRec[2].Length != 0 || updatedRec[3].Length != 0 || updatedRec[4].Length != 0 || updatedRec[5].Length != 0 || updatedRec[6].Length != 0)
                    query = query + " , ";
            }
            if (updatedRec[1].Length != 0)
            {
                query = query + $" author='{updatedRec[1]}'";
                if (updatedRec[2].Length != 0 || updatedRec[3].Length != 0 || updatedRec[4].Length != 0 || updatedRec[5].Length != 0 || updatedRec[6].Length != 0)
                    query = query + " , ";
            }
            if (updatedRec[2].Length != 0)
            {
                query = query + $"description='{updatedRec[2]}'";
                if (updatedRec[3].Length != 0 || updatedRec[4].Length != 0 || updatedRec[5].Length != 0 || updatedRec[6].Length != 0)
                    query = query + " , ";
            }
            if (updatedRec[3].Length != 0)
            {
                query = query + $" ISBN='{updatedRec[3]}'";
                if (updatedRec[4].Length != 0 || updatedRec[5].Length != 0 || updatedRec[6].Length != 0)
                    query = query + " , ";
            }
            if (updatedRec[4].Length != 0)
            {
                query = query + $"edition='{updatedRec[4]}'";
                if (updatedRec[5].Length != 0 || updatedRec[6].Length != 0)
                    query = query + " , ";
            }
            if (updatedRec[5].Length != 0)
            {
                query = query + $"totalCopies='{int.Parse(updatedRec[5])}'";
                if (updatedRec[6].Length != 0)
                    query = query + " , ";
            }
            if (updatedRec[6].Length != 0)
                query = query + $"availableCopies='{int.Parse(updatedRec[6])}'";
            if (query.Length > 16)
            {
                query = query + $" where Id='{id}'";
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                int st = cmd.ExecuteNonQuery();
                sqlConn.Close();
                return "Book information has been successfully updated";
            }
            return "You didn't enter even one field.\n";
        }
        public List<BookDTO> searchBook(string searchInfo)
        {
            string[] searchRec = searchInfo.Split(',');
            string query = "Select * from Book where ";
            if (searchRec[0].Length != 0)
            {
                query = query + $"Id='{int.Parse(searchRec[0])}'";
                if (searchRec[1].Length != 0 || searchRec[2].Length != 0 || searchRec[3].Length != 0 || searchRec[4].Length != 0 || searchRec[5].Length != 0 || searchRec[6].Length != 0 || searchRec[7].Length != 0)
                    query = query + " OR ";
            }
            if (searchRec[1].Length != 0)
            {
                query = query + $" title='{searchRec[1]}'";
                if (searchRec[2].Length != 0 || searchRec[3].Length != 0 || searchRec[4].Length != 0 || searchRec[5].Length != 0 || searchRec[6].Length != 0 || searchRec[7].Length != 0)
                    query = query + " OR ";
            }
            if (searchRec[2].Length != 0)
            {
                query = query + $"author='{searchRec[2]}'";
                if (searchRec[3].Length != 0 || searchRec[4].Length != 0 || searchRec[5].Length != 0 || searchRec[6].Length != 0 || searchRec[7].Length != 0)
                    query = query + " OR ";
            }
            if (searchRec[3].Length != 0)
            {
                query = query + $" description='{searchRec[3]}'";
                if (searchRec[4].Length != 0 || searchRec[5].Length != 0 || searchRec[6].Length != 0 || searchRec[7].Length != 0)
                    query = query + " OR ";
            }
            if (searchRec[4].Length != 0)
            {
                query = query + $"ISBN='{searchRec[4]}'";
                if (searchRec[5].Length != 0 || searchRec[6].Length != 0 || searchRec[7].Length != 0)
                    query = query + " OR ";
            }
            if (searchRec[5].Length != 0)
            {
                query = query + $"edition='{searchRec[5]}'";
                if (searchRec[6].Length != 0 || searchRec[7].Length != 0)
                    query = query + " OR ";
            }
            if (searchRec[6].Length != 0)
            {
                query = query + $"totalCopies='{int.Parse(searchRec[6])}'";
                if (searchRec[7].Length != 0)
                    query = query + " OR ";
            }
            if (searchRec[7].Length != 0)
                query = query + $"availableCopies='{int.Parse(searchRec[7])}'";
            List<BookDTO> listOfBook = new List<BookDTO>();
            if (query.Length > 25)
            {
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    BookDTO dto = new BookDTO();
                    dto.BookId = int.Parse(dr[0].ToString());
                    dto.BookTitle = dr[1].ToString();
                    dto.BookAuthor = dr[2].ToString();
                    dto.BookDescription = dr[3].ToString();
                    dto.BookISBN = dr[4].ToString();
                    dto.BookEdition = dr[5].ToString();
                    dto.BookTotalCopies = int.Parse(dr[6].ToString());
                    dto.BookAvailableCopies = int.Parse(dr[7].ToString());
                    listOfBook.Add(dto);
                }
                dr.Close();
                sqlConn.Close();
            }
            else
            {
                listOfBook.Add(new BookDTO { BookDescription = "You don't enter a single Field for search." });
            }
            return listOfBook;
        }
        public List<string> viewReport(string stDate, string endDate)
        {
            sqlConn.Open();
            string query = $"Select* from Book b, BorrowingRecord br WHERE CONVERT(DATETIME, borrowingDate, 101) BETWEEN '{stDate}' AND '{endDate}' AND br.bookId=b.Id";
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataReader dr = cmd.ExecuteReader();
            List<string> vieRep = new List<string>();
            while (dr.Read())
            {
                string info = dr[1].ToString() + "," + dr[2].ToString() + "," + dr[5].ToString() + "," + dr[10].ToString() + "," + dr[11].ToString() + ",";
                if (dr[12].ToString().Length == 0)
                    info = info + "Not returned.";
                else
                    info = info + dr[12].ToString();
                if (dr[13].ToString().Length == 0)
                    info = info + "," + "0";
                else
                    info = info + "," + dr[13].ToString();
                info = info + "," + dr[14].ToString();
                vieRep.Add(info);
            }
            dr.Close();
            sqlConn.Close();
            return vieRep;
        }
    }
    public class BorrowedRecordDAL
    {
        SqlConnection sqlConn;
        public BorrowedRecordDAL()
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=My Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            sqlConn = new SqlConnection(connectionString: conString);
        }
        public bool isLessthanThreeBooksBorrowed(string borrower)
        {
            if (borrower.Length == 0)
                return false;
            sqlConn.Open();
            string query = $"Select returningDate from BorrowingRecord where borrowedBy='{borrower}'";
            SqlCommand cmd =new SqlCommand(query, sqlConn);
            SqlDataReader dr = cmd.ExecuteReader();
            int i = 0;
            while (dr.Read())
                if (dr[0].ToString().Length == 0)
                    i++;
            dr.Close();
            sqlConn.Close();
            if (i >= 3)
                return false;
            return true;
        }
        public string checkFine(string borrower)
        {
            if (borrower.Length == 0)
                return "false";
            sqlConn.Open();
            string query = $"Select fine,fineStatus from BorrowingRecord where borrowedBy='{borrower}'";
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataReader dr = cmd.ExecuteReader();
            string status = "";
            while (dr.Read())
                if(dr[0].ToString().Length != 0)
                    if (int.Parse(dr[0].ToString()) > 0 && dr[1].ToString() == "unpaid")
                        status = $"This person yet to be paid {dr[0].ToString()} Rs.";
            dr.Close();
            sqlConn.Close();
            if(status == "")
                return "false";
            return status;
        }
        private bool isGreaterThanSeven(DateTime d1,DateTime d2)
        {
            string temp = d1.ToString("yyyy/M/d");
            d1 = DateTime.Parse(temp);
            temp = d2.ToString("yyyy/M/d");
            d2 = DateTime.Parse(temp);
            TimeSpan diff = (d1 - d2);
            int days = (int)diff.TotalDays;
            if (Math.Abs(days) > 7)
                return true;
            return false;
        }
        public bool isLessthanTwoBooksBorrowedInWeek(string borrower)
        {
            if (borrower.Length == 0)
                return false;
            sqlConn.Open();
            string query = $"Select borrowingDate,returningDate from BorrowingRecord where borrowedBy='{borrower}'";
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataReader dr = cmd.ExecuteReader();
            List<string> dates = new List<string>();
            while (dr.Read())
                if (dr[1].ToString().Length == 0)
                    dates.Add(dr[0].ToString());
            if (dates.Count == 2)
            {
                bool status = isGreaterThanSeven(DateTime.Parse(dates[0]), DateTime.Parse(dates[1]));
                if (status == false)
                    return false;
            }
            return true;
        }
        public bool isThisBookAlreadyAssignToHim(string borrower , int id)
        {
            sqlConn.Open();
            string query = $"SELECT COUNT(*) FROM BorrowingRecord WHERE bookId='{id}' AND borrowedBy='{borrower}'"; 
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            int count1 = (int)cmd.ExecuteScalar();
            Console.WriteLine($"1count: {count1}");
            if (count1 > 0)
            {
                query = $"SELECT COUNT(*) FROM BorrowingRecord WHERE bookId='{id}' AND borrowedBy='{borrower}' AND returningDate IS NOT NULL";
                SqlCommand cmd1 = new SqlCommand(query, sqlConn);
                int count2 = (int)cmd1.ExecuteScalar();
                Console.WriteLine($"2count: {count2}");
                if (count1 == count2)
                {
                    sqlConn.Close();
                    return false;
                }
                sqlConn.Close();
                return true;
            }
            else
            {
                sqlConn.Close();
                return false;
            }
           
        }
        public string borrowBookInfo(int id)
        {
            sqlConn.Open();
            string query = $"Select * from Book where Id={id}";
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataReader dr = cmd.ExecuteReader();
            string status = "";
            if (dr.HasRows == false)
                status = "No Book Found with this Identity";
            while (dr.Read())
                status = $"You wish to issue {dr[5].ToString()} edition of the {dr[1].ToString()} book by {dr[2].ToString()}.The remaining available copies after this will be {int.Parse(dr[7].ToString()) - 1} . ";
            dr.Close();
            sqlConn.Close();
            return status;
        }
        public string borrowBook(BorrowedRecordDTO obj)
        {
            string status = "";
            if (this.isThisBookAlreadyAssignToHim(obj.BorrowRecordBorrowedBy,int.Parse(obj.BorrowRecordBookId)) == true)
                return "Already assigned him.";
            sqlConn.Open();
            int availableCopies = 0;
            {
                string query = $"Select availableCopies from Book where Id='{int.Parse(obj.BorrowRecordBookId)}'";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    availableCopies = int.Parse(dr[0].ToString());
                dr.Close();
            }
            if (availableCopies <= 0)
            {
                sqlConn.Close();
                return "This Book is Not available.";
            }
            {
                string query = $"Update Book set availableCopies='{(availableCopies) - 1}' where Id='{int.Parse(obj.BorrowRecordBookId)}'";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.ExecuteNonQuery();
            }
            {
                string query = $"INSERT INTO BorrowingRecord(bookId,borrowedBy,borrowingDate) OUTPUT inserted.id VALUES('{int.Parse(obj.BorrowRecordBookId)}','{obj.BorrowRecordBorrowedBy}','{obj.BorrowRecordBorrowingDate}')";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                int st = cmd.ExecuteNonQuery();
            }
            sqlConn.Close();
            return "Book issued successfully";
        }
        private int calculateFine(DateTime bd,DateTime rd)
        {
            string temp = bd.ToString("yyyy/M/d");
            bd = DateTime.Parse(temp);
            temp = rd.ToString("yyyy/M/d");
            rd = DateTime.Parse(temp);
            TimeSpan diff = rd - bd;
            int days = (int)diff.TotalDays;
            if (days > 14)
                return (days - 14) * 10;
            return -1;
        }
        public string returnBook(string returner,string id,string returnDate)
        {
            int fine = -1;
            sqlConn.Open();
            {
                string query = $"select * from BorrowingRecord where borrowedBy='{returner}'";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataReader dr = cmd.ExecuteReader();
                string bDate = "";
                while (dr.Read())
                    if (dr[1].ToString() == id)
                        bDate = dr[3].ToString();
                if(bDate == "")
                {
                    dr.Close();
                    sqlConn.Close();
                    return "This book was not borrowed by this returner";
                }
                fine = calculateFine(DateTime.Parse(bDate), DateTime.Parse(returnDate));
                dr.Close();
            }
            {
                string query;
                if (fine == -1)
                    query = $"Update BorrowingRecord set returningDate='{returnDate}' where bookId='{int.Parse(id)}' AND borrowedBy='{returner}' ";
                else
                    query = $"Update BorrowingRecord set returningDate='{returnDate}' , fine='{fine}' where bookId='{int.Parse(id)}' AND borrowedBy='{returner}' ";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.ExecuteNonQuery();
               /* query = $"UPDATE BorrowingRecord SET bookId = NULL WHERE bookId = '{int.Parse(id)}' AND borrowedBy = '{returner}'";
                SqlCommand cmd1 = new SqlCommand(query, sqlConn);
                cmd1.ExecuteNonQuery();*/
            }
            string availableCopies="";
            {
                string query = $"Select availableCopies from Book where Id='{int.Parse(id)}'";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                    availableCopies = dr[0].ToString();
                dr.Close();
            }
            {
                string query = $"Update Book set availableCopies='{int.Parse(availableCopies)+1}' where Id='{int.Parse(id)}'";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                cmd.ExecuteNonQuery();
            }
            sqlConn.Close();
            return $"Book returned successfully. Now it's available copies are {int.Parse(availableCopies) + 1}";
        }
        public List<string> viewBorrowerHistory(string borrowerId)
        {
            sqlConn.Open();
            string query = $"Select * from Book b,BorrowingRecord br where borrowedBy='{borrowerId}' and br.BookId = b.Id";
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataReader dr = cmd.ExecuteReader();
            List<string> history = new List<string>();
            while (dr.Read())
            {
                string info = dr[1].ToString() + "," + dr[2].ToString() + "," + dr[5].ToString() + "," + dr[10].ToString() + "," + dr[11].ToString() + ",";
                if (dr[12].ToString().Length == 0)
                    info = info + "Not returned.";
                else
                    info = info + dr[12].ToString();
                if(dr[13].ToString().Length == 0)
                    info = info + "," + "0";
                else
                    info = info + "," + dr[13].ToString();
                info = info + "," + dr[14].ToString();
                history.Add(info);
            }
            dr.Close();
            sqlConn.Close();
            return history;
        }
    }
}
