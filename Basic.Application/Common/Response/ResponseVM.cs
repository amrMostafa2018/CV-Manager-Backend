using Basic.Application.Common.Enums;
using System.Net;

namespace Basic.Application.Common.Response
{
    public class ResponseVM<T>
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public ResponseMessageStatusEnum OperationStatus { get; set; } = ResponseMessageStatusEnum.Success;
        public string OperationMessage { get; set; }
        public T Data { get; set; }
        public object Error { get; set; }
    }

    public class ResponseVM
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string OperationMessage { get; set; }
        public ResponseMessageStatusEnum OperationStatus { get; set; }
        public object Data { get; set; }
        public object Error { get; set; }


    }
}
