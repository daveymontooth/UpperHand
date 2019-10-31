namespace UpperHand.Services
{
    using System.Threading.Tasks;
    using UpperHand.Domain.Models;
    public class HandService : IHandService
    {
        public Task<DealtHandDTO> RequestDealtHand()
        {
            var handModel = new HandModel().DealHand();
            var result = new DealtHandDTO(handModel.Cards, handModel.GetHandStrength());
            
            return Task.FromResult(result);
        }

    }
}
