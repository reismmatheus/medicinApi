﻿

using MedicinApi.Models;
using MedicinApi.Repositories;
using MedicinApi.Repositories.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : Controller
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IEspecialidadeRepository _especialidadeRepository;
        private readonly IEspecialidadeMedicoRepository _especialidadeMedicoRepository;
        public MedicoController(IMedicoRepository medicoRepository, IEspecialidadeRepository especialidadeRepository, IEspecialidadeMedicoRepository especialidadeMedicoRepository)
        {
            _medicoRepository = medicoRepository;
            _especialidadeRepository = especialidadeRepository;
            _especialidadeMedicoRepository = especialidadeMedicoRepository;
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
        //[Authorize]
        public IActionResult Create([FromBody] Models.Medico medico)
        {
            var especialidades = _especialidadeRepository.GetAll().ToList();

            var especialidadesMedico = new List<EspecialidadeMedico>();

            var especialidadesNaoCadastradas = new List<Especialidade>();

            var especialidadesCadastradas = especialidades.Where(x => medico.Especialidades.Any(y => y.ToLower() == x.Nome.ToLower())).ToList();

            especialidadesNaoCadastradas = (especialidades.Any()) ? especialidades.Where(x => !medico.Especialidades.Any(y => y.ToLower() == x.Nome.ToLower())).ToList() : medico.Especialidades.Select(x => new Especialidade { Nome = x }).ToList();

            foreach (var item in especialidadesNaoCadastradas)
            {
                var itemEspecialidade = new Especialidade { Nome = item.Nome };
                var idEspecialidade = _especialidadeRepository.Add(itemEspecialidade);
                especialidadesCadastradas.Add(new Especialidade { Id = new Guid(idEspecialidade), Nome = item.Nome });
            }

            var insert = _medicoRepository.Add(new Repositories.Model.Medico
            {
                Nome = medico.Nome,
                Cpf = medico.Cpf,
                Crm = medico.Crm
            });

            foreach (var item in especialidadesCadastradas)
            {
                especialidadesMedico.Add(new EspecialidadeMedico { EspecialidadeId = item.Id, MedicoId = new Guid(insert) });
            }

            _especialidadeMedicoRepository.AddRange(especialidadesMedico);

            return Ok(insert);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            _medicoRepository.Remove(id);
            return Ok();
        }
    }
}
