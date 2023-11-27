namespace InventoryManagement_api.Models.Design
{
    public class Products
    {
        public int Id { get; set; }
        public string Productname { get; set; }
        public string Description { get; set; }
        public int price { get; set; }
        public string status { get; set; }
        public int weight { get; set; }
        public string size { get; set; }    
        public DateTime? timestamp { get; set; }

    }
}
