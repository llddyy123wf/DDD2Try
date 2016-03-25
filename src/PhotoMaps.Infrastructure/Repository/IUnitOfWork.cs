/*
UnitOfWork的作用是<维护业务对象影响的列表并协调变化写入和并发问题>
UnitOfWork关注写(增删改)非读, 它会记录所有变化(读没有变化), 操作结束后统一对存储进行操作
*/

namespace Framework.Infrastructure.Repository
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}