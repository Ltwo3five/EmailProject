using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
// the flow of the operation is that generateOPTEmail will be called and once it has been sent and assuming 
// that it has been checked that the user has received the email, the checkOTP method will be called probably
// by an event handler that registers the user's input.
// If checkOpt fails then generateOPTEmail will get called again if the user chooses to do so.
// For the time being I have added a function that calls Console.ReadLine instead in readOPT() method in the custom IOStream class.
// Ideally checkOPT should be called synchronously in the same method that opt is generated and sent and the
// opt string should not be put in a class variable for thread safety.
// Testing : unit tests can be written with parameters and/or a simulation of circumstances(like for example if 
// the user takes longer 1 minute to give a correct input) that will result in different messages would be used for each test case.
// Try to think of various scenarios and parameters and their anticipated results, with each unit test covering one specific result
// or branch in order to achieve high test coverage.

// The program can be run and different inputs can be typed into the command line. Because there is no email being sent at the moment
EmailOTP emailOPT = new EmailOTP();
string msg = emailOPT.GenerateOTPEmail("Wong@gmail.dso.org.sg");
Console.WriteLine(msg);
IOStream stream = new IOStream();
string res = emailOPT.CheckOTP(stream);
Console.WriteLine(res);

app.Run();
