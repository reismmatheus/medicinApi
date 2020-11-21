using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicinApi.Repositories
{
    public interface IUserRepository
    {
        Model.User Authenticate(string username, string password);
        Model.User Create(Model.User user, string password);
        //IEnumerable<Model.User> GetAll();
        Model.User GetById(string id);
    }
}
