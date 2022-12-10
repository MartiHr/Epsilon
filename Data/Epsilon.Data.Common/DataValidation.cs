namespace Epsilon.Data.Common
{
    public static class DataValidation
    {
        // TODO: add remaining data validation constants
        public static class ApplicationUser
        {
            public const int ApplicationUserUsernameMinLength = 5;
            public const int ApplicationUserUsernameMaxLength = 20;

            public const int ApplicationUserEmailAddressMinLength = 10;
            public const int ApplicationUserEmailAddressMaxLength = 60;

            public const string ApplicationUserConfirmPasswordErrorMessage = "Password and ConfirmPassword do not match";
        }

        public static class Category
        {
            public const int CategoryNameMinLength = 5;
            public const int CategoryNameMaxLength = 50;
        }

        public static class Computer
        {
            public const int ComputerNameMinLength = 3;
            public const int ComputerNameMaxLength = 50;

            public const int ComputerModelMinLength = 5;
            public const int ComputerModelMaxLength = 50;

            public const string ComputerPriceMinValue = "300";
            public const string ComputerPriceMaxValue = "15000";

            public const int ComputerDescriptionMinLength = 20;
            public const int ComputerDescriptionMaxLength = 500;
        }

        public static class Customer
        {
        }

        public static class Editor
        {
        }

        public static class Manufacturer
        {
            public const int ManufacturerNameMinLength = 3;
            public const int ManufacturerNameMaxLength = 50;

            public const int ManufacturerCountryMinLength = 3;
            public const int ManufacturerCountryMaxLength = 60;
        }

        public static class Order
        {
        }

        public static class Part
        {
            public const int PartTypeMaxLength = 50;

            public const int PartModelMaxLength = 60;

            public const int PartDescriptionMaxLength = 500;
        }

        public static class Warranty
        {
        }
    }
}
