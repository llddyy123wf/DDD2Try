namespace Framework.Infrastructure.Result
{
    public class MessageResult
    {
        public MessageResult() { }

        public MessageResult(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class MessageResult<T> : MessageResult
    {
        public MessageResult() { }
        public MessageResult(bool success, string message, T data)
            : base(success, message)
        {
            this.Data = data;
        }
        public MessageResult(bool success, string message)
            : this(success, message, default(T)) { }

        public T Data { get; set; }
    }
}