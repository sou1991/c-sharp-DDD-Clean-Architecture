using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Member.Model;
using Microsoft.AspNetCore.Http;
using Application.Member.Commands;

namespace Presentation.Controllers
{

    public class MemberController : Controller {

        private ICreateMemberCommand _createMemberCommand;

        public MemberController(ICreateMemberCommand createMemberCommand)
        {
            _createMemberCommand = createMemberCommand;
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

            _createMemberCommand.Execute(memberModel);
            return View("EntryComplete", memberModel);
        }
    }
}