using MedicinApi.Models;
using MedicinApi.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicinApi.Repositories
{
    public interface IMedicoRepository
    {
        string Add(Model.Medico medico);
        IEnumerable<Models.Medico> GetAll();
        IEnumerable<Models.Medico> GetByEspecialidade(string especialidade);
        void Remove(Guid id);
        void Update(Model.Medico item);
    }
}
