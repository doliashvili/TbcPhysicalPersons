namespace Tbc.PhysicalPersonsDirectory.Application.Exceptions;

public class ConflictException : Exception
{
    public int Code { get; set; } = ExceptionLocalizeConstants.ConflictExceptionCode;
    public string Title { get; set; } = ExceptionLocalizeConstants.ConflictExceptionTitle;

    public ConflictException(string message) : base(message)
    {
    }

    public ConflictException() : base()
    {
    }

    public ConflictException(string message, Exception innerException) : base(message, innerException)
    {
    }
}