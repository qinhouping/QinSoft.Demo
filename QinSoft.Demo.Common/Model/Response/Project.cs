using System;
using System.Collections.Generic;
using System.Text;

namespace QinSoft.Demo.Common.Model.Response
{
    public class Project
    {
        public string Id { get; set; }

        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
