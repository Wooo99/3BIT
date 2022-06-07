using System.ComponentModel.DataAnnotations.Schema;

namespace ShareRide.DAL.Entities
{
    [Table("Users")]
    public record UserEntity(
        Guid Id,
        string FirstName,
        string LastName,
        string? PhotoPath) : IEntity
    {

        public ICollection<CarEntity>? OwnedCars { get; init; } = new List<CarEntity>();

        public ICollection<RideEntity>? PassengerRides { get; init; } = new List<RideEntity>();

        public UserEntity() :
            this(Guid.Empty, String.Empty, String.Empty, String.Empty)
            {
            }
    }
}