namespace Tally.Bus.Contracts
{
    public interface ICommandHandler<in TMessage> : IMessageHandler<TMessage>
        where TMessage : ICommandMessage {}
}