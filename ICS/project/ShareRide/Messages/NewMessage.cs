using ShareRide.BL.Models;

namespace ShareRide.App.Messages
{
    public record NewMessage<T> : Message<T>
        where T : IModel
    {
    }
}
