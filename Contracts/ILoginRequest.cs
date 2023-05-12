namespace Contracts
{
    public interface ILoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}