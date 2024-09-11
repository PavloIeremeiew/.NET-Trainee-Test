using Ardalis.Specification;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NET_Trainee_Test_MVC.Specification.Person
{
    public class PersonByIdSpec : Specification<NET_Trainee_Test_MVC.Models.Entities.Person>
    {
        public PersonByIdSpec(int id)
        {
            Query.Where(f => f.Id == id);
        }
    }
}
