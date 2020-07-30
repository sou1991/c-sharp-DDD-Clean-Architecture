using stackDreamPig.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Member.Model
{
    public class MemberModel : ModelBase, IValidatableObject
    {
        public int m_no { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

        [RegularExpression(@"[0-9]+", ErrorMessage = "月収は数字のみ入力できます")]
        public string monthlyIncome { get; set; }

        [RegularExpression(@"[0-9]+", ErrorMessage = "目標貯金額(月)は数字のみ入力できます")]
        public string savings { get; set; }

        [RegularExpression(@"[0-9]+", ErrorMessage = "固定費は数字のみ入力できます")]
        public string fixedCost { get; set; }

        public int amountLimit
        {
            get
            {
                DateTime dt = DateTime.Now;
                var getThisMonth = DateTime.DaysInMonth(dt.Year, dt.Month);
                if (string.IsNullOrEmpty(monthlyIncome) && string.IsNullOrEmpty(savings) && string.IsNullOrEmpty(fixedCost))
                    //月収－目標貯金額－固定費が自由に使用できるお金。それを月の日数で割ると1日の許容使用額
                    return 0;
                else
                    return (SdpCommon.castIntoInteger(monthlyIncome) - SdpCommon.castIntoInteger(savings) - SdpCommon.castIntoInteger(fixedCost)) / getThisMonth;
            }
        }

        public int dispAmountLimit { get; set; }

        public bool UpdateFlg { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
          if (SdpCommon.castIntoInteger(monthlyIncome) < SdpCommon.castIntoInteger(savings) + SdpCommon.castIntoInteger(fixedCost))
                yield return new ValidationResult("目標貯金額と固定費合計が月収を上回ってます。");
        }
    }
}
