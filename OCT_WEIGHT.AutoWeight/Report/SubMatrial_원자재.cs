using System.Data;
//네트워크 프린트 출력(2020-03-28 한민호)
using System.Management;
using System.Drawing.Printing;
using System.IO;


namespace OCT_WEIGHT.AutoWeight.Report
{
    public partial class SubMatrial_원자재 : DevExpress.XtraReports.UI.XtraReport
    {
        public SubMatrial_원자재(DataTable dt)
        {
            //PrintDocument pd = new PrintDocument();
            //pd.PrinterSettings.PrinterName = "120011";

            InitializeComponent();
            DataSet ds = new DataSet();
            DataTable Rep_dt = dt.Copy();
            Rep_dt.TableName =  "PRINT";
            ds.Tables.Add(Rep_dt);
            //품명이 원자재, 수입원자재 일 경우 입차중량, 출차중량 위치 변경(2020-04-20 한민호)
            string item_type_mm = dt.Rows[0]["ITEM_TYPE_NM"].ToString();
            if (item_type_mm == "원자재" || item_type_mm == "수입원자재" || item_type_mm == "이송입고" || item_type_mm == "부자재/저장품" || item_type_mm == "수입부/저장품")
            {
                xrTableCell13.Text = "입차중량";
                xrTableCell15.Text = "출차중량";
            }
            else
            {
                xrTableCell13.Text = "출차중량";
                xrTableCell15.Text = "입차중량";
            }

            //대한제강 필드 수정(2020-02-10 오창휘 수정)
            xrTableCell2.DataBindings.Add("Text", ds, ds.Tables[0].Columns["MEA_DATE"].ColumnName);         // 년월일
            xrTableCell9.DataBindings.Add("Text", ds, ds.Tables[0].Columns["MEA_SEQ"].ColumnName);         // 입차순번
            xrTableCell4.DataBindings.Add("Text", ds, ds.Tables[0].Columns["CAR_NO"].ColumnName);           // 차량번호 
            xrTableCell6.DataBindings.Add("Text", ds, ds.Tables[0].Columns["VENDOR_NM"].ColumnName);        // 회사
            xrTableCell8.DataBindings.Add("Text", ds, ds.Tables[0].Columns["ITEM_TYPE_NM"].ColumnName);     // 품명
            xrTableCell12.DataBindings.Add("Text", ds, ds.Tables[0].Columns["ITEM_NM"].ColumnName);         // 등급
            xrTableCell14.DataBindings.Add("Text", ds, ds.Tables[0].Columns["IN_WGT_DT"].ColumnName);       // 1차시간
            xrTableCell39.DataBindings.Add("Text", ds, ds.Tables[0].Columns["IN_WGT"].ColumnName);          // 1차중량
            xrTableCell16.DataBindings.Add("Text", ds, ds.Tables[0].Columns["OUT_WGT_DT"].ColumnName);      // 2차시간
            xrTableCell40.DataBindings.Add("Text", ds, ds.Tables[0].Columns["OUT_WGT"].ColumnName);         // 2차중량
            xrTableCell18.DataBindings.Add("Text", ds, ds.Tables[0].Columns["REAL_WGT"].ColumnName);        // 실중량

            //대한제강 필드추가 수정(2021-05-18 정성호 수정)
            tableCell2.DataBindings.Add("Text", ds, ds.Tables[0].Columns["REDUCE_WG"].ColumnName); //감량중량
            tableCell3.DataBindings.Add("Text", ds, ds.Tables[0].Columns["CONFIRM_WEIGHT"].ColumnName); //감량후중량
            xrTableCell24.DataBindings.Add("Text", ds, ds.Tables[0].Columns["REMARK2"].ColumnName);          // 비고

            //xrTableCell30.DataBindings.Add("Text", ds, ds.Tables[0].Columns["INOUT_GUBUN"].ColumnName);    // 구분
            //xrTableCell28.DataBindings.Add("Text", ds, ds.Tables[0].Columns["DOWN_STATE"].ColumnName);     // 계근번호
            //xrTableCell2.DataBindings.Add("Text", ds, ds.Tables[0].Columns["RFID_NO"].ColumnName);         // 년월일
            //xrTableCell4.DataBindings.Add("Text", ds, ds.Tables[0].Columns["VEHL_NO"].ColumnName);         // 차량번호 
            ////위치 수정(2019-11-08 한민호)
            //xrTableCell6.DataBindings.Add("Text", ds, ds.Tables[0].Columns["CUST_CD"].ColumnName);         // 거래처
            //xrTableCell8.DataBindings.Add("Text", ds, ds.Tables[0].Columns["ITEM_SO"].ColumnName);        // 품명
            //xrTableCell10.DataBindings.Add("Text", ds, ds.Tables[0].Columns["QTY"].ColumnName);            // 수량
            //xrTableCell12.DataBindings.Add("Text", ds, ds.Tables[0].Columns["STANDARD"].ColumnName);       // 규격
            //xrTableCell14.DataBindings.Add("Text", ds, ds.Tables[0].Columns["TOTAL_WEIGHT"].ColumnName);   // 총중량/시간
            //xrTableCell16.DataBindings.Add("Text", ds, ds.Tables[0].Columns["EMPTY_WEIGHT"].ColumnName);   // 공차량/시간
            //xrTableCell18.DataBindings.Add("Text", ds, ds.Tables[0].Columns["DOWN_WEIGHT"].ColumnName);    // 계근중량
            //xrTableCell20.DataBindings.Add("Text", ds, ds.Tables[0].Columns["REDUCE_WG"].ColumnName);      // 감량
            //xrTableCell22.DataBindings.Add("Text", ds, ds.Tables[0].Columns["CONFIRM_WEIGHT"].ColumnName); // 확정중량
            ////비고(계근대) 추가(2019-11-30 한민호)
            //xrTableCell24.DataBindings.Add("Text", ds, ds.Tables[0].Columns["REMARK"].ColumnName);         // 비고
            //xrTableCell8.DataBindings.Add("Text", ds, ds.Tables[0].Columns["CUST_CD"].ColumnName);         // 거래처
            //xrTableCell10.DataBindings.Add("Text", ds, ds.Tables[0].Columns["ITEM_SO"].ColumnName);        // 품명
            //xrTableCell12.DataBindings.Add("Text", ds, ds.Tables[0].Columns["QTY"].ColumnName);            // 수량
            //xrTableCell14.DataBindings.Add("Text", ds, ds.Tables[0].Columns["STANDARD"].ColumnName);       // 규격
            //xrTableCell16.DataBindings.Add("Text", ds, ds.Tables[0].Columns["TOTAL_WEIGHT"].ColumnName);   // 총중량/시간
            //xrTableCell18.DataBindings.Add("Text", ds, ds.Tables[0].Columns["EMPTY_WEIGHT"].ColumnName);   // 공차량/시간
            //xrTableCell20.DataBindings.Add("Text", ds, ds.Tables[0].Columns["DOWN_WEIGHT"].ColumnName);    // 계근중량
            //xrTableCell22.DataBindings.Add("Text", ds, ds.Tables[0].Columns["REDUCE_WG"].ColumnName);      // 감량
            //xrTableCell22.DataBindings.Add("Text", ds, ds.Tables[0].Columns["CONFIRM_WEIGHT"].ColumnName); // 확정중량
            //xrTableCell22.DataBindings.Add("Text", ds, ds.Tables[0].Columns["REMARK"].ColumnName);         // 비고
            //xrTableCell22.DataBindings.Add("Text", ds, ds.Tables[0].Columns["CRT_USER"].ColumnName);       // 계근자
        }         
    }
}