using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;

namespace Dev.PubLib
{
    /// <summary>
    /// Sysware Com接口的C#封装。
    /// 采用的是 Microsoft.VisualBasic的方法。
    /// 修改接口，仅支持2.8
    /// </summary>
    public class PubSyswareCom
    {
        #region 变量
        private static string m_ErrMsg = string.Empty;
        private PubSyswareCom() { }
        #endregion

        #region 获得syswareObject
        private static object SyswareObj = null;
        /// <summary>
        /// 获得SyswareObject。
        /// </summary>
        private static void mGetSyswareObject()
        {
            if (SyswareObj == null)
                SyswareObj = Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");
        }
        #endregion

        #region 获取错误信息
        /// <summary>
        /// 如果函数发生错误，可以使用该函数获取错误信息。
        /// 每个错误信息只能获得一次，取走错误信息以后，错误信息字符串被置为空。
        /// </summary>
        /// <returns></returns>
        public static string mGetErrorMessage()
        {
            string temp = m_ErrMsg;
            m_ErrMsg = "";
            return temp;
        }
        #endregion

        #region Post出错信息
        /// <summary>
        /// 将信息显示到sysware的日志窗口中，并且根据_stop显示是否停止。
        /// </summary>
        /// <param name="_str">要显示的信息</param>
        /// <param name="_stop">是否停止</param>
        public static void mPostErrorMsg(string _str, bool _stop)
        {
            try
            {
                Object SyswareObj =
                    Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");

                if (SyswareObj != null)
                {
                    Interaction.CallByName(SyswareObj, "PostErrorMessage", CallType.Method,
                                    new Object[] { _str, _stop });
                }
            }
            catch { }
        }
        #endregion

        public static bool IsRuntimeServerStarted()
        {
            try
            {
                Object SyswareObj =
                    Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");


                // 错误消息
                String errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    // 设置参数的值（该种方法的限制是无法通过ref获取到errorMsg消息）
                    Interaction.CallByName(SyswareObj, "IsRuntimeServerStarted", CallType.Method,
                            new Object[] { });
                    return true;
                }
                else
                    return false;
            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
                return false;
            }
        }

