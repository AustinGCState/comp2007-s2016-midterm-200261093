using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Austin Cameron
//Midterm S2016
//5 pm to 7:50 pm

// using statemtents we need to connect to EF DB
using COMP2007_S2016_MidTerm_200261093.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

namespace COMP2007_S2016_MidTerm_200261093
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //if page is being oaded for the first time, populate to do
            if (!IsPostBack) {
                Session["SortColumn"] = "TodoID";
                Session["SortDirection"] = "ASC";
                //get todo data
                this.GetTodo();
            }
        }
        
        // * This method gets the student data from the DB
        //method GetTodo
        //returns {void}
        
        protected void GetTodo() {

            //connect to EF
            using (TodoConnection db = new TodoConnection()) {

                string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                var Todo = (from allTodo in db.Todos select allTodo);

                //bind the result to the Gridview
                TodoGridview.DataSource = Todo.AsQueryable().OrderBy(SortString).ToList();
                TodoGridview.DataBind();
            }
        }

        protected void TodoGridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //store what row was clicked
            int selectedRow = e.RowIndex;

            //get the selected TodoID using the grids DataKey Collection
            int TodoId = Convert.ToInt32(TodoGridview.DataKeys[selectedRow].Values["TodoID"]);

            //Use Ef to find select and remove the Todo
            using (TodoConnection db = new TodoConnection()) {

                //create object of the todo class and store the query in a string
                Todo deletedTodo = (from TodoRecords in db.Todos
                                    where TodoRecords.TodoID == TodoId
                                    select TodoRecords).FirstOrDefault();

                db.Todos.Remove(deletedTodo);

                db.SaveChanges();

                this.GetTodo();

            }
        }

        protected void TodoGridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Set the new page number
            TodoGridview.PageIndex = e.NewPageIndex;

            this.GetTodo();
        }

        protected void PageSizeDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            TodoGridview.PageSize = Convert.ToInt32(PageSizeDropdownList.SelectedValue);

            this.GetTodo();
        }

        protected void TodoGridview_Sorting(object sender, GridViewSortEventArgs e)
        {
            Session["SortColumn"] = e.SortDirection;

            this.GetTodo();

            //toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        protected void TodoGridview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack) {
                if (e.Row.RowType == DataControlRowType.Header) {
                    LinkButton linkbutton = new LinkButton();

                    for (int index = 0; index < TodoGridview.Columns.Count-1; index++) {
                        if (TodoGridview.Columns[index].SortExpression == Session["SortColumn"].ToString()) {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = "<i class='fa fa-caret-up fa-lg></i>";

                            }
                            else {
                                linkbutton.Text = "<i class='fa fa-caret-down fa-lg></i>";
                            }  
                        }
                        e.Row.Cells[index].Controls.Add(linkbutton);
                    }
                }

            }
        }
    }
}