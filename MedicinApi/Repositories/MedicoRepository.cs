using MedicinApi.Data;
using MedicinApi.Models;
using MedicinApi.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicinApi.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly DatabaseContext _context;
        public MedicoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public string Add(Model.Medico medico)
        {
            _context.Medicos.Add(medico);
            _context.SaveChanges();
            return medico.Id.ToString();
        }

        public Model.Medico Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Medico> GetAll()
        {
            var result = new List<Models.Medico>();
            var medicos = _context.Medicos.ToList();
            foreach (var item in medicos)
            {
                result.Add(new Models.Medico
                {
                    Id = item.Id,
                    Cpf = item.Cpf,
                    Crm = item.Crm,
                    Nome = item.Nome,
                    Especialidades = _context.Especialidades.Where(x => item.Especialidades.Any(y => y.EspecialidadeId == x.Id))?.ToList().Select(x => x.Nome).ToList()
                });
            }
            return result;
        }

        public IEnumerable<Models.Medico> GetByEspecialidade(string especialidade)
        {
            var result = new List<Models.Medico>();

            var especialidades = _context.Especialidades.Where(x => x.Nome.ToLower() == especialidade.ToLower());
            var medicos = _context.Medicos.Where(x => x.Especialidades.Any(y => especialidades.Any(x => x.Id == y.EspecialidadeId))).ToList();
            foreach (var item in medicos)
            {
                var especialidadesMedico = _context.EspecialidadeMedicos.Where(x => x.MedicoId == item.Id);
                result.Add(new Models.Medico
                {
                    Id = item.Id,
                    Cpf = item.Cpf,
                    Crm = item.Crm,
                    Nome = item.Nome,
                    Especialidades = _context.Especialidades.Where(x => especialidadesMedico.Any(y => y.EspecialidadeId == x.Id))?.ToList().Select(x => x.Nome).ToList()
                });
            }
            return result;
        }

        public void Remove(Guid id)
        {
            //var medico = _context.Medicos.First(x => x.Id == id);
            //var especialidades = _context.Especialidades.Where(x => x.MedicoId == medico.Id);
            //foreach (var especialidade in especialidades)
            //{
            //    _context.Especialidades.Remove(especialidade);
            //}
            //_context.Medicos.Remove(medico);
            //_context.SaveChanges();
        }

        public void Update(Model.Medico medico)
        {
            _context.Medicos.Update(medico);
            _context.SaveChanges();
        }
    }
}
