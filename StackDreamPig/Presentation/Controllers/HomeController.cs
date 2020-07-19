using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Member.Model;
using Application.Member.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using Presentation.Models;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private ISearchMemberQuary _searchMemberQuary;

        public HomeController(ISearchMemberQuary searchMemberQuary)
        {
            _searchMemberQuary = searchMemberQuary;
        }
        public IActionResult Index(LoginModel loginModel)
        {
            var member = _searchMemberQuary.Execute(loginModel);
            if(member != null)
            {
                //会員Noをセッション情報にセット
                HttpContext.Session.SetString("m_no", member.m_no.ToString());
            }
            else
            {
                loginModel.isError = true;
                loginModel.errorMessege = "ログイン失敗しました。再度入力してください。";

                return Login(loginModel);
            }
            
            return View(loginModel);
        }

        public IActionResult Login(LoginModel loginModel, bool session = false)
        {
            return View("Login", loginModel);
        }

    }
}
