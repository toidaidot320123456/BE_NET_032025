namespace DataAcccess.ResponseData
{
    public class Response
    {
        public Response()
        {
        }
        public Response(bool success, string message, int status)
        {
            this.Success = success;
            this.Message = message;
            this.Status = status;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }
    public class Response<T> : Response
    {
        public Response() : base()
        {

        }
        public Response(T data) : base()
        {
            this.Data = data;
        }
        public Response(bool success, string message, int status, T data) : base(success, message, status)
        {
            this.Data = data;
        }
        public T Data { get; set; }
    }
    public class ResponseList<T, IdT> : Response
    {
        public ResponseList(bool success, string message, int status, List<T> data, IdT total) : base(success, message, status)
        {
            this.Data = data;
            this.Total = total;
        }
        public List<T> Data { get; set; }
        public IdT Total { get; set; }
    }
    public static class MessageResponse
    {
        public const string SuccessAction = "Action completed successfully.";
        public const string FailureAction = "Action failed.";
        public const string InvalidToken = "The token or refresh token is invalid.";
        public const string NotEnoughStock = "Not enough stock";

    }

    public static class StatusResponse
    {
        public const int Success = 200;
        public const int Failure = 500;
        public const int BadRequest = 400;
        public const int Unauthorized = 401;
        public const int NotFound = 404;
    }

}
