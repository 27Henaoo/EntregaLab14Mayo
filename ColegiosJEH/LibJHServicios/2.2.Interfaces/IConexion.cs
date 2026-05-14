using LibJHModelos.EntidadesComunes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LibJHServicios._2._2.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

        DbSet<Estudiantes>? Estudiantes { get; set; }
        DbSet<Salones>? Salones{ get; set; }
        DbSet<Historicos>? Historicos { get; set; }

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
