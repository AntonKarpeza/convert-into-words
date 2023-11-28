using ConvertIntoWords.Data.GrammaticalNumber.Interfaces;
using ConvertIntoWords.Data.NumberValues.Interfaces;
using ConvertIntoWords.Data.OrderOfMagnitude.Interfaces;
using ConvertIntoWords.Helpers.Enums;
using ConvertIntoWords.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ConvertIntoWords.Tests.Services
{
    [TestClass]
    public class EnConvertIntoWordsServiceTests
    {
        private Mock<IEnDollarGrammaticalNumber> enDollarGrammaticalNumberMock;
        private Mock<IEnNumberValues> enNumberValuesMock;
        private Mock<IEnOrderOfMagnitude> enOrderOfMagnitudeMock;
        private EnConvertIntoWordsService convertIntoWordsService;

        [TestInitialize]
        public void Initialize()
        {
            enDollarGrammaticalNumberMock = new Mock<IEnDollarGrammaticalNumber>();
            enNumberValuesMock = new Mock<IEnNumberValues>();
            enOrderOfMagnitudeMock = new Mock<IEnOrderOfMagnitude>();

            convertIntoWordsService = new EnConvertIntoWordsService(enDollarGrammaticalNumberMock.Object, enNumberValuesMock.Object, enOrderOfMagnitudeMock.Object);
        }

        [TestMethod]
        public void ConvertIntoWords_ShouldReturnZero_WhenInputIsZero()
        {
            // Arrange
            decimal value = 0;
            enDollarGrammaticalNumberMock.Setup(m => m.GetCurrencyName(It.IsAny<int>())).Returns("dollars");
            enNumberValuesMock.Setup(m => m.GetNumberName(It.IsAny<int>())).Returns("zero");

            // Act
            var result = convertIntoWordsService.Convert(value);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("zero dollars", result.ResultValue);
        }

        [TestMethod]
        public void ConvertIntoWords_ShouldReturnCorrectString_WhenInputIsOne()
        {
            // Arrange
            decimal value = 1;
            enDollarGrammaticalNumberMock.Setup(m => m.GetCurrencyName(It.IsAny<int>())).Returns("dollar");
            enNumberValuesMock.Setup(m => m.GetNumberName(It.IsAny<int>())).Returns("one");

            // Act
            var result = convertIntoWordsService.Convert(value);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("one dollar", result.ResultValue);
        }

        [TestMethod]
        public void ConvertIntoWords_ShouldReturnCorrectString_WhenInputIs25Point1()
        {
            // Arrange
            decimal value = 25.1m;
            enDollarGrammaticalNumberMock.Setup(m => m.GetCurrencyName(25)).Returns("dollars");
            enNumberValuesMock.Setup(m => m.GetNumberName(20)).Returns("twenty");
            enNumberValuesMock.Setup(m => m.GetNumberName(5)).Returns("five");
            enNumberValuesMock.Setup(m => m.GetNumberName(10)).Returns("ten");
            enDollarGrammaticalNumberMock.Setup(m => m.GetFractionalPartName(10)).Returns("cents");

            // Act
            var result = convertIntoWordsService.Convert(value);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("twenty-five dollars and ten cents", result.ResultValue);
        }

        [TestMethod]
        public void ConvertIntoWords_ShouldReturnCorrectString_WhenInputIs45100()
        {
            // Arrange
            decimal value = 45100;
            enDollarGrammaticalNumberMock.Setup(m => m.GetCurrencyName(It.IsAny<int>())).Returns("dollars");
            enNumberValuesMock.Setup(m => m.GetNumberName(40)).Returns("forty");
            enNumberValuesMock.Setup(m => m.GetNumberName(5)).Returns("five");
            enNumberValuesMock.Setup(m => m.GetNumberName(1)).Returns("one");
            enOrderOfMagnitudeMock.Setup(m => m.GetOrderValue(OrderOfMagnitude.hundreds)).Returns("hundred");
            enOrderOfMagnitudeMock.Setup(m => m.GetOrderValue(OrderOfMagnitude.thousands)).Returns("thousand");

            // Act
            var result = convertIntoWordsService.Convert(value);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("forty-five thousand one hundred dollars", result.ResultValue);
        }

        [TestMethod]
        public void ConvertIntoWords_ShouldReturnCorrectString_WhenInputIs80000()
        {
            // Arrange
            decimal value = 80000;
            enDollarGrammaticalNumberMock.Setup(m => m.GetCurrencyName(It.IsAny<int>())).Returns("dollars");
            enNumberValuesMock.Setup(m => m.GetNumberName(80)).Returns("eighty");
            enOrderOfMagnitudeMock.Setup(m => m.GetOrderValue(OrderOfMagnitude.thousands)).Returns("thousand");

            // Act
            var result = convertIntoWordsService.Convert(value);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("eighty thousand dollars", result.ResultValue);
        }

        [TestMethod]
        public void ConvertIntoWords_ShouldReturnCorrectString_WhenInputIsMaxValue()
        {
            // Arrange
            decimal value = 999999999.99m;
            enDollarGrammaticalNumberMock.Setup(m => m.GetCurrencyName(999999999)).Returns("dollars");
            enDollarGrammaticalNumberMock.Setup(m => m.GetFractionalPartName(99)).Returns("cents");
            enNumberValuesMock.Setup(m => m.GetNumberName(9)).Returns("nine");
            enNumberValuesMock.Setup(m => m.GetNumberName(90)).Returns("ninety");
            enOrderOfMagnitudeMock.Setup(m => m.GetOrderValue(OrderOfMagnitude.hundreds)).Returns("hundred");
            enOrderOfMagnitudeMock.Setup(m => m.GetOrderValue(OrderOfMagnitude.thousands)).Returns("thousand");
            enOrderOfMagnitudeMock.Setup(m => m.GetOrderValue(OrderOfMagnitude.millions)).Returns("million");

            // Act
            var result = convertIntoWordsService.Convert(value);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents", result.ResultValue);
        }

        [TestMethod]
        public void Convert_NegativeValue_ReturnsErrorMessage()
        {
            // Arrange
            decimal negativeValue = -100;

            // Act
            var result = convertIntoWordsService.Convert(negativeValue);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNotNull(result.ResultValue);
            StringAssert.Contains(result.ResultValue, "The value must be positive");
        }

        [TestMethod]
        public void Convert_ValueGreaterThanMax_ReturnsErrorMessage()
        {
            // Arrange
            decimal tooLargeValue = 1000000000;

            // Act
            var result = convertIntoWordsService.Convert(tooLargeValue);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNotNull(result.ResultValue);
            StringAssert.Contains(result.ResultValue, "Maximum value of number is 999999999");
        }

        [TestMethod]
        public void Convert_FractionalPartGreaterThanMax_ReturnsErrorMessage()
        {
            // Arrange
            decimal valueWithLargeFraction = 123456789.123m;

            // Act
            var result = convertIntoWordsService.Convert(valueWithLargeFraction);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsNotNull(result.ResultValue);
            StringAssert.Contains(result.ResultValue, "Maximum value of fractional part is 99");
        }

    }
}
