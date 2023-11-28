namespace ConvertIntoWords.Data.OrderOfMagnitude.Interfaces
{
    using ConvertIntoWords.Helpers.Enums;

    public interface IEnOrderOfMagnitude
    {
        string GetOrderValue(OrderOfMagnitude order);
    }
}
