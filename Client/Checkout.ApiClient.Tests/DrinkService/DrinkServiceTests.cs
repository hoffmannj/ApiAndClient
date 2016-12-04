using System.Configuration;
using System.Linq;
using System.Net;
using Checkout;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.DrinkService
{
    [TestFixture(Category = "DrinksApi")]
    public class DrinkServiceTests : BaseServiceTests
    {
        private readonly string _serviceApiUrl;

        public DrinkServiceTests()
        {
            _serviceApiUrl = ConfigurationManager.AppSettings["DrinkServiceApiUrl"];
        }

        [Test]
        public void CreateDrink()
        {
            SetApiUrl();
            var response = CheckoutClient.DrinkService.CreateDrink("Fanta", 1);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Name.Should().Be("Fanta");
            response.Model.Quantity.Should().Be(1);

            CheckoutClient.DrinkService.DeleteDrink("Fanta");
        }

        [Test]
        public void DeleteDrink()
        {
            SetApiUrl();
            CheckoutClient.DrinkService.CreateDrink("Fanta", 1);
            var response = CheckoutClient.DrinkService.DeleteDrink("Fanta");

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Message.Should().BeEquivalentTo("Ok");
        }

        [Test]
        public void GetDrink()
        {
            SetApiUrl();
            CheckoutClient.DrinkService.CreateDrink("Fanta", 1);
            var response = CheckoutClient.DrinkService.GetDrink("Fanta");

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Name.Should().Be("Fanta");
            response.Model.Quantity.Should().Be(1);

            CheckoutClient.DrinkService.DeleteDrink("Fanta");
        }

        [Test]
        public void GetDrinkList()
        {
            SetApiUrl();
            CheckoutClient.DrinkService.CreateDrink("Fanta", 2);
            CheckoutClient.DrinkService.CreateDrink("Cola", 3);
            var response = CheckoutClient.DrinkService.GetDrinkList();

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Count.Should().Be(2);
            response.Model.Data.Count(drink => drink.Name == "Fanta").Should().Be(1);
            response.Model.Data.Count(drink => drink.Name == "Cola").Should().Be(1);

            CheckoutClient.DrinkService.DeleteDrink("Fanta");
            CheckoutClient.DrinkService.DeleteDrink("Cola");
        }

        [Test]
        public void UpdateDrink()
        {
            SetApiUrl();
            CheckoutClient.DrinkService.CreateDrink("Fanta", 3);
            var updateResponse = CheckoutClient.DrinkService.UpdateDrink("Fanta", 5);
            var response = CheckoutClient.DrinkService.GetDrink("Fanta");

            updateResponse.Should().NotBeNull();
            updateResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            updateResponse.Model.Message.Should().BeEquivalentTo("Ok");

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Name.Should().Be("Fanta");
            response.Model.Quantity.Should().Be(5);

            CheckoutClient.DrinkService.DeleteDrink("Fanta");
        }

        private void SetApiUrl()
        {
            AppSettings.BaseApiUri = _serviceApiUrl;
            ApiUrls.ResetApiUrls();
        }
    }
}
