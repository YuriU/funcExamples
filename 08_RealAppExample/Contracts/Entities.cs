namespace _08_RealAppExample.Contracts
{
    public class PostponedTransfer
    {
        public string Domain { get; set; }

        public string Language { get; set; }
    }

    public class DomainInfo
    {
        public string DomainName { get; set; }

        public bool TransferProhibited { get; set; }
    }
}
