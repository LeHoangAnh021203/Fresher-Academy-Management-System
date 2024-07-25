using DataLayer.Entities;
using DataLayer;
using FamsAPI.IServices;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using DataLayer.Repositories;

namespace FamsAPI.Services
{
    public class RefreshHandler : IRefreshHandler
    {
        private readonly RefreshTokenRepository _refreshTokenRepository;
        public RefreshHandler(RefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public void GenerateRefreshToken(RefreshToken token)
        {
            _refreshTokenRepository.Add(token);
            _refreshTokenRepository.SaveChanges();
        }

        public RefreshToken GetRefreshToken(string refreshToken)
        {
            try
            {
                var _refreshToken = _refreshTokenRepository.Get(x => x.RefreshTokenString == refreshToken);
                return _refreshToken;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RefreshToken GetRefreshTokenByUserID(string userID)
        {
            try
            {
                var _refreshToken = _refreshTokenRepository.Get(x => x.UserId.ToString().Equals(userID));
                return _refreshToken;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveAllRefreshToken()
        {
            try {
                var _refreshTokenList = _refreshTokenRepository.GetAll();
                foreach (var item in _refreshTokenList)
                {
                    _refreshTokenRepository.Remove(item);
                }
                _refreshTokenRepository.SaveChanges();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void ResetRefreshToken()
        {
            try
            {
                var _refreshToken = (List<RefreshToken>)_refreshTokenRepository.GetAll();
                foreach (var item in _refreshToken)
                {
                    if (item.Statuses == ReStatuses.Disable || item.ExpireAt <= DateTime.Now)
                    {
                        _refreshTokenRepository.Remove(item);
                        _refreshTokenRepository.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateRefreshToken(RefreshToken _refreshToken)
        {
            try { 
                _refreshToken.Statuses = ReStatuses.Disable;
                _refreshTokenRepository.Update(_refreshToken);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
