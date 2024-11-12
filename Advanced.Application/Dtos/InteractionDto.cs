using Advanced.Domain.Enums;

namespace Advanced.Application.Dtos;

public record InteractionDto(string UserId, string ProductId, InteractionType InteractionType);