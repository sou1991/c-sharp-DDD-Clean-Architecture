using stackDreamPig.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace stackDreamPig.Models.Book.Query
{
    public class BooksModel : ModelBase
    {
        public int m_no { get; set; }

        public int amountUsed { get; set; }

        public int amountLimit { get; set; }

        [Range(0, 2999, ErrorMessage = "年は0～2999の幅でお願いします")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "年は数字のみ入力できます")]
        public string year { get; set; }
        [Range(1, 12, ErrorMessage = "月は1月～12月で入力してください。")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "月は数字のみ入力できます")]
        public string month { get; set; }

        [Range(1, 31, ErrorMessage = "日付は1～31日の幅でお願いします")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "日付は数字のみ入力できます")]
        public string day { get; set; }
        public Dictionary<string,object> bookList { get; set; }


        public DateTime registDate
        {
            get
            {
                if (!String.IsNullOrEmpty(year) || !String.IsNullOrEmpty(month) || !String.IsNullOrEmpty(day))
                {
                    var registdate = year + "-" + month + "-" + day;
                    try
                    {
                        return DateTime.Parse(registdate);
                    }
                    catch
                    {
                        isError = true;
                        errorMessege = "日付のフォーマットが正しくありません。";
                    }
                }
                return DateTime.Now;
            }

        }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (detectPressSubmitBtn)
        //    {
        //        if (string.IsNullOrEmpty(year))
        //            yield return new ValidationResult("年は必須入力です。");

        //        if (string.IsNullOrEmpty(month))
        //            yield return new ValidationResult("月は必須入力です。");

        //        if (string.IsNullOrEmpty(month))
        //            yield return new ValidationResult("日は必須入力です。");

        //        if (string.IsNullOrEmpty(amountUsed.ToString()))
        //            yield return new ValidationResult("使用額は必須入力です。");
        //    }
        //}
    }
}
