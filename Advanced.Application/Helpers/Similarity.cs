namespace Advanced.Application.Helpers;

public static class Similarity
{
    public static double CosineSimilarity(Dictionary<string, float> productA, Dictionary<string, float> productB)
    {
        var commonUsers = productA.Keys.Intersect(productB.Keys);
        if (!commonUsers.Any())
            return 0; 

        double dotProduct = 0;
        double normA = 0;
        double normB = 0;

        foreach (var user in commonUsers)
        {
            dotProduct += productA[user] * productB[user];
            normA += Math.Pow(productA[user], 2);
            normB += Math.Pow(productB[user], 2);
        }

        normA = Math.Sqrt(normA);
        normB = Math.Sqrt(normB);

        if (normA == 0 || normB == 0)
            return 0;

        return dotProduct / (normA * normB);
    }
}