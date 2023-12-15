namespace DeviceManagement.Exceptions
{
    public class BrandAlreadyExistsException : Exception
    {
        public BrandAlreadyExistsException()
        {
        }

        public BrandAlreadyExistsException(string message)
            : base(message)
        {
        }

        public BrandAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
