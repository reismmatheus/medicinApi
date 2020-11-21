

using MedicinApi.Repositories;
using MedicinApi.Repositories.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MedicinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : Controller
    {
        private readonly IMedicoRepository _medicoRepository;
        public MedicoController(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var medicos = _medicoRepository.GetAll();
            return Ok(medicos);
        }

        [HttpGet("{especialidade}")]
        public IActionResult GetByEspecialidade(string especialidade)
        {
            var medicos = _medicoRepository.GetByEspecialidade(especialidade);
            return Ok(medicos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Models.Medico medico)
        {
            var especialidades = new List<Especialidade>();
            medico.Especialidades.ForEach(x =>
            {
                especialidades.Add(new Especialidade { Nome = x });
            });
            var insert = _medicoRepository.Add(new Medico
            {
                Nome = medico.Nome,
                Cpf = medico.Cpf,
                Crm = medico.Crm,
                Especialidades = especialidades
            });
            return Ok(insert);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _medicoRepository.Remove(id);
            return Ok();
        }
    }
}
