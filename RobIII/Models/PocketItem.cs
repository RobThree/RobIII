using System;

namespace RobIII.Models
{
    public enum PocketStatus
    {
        Unread = 0,
        Read = 1
    }

    public class PocketItem
    {
        public string Id { get; set; }
        public string ResolvedId { get; set; }
        public string Title { get; set; }
        public string FullTitle { get; set; }
        public string Excerpt { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? ReadTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public PocketStatus Status { get; set; }
        public string Uri { get; set; }
        public bool IsArchive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsArticle { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsImage { get; set; }
        public bool IsVideo { get; set; }

        public string GetTitle()
        {
            if (!string.IsNullOrEmpty(Title))
            {
                return Title;
            }

            if (!string.IsNullOrEmpty(FullTitle))
            {
                return FullTitle;
            }

            return Uri;
        }
    }
}