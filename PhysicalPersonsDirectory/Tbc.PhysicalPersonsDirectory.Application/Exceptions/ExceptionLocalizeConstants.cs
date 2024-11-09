using Microsoft.Extensions.Localization;
using Tbc.PhysicalPersonsDirectory.Application.Resources;

namespace Tbc.PhysicalPersonsDirectory.Application.Exceptions
{
    public class ExceptionLocalizeConstants
    {
        private static IStringLocalizer<ExceptionResources> _stringLocalizer;

        public static void Initialize(IStringLocalizer<ExceptionResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        public static string ObjectNotFoundExceptionTitle => _stringLocalizer["ObjectNotFoundExceptionTitle"];
        public static int ObjectNotFoundExceptionCode => 1;

        public static string ConflictExceptionTitle => _stringLocalizer["ConflictExceptionTitle"];
        public static int ConflictExceptionCode => 2;

        public static string ValidationExceptionTitle => _stringLocalizer["ValidationExceptionTitle"];
        public static int ValidationExceptionCode => 3;

        public static string ArgumentNullExceptionTitle => _stringLocalizer["ArgumentNullExceptionTitle"];
        public static int ArgumentNullExceptionCode => 4;

        public static string GlobalExceptionTitle => _stringLocalizer["GlobalExceptionTitle"];
        public static int GlobalExceptionCode => 500;
    }
}