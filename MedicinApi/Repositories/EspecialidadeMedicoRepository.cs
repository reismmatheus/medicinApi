using MedicinApi.Data;
using MedicinApi.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicinApi.Repositories
{
    public class EspecialidadeMedicoRepository : IEspecialidadeMedicoRepository
    {
        private readonly DatabaseContext _context;
        public EspecialidadeMedicoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public string Add(EspecialidadeMedico especialidadeMedico)
        {
            _context.EspecialidadeMedicos.Add(especialidadeMedico);
            _context.SaveChanges();
            return especialidadeMedico.Id.ToString();
        }

        public void AddRange(List<EspecialidadeMedico> listEspecialidadeMedico)
        {
            _context.EspecialidadeMedicos.AddRange(listEspecialidadeMedico);
            _context.SaveChanges();
        }

        public EspecialidadeMedico Get(string idMedico, string idEspecialidade)
        {
            var result = _context.EspecialidadeMedicos.ToList().FirstOrDefault(x => x.EspecialidadeId.ToString() == idEspecialidade && x.MedicoId.ToString() == idMedico);
            return result;
        }
    }
}
