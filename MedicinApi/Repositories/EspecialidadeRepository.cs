using MedicinApi.Data;
using MedicinApi.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicinApi.Repositories
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {
        private readonly DatabaseContext _context;
        public EspecialidadeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public string Add(Especialidade especialidade)
        {
            _context.Especialidades.Add(especialidade);
            _context.SaveChanges();
            return especialidade.Id.ToString();
        }

        public IEnumerable<Especialidade> GetAll()
        {
            var result = _context.Especialidades;
            return result;
        }
    }
}
