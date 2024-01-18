using McgCompetiton.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using McgCompetiton.DAL;
using MCGLib;
using MCGLib.Models;
using MCGLib.Common;

namespace McgCompetiton.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult mcgRegister() { return View("Register"); }
        public ActionResult Login(Users users)
        {
            users = new UsersService().Login(users.UseName,users.Pwd);
            if (users != null)
            {
                Session["user"] = users;
                return Content(ConvertJSON.GetJSON(new McgResult<string>(data: "登录成功")));
            }
            else { return Content(ConvertJSON.GetJSON(new McgResult<string>("账号或密码不正确"))); }
        }
  public string Register(Users users)
        {
            if (new UsersService().InsertMethod(new Users()
            {
                Pwd = users.Pwd,UseName = users.UseName,Roles="用户"
            })>0)
            {
                return ConvertJSON.GetJSON(new McgResult<string>(data:"注册成功"));
            } else return ConvertJSON.GetJSON(new McgResult<string>("注册失败"));
        }


    }
}