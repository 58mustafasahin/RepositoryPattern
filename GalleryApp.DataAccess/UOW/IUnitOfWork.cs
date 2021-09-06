using GalleryApp.DataAccess.EF.Abstract;
using System;
using System.Threading.Tasks;

namespace GalleryApp.DataAccess.UOW
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IAboutRepo Abouts { get; }
        ICarRepo Cars { get; }
        IContactRepo Contacts { get; }
        ISliderRepo Sliders { get; }

        int Save();
        Task<int> SaveAsync();
        Task<int> SaveTransactionAsync();
    }
}
