using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsApp
{
    internal class MaterialCalculation
    {
        public static int Calculate(int productTypeID, int materialTypeID, int productCount, double parameter1, double parameter2)
        {
            ТипПродукта productType = Entities.GetContext().ТипПродукта.FirstOrDefault(t => t.ID == productTypeID);
            ТипМатериала materialType = Entities.GetContext().ТипМатериала.FirstOrDefault(t => t.ID == materialTypeID);

            if (productCount <= 0 || parameter1 <= 0 || parameter2 <= 0 || productType == null || materialType == null)
                return -1;

            float oneItemMaterial = (float)(parameter1 * parameter2 * productType.Коэффициент);
            float result = (float)(oneItemMaterial * productCount * (1 + materialType.ПроцентОтказов));
            return (int)Math.Ceiling(result);
        }
    }
}
