using ApplicationDataModel.UOW;
using ApplicationEntity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppicationServices
{
    public class TokenService : ITokenService
    {
        private readonly UnitOfWork _unitOfWork;

        public TokenService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public TokenEntity GenerateToken(int userId)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddSeconds(
                                              Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
            var tokendomain = new TokenEntity
                                  {
                                      UserId = userId,
                                      AuthToken = token,
                                      IssuedOn = issuedOn,
                                      ExpiresOn = expiredOn
                                  };

            _unitOfWork.TokenRepository.Insert(tokendomain);
            _unitOfWork.Commit();
            var tokenModel = new TokenEntity()
                                 {
                                     UserId = userId,
                                     IssuedOn = issuedOn,
                                     ExpiresOn = expiredOn,
                                     AuthToken = token
                                 };

            return tokenModel;
        }
        public bool ValidateToken(string tokenId)
        {
            var token = _unitOfWork.TokenRepository.Get(t => t.AuthToken == tokenId && t.ExpiresOn > DateTime.Now);
            if (token != null && !(DateTime.Now > token.ExpiresOn))
            {
                token.ExpiresOn = token.ExpiresOn.AddSeconds(
                                              Convert.ToDouble(ConfigurationManager.AppSettings["TokenExpiry"]));
                _unitOfWork.TokenRepository.Update(token);
                _unitOfWork.Commit();
                return true;
            }
            return false;
        }
        public bool Kill(string tokenId)
        {
            _unitOfWork.TokenRepository.Delete(x => x.AuthToken == tokenId);
            _unitOfWork.Commit();
            var isNotDeleted = _unitOfWork.TokenRepository.GetMany(x => x.AuthToken == tokenId).Any();
            if (isNotDeleted) { return false; }
            return true;
        }
        public bool DeleteByUserId(int userId)
        {
            _unitOfWork.TokenRepository.Delete(x => x.UserId == userId);
            _unitOfWork.Commit();

            var isNotDeleted = _unitOfWork.TokenRepository.GetMany(x => x.UserId == userId).Any();
            return !isNotDeleted;
        }
    }
}
