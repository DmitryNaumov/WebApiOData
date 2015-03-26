namespace ProductService
{
    internal static class Singleton<T>
    {
        public static T Instance { get; set; }
    }
}