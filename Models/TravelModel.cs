using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml;

namespace InsuranceBALApi.Models
{
    public class TravelBALAPIModel
    {

        public class TravelApiRequestResponseModel
        {
            public class SearchRequest
            {
                public string InsuranceFor { get; set; } // Family/self/student
                public string DestinationCity { get; set; }
                public List<Family> family { get; set; }
                public List<Group> grp { get; set; }
                public List<Student> student { get; set; }
                public string TravelStartDt { get; set; }
                public string TravelEndDt { get; set; }
                public string travelMultipleTimeFlag { get; set; }  //Yes/No
                public string travelMultipleTimeDuration { get; set; }
                public string medicalConditionFlag { get; set; }  //Yes/No
                public string FirstName { get; set; }
                public string LastName { get; set; }
                public string Emailid { get; set; }
                public string MobileNo { get; set; }                
            }
            
            public class Family
            {
                public string selfAge { get; set; }
                public string spouseAge { get; set; }
                public string childAge1 { get; set; }
                public string childAge2 { get; set; }
                public string childAge3 { get; set; }
                public string fatherAge { get; set; }
                public string motherAge { get; set; }
            }

            public class Group
            {
                public string member1 { get; set; }
                public string member2 { get; set; }
                public string member3 { get; set; }
                public string member4 { get; set; }
                public string member5 { get; set; }
                public string member6 { get; set; }
                public string member7 { get; set; }
                public string member8 { get; set; }
            }

            public class Student
            {
                public string stud1 { get; set; }
                public string stud2 { get; set; }
                public string stud3 { get; set; }
                public string stud4 { get; set; }
                public string stud5 { get; set; }
                public string stud6 { get; set; }
                public string stud7 { get; set; }
                public string stud8 { get; set; }
            }


            public class SearchResponse
            {
                public string enquiryID { get; set; } // Family/self/student
                public string tokenID { get; set; } // Family/self/student
            }

            public class Request
            {
                public string InsuranceFor { get; set; } // Family/self/student
                public string DestinationCity { get; set; }
                public string selfAge { get; set; }
                public string spouseAge { get; set; }
                public string childAge1 { get; set; }
                public string childAge2 { get; set; }
                public string childAge3 { get; set; }
                public string fatherAge { get; set; }
                public string motherAge { get; set; }
                public string TravelStartDt { get; set; }
                public string TravelEndDt { get; set; }

                public string travelMultipleTimeFlag { get; set; }  //Yes/No
                public string travelMultipleTimeDuration { get; set; }
                public string medicalConditionFlag { get; set; }  //Yes/No
                public string FirstName { get; set; }
                public string LastName { get; set; }
                public string Emailid { get; set; }
                public string MobileNo { get; set; }



                public string Enquid { get; set; }
                public string CanBeParent { get; set; }
                public string ContactTelephoneSTD { get; set; }
                public string CustomerName { get; set; }
                public string CustomerType { get; set; }
                public string sumInsured { get; set; }
                public string DOB { get; set; }
                public string Age { get; set; }
                public string PermanentAddressLine1 { get; set; }
                public string PermanentAddressLine2 { get; set; }
                public string PermanentCityDistCode { get; set; }
                public string PermanentPinCode { get; set; }
                public string PermanentStateCode { get; set; }
                public string PosPolicyNo { get; set; }
                public string comm_giv_type { get; set; }
                public string PresentAddressLine2 { get; set; }
                public string PresentPinCode { get; set; }
                public string PresentStateCode { get; set; }
                public string ProductName { get; set; }

                public string BusinessType { get; set; }
                public string DealId { get; set; }
                public string MaxTravelDuration { get; set; }
                public string PolicyEffectiveDate { get; set; }
                public string PolicyNumberChar { get; set; }
                public string PolicyType { get; set; }
                public string ProposalDate { get; set; }
                public string Sector { get; set; }
                public string ServiceTaxExemptionCategory { get; set; }
                public string TravelDuration { get; set; }
                public string TravelGeography { get; set; }
                public string TypeOfBusiness { get; set; }
                public string Fromdate { get; set; }
                public string Fromhour { get; set; }
                public string Todate { get; set; }
                public string Tohour { get; set; }

