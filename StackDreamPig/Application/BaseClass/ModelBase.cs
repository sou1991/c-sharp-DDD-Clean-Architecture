using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stackDreamPig.SeedWork
{
    public class ModelBase
    {
        public string errorMessege { get; set; }

        public bool isError { get; set; }

        public int submitBtn { get; set; }

        public bool detectPressSubmitBtn
        {
            get
            {
                return submitBtn == 1 ? true : false;
            }
        }
    }
}
