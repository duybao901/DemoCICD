using DemoCICD.Contract.Enumerations;

namespace DemoCICD.Contract.Extentions;
public static class SortOrderExtension
{
    public static SortOrder ConvertStringToSortOrder(string? sortOrder)
        => string.IsNullOrWhiteSpace(sortOrder)
        ? SortOrder.Descending :
        sortOrder.Equals("asc") ? SortOrder.Ascending : SortOrder.Descending;

    // Format: Column1-ASC,Column2-DESC...
    public static IDictionary<string, SortOrder> ConvertStringToSortOrderV2(string? sortOrder)
    {
        var result = new Dictionary<string, SortOrder>();

        if (!string.IsNullOrEmpty(sortOrder))
        {
            if (sortOrder.Trim().Split(",").Length > 0)
            {
                foreach (var item in sortOrder.Split(","))
                {
                    if (!item.Contains("-"))
                        throw new FormatException("Sort condition should be follow by format: Column1-ASC, Column2-DESC...");

                    var property = item.Trim().Split("-");
                    var key = ProductExtention.GetSortProductProperty(property[0]);
                    var value = ConvertStringToSortOrder(property[1]);
                    result.TryAdd(key, value);
                }
            }
            else
            {
                if (!sortOrder.Contains("-"))
                    throw new FormatException("Sort condition should be follow by format: Column1-ASC, Column2-DESC...");

                var property = sortOrder.Trim().Split("-");
                result.TryAdd(property[0], ConvertStringToSortOrder(property[1]));
            }
        }

        return result;
    }
}
