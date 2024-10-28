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
    }
}
