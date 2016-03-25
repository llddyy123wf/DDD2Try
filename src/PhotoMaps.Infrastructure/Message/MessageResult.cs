namespace Framework.Infrastructure
{
    public class MessageResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public MessageResult()
        {
        }

        public MessageResult(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }
    }

    public class MessageResult<T> : MessageResult
    {
        public MessageResult()
        {
        }

        public MessageResult(bool success, string message, T data)
            : base(success, message)
        {
            this.Data = data;
        }

        public MessageResult(bool success, string message)
            : this(success, message, default(T)) { }

        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}