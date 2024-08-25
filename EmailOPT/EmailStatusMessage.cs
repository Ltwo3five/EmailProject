using System.ComponentModel;

enum EmailStatus {
        [Description("email containing OTP has been sent successfully.")]
        STATUS_EMAIL_OK,
         [Description("email address does not exist or sending to the email has failed.")]
        STATUS_EMAIL_FAIL,
         [Description("email address is invalid.")]
        STATUS_EMAIL_INVALID
    }

static class EmailStatusMessage
{
    public static string GetErrorMessage(this EmailStatus val)
    {  
        Type? type = val.GetType();
        var field = type.GetField(val.ToString());
        if(field == null) return string.Empty;
        DescriptionAttribute[] customAttributes = (DescriptionAttribute[])field
        .GetCustomAttributes(typeof(DescriptionAttribute), false);
        return customAttributes .Length > 0 ? customAttributes[0].Description : string.Empty;
    }
    
}