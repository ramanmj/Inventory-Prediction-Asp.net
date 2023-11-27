namespace InventoryManagement_api.Models
{
    public class rawmaterialsDto
    {
        public string Materialname { get; set; }
        public int weight { get; set; }
        public int price { get; set; }
    }

    public class UpdaterawmaterialsDto
    {
        public string Materialname { get; set; }
        public int weight { get; set; }
        public int price { get; set; }

        public string status { get; set; }
    }
}
