using API.Data;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IRecordsRepository RecordRepository { get; }
        IAccountRepository UserRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
