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
    - [TokenData](#tokendata)
  - [Enums](#enums)
    - [RoleNames Enum](#rolenames-enum)
  - [Interfaces](#interfaces)
    - [IGenericModel Interface](#igenericmodel-interface)
<!--/TOC-->

## Core Models

### Service Model
<<MODEL::Service>>

### Priority Model
<<MODEL::Priority>>

### State Model
<<MODEL::State>>

### User Model
<<MODEL::User>>

### Order Model
<<MODEL::Order>>

## DTOs

### Response DTOs

#### DeleteResponse
<<RESDTO::DeleteResponse>>

#### LoginResponse
<<RESDTO::LoginResponse>>

#### OrderResponse
<<RESDTO::OrderResponse>>

#### PriorityResponse
<<RESDTO::PriorityResponse>>

#### ServiceResponse
<<RESDTO::ServiceResponse>>

#### StateResponse
<<RESDTO::StateResponse>>

#### UserResponse
<<RESDTO::UserResponse>>

#### ErrorResponse
<<RESDTO::ErrorResponse>>

### Request DTOs

#### LoginRequest
<<REQDTO::LoginRequest>>

#### RefreshRequest
<<REQDTO::RefreshRequest>>

#### CreateOrderRequest
<<REQDTO::CreateOrderRequest>>

#### CreatePriorityRequest
<<REQDTO::CreatePriorityRequest>>

#### CreateServiceRequest
<<REQDTO::CreateServiceRequest>>

#### CreateStateRequest
<<REQDTO::CreateStateRequest>>

#### CreateUserRequest
<<REQDTO::CreateUserRequest>>

#### UpdateOrderRequest
<<REQDTO::UpdateOrderRequest>>

#### UpdatePriorityRequest
<<REQDTO::UpdatePriorityRequest>>   

#### UpdateServiceRequest
<<REQDTO::UpdateServiceRequest>>

#### UpdateStateRequest
<<REQDTO::UpdateStateRequest>>

#### UpdateUserRequest
<<REQDTO::UpdateUserRequest>>

### TokenData

This DTO is used for any token data sent by the API.

<<DTO::TokenData>>

## Enums

### RoleNames Enum
<<ENUM::RoleNames>>

## Interfaces

### IGenericModel Interface
<<INTERFACE::IGenericModel>>