using InsuranceBALApi.Interface;
//using InsuranceBALApi.Common;
using Newtonsoft.Json;
using PayuTest.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static InsuranceBALApi.Models.GenericReturnModel;
using static InsuranceBALApi.Models.TravelBALAPIModel;
using static InsuranceBALApi.Models.TravelBALAPIModel.TravelApiRequestResponseModel;
using static InsuranceBALApi.Models.TravelBALAPIModel.TravelApiResponseModel;

using System.Collections;
using System.Xml.Serialization;
using System.Xml;

using System.IO;
using System.Net;

namespace InsuranceBALApi.Functions
{
    public class TravelServiceFunctions : ITravelServiceFunctions
    {
        DataReturnModel<dynamic> ITravelServiceFunctions.HitDbByEnquiryID(SearchResponse request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();

            try
            {
                // store and check data
                string cstr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                DataTable dt = GetTravelSearchResultByEnquiryID(cstr, request.enquiryID);

                List<EnquiryIDData> list = new List<EnquiryIDData>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    EnquiryIDData m = new EnquiryIDData();
                    m.InsuranceFor = dt.Rows[i]["InsuranceFor"].ToString();
                    m.DestinationCity = dt.Rows[i]["DestinationCity"].ToString();
                    m.TravelStartDt = dt.Rows[i]["TravelStartDt"].ToString();
                    m.TravelEndDt = dt.Rows[i]["TravelEndDt"].ToString();
                    m.travelMultipleTimeFlag = dt.Rows[i]["travelMultipleTimeFlag"].ToString();
                    m.travelMultipleTimeDuration = dt.Rows[i]["travelMultipleTimeDuration"].ToString();
                    m.medicalConditionFlag = dt.Rows[i]["medicalConditionFlag"].ToString();
                    m.Enquid = dt.Rows[i]["Enquid"].ToString();
                    m.CanBeParent = dt.Rows[i]["CanBeParent"].ToString();
                    m.sumInsured = dt.Rows[i]["sumInsured"].ToString();
                    m.comm_giv_type = dt.Rows[i]["comm_giv_type"].ToString();
                    m.ProductName = dt.Rows[i]["ProductName"].ToString();
                    m.BusinessType = dt.Rows[i]["BusinessType"].ToString();
                    m.PolicyType = dt.Rows[i]["PolicyType"].ToString();
                    m.TravelDuration = dt.Rows[i]["TravelDuration"].ToString();
                    m.TravelGeography = dt.Rows[i]["TravelGeography"].ToString();
                    m.TypeOfBusiness = dt.Rows[i]["TypeOfBusiness"].ToString();
                    m.Fromdate = dt.Rows[i]["Fromdate"].ToString();
                    m.Fromhour = dt.Rows[i]["Fromhour"].ToString();
                    m.Todate = dt.Rows[i]["Todate"].ToString();
                    m.Tohour = dt.Rows[i]["Tohour"].ToString();
                    m.NetPremium = dt.Rows[i]["NetPremium"].ToString();
                    m.ServiceTax = dt.Rows[i]["ServiceTax"].ToString();
                    m.StampDuty2 = dt.Rows[i]["StampDuty2"].ToString();
                    m.TotalPremium = dt.Rows[i]["TotalPremium"].ToString();
                    m.PlanType = dt.Rows[i]["PlanType"].ToString();
                    m.USERID = dt.Rows[i]["USERID"].ToString();
                    m.PartyComm = dt.Rows[i]["PartyComm"].ToString();
                    m.PartyCommPer = dt.Rows[i]["PartyCommPer"].ToString();
                    m.PartyCommType = dt.Rows[i]["PartyCommType"].ToString();
                    m.TDSPER = dt.Rows[i]["TDSPER"].ToString();
                    m.tds = dt.Rows[i]["tds"].ToString();
                    m.cgstONCOMM = dt.Rows[i]["cgstONCOMM"].ToString();
                    m.sgstONCOMM = dt.Rows[i]["sgstONCOMM"].ToString();
                    m.igstONCOMM = dt.Rows[i]["igstONCOMM"].ToString();
                    m.visaType = dt.Rows[i]["visaType"].ToString();

                    m.IsPreExistingDiseaseFlag = dt.Rows[i]["IsPreExistingDiseaseFlag"].ToString();
                    m.PreExistingDiseaseAmount = dt.Rows[i]["PreExistingDiseaseAmount"].ToString();
                    m.IsAdventureSportsFlag = dt.Rows[i]["IsAdventureSportsFlag"].ToString();
                    m.AdventureSportsAmount = dt.Rows[i]["AdventureSportsAmount"].ToString();
                    m.IsPetCareFlag = dt.Rows[i]["IsPetCareFlag"].ToString();
                    m.PetCareAmount = dt.Rows[i]["PetCareAmount"].ToString();
                    m.IsCoverageOnCruise = dt.Rows[i]["IsCoverageOnCruise"].ToString();
                    m.CoverageOnCruiseAmount = dt.Rows[i]["CoverageOnCruiseAmount"].ToString();
                    m.IsHomeBurgalaryFlag = dt.Rows[i]["IsHomeBurgalaryFlag"].ToString();
                    m.HomeBurgalaryAmount = dt.Rows[i]["HomeBurgalaryAmount"].ToString();
                    m.IsCardFraudFlag = dt.Rows[i]["IsCardFraudFlag"].ToString();
                    m.CardFlagAmount = dt.Rows[i]["CardFlagAmount"].ToString();
                    m.IsNonIndianPassportHolderFlag = dt.Rows[i]["IsNonIndianPassportHolderFlag"].ToString();
                    m.IsTouristVisaFlag = dt.Rows[i]["IsTouristVisaFlag"].ToString();
                    m.IsShortTermWorkVisaFlag = dt.Rows[i]["IsShortTermWorkVisaFlag"].ToString();
                    m.IsPermanentResidentCardFlag = dt.Rows[i]["IsPermanentResidentCardFlag"].ToString();
                    m.IsLongTermWorkVisaFlag = dt.Rows[i]["IsLongTermWorkVisaFlag"].ToString();
                    m.IsDependentVisaFlag = dt.Rows[i]["IsDependentVisaFlag"].ToString();
                    m.IsDiplomaticVisaFlag = dt.Rows[i]["IsDiplomaticVisaFlag"].ToString();
                    m.IsStudentVisaFlag = dt.Rows[i]["IsStudentVisaFlag"].ToString();
                    m.IsGoinfHomeFlag = dt.Rows[i]["IsGoinfHomeFlag"].ToString();
                    m.IsHolidayFlag = dt.Rows[i]["IsHolidayFlag"].ToString();
                    m.IsStudiesFlag = dt.Rows[i]["IsStudiesFlag"].ToString();
                    m.IsRelocationFlag = dt.Rows[i]["IsRelocationFlag"].ToString();
                    m.IsMedicalTreatmentFlag = dt.Rows[i]["IsMedicalTreatmentFlag"].ToString();
                    m.IsBusinessFlag = dt.Rows[i]["IsBusinessFlag"].ToString();


                    list.Add(m);
                }
                
                dr.info.res = JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {
                ErrorLogging(ex);
            }
            return dr;
        }

