namespace SellingMobileApp.Common
{
    public class AppConstants
    {
        //CreatingListing
        public const int TitleMinLength = 20;
        public const int TitleMaxLength = 100;
        public const int DescriptionMinLength = 20;
        public const int DescriptionMaxLength = 1000;

        //User
        public const int EmailMinLength = 6;
        public const int EmailMaxLength = 256;

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
