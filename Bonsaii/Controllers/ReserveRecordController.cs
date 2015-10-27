using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bonsaii.Models;

namespace Bonsaii.Controllers
{
    public class ReserveRecordController : BaseController
    {
        // GET: ReserveRecord
        public ActionResult Index()
        {
            return View(db.ReserveRecords.ToList());
        }

        // GET: ReserveRecord/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReserveRecord reserveRecord = db.ReserveRecords.Find(id);
            if (reserveRecord == null)
            {
                return HttpNotFound();
            }
            return View(reserveRecord);
        }

        // GET: ReserveRecord/Create
        public ActionResult Create()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "员工档案表", Value = "Staffs" });
            items.Add(new SelectListItem { Text = "部门信息表", Value = "Departments" });
            ViewBag.List = items;
            return View();
        }

        // POST: ReserveRecord/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TableName,RecordName,Description,Status")] ReserveRecord reserveRecord)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "员工档案表", Value = "Staffs" });
            items.Add(new SelectListItem { Text = "部门信息表", Value = "Departments" });
            ViewBag.List = items;

            if (ModelState.IsValid)
            {
                db.ReserveRecords.Add(reserveRecord);
                /*状态设置为有效*/
                reserveRecord.Status = "true";
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reserveRecord);
        }

        // GET: ReserveRecord/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReserveRecord reserveRecord = db.ReserveRecords.Find(id);
            if (reserveRecord == null)
            {
                return HttpNotFound();
            }
            return View(reserveRecord);
        }

        // POST: ReserveRecord/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TableName,RecordName,Description,Status")] ReserveRecord reserveRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reserveRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reserveRecord);
        }

        // GET: ReserveRecord/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReserveRecord reserveRecord = db.ReserveRecords.Find(id);
            if (reserveRecord == null)
            {
                return HttpNotFound();
            }
            return View(reserveRecord);
        }

        // POST: ReserveRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReserveRecord reserveRecord = db.ReserveRecords.Find(id);
            db.ReserveRecords.Remove(reserveRecord);
            db.SaveChanges();
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
