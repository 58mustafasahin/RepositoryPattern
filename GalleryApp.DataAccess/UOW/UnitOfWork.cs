using GalleryApp.DataAccess.EF.Abstract;
using GalleryApp.DataAccess.EF.Concrete.Context;
using GalleryApp.DataAccess.EF.Concrete.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Transactions;

namespace GalleryApp.DataAccess.UOW
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly GalleryDbContext _dbContext;
        private EFAboutRepo _aboutRepo;
        private EFCarRepo _carRepo;
        private EFContactRepo _contactRepo;
        private EFSliderRepo _sliderRepo;

        public UnitOfWork(GalleryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IAboutRepo Abouts => _aboutRepo ??= new EFAboutRepo(_dbContext);
        public ICarRepo Cars => _carRepo ??= new EFCarRepo(_dbContext);
        public IContactRepo Contacts => _contactRepo ??= new EFContactRepo(_dbContext);
        public ISliderRepo Sliders => _sliderRepo ??= new EFSliderRepo(_dbContext);

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> SaveTransactionAsync()
        {
            int result = -1;
            try
            {
                using TransactionScope tScope = new TransactionScope();
                result = await _dbContext.SaveChangesAsync();
                tScope.Complete();
            }
            catch (ValidationException)
            {
                // Todo: Hata yaz. 
            }
            catch (Exception)
            {
                // Todo: Hata yaz. 
            }
            return result;
        }

        public ValueTask DisposeAsync()
        {
            return _dbContext.DisposeAsync();
        }
    }
}
