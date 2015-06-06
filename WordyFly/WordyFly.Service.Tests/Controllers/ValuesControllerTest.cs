using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordyFly.Service;
using WordyFly.Service.Controllers;

namespace WordyFly.Service.Tests.Controllers
{
    [TestClass]
    public class GameControllerControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            GameController controller = new GameController();

            var data = controller.Get();

        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            GameController controller = new GameController();

            // Act
            string result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            GameController controller = new GameController();

            // Act
            controller.Post("value");

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            GameController controller = new GameController();

            // Act
            controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            GameController controller = new GameController();

            // Act
            controller.Delete(5);

            // Assert
        }
    }
}
