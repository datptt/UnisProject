using System;
namespace Unis.API
{
    public class ServiceResult
    {
        public object Data { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsSuccess { get; set; }

        public void ErrorHandle(Exception e)
        {
            this.IsSuccess = false;
            this.Data = e.Message.ToString();
        }
    }
}
