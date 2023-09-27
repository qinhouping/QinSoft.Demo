using System;
using System.Collections.Generic;
using System.Text;

namespace QinSoft.Demo.Common.Model.Request
{
    public class ReqeuestBase
    {
    }

    public class PageRequestBase
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
