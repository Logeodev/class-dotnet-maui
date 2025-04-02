namespace colors_front.Models
{
    public class ItemsApiResponse<T>
    {
        public T[]? Items { get; set; }
    }

    public class ItemApiResponse<T>
    {
        public T? Item { get; set; }
    }
}
