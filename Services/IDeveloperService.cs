﻿using castlers.Dtos;

namespace castlers.Services
{
    public interface IDeveloperService
    {
        public Task<int> DeleteDeveloperAsync(int Id);
        public Task<List<DeveloperDto>> GetDeveloperAsync();
        public Task<DeveloperDto> GetDeveloperByIdAsync(int Id);
        public Task<int> AddDeveloperAsync(DeveloperDto developerDto);
        public Task<int> UpdateDeveloperAsync(DeveloperDto developerDto);
        public Task<List<GenDeveloperDto>> GetDeveloperListPublic();
        public Task AddDeveloperPastProjects(List<DeveloperPastProjectDetailsDto> developerPastProjectDetails, string developerName);
        public Task<int>UpdateDeveloperReviewRating(UpdateDeveloperReviewRatingDto updateDeveloperReviewRatingDto);
    }
}
