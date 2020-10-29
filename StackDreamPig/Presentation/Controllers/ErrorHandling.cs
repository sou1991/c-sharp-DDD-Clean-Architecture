using Microsoft.AspNetCore.Mvc;
using stackDreamPig.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public static class ErrorHandling
    {
        [HttpPost]
        public static void ErrorHandler(ModelBase model,Exception ex)
        {
            if (!model.hasSession)
            {
                model.errorMessege = "セッション情報が切れました。再度ログインしてください。";
            }
            else
            {
                model.errorMessege = "システムエラーが発生しました。";
            }

            model.isError = true;
        }

    }
}
