using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace GitCafeCommon.Models
{
    public class GitCafeRepository
    {
        public int Id { get; set; }

        public string Name { get; set; }

        private string _workPath;
        public string WorkPath
        {
            get { return _workPath; }
            set { _workPath = Path.GetFullPath(value); }
        }

        private string _gitSource;
        public string GitSource
        {
            get { return _gitSource; }
            set
            {
                _gitSource = Path.GetFullPath(value);
            }
        }

        public Repository Repository
        {
            get
            {
                if (GitSource != null)
                {
                    try
                    {
                        return new Repository(GitSource);
                    }
                    catch
                    {

                    }
                }
                return null;
            }
        }

        public void SetData(DataRow row)
        {
            if (row != null)
            {
                this.Id = Convert.ToInt32(row["id"]);
                this.Name = row["name"].ToString();
                this.WorkPath = row["workdir"].ToString();
                this.GitSource = row["gitsource"].ToString();
            }
        }
    }
}
