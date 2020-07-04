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
            var loginSuccessM_no = _searchMemberQuary.Execute(loginModel);
            if(loginSuccessM_no != null)
            {
                //会員Noをセッション情報にセット
                HttpContext.Session.SetString("m_no", loginSuccessM_no.ToString());
            }
            else
            {
                loginModel.isError = true;
                loginModel.errorMessege = "ログイン失敗しました。再度入力してください。";

                return Login(loginModel);
            }
            
            return View();
        }

        public IActionResult Login(LoginModel loginModel)
        {
            return View("Login", loginModel);
        }

    }
}
