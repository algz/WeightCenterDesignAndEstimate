using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;
using XCommon;
using System.Reflection;


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

        static PubSyswareCom()
        {
            try
            {
                //要获取的类型的 ProgID。 
                string progID = "Sysware.SyswareSDK.SyswareCom.SyswareComServer";

                //与指定 ProgID 关联的类型，即获取相应的Com对象
                System.Type comObjectName = System.Type.GetTypeFromProgID(progID);

                if (comObjectName != null)
                {
                    //通过类型创建对象实例
                    SyswareObj = Activator.CreateInstance(comObjectName);

                    ////设置需要设置的参数值
                    //args[0] = true;
                    ////设置可视属性，显示Word窗体
                    //comObjectName.InvokeMember("Visible", BindingFlags.SetProperty, null, comObject, args);
                    //object t = SyswareObj.GetType().InvokeMember("IsRuntimeServerStarted", BindingFlags.InvokeMethod, null, SyswareObj, new Object[] { });

                }
            }
            catch (Exception e)
            {
                XLog.Write("Sysware COM组件创建失败." + e.Message);
            }
        }

        /// <summary>
        /// 获得SyswareObject。
        /// </summary>
        //private static void mGetSyswareObject()
        //{
        //    try{

        //        if (SyswareObj == null)
        //            SyswareObj = Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");
        //        object t1 = Interaction.CallByName(SyswareObj, "IsRuntimeServerStarted", CallType.Method, new Object[] { });
        //    }
        //    catch(Exception e)
        //    {
        //        XLog.TextBoxLog.Text = "Sysware COM组件创建失败." + e.Message;
        //    }
        //}
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
                //mGetSyswareObject();

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
                //mGetSyswareObject();

                // 错误消息
                String errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    
                    // 设置参数的值（该种方法的限制是无法通过ref获取到errorMsg消息）
                    //Interaction.CallByName(SyswareObj, "IsRuntimeServerStarted", CallType.Method,
                    //        new Object[] { });
                     return (bool)SyswareObj.GetType().InvokeMember("IsRuntimeServerStarted", BindingFlags.InvokeMethod, null, SyswareObj, null);
                    //return true;
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
               // mGetSyswareObject();
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
                //mGetSyswareObject();
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
                //mGetSyswareObject();
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
                //mGetSyswareObject();
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
                //mGetSyswareObject();
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
                //mGetSyswareObject();
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
                //mGetSyswareObject();
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
                //mGetSyswareObject();

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
                //mGetSyswareObject();

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
                //mGetSyswareObject();

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

        #region 设置参数单位的值
        /// <summary>
        /// 获取指定参数的单位
        /// </summary>
        /// <param name="_Context">模板路径</param>
        /// <param name="_Name">参数名称</param>
        /// <returns>单位的值</returns>
        public static string GetParamterUnit(String _Context, String _Name)
        {
            try
            {
                //mGetSyswareObject();

                // 设置上下文路径
                String context = _Context;

                // 设置参数名
                String paraName = _Name;

                if (SyswareObj != null)
                {
                    // 设置参数的值（该种方法的限制是无法通过ref获取到errorMsg消息）
                    object obj = Interaction.CallByName(SyswareObj, "GetParamterUnit", CallType.Method,
                              new Object[] { context, paraName });

                    return obj.ToString();
                }
                else
                    return string.Empty;
            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
                return string.Empty;
            }
        }

        #endregion

        #region 设置分组

        public static bool SetParameterGroup(string paraName, string groupName)
        {
            try
            {
                //mGetSyswareObject();

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
                //mGetSyswareObject();

                // 设置上下文路径
                String context = _Context;

                // 错误消息
                String errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    ret= SyswareObj.GetType().InvokeMember("GetParameterNames", BindingFlags.InvokeMethod, null, SyswareObj, new Object[] { context, errorMsg });

            
                    // 获取参数的值（该种方法的限制是无法通过ref获取到errorMsg消息）
                    //ret = Interaction.CallByName(SyswareObj, "GetParameterNames", CallType.Method,
                    //        new Object[] { context, errorMsg });
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
                //mGetSyswareObject();

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

        #region 获取参数的值
        /// <summary>
        /// 获取参数的值
        /// </summary>
        /// <param name="context">上下文（默认空字符串）</param>
        /// <param name="name">参数名</param>
        /// <returns></returns>
        public static Object GetParameter(String context, String name)
        {
            try
            {
                if (SyswareObj != null)
                {
                    return Interaction.CallByName(SyswareObj, "GetParameter", CallType.Method,
                        new object[] { context, name, String.Empty });
                }
            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
            }
            return null;
        }
        #endregion

        #region 设置参数的值
        /// <summary>
        /// 设置参数的值
        /// </summary>
        /// <param name="context">上下文（默认空字符串）</param>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        public static void SetParameter(String context, String name, Object value)
        {
            try
            {
                if (SyswareObj != null)
                {
                    Object obj = Interaction.CallByName(SyswareObj, "SetParameter", CallType.Method,
                        new object[] { context, name, value, String.Empty });
                }
            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
            }
        }
        #endregion

        #region 获取数组参数的值
        /// <summary>
        /// 获取数组参数的值
        /// </summary>
        /// <param name="context">上下文（默认空字符串）</param>
        /// <param name="name">参数名</param>
        /// <returns></returns>
        public static Object GetArrayParameter(String context, String name)
        {
            try
            {
                if (SyswareObj != null)
                {
                    return Interaction.CallByName(SyswareObj, "GetArrayParameter", CallType.Method,
                        new object[] { context, name, String.Empty });
                }
            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
            }
            return null;
        }
        #endregion

        #region 设置数组参数的值
        /// <summary>
        /// 设置数组参数的值
        /// </summary>
        /// <param name="context">上下文（默认空字符串）</param>
        /// <param name="name">参数名</param>
        /// <param name="array">数组对象</param>
        public static void SetArrayParameter(String context, String name, Object array)
        {
            try
            {
                if (SyswareObj != null)
                {
                    Interaction.CallByName(SyswareObj, "SetArrayParameter", CallType.Method,
                        new object[] { context, name, array, String.Empty });
                }
            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
            }
        }
        #endregion

        #region 打开模板接口

        /// <summary>
        /// 打开模板到内存中
        /// </summary>
        /// <param name="templateFilePath">模板文件路径(*.ide)</param>
        /// <returns></returns>
        public static Boolean OpenTemplate(String templateFilePath)
        {
            try
            {
                //mGetSyswareObject();

                if (SyswareObj != null)
                {
                    //此方法适用于TDE-IDE未启动的情况调用
                    object result = (object)Interaction.CallByName(SyswareObj, "OpenTemplate", CallType.Method,
                        new object[] { templateFilePath });
                    return Convert.ToBoolean(result);
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 关闭内存中打开的模板

        /// <summary>
        ///  关闭内存中打开的模板
        /// </summary>
        /// <param name="templateFilePath">模板文件路径(*.ide)</param>
        /// <returns></returns>
        public static void CloseTemplate(String templateFilePath)
        {
            try
            {
                //mGetSyswareObject();

                if (SyswareObj != null)
                {
                    //此方法适用于TDE-IDE未启动的情况调用
                    Interaction.CallByName(SyswareObj, "CloseTemplate", CallType.Method,
                        new object[] { templateFilePath });
                }

            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
            }
        }

        #endregion

        #region 自动化执行模板（不弹出模板GUI界面执行）
        /// <summary>
        /// void AutoExecuteTemplate(String templateFilePath, ref String errorMsg)
        /// 功能：自动化执行模板（不弹出模板GUI界面执行）
        /// 参数：
        /// templateFilePath：模板文件路径(*.ide)
        /// errorMsg：输出的错误消息
        /// </summary>
        /// <param name="templateFilePath">模板文件路径(*.tde，*.ide)</param>
        /// <param name="errorMsg"></param>
        public static void AutoExecuteTemplate(string templateFilePath)
        {
            try
            {
                //mGetSyswareObject();

                // 错误消息
                string errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    // 自动化执行模板（不弹出模板GUI界面执行）（该种方法的限制是无法通过ref获取到errorMsg消息）
                    Interaction.CallByName(SyswareObj, "AutoExecuteTemplate", CallType.Method,
                            new object[] { templateFilePath, errorMsg });
                }
                else
                    throw new Exception("运行‘自动化执行模板’失败");
            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
            }
        }
        #endregion

        #region 创建结构体参数

        /// <summary>
        /// 创建结构体参数
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <param name="strType"></param>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="gui"></param>
        public static void CreateStructParameter(String context, String name, String strType, Boolean input, Boolean output, Boolean gui)
        {
            try
            {
                //mGetSyswareObject();

                // 错误消息
                string errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    Interaction.CallByName(SyswareObj, "CreateStructParameter", CallType.Method,
                            new object[] { context, name, strType, input, output, gui });
                }
            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
            }
        }

        #endregion

        #region  解析指定的单位，返回单位标识

        /// <summary>
        /// 解析指定的单位，返回单位标识
        /// </summary>
        /// <param name="unitName">单位名称</param>
        public static string ParseUnit(String unitName)
        {
            try
            {
                //mGetSyswareObject();

                // 错误消息
                string errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    object obj = Interaction.CallByName(SyswareObj, "ParseUnit", CallType.Method,
                             new object[] { unitName });
                    return obj.ToString();
                }

            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
            }

            return string.Empty;
        }

        #endregion

        #region 设置参数描述信息

        /// <summary>
        /// 设置参数描述信息
        /// </summary>
        /// <param name="context">环境上下文</param>
        /// <param name="name">参数名</param>
        /// <param name="description">描述信息</param>
        public static void SetParameterDescription(String context, String name, String description)
        {
            try
            {
                //mGetSyswareObject();

                // 错误消息
                string errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    Interaction.CallByName(SyswareObj, "SetParameterDescription", CallType.Method,
                             new object[] { context, name, description });
                }

            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
            }
        }

        #endregion

        #region 结构化参数接口

        /// <summary>
        /// 添加（或者合并）结构化参数
        /// </summary>
        /// <returns></returns>
        public static bool AddDataObject(string path)
        {
            try
            {
                if (SyswareObj != null&&path!=null)
                {
                    return (bool)SyswareObj.GetType().InvokeMember("AddDataObject", BindingFlags.InvokeMethod, null, SyswareObj, new Object[] { "", path });
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 修改结构化参数属性值 节点层级间用点"."
        /// </summary>
        /// <returns></returns>
        public static bool UpdateDataObjectProperty(string path)
        {
            try
            {
                if (SyswareObj != null)
                {
                    return (bool)SyswareObj.GetType().InvokeMember("UpdateDataObjectProperty", BindingFlags.InvokeMethod, null, SyswareObj, new Object[] { "", path });
                }
                else
                    return false;
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 删除结构化参数
        /// </summary>
        /// <returns></returns>
        public static bool RemoveDataObject()
        {
            try
            {

                // 错误消息
                String errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    // 设置参数的值（该种方法的限制是无法通过ref获取到errorMsg消息）
                    Interaction.CallByName(SyswareObj, "RemoveDataObject", CallType.Method,
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

        /// <summary>
        /// 添加（或者合并）子结构化参数
        /// </summary>
        /// <returns></returns>
        public static bool AddChildDataObject(string subNode,string path)
        {
            try
            {
                if (SyswareObj != null && path != null)
                {
                    object bl=SyswareObj.GetType().InvokeMember("AddChildDataObject", BindingFlags.InvokeMethod, null, SyswareObj, new Object[] { "",subNode, path });
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