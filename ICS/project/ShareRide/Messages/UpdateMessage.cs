using ShareRide.BL.Models;

namespace ShareRide.App.Messages
{
    public record UpdateMessage<T> : Message<T>
        where T : IModel
    {
    }
}
