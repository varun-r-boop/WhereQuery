using System.Threading.Tasks;

namespace Final.MailServices
{
    public interface IMailService
    {
        Task Send(string toAddress, string subject, string body);
    }
}
