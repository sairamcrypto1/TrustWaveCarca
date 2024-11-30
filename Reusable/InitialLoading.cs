using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using TrustWaveCarca.Data;
using TrustWaveCarca.Migrations;

namespace TrustWaveCarca.Reusable
{
    public class InitialLoading
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        public InitialLoading(AuthenticationStateProvider authenticationStateProvider , IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _dbContextFactory = dbContextFactory;
        }
        public async Task<string> GetCurrentUserEmailAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.Identity?.Name ?? string.Empty;
        }
        public async Task<UserLoginCredentials?> GetUserCredentialsAsync(string email)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.UserLoginCredentials.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<List<ChatRequest>> GetChatRequestsAsync(string receiverEmail)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.ChatRequest
                .Where(r => r.ReceiverEmail == receiverEmail && r.Status == "Pending")
                .ToListAsync();
        }
        public async Task<List<PartnerChat>> GetChatsAsync(string email)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();

            return await  dbContext.PartnerChat
                    .Where(p => p.ReceiverEmail == email && p.Isdelete == false && p.Status == "Accepted")
                    .ToListAsync();
        }

    }
}
