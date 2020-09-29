using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TariffComparison.Model;
using TariffComparison.ProductTypes;

namespace TariffComparison
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IMapper mapper = InitMapper();
            int consumption = GetConsumptionValue();

            IEnumerable<Product> products = GetProducts(mapper, consumption);

            Console.WriteLine(JsonConvert.SerializeObject(products));

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static int GetConsumptionValue()
        {
            int consumption = 0;
            string consumptionInput = string.Empty;
            while (string.IsNullOrWhiteSpace(consumptionInput))
            {
                Console.WriteLine("Please enter the Consumption (kWh/year)");
                consumptionInput = Console.ReadLine();
                bool isParsable = Int32.TryParse(consumptionInput, out consumption);
                if (!isParsable)
                {
                    consumptionInput = string.Empty;
                    Console.WriteLine("Invalid Consumption value");
                }
            }

            return consumption;
        }

        public static IEnumerable<Product> GetProducts(IMapper mapper, int consumption)
        {
            List<ICalculationUnit> units = GetCalculationUnitsForAllProducts(consumption);

            List<Product> products = mapper.Map<List<Product>>(units);

            return products;
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

        private static List<ICalculationUnit> GetCalculationUnitsForAllProducts(int consumption)
        {
            List<ICalculationUnit> units = new List<ICalculationUnit>();
            units.Add(new ProductA(consumption));
            units.Add(new ProductB(consumption));
            units = units.OrderBy(a => a.AnnualCosts).ToList();
            return units;
        }

    }
}
