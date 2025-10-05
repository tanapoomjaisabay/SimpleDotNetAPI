using BookingAPI.Application.DTOs;
using BookingAPI.Domain.Enums;
using FluentValidation;

namespace BookingAPI.Application.Validators;

/// <summary>
/// Validator for FlightSearchRequestDto
/// Validates business rules for flight search requests
/// </summary>
public class FlightSearchRequestValidator : AbstractValidator<FlightSearchRequestDto>
{
    public FlightSearchRequestValidator()
    {
        // Origin validation
        RuleFor(x => x.Origin)
            .NotEmpty()
            .WithMessage("Origin airport code is required")
            .Length(3)
            .WithMessage("Origin must be a 3-letter airport code")
            .Matches("^[A-Z]{3}$")
            .WithMessage("Origin must contain only uppercase letters");

        // Destination validation
        RuleFor(x => x.Destination)
            .NotEmpty()
            .WithMessage("Destination airport code is required")
            .Length(3)
            .WithMessage("Destination must be a 3-letter airport code")
            .Matches("^[A-Z]{3}$")
            .WithMessage("Destination must contain only uppercase letters");

        // Origin and Destination must be different
        RuleFor(x => x)
            .Must(x => !x.Origin.Equals(x.Destination, StringComparison.OrdinalIgnoreCase))
            .WithMessage("Origin and Destination must be different")
            .When(x => !string.IsNullOrEmpty(x.Origin) && !string.IsNullOrEmpty(x.Destination));

        // Departure Date validation
        RuleFor(x => x.DepartureDate)
            .NotEmpty()
            .WithMessage("Departure date is required")
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage("Departure date cannot be in the past")
            .LessThanOrEqualTo(DateTime.UtcNow.Date.AddYears(1))
            .WithMessage("Departure date cannot be more than 1 year in the future");

        // Cabin Class validation - now using enum
        RuleFor(x => x.CabinClass)
            .IsInEnum()
            .WithMessage("Cabin class must be a valid value");

        // Passenger Count validation
        RuleFor(x => x.PassengerCount)
            .InclusiveBetween(1, 9)
            .WithMessage("Passenger count must be between 1 and 9");
    }
}
