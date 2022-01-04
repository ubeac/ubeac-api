using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using uBeac.Repositories.MongoDB;

namespace Identity.Users
{
    public class AuthenticationTests : WebApiTestFixture<Startup>
    {
        [Fact]
        [TestOrder(1)]
        [Trait("DB", "Init Data")]
        public void Init_Data()
        {
            // deleting the test database
            var dbOptions = Services.GetService<MongoDBOptions<MongoDBContext>>();
            if (dbOptions == null)
                throw new ArgumentNullException(nameof(dbOptions));

            var mongoUrl = new MongoUrl(dbOptions.ConnectionString);
            var client = new MongoClient(mongoUrl);
            client.DropDatabase(mongoUrl.DatabaseName);
            Assert.True(true, $"MongoDB {mongoUrl.DatabaseName} is initialized.");
        }

        [Fact]
        [TestOrder(2)]
        [Trait("Register", "New Valid User")]
        public void Register_User()
        {
            var register_request = new RegisterRequest { Email = "ap@ubeac.io", Username = "amir", Password = "P@ssw0rd" };
            var register_response = Post("Account", "Register", register_request).Result;
            register_response.EnsureSuccessStatusCode();
            var apiResult = register_response.GetApiResult<bool>().Result;
            Assert.NotNull(apiResult?.Data);
            Assert.True(apiResult.Data, "New user registered");
        }


        [Fact]
        [TestOrder(3)]
        [Trait("Register", "Duplicate User")]
        public void Register_User_Duplicate()
        {
            var register_request = new RegisterRequest { Email = "ap@ubeac.io", Username = "amir", Password = "P@ssw0rd123" };
            var register_response = Post("Account", "Register", register_request).Result;
            var apiResult = register_response.GetApiResult<bool>().Result;
            Assert.NotNull(apiResult?.Data);
            Assert.True(apiResult?.Errors.Count > 0);
            Assert.False(apiResult?.Data, apiResult?.Errors[0].Description);
        }


        [Fact]
        [TestOrder(4)]
        [Trait("Login", "Invalid User Credentials")]
        public void Login_User_Invalid_Password()
        {
            var login_request = new LoginRequest { Username = "amir", Password = "123" };
            var login_response = Post("Account", "Login", login_request).Result;
            var apiResult = login_response.GetApiResult<TokenResult<Guid>>().Result;
            Assert.Null(apiResult.Data);
            Assert.True(apiResult.Errors.Count > 0, apiResult.Errors[0].Description);
        }

        [Fact]
        [TestOrder(5)]
        [Trait("Login", "Invalid User Credentials")]
        public void Login_User_Valid()
        {
            var login_request = new LoginRequest { Username = "amir", Password = "P@ssw0rd" };
            var login_response = Post("Account", "Login", login_request).Result;
            var apiResult = login_response.GetApiResult<TokenResult<Guid>>().Result;
            Assert.NotNull(apiResult.Data);
            Assert.False(string.IsNullOrEmpty( apiResult.Data.Token));
            Assert.False(apiResult.Data.UserId == Guid.Empty);
        }
    }
}