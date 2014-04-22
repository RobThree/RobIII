using PagedList.Mvc;

namespace RobIII.Helpers
{
    public class PagingOptions
    {
        private static PagedListRenderOptions _renderoptions;

        public static PagedListRenderOptions Default
        {
            get
            {
                if (_renderoptions == null)
                {
                    _renderoptions = new PagedListRenderOptions
				       	{
				       		DisplayLinkToFirstPage = PagedListDisplayMode.Never,
				       		DisplayLinkToLastPage = PagedListDisplayMode.Never,
							DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
							DisplayLinkToNextPage = PagedListDisplayMode.Always,
				       		DisplayLinkToIndividualPages = false,
				       		DisplayPageCountAndCurrentLocation = true,
                            LinkToPreviousPageFormat = "&larr; Older",
			                LinkToNextPageFormat = "Newer &rarr;",
			                PageCountAndCurrentLocationFormat = "Page {0} of {1}",
			                FunctionToDisplayEachPageNumber = null
				       	};
                }
                return _renderoptions;
            }
        }
    }
}