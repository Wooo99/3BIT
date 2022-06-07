using System;
using Microsoft.EntityFrameworkCore;

namespace ShareRide.DAL.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<ShareRideDbContext> _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory<ShareRideDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    //TODO toto je nejaký ojeb
    public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext());
}