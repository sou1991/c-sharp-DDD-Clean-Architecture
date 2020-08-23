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

        public IActionResult EntryConfirm(MemberModel memberModel)
        {
            if (!ModelState.IsValid)
            {
                return Entry(memberModel);
            }

            return View("EntryConfirm", memberModel);
        }

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
                member = _searchMemberQuary.Execute(memberModel);
                HttpContext.Session.SetString("m_no", member.m_no.ToString());
            }
            catch (Exception ex)
            {
                ErrorHandling.ErrorHandler(memberModel, ex);

                return View("_SessionErrorPage", memberModel);
            }
            
            return View("EntryComplete", member);
        }
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

                return View("_SessionErrorPage", memberModel);
            }
        }
        public IActionResult MemberUpdateComplete(MemberModel memberModel)
        {
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

                return View("_SessionErrorPage", memberModel);
            }

        }
    }
}