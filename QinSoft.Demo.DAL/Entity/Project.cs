using MongoDB.Bson.Serialization.Attributes;
using Nest;
using QinSoft.Core.Data.Elasticsearch;
using QinSoft.Core.Data.MongoDB;
using QinSoft.Core.Data.Solr;
using SolrNet.Attributes;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace QinSoft.Demo.DAL.Entity
{
    [SugarTable("project")]
    public class Project
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        [SugarColumn(ColumnName = "project_name")]
        public string ProjectName { get; set; }

        [SugarColumn(ColumnName = "project_description")]
        public string ProjectDescription { get; set; }

        [SugarColumn(ColumnName = "create_time")]
        public DateTime? CreateTime { get; set; }

        [SugarColumn(ColumnName = "update_time")]
        public DateTime? UpdateTime { get; set; }
    }
}
