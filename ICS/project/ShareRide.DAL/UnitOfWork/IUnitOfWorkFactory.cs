namespace ShareRide.DAL.UnitOfWork;

public interface IUnitOfWorkFactory
{
    IUnitOfWork Create();
}