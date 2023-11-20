﻿using SkiServiceAPI.Data;
using SkiServiceAPI.DTOs;

namespace SkiServiceAPI.Interfaces
{
    public interface ITokenService
    {
        TokenData CreateToken(string id, RoleNames role);
    }
}
