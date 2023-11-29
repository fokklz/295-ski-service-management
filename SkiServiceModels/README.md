# SkiServiceModels

## Core Models

<details>
<summary>User</summary>
public int Id { get; set; }<br>
public string Username { get; set; }<br>
public byte[] PasswordHash { get; set; }<br>
public byte[] PasswordSalt { get; set; }<br>
public bool Locked { get; set; } = false;<br>
public RoleNames Role { get; set; } = RoleNames.User;<br>
public int LoginAttempts { get; set; } = 0;<br>
public bool IsDeleted { get; set; } = false;
</details>

## DTOs

### Response DTOs

### Request DTOs
