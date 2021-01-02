using System;
using System.Collections.Generic;
using System.Text;

namespace Valueobject.Member
{
    public class MemberValueObject : ValueObject<MemberValueObject>
    {
        //DIの関係でフィールドをpublicにし、setter側でprivateにする
        public string userName { get; private set; }
        public string password { get; private set; }
        public string saltPassword { get; private set; }

        public MemberValueObject(string userName, string password, string saltPassword)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException(null, "ユーザーの名前が不正です。入力しなおしてください。");
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(null, "パスワード値が不正です。入力しなおしてください。");
            if (string.IsNullOrEmpty(saltPassword)) throw new ArgumentNullException(null, "パスワード値が不正です。入力しなおしてください。");

            this.userName = userName;
            this.password = password;
            this.saltPassword = saltPassword;
        }
        public override bool ClientConditionEquals(MemberValueObject that)
        {
            return this.password == that.password &&
                   this.userName == this.userName &&
                   this.saltPassword == that.saltPassword;
        }
    }
}
