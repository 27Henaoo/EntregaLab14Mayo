

using LibJHModelos.EntidadesComunes;

namespace LibPresentacionesJH._1._2.InterfacesPresentacion
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
