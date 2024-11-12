using Advanced.Domain.Enums;

namespace Advanced.Application.Responses;

public class UserHistoryResponse
{
    public string UserId { get; set; }
    public string ProductId { get; set; }
    public DateTime InteractionDate { get; set; } = DateTime.Now;
    public InteractionType InteractionType { get; set; }
}