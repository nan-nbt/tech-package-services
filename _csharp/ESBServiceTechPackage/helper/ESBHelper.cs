using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Collections.Generic;

using PCIWeb.Tools;
using Newtonsoft.Json;

using PCaG.ESB.TechPackage.model;

namespace PCaG.ESB.TechPackage.helper;

public class ESBHelper
{
    public static DataRowCollection QueryASN(string factNo, string asnNo, string trfStatus)
    {
        return Tool.ToRows(
            DBHelper.Instance.Query("PCaG.ESB.TechPackage.SelectASN",
                Tool.ToDic(
                    "FACT_NO", factNo,
                    "ASN_NO", asnNo,
                    "TRF_STATUS", trfStatus
                )
            )
        );
    }

    public static DataRowCollection QueryASNCredential(string compNo, string factNo, string asnNo)
    {
        return Tool.ToRows(
            DBHelper.Instance.Query("PCaG.ESB.TechPackage.SelectASNCredential",
                Tool.ToDic(
                    "COMP_NO", compNo,
                    "FACT_NO", factNo,
                    "ASN_NO", asnNo
                )
            )
        );
    }

    public static DataRowCollection QueryCPSBC(string factNo, string formNo)
    {
        return Tool.ToRows(
            DBHelper.Instance.Query("PCaG.ESB.TechPackage.SelectCPSBC",
                Tool.ToDic(
                    "FACT_NO", factNo,
                    "FORM_NO", formNo
                )
            )
        );
    }

    public static DataRowCollection QueryWS()
    {
        return Tool.ToRows(
            DBHelper.Instance.Query("PCaG.ESB.TechPackage.SelectWS", null)
        );
    }

    public static List<Dictionary<string, object>> ListDictionaryWS(DataRowCollection rows)
    {
        var keys = new string[] { };
        keys =
        [
            "COL_CODE","COL_WS"
        ];

        return rows.Cast<DataRow>().Select(row => keys.ToDictionary(key => key, key => row[key])).ToList();
    }

    public static Dictionary<string, object> BuildMaster(DataRow row, int type)
    {
        var keys = new string[] { };
        if (type == 1)
        {
            keys =
            [
                "NAME","PASSWORD","COMP_NO","FACT_NO","ASN_NO","TRF_STATUS","INV_NO","INV_DATE","CURRENCY","ARRIVAL_TYPE",
                "BL_NO","SHIP_TYPE","GOODS_KIND","EXT_DEV_NO","EXP_ARR_DATE","SUP_NO","SKEP","TRF_USER","TRF_TIME"
            ];
        }
        else if (type == 2)
        {
            keys =
            [
                "NAME","PASSWORD","COMP_NO","FACT_NO","ASN_NO"
            ];
        }

        return keys.ToDictionary(key => key, key => row[key]);
    }

    public static List<Dictionary<string, object>> BuildDetails(DataRowCollection rows, int type)
    {
        var keys = new[]
        {
            "ASN_ITEM","INV_ITEM","INV_DES","PURD_NO","PURD_ITEM","MAT_NO","MAT_NAME","SCM_BCLASS_NO","SCM_MCLASS_NO","PURD_QTY",
            "ARR_QTY","FOC_QTY","UNIT","PRICE","ITEM_AMT","NET_WEIGHT","GROSS_WEIGHT","WEIGHT_UNIT","PIECES","PACKAGE_WAY","ORIGIN"
        };

        return type == 1 ? rows.Cast<DataRow>().Select(row => keys.ToDictionary(key => key, key => row[key])).ToList() : null;
    }

