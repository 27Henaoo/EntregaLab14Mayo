
using LibJHModelos.EntidadesComunes;

namespace LibJHServicios._2._2.Interfaces
{
    public interface IEstudiantesNegocio
    {
        // Este método consulta y devuelve todos los Estudiantes.
        List<Estudiantes> Consultar();
        // Este método guarda un avión nuevo en la base de datos.
        Estudiantes Guardar(Estudiantes entidad);

        // Este método modifica un avión existente en la base de datos.
        Estudiantes Modificar(Estudiantes entidad);

        // Este método borra un avión existente en la base de datos.
        Estudiantes Borrar(Estudiantes entidad);
    }
}
