using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//using statements required for EF DB access
using COMP2007_S2016_MidTerm_200261093.Models;
using System.Web.ModelBinding;

namespace COMP2007_S2016_MidTerm_200261093
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            //redirect back to TodoList Page
            Response.Redirect("~/TodoList.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            //use EF to connect to the server
            using (TodoConnection db = new TodoConnection()) {
                //use the Todo model to create a new Todo object and also save a record
                Todo newTodo = new Todo();

                // add data to the new Todo Record
                newTodo.TodoName = TodoNameTextBox.Text;
                newTodo.TodoNotes = TodoNoteTextBox.Text;

                db.Todos.Add(newTodo);

                db.SaveChanges();

                Response.Redirect("~/TodoList.aspx");
            }
        }
    }
}