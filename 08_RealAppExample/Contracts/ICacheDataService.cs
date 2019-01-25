using System.Threading.Tasks;

namespace _08_RealAppExample.Contracts
{
    public interface ICacheDataService
    {
        Task<Result<PostponedTransfer>> GetPostponedTransfer(string transferId);

        Task<Result<Unit>> CreateIncomingTransfer(string domain);

        Task<Result<Unit>> DeletePostponedTransfer(string domain);
    }
}
