namespace TestNinja.Fundamentals;

public class ErrorLogger
{
    public string LastError { get; set; }

    public event EventHandler<Guid> ErrorLogged;

    public void Log(string error)
    {
        //null
        //""
        //" "
        if (string.IsNullOrWhiteSpace(error))
            throw new ArgumentNullException();

        LastError = error;

        //Write the log to a storage;
        //...

        OnErrorLogged(Guid.NewGuid());
    }

    //don't test for private of protected method
    protected virtual void OnErrorLogged(Guid errorId)
    {
        ErrorLogged?.Invoke(this, errorId);
    }
}