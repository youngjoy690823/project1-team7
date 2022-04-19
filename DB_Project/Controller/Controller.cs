using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB_Project.Models;
using System.Data;

namespace DB_Project.Controller
{
    public class Controller
    {
        Model models = new Model();
        public Controller()
        {

        }
        public string CheckLogin(string MemberName,string Password)
        {
            return models.CheckLogin(MemberName, Password);
        }

        public DataTable GetBook(string BookName, string AuthorName, string Company, string LocatioNname, string CategoryName,string Permission)
        {
            return models.GetBook(BookName,AuthorName,Company,LocatioNname,CategoryName, Permission);
        }
        public DataSet GetBook2(string BookNO)
        {
            return models.GetBook2(BookNO);
        }
        public void UpdateData(string BookName, string AuthorName, string Company, string LocationName, string CategoryName, string BookNo,string ISBN,string EditionYear)
        {
            models.UpdateData(BookName, AuthorName, Company, LocationName, CategoryName, BookNo,ISBN,EditionYear);
        }
        public void DeleteData(string BookNo)
        {
            models.DeleteData(BookNo);
        }
        public void DeleteBookBorrowData(string BookNo)
        {
            models.DeleteBookBorrowData(BookNo);
        }
        public void DeleteReviewData(string BookNo)
        {
            models.DeleteReviewData(BookNo);
        }
        public DataSet GetLocation()
        {
           return models.GetLocation();
        }
        public DataSet GetCategory()
        {
           return models.GetCategory();
        }
        public void InsertData(string BookName, string AuthorName, string Company, string LocationName, string CategoryName,string ISBN,string EditionYear)
        {
            models.insertData(BookName, AuthorName, Company, LocationName, CategoryName,ISBN,EditionYear);
        }
        public void UpdateDataToStatus(string Status,string BookNO)
        {
            models.UpdateDataToStatus(Status, BookNO);
        }
        public void insertRecordData(string Borrow_MID, string BookNO, string Deal_Date, string Status)
        {
            models.insertRecordData(Borrow_MID, BookNO, Deal_Date, Status);
        }
        public DataSet GetBookAndBorrow_Record(string Account)
        {
            return models.GetBookAndBorrow_Record(Account);
        }
        public DataSet GetBook3(string BookNO)
        {
            return models.GetBook3(BookNO);
        }
        public DataSet GetBookAndBorrow_Record2(string BookNO)
        {
            return models.GetBookAndBorrow_Record2(BookNO);
        }
        public void insertReviewData(string ACCOUNT, string Star, string BOOKNO, string ReviewDate)
        {
            models.insertReviewData(ACCOUNT, Star, BOOKNO, ReviewDate);
        }
    }
}