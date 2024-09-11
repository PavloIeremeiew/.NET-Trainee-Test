using Microsoft.EntityFrameworkCore;
using NET_Trainee_Test_MVC.Models.Persistence;
using NET_Trainee_Test_MVC.Models.Repositories.Interfaces.Base;
using NET_Trainee_Test_MVC.Models.Repositories.Interfaces.Person;
using NET_Trainee_Test_MVC.Models.Repositories.Realizations.Person;

namespace NET_Trainee_Test_MVC.Models.Repositories.Realizations.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly CsvDBContext _dBContext;

        private IPersonRepository? _personRepository = null;

        public RepositoryWrapper(CsvDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public IPersonRepository PersonRepository { get {
                if (_personRepository == null)
                {
                    _personRepository = new PersonRepository(_dBContext);
                }
                return _personRepository;
            } }

        public int SaveChanges()
        {
            return _dBContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dBContext.SaveChangesAsync();
        }
    }
}
