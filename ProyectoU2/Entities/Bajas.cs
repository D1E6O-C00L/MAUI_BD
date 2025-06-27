using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoU2.Entities
{
    public class Baja : BaseModel  // Heredamos de BaseModel
    {
        public int UsuarioId { get; set; } // Referencia al usuario dado de baja
        public DateTime FechaBaja { get; set; } // Fecha de baja
        public string Motivo { get; set; } // Motivo de la baja
        public bool Activo { get; set; } // Estado de la baja
    }
}
