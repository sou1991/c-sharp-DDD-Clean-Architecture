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
            var exType = ex.GetType().Name;

            if (exType == "ArgumentNullException" && !model.hasSession)
            {
                model.errorMessege = "セッション情報が切れました。再度ログインしてください。";
            }
            else if(exType == "NpgsqlException")
            {
                model.errorMessege = "データベースの接続に失敗しました。";
            }
            else
            {
                model.errorMessege = "システムエラーが発生しました。";
            }

            model.isError = true;
        }

    }
}
