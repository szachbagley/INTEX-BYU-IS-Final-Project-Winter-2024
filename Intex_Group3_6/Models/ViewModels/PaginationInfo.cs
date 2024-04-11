namespace Intex_Group3_6.Models.ViewModels
{
    public class PaginationInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
