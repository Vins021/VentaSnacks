using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestVentaSnack
{
    [TestClass]
    public class LoginTest
    {
        [TestMethod]
        public void usuarioCorrecto()
        {
            VentaSnacks.Models.User usuarioCorrecto = new VentaSnacks.Models.User();
            usuarioCorrecto.idUser = "206560371";
            usuarioCorrecto.password = "321";
            usuarioCorrecto.nombre = "Jonathan Rodríguez";

            VentaSnacks.Models.User usuarioRecibido = new VentaSnacks.Models.User();
            usuarioRecibido = usuarioRecibido.validaUsuario("206560371", "321");

            Assert.AreEqual(usuarioCorrecto.idUser, usuarioRecibido.idUser);
            Assert.AreEqual(usuarioCorrecto.nombre, usuarioRecibido.nombre);
            Assert.AreEqual(usuarioCorrecto.password, usuarioRecibido.password);
        }

        [TestMethod]
        public void usuarioIncorrecto()
        {
            VentaSnacks.Models.User usuarioCorrecto = new VentaSnacks.Models.User();
            usuarioCorrecto.idUser = "205550371";
            usuarioCorrecto.password = "485";
            usuarioCorrecto.nombre = "Karla Delgado";

            VentaSnacks.Models.User usuarioRecibido = new VentaSnacks.Models.User();
            usuarioRecibido = usuarioRecibido.validaUsuario("205550371", "485");


            Assert.AreEqual(usuarioCorrecto.idUser, usuarioRecibido.idUser);
            Assert.AreEqual(usuarioCorrecto.nombre, usuarioRecibido.nombre);
            Assert.AreEqual(usuarioCorrecto.password, usuarioRecibido.password);
        }

    }
}
