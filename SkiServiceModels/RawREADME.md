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

<<COREMODEL::RefreshResult>>

## EF Models

### Service Model
<<MODEL::ServiceBase>>
<<EFMODEL::Service>>

### Priority Model
<<MODEL::PriorityBase>>
<<EFMODEL::Priority>>

### State Model
<<MODEL::StateBase>>
<<EFMODEL::State>>

### User Model
<<MODEL::UserBase>>
<<EFMODEL::User>>

### Order Model
<<MODEL::OrderBase>>
<<EFMODEL::Order>>

## BSON Models

### Service Model
<<MODEL::ServiceBase>>
<<BSONMODEL::Service>>

### Priority Model
<<MODEL::PriorityBase>>
<<BSONMODEL::Priority>>

### State Model
<<MODEL::StateBase>>
<<BSONMODEL::State>>

### User Model
<<MODEL::UserBase>>
<<BSONMODEL::User>>

### Order Model
<<MODEL::OrderBase>>
<<BSONMODEL::Order>>

## DTOs

### TokenData

This DTO is used for any token data sent by the API.

<<DTO::TokenData>>

### ErrorData

This DTO is used for any error data sent by the API.

<<DTO::ErrorData>>

## Response DTOs

### DeleteResponse
<<RESDTO::DeleteResponse>>

### LoginResponse
<<RESDTO::LoginResponse>>

### OrderResponse
<<RESDTO::OrderResponse>>

### PriorityResponse
<<RESDTO::PriorityResponse>>

### ServiceResponse
<<RESDTO::ServiceResponse>>

### StateResponse
<<RESDTO::StateResponse>>

### UserResponse
<<RESDTO::UserResponse>>

## Request DTOs

### LoginRequest
<<REQDTO::LoginRequest>>

### RefreshRequest
<<REQDTO::RefreshRequest>>

### CreateOrderRequest
<<REQDTO::CreateOrderRequest>>

### CreatePriorityRequest
<<REQDTO::CreatePriorityRequest>>

### CreateServiceRequest
<<REQDTO::CreateServiceRequest>>

### CreateStateRequest
<<REQDTO::CreateStateRequest>>

### CreateUserRequest
<<REQDTO::CreateUserRequest>>

### UpdateOrderRequest
<<REQDTO::UpdateOrderRequest>>

### UpdatePriorityRequest
<<REQDTO::UpdatePriorityRequest>>   

### UpdateServiceRequest
<<REQDTO::UpdateServiceRequest>>

### UpdateStateRequest
<<REQDTO::UpdateStateRequest>>

### UpdateUserRequest
<<REQDTO::UpdateUserRequest>>

## Enums

### RoleNames Enum
<<ENUM::RoleNames>>

## Interfaces

### IGenericModel Interface

Base Interface for all models.

<<INTERFACE::IGenericModel>>


### IGenericEFModel Interface

Base Interface for all models.

<<INTERFACE::IGenericEFModel>>

### IGenericBSONModel Interface

Base Interface for all models.

<<INTERFACE::IGenericBSONModel>>

### DTO Interface

Base Interface for all DTOs.

<<INTERFACE::IDTO>>

### Response DTO Interface

Base Interface for all Response DTOs.

<<INTERFACE::IResponseDTO>>

### Request DTO Interface

Base Interface for all Request DTOs.

<<INTERFACE::IRequestDTO>>

### ILoginRequest Interface

Interface for LoginRequest & RefreshRequest

<<INTERFACE::ILoginRequest>>