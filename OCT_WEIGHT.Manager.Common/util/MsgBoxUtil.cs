using System.Windows.Forms;

namespace OCT_WEIGHT.Manager.Common.util
{
    /// <summary>
    /// 메세지 박스를 보여주는 유틸리티 메서드 집합 클래스
    /// </summary>
    public static class MsgBoxUtil
    {
        /// <summary>
        /// 입력란에 빈값을 넣었을 경우 메세지박스를 보여줍니다
        /// </summary>
        /// <param name="fieldName">입력란 이름</param>
        public static void AlertTbxEmpty(string fieldName)
        {
            string caption = "알림";
            string msg = "에 값을 입력하세요";

            //if (Properties.AppSettings.Default.Language == "E")
            //{
            //    caption = "Alert";
            //    msg = " item must input";
            //}

            MessageBox.Show(fieldName + msg, caption);
        }

        /// <summary>
        /// Gets the MSGS.
        /// </summary>
        /// <param name="msg_code">The msg_code.</param>
        /// <returns></returns>
        private static string GetMsgs(string msg_code)
        {
            //ServiceAdapter _svc = new ServiceAdapter();
            //RequestMessage _qMsg = new RequestMessage();
            //ResponseMessage _rMsg = new ResponseMessage();

            string rcode = "";

            //ServiceParamCollection p1 = new ServiceParamCollection();
            //p1.AddParam("CODE", msg_code);

            //if (Properties.AppSettings.Default.Language == "E")
            //    _qMsg.AddSpService("COM_MESSAGES.GET_ENG_MSG", p1, RFX.Common.Data.DbCommandType.ExecuteNonQuery);
            //else _qMsg.AddSpService("COM_MESSAGES.GET_KOR_MSG", p1, RFX.Common.Data.DbCommandType.ExecuteNonQuery);

            //_rMsg = _svc.Execute(_qMsg);
            //try
            //{
            //    if (_rMsg.Successed)
            //    {
            //        rcode = _rMsg.Services[0].OutPutParamRows[0][0].Value.ToString();
            //    }
            //}
            //finally
            //{
            //}

            if (msg_code.Equals("MAINCLOSE"))
            {
                rcode = "프로그램을 종료 하시겠습니까?";
            }
            return rcode;
        }

        /// <summary>
        /// 일반적인 알림 메세지를 보여줍니다
        /// </summary>
        /// <param name="msg">메세지</param>
        public static void AlertInformation(string msg)
        {
            string caption = "알림";

            //if (Properties.AppSettings.Default.Language == "E")
            //    caption = "Notice";

            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 일반적인 Error 메세지를 보여줍니다
        /// </summary>
        /// <param name="msg">메세지</param>
        public static void AlertError(string msgs)
        {
            string caption = "Error";

            string[] Msg = msgs.Split('\n');

            MessageBox.Show(Msg[0], caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 일반적인 경고 메세지를 보여줍니다
        /// </summary>
        /// <param name="msg">메세지</param>
        public static void AlertWarning(string msgs)
        {
            string caption = "경고";

            string[] Msg = msgs.Split('\n');

            MessageBox.Show(Msg[0], caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Alerts the information.
        /// </summary>
        /// <param name="msgcode">The msgcode.</param>
        /// <param name="arg">메세지 템플릿의 "%AX%" 부분에 순서대로 들어갈 문자열의 배열</param>
        public static void AlertInformation(string msgcode, string[] arg)
        {
            string cap = "";
            string msgs = "";
            int cnt = 0;

            if (arg != null) cnt = arg.Length;

            cap = GetMsgs(msgcode);
            for (int jj = 0; jj < cnt; jj++)
            {
                cap = cap.Replace("%A" + jj.ToString() + "%", arg[jj]);
            }
            msgs = cap;

            string caption = "알림";
            //if (Properties.AppSettings.Default.Language == "E") caption = "Notice";

            MessageBox.Show(msgs, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// OK Cancel 버튼을 가진 질문 확인 대화상자를 표시합니다
        /// </summary>
        /// <param name="msg">질문 내용</param>
        /// <returns>다이얼로그 결과</returns>
        public static DialogResult AlertQuestion(string msg)
        {
            string caption = "확인";

            // if (Properties.AppSettings.Default.Language == "E") caption = "Confirm";
            return MessageBox.Show(msg, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Alerts the question.
        /// </summary>
        /// <param name="msgcode">The msgcode.</param>
        /// <param name="arg">메세지 템플릿의 "%AX%" 부분에 순서대로 들어갈 문자열의 배열</param>
        /// <returns></returns>
        public static DialogResult AlertQuestion(string msgcode, string[] arg)
        {
            string cap = "";
            string msgs = "";
            int cnt = 0;

            if (arg != null) cnt = arg.Length;

            cap = GetMsgs(msgcode);
            for (int jj = 0; jj < cnt; jj++)
            {
                cap = cap.Replace("%A" + jj.ToString() + "%", arg[jj]);
            }
            msgs = cap;

            string caption = "확인";

            // if (Properties.AppSettings.Default.Language == "E") caption = "Confirm";

            return MessageBox.Show(msgs, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Alerts the question.
        /// </summary>
        /// <param name="msgcode">The msgcode.</param>
        /// <param name="arg">메세지 템플릿의 "%AX%" 부분에 순서대로 들어갈 문자열의 배열</param>
        /// <param name="btns"><c>MessageBoxButton</c> 유형</param>
        /// <returns><c>DialogResult</c></returns>
        public static DialogResult AlertQuestion(string msgcode, string[] arg, MessageBoxButtons btns)
        {
            string cap = "";
            string msgs = "";
            int cnt = 0;

            if (arg != null) cnt = arg.Length;

            cap = GetMsgs(msgcode);
            for (int jj = 0; jj < cnt; jj++)
            {
                cap = cap.Replace("%A" + jj.ToString() + "%", arg[jj]);
            }
            msgs = cap;

            string caption = "확인";

            // if (Properties.AppSettings.Default.Language == "E") caption = "Confirm";

            return MessageBox.Show(msgs, caption, btns, MessageBoxIcon.Question);
        }
        /// <summary>
        /// OK Cancel 버튼을 가진 삭제 확인 대화상자를 표시합니다
        /// </summary>
        /// <returns>다이얼로그 결과</returns>
        public static DialogResult ConfirmDelete()
        {
            string caption = "확인";
            string msg = "삭제하시겠습니까?";

            //if (Properties.AppSettings.Default.Language == "E")
            //{
            //    caption = "Confirm";
            //    msg = "Do you want to delete?";
            //}

            return MessageBox.Show(msg, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        /// <summary>
        /// 입려된 MAWB#, HAWB#를 비교하여 AWB BASE 를 판단한다
        /// </summary>
        /// <param name="mawbNo">The mawb no.</param>
        /// <param name="hawbNo">The hawb no.</param>
        /// <param name="awbNo">The awb no.</param>
        /// <returns>AWB Type Enums</returns>
        //public static AWBType CheckAwbType(string mawbNo, string hawbNo, out string awbNo)
        //{
        //    AWBType awbDiv = AWBType.M;
        //    awbNo = "";

        //    if (mawbNo.Trim() != "")
        //    {
        //        if (hawbNo.Trim() == "")
        //        {
        //            awbNo = mawbNo;
        //            awbDiv = AWBType.M;
        //        }
        //        else
        //        {
        //            awbNo = hawbNo;
        //            awbDiv = AWBType.H;
        //        }
        //    }
        //    else
        //    {
        //        MsgBoxUtil.AlertTbxEmpty("MAWB#");
        //    }

        //    return awbDiv;
        //}
    }
}
