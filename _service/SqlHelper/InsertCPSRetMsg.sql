DB: PCaG.ESB.TechPackage.DB

Type: sql

INSERT
	INTO
	Y_CPS_RET_MSG
    (
        FACT_NO,
        ESB_NAME,
        RET_TIME,
        RET_CODE,
        RET_MSG,
        RET_DATA_1,
        OUT_JSON
    )
    VALUES
    (
        *FACT_NO*,
        *ESB_NAME*,
        *RET_TIME*,
        *CODE*,
        *MSG*,
        *ASN_NO*,
        *XML_DOC*
    )
