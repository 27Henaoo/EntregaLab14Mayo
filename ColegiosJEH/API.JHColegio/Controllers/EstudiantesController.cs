using LibJHModelos.EntidadesComunes;
using LibJHServicios._2._1.Implementaciones;
using LibJHServicios._2._2.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.JHColegio.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de Estudiantes.
        private IEstudiantesNegocio? IEstudiantesNegocio;

        // Constructor del controlador.
        public EstudiantesController()
        {
            // Se crea la implementación concreta del negocio.
            this.IEstudiantesNegocio = new EstudiantesNegocio();
        }
        // Endpoint para consultar todos los Estudiantes.
        [HttpGet]
        public List<Estudiantes> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.IEstudiantesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.IEstudiantesNegocio.Consultar();
        }

        // Endpoint para guardar un avión nuevo.
        [HttpPost]
        public Estudiantes Guardar(Estudiantes entidad)
        {
            // Validación por si no existe implementación.
            if (this.IEstudiantesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.IEstudiantesNegocio.Guardar(entidad);
        }

        // Endpoint para modificar un avión existente.
        [HttpPut]
        public Estudiantes Modificar(Estudiantes entidad)
        {
            // Validación por si no existe implementación.
            if (this.IEstudiantesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.IEstudiantesNegocio.Modificar(entidad);
        }

        // Endpoint para borrar un avión existente.
        [HttpDelete]
        public Estudiantes Borrar(Estudiantes entidad)
        {
            // Validación por si no existe implementación.
            if (this.IEstudiantesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.IEstudiantesNegocio.Borrar(entidad);
        }
    }
}
