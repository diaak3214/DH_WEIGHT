using System;
using System.Collections.Generic;
using System.Data;

namespace OCT_WEIGHT.AutoWeight
{
    public static class DB_Process
    {

        #region 오늘일자 계량 제한 체크 PROCEDURE : SP_MU_CAR_LIMIT_CHK
        public static String CAR_LIMIT_CHK_ALL()
        {
            ServiceAdapter _svc = new ServiceAdapter();
            String rtn = "Y";

            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_FG", "02");
            p.Add("P_VEHL_NO", "");
            DataSet ds = _svc.GetQuerySP("SP_MU_CAR_LIMIT_CHK", p);

            if (ds != null)
            {
                rtn = ds.Tables[0].Rows[0]["LIMIT_YN"].ToString();
            }

            return rtn;
        }
        #endregion

        #region 계량대 명칭 확인 PROCEDURE : SP_MU_COMMON_R
        public static String weight_name(string area_code)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            String rtn = "";

            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_TYPE_CD", "WS_001");
            p.Add("P_CODE", area_code);
            DataSet ds = _svc.GetQuerySP("SP_MU_COMMON_R", p);

            if (ds != null)
            {
                rtn = ds.Tables[0].Rows[0]["TITLE"].ToString();
            }

            return rtn;
        }
        #endregion

        #region LPR 사용여부 조회 PROCEDURE : SP_MU_LPR_INFO_R
        public static DataTable LPR_INFO(string sWeight_Area)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_CODE_VALUE1", sWeight_Area);

            DataSet ds = _svc.GetQuerySP("SP_MU_LPR_INFO_R", p);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 고정 RF카드 정보조회 PROCEDURE : SP_COMBO_R
        //고정 RF카드 정보조회(2021-07-23 정성호 추가)
        public static DataTable Get_RFID_INFO(string Type_cd)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_TYPE_CD", Type_cd);

            DataSet ds = _svc.GetQuerySP("SP_COMBOX_R", p);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region RFID 카드 시리얼 번호로 카드번호 조회 PROCEDURE : SP_MU_RFID_INFO_R
        public static DataTable RFID_INFO(string rfid)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_RFID_CARD", rfid);

            DataSet ds = _svc.GetQuerySP("SP_MU_RFID_INFO_R", p);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 사내이송 여부  조회 PROCEDURE : SP_MU_TRANSFER_CHK
        public static DataTable RFID_TRANSFER_CHK(string rfid, string plnt)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_RFID_CARD", rfid);
            p.Add("P_PLNT_NO", plnt);   //공장구분 추가(2020-02-17 한민호)
            DataSet ds = _svc.GetQuerySP("SP_MU_TRANSFER_CHK", p);

            //DBLK_LIVE는 조회 후 링크 기능을 종료 해야 함(2020-03-12 한민호)
            String Query1 = "COMMIT ";
            _svc.GetQuery(Query1);
            String Query2 = "ALTER SESSION CLOSE DATABASE LINK DBLK_LIVE ";
            _svc.GetQuery(Query2);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 계량화면  DISPLAY 조회 PROCEDURE : SP_MU_RESULT_DISPLAY_R
        public static DataTable RESULT_DISPLAY(string rfid_seq)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            ////2019-12-30 대한제강
            ////계량화면 DISPLAY 조회
            //String Query = " SELECT A.SEQ_NO AS RFID_SEQ, TO_CHAR(A.MEA_DATE,'YYYYMMDD') || LPAD(A.MEA_SEQ,4,'0') AS WGHT_NO, TO_CHAR(A.MEA_DATE,'YYYYMMDD') || LPAD(A.MEA_SEQ,4,'0') AS MAIN_WGHT_NO"
            //             + "       ,A.IN_WGT AS LOAD_WEIGHT, A.OUT_WGT AS DOWN_WEIGHT"
            //             + "       ,CASE WHEN A.ORD_GB = 'S' THEN A.OUT_WGT - A.IN_WGT ELSE A.IN_WGT - A.OUT_WGT END AS REAL_WGHT, 0 AS PROD_WGHT, NULL AS LOAD_ORD_NO, 0 AS PMIN_WGHT, 0 AS PMAX_WGHT"
            //             + "       ,''  AS INSPECT_YN, 0 AS INST_WGT, 'C' AS RMW_SALES_TP "
            //             + "   FROM WMS_MEASURE_RST A"
            //             + "  WHERE A.USE_YN= 'Y' "
            //             + "    AND A.SEQ_NO = '"+rfid_seq+"'"
            //             ;
            //DataSet ds = _svc.GetQuery(Query);
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_RFID_SEQ", rfid_seq);
            DataSet ds = _svc.GetQuerySP("SP_MU_RESULT_DISPLAY_R", p);

