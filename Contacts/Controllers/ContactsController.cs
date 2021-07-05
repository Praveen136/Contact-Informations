using Contacts.ListRepo;
using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contacts.Controllers
{
    public class ContactsController : Controller
    {
        // GET: Employee/GetAllEmpDetails    
        public ActionResult GetAllContactDetails()
        {

            ContRepo contRepo = new ContRepo();
            ModelState.Clear();
            return View(contRepo.GetAllContacts());
        }
        // GET: Employee/AddEmployee    
        public ActionResult AddContact()
        {
            return View();
        }

        // POST: Employee/AddEmployee    
        [HttpPost]
        public ActionResult AddContact(ContModel cont)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContRepo contRepo = new ContRepo();

                    if (contRepo.AddContact(cont))
                    {
                        ViewBag.Message = "Contact details added successfully";
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/EditEmpDetails/5    
        public ActionResult EditContactDetails(int id)
        {
            ContRepo EmpRepo = new ContRepo();



            return View(EmpRepo.GetAllContacts().Find(Cont => Cont.Id == id));

        }

        // POST: Employee/EditEmpDetails/5    
        [HttpPost]

        public ActionResult EditContactDetails(int id, ContModel obj)
        {
            try
            {
                ContRepo contRepo = new ContRepo();

                contRepo.UpdateContact(obj);
                return RedirectToAction("GetAllContactDetails");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/DeleteEmp/5    
        public ActionResult DeleteContact(int id)
        {
            try
            {
                ContRepo contRepo = new ContRepo();
                if (contRepo.DeleteContact(id))
                {
                    ViewBag.AlertMsg = "Contact deleted successfully";

                }
                return RedirectToAction("GetAllContactDetails");

            }
            catch
            {
                return View();
            }
        }
    }
}
