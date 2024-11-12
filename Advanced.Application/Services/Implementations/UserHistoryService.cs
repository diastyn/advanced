using Advanced.Application.Dtos;
using Advanced.Application.Responses;
using Advanced.Application.Services.Interfaces;
using Advanced.Domain.Repositories.Users;

namespace Advanced.Application.Services.Implementations;

public class UserHistoryService : IUserHistoryService
{
    private readonly IUserHistoryRepository _historyRepository;

    public UserHistoryService(IUserHistoryRepository historyRepository)
    {
        _historyRepository = historyRepository;
    }
    
    public async Task AddInteraction(InteractionDto dto) 
        => await _historyRepository.AddInteractionAsync(dto);
    
    public async Task<List<UserHistoryResponse>> GetUserHistory(string userId) 
        => await _historyRepository.GetAsync<UserHistoryResponse>(u => u.UserId == userId);
}