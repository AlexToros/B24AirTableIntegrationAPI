using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using B24AirTableIntegration.Lib.AirTable;
using B24AirTableIntegration.Lib.Bitrix24;

namespace B24AirTableIntegration.Tests.Controllers
{
    /// <summary>
    /// Summary description for BitrixControllerTest
    /// </summary>
    [TestClass]
    public class BitrixControllerTest
    {
        public BitrixControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestGettingID()
        {
            BitrixClient Bitrix = BitrixClient.Instance;
            AirTableClient AirTable = AirTableClient.Instance;

            var Lead = Bitrix.GetLead("23859");
            if (Lead.Lead.DATE_CREATE > new DateTime(2019, 7, 2, 13, 11, 0))
            {

                if (Lead.Lead.IsValid)
                    AirTable.UpdateOrCreate(Lead);
                else
                    AirTable.DeleteIfExist(Lead);
            }
        }
    }
}
