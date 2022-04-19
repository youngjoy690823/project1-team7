<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site4.Master" CodeBehind="Maintenance.aspx.cs" Inherits="DB_Project.Account.Maintenance" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal">
                <div class="form-group">
                    <asp:Label runat="server" ID="lb_BookName" CssClass="col-md-2 control-label">書籍名稱</asp:Label>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txt_BookName" CssClass="form-control" />
                    </div>
                    <asp:Label runat="server" ID="lb_Class" CssClass="col-md-2 control-label">類別</asp:Label>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txt_Class" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="lb_Location" CssClass="col-md-2 control-label">存放地點</asp:Label>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txt_Location" CssClass="form-control" />
                    </div>
                    <asp:Label runat="server" ID="lb_AuthorName" CssClass="col-md-2 control-label">作者</asp:Label>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txt_AuthorName" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" ID="lb_Company" CssClass="col-md-2 control-label">出版社</asp:Label>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txt_Company" CssClass="form-control" />
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" type="button" class="btn btn-secondary" data-dismiss="modal" Text="Close" />
                    <asp:Button runat="server" type="button" class="btn btn-primary" Text="Save changes" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>


