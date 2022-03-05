namespace domain.exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message)
            : base(message) 
        {
        }
    }
}
