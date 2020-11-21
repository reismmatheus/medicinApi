using MedicinApi.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicinApi.Models
{
    public class Medico
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Cpf é obrigatório")]
        [Cpf(ErrorMessage = "Cpf inválido")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Crm é obrigatório")]
        public string Crm { get; set; }
        [Required(ErrorMessage = "Especialidades é obrigatório")]
        [EmptyList(ErrorMessage = "Deve haver ao menos uma especialidade")]
        public List<string> Especialidades { get; set; }
    }
}
