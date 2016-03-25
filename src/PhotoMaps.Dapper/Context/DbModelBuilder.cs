using DapperExtensions.Mapper;
using System.Collections.Generic;

namespace PhotoMaps.Dapper
{
    public class DbModelBuilder
    {
        public DbModelBuilder()
        {
            this.Mappers = new List<IClassMapper>();
        }
        public IList<IClassMapper> Mappers { get; private set; }
    }
}
