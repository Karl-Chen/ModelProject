using Newtonsoft.Json;

namespace WebProject.ViewModels
{
    public class VMSimpleOrderCarItem
    {
        [JsonProperty("ProductID")]
        public string productID { get; set; } = null!;
        [JsonProperty("count")]
        public int count { get; set; } = 0;
    }
}
