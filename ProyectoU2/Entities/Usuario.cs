using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoU2.Entities
{
    public class Usuario : BaseModel
    {

        [PrimaryKey("id")]
        public int id { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string? telefono { get; set; }  

        public bool activo { get; set; }
        public DateTime fecha_creacion { get; set; }
    }


}
