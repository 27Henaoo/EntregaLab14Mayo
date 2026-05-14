

using LibJHModelos.EntidadesComunes;
using LibJHServicios._2._2.Interfaces;
using LibJHServicios._2._3.Nucleo;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace LibJHServicios._2._1.Implementaciones
{
    public class SalonesNegocio : ISalonesNegocio
    {
        private string ObtenerDatosSalon(Salones entidad) //Convierte un Galaxiaito a puro texto papi!
        {
            return $"Id: {entidad.Id}, " +
                   $"Codigo: {entidad.Codigo}, " +
                   $"CupoMax: {entidad.CupoMax}, " +
                   $"CupoActual: {entidad.CupoActual}, " +
                   $"Fecha: {entidad.Fecha}, " +
                   $"Estudiante: {entidad._Estudiante}, ";
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
                Tabla = "Salones",
                Accion = accion,
                RegistroId = registroId,
                Descripcion = descripcion,
                Cambios = cambios,
                ValorAnterior = valorAnterior,
                ValorNuevo = valorNuevo,
                Origen = "API JHAPICOLEGIOS",
                Exitoso = exitoso,
                Error = error,
                Fecha = DateTime.Now
            });
        } // reemplaza el this.iConexion.Historicos.Add(new Historicos() { ... }) que usabamos;

        // Variable privada para manejar la conexión a la base de datos.
        private IConexion? iConexion;

        // Método para consultar todos los Salones.
        public List<Salones> Consultar()
        {
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                // Se consultan todos los registros de Salones y se devuelven en forma de lista.
                var lista = this.iConexion.Salones!.Include(x=> x._Estudiante).ToList();

                AgregarHistorico(
                   accion: "Consultar",
                   registroId: null,
                   descripcion: "Se consultaron los Salones",
                   cambios: "No se modificaron datos",
                   valorAnterior: null,
                   valorNuevo: $"Cantidad de registros consultados: {lista.Count}",
                   exitoso: true,
                   error: "N/A"
                   );


                this.iConexion.SaveChanges();
                return lista;

            }
            catch (Exception ex)
            {
                AgregarHistorico(
                    accion: "Consultar",
                    registroId: null,
                    descripcion: "Fallo al consultar los Salones",
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



        public Salones Guardar(Salones entidad)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                if (entidad.Id != 0)
                    throw new Exception("Ya se guardo");


                entidad.CupoActual = 0;


                this.iConexion.Salones!.Add(entidad);

                AgregarHistorico(
                      accion: "Guardar",
                      registroId: null,
                      descripcion: "Se guardo un nueva Salon",
                      cambios: "Se creo un nuevo registro",
                      valorAnterior: null,
                      valorNuevo: ObtenerDatosSalon(entidad),
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
                    descripcion: "Fallo al guardar una Salon",
                    cambios: "No se pudo crear el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosSalon(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();

                throw;
            }




        }


        //// Método para guardar un avión nuevo.
        //public Salones Guardar(Salones entidad)
        //{
        //    // Si el Id es distinto de 0, significa que la entidad supuestamente ya fue guardada.
        //    if (entidad.Id != 0)
        //        throw new Exception("Ya se guardo");

        //    // Se crea una nueva conexión.
        //    this.iConexion = new Conexion();

        //    // Se asigna la cadena de conexión.
        //    this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

        //    // Se agrega la entidad al conjunto de Salones.
        //    this.iConexion.Salones!.Add(entidad);

        //    // Se guardan los cambios en la base de datos.
        //    this.iConexion.SaveChanges();

        //    // Se devuelve la entidad guardada.
        //    return entidad;
        //}

        // Método para modificar un avión existente.
        public Salones Modificar(Salones entidad)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                // Si el Id es 0, no se puede modificar porque no existe en base de datos.
                if (entidad.Id == 0)
                    throw new Exception("No se puede modificar");

            
                if (entidad.CupoActual > entidad.CupoMax)
                    throw new Exception("Cupo Maximo alcanzado");
                entidad.CupoActual = 0;

                var anterior = this.iConexion.Salones!.FirstOrDefault(x => x.Id == entidad.Id); //Buscamos en la tabla Galaxias el primer Galaxia donde
                                                                                                 //Id sea igual al Id que se va a modificar.

                if (anterior == null)
                    throw new Exception("El Galaxia no existe");

                string valorAnterior = ObtenerDatosSalon(anterior); //Convertimos para pasarle el original
                string valorNuevo = ObtenerDatosSalon(entidad);//Enviamos lo que se modifico

                var entry = this.iConexion.Entry<Salones>(entidad);
                entry.State = EntityState.Modified;

                AgregarHistorico(
                  accion: "Modificar",
                  registroId: entidad.Id,
                  descripcion: "Se modifica una Salon",
                  cambios: "Se cambio la informacion de la Salon",
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
                   descripcion: "Fallo al modificar una Salon",
                   cambios: "No se pudo modificar el registro",
                   valorAnterior: null,
                   valorNuevo: ObtenerDatosSalon(entidad),
                   exitoso: false,
                   error: ex.Message
                   );

                this.iConexion.SaveChanges();

                throw;
            }
        }

        // Método para borrar un avión existente.
        public Salones Borrar(Salones entidad)
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

                var anterior = this.iConexion.Salones!.FirstOrDefault(x => x.Id == entidad.Id);

                if (anterior == null)
                    throw new Exception("El Salon no existe");

                string valorAnterior = ObtenerDatosSalon(anterior);

                // Se marca la entidad para eliminarla.
                this.iConexion.Salones!.Remove(entidad);

                AgregarHistorico(
                  accion: "Borrar",
                  registroId: entidad.Id,
                  descripcion: "Se borro una Salon",
                  cambios: "Se eliminó el registro",
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
                descripcion: "Fallo al borrar una Salon",
                cambios: "No se pudo eliminar el registro",
                valorAnterior: null,
                valorNuevo: ObtenerDatosSalon(entidad),
                exitoso: false,
                error: ex.Message
                );

                this.iConexion.SaveChanges();

                throw;
            }

        }
    }
}
