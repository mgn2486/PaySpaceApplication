using AutoMapper;
using HelperLibrary;
using Microsoft.Extensions.Logging;
using PaySpaceBLL.Abstractions;
using PaySpaceBLL.DomainModels;
using PaySpaceDAL;
using PaySpaceDAL.Abstracions;
using PaySpaceDAL.Entities;

namespace PaySpaceBLL.DomainServices
{
    public class PaySpaceTaxService : IPaySpaceTaxService
    {
        private readonly ITaxRepository _taxRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PaySpaceTaxService> _logger;

        public PaySpaceTaxService(ITaxRepository taxRepository, IMapper mapper, ILogger<PaySpaceTaxService> logger)
        {
            _taxRepository = taxRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResponse<TaxNameCreateDto>> CreateTaxNameAsync(string taxName)
        {
            var serviceResponse = new ServiceResponse<TaxNameCreateDto>();
            try
            {
                var newTaxName = new TaxName()
                { Name = taxName };

                var response = await _taxRepository.CreateTaxNameAsync(newTaxName);

                serviceResponse.IsSuccessfull = true;
                serviceResponse.Data = _mapper.Map<TaxNameCreateDto>(response);
                serviceResponse.Message = "Successfully retrieved";
            }
            catch (Exception ex)
            {
                serviceResponse.IsSuccessfull = false;
                serviceResponse.Data = null;
                serviceResponse.Message = "Failed to retrieve the data expected";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<TaxRangeCreateDto>> CreateTaxRangeAsync(TaxRangeCreateDto taxRange)
        {
            var serviceResponse = new ServiceResponse<TaxRangeCreateDto>();

            try
            {
                var taxNameModel = await _taxRepository.GetTaxNameByIdAsync(taxRange.TaxNameId);

                if (taxNameModel is not null)
                {
                    var newTaxRangeModel = new TaxRange()
                    {
                        Rate = taxRange.Rate,
                        StartAmount = (int)taxRange.StartAmount,
                        EndAmount = (int)taxRange.EndAmount,
                        TaxName  = taxNameModel
                    };

                    var listOfTaxRangeModel = await _taxRepository.GetTaxRangesByTaxNameIdAsync(taxRange.TaxNameId);

                    var alreadyExists = listOfTaxRangeModel.Any(x => x.Rate == taxRange.Rate && x.StartAmount == taxRange.StartAmount);

                    if (alreadyExists)
                    {
                        serviceResponse.IsSuccessfull = false;
                        serviceResponse.Data = null;
                        serviceResponse.Message = "Sorry we could not link this range it already exists";

                        return serviceResponse;
                    }

                    var taxRangeCreateResponseModel = await _taxRepository.CreateTaxRangeAsync(newTaxRangeModel);

                    serviceResponse.IsSuccessfull = true;
                    serviceResponse.Data = _mapper.Map<TaxRangeCreateDto>(taxRangeCreateResponseModel);
                    serviceResponse.Message = "Sorry we could not find the tax name to link this range";
                }
                else 
                {
                    serviceResponse.IsSuccessfull = false;
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Sorry we could not find the tax name to link this range";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.IsSuccessfull = false;
                serviceResponse.Data = null;
                serviceResponse.Message = "Failed to create the Tax Range";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPostalCodeDto>>> GetAllPostalCodes()
        {
            var serviceResponse = new ServiceResponse<List<GetPostalCodeDto>>();

            try
            {
                var postalCodes =  await _taxRepository.GetAllPostalCodes();

                serviceResponse.IsSuccessfull = true;
                serviceResponse.Data = _mapper.Map<List<GetPostalCodeDto>>(postalCodes);
                serviceResponse.Message = "Successfully retrieved";                                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in retrieving the Postal codes : {ex.Message}");

                serviceResponse.IsSuccessfull = false;
                serviceResponse.Data = null;
                serviceResponse.Message = "Failed to retrieve the data expected";
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<PostalCodeDto>> CreatePostalCodeAsync(PostalCodeDto postalCode)
        {
            var serviceResponse = new ServiceResponse<PostalCodeDto>();

            try
            {
                var taxNameModel = await _taxRepository.GetTaxNameByIdAsync(postalCode.TaxNameId);

                var postalCodeNameExists = await _taxRepository.GetTaxPostalCodeByNameAsync(postalCode.Name);

                if (taxNameModel is not null && postalCodeNameExists is null)
                {
                    var newPostalCode = new PostalCode()
                    {
                        Name = postalCode.Name,
                        TaxName = taxNameModel
                    };

                    var createdModelEntyt = await _taxRepository.CreateTaxPostalCodeAsync(newPostalCode);

                    if (createdModelEntyt is not null)
                    {
                        serviceResponse.IsSuccessfull = true;
                        serviceResponse.Data = _mapper.Map<PostalCodeDto>(createdModelEntyt); ;
                        serviceResponse.Message = "Successfully created data expected";
                    }
                    else 
                    {
                        serviceResponse.IsSuccessfull = false;
                        serviceResponse.Data = null;
                        serviceResponse.Message = "Failed to create Postal Code";
                    }
                }
                else 
                {
                    serviceResponse.IsSuccessfull = false;
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Sorry we could not Create the Postal Code, it already exists";
                }
            }
            catch (Exception ex) 
            {
                serviceResponse.IsSuccessfull = false;
                serviceResponse.Data = null;
                serviceResponse.Message = "Failed to create the data expected";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<TaxNameDto>>> GetTaxNamesAsync()
        {
            var serviceResponse = new ServiceResponse<List<TaxNameDto>>();

            try
            {
                var taxNamesResponseModel = await _taxRepository.GetAllTaxNamesAsync();

                if (taxNamesResponseModel is not null)
                {
                        serviceResponse.IsSuccessfull = true;
                        serviceResponse.Data = _mapper.Map<List<TaxNameDto>>(taxNamesResponseModel); ;
                        serviceResponse.Message = "Successfully retrieved data as expected";
                }
                else
                {
                    serviceResponse.IsSuccessfull = false;
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Sorry we could not find any registered tax names";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.IsSuccessfull = false;
                serviceResponse.Data = null;
                serviceResponse.Message = "Failed to process the get Tax Names request";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCalculatedTaxDto>> CalculateAnnualTaxAsync(CalculateTaxRequestDto calculateTaxRequest)
        {
            var serviceResponse = new ServiceResponse<GetCalculatedTaxDto>();

            try
            {
                var taxNamesResponseModel = await _taxRepository.GetTaxPostalCodeDetailsByIdAsync(calculateTaxRequest.PostalCodeId);

                if (taxNamesResponseModel is not null)
                {
                    var result = CalculateAnnualTax(taxNamesResponseModel.TaxName, calculateTaxRequest);

                    serviceResponse.IsSuccessfull = true;
                    serviceResponse.Data = result;
                    serviceResponse.Message = "Successfuly retrieved the postal Details";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.IsSuccessfull = false;
                serviceResponse.Data = null;
                serviceResponse.Message = "Failed to calculate the tax";
            }

            return serviceResponse;
        }

        #region Helper Functions
        private GetCalculatedTaxDto CalculateAnnualTax(TaxName taxName, CalculateTaxRequestDto calculateTaxRequest)
        {
            var response = new GetCalculatedTaxDto();
            var taxEnumType = EnumerationHelperExtension.GetValueFromDescription<EnumTaxNameType>(taxName.Name);

            switch (taxEnumType)
            {
                case EnumTaxNameType.Progressive:
                    response = CalculateProgressiveTax(taxName.TaxRanges, calculateTaxRequest);
                   break;

                case EnumTaxNameType.FlatValue:
                    response = CalculateFlatValueTax(taxName.TaxRanges, calculateTaxRequest);
                    break;

                case EnumTaxNameType.FlatRate:
                    response = CalculateFlatRateTax(taxName.TaxRanges, calculateTaxRequest);
                    break;

                default:
                    response = null; 
                    break;

            }

            return response;
        }

        private GetCalculatedTaxDto CalculateProgressiveTax(ICollection<TaxRange> taxRanges, CalculateTaxRequestDto calculateTaxRequest)
        {
            var response = new GetCalculatedTaxDto();

            foreach (var taxRange in taxRanges) 
            {
                if (calculateTaxRequest.AnnualSalary >= taxRange.StartAmount && calculateTaxRequest.AnnualSalary < taxRange.EndAmount)
                {
                    response.TaxAmount = Math.Round(calculateTaxRequest.AnnualSalary * taxRange.Rate, 2);
                    response.NetSalary = Math.Round(calculateTaxRequest.AnnualSalary - response.TaxAmount, 2);

                    return response;
                }
                else 
                {
                    if (calculateTaxRequest.AnnualSalary > 372950)
                    {
                        response.TaxAmount  = Math.Round(calculateTaxRequest.AnnualSalary * 0.35, 2);
                        response.NetSalary = Math.Round(calculateTaxRequest.AnnualSalary - response.TaxAmount, 2);

                        return response;
                    }
                }
            }

            return response;
        }

        private GetCalculatedTaxDto CalculateFlatRateTax(ICollection<TaxRange> taxRanges, CalculateTaxRequestDto calculateTaxRequest)
        {
            var response = new GetCalculatedTaxDto();
            response.TaxAmount = Math.Round(calculateTaxRequest.AnnualSalary * 0.175, 2);
            response.NetSalary = Math.Round(calculateTaxRequest.AnnualSalary - response.TaxAmount, 2);

            return response;
        }

        private GetCalculatedTaxDto CalculateFlatValueTax(ICollection<TaxRange> taxRanges, CalculateTaxRequestDto calculateTaxRequest)
        {
            var response = new GetCalculatedTaxDto();

            foreach (var taxRange in taxRanges)
            {
                if (calculateTaxRequest.AnnualSalary <= 200000)
                {
                    response.TaxAmount = Math.Round(calculateTaxRequest.AnnualSalary * taxRange.Rate, 2);
                    response.NetSalary = Math.Round(calculateTaxRequest.AnnualSalary - response.TaxAmount, 2);

                    return response;
                }
                else
                {
                    response.TaxAmount = Math.Round(calculateTaxRequest.AnnualSalary - 10000, 2);
                    response.NetSalary = Math.Round(calculateTaxRequest.AnnualSalary - response.TaxAmount, 2);

                    return response;
                }
            }

            return response;
        }

        

        #endregion
    }
}
