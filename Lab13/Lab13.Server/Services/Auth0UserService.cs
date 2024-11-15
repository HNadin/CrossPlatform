using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Lab13.Server.Models;
using Microsoft.Extensions.Configuration;

namespace Lab13.Server.Services
{
    public class Auth0UserService
    {
        private readonly string _domain;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _audience;
        private readonly IConfiguration _configuration;

        public Auth0UserService(IConfiguration configuration)
        {
            _configuration = configuration;
            _domain = _configuration["Auth0:Domain"] ?? throw new ArgumentNullException(nameof(configuration));
            _clientId = _configuration["Auth0:ClientId"] ?? throw new ArgumentNullException(nameof(configuration));
            _clientSecret = _configuration["Auth0:ClientSecret"] ?? throw new ArgumentNullException(nameof(configuration));
            _audience = _configuration["Auth0:Audience"] ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task CreateUserAsync(RegisterViewModel model)
        {
            AuthenticationApiClient tokenClient = new(new Uri($"https://{_domain}"));
            Auth0.AuthenticationApi.Models.AccessTokenResponse tokenResponse = await tokenClient.GetTokenAsync(new ClientCredentialsTokenRequest
            {
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                Audience = _audience
            });

            ManagementApiClient managementClient = new(tokenResponse.AccessToken, new Uri($"https://{_domain}/api/v2"));

            UserCreateRequest userCreateRequest = new()
            {
                UserName = model.UserName,
                Email = model.Email,
                EmailVerified = false,
                Password = model.Password,
                Connection = "Username-Password-Authentication",
                UserMetadata = new
                {
                    model.FullName,
                    model.Phone,
                }
            };

            await managementClient.Users.CreateAsync(userCreateRequest);
        }

        public async Task<ProfileViewModel> GetUser(LoginViewModel model)
        {
            string alternativeValue = "N/A";
            AuthenticationApiClient authClient = new(new Uri($"https://{_domain}"));
            Auth0.AuthenticationApi.Models.AccessTokenResponse authResponse = await authClient.GetTokenAsync(new ResourceOwnerTokenRequest
            {
                Audience = _audience,
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                Realm = "Username-Password-Authentication",
                Username = model.Email,
                Password = model.Password,
                Scope = "openid profile email"
            });

            ManagementApiClient managementClient = new(authResponse.AccessToken, new Uri($"https://{_domain}/api/v2"));

            UserInfo userInfo = await authClient.GetUserInfoAsync(authResponse.AccessToken);
            User user = await managementClient.Users.GetAsync(userInfo.UserId);

            return new ProfileViewModel
            {
                Email = user.Email,
                UserName = user.UserName,
                FullName = user.UserMetadata?["FullName"]?.ToString() ?? alternativeValue,
                Phone = user.UserMetadata?["Phone"]?.ToString() ?? alternativeValue,
                ProfileImage = user.Picture.ToString(),
            };
        }
    }
}
