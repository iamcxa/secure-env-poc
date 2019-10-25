using Demo.Providers.DataBase;
using Demo.Providers.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Controllers
{
    [EnableCors("CORS_POLICY")]
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ResponseCache(NoStore = true)]
    //[SwaggerResponse(StatusCodes.Status401Unauthorized, null, Description = "Token invalid 驗證無效或是過期")]
    //[SwaggerResponse(StatusCodes.Status403Forbidden, null, Description = "Token 有效但是依身分別不可使用此API")]
    [SwaggerResponse(StatusCodes.Status200OK, typeof(JsonResult), Description = "回應 Json 物件 \r\nstate : api 是否成功\r\nmessage : 訊息(optional)\r\n(other fields) : 根據API有不同回應的資料")]
    public abstract class BaseAPIController : ControllerBase
    {
        protected ICryptoProvider cryptoProvider { get; }
        protected IRepository<UserInfo> UserRepository { get; }

        private static string FIELD_STATE => "state";
        private JsonResult JsonResponse(bool state, object res_obj)
        {
            JObject obj = JObject.FromObject(res_obj);
            if (obj.ContainsKey(FIELD_STATE)) obj[FIELD_STATE] = state;
            else obj.Add(FIELD_STATE, state);
            return new JsonResult(obj.ToObject<dynamic>());
        }
        protected JsonResult Success(object res_obj = null) => JsonResponse(true, res_obj ?? new { });
        protected JsonResult Faild(object res_obj = null) => JsonResponse(false, res_obj ?? new { });
        protected bool TryValidateModel<T>(T obj, out string ErrorMessage)
        {
            if (!TryValidateModel(obj))
            {
                List<string> ErrorString = new List<string>();
                foreach (ModelError error in ModelState.Values.SelectMany(o => o.Errors))
                    ErrorString.Add(error.ErrorMessage);
                ErrorMessage = string.Join("、", ErrorString);
                return false;
            }
            ErrorMessage = string.Empty;
            return true;
        }
        protected To Map<From, To>(From from, To to = null) where From : class, new() where To : class, new()
        {
            if (to == null) to = new To();
            foreach (var from_prop in typeof(From).GetProperties())
                foreach (var to_prop in typeof(To).GetProperties())
                    if (from_prop.Name.Equals(to_prop.Name) && from_prop.GetType().Equals(to_prop.GetType()))
                        to_prop.SetValue(to, from_prop.GetValue(from));
            return to;
        }
        public BaseAPIController(ICryptoProvider cryptoProvider, IRepository<UserInfo> UserRepository)
        {
            this.cryptoProvider = cryptoProvider;
            this.UserRepository = UserRepository;
        }
    }
}
