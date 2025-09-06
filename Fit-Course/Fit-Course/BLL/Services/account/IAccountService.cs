using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOS.AccountDTOS;

namespace BLL.Services.account
{
    public interface IAccountService
    {
        Task<bool> SendVerificationCodeAsync(string email, string purpose);
        Task<(bool Success, string? ErrorMessage)> ConfirmEmailCodeAsync(VerifyCodeDTO model);
        bool VerifyEmailCode(string email, string code, string storedCode, string purpose);

    }
}
