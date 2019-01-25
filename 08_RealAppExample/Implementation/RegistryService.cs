using System.Threading.Tasks;
using _08_RealAppExample.Contracts;
using static _08_RealAppExample.F;

namespace _08_RealAppExample.Implementation
{
    public class RegistryService : IRegistryService
    {
        public async Task<Result<DomainInfo>> GetDomainInfo(string domain)
        {
            // Processing
            await Task.Delay(1000);

            return Value(new DomainInfo
            {
                DomainName = domain,
                TransferProhibited = (domain == "transferprohibited.com")
            });
        }

        public async Task<Result<Unit>> RequestTransfer(string domain, string language)
        {
            // Processing
            await Task.Delay(1000);

            if (language == null)
                return Error("Language is not provided");

            return OK();
        }
    }
}