    public static ESBPayload BuildPayload(Dictionary<string, object> mst, List<Dictionary<string, object>> dtl, int type)
    {
        if (type == 1)
        {
            return new ESBPayload
            {
                CPS = new CPS
                {
                    AUTH = new AUTH
                    {
                        NAME = mst.GetValueOrDefault("NAME").ToString(),
                        PASSWORD = mst.GetValueOrDefault("PASSWORD").ToString()
                    },
                    ASN = new ASN
                    {
                        COMP_NO = mst.GetValueOrDefault("COMP_NO").ToString(),
                        FACT_NO = mst.GetValueOrDefault("FACT_NO").ToString(),
                        ASN_NO = mst.GetValueOrDefault("ASN_NO").ToString(),
                        TRF_STATUS = mst.GetValueOrDefault("TRF_STATUS").ToString(),
                        INV_NO = mst.GetValueOrDefault("INV_NO").ToString(),
                        INV_DATE = mst.GetValueOrDefault("INV_DATE").ToString(),
                        CURRENCY = mst.GetValueOrDefault("CURRENCY").ToString(),
                        ARRIVAL_TYPE = mst.GetValueOrDefault("ARRIVAL_TYPE").ToString(),
                        BL_NO = mst.GetValueOrDefault("BL_NO").ToString(),
                        SHIP_TYPE = mst.GetValueOrDefault("SHIP_TYPE").ToString(),
                        GOODS_KIND = mst.GetValueOrDefault("GOODS_KIND").ToString(),
                        EXT_DEV_NO = mst.GetValueOrDefault("EXT_DEV_NO").ToString(),
                        EXP_ARR_DATE = mst.GetValueOrDefault("EXP_ARR_DATE").ToString(),
                        SUP_NO = mst.GetValueOrDefault("SUP_NO").ToString(),
                        SKEP = mst.GetValueOrDefault("SKEP").ToString(),
                        TRF_USER = mst.GetValueOrDefault("TRF_USER").ToString(),
                        TRF_TIME = mst.GetValueOrDefault("TRF_TIME").ToString(),
                        ASNDETAIL = dtl.Select(item => new ASNDETAIL
                        {
                            ASN_ITEM = item.GetValueOrDefault("ASN_ITEM").ToString(),
                            INV_ITEM = item.GetValueOrDefault("INV_ITEM").ToString(),
                            INV_DES = item.GetValueOrDefault("INV_DES").ToString(),
                            PURD_NO = item.GetValueOrDefault("PURD_NO").ToString(),
                            PURD_ITEM = item.GetValueOrDefault("PURD_ITEM").ToString(),
                            MAT_NO = item.GetValueOrDefault("MAT_NO").ToString(),
                            MAT_NAME = item.GetValueOrDefault("MAT_NAME").ToString(),
                            SCM_BCLASS_NO = item.GetValueOrDefault("SCM_BCLASS_NO").ToString(),
                            SCM_MCLASS_NO = item.GetValueOrDefault("SCM_MCLASS_NO").ToString(),
                            PURD_QTY = item.GetValueOrDefault("PURD_QTY").ToString(),
                            ARR_QTY = item.GetValueOrDefault("ARR_QTY").ToString(),
                            FOC_QTY = item.GetValueOrDefault("FOC_QTY").ToString(),
                            UNIT = item.GetValueOrDefault("UNIT").ToString(),
                            PRICE = item.GetValueOrDefault("PRICE").ToString(),
                            ITEM_AMT = item.GetValueOrDefault("ITEM_AMT").ToString(),
                            NET_WEIGHT = item.GetValueOrDefault("NET_WEIGHT").ToString(),
                            GROSS_WEIGHT = item.GetValueOrDefault("GROSS_WEIGHT").ToString(),
                            WEIGHT_UNIT = item.GetValueOrDefault("WEIGHT_UNIT").ToString(),
                            PIECES = item.GetValueOrDefault("PIECES").ToString(),
                            PACKAGE_WAY = item.GetValueOrDefault("PACKAGE_WAY").ToString(),
                            ORIGIN = item.GetValueOrDefault("ORIGIN").ToString()
                        }).ToList()
                    }
                }
            };
        }
        else if (type == 2)
        {
            return new ESBPayload
            {
                CPS = new CPS
                {
                    AUTH = new AUTH
                    {
                        NAME = mst.GetValueOrDefault("NAME").ToString(),
                        PASSWORD = mst.GetValueOrDefault("PASSWORD").ToString()
                    },
                    BC = new BC
                    {
                        COMP_NO = mst.GetValueOrDefault("COMP_NO").ToString(),
                        FACT_NO = mst.GetValueOrDefault("FACT_NO").ToString(),
                        FORM_NO = mst.GetValueOrDefault("ASN_NO").ToString()
                    }
                }
            };
        }
        else
        {
            return null;
        }
    }

