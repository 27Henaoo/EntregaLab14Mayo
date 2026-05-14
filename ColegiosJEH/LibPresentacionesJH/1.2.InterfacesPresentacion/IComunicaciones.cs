

namespace LibPresentacionesJH._1._2.InterfacesPresentacion
{
    public interface IComunicaciones
    {
        Task<Dictionary<string, object>> Ejecutar(Dictionary<string, object> datos);

    }
}
