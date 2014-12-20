using GitCafeCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace GitCafeCommon.Dao
{
    public interface IGitCafeRepositoryDao
    {
        List<GitCafeRepository> Load();

        void Add(GitCafeRepository repository);

        void Update(GitCafeRepository repository);

        void Delete(int id);
    }

    public class GieCafeRepositoryDao : IGitCafeRepositoryDao
    {
        private SQLiteHelper dbHelper;
        static readonly string LoadQuery = "select * from repository order by name";
        static readonly string AddQuery = "insert into repository(name,workdir,gitsource) values(@name,@workdir,@gitsource)";
        static readonly string UpdateQuery = "update repository set name = @name,workdir = @workdir,gitsource = @gitsource where id = @id";
        static readonly string DeleteQuery = "delete from repository where id = @id";
        
        public GieCafeRepositoryDao(SQLiteHelper db)
        {
            this.dbHelper = db;
        }
        public List<GitCafeRepository> Load()
        {
            List<GitCafeRepository> ltResult = new List<GitCafeRepository>();

            var dbRepository = dbHelper.ExecuteDataTable(LoadQuery);
            if (dbRepository != null && dbRepository.Rows.Count > 0)
            {
                foreach (DataRow row in dbRepository.Rows)
                {
                    var ben = new GitCafeRepository();
                    ben.SetData(row);
                    ltResult.Add(ben);
                }               
                
            }

            return ltResult;
        }

        public void Add(GitCafeRepository repository)
        {
            dbHelper.ExecuteNonQuery(AddQuery,
                new SQLiteParameter("@name", repository.Name),
                new SQLiteParameter("@workdir", repository.WorkPath),
                new SQLiteParameter("@gitsource", repository.GitSource));
        }

        public void Update(GitCafeRepository repository)
        {
            dbHelper.ExecuteNonQuery(UpdateQuery,
                new SQLiteParameter("@name", repository.Name),
                new SQLiteParameter("@workdir", repository.WorkPath),
                new SQLiteParameter("@gitsource", repository.GitSource),
                new SQLiteParameter("@id", repository.Id));
        }

        public void Delete(int id)
        {
            dbHelper.ExecuteNonQuery(DeleteQuery, new SQLiteParameter("@id", id));
        }
    }

}