    public static (string code, string msg, string responseContent) PostToCPS(ESBPayload payload, string url)
    {
        string jsonCnt = JsonConvert.SerializeObject(payload);
        Tool.Info("ESB Service Request", "payload", jsonCnt, "time", DateTime.Now);

        using (HttpClient cln = new HttpClient { Timeout = TimeSpan.FromMinutes(5) })
        {
            var cnt = new StringContent(jsonCnt, Encoding.UTF8, "application/json");
            HttpResponseMessage res = cln.PostAsync(url, cnt).GetAwaiter().GetResult();
            string resCnt = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            Tool.Info("ESB Service Response", "statusCode", res.StatusCode.ToString(), "res", resCnt, "time", DateTime.Now);

            var resObj = JsonConvert.DeserializeObject<dynamic>(resCnt);

            string code = "code not found";
            string msg = "message not found";

            if (resObj != null && resObj.RESULT != null)
            {
                if (resObj.RESULT.CODE != null) code = resObj.RESULT.CODE.ToString();
                if (resObj.RESULT.RESULT != null) msg = resObj.RESULT.RESULT.ToString();
                else if (resObj.RESULT.MESSAGE != null) msg = resObj.RESULT.MESSAGE.ToString();
            }

            if (!res.IsSuccessStatusCode)
                throw new Exception($"API call failed with status: {res.StatusCode}, Response: {resCnt}");

            return (code, msg, resCnt);
        }
    }

    public static void HandleResponse(Dictionary<string, object> mst, List<Dictionary<string, object>> dtl, string code, string msg, string resCnt, int type)
    {
        var xml = BuildCustomXml(mst, dtl, type);

        DBHelper.Instance.Execute("PCaG.ESB.TechPackage.InsertCPSRetMsg",
            Tool.ToDic(
                "FACT_NO", mst.GetValueOrDefault("FACT_NO").ToString(),
                "ESB_NAME", type == 1 ? "PCaG_ASN" : "PCaG_BC",
                "RET_TIME", DateTime.Now.ToString("yyyyMMddHHmmss"),
                "CODE", code,
                "MSG", type == 1 ? msg : resCnt,
                "ASN_NO", mst.GetValueOrDefault("ASN_NO").ToString(),
                "XML_DOC", xml
            )
        );

        if (type == 1 || type == 2 && code != "000") { Tool.Info("ESB Service Response", "code", code, "message", msg, "time", DateTime.Now); return; }

        var resObj = JsonConvert.DeserializeObject<dynamic>(resCnt);
        var resMst = resObj.RESULT.RESULT[0];
        var resDtl = resMst.BCDETAIL;

        var row = Tool.ToRow(
            DBHelper.Instance.Query("PCaG.ESB.TechPackage.SelectCPSBC",
                Tool.ToDic(
                    "FACT_NO", resMst.FACT_NO.ToString(),
                    "FORM_NO", resMst.FORM_NO.ToString()
                )
            )
        );

        bool isExist = row["CNT"] > 0;

        var common = Tool.ToDic(
            "COMP_NO", resMst.COMP_NO.ToString(),
            "FACT_NO", resMst.FACT_NO.ToString(),
            "FORM_TYPE", resMst.FORM_TYPE.ToString(),
            "FORM_NO", resMst.FORM_NO.ToString(),
            "CLOSE_DATE", resMst.CLOSE_DATE.ToString(),
            "DECR_STATUS", resMst.DECR_STATUS.ToString(),
            "DECR_TYPE", resMst.DECR_TYPE.ToString(),
            "DECR_NO", resMst.DECR_NO.ToString(),
            "DECR_DATE", resMst.DECR_DATE.ToString(),
            "AJU_NO", resMst.AJU_NO.ToString(),
            "SUP_NO", resMst.SUP_NO.ToString(),
            "INV_NO", resMst.INV_NO.ToString(),
            "INV_DATE", resMst.INV_DATE.ToString(),
            "CURRENCY", resMst.CURRENCY.ToString(),
            "TRF_TIME", resMst.TRF_TIME.ToString(),
            "TRF_USER", resMst.TRF_USER.ToString()
        );

        foreach (var detail in resDtl)
        {
            var dic = new Dictionary<string, object>(common)
            {
                ["FORM_ITEM"] = detail.FORM_ITEM.ToString(),
                ["DECR_ITEM"] = detail.DECR_ITEM.ToString(),
                ["INV_ITEM"] = detail.INV_ITEM.ToString(),
                ["DECR_AMT_FOB"] = detail.DECR_AMT_FOB.ToString(),
                ["DECR_AMT_CIF"] = detail.DECR_AMT_CIF.ToString()
            };

            if (isExist)
            {
                var updateDic = new Dictionary<string, object>(dic)
                {
                    ["COMP_NO__W"] = resMst.COMP_NO.ToString(),
                    ["FACT_NO__W"] = resMst.FACT_NO.ToString(),
                    ["FORM_TYPE__W"] = resMst.FORM_TYPE.ToString(),
                    ["FORM_NO__W"] = resMst.FORM_NO.ToString(),
                    ["FORM_ITEM__W"] = detail.FORM_ITEM.ToString()
                };

                DBHelper.Instance.Execute("PCaG.ESB.TechPackage.UpdateCPSBC", updateDic);
            }
            else
            {
                DBHelper.Instance.Execute("PCaG.ESB.TechPackage.InsertCPSBC", dic);
            }

            DBHelper.Instance.Execute("PCaG.ESB.TechPackage.InsertCPSBCHis", dic);
        }

        Tool.Info("ESB Service Response", "code", code, "message", msg, "time", DateTime.Now);
    }

