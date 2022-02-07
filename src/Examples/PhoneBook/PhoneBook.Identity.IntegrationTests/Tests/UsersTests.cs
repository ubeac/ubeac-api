// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Net.Http;
// using System.Text;
// using System.Threading.Tasks;
// using Newtonsoft.Json;
// using uBeac.Web.Identity;
// using Xunit;
//
// namespace PhoneBook.Identity.IntegrationTests;
//
// public class UsersTests : BaseTestClass, IClassFixture<Factory>
// {
//     private readonly Factory _factory;
//
//     public UsersTests(Factory factory)
//     {
//         _factory = factory;
//     }
//
//     [Fact, TestPriority(1)]
//     public async Task Insert_ReturnsSuccessApiResult()
//     {
//         // Arrange
//         var client = _factory.CreateClient();
//         var content = new StringContent(JsonConvert.SerializeObject(new InsertUserRequest
//         {
//             UserName = "john",
//             Password = "1qaz!QAZ",
//             PhoneNumber = "+98",
//             PhoneNumberConfirmed = false,
//             Email = "john@doe.com",
//             EmailConfirmed = false
//         }), Encoding.UTF8, "application/json");
//
//         // Act
//         var response = await client.PostAsync(UserStaticValues.InsertUri, content);
//         response.EnsureSuccessStatusCode();
//         var result = await response.GetApiResult<Guid>();
//
//         // Assert
//         Assert.NotEqual(default, result.Data);
//
//         // Set Static Values
//         UserStaticValues.UserId = result.Data;
//     }
//
//     [Fact, TestPriority(2)]
//     public async Task All_ReturnsSuccessApiResult()
//     {
//         // Arrange
//         var client = _factory.CreateClient();
//
//         // Act
//         var response = await client.GetAsync(UserStaticValues.AllUri);
//         response.EnsureSuccessStatusCode();
//         var result = await response.GetApiResult<IEnumerable<UserViewModel>>();
//
//         // Assert
//         Assert.NotNull(result.Data);
//         Assert.True(result.Data.Any());
//     }
//
//     [Fact, TestPriority(3)]
//     public async Task Find_ReturnsSuccessApiResult()
//     {
//         // Arrange
//         var client = _factory.CreateClient();
//
//         // Act
//         var response = await client.GetAsync($"{UserStaticValues.FindUri}?id={UserStaticValues.UserId}");
//         response.EnsureSuccessStatusCode();
//         var result = await response.GetApiResult<UserViewModel>();
//
//         // Assert
//         Assert.NotNull(result.Data);
//         Assert.Equal(result.Data.Id, UserStaticValues.UserId);
//     }
//
//     [Fact, TestPriority(4)]
//     public async Task Replace_ReturnsSuccessApiResult()
//     {
//         // Arrange
//         var client = _factory.CreateClient();
//         var content = new StringContent(JsonConvert.SerializeObject(new ReplaceUserRequest
//         {
//             Id = UserStaticValues.UserId,
//             PhoneNumber = "+98",
//             PhoneNumberConfirmed = true,
//             Email = "john@doe.com",
//             EmailConfirmed = true,
//             LockoutEnabled = true,
//             LockoutEnd = null
//         }), Encoding.UTF8, "application/json");
//
//         // Act
//         var response = await client.PostAsync(UserStaticValues.ReplaceUri, content);
//         response.EnsureSuccessStatusCode();
//         var result = await response.GetApiResult<bool>();
//
//         // Assert
//         Assert.True(result.Data);
//     }
//
//     [Fact, TestPriority(5)]
//     public async Task ChangePassword_ReturnsSuccessApiResult()
//     {
//         // Arrange
//         var client = _factory.CreateClient();
//         var newPassword = "1QAZ!qaz";
//         var content = new StringContent(JsonConvert.SerializeObject(new ChangePasswordRequest
//         {
//             UserId = UserStaticValues.UserId,
//             CurrentPassword = UserStaticValues.Password,
//             NewPassword = newPassword
//         }), Encoding.UTF8, "application/json");
//
//         // Act
//         var response = await client.PostAsync(UserStaticValues.ChangePasswordUri, content);
//         response.EnsureSuccessStatusCode();
//         var result = await response.GetApiResult<bool>();
//
//         // Assert
//         Assert.True(result.Data);
//
//         // Set Static Values
//         UserStaticValues.Password = newPassword;
//     }
// }
//
// public static class UserStaticValues
// {
//     public const string InsertUri = "/API/Users/Insert";
//     public const string ReplaceUri = "/API/Users/Replace";
//     public const string ChangePasswordUri = "/API/Users/ChangePassword";
//     public const string FindUri = "/API/Users/Find";
//     public const string AllUri = "/API/Users/All";
//
//     public static Guid UserId { get; set; }
//     public static string Password { get; set; }
// }