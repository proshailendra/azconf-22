using X.PagedList;

namespace ePizzaHub.Repositories.Models
{
    public class PagingListModel<T> where T : class
    {
        public StaticPagedList<T> Data { get; set; }
        public int TotalRows { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }

        public bool ShowPrevious => Page > 1;
        public bool ShowNext => Page < TotalPages;
        public bool ShowFirst => Page != 1;
        public bool ShowLast => Page != TotalPages;
    }
}