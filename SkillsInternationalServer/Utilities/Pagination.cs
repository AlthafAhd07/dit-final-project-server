namespace SkillsInternationalServer.Utilities
{
    public class PaginationResult{
        public int currentPage { get; set; }

        public int totalPages { get; set; }

        public int pageSize { get; set; }
        public int totalCount { get; set; }


    }
    public static class Pagination
    {
        public static (int limit , int offset) GetPagingParameters(int page = 1, int pageSize = 10)
        {
            // Enforce maximum page size
            pageSize = Math.Min(pageSize, 100);

            // Calculate offset (zero-based)
            var offset = (page - 1) * pageSize;

            return (pageSize , offset );
        }

        public static PaginationResult GetPaginationMetadata(int page, int pageSize, int totalCount)
        {
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return new PaginationResult
            {
                currentPage = page,
                pageSize = pageSize,
                totalPages = totalPages,
                totalCount = totalCount,
              };
        }
    }
}
