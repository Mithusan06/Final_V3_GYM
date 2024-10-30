using System.Threading.Tasks;

namespace Gym_Fees.IService
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile img, string[] fileext);
    }
}
