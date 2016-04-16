using ApplicationDataModel.Context;
using ApplicationDataModel.Repository;
using ApplicationEntity;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDataModel.UOW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool disposed = false;

        private readonly EFDbContext _context = null;
        private BaseRepository<UserEntity> _userRepository;
        private BaseRepository<TokenEntity> _tokenRepository;

        public UnitOfWork()
        {
            _context = new EFDbContext("basicAuthenticate");
        }

        public BaseRepository<UserEntity> UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new BaseRepository<UserEntity>(_context);
                return _userRepository;

            }
        }

        public BaseRepository<TokenEntity> TokenRepository
        {
            get
            {
                if (_tokenRepository == null)
                    _tokenRepository = new BaseRepository<TokenEntity>(_context);
                return _tokenRepository;
            }
        }
        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError("--------EF提交数据，出现异常：" + ex.Message);
                logger.LogError("--------EF提交数据，堆栈消息：" + ex.StackTrace);
            }
        }

        public void RollBack()
        {
            throw new Exception();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    logger.LogInfo("释放UnitOfWork资源");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