    public static string BuildCustomXml(Dictionary<string, object> mst, List<Dictionary<string, object>> dtl, int type)
    {
        var doc = new XmlDocument();
        var xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        doc.AppendChild(xmlDeclaration);

        XmlElement root = doc.CreateElement("root");
        doc.AppendChild(root);
        XmlElement cps = doc.CreateElement("CPS");
        root.AppendChild(cps);
        XmlElement auth = doc.CreateElement("AUTH");
        cps.AppendChild(auth);

        XmlElement name = doc.CreateElement("NAME");
        name.InnerText = mst.GetValueOrDefault("NAME").ToString();
        auth.AppendChild(name);

        XmlElement pwd = doc.CreateElement("PASSWORD");
        pwd.InnerText = mst.GetValueOrDefault("PASSWORD").ToString();
        auth.AppendChild(pwd);

        if (type == 1)
        {
            XmlElement asn = doc.CreateElement("ASN");
            cps.AppendChild(asn);

            // ASN mst
            string[] headerFields = {
                "COMP_NO","FACT_NO","ASN_NO","TRF_STATUS","INV_NO","INV_DATE","CURRENCY","ARRIVAL_TYPE",
                "BL_NO","SHIP_TYPE","GOODS_KIND","EXT_DEV_NO","EXP_ARR_DATE","SUP_NO","SKEP","TRF_USER","TRF_TIME"
            };

            foreach (var field in headerFields)
            {
                XmlElement el = doc.CreateElement(field);
                el.InnerText = mst[field].ToString();
                asn.AppendChild(el);
            }

            // ASN dtl (loop over dtl list)
            foreach (var item in dtl)
            {
                XmlElement asnDetail = doc.CreateElement("ASNDETAIL");
                asn.AppendChild(asnDetail);

                string[] detailFields = {
                    "ASN_ITEM","INV_ITEM","INV_DES","PURD_NO","PURD_ITEM","MAT_NO","MAT_NAME","SCM_BCLASS_NO","SCM_MCLASS_NO","PURD_QTY",
                    "ARR_QTY","FOC_QTY","UNIT","PRICE","ITEM_AMT","NET_WEIGHT","GROSS_WEIGHT","WEIGHT_UNIT","PIECES","PACKAGE_WAY","ORIGIN"
                };

                foreach (var field in detailFields)
                {
                    XmlElement el = doc.CreateElement(field);
                    el.InnerText = item.GetValueOrDefault(field).ToString();
                    asnDetail.AppendChild(el);
                }
            }
        }
        else if (type == 2)
        {
            XmlElement bc = doc.CreateElement("BC");
            cps.AppendChild(bc);

            string[] headerFields = {
                "COMP_NO","FACT_NO","FORM_NO"
            };

            foreach (var field in headerFields)
            {
                XmlElement el = doc.CreateElement(field);
                if (field == "FORM_NO") el.InnerText = mst["ASN_NO"].ToString();
                else el.InnerText = mst[field].ToString();
                bc.AppendChild(el);
            }
        }

        return BeautifyXml(doc);
    }

    public static string BeautifyXml(XmlDocument doc)
    {
        var settings = new XmlWriterSettings
        {
            Indent = true,              // pretty print
            IndentChars = "  ",         // 2 spaces
            Encoding = Encoding.UTF8,
            OmitXmlDeclaration = false, // keep <?xml ... ?>
            NewLineChars = "\r\n",      // Windows new line
            NewLineHandling = NewLineHandling.Replace
        };

        using (var ms = new MemoryStream())
        using (var writer = XmlWriter.Create(ms, settings))
        {
            doc.Save(writer);
            writer.Flush();

            ms.Position = 0; // reset stream
            using (var sr = new StreamReader(ms, Encoding.UTF8))
            {
                return sr.ReadToEnd();
            }
        }
    }
}