                public string NetPremium { get; set; }
                public string ServiceTax { get; set; }
                public string StampDuty2 { get; set; }
                public string TotalPremium { get; set; }
                public string Title { get; set; }
                public string Gender { get; set; }
                public string maritalStatus { get; set; }
                public string indianPassportFlag { get; set; }
                public string occupation { get; set; }
                public string NomineeName { get; set; }
                public string NomineeRelationship { get; set; }
                public string PassportNo { get; set; }
                public string PED { get; set; }
                public string PEDDeclared { get; set; }
                public string PhoneNo { get; set; }
                public string PhysicianName { get; set; }
                public string PlanType { get; set; }
                public string Relationship { get; set; }
                public string ArrivalCountry { get; set; }
                public string USERID { get; set; }
                public string CREATIONDATE { get; set; }
                public string CouponDiscount { get; set; }
                public string CouponDiscountCODE { get; set; }
                public double PartyComm { get; set; }
                public double PartyCommPer { get; set; }
                public string PartyCommType { get; set; }
                public double TDSPER { get; set; }
                public double tds { get; set; }
                public double cgstONCOMM { get; set; }
                public double sgstONCOMM { get; set; }
                public double igstONCOMM { get; set; }
                public string visaType { get; set; }
                public string purposeOFTravel { get; set; }
                public string travelerIndianFlag { get; set; }
                public string landmark { get; set; }
                public string alternateNumber { get; set; }
                
            }
            
            public class Response
            {
                public string data { get; set; }
                public string Message { get; set; }
                public object StackTrace { get; set; }
                public string ExceptionType { get; set; }
                public string Status { get; set; }
            }

            public class SmsRequest
            {
                public string phoneNo { get; set; }
                public string Message { get; set; }
               
            }
            
            public class EmailRequest
            {
                public string body { get; set; }
                public string to { get; set; }
                public string cc { get; set; }
                public string bcc { get; set; }
                public string subject { get; set; }               

            }

            public class ProposalForm
            {
                public string enquiryID { get; set; }
                public string suminsured { get; set; }
                public string pincode { get; set; }
                public string policyfor { get; set; }
                public string plantype { get; set; }
                public string premiumTenure { get; set; }//(1 year/2 year/3 year)
                public string first_name { get; set; }
                public string last_name { get; set; }
                public string gender { get; set; }
                public string date_of_birth { get; set; }
                public string height { get; set; }
                public string weight { get; set; }
                public string IsProposerSameAsInsured { get; set; }   //(Yes/No)
                public string ProposerMobileNo { get; set; }
                public string ProposerEmail { get; set; }
                public string ProposerPanCardNo { get; set; }
                public string ProposerAddress { get; set; }
                public string ProposerPincode { get; set; }
                public string ProposerState { get; set; }
                public string ProposerCity { get; set; }
             
                public string NomineeName { get; set; }
                public string NomineeRelation { get; set; }
                public string IsUnlimitedAutoRecharge { get; set; }
                public string IsPersonalAccidentOpt { get; set; }
            }

            //public class SelectedPlan
            //{
            //    public string EnquiryID { get; set; }
            //    public string CompanyName { get; set; }
            //    public string PlanName { get; set; }
            //}

