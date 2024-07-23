using Cortside.Common.Messages.MessageExceptions;
namespace rsr.Max.Exceptions;
public class InvalidItemMessageException: BadRequestResponseException {
    public InvalidItemMessageException() : base("Item is not valid.") {
    }

    public InvalidItemMessageException(string message) : base($"Item is not valid. {message}") {
    }

    public InvalidItemMessageException(string message, System.Exception exception) : base(message, exception) {
    }

    protected InvalidItemMessageException(string key, string property, params object[] properties) : base(key, property, properties) {
    }

    protected InvalidItemMessageException(string message, string property) : base(message, property) {
    }
}