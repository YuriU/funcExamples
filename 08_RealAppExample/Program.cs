using System;
using System.Threading.Tasks;
using _08_RealAppExample.Implementation;
using static _08_RealAppExample.F;

namespace _08_RealAppExample
{
    class Program
    {
        static void Main(string[] args) => MainAsync(args).Wait();
   
        
        static async Task MainAsync(string[] args)
        {
            await TestTransfer("Id:1");

            await TestTransfer("Id:2");

            await TestTransfer("Id:3");

            await TestTransfer("Id:4");
        }

        static async Task TestTransfer(string transferId)
        {
            var transferService = new TransferService(new CacheDataService(), new RegistryService());

            var result = await transferService.ConfirmTransfer(transferId);

            Console.WriteLine($"Transfer result is {result} for transfer with Id {transferId}");
        }
}
}
