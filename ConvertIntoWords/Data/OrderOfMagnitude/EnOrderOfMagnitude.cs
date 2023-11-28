namespace ConvertIntoWords.Data.OrderOfMagnitude
{
    using ConvertIntoWords.Data.OrderOfMagnitude.Interfaces;
    using ConvertIntoWords.Helpers.Enums;

    public class EnOrderOfMagnitude : IEnOrderOfMagnitude
    {
        private static readonly Dictionary<OrderOfMagnitude, string> orderOfMagnitude = new Dictionary<OrderOfMagnitude, string>
        {
            { OrderOfMagnitude.ones, "" },
            { OrderOfMagnitude.hundreds, "hundred" },
            { OrderOfMagnitude.thousands, "thousand" },
            { OrderOfMagnitude.millions, "million" }
        };

        public string GetOrderValue(OrderOfMagnitude order)
        {
            if (orderOfMagnitude.TryGetValue(order, out var outputResult))
            {
                if (outputResult == null)
                {
                    throw new ArgumentNullException("The result is null.");
                }

                return outputResult;
            }

            throw new ArgumentNullException($"Order of magnitude {order} not found in the dictionary.");
        }
    }
}
