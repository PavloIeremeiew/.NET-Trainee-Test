using NET_Trainee_Test_MVC.Models.Repositories.Interfaces.Person;

namespace NET_Trainee_Test_MVC.Models.Repositories.Interfaces.Base
{
    public interface IRepositoryWrapper
    {
        IPersonRepository PersonRepository { get; }

        public int SaveChanges();
        public Task<int> SaveChangesAsync();
    }
}
