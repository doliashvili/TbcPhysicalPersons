namespace Tbc.PhysicalPersonsDirectory.Application.Exceptions;

public class ObjectNotFoundException : Exception
{
    public int Code { get; set; } = ExceptionLocalizeConstants.ObjectNotFoundExceptionCode;
    public string Title { get; set; } = ExceptionLocalizeConstants.ObjectNotFoundExceptionTitle;

    public ObjectNotFoundException(string message) : base(message)
    {
    }

    public ObjectNotFoundException() : base()
    {
    }

    public ObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}