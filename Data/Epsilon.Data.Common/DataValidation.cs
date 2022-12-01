namespace Epsilon.Data.Common
{
    public static class DataValidation
    {
        // TODO: add remaining data validation constants
        public static class ApplicationUser
        {
        }

        public static class Category
        {
            public const int CategoryNameMaxLength = 50;
        }

        public static class Computer
        {
            public const int ComputerNameMaxLength = 50;

            public const int ComputerModelMaxLength = 50;

            public const int ComputerDescriptionMaxLength = 50;
        }

        public static class Customer
        {
        }

        public static class Editor
        {
        }

        public static class Manufacturer
        {
            public const int ManufacturerNameMaxLength = 50;

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
