using System.Collections.Generic;

namespace PCaG.ESB.TechPackage.model;

public class ESBPayload
{
    public CPS CPS { get; set; }
}

public class CPS
{
    public AUTH AUTH { get; set; }
    public ASN ASN { get; set; }
    public BC BC { get; set; }
}

public class AUTH
{
    public string NAME { get; set; }
    public string PASSWORD { get; set; }
}

public class ASN
{
    public string COMP_NO { get; set; }
    public string FACT_NO { get; set; }
    public string ASN_NO { get; set; }
    public string TRF_STATUS { get; set; }
    public string INV_NO { get; set; }
    public string INV_DATE { get; set; }
    public string CURRENCY { get; set; }
    public string ARRIVAL_TYPE { get; set; }
    public string BL_NO { get; set; }
    public string SHIP_TYPE { get; set; }
    public string GOODS_KIND { get; set; }
    public string EXT_DEV_NO { get; set; }
    public string EXP_ARR_DATE { get; set; }
    public string SUP_NO { get; set; }
    public string SKEP { get; set; }
    public string TRF_USER { get; set; }
    public string TRF_TIME { get; set; }
    public List<ASNDETAIL> ASNDETAIL { get; set; }
}

public class ASNDETAIL
{
    public string ASN_ITEM { get; set; }
    public string INV_ITEM { get; set; }
    public string INV_DES { get; set; }
    public string PURD_NO { get; set; }
    public string PURD_ITEM { get; set; }
    public string MAT_NO { get; set; }
    public string MAT_NAME { get; set; }
    public string SCM_BCLASS_NO { get; set; }
    public string SCM_MCLASS_NO { get; set; }
    public string PURD_QTY { get; set; }
    public string ARR_QTY { get; set; }
    public string FOC_QTY { get; set; }
    public string UNIT { get; set; }
    public string PRICE { get; set; }
    public string ITEM_AMT { get; set; }
    public string NET_WEIGHT { get; set; }
    public string GROSS_WEIGHT { get; set; }
    public string WEIGHT_UNIT { get; set; }
    public string PIECES { get; set; }
    public string PACKAGE_WAY { get; set; }
    public string ORIGIN { get; set; }
}

public class BC
{
    public string COMP_NO { get; set; }
    public string FACT_NO { get; set; }
    public string FORM_NO { get; set; }
}