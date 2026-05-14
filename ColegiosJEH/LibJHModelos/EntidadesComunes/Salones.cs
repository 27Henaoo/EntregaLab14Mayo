
using System.ComponentModel.DataAnnotations.Schema;

namespace LibJHModelos.EntidadesComunes
{
    public class Salones
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public int CupoMax { get; set; }
        public int CupoActual { get; set; }
        public DateTime Fecha { get; set; }
        public int Estudiante { get; set; }
        [ForeignKey("Estudiante")] public Estudiantes? _Estudiante { get; set; }
    }
}
