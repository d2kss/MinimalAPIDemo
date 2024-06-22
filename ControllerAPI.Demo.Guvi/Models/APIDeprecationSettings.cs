namespace ControllerAPI.Demo.Guvi.Models
{
    public class APIDeprecationSettings
    {
        public int id {  get; set; }
        public string APIName { get; set; }

        public string MethodName {  get; set; }
        public DateTime DeprecationDatetime { get; set; }
    }
}