            public class SelectedPlan
            {
                public string InsuranceFor { get; set; }
                public string DestinationCity { get; set; }
                public string SelfAge { get; set; }
                public string SpouseAge { get; set; }
                public string ChildAge1 { get; set; }
                public string ChildAge2 { get; set; }
                public string ChildAge3 { get; set; }
                public string FatherAge { get; set; }
                public string MotherAge { get; set; }
                public string Member1 { get; set; }
                public string Member2 { get; set; }
                public string Member3 { get; set; }
                public string Member4 { get; set; }
                public string Member5 { get; set; }
                public string Member6 { get; set; }
                public string Member7 { get; set; }
                public string Member8 { get; set; }
                public string Student1 { get; set; }
                public string Student2 { get; set; }
                public string Student3 { get; set; }
                public string Student4 { get; set; }
                public string Student5 { get; set; }
                public string Student6 { get; set; }
                public string Student7 { get; set; }
                public string Student8 { get; set; }
                public string TravelStartDt { get; set; }
                public string TravelEndDt { get; set; }
                public string TravelMultipleTimeFlag { get; set; }
                public string TravelMultipleTimeDuration { get; set; }
                public string MedicalConditionFlag { get; set; }
                public string FirstName { get; set; }
                public string LastName { get; set; }
                public string Emailid { get; set; }
                public string MobileNo { get; set; }
                public string Enquid { get; set; }
                public string CanBeParent { get; set; }
                public string ContactTelephoneSTD { get; set; }
                public string CustomerName { get; set; }
                public string CustomerType { get; set; }
                public string SumInsured { get; set; }
                public string DOB { get; set; }
                public string Age { get; set; }
                public string PermanentAddressLine1 { get; set; }
                public string PermanentAddressLine2 { get; set; }
                public string PermanentCityDistCode { get; set; }
                public string PermanentPinCode { get; set; }
                public string PermanentStateCode { get; set; }
                public string PosPolicyNo { get; set; }
                public string Comm_giv_type { get; set; }
                public string PresentAddressLine2 { get; set; }
                public string PresentPinCode { get; set; }
                public string PresentStateCode { get; set; }
                public string ProductName { get; set; }
                public string BusinessType { get; set; }
                public string DealId { get; set; }
                public string MaxTravelDuration { get; set; }
                public string PolicyEffectiveDate { get; set; }
                public string PolicyNumberChar { get; set; }
                public string PolicyType { get; set; }
                public string ProposalDate { get; set; }
                public string Sector { get; set; }
                public string ServiceTaxExemptionCategory { get; set; }
                public string TravelDuration { get; set; }
                public string TravelGeography { get; set; }
                public string TypeOfBusiness { get; set; }
                public string Fromdate { get; set; }
                public string Fromhour { get; set; }
                public string Todate { get; set; }
                public string Tohour { get; set; }
                public string NetPremium { get; set; }
                public string ServiceTax { get; set; }
                public string StampDuty2 { get; set; }
                public string TotalPremium { get; set; }
                public string Title { get; set; }
                public string Gender { get; set; }
                public string MaritalStatus { get; set; }
                public string IndianPassportFlag { get; set; }
                public string Occupation { get; set; }
                public string NomineeName { get; set; }
                public string NomineeRelationship { get; set; }
                public string PassportNo { get; set; }
                public string PED { get; set; }
                public string PEDDeclared { get; set; }
                public string PhoneNo { get; set; }
                public string PhysicianName { get; set; }
                public string PlanType { get; set; }
                public string Relationship { get; set; }
                public string ArrivalCountry { get; set; }
                public string USERID { get; set; }
                public string CREATIONDATE { get; set; }
                public string CouponDiscount { get; set; }
                public string CouponDiscountCODE { get; set; }
                public string PartyComm { get; set; }
                public string PartyCommPer { get; set; }
                public string PartyCommType { get; set; }
                public string TDSPER { get; set; }
                public string TDS { get; set; }
                public string CGSTONCOMM { get; set; }
                public string SGSTONCOMM { get; set; }
                public string IGSTONCOMM { get; set; }
                public string visaType { get; set; }
                public string purposeOFTravel { get; set; }
                public string travelerIndianFlag { get; set; }
                public string landmark { get; set; }
                public string alternateNumber { get; set; }
                public string IsPreExistingDiseaseFlag { get; set; }
                public string PreExistingDiseaseAmount { get; set; }
                public string IsAdventureSportsFlag { get; set; }
                public string AdventureSportsAmount { get; set; }
                public string IsPetCareFlag { get; set; }
                public string PetCareAmount { get; set; }
                public string IsCoverageOnCruise { get; set; }
                public string CoverageOnCruiseAmount { get; set; }
                public string IsHomeBurgalaryFlag { get; set; }
                public string HomeBurgalaryAmount { get; set; }
                public string IsCardFraudFlag { get; set; }
                public string CardFlagAmount { get; set; }
                public string IsNonIndianPassportHolderFlag { get; set; }
                public string IsTouristVisaFlag { get; set; }
                public string IsShortTermWorkVisaFlag { get; set; }
                public string IsPermanentResidentCardFlag { get; set; }
                public string IsLongTermWorkVisaFlag { get; set; }
                public string IsDependentVisaFlag { get; set; }
                public string IsDiplomaticVisaFlag { get; set; }
                public string IsStudentVisaFlag { get; set; }
                public string IsGoinfHomeFlag { get; set; }
                public string IsHolidayFlag { get; set; }
                public string IsStudiesFlag { get; set; }
                public string IsRelocationFlag { get; set; }
                public string IsMedicalTreatmentFlag { get; set; }
                public string IsBusinessFlag { get; set; }
            }

