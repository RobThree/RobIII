using PagedList;
using SimpleFeedReader;

namespace RobIII.Models
{
    public class PocketViewmodel
    {
        public IPagedList<PocketItem> PagedList { get; set; }
        public string Status { get; set; }
        public string Search { get; set; }
    }
}