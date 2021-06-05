
using System;
using System.Collections.Generic;
using System.Text;
using Valueobject.Books;

namespace Entities.Books
{
    public class BooksDataModelBuilder
    {
        private RegistDateValueObject _registDateValueObject;
        private int _id;
        private string _m_no;
        private int _amountUsed;
        private DateTime _utime;


        public void SetIdentify(int value)
        {
            _id = value;
        }

        public void SetRegistDate(RegistDateValueObject value)
        {
            this._registDateValueObject = value;
        }

        public void SetMemberNo(string value)
        {
            this._m_no = value;
        }

        public void SetAmountUsed(int value)
        {
            _amountUsed = value;
        }

        public void SetUtime(DateTime time)
        {
            _utime = time;
        }

        public BooksData Build()
        {
            var data = new BooksData(_id, _m_no, _amountUsed, DateTime.Now, _registDateValueObject._registDate);
            return data;

        }

    }
}
