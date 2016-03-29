namespace AppointmentControl
{
    public abstract class Constants
    {
        // Sign up for an Azure account at https://account.windowsazure.com/signup
        // Replace strings with your mobile services url and key.
        public static string AzureApplicationUrl = @"https://appointmentcontrol.azure-mobile.net/";
        public static string AzureApplicationKey = @"kaKHoQLgVKFAzPgPoVKSKeceGXXvNY61"; 

        // Constants for application
        public static string UserPropertyName = "user";

        public static string EmailRegexPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
    }
}