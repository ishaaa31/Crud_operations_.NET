using Demo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;

namespace Demo.Pages.User
{
    public class CreateModel : PageModel
    {
        public Users user = new Users();
        public string successMessage = String.Empty;
        public string errorMessage = String.Empty;

        private readonly IConfiguration configuration;
        public CreateModel(IConfiguration configuration)
        {
            this .configuration = configuration;
        }
        public void OnGet()
        {
        }
        public void OnPost()
        { 
            user.ID = Request.Form["ID"];
            user.Student_name = Request.Form["Student_name"];
            user.Student_uni = Request.Form["Student_uni"];
            user.Uni_Id = Request.Form["Uni_Id"];

            if (user.ID.Length==0 || user.Student_name.Length==0 || user.Student_uni.Length==0 || user.Uni_Id.Length==0)
            {
                errorMessage = "All fields are required.";
                return;
            }
            try
            {
                DAL dal = new();
                int i = dal.AddUser(user, configuration);
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            user.ID = "";
            user.Student_name = "";
            user.Student_uni = "";
            user.Uni_Id = "";

            successMessage = "User has been added.";
            Response.Redirect("/User/Index");
        }
    }
}
