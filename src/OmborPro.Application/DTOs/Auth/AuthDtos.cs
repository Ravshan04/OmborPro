using System;

namespace OmborPro.Application.DTOs.Auth;

public record LoginRequest(string Email, string Password);

public record RegisterRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string? Phone,
    Guid? OrganizationId = null);

public record AuthResponse(
    string Token,
    string Email,
    string FirstName,
    string LastName,
    string? Phone,
    string? AvatarUrl,
    Guid OrganizationId);

public record ProfileDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string? Phone,
    string? AvatarUrl,
    Guid OrganizationId);

public record UpdateProfileRequest(
    string FirstName,
    string LastName,
    string? Phone,
    string? AvatarUrl);
