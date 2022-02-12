// ***************************************************************
//  Enums   version:  1.0
// ***************************************************************
//  created:	2009/08/26
//  author:		박지호
//
//  purpose:	OctoCommon 모듈에서 사용하는 열거형 정의
// ***************************************************************

namespace OCT_WEIGHT.Manager.Common.info
{
    /// <summary>
    /// octoHealer의 타입을 나타냅니다
    /// </summary>
    public enum HelpType
    {
        /// <summary>
        /// 사용자 
        /// </summary>
        USER,
        /// <summary>
        /// 전체거래처
        /// </summary>
        CUST,
        /// <summary>
        /// 이동유형
        /// </summary>
        MOVETYPE,
        /// <summary>
        /// 이동사유
        /// </summary>
        MOVEREASON,
        /// <summary>
        /// 특별재고지시자
        /// </summary>
        SPSTOCK,
        /// <summary>
        /// 창고
        /// </summary>
        PLANT,
        /// <summary>
        /// 코스트센터
        /// </summary>
        COSTCENTER,
        /// <summary>
        /// 품목
        /// </summary>
        ITEM,
        /// <summary>
        /// 입고예정
        /// </summary>
        ENTER,
        /// <summary>
        /// 제품 입고예정
        /// </summary>
        ENTER_GW,
        /// <summary>
        /// 출고예정
        /// </summary>
        OUT,
        /// <summary>
        /// 창고간이전
        /// </summary>
        WAREHOUSE,
        /// <summary>
        /// SAP창고정보
        /// </summary>
        SAPLOC,
        /// <summary>
        /// 기타이동
        /// </summary>
        MOVETYPE_ETC,
        // <summary>
        /// 기타이동
        /// </summary>
        MOVETYPE_ETC_A,
        // <summary>
        /// 기타이동
        /// </summary>
        MOVETYPE_ETC_B,
        // <summary>
        /// 기타이동
        /// </summary>
        MOVETYPE_ETC_C,
        /// <summary>
        /// 공급처
        /// </summary>
        SUPPLY,
        /// <summary>
        /// 사외창고
        /// </summary>
        WMSSTORAGE,
        /// <summary>
        /// 구매오더
        /// </summary>
        ORDER,
        /// <summary>
        /// 작업지시
        /// </summary>
        WORK,
        /// <summary>
        /// 예약입고
        /// </summary>
        RESERVE_IN,
        /// <summary>
        /// 공장간입고
        /// </summary>
        MOVE_IN,
        /// <summary>
        /// 외자입고
        /// </summary>
        IMPORT,
        /// <summary>
        /// 라인피딩
        /// </summary>
        LINE,
        /// <summary>
        /// 출하
        /// </summary>
        SHIP,
        /// <summary>
        /// 예약출고
        /// </summary>
        RESERVE_OUT,
        /// <summary>
        /// 공장간출고
        /// </summary>
        MOVE_OUT
    }

    /// <summary>
    /// octobutton의 타입을 나타냅니다
    /// </summary>
    public enum CRUDType
    {
        C,
        /// <summary>
        /// 입력가능
        /// </summary>
        U,
        /// <summary>
        /// 수정가능
        /// </summary>
        D,
        /// <summary>
        /// 삭제가능
        /// </summary>
        R,
        /// <summary>
        /// 조회가능
        /// </summary>
        P
        /// <summary>
        /// 출력가능
        /// </summary>
    }

    /// <summary>
    /// Y, N 나타내는 열거형
    /// </summary>
    public enum YNFlag
    {
        /// <summary>
        /// ALL
        /// </summary>
        ALL,
        /// <summary>
        /// Y
        /// </summary>
        Y,
        /// <summary>
        /// N
        /// </summary>
        N
    }
}
