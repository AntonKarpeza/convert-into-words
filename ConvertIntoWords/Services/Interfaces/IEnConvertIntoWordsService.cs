using ConvertIntoWords.Models;

namespace ConvertIntoWords.Services.Interfaces
{
    public interface IEnConvertIntoWordsService
    {
        ResultDto Convert(decimal value);
    }
}
