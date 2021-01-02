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
    [AutoValidateAntiforgeryToken]
    public class MemberController : Controller {

        private ICreateMemberCommand _createMemberCommand;
        private ISearchMemberQuary _searchMemberQuary;
        IUpdateMemberCommnd _updateMemberCommnd;

        public MemberController
        (
            ICreateMemberCommand createMemberCommand, 
            ISearchMemberQuary searchMemberQuary,
            IUpdateMemberCommnd updateMemberCommnd
        )
        {
            _createMemberCommand = createMemberCommand;
            _searchMemberQuary = searchMemberQuary;
            _updateMemberCommnd = updateMemberCommnd;
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
                    return View("Entry", memberModel);
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
                return memberUpdate(memberModel);
            }

            memberModel.m_no = HttpContext.Session.GetString("m_no");

            try
            {
                var member = _searchMemberQuary.Execute(memberModel);
                return View("MemberUpdate", member);
            }
            catch (Exception ex)
            {
                ErrorHandling.ErrorHandler(memberModel, ex);
                return View("_ErrorPage", memberModel);
            }
        }
        [HttpPost]
        public IActionResult MemberUpdateConfirm(MemberModel memberModel)
        {
            if (!ModelState.IsValid)
            {
                return memberUpdate(memberModel);
            }

            return View("MemberUpdateConfirm", memberModel);
        }
        [HttpPost]
        public IActionResult MemberUpdateComplete(MemberModel memberModel)
        {
            if (!ModelState.IsValid)
            {
                return Entry(memberModel);
            }

            memberModel.m_no = HttpContext.Session.GetString("m_no");
            
            try
            {
                _updateMemberCommnd.Execute(memberModel);

                return View("MemberUpdateComplete", memberModel);
            }
            catch (Exception ex)
            {
                ErrorHandling.ErrorHandler(memberModel, ex);

                return View("_ErrorPage", memberModel);
            }

        }
    }
}