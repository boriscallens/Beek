using System;
using System.Linq;

namespace System.Collections.Generic
{
    public interface IPagedList
    {
        int TotalPages { get; }
        int TotalCount { get; }
        int PageIndex { get; }
        int PageSize { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
        bool IsFirstPage { get; }
        bool IsLastPage { get; }
    }
    public class PagedList<T> : List<T>, IPagedList
    {

        public PagedList(IEnumerable<T> source, int index, int pageSize)
        {

            //### set source to blank list if source is null to prevent exceptions
            if (source == null)
                source = new List<T>();

            //### set properties
            this.TotalCount = source.Count();
            this.PageSize = pageSize;
            this.PageIndex = index;
            if (this.TotalCount > 0)
                this.TotalPages = (int)Math.Ceiling((double)this.TotalCount / (double)this.PageSize);
            else
                this.TotalPages = 0;
            this.HasPreviousPage = (this.PageIndex > 1);
            this.HasNextPage = (this.PageIndex < this.TotalPages);
            this.IsFirstPage = (this.PageIndex == 1);
            this.IsLastPage = (this.PageIndex == this.TotalPages);

            //### argument checking
            if (index < 1 || index > this.TotalPages)
                throw new ArgumentOutOfRangeException("PageIndex out of range.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("PageSize cannot be less than 1.");

            //### add items to internal list
            if (this.TotalCount > 0)
                this.AddRange(source.Skip((index - 1) * pageSize).Take(pageSize).ToList());

        }

        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public bool HasPreviousPage { get; private set; }
        public bool HasNextPage { get; private set; }
        public bool IsFirstPage { get; private set; }
        public bool IsLastPage { get; private set; }

    }
    public static class Pagination
    {
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int index, int pageSize)
        {
            return new PagedList<T>(source, index, pageSize);
        }
    }
}