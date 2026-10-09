using SistemaAcademicoMVC.Models;
using SistemaAcademicoMVC.Data;
using System.Linq;
using System.Web.Mvc;

namespace SistemaAcademicoMVC.Controllers
{
    [Authorize]
    public class EstudiantesController : Controller
    {
        private readonly SistemaAcademicoContext db =
            new SistemaAcademicoContext();

        // GET: Estudiantes
        [HttpGet]
        public ActionResult Index()
        {
            var estudiantes = db.Estudiantes.ToList();
            return View(estudiantes);
        }

        public ActionResult ListaEstudiantesPartial()
        {
            var estudiantes = db.Estudiantes.ToList();
            return PartialView("_ListaEstudiantesPartial", estudiantes);
        }

        //agregar un nuevo estudiante
        [HttpPost]
        public ActionResult Create(Estudiante estudiante)
        {
            db.Estudiantes.Add(estudiante);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //Details de un estudiante
        [HttpGet]
        public ActionResult Details(int id)
        {
            var estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        //Editar un estudiante
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Estudiante estudiante)
        {
            var estudianteExistente = db.Estudiantes.Find(id);
            if (estudianteExistente == null)
            {
                return HttpNotFound();
            }
            estudianteExistente.Nombre = estudiante.Nombre;
            estudianteExistente.Correo = estudiante.Correo;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //Eliminar un estudiante
        public ActionResult Delete(int id)
        {
            var estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }

            db.Estudiantes.Remove(estudiante);

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}