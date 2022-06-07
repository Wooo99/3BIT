using ShareRide.BL.Models;

namespace ShareRide.App.Messages
{
    public record DeleteMessage<T> : Message<T>
        where T : IModel
    {
    }
}