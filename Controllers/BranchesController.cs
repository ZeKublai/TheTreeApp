using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TreeWebApp.Models;

namespace TreeWebApp.Controllers
{
    /// <summary>
    /// Controller for managing branches in the application.
    /// </summary>
    public class BranchesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Builds a SelectList of employees and sets it in ViewBag.
        /// </summary>
        private void BuildEmployeeSelectList()
        {
            var employees = db.Employees.ToList();
            var employeeList = new SelectList(employees.Select(e => new
            {
                Id = e.Id,
                FullName = e.FullName
            }), "Id", "FullName");
            ViewBag.EmployeeList = employeeList;
        }

        /// <summary>
        /// Displays a list of branches.
        /// </summary>
        /// <returns>The branch list view.</returns>
        public ActionResult Index()
        {
            var branches = db.Branches.ToList();
            return View(branches);
        }

        /// <summary>
        /// Displays details of a specific branch.
        /// </summary>
        /// <param name="id">The ID of the branch to view.</param>
        /// <returns>The branch details view.</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Branch branch = db.Branches.Find(id);

            if (branch == null)
            {
                return HttpNotFound();
            }

            return View(branch);
        }

        /// <summary>
        /// Displays the create branch form.
        /// </summary>
        /// <returns>The branch creation form view.</returns>
        public ActionResult Create()
        {
            BuildEmployeeSelectList();
            return View();
        }

        /// <summary>
        /// Handles the creation of a new branch.
        /// </summary>
        /// <param name="branch">The branch data to be created.</param>
        /// <returns>The view with the branch creation result.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,Address,Barangay,City,PermitNo,ManagerId,DateOpened,IsActive")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the selected manager based on ManagerId
                branch.Manager = db.Employees.Find(branch.ManagerId);

                // Save the branch to the database
                db.Branches.Add(branch);
                db.SaveChanges();

                // Show a custom alert message
                TempData["SuccessMessage"] = "Branch Added Successfully!";
            }

            BuildEmployeeSelectList();
            return View(branch);
        }

        /// <summary>
        /// Displays the branch edit form for a specific branch.
        /// </summary>
        /// <param name="id">The ID of the branch to edit.</param>
        /// <returns>The branch edit view.</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Branch branch = db.Branches.Find(id);

            if (branch == null)
            {
                return HttpNotFound();
            }

            BuildEmployeeSelectList();
            return View(branch);
        }

        /// <summary>
        /// Handles the HTTP POST request to edit a branch.
        /// </summary>
        /// <param name="branch">The branch object with updated information.</param>
        /// <returns>Redirects to the branch edit view with a success message if the update is successful.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,Address,Barangay,City,PermitNo,ManagerId,DateOpened,IsActive")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the selected manager based on ManagerId
                branch.Manager = db.Employees.Find(branch.ManagerId);

                // Mark the branch as modified and save changes to the database
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();

                // Show a custom alert message
                TempData["SuccessMessage"] = "Branch Edited Successfully!";
            }

            BuildEmployeeSelectList();
            return View(branch);
        }

        /// <summary>
        /// Displays a confirmation page for deleting a branch.
        /// </summary>
        /// <param name="id">The ID of the branch to delete.</param>
        /// <returns>The delete confirmation view.</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Branch branch = db.Branches.Find(id);

            if (branch == null)
            {
                return HttpNotFound();
            }

            return View(branch);
        }

        /// <summary>
        /// Handles the deletion of a branch and redirects to the index page.
        /// </summary>
        /// <param name="id">The ID of the branch to delete.</param>
        /// <returns>Redirects to the index page after successful deletion.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Branch branch = db.Branches.Find(id);
            db.Branches.Remove(branch);
            db.SaveChanges();
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
