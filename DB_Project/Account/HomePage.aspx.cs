using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DB_Project.Account
{
    public partial class HomePage : System.Web.UI.Page
    {
        Controller.Controller controller = new Controller.Controller();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                BindGrid();
                if (Request["Permission"] == "1")
                {
                    grid.Columns[6].Visible = true;
                    grid.Columns[9].Visible = true;
                    grid.Columns[10].Visible = false;
                    grid.Columns[11].Visible = true;
                    grid.Columns[12].Visible = true;
                    grid.Columns[13].Visible = false;
                    btn_InsertBook.Visible = true;
                    chk_Return.Visible = false;
                }
                else
                {
                    grid.Columns[6].Visible = false;
                    grid.Columns[9].Visible = false;
                    grid.Columns[10].Visible = false;
                    grid.Columns[11].Visible = false;
                    grid.Columns[12].Visible = false;
                    grid.Columns[13].Visible = true;
                    btn_InsertBook.Visible = false;
                    chk_Return.Visible = true;
                }
            }
        }

        protected void Search(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void LoadData()
        {
            ddl_Location.DataSource = controller.GetLocation();
            ddl_Location.DataBind();
            ddl_Location.Items.Insert(0, new ListItem("", ""));

            ddl_Location_2.DataSource = controller.GetLocation();
            ddl_Location_2.DataBind();
            ddl_Location_2.Items.Insert(0, new ListItem("", ""));

            ddl_Category.DataSource = controller.GetCategory();
            ddl_Category.DataBind();
            ddl_Category.Items.Insert(0, new ListItem("", ""));

            ddl_Category_2.DataSource = controller.GetCategory();
            ddl_Category_2.DataBind();
            ddl_Category_2.Items.Insert(0, new ListItem("", ""));
        }
        private void BindGrid()
        {
            grid.DataSource = controller.GetBook(txt_BookName.Text, txt_AuthorName.Text, txt_Company.Text, ddl_Location.Text, ddl_Category.Text,Request["Permission"]);
            grid.DataBind();
        }

        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                Label deal_date = (Label)e.Row.FindControl("deal_date");
                if (row["deal_date"].ToString() == "")
                {
                    deal_date.Text = "";
                }
                else
                {
                    deal_date.Text = row["deal_date"].ToString().Substring(0, 10);
                }
                Label Status = (Label)e.Row.FindControl("Status");
                if (row["Status"].ToString() == "1")
                {
                    Status.Text = "可借閱";
                }
                else if(row["Status"].ToString() == "2")
                {
                    Status.Text = "已借出";
                }
                else if (row["Status"].ToString() == "3")
                {
                    Status.Text = "已預定";
                }
                LinkButton Return = (LinkButton)e.Row.FindControl("Return");
                if (row["Status"].ToString() == "2")
                {
                    Return.Text = "歸還";
                }
                else if (row["Status"].ToString() == "3")
                {
                    Return.Text = "取消預定";
                }
                LinkButton Borrow = (LinkButton)e.Row.FindControl("Borrow");
                if (row["Status"].ToString() == "3")
                {
                    Borrow.Text = "借出";
                }
                else if (row["Status"].ToString() == "3")
                {
                    Borrow.Text = "";
                }
                Label Return_date = (Label)e.Row.FindControl("Return_date");
                if (row["Status"].ToString() == "2")
                {
                    Return_date.Text = row["Return_date"].ToString();
                }
                else if (row["Status"].ToString() == "3")
                {
                    Return_date.Text = "";
                }
            }
        }
        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void link_Click(object sender, EventArgs e)
        {
            btn_Save.Visible = true;
            btn_Insert.Visible = false;
            LinkButton linkbutton = (LinkButton)sender;
            DataSet ds = new DataSet();
            ds = controller.GetBook2(linkbutton.CommandArgument);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txt_BookName_2.Text = ds.Tables[0].Rows[0]["BookName"].ToString();
                ddl_Category_2.SelectedValue = ds.Tables[0].Rows[0]["CategoryID"].ToString();
                ddl_Location_2.SelectedValue = ds.Tables[0].Rows[0]["LocationID"].ToString();
                txt_AuthorName_2.Text = ds.Tables[0].Rows[0]["AuthorName"].ToString();
                txt_Company_2.Text = ds.Tables[0].Rows[0]["Company"].ToString();
                txt_EditionYear.Text = ds.Tables[0].Rows[0]["EditionYear"].ToString();
                txt_ISBN.Text = ds.Tables[0].Rows[0]["ISBN"].ToString();
            }
            BookNo.Value = linkbutton.CommandArgument;
            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "$('#myModal').modal()", true);//show the modal
        }

        protected void Save(object sender, EventArgs e)
        {
            controller.UpdateData(txt_BookName_2.Text, txt_AuthorName_2.Text, txt_Company_2.Text, ddl_Location_2.SelectedValue, ddl_Category_2.SelectedValue, BookNo.Value, txt_ISBN.Text, txt_EditionYear.Text);
            BindGrid();
            Clear();
        }
        protected void InsertBook(object sender, EventArgs e)
        {
            btn_Save.Visible = false;
            btn_Insert.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "$('#myModal').modal()", true);//show the modal
        }
        protected void Insert(object sender, EventArgs e)
        {
            controller.InsertData(txt_BookName_2.Text, txt_AuthorName_2.Text, txt_Company_2.Text, ddl_Location_2.SelectedValue, ddl_Category_2.SelectedValue, txt_ISBN.Text, txt_EditionYear.Text);
            BindGrid();
            Clear();
        }
        protected void DeleteBook(object sender, EventArgs e)
        {
            LinkButton linkbutton = (LinkButton)sender;
            DataSet ds = new DataSet();
            ds = controller.GetBook2(linkbutton.CommandArgument);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb_BookName_5.Text = ds.Tables[0].Rows[0]["BookName"].ToString();
                lb_Category_3.Text = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                lb_Location_3.Text = ds.Tables[0].Rows[0]["LocationName"].ToString();
                lb_AuthorName_3.Text = ds.Tables[0].Rows[0]["AuthorName"].ToString();
                lb_Company_3.Text = ds.Tables[0].Rows[0]["Company"].ToString();
                lb_EditionYear_2.Text = ds.Tables[0].Rows[0]["EditionYear"].ToString();
                lb_ISBN_2.Text = ds.Tables[0].Rows[0]["ISBN"].ToString();
            }
            hf_BookNo_4.Value = linkbutton.CommandArgument;
            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "$('#myModal5').modal()", true);//show the modal    
        }
        protected void DeleteData(object sender, EventArgs e)
        {
            controller.DeleteReviewData(hf_BookNo_4.Value);
            controller.DeleteBookBorrowData(hf_BookNo_4.Value);
            controller.DeleteData(hf_BookNo_4.Value);
            BindGrid();
        }

        private void Clear()
        {
            txt_AuthorName_2.Text = "";
            ddl_Category_2.SelectedIndex = -1;
            txt_BookName_2.Text = "";
            txt_Company_2.Text = "";
            txt_EditionYear.Text = "";
            txt_ISBN.Text = "";
            ddl_Location_2.SelectedIndex = -1;
        }
        protected void btn_Return_click(object sender, EventArgs e)
        {
            controller.UpdateDataToStatus("1", hf_BookNo_3.Value);
            controller.insertRecordData(Request["Account"], hf_BookNo_3.Value, DateTime.Now.ToString("yyyy/MM/dd"), "1");
            controller.insertReviewData(Request["Account"],ddl_star.SelectedValue, hf_BookNo_3.Value ,DateTime.Now.ToString("yyyy/MM/dd"));
            grid.DataSource = controller.GetBookAndBorrow_Record(Request["Account"]);
            grid.DataBind();
        }
        protected void Return(object sender, EventArgs e)
        {
            LinkButton linkbutton = (LinkButton)sender;
            DataSet ds = new DataSet();
            ds = controller.GetBook3(linkbutton.CommandArgument);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if(ds.Tables[0].Rows[0]["Status"].ToString() == "2")
                {
                    lb_BookName_4.Text = ds.Tables[0].Rows[0]["BookName"].ToString();
                    lb_DealDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                    hf_BookNo_3.Value = linkbutton.CommandArgument;
                    ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "$('#myModal4').modal()", true);//show the modal
                }
                else if (ds.Tables[0].Rows[0]["Status"].ToString() == "3")
                {
                    controller.UpdateDataToStatus("1", linkbutton.CommandArgument);
                    controller.insertRecordData(Request["Account"], linkbutton.CommandArgument, DateTime.Now.ToString("yyyy/MM/dd"), "1");
                    grid.DataSource = controller.GetBookAndBorrow_Record(Request["Account"]);
                    grid.DataBind();
                }
            }
        }
        protected void chk_Return_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Return.Checked == true)
            {
                grid.Columns[2].Visible = false;
                grid.Columns[5].Visible = false;
                grid.Columns[6].Visible = true;
                grid.Columns[7].Visible = true;
                grid.Columns[8].Visible = true;
                grid.Columns[9].Visible = false;
                grid.Columns[10].Visible = true;
                grid.Columns[11].Visible = false;
                grid.Columns[12].Visible = false;
                grid.Columns[13].Visible = false;
                txt_AuthorName.ReadOnly = true;
                txt_BookName.ReadOnly = true;
                txt_Company.ReadOnly = true;
                ddl_Category.Enabled = false;
                ddl_Location.Enabled = false;
                grid.DataSource = controller.GetBookAndBorrow_Record(Request["Account"]);
                grid.DataBind();
            }
            else
            {
                grid.Columns[2].Visible = true;
                grid.Columns[5].Visible = true;
                grid.Columns[6].Visible = false;
                grid.Columns[7].Visible = false;
                grid.Columns[8].Visible = false;
                grid.Columns[9].Visible = false;
                grid.Columns[10].Visible = false;
                grid.Columns[11].Visible = false;
                grid.Columns[12].Visible = false;
                grid.Columns[13].Visible = true;
                txt_AuthorName.ReadOnly = false;
                txt_BookName.ReadOnly = false;
                txt_Company.ReadOnly = false;
                ddl_Category.Enabled = true;
                ddl_Location.Enabled = true;
                BindGrid();
            }
        }
        protected void Disagree(object sender, EventArgs e)
        {
            controller.UpdateDataToStatus("1", hf_BookNo.Value);
            controller.insertRecordData(Request["Account"], hf_BookNo.Value, DateTime.Now.ToString("yyyy/MM/dd"), "1");
            BindGrid();
        }
        protected void Accept(object sender, EventArgs e)
        {
            controller.UpdateDataToStatus("2", hf_BookNo.Value);
            controller.insertRecordData(lb_Account_1.Text, hf_BookNo.Value, DateTime.Now.ToString("yyyy/MM/dd"), "2");
            BindGrid();
        }
        protected void Borrow(object sender, EventArgs e)
        {
            LinkButton linkbutton = (LinkButton)sender;
            DataSet ds = new DataSet();
            ds = controller.GetBookAndBorrow_Record2(linkbutton.CommandArgument);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb_BookName_3.Text = ds.Tables[0].Rows[0]["BookName"].ToString();
                lb_Category_1.Text = ds.Tables[0].Rows[0]["categoryname"].ToString();
                lb_Location_1.Text = ds.Tables[0].Rows[0]["Locationname"].ToString();
                lb_AuthorName_1.Text = ds.Tables[0].Rows[0]["AuthorName"].ToString();
                lb_Company_1.Text = ds.Tables[0].Rows[0]["Company"].ToString();
                lb_Account_1.Text = ds.Tables[0].Rows[0]["borrow_mid"].ToString();
                lb_deal_date_1.Text = ds.Tables[0].Rows[0]["deal_date"].ToString();
            }
            hf_BookNo.Value = linkbutton.CommandArgument;
            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "$('#myModal2').modal()", true);//show the modal
        }
        protected void Booking(object sender, EventArgs e)
        {
            LinkButton linkbutton = (LinkButton)sender;
            DataSet ds = new DataSet();
            ds = controller.GetBook3(linkbutton.CommandArgument);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb_BookName_1.Text = ds.Tables[0].Rows[0]["BookName"].ToString();
                lb_Deal_Date_2.Text = DateTime.Now.ToString("yyyy/MM/dd");
            }
            hf_bookno_2.Value = linkbutton.CommandArgument;
            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "$('#myModal3').modal()", true);//show the modal
        }
        protected void Reserve(object sender, EventArgs e)
        {
            controller.UpdateDataToStatus("3", hf_bookno_2.Value);
            controller.insertRecordData(Request["Account"], hf_bookno_2.Value, DateTime.Now.ToString("yyyy/MM/dd"), "3");
            BindGrid();
        }
    }
}