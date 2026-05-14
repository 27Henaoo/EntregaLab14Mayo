
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace LibJHModelos.EntidadesComunes
{
    public class Estudiantes
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? TipoPersona { get; set; }
        public bool Voto { get; set; }
        public decimal CostoMatricula { get; set; }
        public int Descuento { get; set; }
        public DateTime Fecha { get; set; }

        [NotMapped] public List<Salones>? Salones{ get; set; }

    }
}
