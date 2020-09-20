namespace DeveloperChallenge.Api.DTO
{
    public class DefaultResponse
    {
        public System.Net.HttpStatusCode HttpStatus { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }

        public DefaultResponse(System.Net.HttpStatusCode status, string msg)
        {
            HttpStatus = status;
            Message = msg;
        }

        public DefaultResponse(System.Net.HttpStatusCode status, string msg = "", object data = null)
        {
            HttpStatus = status;
            Message = msg;
            Data = data;
        }
    }
}
