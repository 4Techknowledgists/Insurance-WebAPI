using InsuranceBALApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InsuranceBALApi.Models.GenericReturnModel;
using static InsuranceBALApi.Models.TravelBALAPIModel.TravelApiRequestResponseModel;

namespace InsuranceBALApi.Interface
{
    interface  ITravelServiceFunctions
    {
        DataReturnModel<dynamic> HitDbByEnquiryID(SearchResponse request);
        DataReturnModel<dynamic> HitTravelSearchApi(SearchRequest request);
        DataReturnModel<dynamic> HitSmsApi(SmsRequest request);
        DataReturnModel<dynamic> HitEmailApi(EmailRequest request);
        DataReturnModel<dynamic> HitSelectedPlanApi(SelectedPlan request);
        DataReturnModel<dynamic> HitProposalFormApi(ProposalForm request);
        DataReturnModel<dynamic> HitPaymentGatewayApi(PgResponse request);
        DataReturnModel<dynamic> HitDownloadPolicyApi(PolicyDownload request);
        DataReturnModel<dynamic> HitLoginApi(LoginRequest request);
        DataReturnModel<dynamic> SaveCacheData(TravelBALAPIModel.TravelApiResponseModel.EnquiryIDData request);
        DataReturnModel<dynamic> SaveCacheDataToResultByEnquiryID(TravelBALAPIModel.TravelApiResponseModel.EnquiryIDData request);
    }
}