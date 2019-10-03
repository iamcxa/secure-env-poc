using Demo.Models;
using Demo.Providers.DataBase;
using Demo.Providers.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class EncryptUserController : BaseAPIController
    {
        public EncryptUserController(ICryptoProvider cryptoProvider, IRepository<UserInfo> UserRepository) : base(cryptoProvider, UserRepository) { }

        [HttpPut]
        [OpenApiTag("(加密)使用者資料API")]
        [Description("新增使用者( 全加密模式: 將JsonData整個加密 )")]
        public async Task<IActionResult> Create([FromBody]EncryptPostDataViewModel model)
        {
            if (!TryValidateModel(model, out string message)) return Faild(new { message });
            if (!cryptoProvider.RSA_Decrypt(model.data, out string JsonData)) return Faild(new { message = "data invalid." });

            try
            {
                var postmodel = JsonConvert.DeserializeObject<UserCreateViewModel>(JsonData);
                if (!TryValidateModel(postmodel, out message)) return Faild(new { message });
                postmodel.Password = cryptoProvider.Bcrypt_Encrypt(postmodel.Password);
                await UserRepository.Create(Map<UserCreateViewModel, UserInfo>(postmodel));
                return Success();
            }
            catch (System.Exception ex)
            {
                return Faild(new { message = ex.Message });
            }
        }


        [HttpPost("{UserSN:int}")]
        [OpenApiTag("(加密)使用者資料API")]
        [Description("更新單一使用者資料 ( 部分模式: 將部分欄位加密並新增欄位名稱至Scope內 )")]
        public async Task<IActionResult> UpdateUser([FromRoute]int UserSN, [FromBody]EncryptPostDataViewModel<UserUpdateViewModel> model)
        {
            var IsExist = await UserRepository.Exist(UserSN);
            if (!IsExist) return Faild(new { message = "id not exist" });
            if (!TryValidateModel(model, out string message)) return Faild(new { message });

            foreach (var scope in model.Scope)
                foreach (var prop in model.data.GetType().GetProperties())
                    if (prop.Name.Equals(scope))
                        if (cryptoProvider.RSA_Decrypt(prop.GetValue(model.data, null).ToString(), out string decrypted))
                            prop.SetValue(model.data, decrypted);
                        else return Faild(new { message = $"{prop.Name} invalid" });

            var User = await UserRepository.GetById(UserSN);
            User = Map<UserUpdateViewModel, UserInfo>(model.data, User);
            await UserRepository.Update(UserSN, User);
            return Success();
        }
    }
}
