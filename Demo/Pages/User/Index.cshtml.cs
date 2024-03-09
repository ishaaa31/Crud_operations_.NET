using Demo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration configuration;
        public List<Users> listUsers = new List<Users>(); 
        public IndexModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void OnGet()
        {
            DAL dal = new DAL();
            listUsers = dal.GetUsers(configuration);
        }
    }
}
