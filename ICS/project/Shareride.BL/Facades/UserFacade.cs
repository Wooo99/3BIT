using AutoMapper;
using ShareRide.BL.Models;
using ShareRide.BL.Models.DetailModels;
using ShareRide.BL.Models.ListModels;
using ShareRide.DAL.Entities;
using ShareRide.DAL.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace ShareRide.BL.Facades;

public class UserFacade : CRUDFacade<UserEntity, UserListModel, UserDetailModel>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public UserFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
    }

    public new async Task DeleteAsync(Guid id)
    {
        RideFacade rideFacade = new RideFacade(_unitOfWorkFactory, _mapper);
        CarFacade carFacade = new CarFacade(_unitOfWorkFactory, _mapper);
        var rides = await rideFacade.GetAsync(id, 0);
        foreach(var ride in rides)
        {
            await rideFacade.DeleteAsync(ride.Id);
        }
        /*
        await using var uow = _unitOfWorkFactory.Create();
        uow.GetRepository<UserEntity>().Delete(id);
        await uow.CommitAsync().ConfigureAwait(false);
        */
        }
}