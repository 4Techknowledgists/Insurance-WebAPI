using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

using InsuranceBALApi.Interface;
using InsuranceBALApi.Functions;
using static InsuranceBALApi.Models.GenericReturnModel;
using static InsuranceBALApi.Models.TravelBALAPIModel.TravelApiRequestResponseModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using PayuTest.Common;
using System.Threading.Tasks;
using InsuranceBALApi.Models;
using Newtonsoft.Json.Linq;
using WebApiTokenAuthentication.Models;
//using WebApiTokenAuthentication.Models;

namespace WebApiTokenAuthentication.Controllers
{
    public class TravelController : ApiController
    {
        [AllowAnonymous]
        [Route("api/data/forall")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("Now server time is : " + DateTime.Now.ToString());
        }

        [Authorize]
        [HttpGet]
        [Route("api/data/authenticate")]
        public IHttpActionResult GetAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello" + identity.Name);
        }

        [Authorize(Roles ="admin")]
        [HttpGet]
        [Route("api/data/authorize")]
        public IHttpActionResult GetAuthorizeAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            return Ok("Hello " + identity.Name + " Role: " + string.Join(",",roles.ToList()));
        }

        /// <summary>
        /// 1st Action - Flow begins by requesting Enquiry ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("TravelGetEnquiryID")]
        public async Task<string> GetEnquiryIDAsync([FromBody]EnquiryRequest enquiryRequest)
        {
            //DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
            //dr = ServiceFunctions.HitTravelSearchApi(request);
            
            var enquiryID = Guid.NewGuid().ToString();

            bool travelEnqInsertStatus=await SaveTravelEnquiry(enquiryID, enquiryRequest);
            DataTable dtTravelCache= await FetchAllQuote(enquiryRequest);
            bool fetchCmpQuoteStatus = false;
            if (!(dtTravelCache.Rows.Count > 0))
            {
                fetchCmpQuoteStatus = await FetchCompanyQuote(enquiryRequest);
            }
            return enquiryID;
        }

        private async Task<bool> SaveTravelEnquiry(string enquiryID,EnquiryRequest enquiryRequest)
        {
            try
            {
                string cstr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlParameter[] param = new SqlParameter[12];
                param[0] = new SqlParameter("@enquiryid", enquiryID);
                param[1] = new SqlParameter("@SelfAge", enquiryRequest.SelfAge);
                param[2] = new SqlParameter("@SpouseAge", enquiryRequest.SpouseAge);
                param[3] = new SqlParameter("@Child1Age", enquiryRequest.Child1Age);
                param[4] = new SqlParameter("@Child2Age", enquiryRequest.Child2Age);
                param[5] = new SqlParameter("@Child3Age", enquiryRequest.Child3Age);
                param[6] = new SqlParameter("@FatherAge", enquiryRequest.FatherAge);
                param[7] = new SqlParameter("@MotherAge", enquiryRequest.MotherAge);
                param[8] = new SqlParameter("@TravelMultipleTimes", enquiryRequest.TravelFrequency);
                param[9] = new SqlParameter("@AnyMedicalCondition", enquiryRequest.MedicalCondition);
                param[10] = new SqlParameter("@FromDate", enquiryRequest.TripStartDate);
                param[11] = new SqlParameter("@ToDate", enquiryRequest.TripEndDate);

                new CommonDB().ExecuteNonQuery(cstr, "InsertTravelEnquiry", param);
                return true;



                // DataSet dds = JsonConvert.DeserializeObject<DataSet>(dr.info.res);
                // CALL API FROM HERE AND PASS ENQUIRY ID AND ALL PARAMETER RECEVED FROM UI
            }
            catch (Exception ex)
            {
                throw new Exception();
                return false;
                //ErrorLogging(ex);
                //return 0;
            }
        }
        private async Task<DataTable> FetchAllQuote(EnquiryRequest enquiryRequest)
        {
            DataTable dtTravelCache = new DataTable();
            try
            {
                string cstr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlParameter[] param = new SqlParameter[11];
                param[0] = new SqlParameter("@SelfAge", enquiryRequest.SelfAge);
                param[1] = new SqlParameter("@SpouseAge", enquiryRequest.SpouseAge);
                param[2] = new SqlParameter("@Child1Age", enquiryRequest.Child1Age);
                param[3] = new SqlParameter("@Child2Age", enquiryRequest.Child2Age);
                param[4] = new SqlParameter("@Child3Age", enquiryRequest.Child3Age);
                param[5] = new SqlParameter("@FatherAge", enquiryRequest.FatherAge);
                param[6] = new SqlParameter("@MotherAge", enquiryRequest.MotherAge);
                param[7] = new SqlParameter("@TravelMultipleTimes", enquiryRequest.TravelFrequency);
                param[8] = new SqlParameter("@AnyMedicalCondition", enquiryRequest.MedicalCondition);
                param[9] = new SqlParameter("@FromDate", enquiryRequest.TripStartDate);
                param[10] = new SqlParameter("@ToDate", enquiryRequest.TripEndDate);

                dtTravelCache= new CommonDB().ExecuteQuery(cstr, "SelectTravelQuotes", param);
                //return 0;



                // DataSet dds = JsonConvert.DeserializeObject<DataSet>(dr.info.res);
                // CALL API FROM HERE AND PASS ENQUIRY ID AND ALL PARAMETER RECEVED FROM UI
            }
            catch (Exception ex)
            {
                throw new Exception();

                //ErrorLogging(ex);
                //return 0;
            }
            return dtTravelCache;
        }

        private async Task<bool> FetchCompanyQuote(EnquiryRequest enquiryRequest)
        {
            try
            {
                sbData = new StringBuilder();
                string strXML = SerializeToXml(enquiryRequest, @"E:\Temp");

                bool returnVal = await CallInsuranceApiPost(strXML); //Reliance
                //await CallInsuranceApiPost(strXML); //Iffco
                
                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }


        }

        /// <summary>
        /// This method to fetch data from cache table and then save results to Result table (TravelResult) 
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [Route("GetResultByEnquiryID")]
        public void GetResultByEnquiryID([FromBody]SearchRequest serrequest)
        {
            if (serrequest == null)
                return;

            //XmlDocument doc = new XmlDocument();          
            //sbData = new StringBuilder();
            //string strXML = SerializeToXml(serrequest, @"E:\Temp");
            //doc.LoadXml(strXML);

            string jsonResult = string.Empty;
            string MethodName = "GetResultByEnquiryID";
            string url = "http://localhost:44320/api/data/" + MethodName;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            XmlDocument outdoc = new XmlDocument();
            try
            {
                ////Internal Tag to know Request is sent by dayibpl BAL only
                //XmlElement requestorTag = doc.CreateElement("REQUESTOR");
                //requestorTag.InnerText = "CALLINGFROMBAL2WEBAPI";
                //doc.LastChild.AppendChild(requestorTag);

                byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(serrequest)); // xmlstring.OuterXml);
                request.Method = "POST";
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(data, 0, data.Length);
                dataStream.Close();
                request.ContentType = "application/json; charset=utf-8";
                WebResponse response = request.GetResponse();
                response = request.GetResponse();

                string result = new StreamReader(response.GetResponseStream()).ReadToEnd();

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }



        /// <summary>
        /// Proposal request contains selected plan and this would be sent to Company API and 
        /// as a result we get Payment gateway link which is returned back to UI
        /// </summary>
        [HttpPost]
        [Route("GetPremiumInfo")]
        public string GetPremiumInfo(HttpRequestMessage selPlanrequest)
        {

            if (selPlanrequest == null)
                return "Request Not Valid";

            //just for testing
            //clsProposal objProposal = JsonConvert.DeserializeObject<clsProposal>(selPlanrequest.Content.ReadAsStringAsync().Result);

            string jsonResult = string.Empty;
            string MethodName = "GetPremiumInfo";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:44310/api/data/" + MethodName);

            try
            {
                //Internal Tag to know Request is sent by dayibpl BAL only
                //XmlElement requestorTag = doc.CreateElement("REQUESTOR");
                //requestorTag.InnerText = "CALLINGFROMBAL2WEBAPI";
                //doc.LastChild.AppendChild(requestorTag);

                //string requestData = xmlstring.ToString();
                byte[] data = Encoding.UTF8.GetBytes(selPlanrequest.Content.ReadAsStringAsync().Result);// doc.OuterXml); // xmlstring.OuterXml);
                request.Method = "POST";
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(data, 0, data.Length);
                dataStream.Close();
                request.ContentType = "application/json; charset=utf-8";
                WebResponse response = request.GetResponse();

                string result = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return result;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return msg;
            }


            //get Payment gateway link and return to UI
            //return dr;
        }



        /// <summary>
        /// Proposal request contains selected plan and this would be sent to Company API and 
        /// as a result we get Payment gateway link which is returned back to UI
        /// </summary>
        [HttpPost]
        [Route("CreateProposalForSelectedPlan")]
        public string CreateProposalForSelectedPlan(HttpRequestMessage selPlanrequest)
        {
            
            if (selPlanrequest == null)
                return "Request Not Valid";

            //just for testing
            //clsProposal objProposal = JsonConvert.DeserializeObject<clsProposal>(selPlanrequest.Content.ReadAsStringAsync().Result);

            string jsonResult = string.Empty;
            string MethodName = "CreateProposalForSelectedPlan";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:44310/api/data/" + MethodName);

            try
            {
                //Internal Tag to know Request is sent by dayibpl BAL only
                //XmlElement requestorTag = doc.CreateElement("REQUESTOR");
                //requestorTag.InnerText = "CALLINGFROMBAL2WEBAPI";
                //doc.LastChild.AppendChild(requestorTag);

                //string requestData = xmlstring.ToString();
                byte[] data = Encoding.UTF8.GetBytes(selPlanrequest.Content.ReadAsStringAsync().Result);// doc.OuterXml); // xmlstring.OuterXml);
                request.Method = "POST";
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(data, 0, data.Length);
                dataStream.Close();
                request.ContentType = "application/json; charset=utf-8";
                WebResponse response = request.GetResponse();
               
                string result = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return result;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return msg;
            }


            //get Payment gateway link and return to UI
            //return dr;
        }

        //[HttpPost]
        //[Route("GetTravelPGResp")]
        //public DataReturnModel<dynamic> PaymentgatewayResponse(PgResponse request)
        //{
        //    DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
        //    dr = ServiceFunctions.HitPaymentGatewayApi(request);
        //    return dr;
        //}


        //[HttpPost]
        //[Route("TravelDownloadPolicyFile")]
        //public DataReturnModel<dynamic> GetPolicyPDFToDownload(PolicyDownload request)
        //{
        //    DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
        //    dr = ServiceFunctions.HitDownloadPolicyApi(request);
        //    return dr;
        //}


        private void SaveCacheDataToResultByEnquiryID(SearchRequest request, string enquiryID)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
            TravelBALAPIModel.TravelApiResponseModel.EnquiryIDData enquiryIDData = new TravelBALAPIModel.TravelApiResponseModel.EnquiryIDData();
            enquiryIDData.DestinationCity = request.DestinationCity;            

            dr = ServiceFunctions.SaveCacheDataToResultByEnquiryID(enquiryIDData);
            

            //insert Cache table data to TravelResultCacheByEnquiry table
            //throw new NotImplementedException();
        }
        private void SaveCacheData(SearchRequest request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
            TravelBALAPIModel.TravelApiResponseModel.EnquiryIDData enquiryIDData = new TravelBALAPIModel.TravelApiResponseModel.EnquiryIDData();
            enquiryIDData.DestinationCity = request.DestinationCity;
            //enquiryIDData.DestinationCity = request.Emailid;
            enquiryIDData.TravelEndDt = request.TravelEndDt;
            enquiryIDData.TravelStartDt = request.TravelStartDt;
            enquiryIDData.travelMultipleTimeDuration = request.travelMultipleTimeDuration;
            enquiryIDData.travelMultipleTimeFlag = request.travelMultipleTimeFlag;
            //enquiryIDData.IsStudentVisaFlag = request.student;
            //enquiryIDData.DestinationCity = request.MobileNo;
            //enquiryIDData.firstname= request.FirstName;
            //enquiryIDData.DestinationCity = request.LastName;
            enquiryIDData.InsuranceFor= request.InsuranceFor;
            //enquiryIDData.grp= request.grp;
            //enquiryIDData.family= request.family;
            enquiryIDData.medicalConditionFlag = request.medicalConditionFlag;
            
            dr = ServiceFunctions.SaveCacheData(enquiryIDData);
            
            //insert Cache table data to TravelResultCacheByEnquiry table
            //throw new NotImplementedException();
        }
        private async Task<DataTable> GetAllTravelQuote(string cstr)
        {            
            try
            {
                return (new CommonDB().ExecuteQuery(cstr, "GetAllTravelQuote"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("api/data/createProposalAPIobj")]
        public void CreateProposalAPIobj()
        {
            
            //InsuranceBALApi.Models.RelianceRequest objProposal = new Models.RelianceRequest();

            //objProposal.ClientDetails = new Models.ClientDetails();
            //objProposal.ClientDetails.ClientType = "0";
            //objProposal.ClientDetails.Salutation = "MR";
            //objProposal.ClientDetails.ForeName = "Quick";
            //objProposal.ClientDetails.LastName = "Bima";
            //objProposal.ClientDetails.DOB= "12-08-1990";
            //objProposal.ClientDetails.Gender= "Male";
            //objProposal.ClientDetails.MaritalStatus = "1951";
            //objProposal.ClientDetails.OccupationID= "21";
            //objProposal.ClientDetails.Nationality= "1949";
            //objProposal.ClientDetails.PhoneNo = "855451212121";
            //objProposal.ClientDetails.MobileNo = "7206655511";
            //objProposal.ClientDetails.Email = "test@quickbima.com";
            
            //objProposal.ClientDetails.ClientAddress = new Models.ClientAddress();

            //objProposal.ClientDetails.ClientAddress.CommunicationAddress = new Models.CommunicationAddress();
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.Address1= "delhi";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.Address2 = "delhi";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.Address3 = "";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.CityID = "542238";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.DistrictID = "120";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.StateID = "10";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.AreaID= "140408";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.NearestLandmark = "";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.Country = "India";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.Pincode = "110092";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.MobileNo = "";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.PhoneNo = "";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.Email = "";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.PanNo = "";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.Aadhaar = "";

            //objProposal.ClientDetails.ClientAddress.PermanentAddress = new Models.PermanentAddress();
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.IsPermanentSameasCommAddr = "true";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address = new Models.Address();
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.Address1 = "delhi";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.Address2 = "delhi";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.CityID = "542238";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.DistrictID = "120";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.StateID= "10";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.AreaID = "140408";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.NearestLandmark = "";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.Country = "India";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.Pincode = "110092";

            //objProposal.ChildDetailList = new Models.ChildDetailList();
            //objProposal.ChildDetailList.ChildDetails = new Models.ChildDetails();
            //objProposal.ChildDetailList.ChildDetails.IsUnderMedication = "false";
            //objProposal.ChildDetailList.ChildDetails.ChildName = "";
            //objProposal.ChildDetailList.ChildDetails.ChildRelationID = "";
            //objProposal.ChildDetailList.ChildDetails.DOB = "";
            //objProposal.ChildDetailList.ChildDetails.PassportNo= "";
            //objProposal.ChildDetailList.ChildDetails.NomineeName= "";
            //objProposal.ChildDetailList.ChildDetails.NomineeRelationshipID = "";

            //objProposal.ChildDetailList.ChildDetails.IsUnderMedication = "false";
            //objProposal.ChildDetailList.ChildDetails.PreExistingMC = "";
            //objProposal.ChildDetailList.ChildDetails.SufferingSince = "";
            
            //objProposal.InsuredDetail = new Models.InsuredDetail();
            //objProposal.InsuredDetail.RelationshipWithProposerID= "345";
            //objProposal.InsuredDetail.PassportNumber = "ARZ956232";
            //objProposal.InsuredDetail.NameofNominee = "Ravi";
            //objProposal.InsuredDetail.RelationshipWithNomineeID= "320";
            //objProposal.InsuredDetail.VisitingCountries = "";
            //objProposal.InsuredDetail.IsUnderMedication = "false";
            //objProposal.InsuredDetail.PreExistingIllness = "false";
            //objProposal.InsuredDetail.SufferingSince = "false";
            //objProposal.InsuredDetail.Salutation = "MR";
            //objProposal.InsuredDetail.ForeName = "Quick";
            //objProposal.InsuredDetail.LastName = "Bima";
            //objProposal.InsuredDetail.MidName = "";
            //objProposal.InsuredDetail.Gender = "Male";
            //objProposal.InsuredDetail.DateofBirth = "12-08-1990";
            //objProposal.InsuredDetail.OccupationID = "14";
            //objProposal.InsuredDetail.MobileNo = "9090909090";
            //objProposal.InsuredDetail.PhoneNo = "9090909090";
            //objProposal.InsuredDetail.Email = "test@gmail.com";


            //objProposal.UniversityDetails = new Models.UniversityDetails();
            //objProposal.UniversityDetails.UniversityName = "";
            //objProposal.UniversityDetails.UniversityCountryId = "";
            //objProposal.UniversityDetails.UniversityStateName = "";
            //objProposal.UniversityDetails.CityName = "";
            //objProposal.UniversityDetails.PhoneNumber= "";
            //objProposal.UniversityDetails.MobileNumber = "";
            //objProposal.UniversityDetails.EmailId = "";
            //objProposal.UniversityDetails.Fax = "";

            //objProposal.CourseDetails = new Models.CourseDetails();
            //objProposal.CourseDetails.CourseDuration = "";
            //objProposal.CourseDetails.TutionFeePerSem = "";
            //objProposal.CourseDetails.NoOfSems = "";
            

            //objProposal.SpouseDetails = new Models.SpouseDetails();
            //objProposal.SpouseDetails.FirstName= "";
            //objProposal.SpouseDetails.DOB = "";
            //objProposal.SpouseDetails.PassportNo = "";
            //objProposal.SpouseDetails.RelationshipwithInsuredID = "";
            //objProposal.SpouseDetails.NomineeName = "";
            //objProposal.SpouseDetails.NomineeRelationshipID = "";


            //objProposal.Policy = new Models.Policy();
            //objProposal.Policy.BusinessType= "1";
            //objProposal.Policy.AgentCode = "Direct";
            //objProposal.Policy.AgentName = "Direct";
            //objProposal.Policy.Branch_Name = "Direct";
            //objProposal.Policy.Branch_Code = "9202";
            //objProposal.Policy.ProductCode = "2817";
            //objProposal.Policy.OtherSystemName = "1";

            //objProposal.RiskDetails = new Models.RiskDetails();
            //objProposal.RiskDetails.IsIndianCitizen = "true";
            //objProposal.RiskDetails.IsOverSeasCitizen = "false";
            //objProposal.RiskDetails.IsOCI = "false";
            //objProposal.RiskDetails.IsNONOCI = "false";
            //objProposal.RiskDetails.IsResidingInIndia = "false";
            //objProposal.RiskDetails.PermanentResidenceCountry = "";
            //objProposal.RiskDetails.OCINumber = "";
            //objProposal.RiskDetails.PassportIssuingCountry = "";
            //objProposal.RiskDetails.IsInsuredOnImmigrantVisa = "true";
            //objProposal.RiskDetails.IsTravelInvolvesSportingActivities = "true";
            //objProposal.RiskDetails.SportsActivitiesID = "282, 283";
            //objProposal.RiskDetails.IsSufferingFromPEMC = "false";
            //objProposal.RiskDetails.PreExistDiseaseID = "false";
            //objProposal.RiskDetails.IsVisitingUSACanada = "true";
            //objProposal.RiskDetails.VisitingCountriesID = "";

            //objProposal.RiskDetails.JourneyStartDate ="14-07-2016";
            //objProposal.RiskDetails.JourneyEndDate = "14-08-2016";
            //objProposal.RiskDetails.TravelDays = "32";
            //objProposal.RiskDetails.DateOfBirth = "12-08-1990";
            //objProposal.RiskDetails.CoverageTypeID = "";
            //objProposal.RiskDetails.IsAddOnCover = "false";
            //objProposal.RiskDetails.MaxDaysPerTrip = "";
            //objProposal.RiskDetails.NoOfYears = "";
            //objProposal.RiskDetails.SeniorCitizenPlanID = "";
            //objProposal.RiskDetails.PlanName = "Gold";
            //objProposal.RiskDetails.AddOnBnifitsOpted = "false";

            //objProposal.HomeBurglaryAddress = new Models.HomeBurglaryAddress();
            //objProposal.HomeBurglaryAddress.IsSameAsCommAddr = "true";
            //objProposal.HomeBurglaryAddress.Address = new Models.Address();
            //objProposal.HomeBurglaryAddress.Address.Address1 = "delhi";
            //objProposal.HomeBurglaryAddress.Address.Address2 = "delhi";
            //objProposal.HomeBurglaryAddress.Address.Address3 = "";
            //objProposal.HomeBurglaryAddress.Address.CityID = "542238";
            //objProposal.HomeBurglaryAddress.Address.DistrictID = "120";
            //objProposal.HomeBurglaryAddress.Address.StateID = "10";
            //objProposal.HomeBurglaryAddress.Address.AreaID = "140408";
            //objProposal.HomeBurglaryAddress.Address.NearestLandmark = "";
            //objProposal.HomeBurglaryAddress.Address.Country = "India";
            //objProposal.HomeBurglaryAddress.Address.Pincode = "110092";
            //objProposal.HomeBurglaryAddress.Address.MobileNo = "7206655511";
            //objProposal.HomeBurglaryAddress.Address.PhoneNo = "7206655511";
            //objProposal.HomeBurglaryAddress.Address.Email = "test@quickbima.com";
            //objProposal.HomeBurglaryAddress.Address.Fax = "";
            
            //objProposal.SponsorDetails = new Models.SponsorDetails();
            //objProposal.SponsorDetails.SponsorName = "";
            //objProposal.SponsorDetails.IsSponserAddressSameasCommAddress = "false";
            //objProposal.SponsorDetails.SponserAddress = new Models.SponserAddress();
            //objProposal.SponsorDetails.SponserAddress.Address1 = "";
            //objProposal.SponsorDetails.SponserAddress.CityName = "";
            //objProposal.SponsorDetails.SponserAddress.StateName= "";
            //objProposal.SponsorDetails.SponserAddress.CountryID = "";
            //objProposal.SponsorDetails.SponserAddress.Pincode = "";
            //objProposal.SponsorDetails.SponserAddress.MobileNo = "";
            //objProposal.SponsorDetails.SponserAddress.PhoneNo = "";
            //objProposal.SponsorDetails.SponserAddress.Email = "";
            //objProposal.SponsorDetails.SponserAddress.Fax= "";

            //objProposal.DoctorDetails = new Models.DoctorDetails();
            //objProposal.DoctorDetails.IsDoctorDetails = "true";
            //objProposal.DoctorDetails.Address = new Models.Address();
            //objProposal.DoctorDetails.Address.Address1 = "";
            //objProposal.DoctorDetails.Address.Address2 = "";
            //objProposal.DoctorDetails.Address.Address3 = "";
            //objProposal.DoctorDetails.Address.CityID = "";
            //objProposal.DoctorDetails.Address.DistrictID = "";
            //objProposal.DoctorDetails.Address.StateID = "";
            //objProposal.DoctorDetails.Address.AreaID = "";
            //objProposal.DoctorDetails.Address.NearestLandmark = "";
            //objProposal.DoctorDetails.Address.Country = "India";
            //objProposal.DoctorDetails.Address.Pincode = "";
            //objProposal.DoctorDetails.Address.MobileNo = "";
            //objProposal.DoctorDetails.Address.PhoneNo = "";
            //objProposal.DoctorDetails.Address.Email = "";
            //objProposal.DoctorDetails.Address.Fax = "";

            //objProposal.LstTravelCoverDetails = new Models.LstTravelCoverDetails();
            //objProposal.LstTravelCoverDetails.LstTravelCovers = new Models.LstTravelCovers();
            //objProposal.LstTravelCoverDetails.LstTravelCovers.CoverageName = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.CoverageDisplayName = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.StandardLimit = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.SilverLimit = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.GoldLimit = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.PlatinumLimit = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.BasicLimit = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.EliteLimit = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.PlusLimit = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.StandardDeductible = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.SilverDeductible = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.GoldDeductible = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.PlatinumDeductible = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.BasicDeductible = "false";
            
            //objProposal.LstTravelCoverDetails.LstTravelCovers.IsStandardPlan = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.IsSilverPlan = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.IsGoldPlan = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.IsPlatinumPlan = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.IsBasicPlan = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.IsElitePlan= "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.IsPlusPlan = "false";
            


            ////objProposal.travelDetails = new Models.TravelDetails();

            //objProposal.UserID = "100002";
            //objProposal.ErrorMessages = "";
            //objProposal.SourceSystemID = "MossPortal";
            //objProposal.AuthToken = "pass@123";
           // sbData = new StringBuilder();
            //string strXML = SerializeToXml(objProposal, @"E:\Temp");
        //  //  XmlDocument xdoc = PostInfoToAPI(objProposal, "ProposalCreationForTravel"); //"CoverageDetailsForTravel");//"PremiumCalulationForTravel");
        }


        StringBuilder sbData;
        public string SerializeToXml<T>(T anyobject, string xmlFilePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(anyobject.GetType());

            using (Utf8StringWriter writer = new Utf8StringWriter(sbData,Encoding.UTF8))
            {
                xmlSerializer.Serialize(writer, anyobject);
                
            }
            return sbData.ToString();
        }


        [HttpGet]
        public XmlDocument CallInsuranceApi(string MethodName) //  PostInfoToAPI(object obj, string MethodName)
        {
            MethodName = "createProposalAPIobj";
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://rgipartners.reliancegeneral.co.in/API/Service/" + MethodName);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:44330/api/data/" + MethodName); 
            XmlDocument doc = new XmlDocument();
            try
            {
                string requestData = ""; // obj.ToString();
                byte[] data = Encoding.UTF8.GetBytes(requestData);
                request.Method = "GET";
                //Stream dataStream = request.GetRequestStream();
                //dataStream.Write(data, 0, data.Length);
                //dataStream.Close();
                request.ContentType = "text/xml";
                WebResponse response = request.GetResponse();
                response = request.GetResponse();
                string result = new StreamReader(response.GetResponseStream()).ReadToEnd();
                
                doc.LoadXml(result);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return doc;
        }

        /// <summary>
        /// Post Method
        /// </summary>
        /// <param name="MethodName"></param>
        /// <returns></returns>
        //[HttpPost]
        public async Task<bool> CallInsuranceApiPost(string xmlstring)//HttpRequestMessage xmlstring) //  PostInfoToAPI(object obj, string MethodName)
        //public HttpResponseMessage CallInsuranceApiPost(HttpRequestMessage xmlstring)//HttpRequestMessage xmlstring) //  PostInfoToAPI(object obj, string MethodName)
        {
            //sbData = new StringBuilder();
            //string strXML = SerializeToXml(enquiryRequest, @"E:\Temp");


            bool responseText = await Task.Run(() =>
            {
                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(xmlstring);

                

                //Do mapping of proposal input file to company specific xml
                WebApiTokenAuthentication.Models.clsProposal objProposal = new Models.clsProposal();

                objProposal.ClientDetails = new Models.ClientDetails();
                objProposal.ClientDetails.ClientType = "0";
                objProposal.ClientDetails.Salutation = "MR";
                objProposal.ClientDetails.ForeName = "";
                objProposal.ClientDetails.LastName = "";
                objProposal.ClientDetails.DOB = "";
                objProposal.ClientDetails.Gender = "";
                objProposal.ClientDetails.MaritalStatus = "";
                objProposal.ClientDetails.OccupationID = "";
                objProposal.ClientDetails.Nationality = "1949";
                objProposal.ClientDetails.PhoneNo = "855451212121";
                objProposal.ClientDetails.MobileNo = "7206655511";
                objProposal.ClientDetails.Email =  "test@quickbima.com";

                objProposal.ClientDetails.ClientAddress = new Models.ClientAddress();

                objProposal.ClientDetails.ClientAddress.CommunicationAddress = new Models.CommunicationAddress();
                objProposal.ClientDetails.ClientAddress.CommunicationAddress.Address1 = "";
                objProposal.ClientDetails.ClientAddress.CommunicationAddress.Address2 = "delhi";
                objProposal.ClientDetails.ClientAddress.CommunicationAddress.Address3 = "";
                objProposal.ClientDetails.ClientAddress.CommunicationAddress.CityID = ""; //"542238";
                objProposal.ClientDetails.ClientAddress.CommunicationAddress.DistrictID = "120";
                objProposal.ClientDetails.ClientAddress.CommunicationAddress.StateID = ""; // "10";
                objProposal.ClientDetails.ClientAddress.CommunicationAddress.AreaID = "140408";
                objProposal.ClientDetails.ClientAddress.CommunicationAddress.NearestLandmark = "";
                objProposal.ClientDetails.ClientAddress.CommunicationAddress.Country = "India";
                objProposal.ClientDetails.ClientAddress.CommunicationAddress.Pincode = "";
                objProposal.ClientDetails.ClientAddress.CommunicationAddress.MobileNo = "";
                objProposal.ClientDetails.ClientAddress.CommunicationAddress.PhoneNo = "";
                objProposal.ClientDetails.ClientAddress.CommunicationAddress.Email = "";
                objProposal.ClientDetails.ClientAddress.CommunicationAddress.PanNo = "";
                objProposal.ClientDetails.ClientAddress.CommunicationAddress.Aadhaar = "";

                objProposal.ClientDetails.ClientAddress.PermanentAddress = new Models.PermanentAddress();
                objProposal.ClientDetails.ClientAddress.PermanentAddress.IsPermanentSameasCommAddr = "true";
                objProposal.ClientDetails.ClientAddress.PermanentAddress.Address = new Models.Address();
                objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.Address1 = "delhi";
                objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.Address2 = "delhi";
                objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.CityID = "542238";
                objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.DistrictID = "120";
                objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.StateID = "10";
                objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.AreaID = "140408";
                objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.NearestLandmark = "";
                objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.Country = "India";
                objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.Pincode = "110092";

                objProposal.ChildDetailList = new Models.ChildDetailList();
                objProposal.ChildDetailList.ChildDetails = new Models.ChildDetails();
                objProposal.ChildDetailList.ChildDetails.IsUnderMedication = "false";
                objProposal.ChildDetailList.ChildDetails.ChildName = "";
                objProposal.ChildDetailList.ChildDetails.ChildRelationID = "";
                objProposal.ChildDetailList.ChildDetails.DOB = "";
                objProposal.ChildDetailList.ChildDetails.PassportNo = "";
                objProposal.ChildDetailList.ChildDetails.NomineeName = "";
                objProposal.ChildDetailList.ChildDetails.NomineeRelationshipID = "";

                objProposal.ChildDetailList.ChildDetails.IsUnderMedication = "false";
                objProposal.ChildDetailList.ChildDetails.PreExistingMC = "";
                objProposal.ChildDetailList.ChildDetails.SufferingSince = "";




                objProposal.InsuredDetail = new Models.InsuredDetail();
                objProposal.InsuredDetail.RelationshipWithProposerID = "345";
                objProposal.InsuredDetail.PassportNumber = "ARZ956232";
                objProposal.InsuredDetail.NameofNominee = "";
                objProposal.InsuredDetail.RelationshipWithNomineeID = "";
                objProposal.InsuredDetail.VisitingCountries = "";
                objProposal.InsuredDetail.IsUnderMedication = "false";
                objProposal.InsuredDetail.PreExistingIllness = "false";
                objProposal.InsuredDetail.SufferingSince = "false";
                objProposal.InsuredDetail.Salutation = "MR";
                objProposal.InsuredDetail.ForeName = "Quick";
                objProposal.InsuredDetail.LastName = "Bima";
                objProposal.InsuredDetail.MidName = "";
                objProposal.InsuredDetail.Gender = "Male";
                objProposal.InsuredDetail.DateofBirth = "12-08-1990";
                objProposal.InsuredDetail.OccupationID = "14";
                objProposal.InsuredDetail.MobileNo = "9090909090";
                objProposal.InsuredDetail.PhoneNo = "9090909090";
                objProposal.InsuredDetail.Email = "test@gmail.com";


                objProposal.UniversityDetails = new Models.UniversityDetails();
                objProposal.UniversityDetails.UniversityName = "";
                objProposal.UniversityDetails.UniversityCountryId = "";
                objProposal.UniversityDetails.UniversityStateName = "";
                objProposal.UniversityDetails.CityName = "";
                objProposal.UniversityDetails.PhoneNumber = "";
                objProposal.UniversityDetails.MobileNumber = "";
                objProposal.UniversityDetails.EmailId = "";
                objProposal.UniversityDetails.Fax = "";

                objProposal.CourseDetails = new Models.CourseDetails();
                objProposal.CourseDetails.CourseDuration = "";
                objProposal.CourseDetails.TutionFeePerSem = "";
                objProposal.CourseDetails.NoOfSems = "";


                objProposal.SpouseDetails = new Models.SpouseDetails();
                objProposal.SpouseDetails.FirstName = "";
                objProposal.SpouseDetails.DOB = "";
                objProposal.SpouseDetails.PassportNo = "";
                objProposal.SpouseDetails.RelationshipwithInsuredID = "";
                objProposal.SpouseDetails.NomineeName = "";
                objProposal.SpouseDetails.NomineeRelationshipID = "";


                objProposal.Policy = new Models.Policy();
                objProposal.Policy.BusinessType = "1";
                objProposal.Policy.AgentCode = "Direct";
                objProposal.Policy.AgentName = "Direct";
                objProposal.Policy.Branch_Name = "Direct";
                objProposal.Policy.Branch_Code = "9202";
                objProposal.Policy.ProductCode = "2817";
                objProposal.Policy.OtherSystemName = "1";

                objProposal.RiskDetails = new Models.RiskDetails();
                objProposal.RiskDetails.IsIndianCitizen = "true";
                objProposal.RiskDetails.IsOverSeasCitizen = "false";
                objProposal.RiskDetails.IsOCI = "false";
                objProposal.RiskDetails.IsNONOCI = "false";
                objProposal.RiskDetails.IsResidingInIndia = "false";
                objProposal.RiskDetails.PermanentResidenceCountry = "";
                objProposal.RiskDetails.OCINumber = "";
                objProposal.RiskDetails.PassportIssuingCountry = "";
                objProposal.RiskDetails.IsInsuredOnImmigrantVisa = "true";
                objProposal.RiskDetails.IsTravelInvolvesSportingActivities = "true";
                objProposal.RiskDetails.SportsActivitiesID = "282, 283";
                objProposal.RiskDetails.IsSufferingFromPEMC = "false";
                objProposal.RiskDetails.PreExistDiseaseID = "false";
                objProposal.RiskDetails.IsVisitingUSACanada = "true";
                objProposal.RiskDetails.VisitingCountriesID = "";

                objProposal.RiskDetails.JourneyStartDate = "14-07-2016";
                objProposal.RiskDetails.JourneyEndDate = "14-08-2016";
                objProposal.RiskDetails.TravelDays = "32";
                objProposal.RiskDetails.DateOfBirth = "12-08-1990";
                objProposal.RiskDetails.CoverageTypeID = "";
                objProposal.RiskDetails.IsAddOnCover = "false";
                objProposal.RiskDetails.MaxDaysPerTrip = "";
                objProposal.RiskDetails.NoOfYears = "";
                objProposal.RiskDetails.SeniorCitizenPlanID = "";
                objProposal.RiskDetails.PlanName = "Gold";
                objProposal.RiskDetails.AddOnBnifitsOpted = "false";

                objProposal.HomeBurglaryAddress = new Models.HomeBurglaryAddress();
                objProposal.HomeBurglaryAddress.IsSameAsCommAddr = "true";
                objProposal.HomeBurglaryAddress.Address = new Models.Address();
                objProposal.HomeBurglaryAddress.Address.Address1 = "delhi";
                objProposal.HomeBurglaryAddress.Address.Address2 = "delhi";
                objProposal.HomeBurglaryAddress.Address.Address3 = "";
                objProposal.HomeBurglaryAddress.Address.CityID = "542238";
                objProposal.HomeBurglaryAddress.Address.DistrictID = "120";
                objProposal.HomeBurglaryAddress.Address.StateID = "10";
                objProposal.HomeBurglaryAddress.Address.AreaID = "140408";
                objProposal.HomeBurglaryAddress.Address.NearestLandmark = "";
                objProposal.HomeBurglaryAddress.Address.Country = "India";
                objProposal.HomeBurglaryAddress.Address.Pincode = "110092";
                objProposal.HomeBurglaryAddress.Address.MobileNo = "7206655511";
                objProposal.HomeBurglaryAddress.Address.PhoneNo = "7206655511";
                objProposal.HomeBurglaryAddress.Address.Email = "test@quickbima.com";
                objProposal.HomeBurglaryAddress.Address.Fax = "";

                objProposal.SponsorDetails = new Models.SponsorDetails();
                objProposal.SponsorDetails.SponsorName = "";
                objProposal.SponsorDetails.IsSponserAddressSameasCommAddress = "false";
                objProposal.SponsorDetails.SponserAddress = new Models.SponserAddress();
                objProposal.SponsorDetails.SponserAddress.Address1 = "";
                objProposal.SponsorDetails.SponserAddress.CityName = "";
                objProposal.SponsorDetails.SponserAddress.StateName = "";
                objProposal.SponsorDetails.SponserAddress.CountryID = "";
                objProposal.SponsorDetails.SponserAddress.Pincode = "";
                objProposal.SponsorDetails.SponserAddress.MobileNo = "";
                objProposal.SponsorDetails.SponserAddress.PhoneNo = "";
                objProposal.SponsorDetails.SponserAddress.Email = "";
                objProposal.SponsorDetails.SponserAddress.Fax = "";

                objProposal.DoctorDetails = new Models.DoctorDetails();
                objProposal.DoctorDetails.IsDoctorDetails = "true";
                objProposal.DoctorDetails.Address = new Models.Address();
                objProposal.DoctorDetails.Address.Address1 = "";
                objProposal.DoctorDetails.Address.Address2 = "";
                objProposal.DoctorDetails.Address.Address3 = "";
                objProposal.DoctorDetails.Address.CityID = "";
                objProposal.DoctorDetails.Address.DistrictID = "";
                objProposal.DoctorDetails.Address.StateID = "";
                objProposal.DoctorDetails.Address.AreaID = "";
                objProposal.DoctorDetails.Address.NearestLandmark = "";
                objProposal.DoctorDetails.Address.Country = "India";
                objProposal.DoctorDetails.Address.Pincode = "";
                objProposal.DoctorDetails.Address.MobileNo = "";
                objProposal.DoctorDetails.Address.PhoneNo = "";
                objProposal.DoctorDetails.Address.Email = "";
                objProposal.DoctorDetails.Address.Fax = "";

                objProposal.LstTravelCoverDetails = new Models.LstTravelCoverDetails();
                objProposal.LstTravelCoverDetails.LstTravelCovers = new Models.LstTravelCovers();
                objProposal.LstTravelCoverDetails.LstTravelCovers.CoverageName = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.CoverageDisplayName = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.StandardLimit = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.SilverLimit = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.GoldLimit = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.PlatinumLimit = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.BasicLimit = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.EliteLimit = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.PlusLimit = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.StandardDeductible = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.SilverDeductible = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.GoldDeductible = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.PlatinumDeductible = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.BasicDeductible = "false";

                objProposal.LstTravelCoverDetails.LstTravelCovers.IsStandardPlan = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.IsSilverPlan = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.IsGoldPlan = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.IsPlatinumPlan = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.IsBasicPlan = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.IsElitePlan = "false";
                objProposal.LstTravelCoverDetails.LstTravelCovers.IsPlusPlan = "false";



                //objProposal.travelDetails = new Models.TravelDetails();

                objProposal.UserID = "100002";
                objProposal.ErrorMessages = "";
                objProposal.SourceSystemID = "MossPortal";
                objProposal.AuthToken = "pass@123";



                XmlDocument doc = JsonConvert.DeserializeXmlNode(JsonConvert.SerializeObject(objProposal), "TravelDetails");


                SearchRequest resultingMessage = new SearchRequest();
                string jsonResult = string.Empty;
                string MethodName = "createProposalAPIobjPost";
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://rgipartners.reliancegeneral.co.in/API/Service/" + MethodName);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:44310/api/data/" + MethodName);

                XmlDocument outdoc = new XmlDocument();

                try
                {
                    //HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                    //WebResponse response = request.GetResponse();
                    //Stream responseStream = response.GetResponseStream();
                    //return new StreamReader(responseStream).ReadToEnd();

                    //Internal Tag to know Request is sent by dayibpl BAL only
                    XmlElement requestorTag = doc.CreateElement("REQUESTOR");
                    requestorTag.InnerText = "CALLINGFROMBAL2WEBAPI";
                    doc.LastChild.AppendChild(requestorTag);

                    //string requestData = xmlstring.ToString();
                    byte[] data = Encoding.UTF8.GetBytes(doc.OuterXml); // xmlstring.OuterXml);
                    request.Method = "POST";
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(data, 0, data.Length);
                    dataStream.Close();
                    request.ContentType = "text/xml; encoding=utf-8";
                    WebResponse response = request.GetResponse();
                    //response = request.GetResponse();

                    string result = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    return true;


                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    return false;
                }
            });

            return true;

                        
        }

        /**********************************************/
        ITravelServiceFunctions ServiceFunctions = new TravelServiceFunctions();

        [HttpPost]
        [Route("TravelSearchRequest")]
        public DataReturnModel<dynamic> StoreFlightRequest(SearchRequest request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
            dr = ServiceFunctions.HitTravelSearchApi(request);
            return dr;
        }

        [HttpPost]
        [Route("TravelGetResultByRefNo")]
        public string SearchResult(SearchResponse request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
            dr = ServiceFunctions.HitDbByEnquiryID(request);
            return dr.info.res.ToString();
        }


        [HttpPost]
        [Route("TravelPlanSelected")]
        public DataReturnModel<dynamic> SaveSelectedPlan(SelectedPlan request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
            dr = ServiceFunctions.HitSelectedPlanApi(request);
            return dr;
        }

        [HttpPost]
        //[Route("TravelProposalForm")]
        [Route("api/data/TravelProposalForm")]
        public DataReturnModel<dynamic> SaveProposalPlan([FromBody]ProposalForm requestinp)
        {
            string jsonResult = string.Empty;

            //Do mapping of proposal input file to company specific xml
            //WebApiTokenAuthentication.Models.RelianceRequest objProposal = new Models.RelianceRequest();

            //objProposal.ClientDetails = new Models.ClientDetails();
            //objProposal.ClientDetails.ClientType = "0"; 
            //objProposal.ClientDetails.Salutation = "MR";
            //objProposal.ClientDetails.ForeName = requestinp.first_name;
            //objProposal.ClientDetails.LastName = requestinp.last_name;
            //objProposal.ClientDetails.DOB = requestinp.date_of_birth;
            //objProposal.ClientDetails.Gender = requestinp.gender;
            //objProposal.ClientDetails.MaritalStatus = "1951";
            //objProposal.ClientDetails.OccupationID = "21";
            //objProposal.ClientDetails.Nationality = "1949";
            //objProposal.ClientDetails.PhoneNo =  "855451212121";
            //objProposal.ClientDetails.MobileNo = requestinp.ProposerMobileNo; //"7206655511";
            //objProposal.ClientDetails.Email = requestinp.ProposerEmail;// "test@quickbima.com";

            //objProposal.ClientDetails.ClientAddress = new Models.ClientAddress();

            //objProposal.ClientDetails.ClientAddress.CommunicationAddress = new Models.CommunicationAddress();
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.Address1 = requestinp.ProposerAddress;
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.Address2 = "delhi";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.Address3 = "";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.CityID = requestinp.ProposerCity; //"542238";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.DistrictID = "120";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.StateID = requestinp.ProposerState; // "10";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.AreaID = "140408";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.NearestLandmark = "";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.Country = "India";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.Pincode = requestinp.ProposerPincode;
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.MobileNo = requestinp.ProposerMobileNo;
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.PhoneNo = "";
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.Email = requestinp.ProposerEmail;
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.PanNo = requestinp.ProposerPanCardNo;
            //objProposal.ClientDetails.ClientAddress.CommunicationAddress.Aadhaar = "";

            //objProposal.ClientDetails.ClientAddress.PermanentAddress = new Models.PermanentAddress();
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.IsPermanentSameasCommAddr = "true";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address = new Models.Address();
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.Address1 = "delhi";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.Address2 = "delhi";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.CityID = "542238";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.DistrictID = "120";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.StateID = "10";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.AreaID = "140408";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.NearestLandmark = "";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.Country = "India";
            //objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.Pincode = "110092";

            //objProposal.ChildDetailList = new Models.ChildDetailList();
            //objProposal.ChildDetailList.ChildDetails = new Models.ChildDetails();
            //objProposal.ChildDetailList.ChildDetails.IsUnderMedication = "false";
            //objProposal.ChildDetailList.ChildDetails.ChildName = "";
            //objProposal.ChildDetailList.ChildDetails.ChildRelationID = "";
            //objProposal.ChildDetailList.ChildDetails.DOB = "";
            //objProposal.ChildDetailList.ChildDetails.PassportNo = "";
            //objProposal.ChildDetailList.ChildDetails.NomineeName = "";
            //objProposal.ChildDetailList.ChildDetails.NomineeRelationshipID = "";

            //objProposal.ChildDetailList.ChildDetails.IsUnderMedication = "false";
            //objProposal.ChildDetailList.ChildDetails.PreExistingMC = "";
            //objProposal.ChildDetailList.ChildDetails.SufferingSince = "";




            //objProposal.InsuredDetail = new Models.InsuredDetail();
            //objProposal.InsuredDetail.RelationshipWithProposerID = "345";
            //objProposal.InsuredDetail.PassportNumber = "ARZ956232";
            //objProposal.InsuredDetail.NameofNominee = requestinp.NomineeName;
            //objProposal.InsuredDetail.RelationshipWithNomineeID = requestinp.NomineeRelation;
            //objProposal.InsuredDetail.VisitingCountries = "";
            //objProposal.InsuredDetail.IsUnderMedication = "false";
            //objProposal.InsuredDetail.PreExistingIllness = "false";
            //objProposal.InsuredDetail.SufferingSince = "false";
            //objProposal.InsuredDetail.Salutation = "MR";
            //objProposal.InsuredDetail.ForeName = "Quick";
            //objProposal.InsuredDetail.LastName = "Bima";
            //objProposal.InsuredDetail.MidName = "";
            //objProposal.InsuredDetail.Gender = "Male";
            //objProposal.InsuredDetail.DateofBirth = "12-08-1990";
            //objProposal.InsuredDetail.OccupationID = "14";
            //objProposal.InsuredDetail.MobileNo = "9090909090";
            //objProposal.InsuredDetail.PhoneNo = "9090909090";
            //objProposal.InsuredDetail.Email = "test@gmail.com";


            //objProposal.UniversityDetails = new Models.UniversityDetails();
            //objProposal.UniversityDetails.UniversityName = "";
            //objProposal.UniversityDetails.UniversityCountryId = "";
            //objProposal.UniversityDetails.UniversityStateName = "";
            //objProposal.UniversityDetails.CityName = "";
            //objProposal.UniversityDetails.PhoneNumber = "";
            //objProposal.UniversityDetails.MobileNumber = "";
            //objProposal.UniversityDetails.EmailId = "";
            //objProposal.UniversityDetails.Fax = "";

            //objProposal.CourseDetails = new Models.CourseDetails();
            //objProposal.CourseDetails.CourseDuration = "";
            //objProposal.CourseDetails.TutionFeePerSem = "";
            //objProposal.CourseDetails.NoOfSems = "";


            //objProposal.SpouseDetails = new Models.SpouseDetails();
            //objProposal.SpouseDetails.FirstName = "";
            //objProposal.SpouseDetails.DOB = "";
            //objProposal.SpouseDetails.PassportNo = "";
            //objProposal.SpouseDetails.RelationshipwithInsuredID = "";
            //objProposal.SpouseDetails.NomineeName = "";
            //objProposal.SpouseDetails.NomineeRelationshipID = "";


            //objProposal.Policy = new Models.Policy();
            //objProposal.Policy.BusinessType = "1";
            //objProposal.Policy.AgentCode = "Direct";
            //objProposal.Policy.AgentName = "Direct";
            //objProposal.Policy.Branch_Name = "Direct";
            //objProposal.Policy.Branch_Code = "9202";
            //objProposal.Policy.ProductCode = "2817";
            //objProposal.Policy.OtherSystemName = "1";

            //objProposal.RiskDetails = new Models.RiskDetails();
            //objProposal.RiskDetails.IsIndianCitizen = "true";
            //objProposal.RiskDetails.IsOverSeasCitizen = "false";
            //objProposal.RiskDetails.IsOCI = "false";
            //objProposal.RiskDetails.IsNONOCI = "false";
            //objProposal.RiskDetails.IsResidingInIndia = "false";
            //objProposal.RiskDetails.PermanentResidenceCountry = "";
            //objProposal.RiskDetails.OCINumber = "";
            //objProposal.RiskDetails.PassportIssuingCountry = "";
            //objProposal.RiskDetails.IsInsuredOnImmigrantVisa = "true";
            //objProposal.RiskDetails.IsTravelInvolvesSportingActivities = "true";
            //objProposal.RiskDetails.SportsActivitiesID = "282, 283";
            //objProposal.RiskDetails.IsSufferingFromPEMC = "false";
            //objProposal.RiskDetails.PreExistDiseaseID = "false";
            //objProposal.RiskDetails.IsVisitingUSACanada = "true";
            //objProposal.RiskDetails.VisitingCountriesID = "";

            //objProposal.RiskDetails.JourneyStartDate = "14-07-2016";
            //objProposal.RiskDetails.JourneyEndDate = "14-08-2016";
            //objProposal.RiskDetails.TravelDays = "32";
            //objProposal.RiskDetails.DateOfBirth = "12-08-1990";
            //objProposal.RiskDetails.CoverageTypeID = "";
            //objProposal.RiskDetails.IsAddOnCover = "false";
            //objProposal.RiskDetails.MaxDaysPerTrip = "";
            //objProposal.RiskDetails.NoOfYears = "";
            //objProposal.RiskDetails.SeniorCitizenPlanID = "";
            //objProposal.RiskDetails.PlanName = "Gold";
            //objProposal.RiskDetails.AddOnBnifitsOpted = "false";

            //objProposal.HomeBurglaryAddress = new Models.HomeBurglaryAddress();
            //objProposal.HomeBurglaryAddress.IsSameAsCommAddr = "true";
            //objProposal.HomeBurglaryAddress.Address = new Models.Address();
            //objProposal.HomeBurglaryAddress.Address.Address1 = "delhi";
            //objProposal.HomeBurglaryAddress.Address.Address2 = "delhi";
            //objProposal.HomeBurglaryAddress.Address.Address3 = "";
            //objProposal.HomeBurglaryAddress.Address.CityID = "542238";
            //objProposal.HomeBurglaryAddress.Address.DistrictID = "120";
            //objProposal.HomeBurglaryAddress.Address.StateID = "10";
            //objProposal.HomeBurglaryAddress.Address.AreaID = "140408";
            //objProposal.HomeBurglaryAddress.Address.NearestLandmark = "";
            //objProposal.HomeBurglaryAddress.Address.Country = "India";
            //objProposal.HomeBurglaryAddress.Address.Pincode = "110092";
            //objProposal.HomeBurglaryAddress.Address.MobileNo = "7206655511";
            //objProposal.HomeBurglaryAddress.Address.PhoneNo = "7206655511";
            //objProposal.HomeBurglaryAddress.Address.Email = "test@quickbima.com";
            //objProposal.HomeBurglaryAddress.Address.Fax = "";

            //objProposal.SponsorDetails = new Models.SponsorDetails();
            //objProposal.SponsorDetails.SponsorName = "";
            //objProposal.SponsorDetails.IsSponserAddressSameasCommAddress = "false";
            //objProposal.SponsorDetails.SponserAddress = new Models.SponserAddress();
            //objProposal.SponsorDetails.SponserAddress.Address1 = "";
            //objProposal.SponsorDetails.SponserAddress.CityName = "";
            //objProposal.SponsorDetails.SponserAddress.StateName = "";
            //objProposal.SponsorDetails.SponserAddress.CountryID = "";
            //objProposal.SponsorDetails.SponserAddress.Pincode = "";
            //objProposal.SponsorDetails.SponserAddress.MobileNo = "";
            //objProposal.SponsorDetails.SponserAddress.PhoneNo = "";
            //objProposal.SponsorDetails.SponserAddress.Email = "";
            //objProposal.SponsorDetails.SponserAddress.Fax = "";

            //objProposal.DoctorDetails = new Models.DoctorDetails();
            //objProposal.DoctorDetails.IsDoctorDetails = "true";
            //objProposal.DoctorDetails.Address = new Models.Address();
            //objProposal.DoctorDetails.Address.Address1 = "";
            //objProposal.DoctorDetails.Address.Address2 = "";
            //objProposal.DoctorDetails.Address.Address3 = "";
            //objProposal.DoctorDetails.Address.CityID = "";
            //objProposal.DoctorDetails.Address.DistrictID = "";
            //objProposal.DoctorDetails.Address.StateID = "";
            //objProposal.DoctorDetails.Address.AreaID = "";
            //objProposal.DoctorDetails.Address.NearestLandmark = "";
            //objProposal.DoctorDetails.Address.Country = "India";
            //objProposal.DoctorDetails.Address.Pincode = "";
            //objProposal.DoctorDetails.Address.MobileNo = "";
            //objProposal.DoctorDetails.Address.PhoneNo = "";
            //objProposal.DoctorDetails.Address.Email = "";
            //objProposal.DoctorDetails.Address.Fax = "";

            //objProposal.LstTravelCoverDetails = new Models.LstTravelCoverDetails();
            //objProposal.LstTravelCoverDetails.LstTravelCovers = new Models.LstTravelCovers();
            //objProposal.LstTravelCoverDetails.LstTravelCovers.CoverageName = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.CoverageDisplayName = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.StandardLimit = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.SilverLimit = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.GoldLimit = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.PlatinumLimit = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.BasicLimit = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.EliteLimit = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.PlusLimit = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.StandardDeductible = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.SilverDeductible = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.GoldDeductible = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.PlatinumDeductible = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.BasicDeductible = "false";

            //objProposal.LstTravelCoverDetails.LstTravelCovers.IsStandardPlan = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.IsSilverPlan = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.IsGoldPlan = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.IsPlatinumPlan = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.IsBasicPlan = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.IsElitePlan = "false";
            //objProposal.LstTravelCoverDetails.LstTravelCovers.IsPlusPlan = "false";



            ////objProposal.travelDetails = new Models.TravelDetails();

            //objProposal.UserID = "100002";
            //objProposal.ErrorMessages = "";
            //objProposal.SourceSystemID = "MossPortal";
            //objProposal.AuthToken = "pass@123";



            //XmlDocument doc = JsonConvert.DeserializeXmlNode(JsonConvert.SerializeObject(objProposal), "TravelDetails");

            //string MethodName = "createProposalAPIobjPost";
            ////HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://rgipartners.reliancegeneral.co.in/API/Service/" + MethodName);
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:44310/api/data/" + MethodName);
            //XmlDocument outdoc = new XmlDocument();
            try
            {
                //    //Internal Tag to know Request is sent by dayibpl BAL only
                //    XmlElement requestorTag = doc.CreateElement("REQUESTOR");
                //    requestorTag.InnerText = "CALLINGFROMBAL2WEBAPI";
                //    doc.LastChild.AppendChild(requestorTag);

                //    //string requestData = xmlstring.ToString();
                //    byte[] data = Encoding.UTF8.GetBytes(doc.OuterXml); // xmlstring.OuterXml);
                //    request.Method = "POST";
                //    Stream dataStream = request.GetRequestStream();
                //    dataStream.Write(data, 0, data.Length);
                //    dataStream.Close();
                //    request.ContentType = "text/xml; encoding=utf-8";
                //    WebResponse response = request.GetResponse();
                //    response = request.GetResponse();

                //    string result = new StreamReader(response.GetResponseStream()).ReadToEnd();

                //    //string xml = "<Test><Name>Test class</Name><X>100</X><Y>200</Y></Test>";
                //    //outdoc.LoadXml(result);
                //    //outdoc.LoadXml(outdoc.OuterXml);
                //    //jsonResult = JsonConvert.SerializeXmlNode(outdoc, Newtonsoft.Json.Formatting.None,false);

                var resp = Request.CreateResponse(HttpStatusCode.OK);
                //resp.Content = new StringContent(result, Encoding.UTF8, "application/json");
                return null;
                //return resp;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }

            //var respFinal = Request.CreateResponse(HttpStatusCode.OK);
            //respFinal.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            //return respFinal;


            //DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
            //dr = ServiceFunctions.hitProposalFormApi(request);
            //return dr;
        }

        [HttpPost]
        [Route("api/data/InsertImage")]
        public IHttpActionResult InsertImage(ImageData imageData)
        {
            System.Data.SqlClient.SqlConnection conn = null;
            try
            {
                //Image save to database code here
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotModified, ex.Message);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
            return Content(HttpStatusCode.OK, "");
        }

        [HttpPost]
        [Route("TravelPG")]
        public DataReturnModel<dynamic> PgResponse(PgResponse request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
            dr = ServiceFunctions.HitPaymentGatewayApi(request);
            return dr;
        }


        [HttpPost]
        [Route("TravelDownloadPolicy")]
        public DataReturnModel<dynamic> GetPolicyPDF(PolicyDownload request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
            dr = ServiceFunctions.HitDownloadPolicyApi(request);
            return dr;
        }


        [HttpPost]
        [Route("TravelSendSMS")]
        public DataReturnModel<dynamic> SendSMS(SmsRequest request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
            dr = ServiceFunctions.HitSmsApi(request);
            return dr;
        }

        [HttpPost]
        [Route("TravelSendEmail")]
        public DataReturnModel<dynamic> SendEMAIL(EmailRequest request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
            dr = ServiceFunctions.HitEmailApi(request);
            return dr;
        }

        [HttpPost]
        [Route("TravelCheckUser")]
        public DataReturnModel<dynamic> CheckUser(LoginRequest request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
            dr = ServiceFunctions.HitLoginApi(request);
            return dr;
        }


        /**********************************************/



    }
    public class Utf8StringWriter : StringWriter
    {
        private Encoding uTF8;

        public Utf8StringWriter(StringBuilder sb, Encoding uTF8) : base(sb)
        {
            this.uTF8 = uTF8;
        }

        public override Encoding Encoding => Encoding.UTF8;
    }
    //ImageData class
    public class ImageData
    {
        public int Id { get; set; }
        public byte[] ImageValue { get; set; }
    }
}
