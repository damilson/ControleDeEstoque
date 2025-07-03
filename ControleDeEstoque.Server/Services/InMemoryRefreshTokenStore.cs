namespace ControleDeEstoque.Server.Services
{
    public class InMemoryRefreshTokenStore
    {
        private readonly Dictionary<string, string> _tokens = new();

        public void Save(string user, string refreshToken) => _tokens[refreshToken] = user;
        public string? GetUsername(string refreshToken) => _tokens.TryGetValue(refreshToken, out var user) ? user : null;
        public void Replace(string user, string oldToken, string newToken)
        {
            _tokens.Remove(oldToken);
            Save(user, newToken);
        }
        public void Remove(string refreshToken) => _tokens.Remove(refreshToken);
    }

}
