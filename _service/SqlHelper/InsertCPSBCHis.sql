DB: PCaG.ESB.TechPackage.DB

Type: sql

INSERT INTO Y_CPS_BC_HIS
    (
        COMP_NO,
        FACT_NO,
        FORM_TYPE,
        FORM_NO,
        FORM_ITEM,
        CLOSE_DATE,
        DECR_STATUS,
        DECR_TYPE,
        DECR_NO,
        DECR_ITEM,
        DECR_DATE,
        AJU_NO,
        SUP_NO,
        INV_NO,
        INV_ITEM,
        INV_DATE,
        DECR_AMT_FOB,
        DECR_AMT_CIF,
        CURRENCY,
        TRF_TIME,
        TRF_USER
    )
    VALUES
    (
        *COMP_NO*,
        *FACT_NO*,
        *FORM_TYPE*,
        *FORM_NO*,
        *FORM_ITEM*,
        *CLOSE_DATE*,
        *DECR_STATUS*,
        *DECR_TYPE*,
        *DECR_NO*,
        *DECR_ITEM*,
        *DECR_DATE*,
        *AJU_NO*,
        *SUP_NO*,
        *INV_NO*,
        *INV_ITEM*,
        *INV_DATE*,
        *DECR_AMT_FOB*,
        *DECR_AMT_CIF*,
        *CURRENCY*,
        *TRF_TIME*,
        *TRF_USER*
    )