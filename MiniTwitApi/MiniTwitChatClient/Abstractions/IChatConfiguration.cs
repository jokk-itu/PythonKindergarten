namespace MiniTwitChatClient.Abstractions
{
    public interface IChatConfiguration
    {
        string BrokerHost { get; }
        int BrokerPort { get; }
        string BrokerUser { get; }
        string BrokerPassword { get; }
    }
}