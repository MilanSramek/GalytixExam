using Domain;

namespace Application;

public sealed record GetAveragePremiumInput
(
    Country Country,
    LineOfBusiness[]? LineOfBusinessItems
);
