using ShareRide.BL.Models;

namespace ShareRide.App.Messages
{
    public record SelectedMessage<T> : Message<T>
        where T : IModel
    {
    }
}
