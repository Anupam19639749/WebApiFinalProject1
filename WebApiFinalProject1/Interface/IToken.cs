using WebApiFinalProject1.Models;

namespace WebApiFinalProject1.Interface
{
    public interface IToken
    {
        string GenerateToken(User user);
    }
}
