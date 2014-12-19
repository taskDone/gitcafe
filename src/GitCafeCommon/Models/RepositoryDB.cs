using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitCafeCommon.Models
{
    public class RepositoryDB
    {
        public string Name { get; set; }
        public string LocalPath { get; set; }
        public string GitPath { get; set; }
    }
}
