using Microsoft.AspNetCore.Mvc;
using stackDreamPig.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [AutoValidateAntiforgeryToken]
    public static class ErrorHandling
    {
        [HttpPost]
        public static void ErrorHandler(ModelBase model,Exception ex)
        {
            model.errorMessege = ex.Message;
            model.isError = true;
        }

    }
}
