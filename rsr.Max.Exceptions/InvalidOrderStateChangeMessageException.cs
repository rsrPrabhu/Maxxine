using Cortside.Common.Messages.MessageExceptions;
namespace rsr.Max.Exceptions;

public class InvalidOrderStateChangeMessageException : UnprocessableEntityResponseException {
    public InvalidOrderStateChangeMessageException() : base("Current state does not allow requested operation.") {
    }

    public InvalidOrderStateChangeMessageException(string message) : base($"Current state does not allow requested operation. {message}") {
    }

    public InvalidOrderStateChangeMessageException(string message, System.Exception exception) : base(message, exception) {
    }
}

