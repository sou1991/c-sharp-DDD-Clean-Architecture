using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Member.Model;
using Application.Member.Query;
using Common.Member;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using stackDreamPig.SeedWork;
//using Presentation.Models;

namespace Presentation.Controllers
{
    [AutoValidateAntiforgeryToken]

    public class HomeController : Controller
    {
        private ISearchMemberQuary _searchMemberQuary;

        public HomeController(ISearchMemberQuary searchMemberQuary)
        {
            _searchMemberQuary = searchMemberQuary;
        }
        [HttpPost]
        public IActionResult Index(MemberModel memberModel)
        {
            try
            {
                var member = _searchMemberQuary.Execute(memberModel);

                if (member != null)
                {
                    //会員Noをセッション情報にセット
                    HttpContext.Session.SetString("m_no", member.m_no.ToString());
                    memberModel.hasSession = true;
                }
                else
                {
                    memberModel.isError = true;
                    memberModel.errorMessege = "ログイン失敗しました。再度入力してください。";

                    return Login(memberModel);
                }
            }
            catch (Exception ex)
            {
                ErrorHandling.ErrorHandler(memberModel, ex);
                return View("_ErrorPage", memberModel);
            }

            return View(memberModel);
        }

        public IActionResult Login(MemberModel memberModel)
        {
            return View("Login", memberModel);
        }

        public IActionResult IndexOrLogin()
        {
            var model = new ModelBase();

            var session_M_no = HttpContext.Session.GetString("m_no");

            if(session_M_no == null)
            {
                model.isError = true;
                model.errorMessege = "セッションが切れました。再度ログインして下さい。";
                return View("Login", model);
            }
            else
            {
                model.hasSession = true;
                return View("Index", model);
            }

        }

        public IActionResult Logout()
        {
            var model = new ModelBase();

            HttpContext.Session.Remove("m_no");

            model.hasSession = false;

            return View("Login", model);

        }
    }
}
