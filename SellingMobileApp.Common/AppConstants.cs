namespace SellingMobileApp.Common
{
    public class AppConstants
    {
        //CreatingListing
        public const int TitleMinLength = 10;
        public const int TitleMaxLength = 100;
        public const int DescriptionMinLength = 20;
        public const int DescriptionMaxLength = 1000;
        public const decimal PriceMinValue = 5.00m;
        public const decimal PriceMaxValue = 10000.00m;

        //User
        
        public const int EmailMinLength = 6;
        public const int EmailMaxLength = 256;
        public const int UserNameMinLength = 3;
        public const int UserNameMaxLength = 50;
        public const int PhoneNumberMinLength = 4;
        public const int PhoneNumberMaxLength = 15;
        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 25;

        //PhoneModel
        public const int BrandMinLength = 3;
        public const int BrandMaxLength = 50;
        public const int ModelMinLength = 3;
        public const int ModelMaxLength = 50;
        public const int ManufactureYearMinLength = 2000;
        public const int ManufactureYearMaxLength = 2100;
        public const int StorageCapacityMinLength = 1;
        public const int StorageCapacityMaxLength = 1024;
        public const int RamCapacityMinLength = 1;
        public const int RamCapacityMaxLength = 512;
        public const int RatingMinLength = 1;
        public const int RatingMaxLength = 5;

        //Category
        public const int NameOfCategoryMinLength = 3;
        public const int NameOfCategoryMaxLength = 50;
        public const int DescriptionOfCategoryMinLength = 5;
        public const int DescriptionOfCategoryMaxLength = 250;

        //Review 
        public const int ReviewMinLength = 1;
        public const int ReviewMaxength = 6;
    }
}
