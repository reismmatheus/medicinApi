using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicinApi.Models
{
    public class Medico
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Crm { get; set; }
        public List<string> Especialidades { get; set; }
    }
}
