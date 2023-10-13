using MVCCrud.Models;
using MVCCrud.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCrud.Controllers
{
    public class TablaController : Controller
    {
        // GET: Tabla
        public ActionResult Index()
        {
            List<ListTablaViewModel> lst;
            using(CrudEntities1 db = new CrudEntities1())
            {
                lst = (from d in db.tablo
                           select new ListTablaViewModel
                           {
                               Id = d.id,
                               Nombre= d.nombre,
                               Correo= d.correo,
                           }).ToList();

            }

            return View(lst);
        }

        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(TablaViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    using(CrudEntities1 db = new CrudEntities1())
                    {
                        var oTabla = new tablo();

                        oTabla.correo = model.Correo;
                        oTabla.nombre = model.Nombre;
                        oTabla.fecha_nacimiento = model.Fecha_Nacimiento;

                        db.tablo.Add(oTabla);
                        db.SaveChanges();

                    }

                    return Redirect("~/Tabla/");
                }

                return View(model);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Editar(int id)
        {
            TablaViewModel model = new TablaViewModel();
            using(CrudEntities1 db = new CrudEntities1())
            {
                var oTabla = db.tablo.Find(id);
                model.Id = oTabla.id;
                model.Nombre= oTabla.nombre;
                model.Correo= oTabla.correo;
                //model.Fecha_Nacimiento = (DateTime)oTabla.fecha_nacimiento;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(TablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (CrudEntities1 db = new CrudEntities1())
                    {
                        var oTabla = db.tablo.Find(model.Id);

                        oTabla.correo = model.Correo;
                        oTabla.nombre = model.Nombre;
                        oTabla.fecha_nacimiento = model.Fecha_Nacimiento;

                        db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                    }

                    return Redirect("~/Tabla/");
                }

                return View(model);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            using(CrudEntities1 db = new CrudEntities1())
            {
                var Otabla = db.tablo.Find(id);
                db.tablo.Remove(Otabla);
                db.SaveChanges();
            }

            return Redirect("~/Tabla/Index");
        }
    }
}