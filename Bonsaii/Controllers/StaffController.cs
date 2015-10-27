using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bonsaii.Models;
using PagedList;
namespace Bonsaii.Controllers
{
    public class StaffController : BaseController
    {
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Number" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var Staffs = from s in db.Staffs select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Staffs = Staffs.Where(s => s.StaffNumber.Contains(searchString) || s.BillTypeName.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "Number":
                    Staffs = Staffs.OrderByDescending(s => s.StaffNumber);
                    break;
                default:
                    Staffs = Staffs.OrderBy(s => s.StaffNumber);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(Staffs.ToPagedList(pageNumber, pageSize));//使用ToPagedList方法时，需要引入using PagedList系统集成的分页函数。
        }


        // GET: Staff/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staff/Create
        public ActionResult Create()
        {
            //1.部门信息
            List<SelectListItem> department = db.Departments.ToList().Select(c => new SelectListItem
            {
                Value = c.Name,//保存的值
                Text = c.Name//显示的值
            }).ToList();
            ViewBag.department = department;
            //2.性别信息
            List<SelectListItem> gender = new List<SelectListItem>();
            ///linq多表查询
            var gender1 = from spt in db.StaffParamTypes
                       join sp in db.StaffParams on spt.Id equals sp.StaffParamTypeId
                       where spt.Name == "性别"
                       select new { value = sp.Value };

            foreach (var tt in gender1)
            {
                SelectListItem i = new SelectListItem();
                i.Value = tt.value;
                i.Text = tt.value;
                gender.Add(i);

            }
            ViewBag.gender = gender;
            //3.工种信息
            List<SelectListItem> staff = new List<SelectListItem>();
            ///linq多表查询
            var staff1 = from spt in db.StaffParamTypes
                       join sp in db.StaffParams on spt.Id equals sp.StaffParamTypeId
                         where spt.Name == "工种"
                       select new { value = sp.Value };

            foreach (var tt in staff1)
            {
                SelectListItem i = new SelectListItem();
                i.Value = tt.value;
                i.Text = tt.value;
                staff.Add(i);

            }
            ViewBag.staff = staff;
            //4.国籍信息
            List<SelectListItem> nationality = new List<SelectListItem>();
            ///linq多表查询
            var nationality1 = from spt in db.StaffParamTypes
                       join sp in db.StaffParams on spt.Id equals sp.StaffParamTypeId
                       where spt.Name == "国籍"
                       select new { value = sp.Value};

            foreach (var tt in nationality1)
            {
                SelectListItem i = new SelectListItem();
                i.Value = tt.value;
                i.Text = tt.value;
                nationality.Add(i);

            }
            ViewBag.nationality = nationality;
            //5.籍贯信息
            List<SelectListItem> nativeplace = new List<SelectListItem>();
            ///linq多表查询
            var nativeplace1 = from spt in db.StaffParamTypes
                          join sp in db.StaffParams on spt.Id equals sp.StaffParamTypeId
                          where spt.Name == "籍贯"
                          select new { value = sp.Value };

            foreach (var tt in nativeplace1)
            {
                SelectListItem i = new SelectListItem();
                i.Value = tt.value;
                i.Text = tt.value;
                nativeplace.Add(i);

            }
            ViewBag.nativeplace = nativeplace;
            //6.健康信息
            List<SelectListItem> health = new List<SelectListItem>();
            ///linq多表查询
            var health1 = from spt in db.StaffParamTypes
                               join sp in db.StaffParams on spt.Id equals sp.StaffParamTypeId
                               where spt.Name == "健康状况"
                               select new { value = sp.Value };

            foreach (var tt in health1)
            {
                SelectListItem i = new SelectListItem();
                i.Value = tt.value;
                i.Text = tt.value;
                health.Add(i);

            }
            ViewBag.health = health;
            //7.民族信息
            List<SelectListItem> nation = new List<SelectListItem>();
            ///linq多表查询
            var nation1 = from spt in db.StaffParamTypes
                          join sp in db.StaffParams on spt.Id equals sp.StaffParamTypeId
                          where spt.Name == "民族"
                          select new { value = sp.Value };

            foreach (var tt in nation1)
            {
                SelectListItem i = new SelectListItem();
                i.Value = tt.value;
                i.Text = tt.value;
                nation.Add(i);

            }
            ViewBag.nation = nation;
            //8.学历信息
            List<SelectListItem> background = new List<SelectListItem>();
            ///linq多表查询
            var background1 = from spt in db.StaffParamTypes
                          join sp in db.StaffParams on spt.Id equals sp.StaffParamTypeId
                          where spt.Name == "学历"
                          select new { value = sp.Value };

            foreach (var tt in background1)
            {
                SelectListItem i = new SelectListItem();
                i.Value = tt.value;
                i.Text = tt.value;
                background.Add(i);

            }
            ViewBag.background = background;
            //9.异动信息
            List<SelectListItem> abnormal = new List<SelectListItem>();
            ///linq多表查询
            var abnormal1 = from spt in db.StaffParamTypes
                              join sp in db.StaffParams on spt.Id equals sp.StaffParamTypeId
                              where spt.Name == "异动类型"
                              select new { value = sp.Value };

            foreach (var tt in abnormal1)
            {
                SelectListItem i = new SelectListItem();
                i.Value = tt.value;
                i.Text = tt.value;
                abnormal.Add(i);

            }
            ViewBag.abnormal = abnormal;
            //10.用工性质信息
            List<SelectListItem> workproperty = new List<SelectListItem>();
            ///linq多表查询
            var workproperty1 = from spt in db.StaffParamTypes
                            join sp in db.StaffParams on spt.Id equals sp.StaffParamTypeId
                            where spt.Name == "用工性质"
                            select new { value = sp.Value };

            foreach (var tt in workproperty1)
            {
                SelectListItem i = new SelectListItem();
                i.Value = tt.value;
                i.Text = tt.value;
                workproperty.Add(i);

            }
            ViewBag.workproperty = workproperty;
            //11.员工来源信息
            List<SelectListItem> staffsource = new List<SelectListItem>();
            ///linq多表查询
            var staffsource1 = from spt in db.StaffParamTypes
                        join sp in db.StaffParams on spt.Id equals sp.StaffParamTypeId
                        where spt.Name == "员工来源"
                        select new { value = sp.Value };

            foreach (var tt in staffsource1)
            {
                SelectListItem i = new SelectListItem();
                i.Value = tt.value;
                i.Text = tt.value;
                staffsource.Add(i);

            }
            ViewBag.staffsource = staffsource;
            //12.员工职务信息
            List<SelectListItem> position = new List<SelectListItem>();
            ///linq多表查询
            var position1 = from spt in db.StaffParamTypes
                               join sp in db.StaffParams on spt.Id equals sp.StaffParamTypeId
                               where spt.Name == "员工职务"
                               select new { value = sp.Value };

            foreach (var tt in position1)
            {
                SelectListItem i = new SelectListItem();
                i.Value = tt.value;
                i.Text = tt.value;
                position.Add(i);

            }
            ViewBag.position = position;
            //13.证件类型信息
            List<SelectListItem> identificationtype = new List<SelectListItem>();
            ///linq多表查询
            var identificationtype1 = from spt in db.StaffParamTypes
                            join sp in db.StaffParams on spt.Id equals sp.StaffParamTypeId
                            where spt.Name == "证件类型"
                            select new { value = sp.Value };

            foreach (var tt in identificationtype1)
            {
                SelectListItem i = new SelectListItem();
                i.Value = tt.value;
                i.Text = tt.value;
                identificationtype.Add(i);

            }
            ViewBag.identificationtype = identificationtype;
            //14.学位信息
            List<SelectListItem> degree = new List<SelectListItem>();
            ///linq多表查询
            var degree1 = from spt in db.StaffParamTypes
                                      join sp in db.StaffParams on spt.Id equals sp.StaffParamTypeId
                          where spt.Name == "学位"
                                      select new { value = sp.Value };

            foreach (var tt in degree1)
            {
                SelectListItem i = new SelectListItem();
                i.Value = tt.value;
                i.Text = tt.value;
                degree.Add(i);

            }
            ViewBag.degree = degree;
            //15.婚姻状况信息
            List<SelectListItem> maritalstatus = new List<SelectListItem>();
            ///linq多表查询
            var maritalstatus1 = from spt in db.StaffParamTypes
                          join sp in db.StaffParams on spt.Id equals sp.StaffParamTypeId
                          where spt.Name == "婚姻状况"
                          select new { value = sp.Value };

            foreach (var tt in maritalstatus1)
            {
                SelectListItem i = new SelectListItem();
                i.Value = tt.value;
                i.Text = tt.value;
                maritalstatus.Add(i);

            }
            ViewBag.maritalstatus = maritalstatus;
            //16.专业信息
            List<SelectListItem> major = new List<SelectListItem>();
            ///linq多表查询
            var major1 = from spt in db.StaffParamTypes
                                 join sp in db.StaffParams on spt.Id equals sp.StaffParamTypeId
                                 where spt.Name == "专业"
                                 select new { value = sp.Value };

            foreach (var tt in major1)
            {
                SelectListItem i = new SelectListItem();
                i.Value = tt.value;
                i.Text = tt.value;
                major.Add(i);

            }
            ViewBag.major = major;
                //List<SelectListItem> item2 = db.Healths.ToList().Select(c => new SelectListItem
                //{
                //    Value = c.HealthCondition,//保存的值
                //    Text = c.HealthCondition//显示的值
                //}).ToList();

                //List<SelectListItem> item3 = db.Nations.ToList().Select(c => new SelectListItem
                //{
                //    Value = c.Nationality,//保存的值
                //    Text = c.Nationality//显示的值
                //}).ToList();
                //List<SelectListItem> item4 = db.Backgrounds.ToList().Select(c => new SelectListItem
                //{
                //    Value = c.XueLi,//保存的值
                //    Text = c.XueLi//显示的值
                //}).ToList();
                return View();
            }
        

