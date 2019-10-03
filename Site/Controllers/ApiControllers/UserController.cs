using Demo.Models;
using Demo.Providers.DataBase;
using Demo.Providers.Interface;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class UserController : BaseAPIController
    {
        public UserController(ICryptoProvider cryptoProvider, IRepository<UserInfo> UserRepository) : base(cryptoProvider, UserRepository) { }

        [HttpGet]
        [OpenApiTag("使用者資料API")]
        [Description("取得使用者一覽")]
        public IActionResult List() => Success(new { data = UserRepository.GetList().Select(o => Map<UserInfo, UserViewModel>(o)) });

        [HttpPut]
        [OpenApiTag("使用者資料API")]
        [Description("新增使用者")]
        public async Task<IActionResult> Create([FromBody] UserCreateViewModel model)
        {
            if (!TryValidateModel(model, out string message)) return Faild(new { message });
            model.Password = cryptoProvider.Bcrypt_Encrypt(model.Password);
            await UserRepository.Create(Map<UserCreateViewModel, UserInfo>(model));
            return Success();
        }

        [HttpGet("{UserSN:int}")]
        [OpenApiTag("使用者資料API")]
        [Description("取得單一使用者資料")]
        public async Task<IActionResult> GetUser([FromRoute]int UserSN)
        {
            var IsExist = await UserRepository.Exist(UserSN);
            if (!IsExist) return Faild(new { message = "id not exist" });

            var User = await UserRepository.GetById(UserSN);
            return Success(new { data = Map<UserInfo, UserViewModel>(User) });
        }

        [HttpPost("{UserSN:int}")]
        [OpenApiTag("使用者資料API")]
        [Description("更新單一使用者資料")]
        public async Task<IActionResult> UpdateUser([FromRoute]int UserSN, [FromBody]UserUpdateViewModel model)
        {
            var IsExist = await UserRepository.Exist(UserSN);
            if (!IsExist) return Faild(new { message = "id not exist" });
            if (!TryValidateModel(model, out string message)) return Faild(new { message });

            var User = await UserRepository.GetById(UserSN);
            User = Map<UserUpdateViewModel, UserInfo>(model, User);
            await UserRepository.Update(UserSN, User);
            return Success();
        }

        [HttpDelete("{UserSN:int}")]
        [OpenApiTag("使用者資料API")]
        [Description("刪除單一使用者")]
        public async Task<IActionResult> DeleteUser([FromRoute]int UserSN)
        {
            var IsExist = await UserRepository.Exist(UserSN);
            if (!IsExist) return Faild(new { message = "id not exist" });
            await UserRepository.Delete(UserSN);
            return Success(new { });
        }
    }
}
