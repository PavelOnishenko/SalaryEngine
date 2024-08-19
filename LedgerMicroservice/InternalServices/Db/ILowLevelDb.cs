namespace LedgerMicroservice.InternalServices.Db
{
    // todo do we need to inherit from DB? Maybe make it separate just for test purposes?
    public interface ILowLevelDb : IDb
    {
        Task<T[]> GetAllAsync<T>();

        Task AddAsync<T>(T entity);

        Task DropEverythingAsync();
    }
}