        #region 创建参数的Com接口
        #region 创建整数参数
        /// <summary>
        /// Boolean CreateIntParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
        /// 功能：创建整数型参数
        /// 参数：
        /// name：参数名称
        /// value：参数值
        /// input：是否作为输入参数
        /// output：是否作为输出参数
        /// gui：是否作为GUI参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="input">是否作为输入参数</param>
        /// <param name="output">是否作为输出参数</param>
        /// <param name="gui">是否作为GUI参数</param>
        /// <returns></returns>
        public static bool CreateIntParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
        {
            try
            {
                mGetSyswareObject();
                if (SyswareObj != null)
                {
                    Interaction.CallByName(SyswareObj, "CreateIntParameter", CallType.Method,
                                    new object[] { name, value, input, output, gui });
                }
            }
            catch { m_ErrMsg = "建立整数参数失败。"; return false; }
            return true;
        }
        #endregion
        #region 创建实数参数
        /// <summary>
        /// Boolean CreateDoubleParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
        /// 功能：创建实数型参数
        /// 参数：
        /// name：参数名称
        /// value：参数值
        /// input：是否作为输入参数
        /// output：是否作为输出参数
        /// gui：是否作为GUI参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="input">是否作为输入参数</param>
        /// <param name="output">是否作为输出参数</param>
        /// <param name="gui">是否作为GUI参数</param>
        /// <returns></returns>
        public static bool CreateDoubleParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
        {
            try
            {
                mGetSyswareObject();
                if (SyswareObj != null)
                {
                    Interaction.CallByName(SyswareObj, "CreateDoubleParameter", CallType.Method,
                                    new object[] { name, value, input, output, gui });
                }
            }
            catch { m_ErrMsg = "建立实数参数失败。"; return false; }
            return true;
        }
        #endregion
        #region 创建字符串参数
        /// <summary>
        /// Boolean CreateStringParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
        /// 功能：创建字符串型参数
        /// 参数：
        /// name：参数名称
        /// value：参数值
        /// input：是否作为输入参数
        /// output：是否作为输出参数
        /// gui：是否作为GUI参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="input">是否作为输入参数</param>
        /// <param name="output">是否作为输出参数</param>
        /// <param name="gui">是否作为GUI参数</param>
        /// <returns></returns>
        public static bool CreateStringParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
        {
            try
            {
                mGetSyswareObject();
                if (SyswareObj != null)
                {
                    Interaction.CallByName(SyswareObj, "CreateStringParameter", CallType.Method,
                                    new object[] { name, value, input, output, gui });
                }
            }
            catch { m_ErrMsg = "建立字符串参数失败。"; return false; }
            return true;
        }
        #endregion
        #region 创建Boolean参数
        /// <summary>
        /// Boolean CreateBooleanParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
        /// 功能：创建Bool型参数
        /// 参数：
        /// name：参数名称
        /// value：参数值
        /// input：是否作为输入参数
        /// output：是否作为输出参数
        /// gui：是否作为GUI参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="input">是否作为输入参数</param>
        /// <param name="output">是否作为输出参数</param>
        /// <param name="gui">是否作为GUI参数</param>
        /// <returns></returns>
        public static bool CreateBooleanParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
        {
            try
            {
                mGetSyswareObject();
                if (SyswareObj != null)
                {
                    Interaction.CallByName(SyswareObj, "CreateBooleanParameter", CallType.Method,
                                    new object[] { name, value, input, output, gui });
                }
            }
            catch { m_ErrMsg = "建立字符串参数失败。"; return false; }
            return true;
        }
        #endregion
        #region 创建整型数组参数
        /// <summary>
        /// Boolean CreateIntArrayParameter (String name, Object value, Boolean input, Boolean output, Boolean gui)
        /// 功能：创建整型数组参数
        /// 参数：
        /// name：参数名称
        /// value：参数值
        /// input：是否作为输入参数
        /// output：是否作为输出参数
        /// gui：是否作为GUI参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="input">是否作为输入参数</param>
        /// <param name="output">是否作为输出参数</param>
        /// <param name="gui">是否作为GUI参数</param>
        /// <returns></returns>
        public static bool CreateIntArrayParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
        {
            try
            {
                mGetSyswareObject();
                if (SyswareObj != null)
                {
                    Interaction.CallByName(SyswareObj, "CreateIntArrayParameter", CallType.Method,
                                    new object[] { name, value, input, output, gui });
                }
            }
            catch { m_ErrMsg = "建立整型数组参数失败。"; return false; }
            return true;
        }
        #endregion
        #region 创建实数型数组参数
        /// <summary>
        /// Boolean CreateDoubleArrayParameter  (String name, Object value, Boolean input, Boolean output, Boolean gui)
        /// 功能：创建实数型数组参数
        /// 参数：
        /// name：参数名称
        /// value：参数值
        /// input：是否作为输入参数
        /// output：是否作为输出参数
        /// gui：是否作为GUI参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="input">是否作为输入参数</param>
        /// <param name="output">是否作为输出参数</param>
        /// <param name="gui">是否作为GUI参数</param>
        /// <returns></returns>
        public static bool CreateDoubleArrayParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
        {
            try
            {
                mGetSyswareObject();
                if (SyswareObj != null)
                {
                    Interaction.CallByName(SyswareObj, "CreateDoubleArrayParameter ", CallType.Method,
                                    new object[] { name, value, input, output, gui });
                }
            }
            catch { m_ErrMsg = "建立实数型数组参数失败。"; return false; }
            return true;
        }
        #endregion
        #region 创建字符串数组参数
        /// <summary>
        /// Boolean CreateStringArrayParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
        /// 功能：创建字符串数组参数
        /// 参数：
        /// name：参数名称
        /// value：参数值
        /// input：是否作为输入参数
        /// output：是否作为输出参数
        /// gui：是否作为GUI参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="input">是否作为输入参数</param>
        /// <param name="output">是否作为输出参数</param>
        /// <param name="gui">是否作为GUI参数</param>
        /// <returns></returns>
        public static bool CreateStringArrayParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
        {
            try
            {
                mGetSyswareObject();
                if (SyswareObj != null)
                {
                    Interaction.CallByName(SyswareObj, "CreateStringArrayParameter", CallType.Method,
                                    new object[] { name, value, input, output, gui });
                }
            }
            catch { m_ErrMsg = "建立字符串数组参数失败。"; return false; }
            return true;
        }
        #endregion
        #endregion  结束，创建参数

