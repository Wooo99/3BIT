using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using ShareRide.BL.Models;
using ShareRide.BL.Models.DetailModels;
using ShareRide.BL.Models.ListModels;
using ShareRide.DAL.Entities;
using ShareRide.DAL.UnitOfWork;
using ShareRide.DAL.Seeds;
using System;
using System.Collections.Generic;
using AutoMapper.EntityFrameworkCore;
using ShareRide.DAL;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ShareRide.BL.Facades;

public class CarFacade : CRUDFacade<CarEntity, CarListModel, CarDetailModel>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    public CarFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        _mapper = mapper;
        _unitOfWorkFactory = unitOfWorkFactory;
    }

    public new async Task DeleteAsync(Guid id)
    {
        RideFacade rideFacade = new RideFacade(_unitOfWorkFactory, _mapper);
        await using var uow = _unitOfWorkFactory.Create();

        var query = uow.GetRepository<RideEntity>()
            .Get()
            .Where(r => r.CarId == id);
        var rides = await _mapper.ProjectTo<RideDetailModel>(query).ToArrayAsync().ConfigureAwait(false);
        foreach(var ride in rides)
        {
            await rideFacade.DeleteAsync(ride);
        }

        uow.GetRepository<CarEntity>().Delete(id);
        await uow.CommitAsync().ConfigureAwait(false);
    }


    public async Task<IEnumerable<CarListModel>> GetByOwnerAsync(Guid id)
    {
        await using var uow = _unitOfWorkFactory.Create();
        var query = uow
            .GetRepository<CarEntity>()
            .Get()
            .Where(e => e.Owner.Id == id);
        return await _mapper.ProjectTo<CarListModel>(query).ToArrayAsync().ConfigureAwait(false);
    }
}