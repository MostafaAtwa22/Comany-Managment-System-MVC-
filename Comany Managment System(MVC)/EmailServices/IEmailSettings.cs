using Comany_Managment_System_MVC_.ViewModels.Account;

namespace Comany_Managment_System_MVC_.EmailServices
{
    public interface IEmailSettings
    {
        public void SendEmail(EmailVM email);
    }
}