            public class PgResponse
            {
                public string enquiryID { get; set; }
                public string companyname { get; set; }
                public string res { get; set; }
            }

            public class PolicyDownload
            {
                public string enquiryID { get; set; }
                public string policyNo { get; set; }
            }

            public class LoginRequest
            {
                public string username { get; set; }
                public string password { get; set; }
            }

        }

        public class TravelApiResponseModel
        {
            public class EnquiryIDData
            {
                public string InsuranceFor { get; set; }
                public string DestinationCity { get; set; }
                public string TravelStartDt { get; set; }
                public string TravelEndDt { get; set; }
                public string travelMultipleTimeFlag { get; set; }
                public string travelMultipleTimeDuration { get; set; }
                public string medicalConditionFlag { get; set; }
                public string Enquid { get; set; }
                public string CanBeParent { get; set; }
                public string sumInsured { get; set; }
                public string comm_giv_type { get; set; }
                public string ProductName { get; set; }
                public string BusinessType { get; set; }
                public string PolicyType { get; set; }
                public string TravelDuration { get; set; }
                public string TravelGeography { get; set; }
                public string TypeOfBusiness { get; set; }
                public string Fromdate { get; set; }
                public string Fromhour { get; set; }
                public string Todate { get; set; }
                public string Tohour { get; set; }
                public string NetPremium { get; set; }
                public string ServiceTax { get; set; }
                public string StampDuty2 { get; set; }
                public string TotalPremium { get; set; }
                public string PlanType { get; set; }
                public string USERID { get; set; }
                public string PartyComm { get; set; }
                public string PartyCommPer { get; set; }
                public string PartyCommType { get; set; }
                public string TDSPER { get; set; }
                public string tds { get; set; }
                public string cgstONCOMM { get; set; }
                public string sgstONCOMM { get; set; }
                public string igstONCOMM { get; set; }
                public string visaType { get; set; }

                public string IsPreExistingDiseaseFlag { get; set; }
                public string PreExistingDiseaseAmount { get; set; }
                public string IsAdventureSportsFlag { get; set; }
                public string AdventureSportsAmount { get; set; }
                public string IsPetCareFlag { get; set; }
                public string PetCareAmount { get; set; }
                public string IsCoverageOnCruise { get; set; }
                public string CoverageOnCruiseAmount { get; set; }
                public string IsHomeBurgalaryFlag { get; set; }
                public string HomeBurgalaryAmount { get; set; }
                public string IsCardFraudFlag { get; set; }
                public string CardFlagAmount { get; set; }
                public string IsNonIndianPassportHolderFlag { get; set; }
                public string IsTouristVisaFlag { get; set; }
                public string IsShortTermWorkVisaFlag { get; set; }
                public string IsPermanentResidentCardFlag { get; set; }
                public string IsLongTermWorkVisaFlag { get; set; }
                public string IsDependentVisaFlag { get; set; }
                public string IsDiplomaticVisaFlag { get; set; }
                public string IsStudentVisaFlag { get; set; }
                public string IsGoinfHomeFlag { get; set; }
                public string IsHolidayFlag { get; set; }
                public string IsStudiesFlag { get; set; }
                public string IsRelocationFlag { get; set; }
                public string IsMedicalTreatmentFlag { get; set; }
                public string IsBusinessFlag { get; set; }


            }
        }


    }

}