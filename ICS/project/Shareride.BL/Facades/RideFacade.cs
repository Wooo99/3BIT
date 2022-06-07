using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using ShareRide.BL.Models;
using ShareRide.BL.Models.DetailModels;
using ShareRide.BL.Models.ListModels;
using ShareRide.DAL.Entities;
using ShareRide.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using AutoMapper.EntityFrameworkCore;
using ShareRide.DAL;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ShareRide.BL.Facades;

public class RideFacade : CRUDFacade<RideEntity, RideListModel, RideDetailModel>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public RideFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        _mapper = mapper;
        _unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task<IEnumerable<RideListModel>> GetAsync(string fromCity = ".*", string toCity = ".*", DateTime? startTime = null)
    {
        Regex startRegex = new Regex(fromCity);
        Regex endRegex = new Regex(toCity);

        if(startTime == null)
        {
            startTime = DateTime.MinValue;
        }

        await using var uow = _unitOfWorkFactory.Create();
        var query = uow
            .GetRepository<RideEntity>()
            .Get();
        var q = await _mapper.ProjectTo<RideListModel>(query).ToArrayAsync().ConfigureAwait(false);
        return q.Where(item => startRegex.IsMatch(item.FromCity) && endRegex.IsMatch(item.ToCity) && item.StartTime >= startTime);
    }
    /// <summary>
    /// Mother of all filters
    /// </summary>
    /// <param name="id">User guid</param>
    /// <param name="isDriver">0 to get rides where User IS a driver
    ///                        1 to get rides where User IS NOT a driver and MAY be a passenger
    ///                        2 to get rides where User IS a driver OR a passenger</param>
    /// <param name="fromCity"></param>
    /// <param name="toCity"></param>
    /// <param name="startTime"></param>
    /// <returns>array of RideListModel</returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<IEnumerable<RideListModel>> GetAsync(Guid id, int isDriver, string fromCity = ".*", string toCity = ".*", DateTime? startTime = null)
    {
        Regex startRegex = new Regex(fromCity);
        Regex endRegex = new Regex(toCity);

        if (startTime == null)
        {
            startTime = DateTime.MinValue;
        }

        await using var uow = _unitOfWorkFactory.Create();

        if (isDriver == 0)
        {
            var query = uow
                .GetRepository<RideEntity>()
                .Get()
                .Where(item => item.Driver.Id == id);// && startRegex.IsMatch(item.Start.City) && endRegex.IsMatch(item.Destination.City) && item.StartTime >= startTime);
            var q = await _mapper.ProjectTo<RideListModel>(query).ToArrayAsync().ConfigureAwait(false);
            return q.Where(item => startRegex.IsMatch(item.FromCity) && endRegex.IsMatch(item.ToCity) && item.StartTime >= startTime);
        } else if(isDriver == 1)
        {
            var query = uow
                .GetRepository<RideEntity>()
                .Get()
                .Where(item => item.Driver.Id != id);
            var q = await _mapper.ProjectTo<RideListModel>(query).ToArrayAsync().ConfigureAwait(false);
            return q.Where(item => startRegex.IsMatch(item.FromCity) && endRegex.IsMatch(item.ToCity) && item.StartTime >= startTime);
        }
        else if(isDriver == 2)
        {
            var query = uow
                .GetRepository<RideEntity>()
                .Get();
            var q = await _mapper.ProjectTo<RideListModel>(query).ToArrayAsync().ConfigureAwait(false);
            return q.Where(item => (item.Driver.Id == id || (item.Passengers!.AsQueryable().Where(userItem => userItem.Id == id).Count() > 0))
                    && startRegex.IsMatch(item.FromCity) && endRegex.IsMatch(item.ToCity) && item.StartTime >= startTime);
        } else
        {
            throw new ArgumentException("isDriver out of range\n");
        }
    }
}