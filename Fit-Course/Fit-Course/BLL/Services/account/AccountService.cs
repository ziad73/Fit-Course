using BLL.DTOS.AccountDTOS;
using BLL.Services;
using BLL.Services.account;
using DAL.Entities.user;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services.UserServices
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailSender emailSender;
        private readonly TimeZoneInfo egyptTZ;

        public AccountService(UserManager<User> _userManager, IEmailSender _emailSender)
        {
            userManager = _userManager;
            emailSender = _emailSender;
            // Load Egypt time zone once
            egyptTZ = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
        }

        // Send code for password reset
        public async Task<bool> SendVerificationCodeAsync(string email, string purpose)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return false;

            // Generate code
            var code = new Random().Next(100000, 999999).ToString();
            user.ResetCode = code;
            user.ResetCodeExpiry = DateTime.UtcNow.AddMinutes(10);

            await userManager.UpdateAsync(user);

            // Convert expiry to Egypt time for email
            var expiryLocal = TimeZoneInfo.ConvertTimeFromUtc(user.ResetCodeExpiry.Value, egyptTZ);

            // Send email
            string htmlMessage = $@"
                <h3>Hello {user.FullName},</h3>
                <p>Your {purpose} verification code is:</p>
                <h2 style='text-align:center;color:#007bff;'>{code}</h2>
                <p>This code will expire at <b>{expiryLocal:HH:mm}</b> (local Egypt time).</p>";

            await emailSender.SendEmailAsync(user.Email, $"{purpose} Code", htmlMessage);

            return true;
        }

        // Confirm verification code
        public async Task<(bool Success, string? ErrorMessage)> ConfirmEmailCodeAsync(VerifyCodeDTO model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return (false, "User not found.");

            if (user.ResetCodeExpiry == null || user.ResetCodeExpiry < DateTime.UtcNow)
                return (false, "Verification code expired. Please request a new one.");

            if (user.ResetCode?.Trim() != model.Code?.Trim())
                return (false, "Invalid verification code. Please try again.");

            // ⚠️ Do NOT clear here → clear after password reset is successful
            return (true, null);
        }

        // Quick check method (optional)
        public bool VerifyEmailCode(string email, string code, string storedCode, string purpose)
        {
            return code == storedCode;
        }
    }
}
