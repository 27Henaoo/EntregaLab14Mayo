
namespace LibJHModelos.EntidadesComunes
{
    public class Historicos
    {
        public int Id { get; set; }//Id Entidad

        public string? Usuario { get; set; }//Quien lo hizo
        public string? Tabla { get; set; }//Tabla donde ocurrio
        public string? Accion { get; set; }//Que se hizo
        public int? RegistroId { get; set; }//Id de la entidad afectada, es decir Id del OBJ

        public string? Descripcion { get; set; }//Explicacion del cambio
        public string? Cambios { get; set; }//Resumen de lo que cmabio
        public string? ValorAnterior { get; set; }//Como estaba antes
        public string? ValorNuevo { get; set; }//Como quedo despues

        public string? Origen { get; set; }//Desde que Servicio vino la Accion
        public bool Exitoso { get; set; }//Fue existoso? Si o no?
        public string? Error { get; set; } //Mensaje por posible error

        public DateTime Fecha { get; set; }//Registro, cuando paso
    }
}