        DataReturnModel<dynamic> ITravelServiceFunctions.HitTravelSearchApi(SearchRequest request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();

            try
            {
                SearchResponse rres = new SearchResponse();

                if(request.InsuranceFor.ToUpper()=="FAMILY"|| request.InsuranceFor.ToUpper() =="INDIVIDUAL")
                {
                    rres.enquiryID = ConfigurationManager.AppSettings["fa"];

                }
                else if(request.InsuranceFor.ToUpper()=="GROUP")
                {
                    rres.enquiryID = ConfigurationManager.AppSettings["grp"];
                }
                else if (request.InsuranceFor.ToUpper()=="STUDENT")
                {
                    rres.enquiryID = ConfigurationManager.AppSettings["stu"];
                }
                else
                {
                    rres.enquiryID = "";
                }

                // rres.enquiryID = Guid.NewGuid().ToString();
                dr.info.res = JsonConvert.SerializeObject(rres);

                // CALL API FROM HERE AND PASS ENQUIRY ID AND ALL PARAMETER RECEVED FROM UI

            }
            catch (Exception ex)
            {
                ErrorLogging(ex);
            }
            return dr;
        }
        

        DataReturnModel<dynamic> ITravelServiceFunctions.HitSelectedPlanApi(SelectedPlan request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();

            //try
            //{
            //    string cstr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //    DataSet ds = InsertSelectedPlanData(cstr, request);

            //    dr.info.res = JsonConvert.SerializeObject(ds);

            //    // DataSet dds = JsonConvert.DeserializeObject<DataSet>(dr.info.res);
            //    // CALL API FROM HERE AND PASS ENQUIRY ID AND ALL PARAMETER RECEVED FROM UI
            //}
            //catch (Exception ex)
            //{
            //    ErrorLogging(ex);
            //}
            return dr;
        }

