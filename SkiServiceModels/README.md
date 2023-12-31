# SkiServiceModels

# Contents

<!--TOC-->
  - [Core Models](#core-models)
    - [Service Model](#service-model)
    - [Priority Model](#priority-model)
    - [State Model](#state-model)
    - [User Model](#user-model)
    - [Order Model](#order-model)
  - [DTOs](#dtos)
    - [TokenData](#tokendata)
    - [ErrorData](#errordata)
  - [Response DTOs](#response-dtos)
    - [DeleteResponse](#deleteresponse)
    - [LoginResponse](#loginresponse)
    - [OrderResponse](#orderresponse)
    - [PriorityResponse](#priorityresponse)
    - [ServiceResponse](#serviceresponse)
    - [StateResponse](#stateresponse)
    - [UserResponse](#userresponse)
    - [ErrorResponse](#errorresponse)
  - [Request DTOs](#request-dtos)
    - [LoginRequest](#loginrequest)
    - [RefreshRequest](#refreshrequest)
    - [CreateOrderRequest](#createorderrequest)
    - [CreatePriorityRequest](#createpriorityrequest)
    - [CreateServiceRequest](#createservicerequest)
    - [CreateStateRequest](#createstaterequest)
    - [CreateUserRequest](#createuserrequest)
    - [UpdateOrderRequest](#updateorderrequest)
    - [UpdatePriorityRequest](#updatepriorityrequest)
    - [UpdateServiceRequest](#updateservicerequest)
    - [UpdateStateRequest](#updatestaterequest)
    - [UpdateUserRequest](#updateuserrequest)
  - [Enums](#enums)
    - [RoleNames Enum](#rolenames-enum)
  - [Interfaces](#interfaces)
    - [IGenericModel Interface](#igenericmodel-interface)
    - [DTO Interface](#dto-interface)
    - [Response DTO Interface](#response-dto-interface)
    - [Request DTO Interface](#request-dto-interface)
    - [ILoginRequest Interface](#iloginrequest-interface)
<!--/TOC-->

## Core Models

### Service Model
[back up](#contents)
```csharp
public class Service : IGenericModel
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(1000)]
    public string Description { get; set; }

    public int Price { get; set; }

    public bool IsDeleted { get; set; } = false;

}
```


### Priority Model
[back up](#contents)
```csharp
public class Priority : IGenericModel
{
    [Key]
    public int Id { get; set; }

    [StringLength(20)]
    public string Name { get; set; }

    public int Days { get; set; }

    public bool IsDeleted { get; set; } = false;

}
```


### State Model
[back up](#contents)
```csharp
public class State : IGenericModel
{

    [Key]
    public int Id { get; set; }

    [StringLength(20)]
    public string Name { get; set; }

    public bool IsDeleted { get; set; } = false;

}
```


### User Model
[back up](#contents)
```csharp
public class User : IGenericModel
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Username { get; set; }

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }

    public bool Locked { get; set; } = false;

    public RoleNames Role { get; set; } = RoleNames.User;

    public int LoginAttempts { get; set; } = 0;

    public bool IsDeleted { get; set; } = false;

    public string RefreshToken { get; set; } = null;
}
```


### Order Model
[back up](#contents)
```csharp
public class Order : IGenericModel
{
    [Key]
    public int Id { get; set; }

    // Foreign keys
    public int ServiceId { get; set; }
    public int PriorityId { get; set; }
    public int StateId { get; set; }
    public int? UserId { get; set; } = null;

    // Navigation properties
    public virtual User? User { get; set; }
    public virtual Service Service { get; set; }
    public virtual Priority Priority { get; set; }
    public virtual State State { get; set; }

    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(100)]
    public string Email { get; set; }

    [StringLength(20)]
    public string Phone { get; set; }

    [StringLength(1000)]
    public string? Note { get; set; } = null;

    public DateTime Created { get; set; } = DateTime.Now;

    public bool IsDeleted { get; set; } = false;

}
```


## DTOs

### TokenData

This DTO is used for any token data sent by the API.

[back up](#contents)
```csharp
public class TokenData : IDTO
{
    public string Token { get; set; }

    public string? RefreshToken { get; set; }

    public string TokenType { get; set; } = "Bearer";

    public DateTime Issued { get; set; } = DateTime.UtcNow;

    public DateTime Expires { get; set; }
}
```


### ErrorData

This DTO is used for any error data sent by the API.

[back up](#contents)
```csharp
public class ErrorData : IDTO
{

    public string MessageCode { get; set; }

    public bool Breaking { get; set; } = false;
}
```


## Response DTOs

### DeleteResponse
[back up](#contents)
```csharp
public class DeleteResponse : IResponseDTO
{
    public int Id { get; set; }
}
```


### LoginResponse
[back up](#contents)
```csharp
public class LoginResponse : UserResponse, IResponseDTO
{
    public TokenData Auth { get; set; }
}
```


### OrderResponse
[back up](#contents)
```csharp
public class OrderResponse : IResponseDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string? Note { get; set; }

    public DateTime Created { get; set; }

    // Navigation properties
    public UserResponse User { get; set; }
    public ServiceResponse Service { get; set; }
    public PriorityResponse Priority { get; set; }
    public StateResponse State { get; set; }
}
public class OrderResponseAdmin : OrderResponse, IResponseDTO
{
    public bool IsDeleted { get; set; }

    // Navigation properties
    new public UserResponseAdmin User { get; set; }
    new public ServiceResponseAdmin Service { get; set; }
    new public PriorityResponseAdmin Priority { get; set; }
    new public StateResponseAdmin State { get; set; }
}
```


### PriorityResponse
[back up](#contents)
```csharp
public class PriorityResponse : IResponseDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Days { get; set; }
}
public class PriorityResponseAdmin : PriorityResponse, IResponseDTO
{
    public bool IsDeleted { get; set; }
}
```


### ServiceResponse
[back up](#contents)
```csharp
public class ServiceResponse : IResponseDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Price { get; set; }
}
public class ServiceResponseAdmin : ServiceResponse, IResponseDTO
{
    public bool IsDeleted { get; set; }
}
```


### StateResponse
[back up](#contents)
```csharp
public class StateResponse : IResponseDTO
{
    public int Id { get; set; }

    public string Name { get; set; }
}
public class StateResponseAdmin : StateResponse, IResponseDTO
{
    public bool IsDeleted { get; set; }
}
```


### UserResponse
[back up](#contents)
```csharp
public class UserResponse : IResponseDTO
{
    public int Id { get; set; }
    public string Username { get; set; }
    public bool Locked { get; set; }
    public RoleNames Role { get; set; }

}
public class UserResponseAdmin : UserResponse, IResponseDTO
{
    public bool IsDeleted { get; set; }
    public int LoginAttempts { get; set; } = 0;
}
```


## Request DTOs

### LoginRequest
[back up](#contents)
```csharp
public class LoginRequest : ILoginRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    public bool RememberMe { get; set; } = false;
}
```


### RefreshRequest
[back up](#contents)
```csharp
public class RefreshRequest : ILoginRequest
{
    [Required]
    public string Token { get; set; }

    [Required]
    public string RefreshToken { get; set; }
}
```


### CreateOrderRequest
[back up](#contents)
```csharp
public class CreateOrderRequest : IRequestDTO
{
    [Required]
    public int ServiceId { get; set; }
    [Required]
    public int PriorityId { get; set; }
    [Required]
    public int StateId { get; set; }

    public int? UserId { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(20)]
    [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$", ErrorMessage = "Invalid phone number")]
    public string Phone { get; set; }

    [StringLength(1000)]
    public string? Note { get; set; } = null;
}
```


### CreatePriorityRequest
[back up](#contents)
```csharp
public class CreatePriorityRequest : IRequestDTO
{

    [Required]
    [StringLength(20)]
    public string Name { get; set; }

    [Required]
    [Range(1, 365)]
    public int Days { get; set; }
}
```


### CreateServiceRequest
[back up](#contents)
```csharp
public class CreateServiceRequest : IRequestDTO
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(1000)]
    public string Description { get; set; }

    [Required]
    [Range(1, 1000)]
    public int Price { get; set; }
}
```


### CreateStateRequest
[back up](#contents)
```csharp
public class CreateStateRequest : IRequestDTO
{

    [Required]
    [StringLength(20)]
    public string Name { get; set; }
}
```


### CreateUserRequest
[back up](#contents)
```csharp
public class CreateUserRequest : IRequestDTO
{
    [Required]
    [StringLength(50)]
    public string Username { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }

    public bool? Locked { get; set; }

    public RoleNames? Role { get; set; }
}
```


### UpdateOrderRequest
[back up](#contents)
```csharp
public class UpdateOrderRequest : IRequestDTO
{
    public int? ServiceId { get; set; }

    public int? PriorityId { get; set; }

    public int? StateId { get; set; }

    public int? UserId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(100)]
    [EmailAddress]
    public string? Email { get; set; }

    [StringLength(20)]
    [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$", ErrorMessage = "Invalid phone number")]
    public string? Phone { get; set; }

    [StringLength(1000)]
    public string? Note { get; set; } = null;
}
```


### UpdatePriorityRequest
[back up](#contents)
```csharp
public class UpdatePriorityRequest : IRequestDTO
{
    [StringLength(20)]
    public string? Name { get; set; }

    [Range(1, 365)]
    public int? Days { get; set; }
}
```


### UpdateServiceRequest
[back up](#contents)
```csharp
public class UpdateServiceRequest : IRequestDTO
{
    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Range(1, 1000)]
    public int? Price { get; set; }
}
```


### UpdateStateRequest
[back up](#contents)
```csharp
public class UpdateStateRequest : IRequestDTO
{
    [StringLength(20)]
    public string? Name { get; set; }
}
```


### UpdateUserRequest
[back up](#contents)
```csharp
public class UpdateUserRequest : IRequestDTO
{
    [StringLength(50)]
    public string? Username { get; set; }

    [MinLength(8)]
    public string? Password { get; set; }

    public bool? Locked { get; set; }

    public RoleNames? Role { get; set; }
}
```


## Enums

### RoleNames Enum
[back up](#contents)
```csharp
public enum RoleNames
{
    User,
    SuperAdmin
}
```


## Interfaces

### IGenericModel Interface

Base Interface for all models.

[back up](#contents)
```csharp
public interface IGenericModel
{
    int Id { get; set; }

    bool IsDeleted { get; set; }

}
```


### DTO Interface

Base Interface for all DTOs.

[back up](#contents)
```csharp
public interface IDTO
{
}
```


### Response DTO Interface

Base Interface for all Response DTOs.

[back up](#contents)
```csharp
public interface IResponseDTO : IDTO
{
}
```


### Request DTO Interface

Base Interface for all Request DTOs.

[back up](#contents)
```csharp
public interface IRequestDTO : IDTO
{
}
```


### ILoginRequest Interface

Interface for LoginRequest & RefreshRequest

[back up](#contents)
```csharp
public interface ILoginRequest : IRequestDTO
{
}
```

