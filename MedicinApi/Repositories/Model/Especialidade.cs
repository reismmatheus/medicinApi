using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedicinApi.Repositories.Model
{
    public class Especialidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public ICollection<EspecialidadeMedico> EspecialidadeMedicos { get; set; }
    }
}
