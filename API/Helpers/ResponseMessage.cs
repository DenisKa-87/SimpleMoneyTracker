namespace API.Helpers
{
    public class ResponseMessage
    {
        public ResponseMessage(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
