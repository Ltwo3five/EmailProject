class IOStream{

//Dummy method
public Task<string> readOTP(){
    return Task.Run(() => {
        return Console.ReadLine();
        });

    }
}

