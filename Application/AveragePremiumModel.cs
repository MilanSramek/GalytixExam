using Domain;

namespace Application;

public sealed record AveragePremiumModel
(
    LineOfBusiness LineOfBusiness,
    decimal Value
);
