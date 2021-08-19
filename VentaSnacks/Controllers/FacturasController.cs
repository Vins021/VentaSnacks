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
    public class FacturasController : Controller
    {
        private VentaSnackEntities1 db = new VentaSnackEntities1();

        // GET: Facturas
        public ActionResult Index()
        {
            if (TempData.ContainsKey("mensaje"))
            {
                ViewBag.Mensaje = TempData["mensaje"].ToString();
            }

            var facturas = db.Facturas.Include(f => f.Cliente).Include(f => f.Estado).Include(f => f.TipoPago);
            return View(facturas.ToList());
        }

        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            if (TempData.ContainsKey("mensaje"))
            {
                ViewBag.Mensaje = TempData["mensaje"].ToString();
            }

            ViewBag.idCliente = new SelectList(db.Clientes, "idCliente", "nombre");
            ViewBag.idEstado = new SelectList(db.Estadoes, "idEstado", "nombre");
            ViewBag.idTipoPago = new SelectList(db.TipoPagoes, "idTipoPago", "nombre");
            List<VentaSnacks.Models.Linea> obj = (List<VentaSnacks.Models.Linea>)Session["listaArticulos"];
            ViewBag.listaLineas = obj;
            VentaSnacks.Models.Factura f = new VentaSnacks.Models.Factura();
            ViewData["Subtotal"] = f.subtotal(obj).ToString("0.00");
            ViewData["Descuento"] = f.descuento(Convert.ToDecimal(ViewData["Subtotal"]), Convert.ToDecimal(0)).ToString("0.00");
            ViewData["Impuestos"] = f.impuestos(Convert.ToDecimal(ViewData["Subtotal"]), Convert.ToDecimal(ViewData["Descuento"])).ToString("0.00");
            ViewData["Total"] = f.total(Convert.ToDecimal(ViewData["Subtotal"]), Convert.ToDecimal(ViewData["Descuento"]), Convert.ToDecimal(ViewData["Impuestos"])).ToString("0.00");
            ViewData["AplicaDescuento"] = f.aplicadescuento(obj);

            return View();
        }



        // POST: Facturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Factura factura)
        {

            if (TempData.ContainsKey("mensaje"))
            {
                ViewBag.Mensaje = TempData["mensaje"].ToString();
            }
            //Lista los articulos del carro de compras
            List<VentaSnacks.Models.Linea> obj = (List<VentaSnacks.Models.Linea>)Session["listaArticulos"];
            List<VentaSnacks.Models.Linea> l = obj;

            if (ModelState.IsValid)
            {



                //No permite facturar si la lista de articulos es 0
                if (obj.Count != 0)
                {
                   

                    //Calcula los montos
                    decimal subtotal = factura.subtotal(obj);
                    decimal descuento = factura.descuento(subtotal, Convert.ToDecimal(factura.porcentajeDesc));
                    decimal impuestos = factura.impuestos(subtotal, descuento);
                    decimal tot = factura.total(subtotal, descuento, impuestos);

                    //Agrega los datos de la factura
                    factura.idEstado = 0;
                    factura.Total = tot;
                    factura.fecha = DateTime.Now;



                    factura.Lineas = obj;

                    factura.guardar(factura);
                    Session["listaArticulos"] = new List<VentaSnacks.Models.Linea>();
                    factura = factura.ultFactura();
                    return RedirectToAction("Pagar", routeValues: new { id = factura.idFactura });


                }

                TempData["mensaje"] = "Debe seleccionar al menos 1 Articulo";
                return RedirectToAction("Create");
            }


            ViewBag.idCliente = new SelectList(db.Clientes, "idCliente", "nombre", factura.idCliente);
            ViewBag.idEstado = new SelectList(db.Estadoes, "idEstado", "nombre", factura.idEstado);
            ViewBag.idTipoPago = new SelectList(db.TipoPagoes, "idTipoPago", "nombre", factura.idTipoPago);
            ViewBag.listaLineas = obj;
            ViewData["Subtotal"] = factura.subtotal(obj).ToString("0.00");
            ViewData["Descuento"] = factura.descuento(Convert.ToDecimal(ViewData["Subtotal"]), Convert.ToDecimal(0)).ToString("0.00");
            ViewData["Impuestos"] = factura.impuestos(Convert.ToDecimal(ViewData["Subtotal"]), Convert.ToDecimal(ViewData["Descuento"])).ToString("0.00");
            ViewData["Total"] = factura.total(Convert.ToDecimal(ViewData["Subtotal"]), Convert.ToDecimal(ViewData["Descuento"]), Convert.ToDecimal(ViewData["Impuestos"])).ToString("0.00");
            ViewData["AplicaDescuento"] = factura.aplicadescuento(obj);
            return View(factura);
        }








        public ActionResult Pagar(int id)
        {

            if (TempData.ContainsKey("mensaje"))
            {
                ViewBag.Mensaje = TempData["mensaje"].ToString();
            }

            Factura factura = db.Facturas.Where(x => x.idFactura == id).FirstOrDefault();
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCliente = new SelectList(db.Clientes, "idCliente", "nombre", factura.idCliente);
            ViewBag.idEstado = new SelectList(db.Estadoes, "idEstado", "nombre", factura.idEstado);
            ViewBag.idTipoPago = new SelectList(db.TipoPagoes, "idTipoPago", "nombre", factura.idTipoPago);
            return View(factura);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pagar(Factura factura)
        {

            if (TempData.ContainsKey("mensaje"))
            {
                ViewBag.Mensaje = TempData["mensaje"].ToString();
            }

            if (ModelState.IsValid)
            {
                if (factura.idEstado == 0)
                {

                    factura.idEstado = 1;
                    db.Entry(factura).State = EntityState.Modified;
                    db.SaveChanges();
                    if (factura.idTipoPago == 0)
                    {
                        TempData["mensaje"] = "Se proceso su pago con tarjeta Correctamente";
                        factura.idEstado = 1;
                        db.Entry(factura).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        decimal valor = Convert.ToDecimal(Request.Form["montoCancelar"]);
                        string pagar = factura.pagaFactura(factura, valor);
                        if (pagar == "El monto no es suficiente")
                        {
                            TempData["mensaje"] = pagar;
                            return RedirectToAction("Pagar");
                        }
                        else
                        {
                            TempData["mensaje"] = pagar;
                            return RedirectToAction("Index");
                        }
                    }

                }
                else
                {
                    TempData["mensaje"] = "La Factura ya fue cancelada";
                    return RedirectToAction("Pagar");
                }


            }

            //ViewBag.idCliente = new SelectList(db.Clientes, "idCliente", "nombre", factura.idCliente);
            //ViewBag.idEstado = new SelectList(db.Estadoes, "idEstado", "nombre", factura.idEstado);
            //ViewBag.idTipoPago = new SelectList(db.TipoPagoes, "idTipoPago", "nombre", factura.idTipoPago);
            return View(factura);
        }




        // GET: Facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCliente = new SelectList(db.Clientes, "idCliente", "nombre", factura.idCliente);
            ViewBag.idEstado = new SelectList(db.Estadoes, "idEstado", "nombre", factura.idEstado);
            ViewBag.idTipoPago = new SelectList(db.TipoPagoes, "idTipoPago", "nombre", factura.idTipoPago);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idFactura,idCliente,porcentajeDesc,idEstado,Total,idTipoPago,fecha")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCliente = new SelectList(db.Clientes, "idCliente", "nombre", factura.idCliente);
            ViewBag.idEstado = new SelectList(db.Estadoes, "idEstado", "nombre", factura.idEstado);
            ViewBag.idTipoPago = new SelectList(db.TipoPagoes, "idTipoPago", "nombre", factura.idTipoPago);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factura factura = db.Facturas.Find(id);
            db.Facturas.Remove(factura);
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
    }
}
