using ConvertIntoWords.Data.GrammaticalNumber.Interfaces;

namespace ConvertIntoWords.Data.GrammaticalNumber
{
    public class EnDollarGrammaticalNumber : IEnDollarGrammaticalNumber
    {
        private static readonly string Dollar = "dollar";
        private static readonly string Dollars = "dollars";
        private static readonly string Cent = "cent";
        private static readonly string Cents = "cents";

        public string GetCurrencyName(int number)
        {
            return number == 1 ? Dollar : Dollars;
        }

        public string GetFractionalPartName(int number)
        {
            return number == 1 ? Cent : Cents;
        }
    }
}
