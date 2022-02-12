using System;
using System.Collections.Generic;
using System.Data;

namespace DK_WEIGHT.AutoWeight
{
    public static class DB_Process
    {
     
        #region 오늘일자 계량 제한 체크
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

        #region 계량대 명칭 확인 
        public static String weight_name(string area_code)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            String rtn = "Y";

            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_TYPE_CD", "006");
            p.Add("P_CODE", area_code);
            DataSet ds = _svc.GetQuerySP("SP_MU_COMMON_R", p);

            if (ds != null)
            {
                rtn = ds.Tables[0].Rows[0]["TITLE"].ToString();
            }

            return rtn;
        }
        #endregion

        #region off_find 발생고철 출문 조회 
        public static DataTable off_find(string rfid)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("V_RFID_NO", rfid); 

            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_OFF_R", p);

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

        #region off_save 발생고철 출문 저장 
        public static bool off_save(string wght_no)
        {
            String rtn = "1";

            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("V_WGHT_NO", wght_no);

            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_OFF_SET", p);

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

        #region Start_rfid RFID 카드 조회 
        public static DataTable Start_rfid(string rfid)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_RFID_CARD", rfid);
            p.Add("P_VEHL_NO", "");

            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_ALL_150511R", p);

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

        #region 슬라브 정보 찾기 
        public static DataTable slab_find(string rfid)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_RFID_CARD", rfid); 

            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_D02_R", p);

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

        #region Fix_car 고정 차량 확인
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

        #region 공차 계량 등록 
        public static bool Gong_insert(string rfid, Int32 LOAD_WEIGHT, string fg, string weight_state)
        {
            //fg : 1- 배차 정보 없이 처리 ,그외- 배차 시퀀스 
            ServiceAdapter _svc = new ServiceAdapter();
            String rtn = "1";

            Dictionary<string, string> p = new Dictionary<string, string>();

            p.Add("P_RFID_CARD", rfid);
            p.Add("P_STATE", fg);
            p.Add("P_WEIGHT_FG", weight_state);
            p.Add("P_LOAD_WEIGHT", LOAD_WEIGHT.ToString());
            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_FIX_CAR_SET", p);

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

        #region 차량번호 계량 제한 체크
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
 
        #region 수입 빌렛 2차 계량 
        public static bool Billet_02_set(string wght_no, string state, Int32 down_wght)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            String rtn = "1";
            Dictionary<string, string> p = new Dictionary<string, string>();

            p.Add("P_WGHT_NO", wght_no);
            p.Add("P_STATE", state);
            p.Add("P_DOWN_WEIGHT", down_wght.ToString());
            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_BILLET_02", p);

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

        #region 수입 빌렛 1차 계량
        public static bool Billet_01_set(string rfid_seq, string rfid_no, string state, Int32 down_wght)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            String rtn = "1";
            Dictionary<string, string> p = new Dictionary<string, string>();

            p.Add("P_RFID_SEQ", rfid_seq);
            p.Add("P_RFID_NO", rfid_no);
            p.Add("P_STATE", state);
            p.Add("P_LOAD_WEIGHT", down_wght.ToString());
            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_BILLET_01", p);

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

        #region 계량처리
        public static bool weight_fg(String kind, String area, String gubun, Int32 LOAD_WEIGHT, String RFID_SEQ, String wght_no, string state)
        {
            bool rtn = false;

            if (gubun == "N")
            {
                rtn = insert_first(kind, area, LOAD_WEIGHT, RFID_SEQ, state);
            }

            if (gubun == "Y")
            {
                rtn = update_second(kind, area, LOAD_WEIGHT, wght_no, state);
            }

            if (gubun == "K")
            {
                rtn = update_kumsu(kind, area, LOAD_WEIGHT, wght_no, state);
            }

            return rtn;
        }
        #endregion

        #region insert_first 1차 계량 등록
        private static bool insert_first(string weight_kind, string araa, Int32 LOAD_WEIGHT, String RFID_SEQ, string state)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            String rtn = "1";

            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("V_AREA", araa);
            p.Add("V_STATE", state);
            
            p.Add("V_RFID_SEQ", RFID_SEQ);
            p.Add("V_LOAD_WEIGHT", LOAD_WEIGHT.ToString());
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

        #region update_second 2차 계량 등록(실행 순서 2)
        private static bool update_second(string weight_kind, string araa, Int32 LOAD_WEIGHT, string wght_no, string state)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            String rtn = "1";

            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("V_AREA", araa);
            p.Add("V_STATE", state);
            p.Add("V_WGHT_NO", wght_no);
            p.Add("V_LOAD_WEIGHT", LOAD_WEIGHT.ToString());
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

        #region update_kumsu 검수 계량 등록(실행 순서 2)
        private static bool update_kumsu(string weight_kind, string araa, Int32 LOAD_WEIGHT, string wght_no, string state)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            String rtn = "1";

            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("V_STATE", state);
            p.Add("V_WGHT_NO", wght_no);
            p.Add("V_DOWN_WEIGHT", LOAD_WEIGHT.ToString());
            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_03_SET", p);

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

        #region 자동 공차 하면서 2차 계량 하기 
        public static bool Fix_Car_2ch(string rfid_seq, Int32 DOWN_WEIGHT, string rfid_no, string weight_fg)
        { 
            ServiceAdapter _svc = new ServiceAdapter();
            String rtn = "1"; 
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_RFID_SEQ", rfid_seq);
            p.Add("P_RFID_NO", rfid_no);
            p.Add("P_DOWN_WEIGHT", DOWN_WEIGHT.ToString());
            p.Add("P_STATE", weight_fg);
            DataSet ds = _svc.GetQuerySP("SP_MU_WEIGHT_FIX_CAR_2CH_SET", p);

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

        #region 계량표 출력 매수 확인
        public static DataTable pnt_qty(string gubun, string item_dae,  string item_jung, string item_so, string rfid_seq)
        {
            ServiceAdapter _svc = new ServiceAdapter();

            // 기존 프로시져 호출
            //Dictionary<string, string> p = new Dictionary<string, string>();
            //p.Add("P_FG", "ALL");
            //p.Add("P_GUBUN", gubun);
            //p.Add("P_ITEM_JUNG", item_jung);
            //p.Add("P_RFID_SEQ", "");
            //p.Add("P_WGHT_NO", "");
            //p.Add("P_ITEM_SO", "");
            //DataSet ds = _svc.GetQuerySP("SP_MU_PRINT", p);

            // 소분류 출력 프로시져 호출
            // 2016-09-30 배차번호 추가 조용호
            // 웹배차에서 출력매수 입력된 항목 가져오기
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_GUBUN", gubun);
            p.Add("P_ITEM_DAE", item_dae);
            p.Add("P_ITEM_JUNG", item_jung);
            p.Add("P_ITEM_SO", item_so);
            p.Add("P_RFID_SEQ", rfid_seq);
            DataSet ds = _svc.GetQuerySP("SP_MU_PRINT_SO_NEW", p);
 
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

        #region 계량표 잔량 확인
        public static Int32 pnt_per(string op_code, string op_seq)
        {
            ServiceAdapter _svc = new ServiceAdapter();
            Int32 rtn = 100;

            Dictionary<string, string> p = new Dictionary<string, string>();

            p.Add("P_OP_CODE", op_code);
            p.Add("P_OP_SEQ", op_seq);
            DataSet ds = _svc.GetQuerySP("SP_MU_PRINT_STATE", p);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rtn = Convert.ToInt32(ds.Tables[0].Rows[0]["PRINT_PER"].ToString());
                }
            }
            return rtn;
        }
        #endregion

        #region 슬라브 지시 확인 
        public static Int32 slab_cnt()
        {
            ServiceAdapter _svc = new ServiceAdapter();
            Int32 rtn = 100;

            Dictionary<string, string> p = new Dictionary<string, string>();

            p.Add("P_PARAM", "");
            DataSet ds = _svc.GetQuerySP("SP_MU_SLAB_TIME_R", p);

            if (ds != null)
            {
                rtn = Convert.ToInt32(ds.Tables[0].Rows[0]["CNT"].ToString());
            }
            return rtn;
        }
        #endregion

        #region 올바로 출력
        public static DataTable pnt_all(string rfid_seq)
        {
            ServiceAdapter _svc = new ServiceAdapter();

            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("P_RFID_SEQ", rfid_seq);
            DataSet ds = _svc.GetQuerySP("SP_ALLBARO_PNT", p);

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
    }
}
