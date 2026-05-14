using LibJHModelos.EntidadesComunes;
using LibJHServicios._2._1.Implementaciones;
using LibJHServicios._2._2.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.JHColegio.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class SalonesController : ControllerBase
    {
        // Variable privada para usar la lógica de negocio de Salones.
        private ISalonesNegocio? ISalonesNegocio;

        // Constructor del controlador.
        public SalonesController()
        {
            // Se crea la implementación concreta del negocio.
            this.ISalonesNegocio = new SalonesNegocio();
        }
        // Endpoint para consultar todos los Salones.
        [HttpGet]
        public List<Salones> Consultar()
        {
            // Validación por si no existe implementación.
            if (this.ISalonesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Consultar del negocio.
            return this.ISalonesNegocio.Consultar();
        }

        // Endpoint para guardar un avión nuevo.
        [HttpPost]
        public Salones Guardar(Salones entidad)
        {
            // Validación por si no existe implementación.
            if (this.ISalonesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Guardar del negocio.
            return this.ISalonesNegocio.Guardar(entidad);
        }

        // Endpoint para modificar un avión existente.
        [HttpPut]
        public Salones Modificar(Salones entidad)
        {
            // Validación por si no existe implementación.
            if (this.ISalonesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Modificar del negocio.
            return this.ISalonesNegocio.Modificar(entidad);
        }

        // Endpoint para borrar un avión existente.
        [HttpDelete]
        public Salones Borrar(Salones entidad)
        {
            // Validación por si no existe implementación.
            if (this.ISalonesNegocio == null)
                throw new Exception("No implementado");

            // Se llama al método Borrar del negocio.
            return this.ISalonesNegocio.Borrar(entidad);
        }
    }
}
