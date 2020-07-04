using stackDreamPig.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Member.Model
{
    public class LoginModel : ModelBase, IValidatableObject
    {
        public int? m_no { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (detectPressSubmitBtn)
            {
                if (string.IsNullOrEmpty(userName))
                    yield return new ValidationResult("ユーザー名は必須入力です。");

                if (string.IsNullOrEmpty(password))
                    yield return new ValidationResult("passwordは必須入力です。");
            }
        }
    }
}
