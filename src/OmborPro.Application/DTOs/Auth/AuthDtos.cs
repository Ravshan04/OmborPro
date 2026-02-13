using System;

namespace OmborPro.Application.DTOs.Auth;

public record LoginRequest(string Email, string Password);

public record RegisterRequest(
    string Email, 
    string Password, 
    string FirstName, 
    string LastName, 
    Guid OrganizationId);

public record AuthResponse(
    string Token, 
    string Email, 
    string FirstName, 
    string LastName, 
    Guid OrganizationId);
