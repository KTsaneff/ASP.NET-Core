﻿namespace CinemaWebApp.Services.Data.Interfaces
{
    public interface IBaseService
    {
        bool IsGuidValid(string? id, ref Guid parsedGuid);
    }
}