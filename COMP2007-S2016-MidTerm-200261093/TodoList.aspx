<%@ Page Title="Todo List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP2007_S2016_MidTerm_200261093.TodoList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h1>Todo List</h1>
                <asp:GridView runat="server" CssClass="table table-bordered table-striped table-hover" ID="TodoGridview" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="TodoName" HeaderText="Todo" Visible="true" />
                        <asp:BoundField DataField="TodoNotes" HeaderText="Todo" Visible="true" />
                        <asp:BoundField DataField="Completed" HeaderText="Todo" Visible="true" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>


</asp:Content>
