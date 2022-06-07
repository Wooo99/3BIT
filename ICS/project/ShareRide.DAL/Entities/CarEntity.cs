using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShareRide.Common;
using ShareRide.Common.Enums;

namespace ShareRide.DAL.Entities;

[Table("Cars")]
public record CarEntity(
    Guid Id,
    string Name,
    Manufacturer Manufacturer,
    CarType CarType,
    int RegistrationYear,
    string? PhotoPath,
    int PassengerSeats,
    Guid OwnerGuid) : IEntity
{

    [ForeignKey(nameof(OwnerGuid))]
    public UserEntity? Owner { get; init; } = null!;

    public CarEntity() : 
        this(Guid.Empty, String.Empty, Manufacturer.Abarth ,CarType.Van ,int.MinValue, string.Empty, int.MinValue ,Guid.Empty)
    { }
}