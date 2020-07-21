using stackDreamPig.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Member.Model
{
    public class LoginModel : ModelBase
    {
        public int? m_no { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

    }
}
