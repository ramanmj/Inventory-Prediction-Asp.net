using System.Diagnostics.Contracts;

namespace InventoryManagement_api.Models
{
    public class Productdto
    {
        public string Productname { get; set; }
        public string Description { get; set; }
        public int price { get; set; }
        public int weight { get; set; }
        public string size { get; set; }
    }

    public class UpdateProductdto
    {
        public string Productname { get; set; }
        public string Description { get; set; }
        public int price { get; set; }

        public string status { get; set; }

        public int weight { get; set; }
        public string size { get; set; }
    }


}
