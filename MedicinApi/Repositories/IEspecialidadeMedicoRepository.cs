using MedicinApi.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicinApi.Repositories
{
    public interface IEspecialidadeMedicoRepository
    {
        string Add(EspecialidadeMedico especialidadeMedico);
        void AddRange(List<EspecialidadeMedico> listEspecialidadeMedico);
        EspecialidadeMedico Get(string idMedico, string idEspecialidade);
    }
}
