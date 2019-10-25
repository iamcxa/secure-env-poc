using Demo.Models;
using Demo.Providers.DataBase;
using Demo.Providers.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.ComponentModel;

namespace Demo.Controllers
{
    public class EncryptController : BaseAPIController
    {
        public EncryptController(ICryptoProvider cryptoProvider, IRepository<UserInfo> UserRepository) : base(cryptoProvider, UserRepository) { }

        [HttpGet]
        [OpenApiTag("加解密用")]
        [Description("取得RSA公鑰")]
        public IActionResult GetPublicKey()
        {
            var key = cryptoProvider.RSA_GetPublicKey();
            return Success(new { data = new { key.N, key.E } });
        }

        [HttpPost("Encrypt")]
        [OpenApiTag("加解密用")]
        [Description("測試RSA加密")]
        public IActionResult Encrypt([FromBody]TextPostViewModel model)
        {
            if (!TryValidateModel(model, out string message)) return Faild(new { message });
            return Success(new { data = cryptoProvider.RSA_Encrypt(model.text) });
        }

        [HttpPost("Decrypt")]
        [OpenApiTag("加解密用")]
        [Description("測試RSA解密")]
        public IActionResult Decrypt([FromBody]TextPostViewModel model)
        {
            if (!TryValidateModel(model, out string message)) return Faild(new { message });
            if (!cryptoProvider.RSA_Decrypt(model.text, out string Text)) return Faild(new { message = "Decrypt Faild for invliad input." });
            return Success(new { data = Text });
        }
    }
}
