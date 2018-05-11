using System;
using Checkpoint07.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JourneyTest
{


    [TestClass]
    public class JourneyTest
    {


        [TestMethod]
        public void CheckThreeUnpayed_WillThrowException()
        {
            var startDate = DateTime.Now.AddDays(-1);
            Journey Göteborg = new Journey("Göteborg", "Test", 0, 1, startDate, startDate.AddDays(1));
            Journey Stockholm = new Journey("Stockholm", "Test", 0, 1, startDate, startDate.AddDays(1));
            Journey Helsingborg = new Journey("Helsingborg", "Test", 0, 1, startDate, startDate.AddDays(1));

            startDate = DateTime.Now.AddDays(6);
            Journey Testson = new Journey("Testson", "Test", 0, 1, startDate, startDate.AddDays(1));

            Person kalle = new Person("Kalle", new Address());

            kalle.GoOnJourney(Göteborg);
            kalle.GoOnJourney(Stockholm);
            kalle.GoOnJourney(Helsingborg);

            Assert.ThrowsException<InvalidOperationException>(() => Testson.SignUp(kalle));

        }

        [TestMethod]
        public void LastSignUpDatePassed_WillThrowException()
        {
            var startDate = DateTime.Now.AddDays(4);
            Journey Göteborg = new Journey("Göteborg", "Test", 0, 1, startDate, startDate.AddDays(1));
            Person kalle = new Person("Kalle", new Address());

            Assert.ThrowsException<InvalidOperationException>(() => Göteborg.SignUp(kalle));
        }

        [TestMethod]
        public void JourneyFull_WillThrowException()
        {
            var startDate = DateTime.Now.AddDays(6);
            Journey Göteborg = new Journey("Göteborg", "Test", 0, 1, startDate, startDate.AddDays(1));
            Person kalle = new Person("Kalle", new Address());
            Person johan = new Person("Johan", new Address());

            Göteborg.SignUp(kalle);

            Assert.ThrowsException<InvalidOperationException>(() => Göteborg.SignUp(johan));
        }

        [TestMethod]
        public void CalculateCost_NoUnpayed_WillReturn10()
        {
            var startDate = DateTime.Now.AddDays(6);
            Journey Göteborg = new Journey("Göteborg", "Test", 10, 1, startDate, startDate.AddDays(1));
            Person kalle = new Person("Kalle", new Address());

            Assert.AreEqual(10, Göteborg.SignUp(kalle));

        }

        [TestMethod]
        public void CalculateCost_OneDayUnpayed_WillReturn205()
        {
            var startDate = DateTime.Now.AddDays(-1);
            Journey Göteborg = new Journey("Göteborg", "Test", 100, 1, startDate, startDate.AddDays(1));
            startDate = DateTime.Now.AddDays(6);
            Journey Stockholm = new Journey("Göteborg", "Test", 100, 1, startDate, startDate.AddDays(1));
            Person kalle = new Person("Kalle", new Address());

            kalle.GoOnJourney(Göteborg);
            var cost = Math.Round(Stockholm.SignUp(kalle), 2);

            Assert.AreEqual(205m, cost);

        }

        [TestMethod]
        public void CalculateCost_TwoDaysUnpayed_WillReturn210_25()
        {
            var startDate = DateTime.Now.AddDays(-2);
            Journey Göteborg = new Journey("Göteborg", "Test", 100, 1, startDate, startDate.AddDays(1));
            startDate = DateTime.Now.AddDays(6);
            Journey Stockholm = new Journey("Göteborg", "Test", 100, 1, startDate, startDate.AddDays(1));
            Person kalle = new Person("Kalle", new Address());

            kalle.GoOnJourney(Göteborg);
            var cost = Math.Round(Stockholm.SignUp(kalle), 2);

            Assert.AreEqual(210.25m, cost);

        }

        [TestMethod]
        public void CalculateCost_TwoUnpayedJourneys_WillReturn310()
        {

            var startDate = DateTime.Now.AddDays(-1);
            Journey Göteborg = new Journey("Göteborg", "Test", 100, 1, startDate, startDate.AddDays(1));
            startDate = DateTime.Now.AddDays(6);
            Journey Stockholm = new Journey("Göteborg", "Test", 100, 1, startDate, startDate.AddDays(1));
            Person kalle = new Person("Kalle", new Address());

            kalle.GoOnJourney(Göteborg);
            kalle.GoOnJourney(Göteborg);
            var cost = Math.Round(Stockholm.SignUp(kalle), 2);

            Assert.AreEqual(310m, cost);
        }

    }
}
