
using LibJHModelos.EntidadesComunes;

namespace LibJHServicios._2._2.Interfaces
{
    public interface ISalonesNegocio
    {
        // Este método consulta y devuelve todos los Salones.
        List<Salones> Consultar();

        // Este método guarda un avión nuevo en la base de datos.
        Salones Guardar(Salones entidad);

        // Este método modifica un avión existente en la base de datos.
        Salones Modificar(Salones entidad);

        // Este método borra un avión existente en la base de datos.
        Salones Borrar(Salones entidad);
    }
}
