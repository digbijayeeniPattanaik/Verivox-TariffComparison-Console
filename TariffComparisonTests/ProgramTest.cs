using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TariffComparison;
using TariffComparison.Model;

namespace TariffComparisonTests
{
    [TestClass]
    public class ProgramTest
    {
        private IMapper mapper;

        [TestInitialize]
        public void Init()
        {
            mapper = Program.InitMapper();
        }

        [TestMethod]
        public void Test_3500_Consumption()
        {
            var products = Program.GetProducts(mapper, 3500);
            var expectedList = new List<Product>() {
                new Product() { AnnualCosts = 800, TariffName = "basic electricity tariff" },
                new Product() { AnnualCosts = 830, TariffName = "Packaged tariff" } };
            Assert.IsTrue(products.Count() == 2);
            Assert.AreEqual(products.FirstOrDefault(a => a.TariffName == "basic electricity tariff").AnnualCosts, 830M);
            Assert.AreEqual(products.FirstOrDefault(a => a.TariffName == "Packaged tariff").AnnualCosts, 800M);
            Assert.IsTrue(expectedList.SequenceEqual(expectedList));
        }


        [TestMethod]
        public void Test_4500_Consumption()
        {
            var products = Program.GetProducts(mapper, 4500);
            var expectedList = new List<Product>() {
                new Product() { AnnualCosts = 950M, TariffName = "Packaged tariff" },
                new Product() { AnnualCosts = 1050M, TariffName = "basic electricity tariff" } };

            Assert.IsTrue(products.Count() == 2);
            Assert.AreEqual(products.FirstOrDefault(a => a.TariffName == "basic electricity tariff").AnnualCosts, 1050M);
            Assert.AreEqual(products.FirstOrDefault(a => a.TariffName == "Packaged tariff").AnnualCosts, 950M);
            Assert.IsTrue(expectedList.SequenceEqual(expectedList));
        }


        [TestMethod]
        public void Test_6000_Consumption()
        {
            var products = Program.GetProducts(mapper, 6000);
            var expectedList = new List<Product>() {
                new Product() { AnnualCosts = 1380M, TariffName = "basic electricity tariff" },
                new Product() { AnnualCosts = 1400M, TariffName = "Packaged tariff" } };
            Assert.IsTrue(products.Count() == 2);
            Assert.AreEqual(products.FirstOrDefault(a => a.TariffName == "basic electricity tariff").AnnualCosts, 1380M);
            Assert.AreEqual(products.FirstOrDefault(a => a.TariffName == "Packaged tariff").AnnualCosts, 1400M);
            Assert.IsTrue(expectedList.SequenceEqual(expectedList));
        }
    }
}
