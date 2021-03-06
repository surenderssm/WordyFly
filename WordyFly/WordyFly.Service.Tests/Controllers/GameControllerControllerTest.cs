﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordyFly.Service;
using WordyFly.Service.Controllers;
using System.Threading;
using System.Diagnostics;
using WordFly.Shared.Model;

namespace WordyFly.Service.Tests.Controllers
{
    [TestClass]
    public class GameControllerControllerTest
    {
        [TestMethod]
        public void Get()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            // Arrange
            GameController controller = new GameController();

           // var data = controller.Get();

            watch.Stop();

            Console.WriteLine("Seconds " + watch.Elapsed.Seconds);

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
        public void PostScore()
        {
            // Arrange
            GameController controller = new GameController();
            LeaderboardRequest request = new LeaderboardRequest();
            request.GameID = "123";
            request.GameProfile = new Profile();
            request.GameProfile.NumberOfWords = 12;
            request.GameProfile.Score = 123;
            request.GameProfile.UserID = "apurvapathak";
            request.GameProfile.UserName = "Apurva Pathak";
            // Act
            controller.PostScore(request);

            // Assert
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
