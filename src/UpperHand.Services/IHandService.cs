namespace UpperHand.Services
{
    using System.Threading.Tasks;

    public interface IHandService
    {
        Task<DealtHandDTO> RequestDealtHand();
    }
}
