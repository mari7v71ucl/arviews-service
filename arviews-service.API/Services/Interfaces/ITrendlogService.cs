using System.Threading.Tasks;

namespace arviews_service.API.Services
{
    interface ITrendlogService
    {
        Task<(bool IsSuccess, string viewType, string ErrorMessage)> GetViewTypeAsync(int viewId);
    }
}
