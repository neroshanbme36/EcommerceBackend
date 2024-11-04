namespace Application.Constants
{
    public static class RequestHeaderCodes
    {
        public const string AUTHORIZATION = "Authorization";
        public const string PRODUCT_KEY = "Product-Key"; // middleware product key
        public const string DEVICE_ID = "x-device-id";
        // public const string EPOS_API_KEY = "x-api-key"; // OLD
        public const string EPOS_API_KEY = "x-product-key"; 
    }
}