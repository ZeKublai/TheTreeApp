using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TreeWebApp.Models;

namespace TreeWebApp.Controllers
{
    /// <summary>
    /// Controller for managing employees in the application.
    /// </summary>
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Displays a list of employees.
        /// </summary>
        /// <returns>The employee list view.</returns>
        public ActionResult Index()
        {
            var employees = db.Employees.ToList();
            return View(employees);
        }

        /// <summary>
        /// Displays details of a specific employee.
        /// </summary>
        /// <param name="id">The ID of the employee to view.</param>
        /// <returns>The employee details view.</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        /// <summary>
        /// Displays the employee creation form.
        /// </summary>
        /// <returns>The employee creation form view.</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles the creation of a new employee.
        /// </summary>
        /// <param name="employee">The employee data.</param>
        /// <param name="PhotoFile">The uploaded employee photo file.</param>
        /// <returns>The employee creation form view or a different view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LastName,FirstName,MiddleName,DateHired")] Employee employee, HttpPostedFileBase PhotoFile)
        {
            if (ModelState.IsValid)
            {
                if (PhotoFile != null && PhotoFile.ContentLength > 0)
                {
                    // Read the uploaded file into a byte array
                    using (var binaryReader = new BinaryReader(PhotoFile.InputStream))
                    {
                        employee.PhotoData = binaryReader.ReadBytes(PhotoFile.ContentLength);
                    }

                    // Set the content type
                    employee.PhotoContentType = PhotoFile.ContentType;
                }

                db.Employees.Add(employee);
                db.SaveChanges();

                // Show a custom alert message
                TempData["SuccessMessage"] = "Employee Added Successfully!";
            }

            return View(employee);
        }

        /// <summary>
        /// GET action to display the employee edit view.
        /// </summary>
        /// <param name="id">The ID of the employee to edit.</param>
        /// <returns>The employee edit view.</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        /// <summary>
        /// POST action to save changes after editing an employee.
        /// </summary>
        /// <param name="employee">The edited employee data.</param>
        /// <param name="PhotoFile">The uploaded photo file (optional).</param>
        /// <returns>The view displaying the edited employee.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LastName,FirstName,MiddleName,DateHired")] Employee employee, HttpPostedFileBase PhotoFile)
        {
            if (ModelState.IsValid)
            {
                // Find the existing employee record
                Employee existingEmployee = db.Employees.Find(employee.Id);

                if (existingEmployee != null)
                {
                    // Update the employee's properties from the form data
                    existingEmployee.LastName = employee.LastName;
                    existingEmployee.FirstName = employee.FirstName;
                    existingEmployee.MiddleName = employee.MiddleName;
                    existingEmployee.DateHired = employee.DateHired;

                    if (PhotoFile != null && PhotoFile.ContentLength > 0)
                    {
                        // Read the uploaded file into a byte array
                        using (var binaryReader = new BinaryReader(PhotoFile.InputStream))
                        {
                            existingEmployee.PhotoData = binaryReader.ReadBytes(PhotoFile.ContentLength);
                        }

                        // Set the content type
                        existingEmployee.PhotoContentType = PhotoFile.ContentType;
                    }

                    // Save the changes to the database
                    db.SaveChanges();

                    // Show a custom alert message
                    TempData["SuccessMessage"] = "Employee Edited Successfully!";

                    return View(existingEmployee);
                }
            }

            return View(employee);
        }

        /// <summary>
        /// Displays the delete confirmation view for a specific employee.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>The delete confirmation view.</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        /// <summary>
        /// Deletes a specific employee from the database.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>A redirect to the employee list view.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);

            if (employee != null)
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Disposes of the database context when the controller is disposed.
        /// </summary>
        /// <param name="disposing">True if disposing should be performed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
