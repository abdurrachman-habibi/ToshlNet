namespace ToshlNet.Models
{
    public class Validation<T>
    {
        public string[] ErrorMessages { get; set; }

        public T ReturnObject { get; set; }
    }
}
