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
    public class HomeController : Controller
    {
        private ISearchMemberQuary _searchMemberQuary;

        public HomeController(ISearchMemberQuary searchMemberQuary)
        {
            _searchMemberQuary = searchMemberQuary;
        }

        public IActionResult Index(MemberModel memberModel)
        {
            var member = _searchMemberQuary.Execute(memberModel);
            if(member.m_no != (int)EnumMember.NON_MEMBER)
            {
                //会員Noをセッション情報にセット
                HttpContext.Session.SetString("m_no", member.m_no.ToString());
            }
            else
            {
                memberModel.isError = true;
                memberModel.errorMessege = "ログイン失敗しました。再度入力してください。";

                return Login(memberModel);
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
            try
            {
                var session_M_no = HttpContext.Session.GetString("m_no");
                var view = session_M_no == null ? View("Login", model) : View("Index", model);

                return view;

            }
            catch (Exception ex)
            {
                ErrorHandling.ErrorHandler(model, ex);

                return View("_SessionErrorPage", model);
            }
        }

    }
}
