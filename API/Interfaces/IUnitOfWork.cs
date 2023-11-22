namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IRecordsRepository RecordsRepository { get; }
        IAccountRepository AccountRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
