namespace WebProject.ViewModels
{
    public class VMOrderCar
    {
        public List<VMOrderCarItem>? item { get; set; }
        public string isFix { get; set; } = "0";
        public string sendWay { get; set; } = "1";
    }
}
