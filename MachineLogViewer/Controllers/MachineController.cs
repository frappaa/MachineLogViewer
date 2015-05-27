﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MachineLogViewer.DAL;
using MachineLogViewer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace MachineLogViewer.Controllers
{
    [Authorize]
    public class MachineController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;

        public MachineController()
        {
            _db = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
        }

        // GET: Machine
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var currentUser = await _userManager.FindByIdAsync
                                                 (User.Identity.GetUserId());

            var isAdmin = User.IsInRole("admin");

            var machines = from m in _db.Machines
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                machines = machines.Where(s => s.Description.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    machines = machines.OrderByDescending(s => s.Description);
                    break;
                case "Date":
                    machines = machines.OrderBy(s => s.ExpiryDate);
                    break;
                case "date_desc":
                    machines = machines.OrderByDescending(s => s.ExpiryDate);
                    break;
                default:
                    machines = machines.OrderBy(s => s.Description);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var filteredMachines = machines.ToList();
            filteredMachines.Where(m => isAdmin || m.User.Id == currentUser.Id);
            return View(filteredMachines.ToPagedList(pageNumber, pageSize));
        }

        // GET: Machine/Details/5
        public async Task<ActionResult> Details(int? id, string sortOrder, Category? category, DateTime? startDate, DateTime? endDate, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = _db.Machines.Find(id);

            if (machine == null)
            {
                return HttpNotFound();
            }

            var currentUser = await _userManager.FindByIdAsync
                                                 (User.Identity.GetUserId());

            var isAdmin = User.IsInRole("admin");

            if (!isAdmin && machine.User.Id != currentUser.Id)
            {
                return new HttpUnauthorizedResult();
            }

            ViewBag.CategorySortParm = String.IsNullOrEmpty(sortOrder) ? "category_desc" : "category";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "";

            List<SelectListItem> items = new List<SelectListItem>();
            
            items.Add(new SelectListItem { Text = "", Value = "", Selected = category == null });
            
            foreach (var value in Enum.GetValues(typeof(Category)))
            {
                items.Add(new SelectListItem { Text = value.ToString(), Value = ((int)value).ToString(), Selected = category.HasValue && (Category)value == category.Value });   
            }

            ViewBag.Category = items;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            MachineDetailsViewModel viewModel = new MachineDetailsViewModel
            {
                MachineId = machine.MachineId,
                Description = machine.Description,
                ExpiryDate = machine.ExpiryDate
            };

            var logEntries = machine.LogEntries
                .Where(le => category == null || le.Category == category)
                .Where(le => startDate == null || le.Time >= startDate)
                .Where(le => endDate == null || le.Time <= endDate);

            switch (sortOrder)
            {
                case "category_desc":
                    logEntries = logEntries.OrderByDescending(s => s.Category).ToList();
                    break;
                case "category":
                    logEntries = logEntries.OrderBy(s => s.Category).ToList();
                    break;
                case "date_desc":
                    logEntries = logEntries.OrderByDescending(s => s.Time).ToList();
                    break;
                default:
                    logEntries = logEntries.OrderBy(s => s.Time).ToList();
                    break;
            }

            int pageSize = 2;
            int pageNumber = (page ?? 1);
            viewModel.LogEntries = logEntries.ToPagedList(pageNumber, pageSize);
            return View(viewModel);
        }

        // GET: Machine/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Machine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "MachineId,Description,ExpiryDate")] Machine machine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Machines.Add(machine);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(machine);
        }

        // GET: Machine/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = _db.Machines.Find(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // POST: Machine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var machineToUpdate = _db.Machines.Find(id);
            if (TryUpdateModel(machineToUpdate, "",
               new string[] { "Description", "ExpiryDate" }))
            {
                try
                {
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(machineToUpdate);
        }

        // GET: Machine/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Machine machine = _db.Machines.Find(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // POST: Machine/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                Machine machine = _db.Machines.Find(id);
                _db.Machines.Remove(machine);
                _db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
