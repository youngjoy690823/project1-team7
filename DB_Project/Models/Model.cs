using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;

namespace DB_Project.Models
{
    public class Model
    {
        internal Model()
        {

        }
        string oradb = "Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 140.117.69.58)(PORT = 1521))(CONNECT_DATA =(SID = orcl)));"
                + "User Id=Group7;Password=group07;";

        internal string CheckLogin(string MemberName, string Password)
        {
            string sql = "";
            sql = sql + "SELECT Permission FROM MEMBER WHERE 1=1 and MEMBERNAME = :MemberName and PASSWORD = :Password";

            OracleConnection conn = new OracleConnection(oradb);
            conn.Open();
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.Parameters.AddWithValue(":MemberName", MemberName);
            cmd.Parameters.AddWithValue(":Password", Password);
            object obj = cmd.ExecuteOracleScalar();

            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }
        public DataTable GetBook(string BookName, string AuthorName, string Company, string LocationName, string CategoryName,string Permission)
        {
            DataTable dt = new DataTable();
            OracleConnection conn = new OracleConnection(oradb);  //連線到OracleDB
            string sql2 = @"";

            sql2 = sql2 + @"select t.BookNO,t.bookname,t.authorname,t.company,b.categoryname,c.locationname,'' as deal_date,'' as return_date,t.Status ,count(d.book_no) as borrow_count
                            from book t 
                            left join category b on t.categoryid=b.categoryid 
                            join LOCATION  c on t.locationid=c.locationid 
                            left join borrow_recoder d on t.bookno=d.book_no and d.status='2'
                            where 1=1";

            if(Permission!="1")
            {
                sql2 = sql2 + @" and t.status='1'";
            }
            if (BookName != "")
            {
                sql2 = sql2 + @" and BookName like '%" + BookName + "%'";
            }

            if (AuthorName != "")
            {
                sql2 = sql2 + @" and AuthorName like '%" + AuthorName + "%'";
            }
            if (Company != "")
            {
                sql2 = sql2 + @" and Company like '%" + Company + "%'";
            }
            if (LocationName != "")
            {
                sql2 = sql2 + @" and t.LocationID like '%" + LocationName + "%'";
            }
            if (CategoryName != "")
            {
                sql2 = sql2 + @" and t.CategoryID like '%" + CategoryName + "%'";
            }
            sql2 = sql2 + @" and ROWNUM <=100 
                             group by (t.BookNO,t.bookname,t.authorname,t.company,b.categoryname,c.locationname,t.Status)";
            if (Permission != "1")
            {
                sql2 = sql2 + @" order by borrow_count desc";
            }
            else
            {
                sql2 = sql2 + @" order by t.status desc,borrow_count desc";
            }
            //新增的連線開始
            conn.Open();
            OracleCommand cmd2 = new OracleCommand(sql2, conn);
            OracleDataAdapter ODACA = new OracleDataAdapter(cmd2);
            DataSet dsA = new DataSet();
            ODACA.Fill(dsA, "Table1");
            DataTable dtA = dsA.Tables["Table1"];
            dtA.Constraints.Clear();
            conn.Close();

            return dsA.Tables["Table1"];
        }
        public DataSet GetBook2(string BookNO)
        {
            DataTable dt = new DataTable();
            OracleConnection conn = new OracleConnection(oradb);  //連線到OracleDB
            string sql2 = @"";

            sql2 = sql2 + @"select a.BookNO,a.bookname,a.authorname,a.company,a.categoryID,b.categoryname,a.locationID,c.locationname,a.ISBN,a.EditionYear from book a left join Category b on a.categoryid=b.categoryid left join Location c on a.locationid=c.locationid where 1=1 and a.BookNO='" + BookNO + "'";


            //新增的連線開始
            conn.Open();
            OracleCommand cmd2 = new OracleCommand(sql2, conn);
            OracleDataAdapter ODACA = new OracleDataAdapter(cmd2);
            DataSet dsA = new DataSet();
            ODACA.Fill(dsA, "Table1");
            conn.Close();

            return dsA;
        }
        public void UpdateData(string BookName, string AuthorName, string Company, string LocatioNname, string CategoryName, string BookNo, string ISBN, string EditionYear)
        {
            OracleConnection conn = new OracleConnection(oradb);
            string sql = @"update book set BookName='" + BookName + "',AuthorName='" + AuthorName + "',Company= '" + Company + "',LocationID='" + LocatioNname + "',CategoryID='" + CategoryName + "',ISBN='" + ISBN + "',EditionYear='" + EditionYear + "',Modifydate=LOCALTIMESTAMP(2) where BookNo='" + BookNo + "'";
            conn.Open();
            OracleCommand cmd;

            cmd = new OracleCommand(sql, conn);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void DeleteData(string BookNo)
        {
            OracleConnection conn = new OracleConnection(oradb);
            string sql = @"delete from book where BookNo='" + BookNo + "'";
            conn.Open();
            OracleCommand cmd;

            cmd = new OracleCommand(sql, conn);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void DeleteBookBorrowData(string BookNo)
        {
            OracleConnection conn = new OracleConnection(oradb);
            string sql = @"delete from borrow_recoder where Book_No='" + BookNo + "'";
            conn.Open();
            OracleCommand cmd;

            cmd = new OracleCommand(sql, conn);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void DeleteReviewData(string BookNo)
        {
            OracleConnection conn = new OracleConnection(oradb);
            string sql = @"delete from review where BookNo='" + BookNo + "'";
            conn.Open();
            OracleCommand cmd;

            cmd = new OracleCommand(sql, conn);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public DataSet GetLocation()
        {
            OracleConnection conn = new OracleConnection(oradb);  //連線到OracleDB
            string sql2 = @"";

            sql2 = sql2 + @"select LocationID,LocationName from location";

            //新增的連線開始
            conn.Open();
            OracleCommand cmd2 = new OracleCommand(sql2, conn);
            OracleDataAdapter ODACA = new OracleDataAdapter(cmd2);
            DataSet dsA = new DataSet();
            ODACA.Fill(dsA, "Table1");
            conn.Close();

            return dsA;
        }
        public DataSet GetCategory()
        {
            OracleConnection conn = new OracleConnection(oradb);  //連線到OracleDB
            string sql2 = @"";

            sql2 = sql2 + @"select CategoryID,CategoryName from category";

            //新增的連線開始
            conn.Open();
            OracleCommand cmd2 = new OracleCommand(sql2, conn);
            OracleDataAdapter ODACA = new OracleDataAdapter(cmd2);
            DataSet dsA = new DataSet();
            ODACA.Fill(dsA, "Table1");
            conn.Close();

            return dsA;
        }
        public void insertData(string BookName, string AuthorName, string Company, string LocatioNname, string CategoryName, string ISBN, string EditionYear)
        {
            string BookNO = LocatioNname + DateTime.Now.ToString("yyyyMMddHHmmss");
            OracleConnection conn = new OracleConnection(oradb);
            string sql = @"insert into Book(BookNO,BookName,AuthorName,Company,CategoryID,LocationID,ISBN,EditionYear,Status,ModifyDate) Values('" + BookNO + "','" + BookName + "','" + AuthorName + "','" + Company + "','" + CategoryName + "','" + LocatioNname + "','" + ISBN + "','" + EditionYear + "','1',LOCALTIMESTAMP(2))";
            conn.Open();
            OracleCommand cmd;

            cmd = new OracleCommand(sql, conn);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void UpdateDataToStatus(string Status, string BookNo)
        {
            OracleConnection conn = new OracleConnection(oradb);
            string sql = @"update book set Status='" + Status + "' where BookNo='" + BookNo + "'";
            conn.Open();
            OracleCommand cmd;

            cmd = new OracleCommand(sql, conn);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void insertRecordData(string Borrow_MID, string BookNO, string Deal_Date, string Status)
        {
            string Borrow_No = Status + DateTime.Now.ToString("yyyyMMddHHmmss");
            OracleConnection conn = new OracleConnection(oradb);
            string sql = @"insert into borrow_recoder(Borrow_No,Borrow_MID,Book_NO,Deal_Date,Status) Values('" + Borrow_No + "','" + Borrow_MID + "','" + BookNO + "',TO_DATE('" + Deal_Date + "','yyyy/MM/dd'),'" + Status + "')";
            conn.Open();
            OracleCommand cmd;

            cmd = new OracleCommand(sql, conn);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void insertReviewData(string ACCOUNT, string Star, string BOOKNO,string ReviewDate)
        {
            string ReviewNo = Star + DateTime.Now.ToString("yyyyMMddHHmmss");
            OracleConnection conn = new OracleConnection(oradb);
            string sql = @"insert into review(ReviewNo,ReaderID,Star,REVIEWDATE,BOOKNO) Values('" + ReviewNo + "','" + ACCOUNT + "','" + Star + "',TO_DATE('" + ReviewDate + "','yyyy/MM/dd'),'" + BOOKNO + "')";
            conn.Open();
            OracleCommand cmd;

            cmd = new OracleCommand(sql, conn);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public DataSet GetBookAndBorrow_Record(string Account)
        {
            DataTable dt = new DataTable();
            OracleConnection conn = new OracleConnection(oradb);  //連線到OracleDB

            string sql2 = @"select a.bookno,a.bookname,a.authorname,a.company,c.categoryname,max(b.deal_date) as deal_date,to_char(max(b.deal_date)+ 14,'yyyy/mm/dd') as return_date,a.status
                            from book a 
                            join borrow_recoder b on a.bookno=b.book_no
                            left join category c on a.categoryid=c.categoryid
                            where a.status in ('2','3') and b.borrow_mid='" + Account + "' group by(a.bookno,a.bookname,a.authorname,a.company,c.categoryname,a.status)";


            //新增的連線開始
            conn.Open();
            OracleCommand cmd2 = new OracleCommand(sql2, conn);
            OracleDataAdapter ODACA = new OracleDataAdapter(cmd2);
            DataSet dsA = new DataSet();
            ODACA.Fill(dsA, "Table1");
            conn.Close();

            return dsA;
        }
        public DataSet GetBook3(string BookNO)
        {
            DataTable dt = new DataTable();
            OracleConnection conn = new OracleConnection(oradb);  //連線到OracleDB
            string sql2 = @"";

            sql2 = sql2 + @"select t.BookNO,t.bookname,t.status from book t where 1=1 and t.BookNO='" + BookNO + "'";


            //新增的連線開始
            conn.Open();
            OracleCommand cmd2 = new OracleCommand(sql2, conn);
            OracleDataAdapter ODACA = new OracleDataAdapter(cmd2);
            DataSet dsA = new DataSet();
            ODACA.Fill(dsA, "Table1");
            conn.Close();

            return dsA;
        }
        public DataSet GetBookAndBorrow_Record2(string BookNo)
        {
            DataTable dt = new DataTable();
            OracleConnection conn = new OracleConnection(oradb);  //連線到OracleDB

            string sql2 = @"select a.bookno,a.bookname,a.authorname,a.company,c.categoryname,d.Locationname,b.borrow_mid,max(b.deal_date) as deal_date
                            from book a 
                            join borrow_recoder b on a.bookno=b.book_no
                            left join category c on a.categoryid=c.categoryid
                            left join Location d on a.LocationID=d.LocationID
                            where a.status='3' and a.bookno='" + BookNo + "' group by(a.bookno,a.bookname,a.authorname,a.company,c.categoryname,d.Locationname,b.borrow_mid)";


            //新增的連線開始
            conn.Open();
            OracleCommand cmd2 = new OracleCommand(sql2, conn);
            OracleDataAdapter ODACA = new OracleDataAdapter(cmd2);
            DataSet dsA = new DataSet();
            ODACA.Fill(dsA, "Table1");
            conn.Close();

            return dsA;
        }
    }
}