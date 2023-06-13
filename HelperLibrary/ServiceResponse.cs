namespace HelperLibrary
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccessfull { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}