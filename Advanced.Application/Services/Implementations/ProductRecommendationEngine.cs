using Advanced.Application.Dtos;
using Advanced.Application.Helpers;
using Advanced.Application.Services.Interfaces;

namespace Advanced.Application.Services.Implementations;

public class ProductRecommendationEngine : IProductRecommendationEngine
{
    private readonly Dictionary<string, Dictionary<string, float>> _userProductMatrix;

    public ProductRecommendationEngine(List<ProductInteraction> interactions)
    {
        _userProductMatrix = new Dictionary<string, Dictionary<string, float>>();

        foreach (var interaction in interactions)
        {
            if (!_userProductMatrix.ContainsKey(interaction.UserId))
                _userProductMatrix[interaction.UserId] = new Dictionary<string, float>();

            _userProductMatrix[interaction.UserId][interaction.ProductId] = interaction.Rating;
        }
    }

    public List<string> GetRecommendations(string userId, int topN = 5)
    {
        var userRatings = _userProductMatrix[userId];
        var recommendedProducts = new Dictionary<string, double>();

        foreach (var productB in _userProductMatrix.Values.SelectMany(m => m.Keys).Distinct())
        {
            if (!userRatings.ContainsKey(productB))
            {
                double similarityScore = 0;

                foreach (var productA in userRatings.Keys)
                {
                    var productAInteractions = _userProductMatrix.ToDictionary(
                        kvp => kvp.Key, 
                        kvp => kvp.Value.ContainsKey(productA) ? kvp.Value[productA] : 0
                    );

                    var productBInteractions = _userProductMatrix.ToDictionary(
                        kvp => kvp.Key, 
                        kvp => kvp.Value.ContainsKey(productB) ? kvp.Value[productB] : 0
                    );

                    similarityScore += Similarity.CosineSimilarity(productAInteractions, productBInteractions);
                }

                recommendedProducts[productB] = similarityScore;
            }
        }

        return recommendedProducts.OrderByDescending(r => r.Value).Take(topN).Select(r => r.Key).ToList();
    }
}