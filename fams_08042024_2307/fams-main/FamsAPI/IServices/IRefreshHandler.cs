using DataLayer.Entities;

namespace FamsAPI.IServices
{
    public interface IRefreshHandler
    {
        public void GenerateRefreshToken(RefreshToken refreshToken);
        public void ResetRefreshToken();
        public RefreshToken GetRefreshToken(string refreshToken);
        public RefreshToken GetRefreshTokenByUserID(string userID);
        public void UpdateRefreshToken(RefreshToken refreshToken);
        public void RemoveAllRefreshToken();
    }
}
