using System;
using PCIWeb.Tools;

using PCaG.ESB.TechPackage.helper;

namespace PCaG.ESB.TechPackage;

public class ESBService
{
    private static string _API_URL_1 = ESBHelper.ListDictionaryWS(ESBHelper.QueryWS())[0]["COL_WS"].ToString();

    private static string _API_URL_2 = ESBHelper.ListDictionaryWS(ESBHelper.QueryWS())[1]["COL_WS"].ToString();

    public static string SendToCPS(string factNo, string asnNo, string trfStatus)
    {
        try
        {
            // 1. Query database
            var rows = ESBHelper.QueryASN(factNo, asnNo, trfStatus);
            if (rows == null || rows.Count == 0) return "no data found or data already sent, please check again";

            // 2. Build master and detail
            var type = 1;
            var mst = ESBHelper.BuildMaster(rows[0], type);
            var dtl = ESBHelper.BuildDetails(rows, type);

            // 3. Build payload
            var payload = ESBHelper.BuildPayload(mst, dtl, type);

            // 4. Send to CPS API
            var (code, msg, responseContent) = ESBHelper.PostToCPS(payload, _API_URL_1);

            // 5. Log & persist result
            ESBHelper.HandleResponse(mst, dtl, code, msg, responseContent, type);

            return "success";
        }
        catch (Exception ex)
        {
            Tool.Info("ESB Service Error", "error", ex.Message, "time", DateTime.Now);
            throw;
        }
    }

    public static string GetBC(string compNo, string factNo, string asnNo)
    {
        try
        {
            // 1. Query database            
            var rows = ESBHelper.QueryASNCredential(compNo, factNo, asnNo);
            if (rows == null || rows.Count == 0) return "no data found or data already sent, please check again";

            // 2. Build master and detail
            var type = 2;
            var mst = ESBHelper.BuildMaster(rows[0], type);
            var dtl = ESBHelper.BuildDetails(rows, type);

            // 3. Build payload
            var payload = ESBHelper.BuildPayload(mst, dtl, type);

            // 4. Send to CPS API
            var (code, msg, responseContent) = ESBHelper.PostToCPS(payload, _API_URL_2);

            // 5. Log & persist result
            ESBHelper.HandleResponse(mst, dtl, code, msg, responseContent, type);

            return "success";
        }
        catch (Exception ex)
        {
            Tool.Info("ESB Service Error", "error", ex.Message, "time", DateTime.Now);
            throw;
        }

    }
}
