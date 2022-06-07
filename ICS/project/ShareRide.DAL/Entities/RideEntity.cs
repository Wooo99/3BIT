using System.ComponentModel.DataAnnotations.Schema;

namespace ShareRide.DAL.Entities;

[Table("Rides")]
public record RideEntity(
    Guid Id,
    DateTime StartTime,
    DateTime EstimatedEndTime,
    String Start,
    String Destination,
    Guid? DriverGuid,
    Guid? CarId) : IEntity
{

    [ForeignKey(nameof(DriverGuid))]
    public UserEntity? Driver { get; init; }

    public ICollection<UserEntity>? Passengers { get; init; } = new List<UserEntity>();

    [ForeignKey(nameof(CarId))]
    public CarEntity? Car { get; init; }

    public RideEntity() :
        this(Guid.Empty, DateTime.MinValue, DateTime.MinValue, String.Empty ,String.Empty ,Guid.Empty, Guid.Empty)
    { }
}
