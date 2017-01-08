namespace Application.Requests
{
    public class BookRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Location { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }
    }
}
