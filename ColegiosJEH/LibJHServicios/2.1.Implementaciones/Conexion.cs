using LibJHModelos.EntidadesComunes;
using LibJHServicios._2._2.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibJHServicios._2._1.Implementaciones
{
    public class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Estudiantes>? Estudiantes { get; set; }
        public DbSet<Salones>? Salones { get; set; }

        public DbSet<Historicos>? Historicos { get; set; }
    }
}
