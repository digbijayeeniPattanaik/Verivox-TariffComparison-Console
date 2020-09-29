namespace TariffComparison
{
    public interface ICalculationUnit
    {
        int Consumption { get; set; }
        decimal AnnualCosts { get; }
        decimal BaseCost { get; }
        decimal ConsumptionCost { get; }
    }
}
