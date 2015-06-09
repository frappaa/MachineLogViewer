using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
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
            ViewBag.CodeSortParm = String.IsNullOrEmpty(sortOrder) ? "code_desc" : "";
            ViewBag.DescrSortParm = sortOrder == "Descr" ? "descr_desc" : "Descr";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.UserSortParm = sortOrder == "User" ? "user_desc" : "User";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var currentUser = await _userManager.FindByIdAsync(User.Identity.GetUserId());

            var isAdmin = User.IsInRole("admin");

            var machines = from m in _db.Machines
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                machines = machines.Where(s => s.Code.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "code_desc":
                    machines = machines.OrderByDescending(s => s.Code);
                    break;
                case "Date":
                    machines = machines.OrderBy(s => s.ExpiryDate);
                    break;
                case "date_desc":
                    machines = machines.OrderByDescending(s => s.ExpiryDate);
                    break;
                case "Descr":
                    machines = machines.OrderBy(s => s.Description);
                    break;
                case "descr_desc":
                    machines = machines.OrderByDescending(s => s.Description);
                    break;
                case "User":
                    machines = machines.OrderBy(s => s.User.UserName);
                    break;
                case "user_desc":
                    machines = machines.OrderByDescending(s => s.User.UserName);
                    break;
                default:
                    machines = machines.OrderBy(s => s.Code);
                    break;
            }
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            var filteredMachines = machines.ToList();
            filteredMachines = filteredMachines.Where(m => isAdmin || (m.User != null && m.User.Id == currentUser.Id && m.ExpiryDate >= DateTime.Today)).ToList();
            return View(filteredMachines.ToPagedList(pageNumber, pageSize));
        }

        // GET: Machine
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> IndexByUser(string sortOrder, int? page, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            ViewBag.Title = string.Format("Machines for user {0}", user.UserName);
            ViewBag.UserId = user.Id;
            ViewBag.UserName = user.UserName;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.CodeSortParm = String.IsNullOrEmpty(sortOrder) ? "code_desc" : "";
            ViewBag.DescrSortParm = sortOrder == "Descr" ? "descr_desc" : "Descr";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var machines = from m in _db.Machines
                           select m;

            switch (sortOrder)
            {
                case "code_desc":
                    machines = machines.OrderByDescending(s => s.Code);
                    break;
                case "Date":
                    machines = machines.OrderBy(s => s.ExpiryDate);
                    break;
                case "date_desc":
                    machines = machines.OrderByDescending(s => s.ExpiryDate);
                    break;
                case "Descr":
                    machines = machines.OrderBy(s => s.Description);
                    break;
                case "descr_desc":
                    machines = machines.OrderByDescending(s => s.Description);
                    break;
                default:
                    machines = machines.OrderBy(s => s.Code);
                    break;
            }
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            var filteredMachines = machines.ToList();
            filteredMachines = filteredMachines.Where(m => m.User != null && m.User.Id == userId).ToList();
            return View(filteredMachines.ToPagedList(pageNumber, pageSize));
        }

        public async Task<ActionResult> Takings(int? id, string sortOrder, DateTime? startDate, DateTime? endDate,
            int? page)
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

            ViewBag.MachineCode = machine.Code;
            ViewBag.MachineExpiryDate = machine.ExpiryDate;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            var currentUser = await _userManager.FindByIdAsync(User.Identity.GetUserId());

            var isAdmin = User.IsInRole("admin");

            if (!isAdmin && (machine.User.Id != currentUser.Id || machine.ExpiryDate < DateTime.Today))
            {
                return new HttpUnauthorizedResult();
            }

            var takings = machine.Takings
                .Where(t => (startDate == null || t.Date >= startDate) && (endDate == null || t.Date <= endDate))
                .OrderBy(t => t.Date);

            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return View(takings.ToPagedList(pageNumber, pageSize));
        }

        public async Task<ActionResult> Log(int? id, string sortOrder, Category? category, DateTime? startDate, DateTime? endDate, int? page)
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

            var currentUser = await _userManager.FindByIdAsync(User.Identity.GetUserId());

            var isAdmin = User.IsInRole("admin");

            if (!isAdmin && (machine.User.Id != currentUser.Id || machine.ExpiryDate < DateTime.Today))
            {
                return new HttpUnauthorizedResult();
            }

            ViewBag.CategorySortParm = sortOrder == "category" ? "category_desc" : "category";
            ViewBag.EventDateSortParm = string.IsNullOrEmpty(sortOrder) ? "eventdate_desc" : "";
            ViewBag.DescrSortParm = sortOrder == "descr" ? "descr_desc" : "descr";
            ViewBag.ReceivedDateSortParm = sortOrder == "receiveddate" ? "receiveddate_desc" : "receiveddate";

            List<SelectListItem> items = new List<SelectListItem>();
            
            items.Add(new SelectListItem { Text = "", Value = "", Selected = category == null });
            
            foreach (var value in Enum.GetValues(typeof(Category)))
            {
                items.Add(new SelectListItem { Text = value.ToString(), Value = ((int)value).ToString(), Selected = category.HasValue && (Category)value == category.Value });   
            }

            ViewBag.Category = items;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            LogViewModel viewModel = new LogViewModel
            {
                MachineId = machine.MachineId,
                Code = machine.Code,
                ExpiryDate = machine.ExpiryDate
            };

            var logEntries = machine.LogEntries
                .Where(le =>
                    (category == null || le.Category == category) &&
                    (startDate == null || le.EventTime >= startDate) &&
                    (endDate == null || le.EventTime <= endDate));

            switch (sortOrder)
            {
                case "descr_desc":
                    logEntries = logEntries.OrderByDescending(s => s.Description).ToList();
                    break;
                case "descr":
                    logEntries = logEntries.OrderBy(s => s.Description).ToList();
                    break;
                case "category_desc":
                    logEntries = logEntries.OrderByDescending(s => s.Category).ToList();
                    break;
                case "category":
                    logEntries = logEntries.OrderBy(s => s.Category).ToList();
                    break;
                case "receiveddate_desc":
                    logEntries = logEntries.OrderByDescending(s => s.ReceivedTime).ToList();
                    break;
                case "receiveddate":
                    logEntries = logEntries.OrderBy(s => s.ReceivedTime).ToList();
                    break;
                case "eventdate_desc":
                    logEntries = logEntries.OrderByDescending(s => s.EventTime).ToList();
                    break;
                default:
                    logEntries = logEntries.OrderBy(s => s.EventTime).ToList();
                    break;
            }

            int pageSize = 30;
            int pageNumber = (page ?? 1);
            viewModel.LogEntries = logEntries.ToPagedList(pageNumber, pageSize);
            return View(viewModel);
        }

        // GET: Machine/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            var viewModel = new EditMachineViewModel();
            viewModel.MachineId = 0;
            viewModel.ExpiryDate = DateTime.Today.AddYears(1);
            viewModel.UserList = GetSelectableUsers(null);
            return View(viewModel);
        }

        // POST: Machine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(EditMachineViewModel viewModel)
        {
            try
            {
                Machine machine = new Machine();
                machine.Code = viewModel.Code.ToUpper();
                machine.Description = viewModel.Description;
                machine.ExpiryDate = viewModel.ExpiryDate;
                machine.User = await _userManager.FindByIdAsync(viewModel.UserId);

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
            return View(viewModel);
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

            var viewModel = GetEditMachineViewModel(machine);

            var items = GetSelectableUsers(machine);

            viewModel.UserList = items;

            return View(viewModel);
        }

        private static EditMachineViewModel GetEditMachineViewModel(Machine machine)
        {
            var viewModel = new EditMachineViewModel
            {
                MachineId = machine.MachineId,
                Code = machine.Code,
                Description = machine.Description,
                ExpiryDate = machine.ExpiryDate,
                UserId = machine.User != null ? machine.User.Id : ""
            };
            return viewModel;
        }

        public ActionResult EditDescription(int? id)
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

            if (machine.ExpiryDate < DateTime.Today)
            {
                return new HttpUnauthorizedResult(); 
            }

            var viewModel = GetEditMachineViewModel(machine);

            return View(viewModel);
        }

        private List<SelectListItem> GetSelectableUsers(Machine machine)
        {
            var users = _userManager.Users.OrderBy(u => u.UserName).ToList();

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem {Text = "", Value = "", Selected = machine == null || machine.User == null});

            foreach (var user in users)
            {
                items.Add(new SelectListItem
                {
                    Text = user.UserName,
                    Value = user.Id,
                    Selected = (machine != null && machine.User != null && machine.User.Id == user.Id)
                });
            }
            return items;
        }

        // POST: Machine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> EditPost(EditMachineViewModel viewModel)
        {
            try
            {
                var machineToUpdate = _db.Machines.Find(viewModel.MachineId);
                machineToUpdate.Code = viewModel.Code.ToUpper();
                machineToUpdate.Description = viewModel.Description;
                machineToUpdate.ExpiryDate = viewModel.ExpiryDate;
                if (string.IsNullOrEmpty(viewModel.UserId))
                {
                    machineToUpdate.User = null;
                    machineToUpdate.UserId = null;
                }
                else
                {
                    machineToUpdate.User = await _userManager.FindByIdAsync(viewModel.UserId);
                    machineToUpdate.UserId = viewModel.UserId;
                }
                _db.Entry(machineToUpdate).State = System.Data.Entity.EntityState.Modified;
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditDescription(EditMachineViewModel viewModel)
        {
            var machineToUpdate = _db.Machines.Find(viewModel.MachineId);
            if (machineToUpdate.User == null || machineToUpdate.User.Id != User.Identity.GetUserId() || machineToUpdate.ExpiryDate < DateTime.Today)
            {
                return new HttpUnauthorizedResult();
            }

            try
            {
                machineToUpdate.Description = viewModel.Description;
                
                _db.Entry(machineToUpdate).State = System.Data.Entity.EntityState.Modified;
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(viewModel);
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

        [HttpPost]
        [Authorize(Roles = "admin")]
        public JsonResult DoesMachineCodeExist(string code, int MachineId)
        {
            Machine machine = _db.Machines.SingleOrDefault(m => m.MachineId != MachineId && m.Code.ToUpper() == code.ToUpper());

            return Json(machine == null);
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
