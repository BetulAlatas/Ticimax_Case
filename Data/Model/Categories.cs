using Dapper.Extensions.Linq.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Categories : IEntity
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        public bool Status { get; set; }
    }
}
