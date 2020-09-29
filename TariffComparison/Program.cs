using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TariffComparison.Model;
using TariffComparison.ProductTypes;

namespace TariffComparison
{
    class Program
    {
        static void Main(string[] args)
        {
            IMapper mapper = InitMapper();
            string consumptionInput = string.Empty;
            int consumption = 0;
            while (string.IsNullOrWhiteSpace(consumptionInput))
            {
                while (string.IsNullOrWhiteSpace(consumptionInput))
                {
                    Console.WriteLine("Please enter the Consumption (kWh/year)");
                    consumptionInput = Console.ReadLine();
                    if (consumptionInput.All(a => char.IsDigit(a)))
                    {
                        consumption = Convert.ToInt32(consumptionInput);
                    }
                }

                IEnumerable<Product> products = GetProducts(mapper, consumption);

                Console.WriteLine(JsonConvert.SerializeObject(products));
                consumptionInput = string.Empty;
            }
            Console.ReadLine();
        }

        public static IEnumerable<Product> GetProducts(IMapper mapper, int consumption)
        {
            List<ICalculationUnit> units = GetCalculationUnitsForAllProducts(consumption);

            List<Product> products = mapper.Map<List<Product>>(units);

            return products;
        }

        private static List<ICalculationUnit> GetCalculationUnitsForAllProducts(int consumption)
        {
            List<ICalculationUnit> units = new List<ICalculationUnit>();
            units.Add(new ProductA(consumption));
            units.Add(new ProductB(consumption));
            units = units.OrderBy(a => a.AnnualCosts).ToList();
            return units;
        }

        public static Mapper InitMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductA, Product>();
                cfg.CreateMap<ProductB, Product>();
            });
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
