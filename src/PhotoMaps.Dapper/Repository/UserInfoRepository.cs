using Dapper;
using DapperExtensions;
using PhotoMaps.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using PhotoMaps.Domain.IRepository;

namespace PhotoMaps.Dapper
{
    public class UserInfoRepository : IUserInfoRepository
    {
        protected IDbTransaction Transaction { get; set; }
        protected IDbConnection Context { get; set; }
        private int changes = 0;

        protected UserInfoRepository(string conn)
        {
           
            if (Transaction != null)
            {
                Context = this.Transaction.Connection;
            }
            else
            {
                Context = new SqlConnection(conn);
                Context.Open();
                Transaction = Context.BeginTransaction();
            }
        }

        public IQueryable<UserInfo> UserInfos
        {
            get { throw new System.NotImplementedException(); }
        }

        public UserInfo LoadByKey<TKey>(TKey key)
        {
            UserInfo user = this.Context.Get<UserInfo>(key);
            return user;
        }

        public void Save(UserInfo entity)
        {
            this.Context.Insert<UserInfo>(entity, this.Transaction);
            this.changes++;
        }
        public void Save(IEnumerable<UserInfo> entities)
        {
            this.Context.Insert<UserInfo>(entities, this.Transaction);
            this.changes = this.changes + entities.Count();
        }
        public void Delete(UserInfo entity)
        {
            this.Context.Delete<UserInfo>(entity, this.Transaction);
            this.changes++;
        }
        public void Update(UserInfo entity)
        {
            this.Context.Update<UserInfo>(entity, this.Transaction);
            this.changes++;
        }
        public bool Delete(Expression<Func<UserInfo, bool>> filter)
        {
            return this.Context.Delete<UserInfo>(filter);
        }
        public bool Update(Expression<Func<UserInfo, bool>> filter, Expression<Func<UserInfo, UserInfo>> update)
        {
            throw new System.NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            object changes = this.Context.ExecuteScalar(sql, parameters);
            return Convert.ToInt32(changes);
        }
        public IEnumerable<UserInfo> ExcuteSql(string sql, params object[] parameters)
        {
            return this.Context.Query<UserInfo>(sql, parameters);
        }

        public int Commit()
        {
            this.Transaction.Commit();
            this.Context.Close();
            return this.changes;
        }

        public void RollBack()
        {
            this.Transaction.Rollback();
            this.Context.Close();
        }


        int Framework.Infrastructure.Repository.IRepository<UserInfo>.Delete(Expression<Func<UserInfo, bool>> filter)
        {
            throw new NotImplementedException();
        }

        int Framework.Infrastructure.Repository.IRepository<UserInfo>.Update(Expression<Func<UserInfo, bool>> filter, Expression<Func<UserInfo, UserInfo>> update)
        {
            throw new NotImplementedException();
        }
    }
}
