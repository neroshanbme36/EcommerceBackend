using System;

namespace Application.Helpers
{
  public static class ItemCalculator
  {
    // get profit
    public static decimal GetMargin(decimal vatRate, decimal cost, decimal price)
    {
      decimal margin = 0.00m;

      if (vatRate >= 0 && cost > 0 && price > 0)
        margin = (price - (1 + vatRate / 100) * cost) / price * 100;

      return Math.Round(margin, 2);
    }

    public static decimal GetCost(decimal packCost, decimal packQty)
    {
      decimal cost = 0.00m;

      if (packCost > 0 && packQty > 0)
        cost = packCost / packQty;

      return Math.Round(cost, 2);
    }

    public static decimal GetPackCost(decimal unitCost, decimal packQty)
    {
      decimal packCost =  unitCost * packQty;
      return Math.Round(packCost, 2);
    }

    // public static decimal GetPrice(decimal vatRate, decimal cost, decimal margin)
    // {
    //   decimal price = 0.00m;

    //   if (vatRate >= 0 && cost > 0)
    //     price = (1 + vatRate / 100) * cost / (1 - (margin / 100));

    //   return Math.Round(price, 2);
    // }

    // public static decimal GetCostIncVat(decimal vatRate, decimal cost)
    // {
    //   return Math.Round(cost * (1 + vatRate / 100), 2);
    // }
  }
}