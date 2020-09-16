using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Xml.Serialization;

namespace WebApiTokenAuthentication.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class CommunicationAddress
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string CityID { get; set; }
        public string DistrictID { get; set; }
        public string StateID { get; set; }
        public string AreaID { get; set; }
        public string NearestLandmark { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string PanNo { get; set; }
        public string Aadhaar { get; set; }
    }

    public class Address
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string CityID { get; set; }
        public string DistrictID { get; set; }
        public string StateID { get; set; }
        public string AreaID { get; set; }
        public string NearestLandmark { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
    }

    public class PermanentAddress
    {
        public string IsPermanentSameasCommAddr { get; set; }
        public Address Address { get; set; }
    }

    public class ClientAddress
    {
        public CommunicationAddress CommunicationAddress { get; set; }
        public PermanentAddress PermanentAddress { get; set; }
    }

    public class ClientDetails
    {
        public string ClientType { get; set; }
        public string Salutation { get; set; }
        public string ForeName { get; set; }
        public string LastName { get; set; }
        public string MidName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string OccupationID { get; set; }
        public string MaritalStatus { get; set; }
        public string Nationality { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public ClientAddress ClientAddress { get; set; }
    }

    public class InsuredDetail
    {
        public string RelationshipWithProposerID { get; set; }
        public string PassportNumber { get; set; }
        public string NameofNominee { get; set; }
        public string RelationshipWithNomineeID { get; set; }
        public string VisitingCountries { get; set; }
        public string IsUnderMedication { get; set; }
        public string PreExistingIllness { get; set; }
        public string SufferingSince { get; set; }
        public string Salutation { get; set; }
        public string ForeName { get; set; }
        public string LastName { get; set; }
        public string MidName { get; set; }
        public string Gender { get; set; }
        public string DateofBirth { get; set; }
        public string OccupationID { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
    }

    public class SpouseDetails
    {
        public string FirstName { get; set; }
        public string DOB { get; set; }
        public string PassportNo { get; set; }
        public string RelationshipwithInsuredID { get; set; }
        public string NomineeName { get; set; }
        public string NomineeRelationshipID { get; set; }
    }

    public class ChildDetails
    {
        public string ChildName { get; set; }
        public string ChildRelationID { get; set; }
        public string DOB { get; set; }
        public string PassportNo { get; set; }
        public string NomineeName { get; set; }
        public string NomineeRelationshipID { get; set; }
        public string IsUnderMedication { get; set; }
        public string PreExistingMC { get; set; }
        public string SufferingSince { get; set; }
    }

    public class ChildDetailList
    {
        public ChildDetails ChildDetails { get; set; }
    }

    public class UniversityDetails
    {
        public string UniversityName { get; set; }
        public string UniversityCountryId { get; set; }
        public string UniversityStateName { get; set; }
        public string CityName { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string Fax { get; set; }
    }

    public class CourseDetails
    {
        public string CourseDuration { get; set; }
        public string TutionFeePerSem { get; set; }
        public string NoOfSems { get; set; }
    }

    public class HomeBurglaryAddress
    {
        public string IsSameAsCommAddr { get; set; }
        public Address Address { get; set; }
    }

    public class SponserAddress
    {
        public string Address1 { get; set; }
        public List<Address> Address2 { get; set; }
        public List<Address> Address3 { get; set; }
        public string CityID { get; set; }
        public string DistrictID { get; set; }
        public string StateID { get; set; }
        public string NearestLandmark { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryID { get; set; }
    }

    public class SponsorDetails
    {
        public string SponsorName { get; set; }
        public string IsSponserAddressSameasCommAddress { get; set; }
        public SponserAddress SponserAddress { get; set; }
    }
     

    public class DoctorDetails
    {
        public string IsDoctorDetails { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }

    public class Policy
    {
        public string BusinessType { get; set; }
        public string AgentCode { get; set; }
        public string AgentName { get; set; }
        public string Branch_Name { get; set; }
        public string Branch_Code { get; set; }
        public string ProductCode { get; set; }
        public string OtherSystemName { get; set; }
    }

    public class RiskDetails
    {
        public string IsIndianCitizen { get; set; }
        public string IsOverSeasCitizen { get; set; }
        public string IsOCI { get; set; }
        public string IsNONOCI { get; set; }
        public string IsResidingInIndia { get; set; }
        public string PermanentResidenceCountry { get; set; }
        public string OCINumber { get; set; }
        public string PassportIssuingCountry { get; set; }
        public string IsInsuredOnImmigrantVisa { get; set; }
        public string IsTravelInvolvesSportingActivities { get; set; }
        public string SportsActivitiesID { get; set; }
        public string IsSufferingFromPEMC { get; set; }
        public string PreExistDiseaseID { get; set; }
        public string IsVisitingUSACanada { get; set; }
        public string VisitingCountriesID { get; set; }
        public string JourneyStartDate { get; set; }
        public string JourneyEndDate { get; set; }
        public string TravelDays { get; set; }
        public string DateOfBirth { get; set; }
        public string CoverageTypeID { get; set; }
        public string IsAddOnCover { get; set; }
        public string MaxDaysPerTrip { get; set; }
        public string NoOfYears { get; set; }
        public string SeniorCitizenPlanID { get; set; }
        public string PlanName { get; set; }
        public string AddOnBnifitsOpted { get; set; }
    }


    public class LstTravelCovers
    {
        public string CoverageName { get; set; }
        public string CoverageDisplayName { get; set; }

        public string StandardLimit { get; set; }

        public string SilverLimit { get; set; }

        public string GoldLimit { get; set; }

        public string PlatinumLimit { get; set; }

        public string BasicLimit { get; set; }

        public string EliteLimit { get; set; }
        public string PlusLimit { get; set; }
        public string StandardDeductible { get; set; }
        public string SilverDeductible { get; set; }
        public string GoldDeductible { get; set; }
        public string PlatinumDeductible { get; set; }
        public string BasicDeductible { get; set; }
        public string IsStandardPlan { get; set; }
        public string IsSilverPlan { get; set; }
        public string IsGoldPlan { get; set; }
        public string IsPlatinumPlan { get; set; }
        public string IsBasicPlan { get; set; }
        public string IsElitePlan { get; set; }
        public string IsPlusPlan { get; set; }
    }

    public class LstTravelCoverDetails
    {
        public LstTravelCovers LstTravelCovers { get; set; }
    }

    public class clsProposal
    {
        public string UserID { get; set; }
        public ClientDetails ClientDetails { get; set; }
        public InsuredDetail InsuredDetail { get; set; }
        public SpouseDetails SpouseDetails { get; set; }
        public ChildDetailList ChildDetailList { get; set; }
        public UniversityDetails UniversityDetails { get; set; }
        public CourseDetails CourseDetails { get; set; }
        public HomeBurglaryAddress HomeBurglaryAddress { get; set; }
        public SponsorDetails SponsorDetails { get; set; }
        public DoctorDetails DoctorDetails { get; set; }
        public Policy Policy { get; set; }
        public RiskDetails RiskDetails { get; set; }
        public LstTravelCoverDetails LstTravelCoverDetails { get; set; }
        public string SourceSystemID { get; set; }
        public string AuthToken { get; set; }
        public string ErrorMessages { get; set; }
    }


    //[XmlRoot(ElementName = "TravelDetails")]
    //public class clsProposal
    //{
    //    [XmlElement(ElementName = "UserID")]
    //    public string UserID { get; set; }


    //    [XmlElement(ElementName = "ClientDetails")]
    //    public ClientDetails ClientDetails { get; set; }
    //    [XmlElement(ElementName = "InsuredDetail")]
    //    public InsuredDetail InsuredDetail { get; set; }
    //    [XmlElement(ElementName = "SpouseDetails")]
    //    public SpouseDetails SpouseDetails { get; set; }
    //    [XmlElement(ElementName = "ChildDetailList")]
    //    public ChildDetailList ChildDetailList { get; set; }
    //    [XmlElement(ElementName = "UniversityDetails")]
    //    public UniversityDetails UniversityDetails { get; set; }
    //    [XmlElement(ElementName = "CourseDetails")]
    //    public CourseDetails CourseDetails { get; set; }
    //    [XmlElement(ElementName = "HomeBurglaryAddress")]
    //    public HomeBurglaryAddress HomeBurglaryAddress { get; set; }
    //    [XmlElement(ElementName = "SponsorDetails")]
    //    public SponsorDetails SponsorDetails { get; set; }
    //    [XmlElement(ElementName = "DoctorDetails")]
    //    public DoctorDetails DoctorDetails { get; set; }
    //    [XmlElement(ElementName = "Policy")]
    //    public Policy Policy { get; set; }
    //    [XmlElement(ElementName = "RiskDetails")]
    //    public RiskDetails RiskDetails { get; set; }
    //    [XmlElement(ElementName = "LstTravelCoverDetails")]
    //    public LstTravelCoverDetails LstTravelCoverDetails { get; set; }
    //    [XmlElement(ElementName = "ErrorMessages")]
    //    public string ErrorMessages { get; set; }
    //    [XmlElement(ElementName = "SourceSystemID")]
    //    public string SourceSystemID { get; set; }
    //    [XmlElement(ElementName = "AuthToken")]
    //    public string AuthToken { get; set; }
    //    //public TravelDetails travelDetails { get; set; }
    //    //[XmlElement(ElementName = "ErrorMessages")]
    //    //public string ErrorMessages { get; set; }
    //    //[XmlElement(ElementName = "SourceSystemID")]
    //    //public string SourceSystemID { get; set; }
    //    //[XmlElement(ElementName = "AuthToken")]
    //    //public string AuthToken { get; set; }
    //    [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
    //    public string Xsd { get; set; }
    //    [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
    //    public string Xsi { get; set; }

    //}
    //[XmlRoot(ElementName = "CommunicationAddress")]
    //public class CommunicationAddress
    //{
    //    [XmlElement(ElementName = "Address1")]
    //    public string Address1 { get; set; }
    //    [XmlElement(ElementName = "Address2")]
    //    public string Address2 { get; set; }
    //    [XmlElement(ElementName = "Address3")]
    //    public string Address3 { get; set; }
    //    [XmlElement(ElementName = "CityID")]
    //    public string CityID { get; set; }
    //    [XmlElement(ElementName = "DistrictID")]
    //    public string DistrictID { get; set; }
    //    [XmlElement(ElementName = "StateID")]
    //    public string StateID { get; set; }
    //    [XmlElement(ElementName = "AreaID")]
    //    public string AreaID { get; set; }
    //    [XmlElement(ElementName = "NearestLandmark")]
    //    public string NearestLandmark { get; set; }
    //    [XmlElement(ElementName = "Country")]
    //    public string Country { get; set; }
    //    [XmlElement(ElementName = "Pincode")]
    //    public string Pincode { get; set; }
    //    [XmlElement(ElementName = "MobileNo")]
    //    public string MobileNo { get; set; }
    //    [XmlElement(ElementName = "PhoneNo")]
    //    public string PhoneNo { get; set; }
    //    [XmlElement(ElementName = "Email")]
    //    public string Email { get; set; }
    //    [XmlElement(ElementName = "PanNo")]
    //    public string PanNo { get; set; }
    //    [XmlElement(ElementName = "Aadhaar")]
    //    public string Aadhaar { get; set; }
    //}

    //[XmlRoot(ElementName = "Address")]
    //public class Address
    //{
    //    [XmlElement(ElementName = "Address1")]
    //    public string Address1 { get; set; }
    //    [XmlElement(ElementName = "Address2")]
    //    public string Address2 { get; set; }
    //    [XmlElement(ElementName = "Address3")]
    //    public string Address3 { get; set; }
    //    [XmlElement(ElementName = "CityID")]
    //    public string CityID { get; set; }
    //    [XmlElement(ElementName = "DistrictID")]
    //    public string DistrictID { get; set; }
    //    [XmlElement(ElementName = "StateID")]
    //    public string StateID { get; set; }
    //    [XmlElement(ElementName = "AreaID")]
    //    public string AreaID { get; set; }
    //    [XmlElement(ElementName = "NearestLandmark")]
    //    public string NearestLandmark { get; set; }
    //    [XmlElement(ElementName = "Country")]
    //    public string Country { get; set; }
    //    [XmlElement(ElementName = "Pincode")]
    //    public string Pincode { get; set; }
    //    [XmlElement(ElementName = "MobileNo")]
    //    public string MobileNo { get; set; }
    //    [XmlElement(ElementName = "PhoneNo")]
    //    public string PhoneNo { get; set; }
    //    [XmlElement(ElementName = "Email")]
    //    public string Email { get; set; }
    //    [XmlElement(ElementName = "Fax")]
    //    public string Fax { get; set; }
    //}

    //[XmlRoot(ElementName = "PermanentAddress")]
    //public class PermanentAddress
    //{
    //    [XmlElement(ElementName = "IsPermanentSameasCommAddr")]
    //    public string IsPermanentSameasCommAddr { get; set; }
    //    [XmlElement(ElementName = "Address")]
    //    public Address Address { get; set; }
    //}

    //[XmlRoot(ElementName = "ClientAddress")]
    //public class ClientAddress
    //{
    //    [XmlElement(ElementName = "CommunicationAddress")]
    //    public CommunicationAddress CommunicationAddress { get; set; }
    //    [XmlElement(ElementName = "PermanentAddress")]
    //    public PermanentAddress PermanentAddress { get; set; }
    //}

    //[XmlRoot(ElementName = "ClientDetails")]
    //public class ClientDetails
    //{
    //    [XmlElement(ElementName = "ClientType")]
    //    public string ClientType { get; set; }
    //    [XmlElement(ElementName = "Salutation")]
    //    public string Salutation { get; set; }
    //    [XmlElement(ElementName = "ForeName")]
    //    public string ForeName { get; set; }
    //    [XmlElement(ElementName = "LastName")]
    //    public string LastName { get; set; }
    //    [XmlElement(ElementName = "MidName")]
    //    public string MidName { get; set; }
    //    [XmlElement(ElementName = "DOB")]
    //    public string DOB { get; set; }
    //    [XmlElement(ElementName = "Gender")]
    //    public string Gender { get; set; }
    //    [XmlElement(ElementName = "OccupationID")]
    //    public string OccupationID { get; set; }
    //    [XmlElement(ElementName = "MaritalStatus")]
    //    public string MaritalStatus { get; set; }
    //    [XmlElement(ElementName = "Nationality")]
    //    public string Nationality { get; set; }
    //    [XmlElement(ElementName = "PhoneNo")]
    //    public string PhoneNo { get; set; }
    //    [XmlElement(ElementName = "MobileNo")]
    //    public string MobileNo { get; set; }
    //    [XmlElement(ElementName = "Email")]
    //    public string Email { get; set; }
    //    [XmlElement(ElementName = "ClientAddress")]
    //    public ClientAddress ClientAddress { get; set; }
    //}

    //[XmlRoot(ElementName = "InsuredDetail")]
    //public class InsuredDetail
    //{
    //    [XmlElement(ElementName = "RelationshipWithProposerID")]
    //    public string RelationshipWithProposerID { get; set; }
    //    [XmlElement(ElementName = "PassportNumber")]
    //    public string PassportNumber { get; set; }
    //    [XmlElement(ElementName = "NameofNominee")]
    //    public string NameofNominee { get; set; }
    //    [XmlElement(ElementName = "RelationshipWithNomineeID")]
    //    public string RelationshipWithNomineeID { get; set; }
    //    [XmlElement(ElementName = "VisitingCountries")]
    //    public string VisitingCountries { get; set; }
    //    [XmlElement(ElementName = "IsUnderMedication")]
    //    public string IsUnderMedication { get; set; }
    //    [XmlElement(ElementName = "PreExistingIllness")]
    //    public string PreExistingIllness { get; set; }
    //    [XmlElement(ElementName = "SufferingSince")]
    //    public string SufferingSince { get; set; }
    //    [XmlElement(ElementName = "Salutation")]
    //    public string Salutation { get; set; }
    //    [XmlElement(ElementName = "ForeName")]
    //    public string ForeName { get; set; }
    //    [XmlElement(ElementName = "LastName")]
    //    public string LastName { get; set; }
    //    [XmlElement(ElementName = "MidName")]
    //    public string MidName { get; set; }
    //    [XmlElement(ElementName = "Gender")]
    //    public string Gender { get; set; }
    //    [XmlElement(ElementName = "DateofBirth")]
    //    public string DateofBirth { get; set; }
    //    [XmlElement(ElementName = "OccupationID")]
    //    public string OccupationID { get; set; }
    //    [XmlElement(ElementName = "MobileNo")]
    //    public string MobileNo { get; set; }
    //    [XmlElement(ElementName = "PhoneNo")]
    //    public string PhoneNo { get; set; }
    //    [XmlElement(ElementName = "Email")]
    //    public string Email { get; set; }
    //}

    //[XmlRoot(ElementName = "SpouseDetails")]
    //public class SpouseDetails
    //{
    //    [XmlElement(ElementName = "FirstName")]
    //    public string FirstName { get; set; }
    //    [XmlElement(ElementName = "DOB")]
    //    public string DOB { get; set; }
    //    [XmlElement(ElementName = "PassportNo")]
    //    public string PassportNo { get; set; }
    //    [XmlElement(ElementName = "RelationshipwithInsuredID")]
    //    public string RelationshipwithInsuredID { get; set; }
    //    [XmlElement(ElementName = "NomineeName")]
    //    public string NomineeName { get; set; }
    //    [XmlElement(ElementName = "NomineeRelationshipID")]
    //    public string NomineeRelationshipID { get; set; }
    //}

    //[XmlRoot(ElementName = "ChildDetails")]
    //public class ChildDetails
    //{
    //    [XmlElement(ElementName = "ChildName")]
    //    public string ChildName { get; set; }
    //    [XmlElement(ElementName = "ChildRelationID")]
    //    public string ChildRelationID { get; set; }
    //    [XmlElement(ElementName = "DOB")]
    //    public string DOB { get; set; }
    //    [XmlElement(ElementName = "PassportNo")]
    //    public string PassportNo { get; set; }
    //    [XmlElement(ElementName = "NomineeName")]
    //    public string NomineeName { get; set; }
    //    [XmlElement(ElementName = "NomineeRelationshipID")]
    //    public string NomineeRelationshipID { get; set; }
    //    [XmlElement(ElementName = "IsUnderMedication")]
    //    public string IsUnderMedication { get; set; }
    //    [XmlElement(ElementName = "PreExistingMC")]
    //    public string PreExistingMC { get; set; }
    //    [XmlElement(ElementName = "SufferingSince")]
    //    public string SufferingSince { get; set; }
    //}

    //[XmlRoot(ElementName = "ChildDetailList")]
    //public class ChildDetailList
    //{
    //    [XmlElement(ElementName = "ChildDetails")]
    //    public ChildDetails ChildDetails { get; set; }
    //}

    //[XmlRoot(ElementName = "UniversityDetails")]
    //public class UniversityDetails
    //{
    //    [XmlElement(ElementName = "UniversityName")]
    //    public string UniversityName { get; set; }
    //    [XmlElement(ElementName = "UniversityCountryId")]
    //    public string UniversityCountryId { get; set; }
    //    [XmlElement(ElementName = "UniversityStateName")]
    //    public string UniversityStateName { get; set; }
    //    [XmlElement(ElementName = "CityName")]
    //    public string CityName { get; set; }
    //    [XmlElement(ElementName = "PhoneNumber")]
    //    public string PhoneNumber { get; set; }
    //    [XmlElement(ElementName = "MobileNumber")]
    //    public string MobileNumber { get; set; }
    //    [XmlElement(ElementName = "EmailId")]
    //    public string EmailId { get; set; }
    //    [XmlElement(ElementName = "Fax")]
    //    public string Fax { get; set; }
    //}

    //[XmlRoot(ElementName = "CourseDetails")]
    //public class CourseDetails
    //{
    //    [XmlElement(ElementName = "CourseDuration")]
    //    public string CourseDuration { get; set; }
    //    [XmlElement(ElementName = "TutionFeePerSem")]
    //    public string TutionFeePerSem { get; set; }
    //    [XmlElement(ElementName = "NoOfSems")]
    //    public string NoOfSems { get; set; }
    //}

    //[XmlRoot(ElementName = "HomeBurglaryAddress")]
    //public class HomeBurglaryAddress
    //{
    //    [XmlElement(ElementName = "IsSameAsCommAddr")]
    //    public string IsSameAsCommAddr { get; set; }
    //    [XmlElement(ElementName = "Address")]
    //    public Address Address { get; set; }
    //}

    //[XmlRoot(ElementName = "SponserAddress")]
    //public class SponserAddress
    //{
    //    [XmlElement(ElementName = "SponsorName")]
    //    public string SponsorName { get; set; }
    //    [XmlElement(ElementName = "Address1")]
    //    public string Address1 { get; set; }
    //    [XmlElement(ElementName = "Pincode")]
    //    public string Pincode { get; set; }
    //    [XmlElement(ElementName = "MobileNo")]
    //    public string MobileNo { get; set; }
    //    [XmlElement(ElementName = "PhoneNo")]
    //    public string PhoneNo { get; set; }
    //    [XmlElement(ElementName = "Email")]
    //    public string Email { get; set; }
    //    [XmlElement(ElementName = "Fax")]
    //    public string Fax { get; set; }
    //    [XmlElement(ElementName = "CityName")]
    //    public string CityName { get; set; }
    //    [XmlElement(ElementName = "StateName")]
    //    public string StateName { get; set; }
    //    [XmlElement(ElementName = "CountryID")]
    //    public string CountryID { get; set; }
    //}

    //[XmlRoot(ElementName = "SponsorDetails")]
    //public class SponsorDetails
    //{
    //    [XmlElement(ElementName = "SponsorName")]
    //    public string SponsorName { get; set; }
    //    [XmlElement(ElementName = "IsSponserAddressSameasCommAddress")]
    //    public string IsSponserAddressSameasCommAddress { get; set; }
    //    [XmlElement(ElementName = "SponserAddress")]
    //    public SponserAddress SponserAddress { get; set; }
    //}

    //[XmlRoot(ElementName = "DoctorDetails")]
    //public class DoctorDetails
    //{
    //    [XmlElement(ElementName = "IsDoctorDetails")]
    //    public string IsDoctorDetails { get; set; }
    //    [XmlElement(ElementName = "Name")]
    //    public string Name { get; set; }
    //    [XmlElement(ElementName = "Address")]
    //    public Address Address { get; set; }
    //}

    //[XmlRoot(ElementName = "Policy")]
    //public class Policy
    //{
    //    [XmlElement(ElementName = "BusinessType")]
    //    public string BusinessType { get; set; }
    //    [XmlElement(ElementName = "AgentCode")]
    //    public string AgentCode { get; set; }
    //    [XmlElement(ElementName = "AgentName")]
    //    public string AgentName { get; set; }
    //    [XmlElement(ElementName = "Branch_Name")]
    //    public string Branch_Name { get; set; }
    //    [XmlElement(ElementName = "Branch_Code")]
    //    public string Branch_Code { get; set; }
    //    [XmlElement(ElementName = "ProductCode")]
    //    public string ProductCode { get; set; }
    //    [XmlElement(ElementName = "OtherSystemName")]
    //    public string OtherSystemName { get; set; }
    //}

    //[XmlRoot(ElementName = "RiskDetails")]
    //public class RiskDetails
    //{
    //    [XmlElement(ElementName = "IsIndianCitizen")]
    //    public string IsIndianCitizen { get; set; }
    //    [XmlElement(ElementName = "IsOverSeasCitizen")]
    //    public string IsOverSeasCitizen { get; set; }
    //    [XmlElement(ElementName = "IsOCI")]
    //    public string IsOCI { get; set; }
    //    [XmlElement(ElementName = "IsNONOCI")]
    //    public string IsNONOCI { get; set; }
    //    [XmlElement(ElementName = "IsResidingInIndia")]
    //    public string IsResidingInIndia { get; set; }
    //    [XmlElement(ElementName = "PermanentResidenceCountry")]
    //    public string PermanentResidenceCountry { get; set; }
    //    [XmlElement(ElementName = "OCINumber")]
    //    public string OCINumber { get; set; }
    //    [XmlElement(ElementName = "PassportIssuingCountry")]
    //    public string PassportIssuingCountry { get; set; }
    //    [XmlElement(ElementName = "IsInsuredOnImmigrantVisa")]
    //    public string IsInsuredOnImmigrantVisa { get; set; }
    //    [XmlElement(ElementName = "IsTravelInvolvesSportingActivities")]
    //    public string IsTravelInvolvesSportingActivities { get; set; }
    //    [XmlElement(ElementName = "SportsActivitiesID")]
    //    public string SportsActivitiesID { get; set; }
    //    [XmlElement(ElementName = "IsSufferingFromPEMC")]
    //    public string IsSufferingFromPEMC { get; set; }
    //    [XmlElement(ElementName = "PreExistDiseaseID")]
    //    public string PreExistDiseaseID { get; set; }
    //    [XmlElement(ElementName = "IsVisitingUSACanada")]
    //    public string IsVisitingUSACanada { get; set; }
    //    [XmlElement(ElementName = "VisitingCountriesID")]
    //    public string VisitingCountriesID { get; set; }
    //    [XmlElement(ElementName = "JourneyStartDate")]
    //    public string JourneyStartDate { get; set; }
    //    [XmlElement(ElementName = "JourneyEndDate")]
    //    public string JourneyEndDate { get; set; }
    //    [XmlElement(ElementName = "TravelDays")]
    //    public string TravelDays { get; set; }
    //    [XmlElement(ElementName = "DateOfBirth")]
    //    public string DateOfBirth { get; set; }
    //    [XmlElement(ElementName = "CoverageTypeID")]
    //    public string CoverageTypeID { get; set; }
    //    [XmlElement(ElementName = "IsAddOnCover")]
    //    public string IsAddOnCover { get; set; }
    //    [XmlElement(ElementName = "MaxDaysPerTrip")]
    //    public string MaxDaysPerTrip { get; set; }
    //    [XmlElement(ElementName = "NoOfYears")]
    //    public string NoOfYears { get; set; }
    //    [XmlElement(ElementName = "SeniorCitizenPlanID")]
    //    public string SeniorCitizenPlanID { get; set; }
    //    [XmlElement(ElementName = "PlanName")]
    //    public string PlanName { get; set; }
    //    [XmlElement(ElementName = "AddOnBnifitsOpted")]
    //    public string AddOnBnifitsOpted { get; set; }
    //}

    //[XmlRoot(ElementName = "LstTravelCovers")]
    //public class LstTravelCovers
    //{
    //    [XmlElement(ElementName = "CoverageName")]
    //    public string CoverageName { get; set; }
    //    [XmlElement(ElementName = "CoverageDisplayName")]
    //    public string CoverageDisplayName { get; set; }
    //    [XmlElement(ElementName = "StandardLimit")]
    //    public string StandardLimit { get; set; }
    //    [XmlElement(ElementName = "SilverLimit")]
    //    public string SilverLimit { get; set; }
    //    [XmlElement(ElementName = "GoldLimit")]
    //    public string GoldLimit { get; set; }
    //    [XmlElement(ElementName = "PlatinumLimit")]
    //    public string PlatinumLimit { get; set; }
    //    [XmlElement(ElementName = "BasicLimit")]
    //    public string BasicLimit { get; set; }
    //    [XmlElement(ElementName = "EliteLimit")]
    //    public string EliteLimit { get; set; }
    //    [XmlElement(ElementName = "PlusLimit")]
    //    public string PlusLimit { get; set; }
    //    [XmlElement(ElementName = "StandardDeductible")]
    //    public string StandardDeductible { get; set; }
    //    [XmlElement(ElementName = "SilverDeductible")]
    //    public string SilverDeductible { get; set; }
    //    [XmlElement(ElementName = "GoldDeductible")]
    //    public string GoldDeductible { get; set; }
    //    [XmlElement(ElementName = "PlatinumDeductible")]
    //    public string PlatinumDeductible { get; set; }
    //    [XmlElement(ElementName = "BasicDeductible")]
    //    public string BasicDeductible { get; set; }
    //    [XmlElement(ElementName = "IsStandardPlan")]
    //    public string IsStandardPlan { get; set; }
    //    [XmlElement(ElementName = "IsSilverPlan")]
    //    public string IsSilverPlan { get; set; }
    //    [XmlElement(ElementName = "IsGoldPlan")]
    //    public string IsGoldPlan { get; set; }
    //    [XmlElement(ElementName = "IsPlatinumPlan")]
    //    public string IsPlatinumPlan { get; set; }
    //    [XmlElement(ElementName = "IsBasicPlan")]
    //    public string IsBasicPlan { get; set; }
    //    [XmlElement(ElementName = "IsElitePlan")]
    //    public string IsElitePlan { get; set; }
    //    [XmlElement(ElementName = "IsPlusPlan")]
    //    public string IsPlusPlan { get; set; }
    //}

    //[XmlRoot(ElementName = "LstTravelCoverDetails")]
    //public class LstTravelCoverDetails
    //{
    //    [XmlElement(ElementName = "LstTravelCovers")]
    //    public LstTravelCovers LstTravelCovers { get; set; }
    //}




}