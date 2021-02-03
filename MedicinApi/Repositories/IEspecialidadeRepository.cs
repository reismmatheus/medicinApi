using MedicinApi.Repositories.Model;
using System.Collections.Generic;

namespace MedicinApi.Repositories
{
    public interface IEspecialidadeRepository
    {
        string Add(Especialidade especialidade);
        IEnumerable<Especialidade> GetAll();
    }
}
