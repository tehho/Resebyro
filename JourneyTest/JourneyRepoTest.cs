using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint07.Data;
using Checkpoint07.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JourneyTest
{
    [TestClass]
    public class JourneyRepoTest
    {
        [TestMethod]
        public void TestNext_ReturnCorrect()
        {
            JourneyRepository repo = new JourneyRepository(new ListManagerJourney());

            var startDate = new DateTime(2018,01,01);

            repo.Add(new Journey("test", "test", 0, 0, startDate, startDate.AddDays(7)));
            startDate = DateTime.Now;
            repo.Add(new Journey("test", "test", 0, 0, startDate, startDate.AddDays(7)));
            startDate = startDate.AddDays(6);
            var journey = new Journey("test", "test", 0, 0, startDate, startDate.AddDays(7));
            repo.Add(journey);

            Assert.AreEqual(journey, repo.Next());

        }
    }
}
