using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly GymDbContext _dbContext;

        public DbInitializer(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InitializeAsync()
        {
            //await _dbContext.Database.ExecuteSqlAsync($"DELETE FROM  Connections");
            await _dbContext.Database.ExecuteSqlAsync($"TRUNCATE  TABLE  Connections");

            // Apply any pending migrations before seeding
            if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
            {
                await _dbContext.Database.MigrateAsync();
            }

            // --- Seed Muscles ---
            if (!_dbContext.Muscles.Any())
            {
                var json = await File.ReadAllTextAsync(@"../Infrastructure/Persistence/Seeding/Data/muscles.json");
                // Deserialize directly into a list of dictionaries
                var muscleData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(json);

                if (muscleData?.Any() == true)
                {
                    var muscles = muscleData.Select(data => new Muscle
                    {
                        Name = data["name"],
                        ImageUrl = data["image"]
                    }).ToList();
                    _dbContext.Muscles.AddRange(muscles);
                }

                try
                {
                    // Save muscles first to generate their IDs
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR SEEDING MUSCLES: {ex.Message}");
                    throw;
                }
            }

            // --- Seed Exercises ---
            if (!_dbContext.Exercises.Any())
            {
                var json = await File.ReadAllTextAsync(@"../Infrastructure/Persistence/Seeding/Data/gym_exercises.json");

                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    var musclesFromDb = await _dbContext.Muscles.ToDictionaryAsync(m => m.Name, m => m.Id, StringComparer.OrdinalIgnoreCase);
                    var exercises = new List<Exercise>();

                    foreach (JsonElement element in doc.RootElement.EnumerateArray())
                    {
                        string targetMuscleName = element.GetProperty("targetMuscle").GetString();

                        if (musclesFromDb.TryGetValue(targetMuscleName, out var muscleId))
                        {
                            string instructions = string.Join("\n",
                                element.GetProperty("instructions").EnumerateArray().Select(i => i.GetString())
                            );

                            // Safely get the image URL if it exists
                            string? imageUrl = null;
                            if (element.TryGetProperty("image", out var imageProperty) && imageProperty.ValueKind == JsonValueKind.String)
                            {
                                imageUrl = imageProperty.GetString();
                            }

                            exercises.Add(new Exercise
                            {
                                Name = element.GetProperty("name").GetString(),
                                Description = element.GetProperty("description").GetString(),
                                Instructions = instructions,
                                VideoUrl = element.TryGetProperty("video", out var videoProp) ? videoProp.GetString() : null,
                                ImageUrl = imageUrl, // Assign the image URL here
                                TargetMuscleId = muscleId
                            });
                        }
                    }
                    _dbContext.Exercises.AddRange(exercises);
                }

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR SEEDING EXERCISES: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
