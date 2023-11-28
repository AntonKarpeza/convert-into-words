using ConvertIntoWords.Services.Interfaces;
using ConvertIntoWords.Services;
using ConvertIntoWords.Data.GrammaticalNumber.Interfaces;
using ConvertIntoWords.Data.GrammaticalNumber;
using ConvertIntoWords.Data.NumberValues.Interfaces;
using ConvertIntoWords.Data.NumberValues;
using ConvertIntoWords.Data.OrderOfMagnitude.Interfaces;
using ConvertIntoWords.Data.OrderOfMagnitude;

namespace ConvertIntoWords.IoC
{
    public static class TransformToStringConfiguration
    {
        public static IServiceCollection AddConfig(this IServiceCollection services)
        {
            RegisterServices(services);
            RegisterDataGrammaticalNumber(services);
            RegisterDataNumberValues(services);
            RegisterDataOrderOfMagnitude(services);

            return services;
        }

        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IEnConvertIntoWordsService, EnConvertIntoWordsService>();
        }

        private static void RegisterDataGrammaticalNumber(this IServiceCollection services)
        {
            services.AddScoped<IEnDollarGrammaticalNumber, EnDollarGrammaticalNumber>();
        }

        private static void RegisterDataNumberValues(this IServiceCollection services)
        {
            services.AddScoped<IEnNumberValues, EnNumberValues>();
        }

        private static void RegisterDataOrderOfMagnitude(this IServiceCollection services)
        {
            services.AddScoped<IEnOrderOfMagnitude, EnOrderOfMagnitude>();
        }
    }
}

