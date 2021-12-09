using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult GetEmployees()
        {
            return View("Index", EmployeeStore.FetchEmployees());
        }

        public ActionResult GetEmployee(int id)
        {
            Employee emp = EmployeeStore.EmployeeList.FirstOrDefault(x => x.EmpId == id);
            return View("EmployeeDetail", emp);
        }

        [HttpPost]
        public ActionResult SetEmployee(Employee emp)
        {
            Employee employee = EmployeeStore.EmployeeList.FirstOrDefault(x => x.EmpId == emp.EmpId);
            employee.Name = emp.Name;
            employee.Salary = emp.Salary;
            return View("EmployeeDetail", emp);
        }

        public ActionResult AddEmployee()
        {
            return View("AddEmployee");
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee emp)
        {
            bool IsExist = EmployeeStore.EmployeeList.Any(x => x.Name == emp.Name);
            if (IsExist)
            {
                TempData["test"] = "Name is Already Exist";
                ViewBag.Message = "Name is Already Exist";

                TempData["msg"] = "<script>alert('Change succesfully');</script>";
                ViewBag.Javascript = "<script language='javascript' type='text/javascript'>alert('Data Already Exists');</script>";
                return Json(new { error = true, message = "Name is Already Exist" });
            }
            else
            {
                EmployeeStore.EmployeeList.Add(new Employee()
                {
                    EmpId = EmployeeStore.EmployeeList.Count == 0 ? 1 : EmployeeStore.EmployeeList.Max(x => x.EmpId) + 1,
                    Name = emp.Name,
                    Salary = emp.Salary,

                });
                return Json(new { error = true, message = "Employee saved successfully" });
            }

            return View("EmployeeDetail", emp);
        }

        [HttpPost]
        public void DeleteEmployee(int[] empIds)
        {
            EmployeeStore.EmployeeList.RemoveAll(x => empIds.Contains(x.EmpId));
        }
    }
}