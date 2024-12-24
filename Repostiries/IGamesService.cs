namespace GameZone.Repostiries
{
    public interface IGamesService
    {
        Task<IEnumerable<Game>> GetAll();
        Task<Game?> GetById(int id);
        Task<Game?> Update(UpdateGameFormViewModel Model);
        Task Create(CreateGameFormViewModel Model);
        Task<bool> Delete(int id);



    }
}
