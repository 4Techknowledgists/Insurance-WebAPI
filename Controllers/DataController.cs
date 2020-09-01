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
using System.Xml.Serialization;

namespace WebApiTokenAuthentication.Controllers
{
    public class DataController : ApiController
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

        [HttpGet]
        [Route("api/data/createProposalAPIobj")]
        public void CreateProposalAPIobj()
        {

            http://localhost:44330/api/data/createProposalAPIobj


            WebApiTokenAuthentication.Models.clsProposal objProposal = new Models.clsProposal();

            objProposal.ClientDetails = new Models.ClientDetails();
            objProposal.ClientDetails.ClientType = "0";
            objProposal.ClientDetails.Salutation = "MR";
            objProposal.ClientDetails.ForeName = "Quick";
            objProposal.ClientDetails.LastName = "Bima";
            objProposal.ClientDetails.DOB= "12-08-1990";
            objProposal.ClientDetails.Gender= "Male";
            objProposal.ClientDetails.MaritalStatus = "1951";
            objProposal.ClientDetails.OccupationID= "21";
            objProposal.ClientDetails.Nationality= "1949";
            objProposal.ClientDetails.PhoneNo = "855451212121";
            objProposal.ClientDetails.MobileNo = "7206655511";
            objProposal.ClientDetails.Email = "test@quickbima.com";
            
            objProposal.ClientDetails.ClientAddress = new Models.ClientAddress();

            objProposal.ClientDetails.ClientAddress.CommunicationAddress = new Models.CommunicationAddress();
            objProposal.ClientDetails.ClientAddress.CommunicationAddress.Address1= "delhi";
            objProposal.ClientDetails.ClientAddress.CommunicationAddress.Address2 = "delhi";
            objProposal.ClientDetails.ClientAddress.CommunicationAddress.Address3 = "";
            objProposal.ClientDetails.ClientAddress.CommunicationAddress.CityID = "542238";
            objProposal.ClientDetails.ClientAddress.CommunicationAddress.DistrictID = "120";
            objProposal.ClientDetails.ClientAddress.CommunicationAddress.StateID = "10";
            objProposal.ClientDetails.ClientAddress.CommunicationAddress.AreaID= "140408";
            objProposal.ClientDetails.ClientAddress.CommunicationAddress.NearestLandmark = "";
            objProposal.ClientDetails.ClientAddress.CommunicationAddress.Country = "India";
            objProposal.ClientDetails.ClientAddress.CommunicationAddress.Pincode = "110092";
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
            objProposal.ClientDetails.ClientAddress.PermanentAddress.Address.StateID= "10";
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
            objProposal.ChildDetailList.ChildDetails.PassportNo= "";
            objProposal.ChildDetailList.ChildDetails.NomineeName= "";
            objProposal.ChildDetailList.ChildDetails.NomineeRelationshipID = "";

            objProposal.ChildDetailList.ChildDetails.IsUnderMedication = "false";
            objProposal.ChildDetailList.ChildDetails.PreExistingMC = "";
            objProposal.ChildDetailList.ChildDetails.SufferingSince = "";



            
            objProposal.InsuredDetail = new Models.InsuredDetail();
            objProposal.InsuredDetail.RelationshipWithProposerID= "345";
            objProposal.InsuredDetail.PassportNumber = "ARZ956232";
            objProposal.InsuredDetail.NameofNominee = "Ravi";
            objProposal.InsuredDetail.RelationshipWithNomineeID= "320";
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
            objProposal.UniversityDetails.PhoneNumber= "";
            objProposal.UniversityDetails.MobileNumber = "";
            objProposal.UniversityDetails.EmailId = "";
            objProposal.UniversityDetails.Fax = "";

            objProposal.CourseDetails = new Models.CourseDetails();
            objProposal.CourseDetails.CourseDuration = "";
            objProposal.CourseDetails.TutionFeePerSem = "";
            objProposal.CourseDetails.NoOfSems = "";
            

            objProposal.SpouseDetails = new Models.SpouseDetails();
            objProposal.SpouseDetails.FirstName= "";
            objProposal.SpouseDetails.DOB = "";
            objProposal.SpouseDetails.PassportNo = "";
            objProposal.SpouseDetails.RelationshipwithInsuredID = "";
            objProposal.SpouseDetails.NomineeName = "";
            objProposal.SpouseDetails.NomineeRelationshipID = "";


            objProposal.Policy = new Models.Policy();
            objProposal.Policy.BusinessType= "1";
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

            objProposal.RiskDetails.JourneyStartDate ="14-07-2016";
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
            objProposal.SponsorDetails.SponserAddress.StateName= "";
            objProposal.SponsorDetails.SponserAddress.CountryID = "";
            objProposal.SponsorDetails.SponserAddress.Pincode = "";
            objProposal.SponsorDetails.SponserAddress.MobileNo = "";
            objProposal.SponsorDetails.SponserAddress.PhoneNo = "";
            objProposal.SponsorDetails.SponserAddress.Email = "";
            objProposal.SponsorDetails.SponserAddress.Fax= "";

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
            objProposal.LstTravelCoverDetails.LstTravelCovers.IsElitePlan= "false";
            objProposal.LstTravelCoverDetails.LstTravelCovers.IsPlusPlan = "false";
            


            //objProposal.travelDetails = new Models.TravelDetails();

            objProposal.UserID = "100002";
            objProposal.ErrorMessages = "";
            objProposal.SourceSystemID = "MossPortal";
            objProposal.AuthToken = "pass@123";
            sbData = new StringBuilder();
            string strXML = SerializeToXml(objProposal, @"E:\Temp");
        //    XmlDocument xdoc = PostInfoToAPI(objProposal, "ProposalCreationForTravel"); //"CoverageDetailsForTravel");//"PremiumCalulationForTravel");
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

        [HttpPost]
        public XmlDocument CallInsuranceApiPost(string MethodName) //  PostInfoToAPI(object obj, string MethodName)
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
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(data, 0, data.Length);
                dataStream.Close();
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
}
