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
        DataReturnModel<dynamic> hitDbByEnquiryID(searchResponse request);
        DataReturnModel<dynamic> hitTravelSearchApi(searchRequest request);
        DataReturnModel<dynamic> hitSmsApi(smsRequest request);
        DataReturnModel<dynamic> hitEmailApi(emailRequest request);
        DataReturnModel<dynamic> hitSelectedPlanApi(selectedPlan request);
        DataReturnModel<dynamic> hitProposalFormApi(proposalForm request);
        DataReturnModel<dynamic> hitPaymentGatewayApi(pgResponse request);
        DataReturnModel<dynamic> hitDownloadPolicyApi(PolicyDownload request);
        DataReturnModel<dynamic> hitLoginApi(loginRequest request);
    }
}