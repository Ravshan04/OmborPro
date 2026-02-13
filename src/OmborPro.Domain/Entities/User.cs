using System;
using System.Collections.Generic;
using OmborPro.Domain.Common;

namespace OmborPro.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
    public List<Guid> WarehouseIds { get; set; } = new();
    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginAt { get; set; }
}
