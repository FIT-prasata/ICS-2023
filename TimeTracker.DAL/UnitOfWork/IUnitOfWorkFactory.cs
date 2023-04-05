namespace TimeTracker.DAL.UnitOfWork;

public interface IUnitOfWorkFactory
{
    IUnitOfWork Create();
}