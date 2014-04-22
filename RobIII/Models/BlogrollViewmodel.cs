using PagedList;
using SimpleFeedReader;

namespace RobIII.Models
{
    public class BlogrollViewmodel
    {
        public IPagedList<FeedItem> PagedList { get; set; }
        public string Language { get; set; }
    }
}