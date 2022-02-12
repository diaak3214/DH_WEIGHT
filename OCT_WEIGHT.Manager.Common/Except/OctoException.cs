// ***************************************************************
//  KASException   version:  1.1
// ***************************************************************
//  created:	2006/07/23 16:00
//  author:		장기원
//
//  purpose:	예외가 발생했을때 이 클래스의 새 인스턴스를 throw합니다
// ***************************************************************

using System;
using System.Runtime.Serialization;

namespace OCT_WEIGHT.Manager.Common.Except
{
    /// <summary>
    /// 예외가 발생했을때 이 클래스의 새 인스턴스를 throw합니다
    /// </summary>
    /// <remarks>
    /// 사용예 : throw new KASExcpetion(msg, this.GetType().ToString(), ex);
    /// </remarks>
    public sealed class OctoException : Exception
    {
        #region Private Member
        #endregion

        #region Properties
        #endregion

        #region Constructor
        /// <summary>
        /// 생성자
        /// </summary>
        public OctoException()
            : base("알 수 없는 예외가 발생하였습니다")
        {
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="message">예외 메세지</param>
        public OctoException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="message">예외 메세지</param>
        /// <param name="source">예외 발생 클래스 이름</param>
        public OctoException(string message, string source)
            : base(message)
        {
            this.Source = source;
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="message">예외 메세지</param>
        /// <param name="innerException">내부 예외</param>
        public OctoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="message">예외 메세지</param>
        /// <param name="source">예외 발생 클래스 이름</param>
        /// <param name="innerException">내부 예외</param>
        public OctoException(string message, string source, Exception innerException)
            : base(message, innerException)
        {
            this.Source = source;
        }

        private OctoException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion

        /// <summary>
        /// 예외를 설명하는 형식화된 문자열을 반환합니다
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "[예외메세지] " + this.Message;
            /*
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("[예외메세지] " + this.Message);
                sb.AppendLine("\r\n[예외발생모듈] " + this.Source);
                if (InnerException != null)
                    sb.AppendLine("\r\n[내부예외] " + this.InnerException.ToString());

                sb.AppendLine("\r\n[Stack Trace] " + this.StackTrace);

                return sb.ToString();
             * */
        }

    }
}
