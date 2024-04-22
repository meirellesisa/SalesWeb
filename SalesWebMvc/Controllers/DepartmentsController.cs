using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Department> ListDepartments = new List<Department>();
            ListDepartments.Add(new Department { Id = 1, Name = "Eletronics" });
            ListDepartments.Add(new Department { Id = 2, Name = "Fashion" });

            //enviando a lista de dados para a view 
            return View(ListDepartments);
        }
    }
}
