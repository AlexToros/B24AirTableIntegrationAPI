﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using B24AirTableIntegration.Lib.AirTable;
using B24AirTableIntegration.Lib.Bitrix24;
using Newtonsoft.Json;

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

            var d = Bitrix.GetDeal("1521");
            string s = JsonConvert.SerializeObject(d);
            s = s.Replace("[75]", "false");
            var dd = JsonConvert.DeserializeObject<DealResponse>(s);
            s = JsonConvert.SerializeObject(dd);
            AirTable.UpdateOrCreate(d);
        }

        [TestMethod]
        public void TestListOrBoleanConverter()
        {
            string s = "{ \"result\": { \"UF_CRM_5BCF50BA94A18\": false, \"f\": 4 } }";

            DealResponse r = JsonConvert.DeserializeObject<DealResponse>(s);
            Assert.IsNull(r.Deal.Type_IDs);
            s = JsonConvert.SerializeObject(r);
            s = "{ \"result\": { \"UF_CRM_5BCF50BA94A18\": [ 0, 75 ] } }";
             r = JsonConvert.DeserializeObject<DealResponse>(s);
            Assert.AreEqual(r.Deal.Type_IDs[1], 75);

            s = JsonConvert.SerializeObject(r);

        }

        [TestMethod]
        public void TestAirTableClientStringBuilding()
        {
            LeadResponse leadResponse = new LeadResponse();
            Lead lead = new Lead();
            leadResponse.Lead = lead;
            ContactResponse contactResponse = new ContactResponse();
            Contact contact = new Contact();
            contactResponse.Contact = contact;
            DealResponse dealResponse = new DealResponse();
            Deal deal = new Deal();
            dealResponse.Deal = deal;
            CompanyResponse companyResponse = new CompanyResponse();
            Company company = new Company();
            companyResponse.Company = company;

            Assert.IsTrue(string.IsNullOrEmpty(deal.AirTableClientString));
            Assert.IsTrue(string.IsNullOrEmpty(lead.AirTableClientString));

            lead.NAME = "Alex";
            lead.PHONE = new List<PHONE> { new PHONE() { VALUE = "123" } };

            Assert.AreEqual(lead.AirTableClientString, "Alex (123)");

            deal.Contact = contactResponse;
            contact.NAME = "Alex";
            contact.PHONE = new List<PHONE> { new PHONE { VALUE = "123" } };

            Assert.AreEqual(deal.AirTableClientString, "Alex (123)");

            deal.Company = companyResponse;
            company.TITLE = "Company";

            Assert.AreEqual(deal.AirTableClientString, "Company Alex (123)");
        }
    }
}
