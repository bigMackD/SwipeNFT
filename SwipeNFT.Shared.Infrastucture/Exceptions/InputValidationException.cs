using System.ComponentModel.DataAnnotations;

namespace SwipeNFT.Shared.Infrastructure.Exceptions
{
    /// <summary>
    ///     Exception for business logic validation
    /// </summary>
    public class InputValidationException : ValidationException
    {
        public string[] Messages { get; }
        /// <summary>
        ///     Constructor that accepts error message
        /// </summary>
        /// <param name="message">The error message</param>
        public InputValidationException(string message) : base(message)
        {
            
        }

        /// <summary>
        ///     Constructor that accepts array of error messages
        /// </summary>
        /// <param name="messages">The error messages</param>
        public InputValidationException(string[] messages)
        {
            Messages = messages;
        }
    }
}
