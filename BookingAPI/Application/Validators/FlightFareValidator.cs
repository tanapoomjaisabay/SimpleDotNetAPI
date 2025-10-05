using BookingAPI.Domain.Entities;
using BookingAPI.Domain.Enums;
using FluentValidation;

namespace BookingAPI.Application.Validators;

/// <summary>
/// Validator for FlightFare domain entity
/// Validates business rules and invariants for flight fares
/// </summary>
public class FlightFareValidator : AbstractValidator<FlightFare>
{
    private static readonly string[] ValidCurrencies = { "USD", "EUR", "GBP", "JPY", "CAD" };

    public FlightFareValidator()
    {
        // Flight Number validation
        RuleFor(x => x.FlightNumber)
            .NotEmpty()
            .WithMessage("Flight number is required")
            .Matches("^[A-Z]{2}\\d{1,4}$")
            .WithMessage("Flight number must be in format: 2 uppercase letters followed by 1-4 digits (e.g., AA123)");

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

        // Base Fare validation
        RuleFor(x => x.BaseFare)
            .GreaterThan(0)
            .WithMessage("Base fare must be greater than 0")
            .LessThan(100000)
            .WithMessage("Base fare seems unreasonably high");

        // Tax validation
        RuleFor(x => x.Tax)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Tax cannot be negative")
            .LessThan(50000)
            .WithMessage("Tax seems unreasonably high");

        // Currency validation
        RuleFor(x => x.Currency)
            .NotEmpty()
            .WithMessage("Currency is required")
            .Must(currency => ValidCurrencies.Contains(currency, StringComparer.OrdinalIgnoreCase))
            .WithMessage($"Currency must be one of: {string.Join(", ", ValidCurrencies)}");

        // Departure Date validation
        RuleFor(x => x.DepartureDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage("Departure date cannot be in the past");

        // Cabin Class validation - now using enum
        RuleFor(x => x.CabinClass)
            .IsInEnum()
            .WithMessage("Cabin class must be a valid value");
    }
}
