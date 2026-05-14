using LibJHModelos.EntidadesComunes;
using LibPresentacionesJH._1._1.ImplementacionesPresentacion;
using LibPresentacionesJH._1._2.InterfacesPresentacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AsepNetCoreJH.Pages.Ventanas
{
    public class SalonesModel : PageModel
    {
        private IEstudiantesNegocio? iEstudiantesNegocio;
        public List<Estudiantes> Estudiantes { get; set; } = new List<Estudiantes>();
        private ISalonesNegocio? iSalonesNegocio;
        [BindProperty] public List<Salones>? Lista { get; set; }
        [BindProperty] public Salones? Salon { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public SalonesModel()
        {
            iSalonesNegocio = new SalonesNegocio();
            iEstudiantesNegocio = new EstudiantesNegocio();

        }

        private void CargarCombos()
        {
            Estudiantes = iEstudiantesNegocio!.Consultar();

        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iSalonesNegocio == null)
                    return;
                Lista = iSalonesNegocio.Consultar();
                CargarCombos();
                Salon = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            CargarCombos();
            Salon = new Salones()
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
                Salon = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Salon == null)
                    return;
                if (Salon.Id == 0)
                    Salon = iSalonesNegocio!.Guardar(Salon!);
                else
                    Salon = iSalonesNegocio!.Modificar(Salon!);
                if (Salon.Id == 0)
                    return;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                CargarCombos();
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Salon == null)
                    return;
                Salon = iSalonesNegocio!.Borrar(Salon!);
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                CargarCombos();
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Salon = Lista!.FirstOrDefault(x => x.Id == data);
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

