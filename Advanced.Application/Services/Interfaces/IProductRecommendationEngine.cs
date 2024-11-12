namespace Advanced.Application.Services.Interfaces;

public interface IProductRecommendationEngine
{
    List<string> GetRecommendations(string userId, int topN = 5);
}