using Crm.Application.Configuration.Queries;


namespace Crm.Application.Authentication.GetAuthenticationToken
{
    public class GetAuthenticatedQuery : IQuery<TokenDto>
    {
        public GetAuthenticatedQuery(string username, string password)
        {
            UserName = username;
            Password = password;
        }
        public string UserName { get; }

        public string Password { get; }
    }
}
