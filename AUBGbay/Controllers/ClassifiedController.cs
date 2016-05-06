using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AUBGbay.Models;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Threading.Tasks;
using PagedList;
using System.Configuration;
using Microsoft.AspNet.Identity.Owin;

namespace AUBGbay.Controllers
{
    public class ClassifiedController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Classified
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {

            ViewBag.Category = db.Categories.ToList();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var classifieds = db.Classifieds.Include(c => c.Category).Include(c => c.User).Include(c=>c.Images);

            if (!String.IsNullOrEmpty(searchString))
            {
                classifieds = classifieds.Where(s => s.Title.Contains(searchString)
                                       || s.ShortCaption.Contains(searchString) || s.Description.Contains(searchString));
            }
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(classifieds.OrderBy(i => i.DateCreated).ToPagedList(pageNumber, pageSize));

        }

        public ActionResult Manage(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CategorySortParm = sortOrder == "Category" ? "category_desc" : "Category";

            string loggedInUserId = User.Identity.GetUserId();
            List<Classified> classifieds = (from r in db.Classifieds where r.UserId == loggedInUserId select r).ToList();
            List<Classified> adminlist = (from d in db.Classifieds select d).ToList();

            if (User.IsInRole("admin"))
            {
                ViewBag.classifiedCount = adminlist.Count;
                switch (sortOrder)
                {
                    case "name_desc":
                        adminlist = adminlist.OrderByDescending(s => s.User.UserName).ToList();
                        break;
                    case "Date":
                        adminlist = adminlist.OrderBy(s => s.DateCreated).ToList();
                        break;
                    case "date_desc":
                        adminlist = adminlist.OrderByDescending(s => s.DateCreated).ToList();
                        break;
                    case "category_desc":
                        adminlist = adminlist.OrderByDescending(s => s.CategoryID).ToList();
                        break;
                    case "Category":
                        adminlist = adminlist.OrderBy(s => s.CategoryID).ToList();
                        break;
                    default:
                        adminlist = adminlist.OrderBy(s => s.User.UserName).ToList();
                        break;
                }
                return View(adminlist);
            }
            else
            {
                ViewBag.classifiedCount = classifieds.Count;
                return View(classifieds);
            }



        }

        public ActionResult Category(int? id, int? page)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            ViewBag.Category = db.Categories.ToList();
            if (id == 1)
            {
                List<Classified> classifieds = (from r in db.Classifieds where r.CategoryID == 1 select r).ToList();
                return View("Index", classifieds.ToPagedList(pageNumber, pageSize));
            }
            else if (id == 2)
            {
                List<Classified> classifieds = (from r in db.Classifieds where r.CategoryID == 2 select r).ToList();
                return View("Index", classifieds.ToPagedList(pageNumber, pageSize));
            }
            else if (id == 3)
            {
                List<Classified> classifieds = (from r in db.Classifieds where r.CategoryID == 3 select r).ToList();
                return View("Index", classifieds.ToPagedList(pageNumber, pageSize));
            }
            else if (id == 4)
            {
                List<Classified> classifieds = (from r in db.Classifieds where r.CategoryID == 4 select r).ToList();
                return View("Index", classifieds.ToPagedList(pageNumber, pageSize));
            }
            else
                return View("Index");

        }

        // GET: Classified/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classified classified = db.Classifieds.Find(id);
            if (classified == null)
            {
                return HttpNotFound();
            }
            return View(classified);
        }

        // GET: Classified/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return View();
        }

        // POST: Classified/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassifiedID,UserId,CategoryID,Title,ShortCaption,Description,DateCreated,Price")] Classified classified, List<HttpPostedFileBase> images)
        {
            if (ModelState.IsValid)
            {
                List<Image> allimages = new List<Image>();

                foreach (var upload in images)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        var image = new Image
                        {
                            ImageName = System.IO.Path.GetFileName(upload.FileName),
                            //FileType = FileType.Avatar,
                            //ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            image.ImageContent = reader.ReadBytes(upload.ContentLength);
                        }
                        allimages.Add(image);      
                    }
                    else
                    {
                        string filePath = Server.MapPath(Url.Content("~/Content/images/aubgbay_logo.jpg"));

                        byte[] buffer = System.IO.File.ReadAllBytes(filePath);

                        var aubglogo = new Image
                        {
                            ImageName = "AUBG_LOGO",
                            ImageContent = buffer
                        };
                        allimages.Add(aubglogo);
                    }
                }

                classified.Images =  allimages;

                classified.DateCreated = DateTime.Now;
                classified.UserId = User.Identity.GetUserId();
                db.Classifieds.Add(classified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", classified.CategoryID);
            return View(classified);
        }

        // GET: Classified/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classified classified = db.Classifieds.Find(id);
            if (classified == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", classified.CategoryID);
            return View(classified);
        }

        // POST: Classified/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassifiedID,CategoryID,Title,ShortCaption,Description,Price")] Classified classified)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classified).State = EntityState.Modified;
                db.Entry(classified).Property(x => x.UserId).IsModified = false;
                db.Entry(classified).Property(x => x.DateCreated).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", classified.CategoryID);
            return View(classified);
        }

        // GET: Classified/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classified classified = db.Classifieds.Find(id);
            if (classified == null)
            {
                return HttpNotFound();
            }
            return View(classified);
        }

        // POST: Classified/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Classified classified = db.Classifieds.Find(id);
            db.Classifieds.Remove(classified);
            db.SaveChanges();
            return RedirectToAction("Manage");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        async public Task SendEmail(string resEmail, string subject, string message)
        {
            try
            {
                ApplicationUser user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
                string currUserEmail = user.Email;
                string firstname = user.FirstName;
                string lastname = user.LastName;
                string fullname = firstname + " " + lastname;

                var body = "<p>Concerning Item: \"{0}\"</p> <p>Email from: {1} ({2}) </p> <p>Message:</p><p>{3}</p>";

                MailMessage myMessage = new MailMessage();
                myMessage.To.Add(new MailAddress(resEmail));
                myMessage.From = new MailAddress(currUserEmail, "AUBGbay");
                myMessage.Sender = new MailAddress(currUserEmail, "AUBGbay");
                myMessage.ReplyToList.Add(currUserEmail);
                myMessage.Subject = subject + "Request";
                myMessage.Body = string.Format(body, subject, fullname, currUserEmail, message);
                myMessage.IsBodyHtml = true;

                ViewBag.success = "Message Sent Successfully";

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    smtp.Host = ConfigurationManager.AppSettings["GmailHost"];
                    smtp.Port = Int32.Parse(ConfigurationManager.AppSettings["GmailPort"]);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["GmailUserName"], ConfigurationManager.AppSettings["GmailPassword"]);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    await smtp.SendMailAsync(myMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
