using Dapper;
using DapperExtensions;
using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using Framework.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PhotoMaps.Dapper
{
    public abstract class DbContext : IContext
    {
        private readonly IDapperImplementor dapper;
        private DbModelBuilder builder;
        private IDbConnection connection;
        private IDbTransaction transaction;
        private bool isTransactionStarted;
        private int changes;

        public int Timeout { get; protected set; }
        protected virtual void OnModelCreating(DbModelBuilder builder)
        {
        }

        protected abstract IDbConnection CreateConnection();
        public DbContext()
        {
            this.changes = 0;
            this.Timeout = 1000;
            this.isTransactionStarted = false;


            this.builder = new DbModelBuilder();
            this.OnModelCreating(this.builder);

            var config = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new SqlServerDialect());
            SqlGeneratorImpl sqlGenerator = new SqlGeneratorImpl(config);
            foreach (var mapper in this.builder.Mappers)
            {
                sqlGenerator.Insert(mapper);
            }

            this.dapper = new DapperImplementor(sqlGenerator);

            this.connection = CreateConnection();
            this.connection.Open();
        }

        public T LoadByKey<T, TKey>(TKey key) where T : class
        {
            T user = this.dapper.Get<T>(this.connection, key, this.transaction, this.Timeout);
            return user;
        }

        public void Insert<T>(T entity) where T : class
        {
            this.dapper.Insert<T>(this.connection, entity, this.transaction, this.Timeout);
            this.changes++;

        }
        public void Insert<T>(IEnumerable<T> entities) where T : class
        {
            this.dapper.Insert<T>(this.connection, entities, this.transaction, this.Timeout);
            this.changes = this.changes + entities.Count();
        }
        public void Delete<T>(T entity) where T : class
        {
            this.dapper.Delete<T>(this.connection, entity, this.transaction, this.Timeout);
            this.changes++;
        }
        public void Update<T>(T entity) where T : class
        {
            this.dapper.Update<T>(this.connection, entity, this.transaction, this.Timeout);
            this.changes++;
        }
        public bool Delete<T>(Expression<Func<T, bool>> filter) where T : class
        {
            return this.dapper.Delete<T>(this.connection, filter, this.transaction, this.Timeout);
        }
        public bool Update<T>(Expression<Func<T, bool>> filter, Expression<Func<T, T>> update) where T : class
        {
            throw new NotImplementedException();
        }


        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            object changes = this.connection.ExecuteScalar(sql, parameters);
            return Convert.ToInt32(changes);
        }
        public IEnumerable<T> ExcuteSql<T>(string sql, params object[] parameters) where T : class
        {
            return this.connection.Query<T>(sql, parameters);
        }
        
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, string splitOn = "Id")
        {
            return SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this.connection, sql, map, param, this.transaction, true, splitOn, this.Timeout, CommandType.Text);
        }
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, string splitOn = "Id")
        {
            return SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this.connection, sql, map, param, this.transaction, true, splitOn, this.Timeout, CommandType.Text);
        }
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, string splitOn = "Id")
        {
            return SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this.connection, sql, map, param, this.transaction, true, splitOn, this.Timeout, CommandType.Text);
        }
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, string splitOn = "Id")
        {
            return SqlMapper.Query<TFirst, TSecond, TThird, TFourth, TReturn>(this.connection, sql, map, param, this.transaction, true, splitOn, this.Timeout, CommandType.Text);
        }
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, string splitOn = "Id")
        {
            return SqlMapper.Query<TFirst, TSecond, TThird, TReturn>(this.connection, sql, map, param, this.transaction, true, splitOn, this.Timeout, CommandType.Text);
        }
        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id")
        {
            return SqlMapper.Query<TFirst, TSecond, TReturn>(this.connection, sql, map, param, this.transaction, true, splitOn, this.Timeout, CommandType.Text);
        }
        public IEnumerable<T> GetPage<T>(object predicate, IList<ISort> sort, int pageIndex, int pageSize) where T : class
        {
            return this.dapper.GetPage<T>(this.connection, predicate, sort, pageIndex, pageSize, this.transaction, this.Timeout, true);
        }
        

        public bool IsTransactionStarted
        {
            get { return this.isTransactionStarted; }
        }
        public void BeginTransaction()
        {
            if (this.isTransactionStarted) return;
            this.transaction = this.connection.BeginTransaction();
            this.isTransactionStarted = true;
        }
        public void Commit()
        {
            if (!this.isTransactionStarted)
                throw new InvalidOperationException("No transaction started.");

            this.transaction.Commit();
            this.transaction = null;
        }
        public void Rollback()
        {
            if (!this.isTransactionStarted)
                throw new InvalidOperationException("No transaction started.");

            this.transaction.Rollback();
            this.transaction.Dispose();
            this.transaction = null;

            this.isTransactionStarted = false;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                if (this.connection.State != ConnectionState.Closed && this.isTransactionStarted
                    && this.transaction != null)
                {
                    this.transaction.Rollback();
                }
                this.connection.Close();
            }

            disposed = true;
        }
        ~DbContext()
        {
            Dispose(false);
        }
    }
}
