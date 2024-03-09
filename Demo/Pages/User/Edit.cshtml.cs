using Demo.Model; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;

namespace Demo.Pages.User
{
    public class EditModel : PageModel
    {
        public Users user = new Users();
        public string successMsg = String.Empty;
        public string errorMsg = String.Empty;

        private readonly IConfiguration configuration;

        public EditModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void OnGet()
        {
            string idString = Request.Query["ID"];
            if (int.TryParse(idString, out int id))
            {
                try
                {
                    DAL dal = new DAL();
                    user = dal.GetUser(id, configuration);
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                }
            }
            else
            {
                // Handle invalid or missing ID parameter
                errorMsg = "Invalid or missing ID parameter.";
            }
        }

        public void OnPost()
        {
            user.ID = Request.Form["hiddenId"];
            user.Student_name = Request.Form["Student_name"];
            user.Student_uni = Request.Form["Student_uni"];
            user.Uni_Id = Request.Form["Uni_Id"];

            if (user.Student_name.Length == 0 || user.Student_uni.Length == 0 || user.Uni_Id.Length == 0)
            {
                errorMsg = "All fields are needed";
                return;
            }
            try
            {
                DAL dal = new DAL();
                int i = dal.UpdateUser(user, configuration);
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return;
            }

            user.Student_name = "";
            user.Student_uni = "";
            user.Uni_Id = "";

            successMsg = "User has been updated";
            Response.Redirect("/User/Index");
        }
    }
}
