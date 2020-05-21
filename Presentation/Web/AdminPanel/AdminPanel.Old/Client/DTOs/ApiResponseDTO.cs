namespace AdminPanel.Client.DTOs
{
    public class ApiResponseDTO<T>
    {
        public int Code { get; set; }

        public T Value { get; set; }
    }
}