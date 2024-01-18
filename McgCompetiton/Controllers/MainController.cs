using McgCompetiton.DAL;
using McgCompetiton.Models;
using MCGLib;
using MCGLib.Common;
using MCGLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace McgCompetiton.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Login");
        }
        public ActionResult UserManage()
        {
            ViewBag.Admin=new UsersService().GetAllUsers();
            return View();
        }
        public ActionResult AddAdmin(Users users)
        {
            if (new UsersService().InsertMethod(users) >0)          
                return Redirect("UserManage");
             else return Content(McgTips.WebTips("添加失败"));
        }
        public ActionResult AdminDelete(int  id)
        {
            if (new UsersService().DeleteMethod(new Users() { UserId=id}) > 0)         
                return Redirect("UserManage");         
            else return Content(McgTips.WebTips("删除失败"));
        }
        public ActionResult Comprtition() {            
                      ViewBag.Comprtition = new CompetitionsService().GetAllCompetitions();
            return View(); }    
        public ActionResult UpdateCompetition(int Id, string pass)
        {
            if (new CompetitionsService().UpdateMethodById(new Competitions() { CompetitionId=Id,ComStartus=pass}))
                return Redirect("Comprtition");
            else return Content(McgTips.WebTips("失败"));
        }
        public ActionResult Registration() {            
            ViewBag.Registratition = new CompetitionsService().GetAllCompetitions(-1, "通过", ((Users)Session["user"]).UserId );
            return View(); }
        public ActionResult RegistrationSelect() {
            ViewBag.RegistrationSelect = new RegistrationsService().GetAllRegistrations();
            return View(); }
        public ActionResult Registrations(int id)
        {
            if (new RegistrationsService().InsertMethod(new Models.Registrations()
            {
                CompetitionId = id,
                UserId = ((Users)Session["user"]).UserId                
            }) > 0)
                return Redirect("Registration");
            else return Content(McgTips.WebTips("报名失败"));
        }
        public ActionResult PubishComprtition() {
            ViewBag.PubishComprtition = new CompetitionsService().GetAllCompetitions(         
               ((Users)Session["user"]).UserId
            );
            return View(); }
        public ActionResult CategoryManage() {
            ViewBag.Catergory=new CategorysService().GetAllCategorys();
            return View();
        }
        public ActionResult CategoryManageSelect() {
            
            return Content(ConvertJSON.GetJSON(GetAllCategorys()));
        }
        private List<Categorys> GetAllCategorys()
        {
            return new CategorysService().GetAllCategorys();
        }
        public ActionResult AddCategory(Categorys categorys) {
            if (categorys.CategoryId==0)
            {
                if (new CategorysService().InsertMethod(categorys) > 0)
                    return Redirect("CategoryManage");
                else return Content(McgTips.WebTips("添加失败"));
            }
            else
            {
                if (new CategorysService().UpdateMethod(categorys) > 0)
                    return Redirect("CategoryManage");
                else return Content(McgTips.WebTips("修改失败"));
            }
        }
        public ActionResult CategoryDelete(int id)
        {
            if (new CategorysService().DeleteMethod(new Categorys() { CategoryId=id}) > 0)
                return Redirect("CategoryManage");
            else return Content(McgTips.WebTips("删除失败",Url.Action("CategoryManage")));
        }
        public ActionResult PublishComprtition(Competitions competitions) {
            competitions.UserId = ((Users)Session["user"]).UserId;
            if (new CompetitionsService().InsertMethod(competitions) > 0)
                return Content(ConvertJSON.GetJSON(new McgResult<string>(data:"发布成功")));
            else return Content(McgTips.WebTips("发布失败"));
        }
 
  public ActionResult ModifyAdmin(Users users)
        {
            users.UserId = ((Users)Session["user"]).UserId;
            if (new UsersService().UpdateMethod(users) > 0)
                return Content(McgTips.WebTips("成功"));
            else return Content(McgTips.WebTips("失败"));
        }



    }
}