        // POST: Staff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Number,BillTypeName,BillTypeNumber,StaffNumber,Name,Gender,Department,WorkType,Position,IdentificationType,Nationality,IdentificationNumber,Entrydate,ClassOrder,AppSwitch,JobState,AbnormalChange,FreeCard,WorkProperty,ApplyOvertimeSwitch,Source,QualifyingPeriodFull,MaritalStatus,BirthDate,NativePlace,HealthCondition,Nation,Address,VisaOffice,HomeTelNumber,EducationBackground,GraduationSchool,SchoolMajor,Degree,Introducer,IndividualTelNumber,BankCardNumber,UrgencyContactMan,UrgencyContactAddress,UrgencyContactPhoneNumber,InBlacklist,PhysicalCardNumber,LeaveDate,LeaveType,LeaveReason,AccountVersion,AuditStatus,BindingNumber")] Staff staff)
        {
            if (ModelState.IsValid)
            {

                var tmp = db.Staffs.Where(p => p.StaffNumber.Equals(staff.StaffNumber)).ToList();
                    if (tmp.Count != 0)
                    {
                        List<SelectListItem> item = db.Departments.ToList().Select(c => new SelectListItem
                        {
                            Value = c.Name,//保存的值
                            Text = c.Name//显示的值
                        }).ToList();
                        ViewBag.List = item;

                        ModelState.AddModelError("", "抱歉，该工号已经被注册！");

                        return View(staff);

                    }
                    else
                    {
                        db.Staffs.Add(staff);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
            }

            return View(staff);
        }

        // GET: Staff/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> item = new List<SelectListItem>();
            SelectListItem i = new SelectListItem();
            i.Value = staff.Department;
            i.Text = staff.Department;
            item.Add(i);
            item = db.Departments.ToList().Select(c => new SelectListItem
            {
                Value = c.Name,//保存的值
                Text = c.Name//显示的值
            }).ToList();
            ViewBag.List = item;
            return View(staff);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Number,BillTypeName,BillTypeNumber,StaffNumber,Name,Gender,Department,WorkType,Position,IdentificationType,Nationality,IdentificationNumber,Entrydate,ClassOrder,AppSwitch,JobState,AbnormalChange,FreeCard,WorkProperty,ApplyOvertimeSwitch,Source,QualifyingPeriodFull,MaritalStatus,BirthDate,NativePlace,HealthCondition,Nation,Address,VisaOffice,HomeTelNumber,EducationBackground,GraduationSchool,SchoolMajor,Degree,Introducer,IndividualTelNumber,BankCardNumber,UrgencyContactMan,UrgencyContactAddress,UrgencyContactPhoneNumber,InBlacklist,PhysicalCardNumber,LeaveDate,LeaveType,LeaveReason,AccountVersion,AuditStatus,BindingNumber")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staff);
        }

        // GET: Staff/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
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
