using System.Collections.Generic;
using System.Threading.Tasks;
using _08_RealAppExample.Contracts;
using static _08_RealAppExample.F;

namespace _08_RealAppExample.Implementation
{
    public class CacheDataService : ICacheDataService
    {
        private Dictionary<string, PostponedTransfer> _postponedTransfersById = new Dictionary<string, PostponedTransfer>()
        {
            { "Id:1", new PostponedTransfer {  Domain = "hapiness.com", Language = "en" } },
            { "Id:2", new PostponedTransfer {  Domain = "pain.com", Language = null} },
            { "Id:3", new PostponedTransfer {  Domain = "timeout.com", Language = "en" } },
            { "Id:4", new PostponedTransfer {  Domain = "transferprohibited.com", Language = "en" } },
        };

        public async Task<Result<PostponedTransfer>> GetPostponedTransfer(string transferId)
        {
            // Processing
            await Task.Delay(1000);

            if (_postponedTransfersById.TryGetValue(transferId, out var transfer))
            {
                return Value(transfer);
            }

            return Error("Cannot find transfer");
        }

        public async Task<Result<Unit>> CreateIncomingTransfer(string domain)
        {
            // Processing

            await Task.Delay(2000);

            return OK();
        }

        public async Task<Result<Unit>> DeletePostponedTransfer(string domain)
        {
            // Processing

            await Task.Delay(2000);

            if (domain == "timeout.com")
            {
                return Error("Timeout during connection to DB");
            }

            return OK();
        }
    }
}
