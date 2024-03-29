# SkiServiceModels <!-- omit in toc -->

## Contents <!-- omit in toc -->

<!--TOC-->
- [Core Models](#core-models)
  - [RefreshResult](#refreshresult)
- [EF Models](#ef-models)
  - [Service Model](#service-model)
  - [Priority Model](#priority-model)
  - [State Model](#state-model)
  - [User Model](#user-model)
  - [Order Model](#order-model)
- [BSON Models](#bson-models)
  - [Service Model](#service-model-1)
  - [Priority Model](#priority-model-1)
  - [State Model](#state-model-1)
  - [User Model](#user-model-1)
  - [Order Model](#order-model-1)
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
  - [IGenericEFModel Interface](#igenericefmodel-interface)
  - [IGenericBSONModel Interface](#igenericbsonmodel-interface)
  - [DTO Interface](#dto-interface)
  - [Response DTO Interface](#response-dto-interface)
  - [Request DTO Interface](#request-dto-interface)
  - [ILoginRequest Interface](#iloginrequest-interface)
<!--/TOC-->

## Core Models

### RefreshResult

[back up](#contents-)
```csharp
public class RefreshResult
{
    public TokenData TokenData { get; set; }

    public User User { get; set; }
}
```


## EF Models

### Service Model
[back up](#contents-)
```csharp
public class ServiceBase : IGenericModel
{
    [StringLength(50)]
    [BsonElement("name")]
    public required string Name { get; set; }

    [StringLength(1000)]
    [BsonElement("description")]
    public required string Description { get; set; }

    [BsonElement("price")]
    public int Price { get; set; }

    [BsonElement("is_deleted")]
    public bool IsDeleted { get; set; } = false;

}
```

```csharp
public class Service : ServiceBase, IGenericEFModel
{
    [Key]
    public int Id { get; set; }
}
```


### Priority Model
[back up](#contents-)
```csharp
public class PriorityBase : IGenericModel
{
    [StringLength(20)]
    [BsonElement("name")]
    public required string Name { get; set; }

    [BsonElement("days")]
    public int Days { get; set; }

    [BsonElement("is_deleted")]
    public bool IsDeleted { get; set; } = false;

}
```

```csharp
public class Priority : PriorityBase, IGenericEFModel
{
    [Key]
    public int Id { get; set; }
}
```


### State Model
[back up](#contents-)
```csharp
public class StateBase : IGenericModel
{
    [StringLength(20)]
    [BsonElement("name")]
    public required string Name { get; set; }

    [BsonElement("is_deleted")]
    public bool IsDeleted { get; set; } = false;

}
```

```csharp
public class State : StateBase, IGenericEFModel
{
    [Key]
    public int Id { get; set; }
}
```


### User Model
[back up](#contents-)
```csharp
public class UserBase : IGenericModel
{
    [StringLength(50)]
    [BsonElement("username")]
    public required string Username { get; set; }

    [BsonElement("password_hash")]
    public byte[] PasswordHash { get; set; }

    [BsonElement("password_salt")]
    public byte[] PasswordSalt { get; set; }

    [BsonElement("locked")]
    public bool Locked { get; set; } = false;

    [BsonElement("role")]
    [BsonRepresentation(BsonType.String)]
    public RoleNames Role { get; set; } = RoleNames.User;

    [BsonElement("login_attempts")]
    public int LoginAttempts { get; set; } = 0;

    [BsonElement("refresh_token")]
    public string? RefreshToken { get; set; } = null;

    [BsonElement("is_deleted")]
    public bool IsDeleted { get; set; } = false;

}
```

```csharp
public class User : UserBase, IGenericEFModel
{
    [Key]
    public int Id { get; set; }
}
```


### Order Model
[back up](#contents-)
```csharp
public class OrderBase : IGenericModel
{
    [StringLength(50)]
    [BsonElement("name")]
    public required string Name { get; set; }

    [StringLength(100)]
    [BsonElement("email")]
    public required string Email { get; set; }

    [StringLength(20)]
    [BsonElement("phone")]
    public required string Phone { get; set; }

    [StringLength(1000)]
    [BsonElement("note")]
    public string? Note { get; set; } = null;

    [BsonElement("created")]
    public DateTime Created { get; set; } = DateTime.Now;

    [BsonElement("is_deleted")]
    public bool IsDeleted { get; set; } = false;

}
```

```csharp
public class Order : OrderBase, IGenericEFModel
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
}
```


## BSON Models

### Service Model
[back up](#contents-)
```csharp
public class ServiceBase : IGenericModel
{
    [StringLength(50)]
    [BsonElement("name")]
    public required string Name { get; set; }

    [StringLength(1000)]
    [BsonElement("description")]
    public required string Description { get; set; }

    [BsonElement("price")]
    public int Price { get; set; }

    [BsonElement("is_deleted")]
    public bool IsDeleted { get; set; } = false;

}
```

```csharp
public class Service : ServiceBase, IGenericBSONModel
{
    [BsonId]
    public ObjectId Id { get; set; }
}
```


### Priority Model
[back up](#contents-)
```csharp
public class PriorityBase : IGenericModel
{
    [StringLength(20)]
    [BsonElement("name")]
    public required string Name { get; set; }

    [BsonElement("days")]
    public int Days { get; set; }

    [BsonElement("is_deleted")]
    public bool IsDeleted { get; set; } = false;

}
```

```csharp
public class Priority : PriorityBase, IGenericBSONModel
{
    [BsonId]
    public ObjectId Id { get; set; }
}
```


### State Model
[back up](#contents-)
```csharp
public class StateBase : IGenericModel
{
    [StringLength(20)]
    [BsonElement("name")]
    public required string Name { get; set; }

    [BsonElement("is_deleted")]
    public bool IsDeleted { get; set; } = false;

}
```

```csharp
public class State : StateBase, IGenericBSONModel
{
    [BsonId]
    public ObjectId Id { get; set; }
}
```


### User Model
[back up](#contents-)
```csharp
public class UserBase : IGenericModel
{
    [StringLength(50)]
    [BsonElement("username")]
    public required string Username { get; set; }

    [BsonElement("password_hash")]
    public byte[] PasswordHash { get; set; }

    [BsonElement("password_salt")]
    public byte[] PasswordSalt { get; set; }

    [BsonElement("locked")]
    public bool Locked { get; set; } = false;

    [BsonElement("role")]
    [BsonRepresentation(BsonType.String)]
    public RoleNames Role { get; set; } = RoleNames.User;

    [BsonElement("login_attempts")]
    public int LoginAttempts { get; set; } = 0;

    [BsonElement("refresh_token")]
    public string? RefreshToken { get; set; } = null;

    [BsonElement("is_deleted")]
    public bool IsDeleted { get; set; } = false;

}
```

```csharp
public class User : UserBase, IGenericBSONModel
{
    [BsonId]
    public ObjectId Id { get; set; }
}
```


### Order Model
[back up](#contents-)
```csharp
public class OrderBase : IGenericModel
{
    [StringLength(50)]
    [BsonElement("name")]
    public required string Name { get; set; }

    [StringLength(100)]
    [BsonElement("email")]
    public required string Email { get; set; }

    [StringLength(20)]
    [BsonElement("phone")]
    public required string Phone { get; set; }

    [StringLength(1000)]
    [BsonElement("note")]
    public string? Note { get; set; } = null;

    [BsonElement("created")]
    public DateTime Created { get; set; } = DateTime.Now;

    [BsonElement("is_deleted")]
    public bool IsDeleted { get; set; } = false;

}
```

```csharp
public class Order : OrderBase, IGenericBSONModel
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("priority_id")]
    public ObjectId PriorityId { get; set; }

    [BsonElement("service_id")]
    public ObjectId ServiceId { get; set; }

    [BsonElement("state_id")]
    public ObjectId StateId { get; set; }

    [BsonElement("user_id")]
    public ObjectId? UserId { get; set; } = null;

    [BsonElement("priority")]
    public virtual Priority Priority { get; set; }
    public bool ShouldSerializePriority() => false;

    [BsonElement("service")]
    public virtual Service Service { get; set; }
    public bool ShouldSerializeService() => false;

    [BsonElement("state")]
    public virtual State State { get; set; }
    public bool ShouldSerializeState() => false;

    [BsonElement("user")]
    public virtual User? User { get; set; }
    public bool ShouldSerializeUser() => false;

}
```


## DTOs

### TokenData

This DTO is used for any token data sent by the API.

[back up](#contents-)
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

[back up](#contents-)
```csharp
public class ErrorData : IDTO
{

    public string MessageCode { get; set; }

    public bool Breaking { get; set; } = false;
}
```


## Response DTOs

### DeleteResponse
[back up](#contents-)
```csharp
public class DeleteResponse : IResponseDTO
{
    public int Count { get; set; }
}
```


### LoginResponse
[back up](#contents-)
```csharp
public class LoginResponse : UserResponse, IResponseDTO
{
    public TokenData Auth { get; set; }
}
```


### OrderResponse
[back up](#contents-)
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
[back up](#contents-)
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
[back up](#contents-)
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
[back up](#contents-)
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
[back up](#contents-)
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
[back up](#contents-)
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
[back up](#contents-)
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
[back up](#contents-)
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
[back up](#contents-)
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
[back up](#contents-)
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
[back up](#contents-)
```csharp
public class CreateStateRequest : IRequestDTO
{

    [Required]
    [StringLength(20)]
    public string Name { get; set; }
}
```


### CreateUserRequest
[back up](#contents-)
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
[back up](#contents-)
```csharp
public class UpdateOrderRequest : IRequestDTO
{
    public int? ServiceId { get; set; } = null;

    public int? PriorityId { get; set; } = null;

    public int? StateId { get; set; } = null;

    public int? UserId { get; set; } = null;

    [StringLength(50)]
    public string? Name { get; set; } = null;

    [StringLength(100)]
    [EmailAddress]
    public string? Email { get; set; } = null;

    [StringLength(20)]
    [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$", ErrorMessage = "Invalid phone number")]
    public string? Phone { get; set; } = null;

    [StringLength(1000)]
    public string? Note { get; set; } = null;
}
```


### UpdatePriorityRequest
[back up](#contents-)
```csharp
public class UpdatePriorityRequest : IRequestDTO
{
    [StringLength(20)]
    public string? Name { get; set; } = null;

    [Range(1, 365)]
    public int? Days { get; set; } = null;
}
```


### UpdateServiceRequest
[back up](#contents-)
```csharp
public class UpdateServiceRequest : IRequestDTO
{
    [StringLength(50)]
    public string? Name { get; set; } = null;

    [StringLength(1000)]
    public string? Description { get; set; } = null;

    [Range(1, 1000)]
    public int? Price { get; set; } = null;
}
```


### UpdateStateRequest
[back up](#contents-)
```csharp
public class UpdateStateRequest : IRequestDTO
{
    [StringLength(20)]
    public string? Name { get; set; } = null;
}
```


### UpdateUserRequest
[back up](#contents-)
```csharp
public class UpdateUserRequest : IRequestDTO
{
    [StringLength(50)]
    public string? Username { get; set; } = null;

    [MinLength(8)]
    public string? Password { get; set; } = null;

    public bool? Locked { get; set; } = null;

    public RoleNames? Role { get; set; } = null;
}
```


## Enums

### RoleNames Enum
[back up](#contents-)
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

[back up](#contents-)
```csharp
public interface IGenericModel
{
    bool IsDeleted { get; set; }
}
```



### IGenericEFModel Interface

Base Interface for all models.

[back up](#contents-)
```csharp
public interface IGenericEFModel : IGenericModel
{
    public int Id { get; set; }
}
```


### IGenericBSONModel Interface

Base Interface for all models.

[back up](#contents-)
```csharp
public interface IGenericBSONModel : IGenericModel
{
    public ObjectId Id { get; set; }
}
```


### DTO Interface

Base Interface for all DTOs.

[back up](#contents-)
```csharp
public interface IDTO
{
}
```


### Response DTO Interface

Base Interface for all Response DTOs.

[back up](#contents-)
```csharp
public interface IResponseDTO : IDTO
{
}
```


### Request DTO Interface

Base Interface for all Request DTOs.

[back up](#contents-)
```csharp
public interface IRequestDTO : IDTO
{
}
```


### ILoginRequest Interface

Interface for LoginRequest & RefreshRequest

[back up](#contents-)
```csharp
public interface ILoginRequest : IRequestDTO
{
}
```