        DataReturnModel<dynamic> ITravelServiceFunctions.HitProposalFormApi(ProposalForm request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
            try
            {
                string cstr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                DataSet ds = InsertProposalFormData(cstr, request);
                dr.info.res = JsonConvert.SerializeObject(ds);
                // DataSet dds = JsonConvert.DeserializeObject<DataSet>(dr.info.res);
                // CALL API FROM HERE AND PASS ENQUIRY ID AND ALL PARAMETER RECEVED FROM UI
            }
            catch (Exception ex)
            {
                ErrorLogging(ex);
            }
            return dr;
        }


        DataReturnModel<dynamic> ITravelServiceFunctions.HitPaymentGatewayApi(PgResponse request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
            try
            {
                string cstr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                // DataSet ds = insertProposalFormData(cstr, request);
                // dr.info.res = JsonConvert.SerializeObject(ds);
                // DataSet dds = JsonConvert.DeserializeObject<DataSet>(dr.info.res);
                // CALL API FROM HERE AND PASS ENQUIRY ID AND ALL PARAMETER RECEVED FROM UI

                DataTable dt = new DataTable();
                dt.Columns.Add("policyNo", typeof(string));
                dt.Columns.Add("enquiryNo", typeof(string));

                DataRow dr1 = dt.NewRow();
                dr1["policyNo"] = "TEST123456";
                dr1["enquiryNo"] = "9a121f44-af60-45ab-904a-d8199ec0b16b";
                dt.Rows.Add(dr1);

                dr.info.res = JsonConvert.SerializeObject(dt);

            }
            catch (Exception ex)
            {
                ErrorLogging(ex);
            }
            return dr;
        }

        DataReturnModel<dynamic> ITravelServiceFunctions.HitDownloadPolicyApi(PolicyDownload request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
            try
            {
                string cstr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                DataTable dt = new DataTable();
                dt.Columns.Add("policyNo", typeof(string));
                dt.Columns.Add("enquiryNo", typeof(string));
                dt.Columns.Add("link", typeof(string));

                DataRow dr1 = dt.NewRow();
                dr1["policyNo"] = "TEST123456";
                dr1["enquiryNo"] = "9a121f44-af60-45ab-904a-d8199ec0b16b";
                dr1["link"] = "http://103.234.64.253:91/DL8SBT9928.pdf";
                dt.Rows.Add(dr1);

                dr.info.res = JsonConvert.SerializeObject(dt);

            }
            catch (Exception ex)
            {
                ErrorLogging(ex);
            }
          

            return dr;
        }

        

        DataReturnModel<dynamic> ITravelServiceFunctions.HitSmsApi(SmsRequest request)
        {

            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();

            try
            {
                Send_sms(request.phoneNo, request.Message);

                dr.info.res = "{status:SUCCESS}";

                // CALL API FROM HERE AND PASS ENQUIRY ID AND ALL PARAMETER RECEVED FROM UI

            }
            catch (Exception ex)
            {
                ErrorLogging(ex);
            }
            return dr;
        }

        DataReturnModel<dynamic> ITravelServiceFunctions.HitEmailApi(EmailRequest request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
            try
            {
                Sendmail(request.body, request.to, request.cc, request.subject, request.bcc);
                dr.info.res = "{status:SUCCESS}";
                // CALL API FROM HERE AND PASS ENQUIRY ID AND ALL PARAMETER RECEVED FROM UI

            }
            catch (Exception ex)
            {
                ErrorLogging(ex);
            }
            return dr;
        }

