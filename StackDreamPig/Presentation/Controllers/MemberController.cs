using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Member.Model;
using Microsoft.AspNetCore.Http;
using Application.Member.Commands;
using Application.Member.Query;
using Common.Member;

namespace Presentation.Controllers
{

    public class MemberController : Controller {

        private ICreateMemberCommand _createMemberCommand;
        private ISearchMemberQuary _searchMemberQuary;

        public MemberController(ICreateMemberCommand createMemberCommand, ISearchMemberQuary searchMemberQuary)
        {
            _createMemberCommand = createMemberCommand;
            _searchMemberQuary = searchMemberQuary;
        }

        public IActionResult Entry(MemberModel memberModel)
        {
            return View("Entry", memberModel);
        }
        [HttpPost]
        public IActionResult EntryConfirm(MemberModel memberModel)
        {
            if (!ModelState.IsValid)
            {
                return Entry(memberModel);
            }

            return View("EntryConfirm", memberModel);
        }
        [HttpPost]
        public IActionResult EntryComplete(MemberModel memberModel)
        {
            if (!ModelState.IsValid)
            {
                return Entry(memberModel);
            }
            MemberModel member;
            try
            {
                _createMemberCommand.Execute(memberModel);

                if(memberModel.isError) 
                {
                    return View("_ErrorPage", memberModel);
                } 

                member = _searchMemberQuary.Execute(memberModel);
                HttpContext.Session.SetString("m_no", member.m_no.ToString());
            }
            catch (Exception ex)
            {
                ErrorHandling.ErrorHandler(memberModel, ex);

                return View("_ErrorPage", memberModel);
            }
            
            return View("EntryComplete", member);
        }
        [HttpPost]
        public IActionResult memberUpdate(MemberModel memberModel)
        {
            if (!ModelState.IsValid)
            {
                return Entry(memberModel);
            }

            try
            {
                memberModel.m_no = int.Parse(HttpContext.Session.GetString("m_no"));
                var member = _searchMemberQuary.Execute(memberModel);
                member.UpdateFlg = true;
                member.hasSession = true;
                return View("Entry", member);
            }
            catch (Exception ex)
            {
                ErrorHandling.ErrorHandler(memberModel, ex);

                return View("_ErrorPage", memberModel);
            }
        }
        [HttpPost]
        public IActionResult MemberUpdateComplete(MemberModel memberModel)
        {
            memberModel.UpdateFlg = true;
            if (!ModelState.IsValid)
            {
                return Entry(memberModel);
            }
            try
            {
                memberModel.m_no = int.Parse(HttpContext.Session.GetString("m_no"));
                _createMemberCommand.Execute(memberModel);
                memberModel.hasSession = true;
                return View("EntryComplete", memberModel);
            }
            catch (Exception ex)
            {
                ErrorHandling.ErrorHandler(memberModel, ex);

                return View("_ErrorPage", memberModel);
            }

        }
    }
}