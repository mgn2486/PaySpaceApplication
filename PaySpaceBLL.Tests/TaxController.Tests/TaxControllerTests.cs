using HelperLibrary;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using PaySpaceApplication.Models;
using PaySpaceBLL.DomainModels;
using PaySpaceDAL.Entities;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PaySpaceBLL.Tests
{
    [TestFixture]
    public class TaxControllerTests
    {
        public readonly HttpClient _httpClient;

        public TaxControllerTests()
        {
            var webApplicationFactory = new WebApplicationFactory<Program>();

            _httpClient = webApplicationFactory.CreateClient();
        }


        [TestCase("Progressive")]
        [TestCase("Flat Value")]
        [TestCase("Flat Rate")]
        public async Task TaxName_CreateTaxName_Fail_On_Error(string name)
        {
            // ARRANGE
            var calculateTax = new TaxNameCreateDto { Name = name };

            // ACT
            var requestResult = await _httpClient.PostAsJsonAsync("tax/CreateTaxName", calculateTax);

            // ASSERT
            requestResult.EnsureSuccessStatusCode();
            var json = await requestResult.Content.ReadAsStringAsync();
            var taxNameResponse = JsonConvert.DeserializeObject<ServiceResponse<TaxNameCreateDto>>(json);

            Assert.AreEqual(taxNameResponse.IsSuccessfull, true);
        }


        [TestCase( 0.01, 0, 8350, 1 )]
        [TestCase( 0.15, 8351, 33950, 1 )]
        [TestCase( 0.25, 33951, 82250, 1 )]
        [TestCase( 0.28, 82251, 171550, 1 )]
        [TestCase( 0.33, 171551, 372950, 1 )]
        [TestCase( 0.35, 372950, 600000, 1 )]
        [TestCase( 0.05, 0, 10000, 2 )]
        [TestCase( 17.5, 0, 0, 3 )]
        public async Task TaxRange_CreateTaxRange_Fail_On_Error(double rate, int startAmount, int endAmount, int taxNameId)
        {
            // ARRANGE
            var createTaxRange = new TaxRangeCreateDto { Rate = rate, StartAmount = startAmount, EndAmount = endAmount, TaxNameId = taxNameId };

            // ACT
            var requestResult = await _httpClient.PostAsJsonAsync("tax/CreateTaxRangeRequest", createTaxRange);

            // ASSERT
            requestResult.EnsureSuccessStatusCode();
            var json = await requestResult.Content.ReadAsStringAsync();
            var taxRangeResponse = JsonConvert.DeserializeObject<ServiceResponse<TaxRangeCreateDto>>(json);

            Assert.AreEqual(taxRangeResponse.IsSuccessfull, true);

        }

        [TestCase("7441", 1)]
        [TestCase("A100", 2)]
        [TestCase("7000", 3)]
        [TestCase("1000", 1)]
        public async Task PostalCode_CreateTaxPostalCode_Fail_On_Error(string postalCode, int taxNameId)
        {
            // ARRANGE
            var createPostalCode = new PostalCodeDto()
            {
                Name = postalCode,
                TaxNameId = taxNameId
            };

            // ACT
            var requestResult = await _httpClient.PostAsJsonAsync("tax/CreateTaxPostal", createPostalCode);

            // ASSERT
            requestResult.EnsureSuccessStatusCode();
            var json = await requestResult.Content.ReadAsStringAsync();
            var postalCodeResponse = JsonConvert.DeserializeObject<ServiceResponse<PostalCodeDto>>(json);

            Assert.AreEqual(postalCodeResponse.IsSuccessfull, true);

        }

        [TestCase(23.89, 1, 23.65)]
        [TestCase(37251, 1, 27938.25)]
        [TestCase(23000, 2, 21850)]
        [TestCase(34423, 3, 28398.98)]
        [TestCase(3333.98, 4, 3300.64)]
        public async Task TaCalculator_GetTaxCalculated_Fail_On_Error(double annualSalary, int postalCodeId, double netSalary)
        {
            // ARRANGE
            var requestCalculation = new CalculateTaxRequestDto()
            {
                AnnualSalary = annualSalary,
                PostalCodeId = postalCodeId
            };

            // ACT
            var requestResult = await _httpClient.PostAsJsonAsync("tax/CalculateSalaryTax", requestCalculation);

            // ASSERT
            requestResult.EnsureSuccessStatusCode();
            var json = await requestResult.Content.ReadAsStringAsync();
            var postalCodeResponse = JsonConvert.DeserializeObject<ServiceResponse<GetCalculatedTaxDto>>(json);

            var data = postalCodeResponse.Data;

            Assert.AreEqual(postalCodeResponse.IsSuccessfull, true);
            Assert.AreEqual(data.NetSalary, netSalary);
        }
    }
}