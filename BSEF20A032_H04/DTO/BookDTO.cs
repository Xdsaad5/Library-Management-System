using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BookDTO
    {
        private int id;
        private string title;
        private string author;
        private string description;
        private string ISBN;
        private string edition;
        private int totalCopies;
        private int availableCopies;
        public int BookId
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string BookTitle
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        public string BookAuthor
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
            }
        }
        public string BookDescription
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        public string BookISBN
        {
            get
            {
                return ISBN;
            }
            set
            {
                if(value.Length >= 13)
                    ISBN = value;
            }
        }
        public string BookEdition
        {
            get
            {
                return edition;
            }
            set
            {
                edition = value;
            }
        }
        public int BookTotalCopies
        {
            get
            {
                return totalCopies;
            }
            set
            {
                totalCopies = value;
            }
        }
        public int BookAvailableCopies
        {
            get
            {
                return availableCopies;
            }
            set
            {
                availableCopies = value;
            }
        }

    }
    public class BorrowedRecordDTO
    {
        private int id;
        private string bookId;
        private string borrowewdBy;
        private string borrowingDate;
        private string returnDate;
        private int fine;
        private string fineStatus;
        public int  BorrowRecordId
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string BorrowRecordBookId
        {
            get
            {
                return bookId;
            }
            set
            {
                bookId = value;
            }
        }
        public string BorrowRecordBorrowedBy
        {
            get
            {
                return borrowewdBy;
            }
            set
            {
                borrowewdBy = value;
            }
        }
        public string BorrowRecordBorrowingDate
        {
            get
            {
                return borrowingDate;
            }
            set
            {
                borrowingDate = value;
            }
        }
        public string BorrowRecordReturnDate
        {
            get
            {
                return returnDate;
            }
            set
            {
                returnDate = value;
            }
        }


        public int BorrowRecordFine
        {
            get
            {
                return fine;
            }
            set
            {
                fine = value;
            }
        }
        public string BorrowRecordFineStatus
        {
            get
            {
                return fineStatus;
            }
            set
            {
                fineStatus = value;
            }
        }


    }
}
