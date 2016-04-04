namespace AppointmentControl
{
    public abstract class Constants
    {
        // Sign up for an Azure account at https://account.windowsazure.com/signup
        // Replace strings with your mobile services url and key.
        public static string AzureApplicationUrl = @"https://appointcontrol.azure-mobile.net/";
        public static string AzureApplicationKey = @"zSPzmzgfnsCvixsYXRgbmSPLWfvhDI82"; 

        // Constants for application
        public static string UserPropertyName = "user";

        public static string EmailRegexPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public static string DateFormat = "yyyy-MM-dd";
        public static string TimeFormat = "hh\\:mm\\:ss\\.ffff";
    }
}