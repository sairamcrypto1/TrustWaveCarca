using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using TrustWaveCarca.Components.Account.Pages.User;
using TrustWaveCarca.Data;
using TrustWaveCarca.Migrations;

namespace TrustWaveCarca.Reusable
{
    public class InitialLoading
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public List<ChatRequest> Requests { get; private set; } = new List<ChatRequest>();
        public List<PartnerChat> PartnerChats { get; private set; } = new List<PartnerChat>();
       

        public event Action OnChange;
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

        public async Task LoadChatRequestsAsync(string receiverEmail)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();
            Requests = await dbContext.ChatRequest
                .Where(r => r.ReceiverEmail == receiverEmail && r.Status == "Pending" )
                .ToListAsync();
            Console.WriteLine($"Loaded {Requests.Count} chat requests.");

            NotifyStateChanged();
        }
       
        public async Task LoadPartnerChatsAsync(string receiverEmail)
        {
            await using var dbContext = _dbContextFactory.CreateDbContext();
            PartnerChats = await dbContext.PartnerChat
                .Where(p => p.ReceiverEmail == receiverEmail && p.Isdelete == false && p.Status == "Accepted")
                .ToListAsync();
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
