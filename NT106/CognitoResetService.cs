using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using System;
using System.Threading.Tasks;

namespace NT106
{
    public static class CognitoResetService
    {
        // điền của bạn:
        private const string REGION = "ap-southeast-1";
        private const string APP_CLIENT_ID = "YOUR_APP_CLIENT_ID";

        private static IAmazonCognitoIdentityProvider Client =>
            new AmazonCognitoIdentityProviderClient(RegionEndpoint.GetBySystemName(REGION));

        // B1: Gửi mã đặt lại mật khẩu
        public static async Task<(bool ok, string msg)> SendResetCodeAsync(string emailOrUsername)
        {
            try
            {
                await Client.ForgotPasswordAsync(new ForgotPasswordRequest
                {
                    ClientId = APP_CLIENT_ID,
                    Username = emailOrUsername
                });
                return (true, "Nếu tài khoản tồn tại, mã xác minh đã được gửi. Vui lòng kiểm tra email.");
            }
            catch (UserNotFoundException)
            {
                // Trả thông điệp trung lập để tránh lộ dữ liệu
                return (true, "Nếu tài khoản tồn tại, mã xác minh đã được gửi. Vui lòng kiểm tra email.");
            }
            catch (Exception ex)
            {
                return (false, "Không thể gửi mã: " + ex.Message);
            }
        }

        // B2: Xác nhận mã + đặt mật khẩu mới
        public static async Task<(bool ok, string msg)> ConfirmResetAsync(string emailOrUsername, string code, string newPassword)
        {
            try
            {
                await Client.ConfirmForgotPasswordAsync(new ConfirmForgotPasswordRequest
                {
                    ClientId = APP_CLIENT_ID,
                    Username = emailOrUsername,
                    ConfirmationCode = code,
                    Password = newPassword
                });
                return (true, "Đổi mật khẩu thành công. Vui lòng đăng nhập lại.");
            }
            catch (CodeMismatchException)
            {
                return (false, "Mã xác minh không đúng.");
            }
            catch (ExpiredCodeException)
            {
                return (false, "Mã xác minh đã hết hạn. Vui lòng gửi lại mã.");
            }
            catch (InvalidPasswordException ex)
            {
                return (false, "Mật khẩu mới không hợp lệ: " + ex.Message);
            }
            catch (Exception ex)
            {
                return (false, "Không thể xác nhận: " + ex.Message);
            }
        }
    }
}
