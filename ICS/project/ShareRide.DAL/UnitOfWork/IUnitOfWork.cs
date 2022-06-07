using System;
using System.Threading.Tasks;
using ShareRide.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShareRide.DAL.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
    Task CommitAsync();
}