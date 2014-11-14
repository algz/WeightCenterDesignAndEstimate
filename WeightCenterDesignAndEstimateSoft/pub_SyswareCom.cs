using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;
using XCommon;
using System.Reflection;


namespace Dev.PubLib
{
    /// <summary>
    /// Sysware Com�ӿڵ�C#��װ��
    /// ���õ��� Microsoft.VisualBasic�ķ�����
    /// �޸Ľӿڣ���֧��2.8
    /// </summary>
    public class PubSyswareCom
    {
        #region ����
        private static string m_ErrMsg = string.Empty;
        private PubSyswareCom() { }
        #endregion

        #region ���syswareObject
        private static object SyswareObj = null;

        static PubSyswareCom()
        {
            try
            {
                //Ҫ��ȡ�����͵� ProgID�� 
                string progID = "Sysware.SyswareSDK.SyswareCom.SyswareComServer";

                //��ָ�� ProgID ���������ͣ�����ȡ��Ӧ��Com����
                System.Type comObjectName = System.Type.GetTypeFromProgID(progID);

                if (comObjectName != null)
                {
                    //ͨ�����ʹ�������ʵ��
                    SyswareObj = Activator.CreateInstance(comObjectName);

                    ////������Ҫ���õĲ���ֵ
                    //args[0] = true;
                    ////���ÿ������ԣ���ʾWord����
                    //comObjectName.InvokeMember("Visible", BindingFlags.SetProperty, null, comObject, args);
                    //object t = SyswareObj.GetType().InvokeMember("IsRuntimeServerStarted", BindingFlags.InvokeMethod, null, SyswareObj, new Object[] { });

                }
            }
            catch (Exception e)
            {
                XLog.Write("Sysware COM�������ʧ��." + e.Message);
            }
        }

        /// <summary>
        /// ���SyswareObject��
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
        //        XLog.TextBoxLog.Text = "Sysware COM�������ʧ��." + e.Message;
        //    }
        //}
        #endregion

        #region ��ȡ������Ϣ
        /// <summary>
        /// ��������������󣬿���ʹ�øú�����ȡ������Ϣ��
        /// ÿ��������Ϣֻ�ܻ��һ�Σ�ȡ�ߴ�����Ϣ�Ժ󣬴�����Ϣ�ַ�������Ϊ�ա�
        /// </summary>
        /// <returns></returns>
        public static string mGetErrorMessage()
        {
            string temp = m_ErrMsg;
            m_ErrMsg = "";
            return temp;
        }
        #endregion

        #region Post������Ϣ
        /// <summary>
        /// ����Ϣ��ʾ��sysware����־�����У����Ҹ���_stop��ʾ�Ƿ�ֹͣ��
        /// </summary>
        /// <param name="_str">Ҫ��ʾ����Ϣ</param>
        /// <param name="_stop">�Ƿ�ֹͣ</param>
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

                // ������Ϣ
                String errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    
                    // ���ò�����ֵ�����ַ������������޷�ͨ��ref��ȡ��errorMsg��Ϣ��
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

