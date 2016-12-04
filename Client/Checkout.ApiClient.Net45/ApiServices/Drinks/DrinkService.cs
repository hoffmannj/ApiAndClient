using Checkout.ApiServices.Drinks.Models;
using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.Drinks
{
    public class DrinkService
    {
        public HttpResponse<Drink> CreateDrink(string name, int quantity)
        {
            var payLoad = new Drink {Name = name, Quantity = quantity};
            return new ApiHttpClient().PostRequest<Drink>(ApiUrls.Drinks, AppSettings.SecretKey, payLoad);
        }

        public HttpResponse<Drink> GetDrink(string name)
        {
            var getDrinkUri = string.Format(ApiUrls.Drink, name);
            return new ApiHttpClient().GetRequest<Drink>(getDrinkUri, AppSettings.SecretKey);
        }

        public HttpResponse<OkResponse> UpdateDrink(string name, int quantity)
        {
            var payLoad = new Drink { Name = name, Quantity = quantity };
            return new ApiHttpClient().PutRequest<OkResponse>(ApiUrls.Drinks, AppSettings.SecretKey, payLoad);
        }

        public HttpResponse<OkResponse> DeleteDrink(string name)
        {
            var deleteDrinkUri = string.Format(ApiUrls.Drink, name);
            return new ApiHttpClient().DeleteRequest<OkResponse>(deleteDrinkUri, AppSettings.SecretKey);
        }

        public HttpResponse<DrinkList> GetDrinkList()
        {
            return new ApiHttpClient().GetRequest<DrinkList>(ApiUrls.Drinks, AppSettings.SecretKey);
        }
    }
}
