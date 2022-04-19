<%@ Page Title="館藏查詢" Language="C#" MasterPageFile="~/Site3.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="DB_Project.Account.HomePage" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
    <h2><%: Title %>.</h2>
    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal">
                <div class="form-group">
                    <asp:Label runat="server" ID="lb_BookName" CssClass="col-md-2 control-label">書籍名稱</asp:Label>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txt_BookName" CssClass="form-control" />
                    </div>
                    <asp:Label runat="server" ID="lb_Category" CssClass="col-md-2 control-label">類別</asp:Label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddl_Category" runat="server" DataTextField="CategoryName" DataValueField="CategoryID" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="lb_Location" CssClass="col-md-2 control-label">存放地點</asp:Label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddl_Location" runat="server" DataTextField="LocationName" DataValueField="LocationID" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <asp:Label runat="server" ID="lb_AuthorName" CssClass="col-md-2 control-label">作者</asp:Label>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txt_AuthorName" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="lb_Company" CssClass="col-md-2 control-label">出版社</asp:Label>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txt_Company" CssClass="form-control" Text="" />
                    </div>
                    <asp:Label runat="server" ID="Label1" CssClass="col-md-2 control-label"></asp:Label>
                    <div class="col-md-4">
                        <asp:CheckBox runat="server" ID="chk_Return" Text="已借閱書籍" OnCheckedChanged="chk_Return_CheckedChanged" AutoPostBack="true" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <asp:Button runat="server" Text="查詢" CssClass="btn btn-default" OnClick="Search" />
                    </div>
                </div>
                <asp:Button runat="server" ID="btn_InsertBook" Text="新增書籍" CssClass="btn btn-default" OnClick="InsertBook" />
                <asp:GridView ID="grid" runat="server" DataKeyNames="BookNO" AllowSorting="True" AutoGenerateSelectButton="false"
                    AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="No data found"
                    EnableModelValidation="True" PageSize="15" OnRowDataBound="grid_RowDataBound"
                    OnPageIndexChanging="grid_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="BookName" HeaderText="書名" InsertVisible="False" ReadOnly="True" HeaderStyle-Width="400px" />
                        <asp:BoundField DataField="Categoryname" HeaderText="類別" HeaderStyle-Width="100px" />
                        <asp:BoundField DataField="Locationname" HeaderText="地點" HeaderStyle-Width="70px" />
                        <asp:BoundField DataField="authorname" HeaderText="作者" HeaderStyle-Width="150px" />
                        <asp:BoundField DataField="Company" HeaderText="出版社" HeaderStyle-Width="150px" />
                        <asp:BoundField DataField="borrow_count" HeaderText="借出次數" HeaderStyle-Width="70px" />
                        <asp:TemplateField HeaderText="狀態" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Status" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="預定/借閱日期" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="deal_date" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="需歸還日期" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="Return_date" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="Borrow" runat="server" CommandName="Borrow" CommandArgument='<%# Eval("BookNO") %>' OnClick="Borrow"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="Return" runat="server" CommandName="Return" CommandArgument='<%# Eval("BookNO") %>' OnClick="Return"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="Change" Text="修改" runat="server" CommandName="BookNO" CommandArgument='<%# Eval("BookNO") %>' OnClick="link_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton Text="刪除" runat="server" CommandArgument='<%# Eval("BookNO") %>' OnClick="DeleteBook"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="Booking" Text="預定" runat="server" CommandArgument='<%# Eval("BookNO") %>' CommandName="Booking" OnClick="Booking"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div id="myModal" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 500px;height: 700px;">
                <div class="modal-header">
                    <h5 class="modal-title">書籍資料</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lb_BookName_2">書籍名稱</asp:Label>
                        <div>
                            <asp:TextBox runat="server" ID="txt_BookName_2" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="lb_Category_2">類別</asp:Label>
                        <div>
                            <asp:DropDownList ID="ddl_Category_2" runat="server" DataTextField="CategoryName" DataValueField="CategoryID" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="lb_Location_2">存放地點</asp:Label>
                        <div>
                            <asp:DropDownList ID="ddl_Location_2" runat="server" DataTextField="LocationName" DataValueField="LocationID" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="lb_AuthorName_2">作者</asp:Label>
                        <div>
                            <asp:TextBox runat="server" ID="txt_AuthorName_2" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="lb_Company_2">出版社</asp:Label>
                        <div>
                            <asp:TextBox runat="server" ID="txt_Company_2" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="lb_EditionYear">出版日期</asp:Label>
                        <div>
                            <asp:TextBox runat="server" ID="txt_EditionYear" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="lb_ISBN">ISBN</asp:Label>
                        <div>
                            <asp:TextBox runat="server" ID="txt_ISBN" CssClass="form-control" />
                        </div>
                    </div>
                    <asp:HiddenField ID="BookNo" runat="server" Value="" />
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" type="button" class="btn btn-secondary" data-dismiss="modal" Text="Close" />
                    <asp:Button runat="server" ID="btn_Save" type="button" class="btn btn-primary" Text="修改" OnClick="Save" Visible="false" />
                    <asp:Button runat="server" ID="btn_Insert" type="button" class="btn btn-primary" Text="新增" OnClick="Insert" Visible="false" />
                </div>
            </div>
        </div>
    </div>

    <div id="myModal2" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 500px;height: 700px;">
                <div class="modal-header">
                    <h5 class="modal-title">預定書籍是否借出</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label2">書籍名稱</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_BookName_3"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label3">類別</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_Category_1"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label4">存放地點</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_Location_1"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label5">作者</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_AuthorName_1"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label6">出版社</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_Company_1"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label8">借閱者</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_Account_1"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label7">預定日期</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_deal_date_1"></asp:Label>
                        </div>
                    </div>
                    <asp:HiddenField ID="hf_BookNo" runat="server" Value="" />
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" type="button" class="btn btn-secondary" data-dismiss="modal" Text="Close" />
                    <asp:Button runat="server" ID="btn_disagree" type="button" class="btn btn-primary" Text="不同意" OnClick="Disagree" />
                    <asp:Button runat="server" ID="btn_accept" type="button" class="btn btn-primary" Text="同意" OnClick="Accept" />
                </div>
            </div>
        </div>
    </div>

    <div id="myModal3" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 500px;height: 400px;">
                <div class="modal-header">
                    <h5 class="modal-title">預借書籍</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label9">書籍名稱</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_BookName_1"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label15">預借日期</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_Deal_Date_2"></asp:Label>
                        </div>
                    </div>
                    <asp:HiddenField ID="hf_bookno_2" runat="server" Value="" />
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" type="button" class="btn btn-secondary" data-dismiss="modal" Text="Close" />
                    <asp:Button runat="server" ID="btn_Reserve" type="button" class="btn btn-primary" Text="預借" OnClick="Reserve" />
                </div>
            </div>
        </div>
    </div> 

    <div id="myModal4" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content"  style="width: 500px;height: 400px;">
                <div class="modal-header">
                    <h5 class="modal-title">歸還書籍</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label10">書籍名稱</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_BookName_4"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label12">歸還日期</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_DealDate"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label14">評分</asp:Label>
                        <div>
                            <asp:DropDownList ID="ddl_star" runat="server" AutoPostBack="false">
                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <asp:HiddenField ID="hf_BookNo_3" runat="server" Value="" />
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" type="button" class="btn btn-secondary" data-dismiss="modal" Text="Close" />
                    <asp:Button runat="server" ID="btn_Return" type="button" class="btn btn-primary" Text="歸還" OnClick="btn_Return_click" />
                </div>
            </div>
        </div>
    </div>

    
    <div id="myModal5" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 500px;height: 700px;">
                <div class="modal-header">
                    <h5 class="modal-title">書籍資料</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label11">書籍名稱</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_BookName_5"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label13">類別</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_Category_3"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label16">存放地點</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_Location_3"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label17">作者</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_AuthorName_3"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label18">出版社</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_Company_3"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label19">出版日期</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_EditionYear_2"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="Label20">ISBN</asp:Label>
                        <div>
                            <asp:Label runat="server" ID="lb_ISBN_2"></asp:Label>
                        </div>
                    </div>
                    <asp:HiddenField ID="hf_BookNo_4" runat="server" Value="" />
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" type="button" class="btn btn-secondary" data-dismiss="modal" Text="Close" />
                    <asp:Button runat="server" ID="btn_Delete" type="button" class="btn btn-primary" Text="刪除" OnClick="DeleteData"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

