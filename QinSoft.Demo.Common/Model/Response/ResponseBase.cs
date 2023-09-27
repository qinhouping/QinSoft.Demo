using System;
using System.Collections.Generic;
using System.Text;

namespace QinSoft.Demo.Common.Model.Response
{
    public class ResponseBase
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }
    }

    public class ResponseBase<T> : ResponseBase
    {
        public T Data { get; set; }
    }

    public class PageResponseBase<T> : ResponseBase
    {
        public T[] Data { get; set; }

        public long Total { get; set; }
    }
}
