using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VentaSnacks.Models;

namespace VentaSnacks.Controllers
{
    public class LineasController : Controller
    {
        private VentaSnackEntities1 db = new VentaSnackEntities1();

        // GET: Lineas
        public ActionResult Index()
        {
            var lineas = db.Lineas.Include(l => l.Articulo);
            return View(lineas.ToList());
        }

        // GET: Lineas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Linea linea = db.Lineas.Find(id);
            if (linea == null)
            {
                return HttpNotFound();
            }
            return View(linea);
        }

        // GET: Lineas/Create
        public ActionResult Create()
        {
            if (TempData.ContainsKey("mensaje"))
            {
                ViewBag.Mensaje = TempData["mensaje"].ToString();
            }

            List<Articulo> lista = db.Articuloes.ToList();
            ViewBag.listaProductos = lista;
            Linea l = new Linea();
            return PartialView(l);
        }

        // POST: Lineas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Linea linea)
        {
            //Mensaje
            if (TempData.ContainsKey("mensaje"))
            {
                ViewBag.Mensaje = TempData["mensaje"].ToString();
            }

            if (ModelState.IsValid)
            {
                if (linea.cantidad>0)
                {
                Articulo art = obtenerArticulo(linea);

                linea.Articulo = art;
                List<VentaSnacks.Models.Linea> obj = (List<VentaSnacks.Models.Linea>)Session["listaArticulos"];
                Linea remover = null;
                foreach (var li in obj)
                {
                    if (linea.idArticulo==li.idArticulo)
                    {
                        linea.cantidad += li.cantidad;
                        remover = li;
                    }
                }
                obj.Remove(remover);
                linea.total = Decimal.Round(art.TotalLinea(linea),2);
                obj.Add(linea);
                Session["listaArticulos"] = obj;
                TempData["mensaje"] = "Se Agregaron:  "+linea.cantidad+" - "+linea.Articulo.nombre;
                return RedirectToAction("Create");


                }
                else
                {
                    TempData["mensaje"] = "La cantidad seleccionada debe ser mayor a 0";
                    return RedirectToAction("Create");
                }
            }

            ViewBag.idArticulo = new SelectList(db.Articuloes, "idArticulo", "nombre", linea.idArticulo);
            return View(linea);
        }

        // GET: Lineas/Seleccionar
        public ActionResult Seleccionar()
        {
            if (TempData.ContainsKey("mensaje"))
            {
                ViewBag.Mensaje = TempData["mensaje"].ToString();
            }

            List<Articulo> lista = db.Articuloes.ToList();
            ViewBag.listaProductos = lista;
            Linea l = new Linea();
            return View(l);
        }

        // POST: Lineas/Seleccionar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Seleccionar(Linea linea)
        {
            if (TempData.ContainsKey("mensaje"))
            {
                ViewBag.Mensaje = TempData["mensaje"].ToString();
            }

            if (ModelState.IsValid)
            {
                Articulo art = obtenerArticulo(linea);
                linea.Articulo = art;
                linea.total = art.TotalLinea(linea);
                db.Lineas.Add(linea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idArticulo = new SelectList(db.Articuloes, "idArticulo", "nombre", linea.idArticulo);
            return View(linea);
        }


        // GET: Lineas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Linea linea = db.Lineas.Find(id);
            if (linea == null)
            {
                return HttpNotFound();
            }
            ViewBag.idArticulo = new SelectList(db.Articuloes, "idArticulo", "nombre", linea.idArticulo);
            return View(linea);
        }

        // POST: Lineas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idLinea,idArticulo,cantidad,total")] Linea linea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(linea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idArticulo = new SelectList(db.Articuloes, "idArticulo", "nombre", linea.idArticulo);
            return View(linea);
        }

        // GET: Lineas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Linea linea = db.Lineas.Find(id);
            if (linea == null)
            {
                return HttpNotFound();
            }
            return View(linea);
        }

        // POST: Lineas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Linea linea = db.Lineas.Find(id);
            db.Lineas.Remove(linea);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public Articulo obtenerArticulo(Linea lin)
        {
            Articulo l = db.Articuloes.Where(x => x.idArticulo == lin.idArticulo).FirstOrDefault();

            return l;
        }
    }
}