            //DBLK_LIVE는 조회 후 링크 기능을 종료 해야 함(2020-03-12 한민호)
            String Query1 = "COMMIT ";
            _svc.GetQuery(Query1);
            String Query2 = "ALTER SESSION CLOSE DATABASE LINK DBLK_LIVE ";
            _svc.GetQuery(Query2);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region TEST 계량 등록 PROCEDURE : SP_MU_TEST_WEIGHT_SET
        public static DataTable insert_test(string Weight_Area, string RFID_NO, Int32 LOAD_WEIGHT)
        {
            ServiceAdapter _svc = new ServiceAdapter();

            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_AREA", Weight_Area);
            p.Add("P_RFID_CARD", RFID_NO);
            p.Add("P_DOWN_WEIGHT", LOAD_WEIGHT.ToString());
            DataSet ds = _svc.GetQuerySP("SP_MU_TEST_WEIGHT_SET", p);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 방사능 체크 등록 PROCEDURE : SP_MU_RADIATION_CHK_SET
        public static DataTable insert_RADIATION_CHK(string rfid_seq)
        {
            ServiceAdapter _svc = new ServiceAdapter();

            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_RFID_SEQ", rfid_seq);

            DataSet ds = _svc.GetQuerySP("SP_MU_RADIATION_CHK_SET", p);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion


        #region 계량처리 PROCEDURE : SP_MU_WEIGHT_01_SET ,SP_MU_WEIGHT_02_SET
        //스틸컷 이미지 경로 추가(2020-02-10 오창휘 수정)
        public static bool weight_fg(String kind, String gubun, Int32 LOAD_WEIGHT, String RFID_SEQ, String wght_no, string state, string img_path1, string img_path2)
        //public static bool weight_fg(String kind, String gubun, Int32 LOAD_WEIGHT, String RFID_SEQ, String wght_no, string state)
        {
            bool rtn = false;

            if (gubun == "N")
            {
                //스틸컷 이미지 경로 추가(2020-02-10 오창휘 수정)
                rtn = insert_first(kind, LOAD_WEIGHT, RFID_SEQ, state, img_path1, img_path2);
                //rtn = insert_first(kind, LOAD_WEIGHT, RFID_SEQ, state);
            }

            if (gubun == "Y")
            {
                //스틸컷 이미지 경로 추가(2020-02-10 오창휘 수정)
                rtn = update_second(kind, LOAD_WEIGHT, RFID_SEQ, state, img_path1, img_path2);
                //대한제강은 일련번호로 계량함(2019-12-30 대한제강)
                //rtn = update_second(kind, LOAD_WEIGHT, RFID_SEQ, state);
                //rtn = update_second(kind, LOAD_WEIGHT, wght_no, state);
            }
            return rtn;
        }

        #region insert_first 1차 계량 등록 PROCEDURE : SP_MU_WEIGHT_01_SET
        //스틸컷 이미지 경로 추가(2020-02-10 오창휘 수정)
        private static bool insert_first(string weight_kind, Int32 LOAD_WEIGHT, String RFID_SEQ, string state, string img_path1, string img_path2)
        //private static bool insert_first(string weight_kind, Int32 LOAD_WEIGHT, String RFID_SEQ, string state)
        {
            String rtn = "1";
            ////2019-12-30 대한제강
            ////1차계량
            //ServiceAdapter _svc = new ServiceAdapter();
            //String Query = null;
            //Query = " UPDATE WMS_MEASURE_RST SET IN_WGT_DT = SYSDATE, IN_WGT = '" + LOAD_WEIGHT.ToString() + "', MOD_ID = 'octosys', MOD_DT = SYSDATE WHERE SEQ_NO = '" + RFID_SEQ + "'";
            //_svc.GetQuery(Query);
            //Query = " SELECT 0 AS RESULT, '" + RFID_SEQ + "' AS ERRMSG FROM DUAL";
            //DataSet ds = _svc.GetQuery(Query);

            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("V_STATE", state);
            p.Add("V_RFID_SEQ", RFID_SEQ);
            p.Add("V_LOAD_WEIGHT", LOAD_WEIGHT.ToString());

            p.Add("V_IN_IMG_PATH", img_path1);
            p.Add("V_IN_IMG_PATH2", img_path2);

            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_01_SET", p);
            if (ds != null)
            {
                rtn = ds.Tables[0].Rows[0]["RESULT"].ToString();
            }

            if (rtn == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region update_second 2차 계량 등록(실행 순서 2) PROCEDURE : SP_MU_WEIGHT_02_SET
        //스틸컷 이미지 경로 추가(2020-02-10 오창휘 수정)
        private static bool update_second(string weight_kind, Int32 LOAD_WEIGHT, string wght_no, string state, string img_path1, string img_path2)
        //private static bool update_second(string weight_kind, Int32 LOAD_WEIGHT, string wght_no, string state)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            String rtn = "1";

            //2019-12-30 대한제강
            //2차계량
            //String Query = null;
            //Query = " UPDATE ( "
            //      + "        SELECT OUT_WGT_DT, OUT_WGT, REAL_WGT "
            //      + "              ,CASE WHEN ORD_GB = 'S' THEN '" + LOAD_WEIGHT.ToString() + "' - IN_WGT ELSE IN_WGT - '" + LOAD_WEIGHT.ToString() + "' END AS REAL_WGT2 "
            //      + "              ,MOD_ID, MOD_DT"
            //      + "          FROM WMS_MEASURE_RST "
            //      + "         WHERE SEQ_NO = '" + wght_no + "' "
            //      + "        ) "
            //      + " SET OUT_WGT_DT = SYSDATE"
            //      + "    ,OUT_WGT = '" + LOAD_WEIGHT.ToString() + "'"
            //      + "    ,REAL_WGT = REAL_WGT2"
            //      + "    ,MOD_ID = 'octosys' "
            //      + "    ,MOD_DT = SYSDATE";
            //_svc.GetQuery(Query);

            //Query = " SELECT 0 AS RESULT, '" + wght_no + "' AS ERRMSG FROM DUAL";
            //DataSet ds = _svc.GetQuery(Query);
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("V_STATE", state);
            p.Add("V_WGHT_NO", wght_no);
            p.Add("V_LOAD_WEIGHT", LOAD_WEIGHT.ToString());

            p.Add("V_OUT_IMG_PATH", img_path1);
            p.Add("V_OUT_IMG_PATH2", img_path2);

            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_02_SET", p);

            if (ds != null)
            {
                rtn = ds.Tables[0].Rows[0]["RESULT"].ToString();
            }

            if (rtn == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #endregion

        #region Start_rfid RFID 카드 조회 PROCEDURE : SP_MU_WEIGHT_ALL_R
        public static DataTable Start_rfid(string rfid)
        {
            ////2019-12-30 대한제강
            ////배차정보 찾기
            //String Query = " SELECT A.SEQ_NO AS RFID_SEQ, A.CAR_NO AS VEHL_NO, A.ITEM_CODE AS ITEM_SO, A.CARD_NO AS RFID_NO, A.ITEM_NM AS ITEM_SO_NM"
            //             + "       ,A.ITEM_TYPE AS ITEM_JUNG, TO_CHAR(A.MEA_DATE,'YYYYMMDD') || LPAD(A.MEA_SEQ,4,'0') AS WGHT_NO"
            //             + "       ,TO_CHAR(A.MEA_DATE,'YYYYMMDD') || LPAD(A.MEA_SEQ,4,'0') AS MAIN_WGHT_NO, A.IN_WGT AS LOAD_WEIGHT, A.ITEM_TYPE AS ITEM_JUNG_NM"
            //             + "       ,CASE WHEN A.ORD_GB = 'S' THEN '3' ELSE '1' END INOUT_GUBUN,'' AS IF_NO,'' AS IF_NO2"
            //             + "       ,CASE WHEN IN_WGT_DT IS NULL AND OUT_WGT_DT IS NULL THEN '' WHEN OUT_WGT_DT IS NULL THEN '1' END AS WEIGHT_STATE, NULL AS PMAX_WGHT"
            //             + "       ,NULL AS PMIN_WGHT, VENDOR_ID AS CUST_CD, VENDOR_ID AS CUST_NM, NULL AS UP_SITE_NM, NULL AS DOWN_SITE_CD"
            //             + "       ,NULL AS DOWN_SITE_NM, NULL AS INSPECT, NULL AS RADIATION_CHK_COUNT, NULL AS CAR_ENT_SEQ, NULL AS BJ_FG"
            //             + "       ,NULL AS IMG_VEHL_NO"
            //             + "   FROM WMS_MEASURE_RST A "
            //             + "  WHERE CARD_NO = '"+rfid+"' "
            //             + "  ORDER BY A.CARD_REG_DT "
            //             ;
            //ServiceAdapter _svc = new ServiceAdapter();
            //DataSet ds = _svc.GetQuery(Query);
            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_RFID_CARD", rfid);
            p.Add("P_VEHL_NO", "");
            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_ALL_NEW_R", p);

            //DBLK_LIVE는 조회 후 링크 기능을 종료 해야 함(2020-03-12 한민호)
            String Query1 = "COMMIT ";
            _svc.GetQuery(Query1);
            String Query2 = "ALTER SESSION CLOSE DATABASE LINK DBLK_LIVE ";
            _svc.GetQuery(Query2);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Start_rfid RFID 카드 조회 PROCEDURE : SP_MU_WEIGHT_ALL_R
        public static DataTable Start_rfid2(string rfid)
        {
            ////2019-12-30 대한제강
            ////배차정보 찾기
            //String Query = " SELECT A.SEQ_NO AS RFID_SEQ, A.CAR_NO AS VEHL_NO, A.ITEM_CODE AS ITEM_SO, A.CARD_NO AS RFID_NO, A.ITEM_NM AS ITEM_SO_NM"
            //             + "       ,A.ITEM_TYPE AS ITEM_JUNG, TO_CHAR(A.MEA_DATE,'YYYYMMDD') || LPAD(A.MEA_SEQ,4,'0') AS WGHT_NO"
            //             + "       ,TO_CHAR(A.MEA_DATE,'YYYYMMDD') || LPAD(A.MEA_SEQ,4,'0') AS MAIN_WGHT_NO, A.IN_WGT AS LOAD_WEIGHT, A.ITEM_TYPE AS ITEM_JUNG_NM"
            //             + "       ,CASE WHEN A.ORD_GB = 'S' THEN '3' ELSE '1' END INOUT_GUBUN,'' AS IF_NO,'' AS IF_NO2"
            //             + "       ,CASE WHEN IN_WGT_DT IS NULL AND OUT_WGT_DT IS NULL THEN '' WHEN OUT_WGT_DT IS NULL THEN '1' END AS WEIGHT_STATE, NULL AS PMAX_WGHT"
            //             + "       ,NULL AS PMIN_WGHT, VENDOR_ID AS CUST_CD, VENDOR_ID AS CUST_NM, NULL AS UP_SITE_NM, NULL AS DOWN_SITE_CD"
            //             + "       ,NULL AS DOWN_SITE_NM, NULL AS INSPECT, NULL AS RADIATION_CHK_COUNT, NULL AS CAR_ENT_SEQ, NULL AS BJ_FG"
            //             + "       ,NULL AS IMG_VEHL_NO"
            //             + "   FROM WMS_MEASURE_RST A "
            //             + "  WHERE CARD_NO = '"+rfid+"' "
            //             + "  ORDER BY A.CARD_REG_DT "
            //             ;
            //ServiceAdapter _svc = new ServiceAdapter();
            //DataSet ds = _svc.GetQuery(Query);
            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_RFID_CARD", rfid);
            p.Add("P_VEHL_NO", "");
            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_ALL_NEW_R2", p);

            //DBLK_LIVE는 조회 후 링크 기능을 종료 해야 함(2020-03-12 한민호)
            String Query1 = "COMMIT ";
            _svc.GetQuery(Query1);
            String Query2 = "ALTER SESSION CLOSE DATABASE LINK DBLK_LIVE ";
            _svc.GetQuery(Query2);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 프린트 용지 체크 PROCEDURE : SP_MU_PRINT_CHK_R
        public static DataTable PRINT_CHK_R(string Weight_Area)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_WEIGHT_AREA", Weight_Area);

            DataSet ds = _svc.GetQuerySP("SP_MU_PRINT_CHK_R", p);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 계량방식 조회 PROCEDURE : SP_MU_WEIGHT_INFO_R
        public static DataTable WEIGHT_INFO(string sWEIGHT_FG, string sITEM_JUNG, string sITEM_SO)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_WEIGHT_FG", sWEIGHT_FG);
            p.Add("P_ITEM_JUNG", sITEM_JUNG);
            p.Add("P_ITEM_SO", sITEM_SO);

            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_INFO_R", p);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Fix_car 고정 차량 확인 PROCEDURE : SP_MU_WEIGHT_FIX_CAR
        public static String Fix_car(string rfid)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            String rtn = "Y";

            Dictionary<string, string> p = new Dictionary<string, string>();

            p.Add("P_RFID_CARD", rfid);
            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_FIX_CAR", p);

            if (ds != null)
            {
                rtn = ds.Tables[0].Rows[0]["RESULT"].ToString();
            }
            return rtn;
        }
        #endregion

        #region 자동 배차 카드 인지 확인 PROCEDURE : SP_MU_WEIGHT_AUTO_CAR
        public static String Act_car(string rfid)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            String rtn = "Y";

            Dictionary<string, string> p = new Dictionary<string, string>();

            p.Add("P_RFID_CARD", rfid);
            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_AUTO_CAR", p);

            if (ds != null)
            {
                rtn = ds.Tables[0].Rows[0]["RESULT"].ToString();
            }
            return rtn;
        }
        #endregion

        #region 공차 계량 등록 PROCEDURE : SP_MU_WEIGHT_FIX_CAR_SET
        //자가고철 스틸컷 이미지 경로 추가(2020-02-24 한민호)
        public static DataTable Gongcha_insert(string rfid, Int32 LOAD_WEIGHT, string area, string img_path1, string img_path2)
        //public static DataTable Gongcha_insert(string rfid, Int32 LOAD_WEIGHT, string area)
        {
            //fg : 1- 배차 정보 없이 처리 ,그외- 배차 시퀀스 
            ServiceAdapter _svc = new ServiceAdapter();

            Dictionary<string, string> p = new Dictionary<string, string>();

            p.Add("P_RFID_CARD", rfid);
            p.Add("P_AREA", area);
            p.Add("P_LOAD_WEIGHT", LOAD_WEIGHT.ToString());
            //자가고철 스틸컷 이미지 경로 추가(2020-02-24 한민호)
            p.Add("P_IMG_PATH", img_path1);
            p.Add("P_IMG_PATH2", img_path2);
            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_FIX_CAR_SET", p);

            //DBLK_LIVE는 조회 후 링크 기능을 종료 해야 함(2020-03-12 한민호)
            String Query1 = "COMMIT ";
            _svc.GetQuery(Query1);
            String Query2 = "ALTER SESSION CLOSE DATABASE LINK DBLK_LIVE ";
            _svc.GetQuery(Query2);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region GAP체크 PROCEDURE : SP_MU_WEIGHT_FIX_CAR_SET2
        //1차계량후 GAP체크(2021-06-29 정성호)
        public static DataTable GAP_Check(string rfid, string area)
        {
            ServiceAdapter _svc = new ServiceAdapter();

            Dictionary<string, string> p = new Dictionary<string, string>();

            p.Add("P_RFID_CARD", rfid);
            p.Add("P_AREA", area);
            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_FIX_CAR_SET2", p);

            //DBLK_LIVE 조회안하는데 이구문 사용하면 database Link is not open 오류 발생함
            ////DBLK_LIVE는 조회 후 링크 기능을 종료 해야 함(2020-03-12 한민호) 
            //String Query1 = "COMMIT ";
            //_svc.GetQuery(Query1);
            //String Query2 = "ALTER SESSION CLOSE DATABASE LINK DBLK_LIVE ";
            //_svc.GetQuery(Query2);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 프린터 사용 매수 저장 PROCEDURE : SP_MU_PRINT_PAGE_CNT_SET
        public static bool PRINT_PAGE_CNT_SET(string Weight_Area, Int32 PAGE_CNT)
        {
            String rtn = "1";
            ServiceAdapter _svc = new ServiceAdapter();

            Dictionary<string, string> p = new Dictionary<string, string>();

            p.Add("P_WEIGHT_AREA", Weight_Area);
            p.Add("P_PNT_PAGE_CNT", PAGE_CNT.ToString());
            DataSet ds = _svc.GetQuerySP("SP_MU_PRINT_PAGE_CNT_SET", p);

            if (ds != null)
            {
                rtn = ds.Tables[0].Rows[0]["RESULT"].ToString();
            }

            if (rtn == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 차량번호 계량 제한 체크 PROCEDURE : SP_MU_CAR_LIMIT_CHK
        public static String check_car_limit(string Flag, string carno)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            String rtn = "Y";

            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_FG", Flag);
            p.Add("P_VEHL_NO", carno);
            DataSet ds = _svc.GetQuerySP("SP_MU_CAR_LIMIT_CHK", p);

            if (ds != null)
            {
                rtn = ds.Tables[0].Rows[0]["LIMIT_YN"].ToString();
            }

            return rtn;
        }
        #endregion


        #region 자동 공차 하면서 2차 계량 하기 PROCEDURE : SP_MU_WEIGHT_FIX_CAR_2CH_SET
        public static String Fix_Car_2ch(Int32 DOWN_WEIGHT, string rfid_no, string weight_fg, string item_so, string up_area, string down_area)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            String rtn = "1";
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_RFID_NO", rfid_no);
            p.Add("P_DOWN_WEIGHT", DOWN_WEIGHT.ToString());

            p.Add("P_STATE", weight_fg);
            p.Add("P_UP_AREA", up_area);
            p.Add("P_DOWN_AREA", down_area);
            p.Add("P_ITEM_SO", item_so);
            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_FIX_CAR_2CH_SET", p);

            if (ds != null)
            {
                rtn = ds.Tables[0].Rows[0]["ERRMSG"].ToString();
            }

            return rtn;
        }
        #endregion

        #region 계량표 출력 매수 확인
        public static DataTable pnt_qty(string gubun, string item_dae, string item_jung, string item_so, string rfid_seq)
        {
            ServiceAdapter _svc = new ServiceAdapter();

            // 소분류 출력 프로시져 호출
            // 2016-09-30 배차번호 추가 조용호
            // 웹배차에서 출력매수 입력된 항목 가져오기
            Dictionary<string, string> p = new Dictionary<string, string>();
            //프로시저 신규생성으로 파라미터 수정(2020-02-12 오창휘 수정))
            p.Add("P_SEQ_NO", rfid_seq);
            //p.Add("P_GUBUN", gubun);
            ////p.Add("P_ITEM_DAE", item_dae);
            //p.Add("P_ITEM_JUNG", item_jung);
            //p.Add("P_ITEM_SO", item_so);
            //p.Add("P_RFID_SEQ", rfid_seq);
            

            //원자재용출력양식 추가로 인해 프로시저 추가(2021-05-27 정성호 수정)
            DataSet ds = _svc.GetQuerySP("SP_MU_PRINT_SO_NEW2", p);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 계량표 출력 매수 확인TEST
        public static DataTable pnt_qty2(string gubun, string item_dae, string item_jung, string item_so, string rfid_seq)
        {
            ServiceAdapter _svc = new ServiceAdapter();

            // 소분류 출력 프로시져 호출
            // 2016-09-30 배차번호 추가 조용호
            // 웹배차에서 출력매수 입력된 항목 가져오기
            Dictionary<string, string> p = new Dictionary<string, string>();
            //프로시저 신규생성으로 파라미터 수정(2020-02-12 오창휘 수정))
            p.Add("P_SEQ_NO", rfid_seq);
            //p.Add("P_GUBUN", gubun);
            ////p.Add("P_ITEM_DAE", item_dae);
            //p.Add("P_ITEM_JUNG", item_jung);
            //p.Add("P_ITEM_SO", item_so);
            //p.Add("P_RFID_SEQ", rfid_seq);
            DataSet ds = _svc.GetQuerySP("SP_MU_PRINT_SO_NEW2", p);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region LPR Return value Update 2-2 table 실적테이블에 계량 차량 번호 Update

        public static DataTable updatevehlno(string vehl_no)
        {
            ServiceAdapter _svc = new ServiceAdapter();

            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_VEHL_NO", vehl_no);
            
            DataSet ds = _svc.GetQuerySP("SP_TB_WS02_0002_U", p);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region 계량표 출력
        public static DataTable pnt(string gubun, string rfid_seq, string wght_no, string item_so)
        {
            ServiceAdapter _svc = new ServiceAdapter();

            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_FG", "PNT");
            p.Add("P_GUBUN", gubun);
            p.Add("P_ITEM_JUNG", "");
            p.Add("P_RFID_SEQ", rfid_seq);
            p.Add("P_WGHT_NO", wght_no);
            p.Add("P_ITEM_SO", item_so);
            DataSet ds = _svc.GetQuerySP("SP_MU_PRINT", p);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 차량번호로 RFID 조회
        public static DataTable SearchRFID(string VEHL_NO)
        {
            ServiceAdapter _svc = new ServiceAdapter();

            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_VEHL_NO", VEHL_NO);

            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_CAR_150511R", p);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 배차정보에서 RFID_SEQ 조회

        public static DataTable _ExDataSet(string sp_name, string start_date, string end_date)
        {
            ServiceAdapter _svc = new ServiceAdapter();

            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_START_DATE", start_date);
            p.Add("P_END_DATE", end_date);

            DataSet ds = _svc.GetQuerySP(sp_name, p);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region 1차 계량 완료후 행선지 표시하기 위해 하차지 정보 배차정보 테이블에서 가져오기 20190910 김상우

        public static DataTable getDownSite(string sp_name, string start_date, string end_date, string itemso)
        {
            ServiceAdapter _svc = new ServiceAdapter();

            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_START_DATE", start_date);
            p.Add("P_END_DATE", end_date);
            p.Add("V_ITEM_SO", itemso);

            DataSet ds = _svc.GetQuerySP(sp_name, p);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region 배차정보 유무 - 일반 계량
        public static DataTable FINDVEHLNO(string VEHL_NO)
        {
            String rtn = "1";
            ServiceAdapter _svc = new ServiceAdapter();

            Dictionary<string, string> p = new Dictionary<string, string>();

            p.Add("P_RFID_NO", VEHL_NO);
            DataSet ds = _svc.GetQuerySP("SP_TB_WS02_0001_V", p);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 배차정보 유무 - 사내이송
        public static DataTable FINDVEHLNO2(string VEHL_NO)
        {
            //2019-12-28 대한제강
            //배차카운트
            //String Query = " SELECT COUNT(A.CARD_NO) AS CNT FROM WMS_MEASURE_RST A WHERE A.CARD_NO = '"+VEHL_NO+"' AND OUT_WGT_DT IS NULL ";
            //ServiceAdapter _svc = new ServiceAdapter();
            //DataSet ds = _svc.GetQuery(Query);
            String rtn = "1";
            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_RFID_NO", VEHL_NO);
            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_RECEPT_CHK", p);
            //프로시져명 변경(2020-01-21 대한제강)
            //DataSet ds = _svc.GetQuerySP("SP_TB_WS02_0001_V", p); 

            //DBLK_LIVE는 조회 후 링크 기능을 종료 해야 함(2020-03-12 한민호)
            String Query1 = "COMMIT ";
            _svc.GetQuery(Query1);
            String Query2 = "ALTER SESSION CLOSE DATABASE LINK DBLK_LIVE ";
            _svc.GetQuery(Query2);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 크로스 체크 2019-10-04 김상우 추가
        public static DataTable CrossCheck(string VEHL_NO, string sp_name)
        {
            String rtn = "1";
            ServiceAdapter _svc = new ServiceAdapter();

            Dictionary<string, string> p = new Dictionary<string, string>();

            p.Add("P_RFID_NO", VEHL_NO);
            DataSet ds = _svc.GetQuerySP(sp_name, p);

            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region LPR 데이터 배차에 Update //2019-10-04 김상우 추가
        public static string updatevehlno(string rfid_seq, string vehl_no, string img_vehl_no, string img_file_Nm, string sp_name)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("V_RFID_SEQ", rfid_seq); // 배차번호
            p.Add("V_VEHL_NO", vehl_no); // 차량번호
            p.Add("V_IMG_VEHL_NO", img_vehl_no); // LPR차량번호
            p.Add("V_IMG_FILE_NM", img_file_Nm); // LPR 스틸컷 경로
            string ret = _svc.SetQuerySP(sp_name, p);
            return ret;
            //.GetQuerySP(sp_name, p);
            /*
            if (ret != "-1")
            {
                return ret;
            }
            else
            {
                return "";
            }
            */
        }
        #endregion

        #region LPR 데이터 배차에 Update //2019-10-11 오창휘 추가
        public static string deletevehlno(string rfid_seq)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("V_RFID_SEQ", rfid_seq); // 차량번호
            string ret = _svc.SetQuerySP("SP_TB_WS02_0002_VD", p);
            return ret;
        }
        #endregion

        #region 배차순서 체크 PROCEDURE : SP_MU_ENT_CAR_SCH_TM_CHK
        public static String enter_car_search(string P_RFID_SEQ, string P_RFID_NO)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            String rtn = "Y";

            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_RFID_SEQ", P_RFID_SEQ);
            p.Add("P_RFID_NO", P_RFID_NO);
            DataSet ds = _svc.GetQuerySP("SP_MU_ENT_CAR_SCH_TM_CHK", p);

            if (ds != null)
            {
                rtn = ds.Tables[0].Rows[0]["CHK_YN"].ToString();
            }

            return rtn;
        }
        #endregion

        #region 국내고철 하차지 안내 문자 전송 
        public static string sendSMS(string rfid_seq)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_RFID_SEQ", rfid_seq); // 배차번호
            string ret = _svc.SetQuerySP("SP_MU_SEND_SMS", p);
            return ret;
        }
        #endregion

        #region 국내고철 계량 완료 후 계량값 문자 전송
        public static string sendSMS2(string rfid_seq)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_RFID_SEQ", rfid_seq); // 배차번호
            string ret = _svc.SetQuerySP("SP_MU_SEND_SMS2", p);
            return ret;
        }
        #endregion

        #region 카메라정보 조회 PROCEDURE : SP_MU_CAMERA_INFO_R (2020-01-29 오창휘 추가)
        public static DataTable Camera_Info(string WEIGHT_NO)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_WEIGHT_NO", WEIGHT_NO);
            DataSet ds = _svc.GetQuerySP("SP_MU_CAMERA_INFO_R", p);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
