

using LibJHModelos.EntidadesComunes;
using LibJHServicios._2._2.Interfaces;
using LibJHServicios._2._3.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace LibJHServicios._2._1.Implementaciones
{
    public class EstudiantesNegocio : IEstudiantesNegocio
    {
        private string ObtenerDatosEstudiante(Estudiantes entidad) //Convierte un Galaxiaito a puro texto papi!
        {
            return $"Id: {entidad.Id}, " +
                   $"Nombre: {entidad.Nombre}, " +
                   $"TipoEstudiante: {entidad.TipoPersona}, " +
                   $"Voto: {entidad.Voto}, " +
                   $"CostoMatricula: {entidad.CostoMatricula}, " +
                   $"Descuento: {entidad.Descuento}, ";
                  
        }

        private void AgregarHistorico(
            string accion,
            int? registroId,
            string descripcion,
            string? cambios,
            string? valorAnterior,
            string? valorNuevo,
            bool exitoso,
            string? error)
        {
            this.iConexion!.Historicos!.Add(new Historicos()
            {
                Usuario = "ADMIN",
                Tabla = "Estudiantes",
                Accion = accion,
                RegistroId = registroId,
                Descripcion = descripcion,
                Cambios = cambios,
                ValorAnterior = valorAnterior,
                ValorNuevo = valorNuevo,
                Origen = "API JHColegio",
                Exitoso = exitoso,
                Error = error,
                Fecha = DateTime.Now
            });
        } // reemplaza el this.iConexion.Historicos.Add(new Historicos() { ... }) que usabamos;

        // Variable privada para manejar la conexión a la base de datos.
        private IConexion? iConexion;

        // Método para consultar todos los Estudiantes.
        public List<Estudiantes> Consultar()
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");


            try
            {
                //var Lista = this.iConexion.Estudiantes!.Include(x => x._Piscina).ToList();
                var Lista = this.iConexion.Estudiantes!.ToList();

                AgregarHistorico(
                   accion: "Consultar",
                   registroId: null,
                   descripcion: "Se consultaron las Estudiantes",
                   cambios: "No se modificaron datos",
                   valorAnterior: null,
                   valorNuevo: $"Cantidad de registros consultados: {Lista.Count}",
                   exitoso: true,
                   error: "N/A"
                   );

                this.iConexion.SaveChanges();
                return Lista;
            }
            catch (Exception ex)
            {
                AgregarHistorico(
                   accion: "Consultar",
                   registroId: null,
                   descripcion: "Fallo al consultar las Estudiantes",
                   cambios: "No se pudo consultar la lista",
                   valorAnterior: "Erro al Consultar",
                   valorNuevo: "Error al Consultar",
                   exitoso: false,
                   error: ex.Message
                   );

                this.iConexion.SaveChanges();

                throw;
            }


        }

        public decimal CalcularDescuento(Estudiantes entidad)
        {
            if (entidad == null)
                throw new Exception("La Estudiante es obligatoria");

            if (entidad.CostoMatricula < 0)
                throw new Exception("El valor de la matricula no puede ser negativo");

            if (EsFechaEspecial(entidad.Fecha))
                return entidad.CostoMatricula * 0.1m;

            if (entidad.Voto == true)
                return entidad.CostoMatricula * 0.15m;

            if (entidad.TipoPersona == "Desplazado" && entidad.TipoPersona == "desplazado")
                return entidad.CostoMatricula * 0.40m;


            return entidad.CostoMatricula * 0.1m;
        }

        public bool EsFechaEspecial(DateTime fecha)
        {
            DateTime soloFecha = fecha.Date;

            DateTime[] fechasEspeciales =
            {
        new DateTime(fecha.Year, 1, 16),   // Año nuevo
        new DateTime(fecha.Year, 7, 20),  // Independencia
        new DateTime(fecha.Year, 8, 7),   // Batalla de Boyacá
        new DateTime(fecha.Year, 12, 25)  // Navidad
            };

            return fechasEspeciales.Contains(soloFecha);
        }
        public decimal CalcularValorEntrada(Estudiantes entidad)
        {
            if (entidad == null)
                throw new Exception("La Estudiante es obligatoria");

            if (entidad.CostoMatricula < 0)
                throw new Exception("El valor de la entrada no puede ser negativo");

            decimal descuento = CalcularDescuento(entidad);
            return entidad.CostoMatricula - descuento;
        }


        public Estudiantes Guardar(Estudiantes entidad)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");


            try
            {
                if (entidad.Id != 0)
                    throw new Exception("Ya se guardo");

                if (string.IsNullOrWhiteSpace(entidad.Nombre))
                    throw new Exception("El nombre es obligatorio"); 

                if (entidad.CostoMatricula < 0)
                    throw new Exception("El valor de entrada no puede ser negativo");

                if (entidad.CostoMatricula != 8000)
                    entidad.CostoMatricula = 8000;


                entidad.CostoMatricula = CalcularValorEntrada(entidad);


                this.iConexion.Estudiantes!.Add(entidad);
                AgregarHistorico(
                      accion: "Guardar",
                      registroId: null,
                      descripcion: "Se guardo un nueva Estudiante",
                      cambios: "Se creo un nuevo registro",
                      valorAnterior: null,
                      valorNuevo: ObtenerDatosEstudiante(entidad),
                      exitoso: true,
                      error: "N/A"
                  );
                this.iConexion.SaveChanges();

                return entidad;
            }
            catch (Exception ex)
            {
                AgregarHistorico(
                   accion: "Guardar",
                   registroId: entidad.Id,
                   descripcion: "Fallo al guardar una Galaxia",
                   cambios: "No se pudo crear el registro",
                   valorAnterior: null,
                   valorNuevo: ObtenerDatosEstudiante(entidad),
                   exitoso: false,
                   error: ex.Message
               );

                this.iConexion.SaveChanges();
                throw;
            }


        }


        //// Método para guardar un avión nuevo.
        //public Estudiantes Guardar(Estudiantes entidad)
        //{
        //    // Si el Id es distinto de 0, significa que la entidad supuestamente ya fue guardada.
        //    if (entidad.Id != 0)
        //        throw new Exception("Ya se guardo");

        //    // Se crea una nueva conexión.
        //    this.iConexion = new Conexion();

        //    // Se asigna la cadena de conexión.
        //    this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

        //    // Se agrega la entidad al conjunto de Estudiantes.
        //    this.iConexion.Estudiantes!.Add(entidad);

        //    // Se guardan los cambios en la base de datos.
        //    this.iConexion.SaveChanges();

        //    // Se devuelve la entidad guardada.
        //    return entidad;
        //}

        // Método para modificar un avión existente.
        public Estudiantes Modificar(Estudiantes entidad)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");
            try
            {
                // Si el Id es 0, no se puede modificar porque no existe en base de datos.
                if (entidad.Id == 0)
                    throw new Exception("No se puede modificar");

                if (string.IsNullOrWhiteSpace(entidad.Nombre))
                    throw new Exception("El nombre es obligatorio");

              

                if (entidad.CostoMatricula < 0)
                    throw new Exception("El valor de entrada no puede ser negativo");
                if (entidad.CostoMatricula != 8000)
                    entidad.CostoMatricula = 8000;
                entidad.CostoMatricula = CalcularValorEntrada(entidad);

                var anterior = this.iConexion.Estudiantes!.FirstOrDefault(x => x.Id == entidad.Id); //Buscamos en la tabla Galaxias el primer Galaxia donde
                                                                                                 //Id sea igual al Id que se va a modificar.

                if (anterior == null)
                    throw new Exception("El Galaxia no existe");

                string valorAnterior = ObtenerDatosEstudiante(anterior); //Convertimos para pasarle el original
                string valorNuevo = ObtenerDatosEstudiante(entidad);//Enviamos lo que se modifico


                var entry = this.iConexion.Entry<Estudiantes>(entidad);
                entry.State = EntityState.Modified;

                AgregarHistorico(
                  accion: "Modificar",
                  registroId: entidad.Id,
                  descripcion: "Se modifica una Estudiante",
                  cambios: "Se cambio la informacion de la Estudiante",
                  valorAnterior: valorAnterior,
                  valorNuevo: valorNuevo,
                  exitoso: true,
                  error: "N/A"
                   );
                this.iConexion.SaveChanges();

                return entidad;
            }
            catch (Exception ex)
            {
                AgregarHistorico(
                   accion: "Modificar",
                   registroId: entidad.Id,
                   descripcion: "Fallo al modificar una Estudiante",
                   cambios: "No se pudo modificar el registro",
                   valorAnterior: null,
                   valorNuevo: ObtenerDatosEstudiante(entidad),
                   exitoso: false,
                   error: ex.Message
                   );

                this.iConexion.SaveChanges();

                throw;
            }

        }

        // Método para borrar un avión existente.
        public Estudiantes Borrar(Estudiantes entidad)
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                // Si el Id es 0, no se puede borrar porque no existe en base de datos.
                if (entidad.Id == 0)
                    throw new Exception("No se puede borrar");

                var anterior = this.iConexion.Estudiantes!.FirstOrDefault(x => x.Id == entidad.Id);

                if (anterior == null)
                    throw new Exception("la Estudiante no existe");

                string valorAnterior = ObtenerDatosEstudiante(anterior);

                // Se marca la entidad para eliminarla.
                this.iConexion.Estudiantes!.Remove(entidad);

                AgregarHistorico(
                  accion: "Borrar",
                  registroId: entidad.Id,
                  descripcion: "Se borro una Estudiante",
                  cambios: "Se elimino el registro",
                  valorAnterior: valorAnterior,
                  valorNuevo: null,
                  exitoso: true,
                  error: "N/A"
               );

                this.iConexion.SaveChanges();

                // Se devuelve la entidad borrada.
                return entidad;


            }
            catch (Exception ex)
            {
                AgregarHistorico(
                accion: "Borrar",
                registroId: entidad.Id,
                descripcion: "Fallo al borrar una Estudiante",
                cambios: "No se pudo eliminar el registro",
                valorAnterior: null,
                valorNuevo: ObtenerDatosEstudiante(entidad),
                exitoso: false,
                error: ex.Message
                );

                this.iConexion.SaveChanges();

                throw;
            }
        }
    }
}
