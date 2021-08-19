using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestVentaSnack
{
    [TestClass]
    public class FacturaTest
    {

        //2 Botellas de Agua + 2 Papas
        [TestMethod]
        public void aguaPapas()
        {
            VentaSnacks.Controllers.LineasController lc = new VentaSnacks.Controllers.LineasController();

            Boolean resultadoEsperado = true;
            VentaSnacks.Models.Articulo Art = new VentaSnacks.Models.Articulo();


            VentaSnacks.Models.Linea l1 = new VentaSnacks.Models.Linea();
            l1.idArticulo = "5";
            l1.cantidad = 2;
            l1.total = Art.TotalLinea(l1);


            VentaSnacks.Models.Linea l2 = new VentaSnacks.Models.Linea();
            l2.idArticulo = "2";
            l2.cantidad = 2;
            l2.total = Art.TotalLinea(l2);

            VentaSnacks.Models.Factura f = new VentaSnacks.Models.Factura();
            f.idEstado = 0;
            f.idTipoPago = 1;

            List<VentaSnacks.Models.Linea> l = new List<VentaSnacks.Models.Linea>();
            l.Add(l1);
            l.Add(l2);
            f.Lineas = l;

            Boolean resultadoObtenido = f.guardar(f);
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);

        }

        //-1 Gelatina
        [TestMethod]
        public void menos1Gelatina()
        {
            VentaSnacks.Controllers.LineasController lc = new VentaSnacks.Controllers.LineasController();

            Boolean resultadoEsperado = true;
            VentaSnacks.Models.Articulo Art = new VentaSnacks.Models.Articulo();


            VentaSnacks.Models.Linea l1 = new VentaSnacks.Models.Linea();
            l1.idArticulo = "3";
            l1.cantidad = -1;
            l1.total = Art.TotalLinea(l1);

            VentaSnacks.Models.Factura f = new VentaSnacks.Models.Factura();
            f.idEstado = 0;
            f.idTipoPago = 0;

            List<VentaSnacks.Models.Linea> l = new List<VentaSnacks.Models.Linea>();
            l.Add(l1);
            f.Lineas = l;

            Boolean resultadoObtenido = f.guardar(f);
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);

        }

        //+1 Gelatina
        [TestMethod]
        public void mas1Gelatina()
        {
            VentaSnacks.Controllers.LineasController lc = new VentaSnacks.Controllers.LineasController();

            string resultadoEsperado = "El monto no es suficiente";
            VentaSnacks.Models.Articulo Art = new VentaSnacks.Models.Articulo();
            decimal montoPagar = 0;

            VentaSnacks.Models.Linea l1 = new VentaSnacks.Models.Linea();
            l1.idArticulo = "3";
            l1.cantidad = 1;
            l1.total = Art.TotalLinea(l1);

            VentaSnacks.Models.Factura f = new VentaSnacks.Models.Factura();
            f.idEstado = 0;
            f.idTipoPago = 0;

            List<VentaSnacks.Models.Linea> l = new List<VentaSnacks.Models.Linea>();
            l.Add(l1);
            f.Lineas = l;

            f.guardar(f);
            f = f.ultFactura();
            string resultadoObtenido = f.pagaFactura(f, montoPagar);

            Assert.AreEqual(resultadoEsperado, resultadoObtenido);

        }


        //Descuento Correcto
        [TestMethod]
        public void descuentoValido()
        {
            VentaSnacks.Controllers.LineasController lc = new VentaSnacks.Controllers.LineasController();

            Boolean resultadoEsperado = true;
            VentaSnacks.Models.Articulo Art = new VentaSnacks.Models.Articulo();


            VentaSnacks.Models.Linea l1 = new VentaSnacks.Models.Linea();
            l1.idArticulo = "5";
            l1.cantidad = 10;
            l1.total = Art.TotalLinea(l1);


            VentaSnacks.Models.Linea l2 = new VentaSnacks.Models.Linea();
            l2.idArticulo = "2";
            l2.cantidad = 10;
            l2.total = Art.TotalLinea(l2);

            VentaSnacks.Models.Linea l3 = new VentaSnacks.Models.Linea();
            l3.idArticulo = "4";
            l3.cantidad = 10;
            l3.total = Art.TotalLinea(l3);

            VentaSnacks.Models.Factura f = new VentaSnacks.Models.Factura();
            f.idEstado = 0;
            f.idTipoPago = 1;
            f.porcentajeDesc = 5;

            List<VentaSnacks.Models.Linea> l = new List<VentaSnacks.Models.Linea>();
            l.Add(l1);
            l.Add(l2);
            l.Add(l3);
            f.Lineas = l;

            Boolean resultadoObtenido = f.guardar(f);
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);

        }

        //Descuento con menor monto
        [TestMethod]
        public void descuentoMontoMenor()
        {
            VentaSnacks.Controllers.LineasController lc = new VentaSnacks.Controllers.LineasController();

            Boolean resultadoEsperado = true;
            VentaSnacks.Models.Articulo Art = new VentaSnacks.Models.Articulo();


            VentaSnacks.Models.Linea l1 = new VentaSnacks.Models.Linea();
            l1.idArticulo = "5";
            l1.cantidad = 1;
            l1.total = Art.TotalLinea(l1);


            VentaSnacks.Models.Linea l2 = new VentaSnacks.Models.Linea();
            l2.idArticulo = "2";
            l2.cantidad = 1;
            l2.total = Art.TotalLinea(l2);

            VentaSnacks.Models.Linea l3 = new VentaSnacks.Models.Linea();
            l3.idArticulo = "4";
            l3.cantidad = 1;
            l3.total = Art.TotalLinea(l3);

            VentaSnacks.Models.Factura f = new VentaSnacks.Models.Factura();
            f.idEstado = 0;
            f.idTipoPago = 1;
            f.porcentajeDesc = 10;

            List<VentaSnacks.Models.Linea> l = new List<VentaSnacks.Models.Linea>();
            l.Add(l1);
            l.Add(l2);
            l.Add(l3);
            f.Lineas = l;

            Boolean resultadoObtenido = f.guardar(f);
            Assert.AreEqual(resultadoEsperado, resultadoObtenido);

        }

        //+4 Gelatina
        [TestMethod]
        public void mas4Gelatinas()
        {
            VentaSnacks.Controllers.LineasController lc = new VentaSnacks.Controllers.LineasController();

            string resultadoEsperado = "Su vuelto es: ₡0.000000";
            VentaSnacks.Models.Articulo Art = new VentaSnacks.Models.Articulo();
            decimal montoPagar = 1130;

            VentaSnacks.Models.Linea l1 = new VentaSnacks.Models.Linea();
            l1.idArticulo = "3";
            l1.cantidad = 4;
            l1.total = Art.TotalLinea(l1);

            VentaSnacks.Models.Factura f = new VentaSnacks.Models.Factura();
            f.idEstado = 0;
            f.idTipoPago = 0;

            List<VentaSnacks.Models.Linea> l = new List<VentaSnacks.Models.Linea>();
            l.Add(l1);
            f.Lineas = l;

            f.guardar(f);
            f = f.ultFactura();
            string resultadoObtenido = f.pagaFactura(f, montoPagar);

            Assert.AreEqual(resultadoEsperado, resultadoObtenido);

        }

        //+3 Gelatina
        [TestMethod]
        public void mas3Gelatinas()
        {
            VentaSnacks.Controllers.LineasController lc = new VentaSnacks.Controllers.LineasController();

            string resultadoEsperado = "Su vuelto es: ₡0.000000";
            VentaSnacks.Models.Articulo Art = new VentaSnacks.Models.Articulo();
            decimal montoPagar = 1130;

            VentaSnacks.Models.Linea l1 = new VentaSnacks.Models.Linea();
            l1.idArticulo = "3";
            l1.cantidad = 3;
            l1.total = Art.TotalLinea(l1);

            VentaSnacks.Models.Factura f = new VentaSnacks.Models.Factura();
            f.idEstado = 0;
            f.idTipoPago = 0;

            List<VentaSnacks.Models.Linea> l = new List<VentaSnacks.Models.Linea>();
            l.Add(l1);
            f.Lineas = l;

            f.guardar(f);
            f = f.ultFactura();
            string resultadoObtenido = f.pagaFactura(f, montoPagar);

            Assert.AreEqual(resultadoEsperado, resultadoObtenido);

        }
    }
}
