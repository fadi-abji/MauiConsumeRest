﻿using PartsClient.Data;
namespace PartsClient.Service
{
    public interface IPartsService
    {
        Task<IEnumerable<Part>> GetAll();
        Task<Part> Add(string partName, string supplier, string partType);
        Task Update(Part part);
        Task Delete(string partID);
    }
}