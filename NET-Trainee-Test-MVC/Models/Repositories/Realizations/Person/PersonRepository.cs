using NET_Trainee_Test_MVC.Models.Persistence;
using NET_Trainee_Test_MVC.Models.Repositories.Interfaces.Person;
using NET_Trainee_Test_MVC.Models.Repositories.Realizations.Base;

namespace NET_Trainee_Test_MVC.Models.Repositories.Realizations.Person
{
    public class PersonRepository : RepositoryBase<NET_Trainee_Test_MVC.Models.Entities.Person>, IPersonRepository
    {
        public PersonRepository(CsvDBContext dbContext)
            : base(dbContext)
        {
        }
    }
}
