using CsvHelper;
using CsvHelper.Configuration;
using InsetByCsv.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsetByCsv.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult UploadCSV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadCSV(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                List<Employee> employees = new List<Employee>();
                using (var reader = new StreamReader(file.InputStream))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.Configuration.Delimiter = ",";
                    employees = csv.GetRecords<Employee>().ToList();
                }

                InsertEmployeesIntoDatabase(employees);

                return RedirectToAction("Index"); 
            }

            ViewBag.Message = "Please upload a file.";
            return View();
        }
      
        private void InsertEmployeesIntoDatabase(List<Employee> employees)
        {
            foreach (var employee in employees)
            {
                _db.Employee.Add(employee);
            }
            _db.SaveChanges();
        }

        public ActionResult Index()
        {
            return View();
        }
        
    }
}
