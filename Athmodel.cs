namespace jwt
{
    public class Athmodel
    {
        public string message {  get; set; }    
        public bool isathu { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public List<string> roles { get; set; }
        public string token {  get; set; }
        public DateTime expire { get; set; }

    }
}