        #region 获取参数的值
        /// <summary>
        /// 获取参数的值
        /// </summary>
        /// <param name="_Context">模板路径</param>
        /// <param name="_Name">参数名称</param>
        /// <param name="ret">参数的值</param>
        /// <returns>True 或者 False</returns>
        public static bool mGetParameter(String _Context, String _Name, ref Object ret)
        {
            try
            {
                Object SyswareObj =
                    Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");

                // 设置上下文路径
                String context = _Context;

                // 设置参数名
                String paraName = _Name;

                // 错误消息
                String errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    // 获取参数的值（该种方法的限制是无法通过ref获取到errorMsg消息）
                    ret = Interaction.CallByName(SyswareObj, "GetParameter", CallType.Method,
                            new Object[] { context, paraName, errorMsg });
                    return true;
                }
                else
                    return false;
            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
                return false;
            }
        }
        #endregion

        #region 设置参数的值
        /// <summary>
        /// 设置参数的值
        /// </summary>
        /// <param name="_Context">模板路径</param>
        /// <param name="_Name">参数名称</param>
        /// <param name="value">参数的值</param>
        /// <returns>True 或者 False</returns>
        public static bool mSetParameter(String _Context, String _Name, Object value)
        {
            try
            {
                Object SyswareObj =
                    Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");

                // 设置上下文路径
                String context = _Context;

                // 设置参数名
                String paraName = _Name;

                // 错误消息
                String errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    // 设置参数的值（该种方法的限制是无法通过ref获取到errorMsg消息）
                    Interaction.CallByName(SyswareObj, "SetParameter", CallType.Method,
                            new Object[] { context, paraName, value, errorMsg });
                    return true;
                }
                else
                    return false;
            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
                return false;
            }
        }
        #endregion

        #region 设置参数单位的值
        /// <summary>
        /// 设置参数单位的值
        /// </summary>
        /// <param name="_Context">模板路径</param>
        /// <param name="_Name">参数名称</param>
        /// <param name="value">参数单位的值</param>
        /// <returns>True 或者 False</returns>
        public static bool SetParameterUnit(String _Context, String _Name, Object value)
        {
            try
            {
                Object SyswareObj =
                    Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");

                // 设置上下文路径
                String context = _Context;

                // 设置参数名
                String paraName = _Name;

                if (SyswareObj != null)
                {
                    // 设置参数的值（该种方法的限制是无法通过ref获取到errorMsg消息）
                    Interaction.CallByName(SyswareObj, "SetParameterUnit", CallType.Method,
                            new Object[] { context, paraName, value });
                    return true;
                }
                else
                    return false;
            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
                return false;
            }
        }

        #endregion

        #region 设置分组

        public static bool SetParameterGroup(string paraName, string groupName)
        {
            try
            {
                Object SyswareObj =
                    Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");

                if (SyswareObj != null)
                {
                    // 设置参数的值（该种方法的限制是无法通过ref获取到errorMsg消息）
                    Interaction.CallByName(SyswareObj, "SetParameterGroup", CallType.Method,
                            new Object[] { paraName, groupName });
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
                return false;
            }
        }

        #endregion

        #region 获取所有参数名称的数组

        /// <summary>
        /// 获取所有参数名称的数组
        /// </summary>
        /// <param name="_Context"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public static bool GetParameterNames(string _Context, ref Object ret)
        {
            try
            {
                Object SyswareObj =
                    Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");

                // 设置上下文路径
                String context = _Context;

                // 错误消息
                String errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    // 获取参数的值（该种方法的限制是无法通过ref获取到errorMsg消息）
                    ret = Interaction.CallByName(SyswareObj, "GetParameterNames", CallType.Method,
                            new Object[] { context, errorMsg });
                    return true;
                }
                else
                    return false;
            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
                return false;
            }
        }
        #endregion

        #region 删除参数

        public static bool DeleteParameter(String _Context, String _Name)
        {
            try
            {
                Object SyswareObj =
                    Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");

                // 设置上下文路径
                String context = _Context;

                // 错误消息
                String errorMsg = String.Empty;
                if (SyswareObj != null)
                {
                    // 设置参数的值（该种方法的限制是无法通过ref获取到errorMsg消息）
                    Interaction.CallByName(SyswareObj, "DeleteParameter", CallType.Method,
                            new Object[] { context, _Name });
                    return true;
                }
                else
                    return false;
            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
                return false;
            }
        }

        #endregion
    }
}