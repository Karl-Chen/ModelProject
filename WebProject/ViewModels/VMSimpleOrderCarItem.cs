using Newtonsoft.Json;

namespace WebProject.ViewModels
{
    public class VMSimpleOrderCarItem
    {
        [JsonProperty("ProductID")]
        string productID { get; set; } = null!;
        [JsonProperty("count")]
        int count { get; set; } = 0;
    }
}
