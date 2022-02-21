﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API;
using Newtonsoft.Json;
using uBeac.Identity;
using Xunit;

namespace IntegrationTests;

public class AccountsTests : BaseTestClass, IClassFixture<Factory>
{
    private readonly Factory _factory;

    private const string RegisterUri = "/API/Accounts/Register";
    private const string LoginUri = "/API/Accounts/Login";
    private const string RefreshTokenUri = "/API/Accounts/RefreshToken";
    private const string ForgotPasswordUri = "/API/Accounts/ForgotPassword";

    private static string _userName = "amir";
    private static string _email = "ap@ubeac.io";
    private static string _password = "1qaz!QAZ";
    private static Guid _userId;
    private static string _accessToken;
    private static string _refreshToken;

    public AccountsTests(Factory factory)
    {
        _factory = factory;
    }

    [Fact, TestPriority(1)]
    public async Task Register_ReturnsSuccessApiResult()
    {
        // Arrange
        var client = _factory.CreateClient();
        var content = new StringContent(JsonConvert.SerializeObject(new RegisterRequest
        {
            UserName = _userName,
            Email = _email,
            Password = _password
        }), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync(RegisterUri, content);
        response.EnsureSuccessStatusCode();
        var result = await response.GetApiResult<bool>();

        // Assert
        Assert.True(result.Data);
    }

    [Fact, TestPriority(2)]
    public async Task Login_ReturnsSuccessApiResult()
    {
        // Arrange
        var client = _factory.CreateClient();
        var content = new StringContent(JsonConvert.SerializeObject(new LoginRequest
        {
            UserName = _userName,
            Password = _password
        }), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync(LoginUri, content);
        response.EnsureSuccessStatusCode();
        var result = await response.GetApiResult<TokenResult>();

        // Assert
        Assert.NotNull(result.Data);
        Assert.NotEqual(default, result.Data.UserId);
        Assert.NotEmpty(result.Data.Token);
        Assert.NotEmpty(result.Data.RefreshToken);

        // Set Static Values
        _userId = result.Data.UserId;
        _accessToken = result.Data.Token;
        _refreshToken = result.Data.RefreshToken;
    }

    [Fact, TestPriority(3)]
    public async Task RefreshToken_ReturnsSuccessApiResult()
    {
        // Arrange
        var client = _factory.CreateClient();
        var content = new StringContent(JsonConvert.SerializeObject(new RefreshTokenRequest
        {
            Token = _accessToken,
            RefreshToken = _refreshToken
        }), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync(RefreshTokenUri, content);
        response.EnsureSuccessStatusCode();
        var result = await response.GetApiResult<TokenResult>();

        // Set Static Values
        _userId = result.Data.UserId;
        _accessToken = result.Data.Token;
        _refreshToken = result.Data.RefreshToken;
    }

    [Fact, TestPriority(4)]
    public async Task ForgotPassword_ReturnsSuccessApiResult()
    {
        // Arrange
        var client = _factory.CreateClient();
        var content = new StringContent(JsonConvert.SerializeObject(new ForgotPasswordRequest
        {
            UserName = _userName
        }), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync(ForgotPasswordUri, content);
        response.EnsureSuccessStatusCode();
        var result = await response.GetApiResult<bool>();

        // Assert
        Assert.True(result.Data);
    }
}