﻿using castlers.Dtos;

namespace castlers.Services
{
    public interface IDeveloperService
    {
        public Task<List<DeveloperDto>> GetDeveloperAsync();
        public Task<DeveloperDto> GetDeveloperByIdAsync(int Id);
        public Task<int> AddDeveloperAsync(DeveloperDto developerDto);
        public Task<int> UpdateDeveloperAsync(DeveloperDto developerDto);
        public Task<int> DeleteDeveloperAsync(int Id);
    }
}
