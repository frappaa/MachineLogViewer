using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MachineLogViewer.DAL;
using MachineLogViewer.Models;
using PagedList;

namespace MachineLogViewer.Controllers
{
    public class MachineController : Controller
    {
        private MachineLogViewerContext db = new MachineLogViewerContext();

        // GET: Machine
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
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

            var machines = from m in db.Machines
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
            return View(machines.ToPagedList(pageNumber, pageSize));
        }

        // GET: Machine/Details/5
        public ActionResult Details(int? id, string sortOrder, Category? category, DateTime? startDate, DateTime? endDate)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = db.Machines.Find(id);
            if (machine == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategorySortParm = String.IsNullOrEmpty(sortOrder) ? "category_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

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

            viewModel.LogEntries = machine.LogEntries
                .Where(le => category == null || le.Category == category)
                .Where(le => startDate == null || le.Time >= startDate)
                .Where(le => endDate == null || le.Time <= endDate).ToList();

            switch (sortOrder)
            {
                case "category_desc":
                    viewModel.LogEntries = viewModel.LogEntries.OrderByDescending(s => s.Category).ToList();
                    break;
                case "Date":
                    viewModel.LogEntries = viewModel.LogEntries.OrderBy(s => s.Time).ToList();
                    break;
                case "date_desc":
                    viewModel.LogEntries = viewModel.LogEntries.OrderByDescending(s => s.Time).ToList();
                    break;
                default:
                    viewModel.LogEntries = viewModel.LogEntries.OrderBy(s => s.Category).ToList();
                    break;
            }

            return View(viewModel);
        }

        // GET: Machine/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Machine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MachineId,Description,ExpiryDate")] Machine machine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Machines.Add(machine);
                    db.SaveChanges();
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = db.Machines.Find(id);
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
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var machineToUpdate = db.Machines.Find(id);
            if (TryUpdateModel(machineToUpdate, "",
               new string[] { "Description", "ExpiryDate" }))
            {
                try
                {
                    db.SaveChanges();

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
            Machine machine = db.Machines.Find(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // POST: Machine/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Machine machine = db.Machines.Find(id);
                db.Machines.Remove(machine);
                db.SaveChanges();
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
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
