using LibJHModelos.EntidadesComunes;
using LibPresentacionesJH._1._1.ImplementacionesPresentacion;
using LibPresentacionesJH._1._2.InterfacesPresentacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AsepNetCoreJH.Pages.Ventanas
{
    public class EstudiantesModel : PageModel
    {
        private IEstudiantesNegocio? iEstudiantesNegocio;
        [BindProperty] public List<Estudiantes>? Lista { get; set; }
        [BindProperty] public Estudiantes? Estudiante { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public EstudiantesModel()
        {
            iEstudiantesNegocio = new EstudiantesNegocio();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iEstudiantesNegocio == null)
                    return;
                Lista = iEstudiantesNegocio.Consultar();
                Estudiante = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Estudiante = new Estudiantes()
            {
                Fecha = DateTime.Now
            };
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Estudiante = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Estudiante == null)
                    return;
                if (Estudiante.Id == 0)
                    Estudiante = iEstudiantesNegocio!.Guardar(Estudiante!);
                else
                    Estudiante = iEstudiantesNegocio!.Modificar(Estudiante!);
                if (Estudiante.Id == 0)
                    return;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Estudiante == null)
                    return;
                Estudiante = iEstudiantesNegocio!.Borrar(Estudiante!);
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Estudiante = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtCerrar()
        {
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}
