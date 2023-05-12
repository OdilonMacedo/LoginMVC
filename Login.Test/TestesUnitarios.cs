using Components.Consumers;
using Contracts;
using MassTransit;
using Moq;

namespace Login.Test
{
    public class TestesUnitarios
    {
        [Fact]
        public async Task LoginConsumerTest()
        {
            var request = new Mock<ILoginRequest>();

            request.Setup(r => r.Username).Returns("six");
            request.Setup(r => r.Password).Returns("123");

            var consumeContext = new Mock<ConsumeContext<ILoginRequest>>();
            consumeContext.Setup(c => c.Message).Returns(request.Object);

            var consumer = new LoginConsumer();

            await consumer.Consume(consumeContext.Object);

        }

    }
}
