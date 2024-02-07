﻿namespace e_Estoque_API.Application.Auth.ViewModels;

public class TokenViewModel
{
    public string AccessToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public int RefreshExpiresIn { get; set; }
    public string TokenType { get; set; } = string.Empty;
    public string NotBeforePolicy { get; set; } = string.Empty;
    public string SessionState { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
}