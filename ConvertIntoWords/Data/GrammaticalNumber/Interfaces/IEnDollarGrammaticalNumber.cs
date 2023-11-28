namespace ConvertIntoWords.Data.GrammaticalNumber.Interfaces
{
    public interface IEnDollarGrammaticalNumber
    {
        string GetCurrencyName(int number);
        string GetFractionalPartName(int number);
    }
}
