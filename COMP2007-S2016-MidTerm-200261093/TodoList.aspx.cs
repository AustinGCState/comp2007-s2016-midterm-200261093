using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statemtents we need to connect to EF DB
using COMP2007_S2016_MidTerm_200261093.Models;
using System.Web.ModelBinding;

namespace COMP2007_S2016_MidTerm_200261093
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //if page is being oaded for the first time, populate to do
            if (!IsPostBack) {
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

                var Todo = (from allTodo in db.Todos select allTodo);

                //bind the result to the Gridview
                TodoGridview.DataSource = Todo.ToList();
                TodoGridview.DataBind();
            }
        }

    }
}