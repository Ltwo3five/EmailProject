using System.ComponentModel;

enum OPTStatus {
        [Description("OTP is valid and checked.")]
        STATUS_OTP_OK,
        [Description("OTP is wrong after 10 tries.")]
        STATUS_OTP_FAIL,
        [Description("timeout after 1 min.")]
        STATUS_OTP_TIMEOUT
    }

static class OPTStatusMessage
{
    public static string GetErrorMessage(this OPTStatus val)
    {  
        Type? type = val.GetType();
        var field = type.GetField(val.ToString());
        if(field == null) return string.Empty;
        DescriptionAttribute[] customAttributes = (DescriptionAttribute[])field
        .GetCustomAttributes(typeof(DescriptionAttribute), false);
        return customAttributes .Length > 0 ? customAttributes[0].Description : string.Empty;
    }
    
}