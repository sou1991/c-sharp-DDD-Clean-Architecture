using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace RESTfulWebAPI.Controllers
    {
        class ClientController
        {
            private ISearchUserApplicationService _searchUserApplicationService;

            //# TODO:　実際はDI（依存性の注入）が必要
            public ClientController(ISearchUserApplicationService searchUserApplicationService)
            {
                _searchUserApplicationService = searchUserApplicationService;
            }

            /*====================================
             Request API : domain/v1/User/{pref} 
             ===================================*/
            public void SearchUser(string pref)
            {
                var targetPref = new UserPrefectures(pref);

                var clientList = _searchUserApplicationService.Execute(targetPref);
            }
        }

        /*====================================
        * 　Entity
        ===================================*/
        class User
        {
            public UserID userID { get; }

            public UserName userName { get; }

            public UserAddress userAddress { get; }

            public User(UserID user_id, UserName user_name, UserAddress user_address)
            {
                if (user_id == null) throw new ArgumentNullException(nameof(userID));
                if (user_name == null) throw new ArgumentNullException(nameof(userName));
                if (user_address == null) throw new ArgumentNullException(nameof(userAddress));

                this.userID = user_id;
                this.userName = user_name;
                this.userAddress = user_address;
            }
        }

        /*====================================
        *　値オブジェクト
        ===================================*/
        class UserID
        {
            public string value { get; }

            public UserID(string value)
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("ユーザーIDがnullもしくは空白です。");
                this.value = value;
            }
        }

        class UserName
        {
            public string value { get; }

            public UserName(string value)
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("ユーザー名がnullもしくは空白です。");
                this.value = value;
            }
        }

        class UserAddress
        {
            public string value { get; }

            public UserAddress(string value)
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("ユーザー住所がnullもしくは空白です。");
                this.value = value;
            }
        }

        class UserPrefectures
        {
            public string value { get; }

            public UserPrefectures(string value)
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("リクエストパラメーターがnullもしくは空白です。");
                this.value = value;
            }
        }

        /*====================================
        *　ApplicationService
        ===================================*/

        interface ISearchUserApplicationService
        {
            IEnumerable<User> Execute(UserPrefectures clientPrefectures);
        }

        class SearchUserApplicationService : ISearchUserApplicationService
        {
            private readonly IUserRepository _userRepository;

            //# TODO:　実際はDI（依存性の注入）が必要
            public SearchUserApplicationService(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public IEnumerable<User> Execute(UserPrefectures client)
            {
                return _userRepository.Find(client);
            }

        }

        /*====================================
        *  Infrastructure
        ===================================*/
        interface IUserRepository
        {
            IEnumerable<User> Find(UserPrefectures userPrefectures);
        }

        //テスト・インメモリークラス
        class InMemoryUserRepository : IUserRepository
        {
            private IQueryable<User> store = new List<User>()
            {
                new User(new UserID("1"), new UserName("Dummy"), new UserAddress("Dummy県Dummy市Dummy町"))

            }.AsQueryable();

            public IEnumerable<User> Find(UserPrefectures clientPrefectures)
            {
                //ユーザー住所を前方一致検索
                var result = store.OrderBy(p => p.userID.value)
                            .Where(p => p.userAddress.value
                            .StartsWith(clientPrefectures.value));

                return result;
            }
        }
        //DBサービスを使用したクラス。※DBによってコードが異なるので実装はしない。
        class USerRepository : IUserRepository
        {
            public IEnumerable<User> Find(UserPrefectures userPrefectures)
            {
                throw new NotImplementedException();
            }
        }

    }

}
