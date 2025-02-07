﻿using System.ComponentModel.DataAnnotations;

namespace DemoCICD.Persistence.DependencyInjection.Options;

// Biding from appsettings.json
public record SqlServerRetryOptions
{
    [Required]
    [Range(5, 20)]
    public int MaxRetryCount { get; init; }

    [Required]
    [Timestamp]
    public TimeSpan MaxRetryDelay { get; init; }
    public int[]? ErrorNumbersToAdd { get; init; }
}
