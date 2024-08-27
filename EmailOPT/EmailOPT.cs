using System.ComponentModel;
using System.Diagnostics;

class EmailOTP{
    const string domain = ".dso.org.sg";
    string optCode = "";

    //dummy method assuming it was already created
    bool sendEmail(string email_address, string email_body){
        return true;
    }
    string GenerateOPT(){
        return (OPTGenerator.NextInt() % 1000000).ToString("000000");;
    }

   //Assuming that GenerateOTPEmail will be called again by the user if they do not supply opt within a minute. 
    public string GenerateOTPEmail(string user_email){
        int start = user_email.Length - domain.Length;
        bool validDomain = user_email.Contains(domain) && start<=user_email.Length-1 && user_email.Substring(start).Equals(domain);
        if(!validDomain) return EmailStatusMessage.GetErrorMessage(EmailStatus.STATUS_EMAIL_INVALID);
        optCode = GenerateOPT();
        string email_body = string.Format("Your OTP Code is {0}. The code is valid for 1 minute",optCode);
        bool messageSent = sendEmail(user_email,email_body);
        return messageSent?  EmailStatusMessage.GetErrorMessage(EmailStatus.STATUS_EMAIL_OK) : EmailStatusMessage.GetErrorMessage(EmailStatus.STATUS_EMAIL_FAIL);
    }

    //Assuming that IOStream is a custom method, a custom method was created;
    public string CheckOTP(IOStream input){
        TimeSpan timeout = TimeSpan.FromSeconds(60);
        var timer = new Stopwatch();
        timer.Start();
        TimeSpan timeTaken = timer.Elapsed;
        Task<string> optInput = input.readOTP();
        if(!optInput.Wait(timeout)) return OPTStatusMessage.GetErrorMessage(OPTStatus.STATUS_OTP_TIMEOUT);
        int retries = 0;
        while(retries<10 && timeTaken.TotalSeconds <= 60){
            if(!optInput.Result.Equals(optCode)){
                    optInput = input.readOTP();
                    if(!optInput.Wait(timeout))break;
                    retries++; 
            } 
            else {
                return OPTStatusMessage.GetErrorMessage(OPTStatus.STATUS_OTP_OK);
            }
                
        }
        if(timeTaken.TotalSeconds <= 60) return OPTStatusMessage.GetErrorMessage(OPTStatus.STATUS_OTP_FAIL);
        timer.Stop();
        return OPTStatusMessage.GetErrorMessage(OPTStatus.STATUS_OTP_TIMEOUT);
    }
}

    



