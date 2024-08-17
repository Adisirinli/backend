using BackEndProject_Edu.ViewModels;

namespace BackEndProject_Edu.Services.Interfaces
{
    public interface IBasketService
    {
        int GetBasketCount();
        List<BasketVM> GetBasketList();
    }
}
