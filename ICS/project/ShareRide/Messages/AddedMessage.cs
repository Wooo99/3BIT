using ShareRide.BL.Models;

namespace ShareRide.App.Messages
{
    public record AddedMessage<T> : Message<T>
        where T : IModel
    {
    }
}