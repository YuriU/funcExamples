using System.Threading.Tasks;
using _08_RealAppExample.Contracts;
using static _08_RealAppExample.F;

namespace _08_RealAppExample
{
    public class TransferService
    {
        private readonly ICacheDataService _cacheDataService;
        private readonly IRegistryService _registryService;

        public TransferService(ICacheDataService cacheDataService, IRegistryService registryService)
        {
            _cacheDataService = cacheDataService;
            _registryService = registryService;
        }

        public async Task<string> ConfirmTransfer(string transferId)
        {
            var strategy =
                from postponedTransfer in _cacheDataService.GetPostponedTransfer(transferId)
                from domainInfo in _registryService.GetDomainInfo(postponedTransfer.Domain)
                from _1 in AsyncResult(ValidateDomain(domainInfo))
                from _2 in _registryService.RequestTransfer(domainInfo.DomainName, postponedTransfer.Language)
                from _3 in _cacheDataService.CreateIncomingTransfer(postponedTransfer.Domain)
                from _4 in _cacheDataService.DeletePostponedTransfer(postponedTransfer.Domain)
                select _4;


            var result = await strategy;
            return result.Match(
                Error: e => e.Message,
                Value: u => "OK"
            );
        }

        private static Result<Unit> ValidateDomain(DomainInfo info)
        {
            if (info.TransferProhibited)
                return Error("Transfer is prohibited");

            return OK();
        }
    }
}
