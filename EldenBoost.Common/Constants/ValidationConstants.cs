namespace EldenBoost.Common.Constants
{
    public static class ValidationConstants
    {
        public static class ApplicationUserValidations
        {
            public const int NicknameMinLength = 3;
            public const int NicknameMaxLength = 64;

            public const int ProfilePictureMaxLength = 2048;
        }

        public static class ServiceValidations
        {
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 80;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 248;

            public const int ImageUrlMaxLength = 2048;
        }

        public static class ServiceOptionValidations
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 80;
        }

        public static class BoosterValidations
        {
            public const int CountryMinLength = 2;
            public const int CountryMaxLength = 56;

            public const int ExperienceMinLength = 2;
            public const int ExperienceMaxLength = 200;
        }

        public static class PlatformValidations
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 11;
        }

        public static class ApplicationValidations
        {
            public const int CountryMinLength = 2;
            public const int CountryMaxLength = 56;

            public const int ExperienceMinLength = 2;
            public const int ExperienceMaxLength = 200;
        }
    }
}
