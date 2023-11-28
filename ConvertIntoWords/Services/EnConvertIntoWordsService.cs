using ConvertIntoWords.Data.GrammaticalNumber.Interfaces;
using ConvertIntoWords.Data.NumberValues.Interfaces;
using ConvertIntoWords.Data.OrderOfMagnitude.Interfaces;
using ConvertIntoWords.Helpers.Enums;
using ConvertIntoWords.Helpers;
using ConvertIntoWords.Services.Interfaces;
using ConvertIntoWords.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConvertIntoWords.Services
{
    public class EnConvertIntoWordsService : IEnConvertIntoWordsService
    {
        private readonly IEnDollarGrammaticalNumber enDollarGrammaticalNumber;
        private readonly IEnNumberValues enNumberValues;
        private readonly IEnOrderOfMagnitude enOrderOfMagnitude;

        public EnConvertIntoWordsService(IEnDollarGrammaticalNumber enDollarGrammaticalNumber, IEnNumberValues enNumberValues, IEnOrderOfMagnitude enOrderOfMagnitude)
        {
            this.enDollarGrammaticalNumber = enDollarGrammaticalNumber ?? throw new ArgumentNullException(nameof(enDollarGrammaticalNumber));
            this.enNumberValues = enNumberValues ?? throw new ArgumentNullException(nameof(enNumberValues));
            this.enOrderOfMagnitude = enOrderOfMagnitude ?? throw new ArgumentNullException(nameof(enOrderOfMagnitude));
        }

        public ResultDto Convert(decimal value)
        {
            var resultDto = new ResultDto();

            try
            {
                resultDto.ResultValue = ConvertIntoWords(value);
                resultDto.IsSuccess = true;
            }
            catch(Exception ex)
            {
                resultDto.ResultValue = ex.Message;
            }

            return resultDto;
        }

        private string ConvertIntoWords(decimal value)
        {
            if (value < 0)
                throw new ArgumentException("The value must be positive");

            int number = (int)Math.Floor(value);
            decimal fractionalPart = (value - number) * 100;

            ValidateNumber(number);
            ValidateFractionalPart(fractionalPart);

            return ConvertIntoWords(number, (int)fractionalPart);
        }

        private void ValidateNumber(int number)
        {
            if (number > 999999999)
                throw new ArgumentException("Maximum value of number is 999999999");
        }

        private void ValidateFractionalPart(decimal value)
        {
            if (value == 0)
                return;

            int number = (int)Math.Floor(value);
            decimal fractionalPart = (value - number) * 100;

            if (fractionalPart >0)
            {
                throw new ArgumentException("Maximum value of fractional part is 99");
            }
        }

        private string ConvertIntoWords(int number, int fractionalPart)
        {
            var numberString = GetStringValue(number);
            var currencyName = enDollarGrammaticalNumber.GetCurrencyName(number);

            string result = $"{numberString} {currencyName}";

            if (fractionalPart > 0)
            {
                var fractionalPartString = GetStringValue(fractionalPart);
                var fractionalPartName = enDollarGrammaticalNumber.GetFractionalPartName(fractionalPart);

                result += $" and {fractionalPartString} {fractionalPartName}";
            }

            return result;
        }

        private string? GetStringValue(int number)
        {
            if (number < 0)
            {
                return string.Empty;
            }

            if (number < 20)
            {
                return enNumberValues.GetNumberName(number);
            }

            if (number < 100)
            {
                return UpTo99(number);
            }

            if (number < 1000)
            {
                return UpTo999(number);
            }

            return UpToMax(number, OrderOfMagnitude.ones);
        }

        private string? UpTo99(int number)
        {
            if (number == 0)
                return string.Empty;

            var tens = number / 10 * 10;
            var units = number % 10;

            var result = enNumberValues.GetNumberName(tens);

            if (units > 0)
            {
                var unitsString = enNumberValues.GetNumberName(units);
                result += $"-{unitsString}";
            }

            return result;
        }

        private string UpTo999(int number)
        {
            if (number == 0)
                return string.Empty;

            var hundreds = number / 100;
            var tens = number % 100;

            var orderValue = enOrderOfMagnitude.GetOrderValue(OrderOfMagnitude.hundreds);
            var hundredsString = enNumberValues.GetNumberName(hundreds);
            var tensString = UpTo99(tens);

            return $"{hundredsString} {orderValue}" + (string.IsNullOrWhiteSpace(tensString) ? "" : $" {tensString}");
        }

        private string UpToMax(int number, OrderOfMagnitude order, string result = "")
        {
            var remainingNumber = number / 1000;
            var hundreds = number % 1000;

            if (hundreds > 0)
            {
                var orderValue = enOrderOfMagnitude.GetOrderValue(order);
                var valueString = GetStringValue(hundreds);

                result = $"{valueString}" + (string.IsNullOrWhiteSpace(orderValue) ? "" : $" {orderValue}") + (string.IsNullOrWhiteSpace(result) ? "" : $" {result}");
            }

            if (remainingNumber == 0)
                return result;

            order += MathConstants.multiples;

            return UpToMax(remainingNumber, order, result);
        }
    }
}
