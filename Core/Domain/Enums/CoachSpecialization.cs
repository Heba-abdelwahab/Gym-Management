namespace Domain.Enums;


[Flags]
public enum CoachSpecialization
{
    None = 0,
    GeneralFitness = 1 << 0,
    WeightLoss = 1 << 1,
    Bodybuilding = 1 << 2,
    StrengthTraining = 1 << 3,
    Powerlifting = 1 << 4,
    CrossFit = 1 << 5,
    FunctionalTraining = 1 << 6,
    CardioTraining = 1 << 7,
    HIIT = 1 << 8,
    Yoga = 1 << 9,
    Pilates = 1 << 10,
    StretchingAndMobility = 1 << 11,
    Rehabilitation = 1 << 12,
    SportsPerformance = 1 << 13,
    MartialArts = 1 << 14,
    Boxing = 1 << 15,
    Kickboxing = 1 << 16,
    MMA = 1 << 17,
    OlympicLifting = 1 << 18,
    SeniorFitness = 1 << 19,
    YouthFitness = 1 << 20,
    PrenatalAndPostnatal = 1 << 21,
    NutritionAndWellness = 1 << 22,
    OnlineCoaching = 1 << 23,
    GroupTraining = 1 << 24
}
