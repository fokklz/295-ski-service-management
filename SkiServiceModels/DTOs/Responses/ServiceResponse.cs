﻿using SkiServiceModels.Interfaces;

namespace SkiServiceModels.DTOs.Responses
{
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
}
