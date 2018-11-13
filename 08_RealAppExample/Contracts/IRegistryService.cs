using System.Threading.Tasks;

namespace _08_RealAppExample.Contracts
{
    public interface IRegistryService
    {
        Task<Result<DomainInfo>> GetDomainInfo(string domain);

        Task<Result<Unit>> RequestTransfer(string domain, string language);
    }
}