        DataReturnModel<dynamic> ITravelServiceFunctions.HitLoginApi(LoginRequest request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();
            try
            {
              // code to check username password 


                dr.info.res = "{\"status\":\"SUCCESS\",\"tokenID\":\"csdsdsds\"}";              

            }
            catch (Exception ex)
            {
                ErrorLogging(ex);
            }
            return dr;
        }


        public void ErrorLogging(Exception ex)
        {
            // errorDir
            string strPath = ConfigurationManager.AppSettings["errorDir"] + "test.txt"; ;
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
                sw.WriteLine("=============Error Logging ===========");
                sw.WriteLine("===========Start============= " + DateTime.Now);
                sw.WriteLine("Error Message: " + ex.Message);
                sw.WriteLine("Stack Trace: " + ex.StackTrace);
                sw.WriteLine("===========End============= " + DateTime.Now);

            }
        }

        

        public string Send_sms(string to, string message)
        {
            DataTable dt = GetCredential_SMS_EMAIL(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, "SMS");

            string userid = dt.Rows[0]["Userid"].ToString();
            string pass = dt.Rows[0]["PWD"].ToString();
            string send_id = dt.Rows[0]["GSMSenderID"].ToString();

            Hashtable prms = new Hashtable();
            prms.Add("username", userid);
            prms.Add("password", pass);
            prms.Add("sender", send_id);
            prms.Add("to", to);
            prms.Add("message", message);

            String postdata = string.Empty;
            foreach (DictionaryEntry prm in prms)
            {
                postdata += prm.Key + "=" + prm.Value + "&";
            }

            postdata = postdata.TrimEnd('&');

            string successStr = "";
            string paymentUrl = dt.Rows[0]["Url"].ToString();

            System.Net.HttpWebRequest request = (HttpWebRequest)WebRequest.Create(paymentUrl + "?" + postdata + "&priority=1&dnd=1&unicode=0");
            request.Method = "POST";
            request.ContentLength = postdata.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            //Post Data
            StreamWriter sw = null;
            sw = new StreamWriter(request.GetRequestStream());
            sw.Write(postdata);
            sw.Close();

            // Get response
            String post_response;
            HttpWebResponse objResponse = (HttpWebResponse)request.GetResponse();
            using (StreamReader responseStream = new StreamReader(objResponse.GetResponseStream()))
            {
                post_response = responseStream.ReadToEnd();
                responseStream.Close();
            }

            successStr = post_response;
            return successStr;

        }

        protected void Sendmail(string html, string to, string cc, string sub, string bcc)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string htmlString = html;
                string sto = to;
                string sSMtpServer, SSMtpServerPort, sSender, sPassword;
                System.Net.Mail.MailMessage smail = new System.Net.Mail.MailMessage();

                DataTable dt = GetCredential_SMS_EMAIL(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, "EMAIL");

                sSMtpServer = dt.Rows[0]["SenderSMTP"].ToString();
                SSMtpServerPort = dt.Rows[0]["SenderSMTPPort"].ToString();
                sSender = dt.Rows[0]["SenderMail"].ToString();
                sPassword = dt.Rows[0]["SenderPassword"].ToString();

                System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient(sSMtpServer);
                smail.From = new System.Net.Mail.MailAddress(sSender);
                smail.To.Add(sto);
                if (cc.Length > 1)
                {
                    smail.CC.Add(cc);
                }
                if (bcc.Length > 1)
                {
                    smail.Bcc.Add(bcc);
                }

