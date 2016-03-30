using System.Collections.Generic;

namespace Framework.Infrastructure.Result
{
    public class PageResult<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public int PrevPage
        {
            get
            {
                if (this.PageIndex == 1) return 1;
                return this.PageIndex - 1;
            }
        }

        public int NextPage
        {
            get
            {
                if (this.PageIndex == this.TotalPages) return this.TotalPages;
                return this.PageIndex - 1;
            }
        }

        public int TotalPages
        {
            get
            {
                if (this.TotalRecords <= 0) return 1;
                return (this.TotalRecords / this.PageSize) + (this.TotalRecords % this.PageSize == 0 ? 0 : 1);
            }
        }

        public int TotalRecords
        {
            get;
            set;
        }

        public IEnumerable<T> DataSource { get; set; }
    }
}