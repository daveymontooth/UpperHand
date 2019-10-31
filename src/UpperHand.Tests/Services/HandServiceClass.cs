namespace UpperHand.Tests.Services
{
    using UpperHand.Services;
    using Xunit;

    public class HandServiceClass
    {
        /* This could be injected in */
        private readonly HandService _service = new HandService();

        [Fact]
        public void CanReturnHandServiceResult()
        {
            var result = _service.RequestDealtHand();

            Assert.NotNull(result);
        }
    }
}
