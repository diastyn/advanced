using Advanced.Domain.Entities.Common;
using Advanced.Domain.Enums;

namespace Advanced.Domain.Entities;

public class UserHistory : BaseEntity
{ 
    public string UserId { get; set; }
    public string ProductId { get; set; }
    public DateTime InteractionDate { get; set; } = DateTime.Now;
    public InteractionType InteractionType { get; set; }
}