                smail.Subject = sub;
                smail.IsBodyHtml = true;
                smail.Body = htmlString;
                SmtpServer.Port = int.Parse(SSMtpServerPort);
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                SmtpServer.Credentials = new System.Net.NetworkCredential(sSender, sPassword);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(smail);
            }
            catch (Exception e11)
            {
                return;
            }
        }

        private DataTable GetTravelSearchResultByEnquiryID(string cstr, string enqID)
        {
            //Enter Values in Recharge Table
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@enquiryid", enqID);

                return (new CommonDB().ExecuteQuery(cstr, "GetTravelSearchByEnquiryID", param));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private DataTable GetCredential_SMS_EMAIL(string cstr, string crdReq)
        {
            //Enter Values in Recharge Table
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                if (crdReq == "SMS")
                {
                    param[0] = new SqlParameter("@crdReq", "SMS");
                    return (new CommonDB().ExecuteQuery(cstr, "sp_getCredential", param));
                }
                else if (crdReq == "EMAIL")
                {
                    param[0] = new SqlParameter("@crdReq", "EMAIL");
                    return (new CommonDB().ExecuteQuery(cstr, "sp_getCredential", param));
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //private DataSet InsertSelectedPlanData(string cstr, SelectedPlan sReq)
        //{
        //    //Enter Values in Recharge Table
        //    try
        //    {
        //        SqlParameter[] param = new SqlParameter[3];
        //        param[0] = new SqlParameter("@enquiryid", sReq.EnquiryID);
        //        param[1] = new SqlParameter("@CompanyName", sReq.EnquiryID);
        //        param[2] = new SqlParameter("@PlanName", sReq.EnquiryID);

        //        return (new CommonDB().ExecuteMultipleQuery(cstr, "InsertSelectedTravelPlanDetail", param));
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        private DataSet InsertProposalFormData(string cstr, ProposalForm sReq)
        {
            //Enter Values in Recharge Table
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@enquiryid", sReq.enquiryID);

                return (new CommonDB().ExecuteMultipleQuery(cstr, "InsertTravelProposalFormDetail", param));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        DataReturnModel<dynamic> ITravelServiceFunctions.SaveCacheDataToResultByEnquiryID(EnquiryIDData request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();

            try
            {
                // store and check data
                string cstr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                DataTable dt = new DataTable(); // GetTravelSearchResultInCache(cstr, request.Enquid);
                bool blnDataFound = false; //Not Found Data in cache
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //Found Data in cache
                        blnDataFound = true;
                    }
                }
                if (blnDataFound) //Found Data in cache
                {
                    DataRow drNew = dt.NewRow();
                    drNew["InsuranceFor"] = request.InsuranceFor;
                    drNew["DestinationCity"] = request.DestinationCity;
                    drNew["TravelStartDt"] = request.TravelStartDt;
                    drNew["TravelEndDt"] = request.TravelEndDt;
                    drNew["travelMultipleTimeFlag"] = request.travelMultipleTimeFlag;
                    drNew["travelMultipleTimeDuration"] = request.travelMultipleTimeDuration;
                    drNew["medicalConditionFlag"] = request.medicalConditionFlag;
                    drNew["Enquid"] = request.Enquid;
                    drNew["CanBeParent"] = request.CanBeParent;
                    drNew["sumInsured"] = request.sumInsured;
                    drNew["comm_giv_type"] = request.comm_giv_type;
                    drNew["ProductName"] = request.ProductName;
                    drNew["BusinessType"] = request.BusinessType;
                    drNew["PolicyType"] = request.PolicyType;
                    drNew["TravelDuration"] = request.TravelDuration;
                    drNew["TravelGeography"] = request.TravelGeography;
                    drNew["TypeOfBusiness"] = request.TypeOfBusiness;
                    drNew["Fromdate"] = request.Fromdate;
                    drNew["Fromhour"] = request.Fromhour;
                    drNew["Todate"] = request.Todate;
                    drNew["Tohour"] = request.Tohour;
                    drNew["NetPremium"] = request.NetPremium;
                    drNew["ServiceTax"] = request.ServiceTax;
                    drNew["StampDuty2"] = request.StampDuty2;
                    drNew["TotalPremium"] = request.TotalPremium;
                    drNew["PlanType"] = request.PlanType;
                    drNew["USERID"] = request.USERID;
                    drNew["PartyComm"] = request.PartyComm;
                    drNew["PartyCommPer"] = request.PartyCommPer;
                    drNew["PartyCommType"] = request.PartyCommType;
                    drNew["TDSPER"] = request.TDSPER;
                    drNew["tds"] = request.tds;
                    drNew["cgstONCOMM"] = request.cgstONCOMM;
                    drNew["sgstONCOMM"] = request.sgstONCOMM;
                    drNew["igstONCOMM"] = request.igstONCOMM;
                    drNew["visaType"] = request.visaType;

                    drNew["IsPreExistingDiseaseFlag"] = request.IsPreExistingDiseaseFlag;
                    drNew["PreExistingDiseaseAmount"] = request.PreExistingDiseaseAmount;
                    drNew["IsAdventureSportsFlag"] = request.IsAdventureSportsFlag;
                    drNew["AdventureSportsAmount"] = request.AdventureSportsAmount;
                    drNew["IsPetCareFlag"] = request.IsPetCareFlag;
                    drNew["PetCareAmount"] = request.PetCareAmount;
                    drNew["IsCoverageOnCruise"] = request.IsCoverageOnCruise;
                    drNew["CoverageOnCruiseAmount"] = request.CoverageOnCruiseAmount;
                    drNew["IsHomeBurgalaryFlag"] = request.IsHomeBurgalaryFlag;
                    drNew["HomeBurgalaryAmount"] = request.HomeBurgalaryAmount;
                    drNew["IsCardFraudFlag"] = request.IsCardFraudFlag;
                    drNew["CardFlagAmount"] = request.CardFlagAmount;
                    drNew["IsNonIndianPassportHolderFlag"] = request.IsNonIndianPassportHolderFlag;
                    drNew["IsTouristVisaFlag"] = request.IsTouristVisaFlag;
                    drNew["IsShortTermWorkVisaFlag"] = request.IsShortTermWorkVisaFlag;
                    drNew["IsPermanentResidentCardFlag"] = request.IsPermanentResidentCardFlag;
                    drNew["IsLongTermWorkVisaFlag"] = request.IsLongTermWorkVisaFlag;
                    drNew["IsDependentVisaFlag"] = request.IsDependentVisaFlag;
                    drNew["IsDiplomaticVisaFlag"] = request.IsDiplomaticVisaFlag;
                    drNew["IsStudentVisaFlag"] = request.IsStudentVisaFlag;
                    drNew["IsGoinfHomeFlag"] = request.IsGoinfHomeFlag;
                    drNew["IsHolidayFlag"] = request.IsHolidayFlag;
                    drNew["IsStudiesFlag"] = request.IsStudiesFlag;
                    drNew["IsRelocationFlag"] = request.IsRelocationFlag;
                    drNew["IsMedicalTreatmentFlag"] = request.IsMedicalTreatmentFlag;
                    drNew["IsBusinessFlag"] = request.IsBusinessFlag;

                }

                //dr.info.res = JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {
                ErrorLogging(ex);
            }
            return dr;
        }

        DataReturnModel<dynamic> ITravelServiceFunctions.SaveCacheData(EnquiryIDData request)
        {
            DataReturnModel<dynamic> dr = new DataReturnModel<dynamic>();

            try
            {
                // store and check data
                string cstr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(cstr);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("Select * from TravelResultCaching", sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dsCache = new DataSet("dsCache");                
                sqlDataAdapter.Fill(dsCache);
                if (dsCache.Tables.Count == 0)
                    return null;

                DataTable dtCache = dsCache.Tables[0]; //GetTravelSearchResultInCache(cstr, request.Enquid);
                //bool blnDataFound = false; //Not Found Data in cache
                //if (dt != null)
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //        //Found Data in cache
                //        blnDataFound = true;
                //    }
                //}
                //if (blnDataFound) //Found Data in cache
                //{
                    DataRow drNew = dtCache.NewRow();
                    drNew["InsuranceFor"] = request.InsuranceFor;
                    drNew["DestinationCity"] = request.DestinationCity;
                    drNew["TravelStartDt"] = request.TravelStartDt;
                    drNew["TravelEndDt"] = request.TravelEndDt;
                    drNew["travelMultipleTimeFlag"] = request.travelMultipleTimeFlag;
                    drNew["travelMultipleTimeDuration"] = request.travelMultipleTimeDuration;
                    drNew["medicalConditionFlag"] = request.medicalConditionFlag;
                    drNew["Enquid"] = request.Enquid;
                    drNew["CanBeParent"] = request.CanBeParent;
                    drNew["sumInsured"] = request.sumInsured;
                    drNew["comm_giv_type"] = request.comm_giv_type;
                    drNew["ProductName"] = request.ProductName;
                    drNew["BusinessType"] = request.BusinessType;
                    drNew["PolicyType"] = request.PolicyType;
                    drNew["TravelDuration"] = request.TravelDuration;
                    drNew["TravelGeography"] = request.TravelGeography;
                    drNew["TypeOfBusiness"] = request.TypeOfBusiness;
                    drNew["Fromdate"] = request.Fromdate;
                    drNew["Fromhour"] = request.Fromhour;
                    drNew["Todate"] = request.Todate;
                    drNew["Tohour"] = request.Tohour;
                    drNew["NetPremium"] = request.NetPremium;
                    drNew["ServiceTax"] = request.ServiceTax;
                    drNew["StampDuty2"] = request.StampDuty2;
                    drNew["TotalPremium"] = request.TotalPremium;
                    drNew["PlanType"] = request.PlanType;
                    drNew["USERID"] = request.USERID;
                    drNew["PartyComm"] = request.PartyComm;
                    drNew["PartyCommPer"] = request.PartyCommPer;
                    drNew["PartyCommType"] = request.PartyCommType;
                    drNew["TDSPER"] = request.TDSPER;
                    drNew["tds"] = request.tds;
                    drNew["cgstONCOMM"] = request.cgstONCOMM;
                    drNew["sgstONCOMM"] = request.sgstONCOMM;
                    drNew["igstONCOMM"] = request.igstONCOMM;
                    drNew["visaType"] = request.visaType;

                    drNew["IsPreExistingDiseaseFlag"] = request.IsPreExistingDiseaseFlag;
                    drNew["PreExistingDiseaseAmount"] = request.PreExistingDiseaseAmount;
                    drNew["IsAdventureSportsFlag"] = request.IsAdventureSportsFlag;
                    drNew["AdventureSportsAmount"] = request.AdventureSportsAmount;
                    drNew["IsPetCareFlag"] = request.IsPetCareFlag;
                    drNew["PetCareAmount"] = request.PetCareAmount;
                    drNew["IsCoverageOnCruise"] = request.IsCoverageOnCruise;
                    drNew["CoverageOnCruiseAmount"] = request.CoverageOnCruiseAmount;
                    drNew["IsHomeBurgalaryFlag"] = request.IsHomeBurgalaryFlag;
                    drNew["HomeBurgalaryAmount"] = request.HomeBurgalaryAmount;
                    drNew["IsCardFraudFlag"] = request.IsCardFraudFlag;
                    drNew["CardFlagAmount"] = request.CardFlagAmount;
                    drNew["IsNonIndianPassportHolderFlag"] = request.IsNonIndianPassportHolderFlag;
                    drNew["IsTouristVisaFlag"] = request.IsTouristVisaFlag;
                    drNew["IsShortTermWorkVisaFlag"] = request.IsShortTermWorkVisaFlag;
                    drNew["IsPermanentResidentCardFlag"] = request.IsPermanentResidentCardFlag;
                    drNew["IsLongTermWorkVisaFlag"] = request.IsLongTermWorkVisaFlag;
                    drNew["IsDependentVisaFlag"] = request.IsDependentVisaFlag;
                    drNew["IsDiplomaticVisaFlag"] = request.IsDiplomaticVisaFlag;
                    drNew["IsStudentVisaFlag"] = request.IsStudentVisaFlag;
                    drNew["IsGoinfHomeFlag"] = request.IsGoinfHomeFlag;
                    drNew["IsHolidayFlag"] = request.IsHolidayFlag;
                    drNew["IsStudiesFlag"] = request.IsStudiesFlag;
                    drNew["IsRelocationFlag"] = request.IsRelocationFlag;
                    drNew["IsMedicalTreatmentFlag"] = request.IsMedicalTreatmentFlag;
                    drNew["IsBusinessFlag"] = request.IsBusinessFlag;

                dtCache.Rows.Add(drNew);
                SqlCommandBuilder scb = new SqlCommandBuilder(sqlDataAdapter);
                //dtCache.AcceptChanges();
                sqlDataAdapter.InsertCommand = scb.GetInsertCommand();
                sqlDataAdapter.Update(dtCache);
                //}

                //dr.info.res = JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {
                ErrorLogging(ex);
            }
            return dr;
        }
    }
}