        #region ����������Com�ӿ�
        #region ������������
        /// <summary>
        /// Boolean CreateIntParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
        /// ���ܣ����������Ͳ���
        /// ������
        /// name����������
        /// value������ֵ
        /// input���Ƿ���Ϊ�������
        /// output���Ƿ���Ϊ�������
        /// gui���Ƿ���ΪGUI����
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="value">����ֵ</param>
        /// <param name="input">�Ƿ���Ϊ�������</param>
        /// <param name="output">�Ƿ���Ϊ�������</param>
        /// <param name="gui">�Ƿ���ΪGUI����</param>
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
            catch { m_ErrMsg = "������������ʧ�ܡ�"; return false; }
            return true;
        }
        #endregion
        #region ����ʵ������
        /// <summary>
        /// Boolean CreateDoubleParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
        /// ���ܣ�����ʵ���Ͳ���
        /// ������
        /// name����������
        /// value������ֵ
        /// input���Ƿ���Ϊ�������
        /// output���Ƿ���Ϊ�������
        /// gui���Ƿ���ΪGUI����
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="value">����ֵ</param>
        /// <param name="input">�Ƿ���Ϊ�������</param>
        /// <param name="output">�Ƿ���Ϊ�������</param>
        /// <param name="gui">�Ƿ���ΪGUI����</param>
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
            catch { m_ErrMsg = "����ʵ������ʧ�ܡ�"; return false; }
            return true;
        }
        #endregion
        #region �����ַ�������
        /// <summary>
        /// Boolean CreateStringParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
        /// ���ܣ������ַ����Ͳ���
        /// ������
        /// name����������
        /// value������ֵ
        /// input���Ƿ���Ϊ�������
        /// output���Ƿ���Ϊ�������
        /// gui���Ƿ���ΪGUI����
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="value">����ֵ</param>
        /// <param name="input">�Ƿ���Ϊ�������</param>
        /// <param name="output">�Ƿ���Ϊ�������</param>
        /// <param name="gui">�Ƿ���ΪGUI����</param>
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
            catch { m_ErrMsg = "�����ַ�������ʧ�ܡ�"; return false; }
            return true;
        }
        #endregion
        #region ����Boolean����
        /// <summary>
        /// Boolean CreateBooleanParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
        /// ���ܣ�����Bool�Ͳ���
        /// ������
        /// name����������
        /// value������ֵ
        /// input���Ƿ���Ϊ�������
        /// output���Ƿ���Ϊ�������
        /// gui���Ƿ���ΪGUI����
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="value">����ֵ</param>
        /// <param name="input">�Ƿ���Ϊ�������</param>
        /// <param name="output">�Ƿ���Ϊ�������</param>
        /// <param name="gui">�Ƿ���ΪGUI����</param>
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
            catch { m_ErrMsg = "�����ַ�������ʧ�ܡ�"; return false; }
            return true;
        }
        #endregion
        #region ���������������
        /// <summary>
        /// Boolean CreateIntArrayParameter (String name, Object value, Boolean input, Boolean output, Boolean gui)
        /// ���ܣ����������������
        /// ������
        /// name����������
        /// value������ֵ
        /// input���Ƿ���Ϊ�������
        /// output���Ƿ���Ϊ�������
        /// gui���Ƿ���ΪGUI����
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="value">����ֵ</param>
        /// <param name="input">�Ƿ���Ϊ�������</param>
        /// <param name="output">�Ƿ���Ϊ�������</param>
        /// <param name="gui">�Ƿ���ΪGUI����</param>
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
            catch { m_ErrMsg = "���������������ʧ�ܡ�"; return false; }
            return true;
        }
        #endregion
        #region ����ʵ�����������
        /// <summary>
        /// Boolean CreateDoubleArrayParameter  (String name, Object value, Boolean input, Boolean output, Boolean gui)
        /// ���ܣ�����ʵ�����������
        /// ������
        /// name����������
        /// value������ֵ
        /// input���Ƿ���Ϊ�������
        /// output���Ƿ���Ϊ�������
        /// gui���Ƿ���ΪGUI����
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="value">����ֵ</param>
        /// <param name="input">�Ƿ���Ϊ�������</param>
        /// <param name="output">�Ƿ���Ϊ�������</param>
        /// <param name="gui">�Ƿ���ΪGUI����</param>
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
            catch { m_ErrMsg = "����ʵ�����������ʧ�ܡ�"; return false; }
            return true;
        }
        #endregion
        #region �����ַ����������
        /// <summary>
        /// Boolean CreateStringArrayParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
        /// ���ܣ������ַ����������
        /// ������
        /// name����������
        /// value������ֵ
        /// input���Ƿ���Ϊ�������
        /// output���Ƿ���Ϊ�������
        /// gui���Ƿ���ΪGUI����
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="value">����ֵ</param>
        /// <param name="input">�Ƿ���Ϊ�������</param>
        /// <param name="output">�Ƿ���Ϊ�������</param>
        /// <param name="gui">�Ƿ���ΪGUI����</param>
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
            catch { m_ErrMsg = "�����ַ����������ʧ�ܡ�"; return false; }
            return true;
        }
        #endregion
        #endregion  ��������������

        #region ��ȡ������ֵ
        /// <summary>
        /// ��ȡ������ֵ
        /// </summary>
        /// <param name="_Context">ģ��·��</param>
        /// <param name="_Name">��������</param>
        /// <param name="ret">������ֵ</param>
        /// <returns>True ���� False</returns>
        public static bool mGetParameter(String _Context, String _Name, ref Object ret)
        {
            try
            {
                //mGetSyswareObject();

                // ����������·��
                String context = _Context;

                // ���ò�����
                String paraName = _Name;

                // ������Ϣ
                String errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    // ��ȡ������ֵ�����ַ������������޷�ͨ��ref��ȡ��errorMsg��Ϣ��
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

        #region ���ò�����ֵ
        /// <summary>
        /// ���ò�����ֵ
        /// </summary>
        /// <param name="_Context">ģ��·��</param>
        /// <param name="_Name">��������</param>
        /// <param name="value">������ֵ</param>
        /// <returns>True ���� False</returns>
        public static bool mSetParameter(String _Context, String _Name, Object value)
        {
            try
            {
                //mGetSyswareObject();

                // ����������·��
                String context = _Context;

                // ���ò�����
                String paraName = _Name;

                // ������Ϣ
                String errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    // ���ò�����ֵ�����ַ������������޷�ͨ��ref��ȡ��errorMsg��Ϣ��
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

        #region ���ò�����λ��ֵ
        /// <summary>
        /// ���ò�����λ��ֵ
        /// </summary>
        /// <param name="_Context">ģ��·��</param>
        /// <param name="_Name">��������</param>
        /// <param name="value">������λ��ֵ</param>
        /// <returns>True ���� False</returns>
        public static bool SetParameterUnit(String _Context, String _Name, Object value)
        {
            try
            {
                //mGetSyswareObject();

                // ����������·��
                String context = _Context;

                // ���ò�����
                String paraName = _Name;

                if (SyswareObj != null)
                {
                    // ���ò�����ֵ�����ַ������������޷�ͨ��ref��ȡ��errorMsg��Ϣ��
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

        #region ���ò�����λ��ֵ
        /// <summary>
        /// ��ȡָ�������ĵ�λ
        /// </summary>
        /// <param name="_Context">ģ��·��</param>
        /// <param name="_Name">��������</param>
        /// <returns>��λ��ֵ</returns>
        public static string GetParamterUnit(String _Context, String _Name)
        {
            try
            {
                //mGetSyswareObject();

                // ����������·��
                String context = _Context;

                // ���ò�����
                String paraName = _Name;

                if (SyswareObj != null)
                {
                    // ���ò�����ֵ�����ַ������������޷�ͨ��ref��ȡ��errorMsg��Ϣ��
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

        #region ���÷���

        public static bool SetParameterGroup(string paraName, string groupName)
        {
            try
            {
                //mGetSyswareObject();

                if (SyswareObj != null)
                {
                    // ���ò�����ֵ�����ַ������������޷�ͨ��ref��ȡ��errorMsg��Ϣ��
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

        #region ��ȡ���в������Ƶ�����

        /// <summary>
        /// ��ȡ���в������Ƶ�����
        /// </summary>
        /// <param name="_Context"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public static bool GetParameterNames(string _Context, ref Object ret)
        {
            try
            {
                //mGetSyswareObject();

                // ����������·��
                String context = _Context;

                // ������Ϣ
                String errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    ret= SyswareObj.GetType().InvokeMember("GetParameterNames", BindingFlags.InvokeMethod, null, SyswareObj, new Object[] { context, errorMsg });

            
                    // ��ȡ������ֵ�����ַ������������޷�ͨ��ref��ȡ��errorMsg��Ϣ��
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

        #region ɾ������

        public static bool DeleteParameter(String _Context, String _Name)
        {
            try
            {
                //mGetSyswareObject();

                // ����������·��
                String context = _Context;

                // ������Ϣ
                String errorMsg = String.Empty;
                if (SyswareObj != null)
                {
                    // ���ò�����ֵ�����ַ������������޷�ͨ��ref��ȡ��errorMsg��Ϣ��
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

        #region ��ȡ������ֵ
        /// <summary>
        /// ��ȡ������ֵ
        /// </summary>
        /// <param name="context">�����ģ�Ĭ�Ͽ��ַ�����</param>
        /// <param name="name">������</param>
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

        #region ���ò�����ֵ
        /// <summary>
        /// ���ò�����ֵ
        /// </summary>
        /// <param name="context">�����ģ�Ĭ�Ͽ��ַ�����</param>
        /// <param name="name">������</param>
        /// <param name="value">����ֵ</param>
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

        #region ��ȡ���������ֵ
        /// <summary>
        /// ��ȡ���������ֵ
        /// </summary>
        /// <param name="context">�����ģ�Ĭ�Ͽ��ַ�����</param>
        /// <param name="name">������</param>
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

        #region �������������ֵ
        /// <summary>
        /// �������������ֵ
        /// </summary>
        /// <param name="context">�����ģ�Ĭ�Ͽ��ַ�����</param>
        /// <param name="name">������</param>
        /// <param name="array">�������</param>
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

        #region ��ģ��ӿ�

        /// <summary>
        /// ��ģ�嵽�ڴ���
        /// </summary>
        /// <param name="templateFilePath">ģ���ļ�·��(*.ide)</param>
        /// <returns></returns>
        public static Boolean OpenTemplate(String templateFilePath)
        {
            try
            {
                //mGetSyswareObject();

                if (SyswareObj != null)
                {
                    //�˷���������TDE-IDEδ�������������
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

        #region �ر��ڴ��д򿪵�ģ��

        /// <summary>
        ///  �ر��ڴ��д򿪵�ģ��
        /// </summary>
        /// <param name="templateFilePath">ģ���ļ�·��(*.ide)</param>
        /// <returns></returns>
        public static void CloseTemplate(String templateFilePath)
        {
            try
            {
                //mGetSyswareObject();

                if (SyswareObj != null)
                {
                    //�˷���������TDE-IDEδ�������������
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

        #region �Զ���ִ��ģ�壨������ģ��GUI����ִ�У�
        /// <summary>
        /// void AutoExecuteTemplate(String templateFilePath, ref String errorMsg)
        /// ���ܣ��Զ���ִ��ģ�壨������ģ��GUI����ִ�У�
        /// ������
        /// templateFilePath��ģ���ļ�·��(*.ide)
        /// errorMsg������Ĵ�����Ϣ
        /// </summary>
        /// <param name="templateFilePath">ģ���ļ�·��(*.tde��*.ide)</param>
        /// <param name="errorMsg"></param>
        public static void AutoExecuteTemplate(string templateFilePath)
        {
            try
            {
                //mGetSyswareObject();

                // ������Ϣ
                string errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    // �Զ���ִ��ģ�壨������ģ��GUI����ִ�У������ַ������������޷�ͨ��ref��ȡ��errorMsg��Ϣ��
                    Interaction.CallByName(SyswareObj, "AutoExecuteTemplate", CallType.Method,
                            new object[] { templateFilePath, errorMsg });
                }
                else
                    throw new Exception("���С��Զ���ִ��ģ�塯ʧ��");
            }
            catch (System.Exception ex)
            {
                m_ErrMsg = ex.Message;
            }
        }
        #endregion

        #region �����ṹ�����

        /// <summary>
        /// �����ṹ�����
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

                // ������Ϣ
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

        #region  ����ָ���ĵ�λ�����ص�λ��ʶ

        /// <summary>
        /// ����ָ���ĵ�λ�����ص�λ��ʶ
        /// </summary>
        /// <param name="unitName">��λ����</param>
        public static string ParseUnit(String unitName)
        {
            try
            {
                //mGetSyswareObject();

                // ������Ϣ
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

        #region ���ò���������Ϣ

        /// <summary>
        /// ���ò���������Ϣ
        /// </summary>
        /// <param name="context">����������</param>
        /// <param name="name">������</param>
        /// <param name="description">������Ϣ</param>
        public static void SetParameterDescription(String context, String name, String description)
        {
            try
            {
                //mGetSyswareObject();

                // ������Ϣ
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

        #region �ṹ�������ӿ�

        /// <summary>
        /// ��ӣ����ߺϲ����ṹ������
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
        /// �޸Ľṹ����������ֵ �ڵ�㼶���õ�"."
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
        /// ɾ���ṹ������
        /// </summary>
        /// <returns></returns>
        public static bool RemoveDataObject()
        {
            try
            {

                // ������Ϣ
                String errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    // ���ò�����ֵ�����ַ������������޷�ͨ��ref��ȡ��errorMsg��Ϣ��
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
        /// ��ӣ����ߺϲ����ӽṹ������
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