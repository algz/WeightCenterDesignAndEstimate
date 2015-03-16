using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;

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
        /// <summary>
        /// ���SyswareObject��
        /// </summary>
        private static void mGetSyswareObject()
        {
            if (SyswareObj == null)
                SyswareObj = Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");
        }
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


                // ������Ϣ
                String errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    // ���ò�����ֵ�����ַ������������޷�ͨ��ref��ȡ��errorMsg��Ϣ��
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
                mGetSyswareObject();
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
                mGetSyswareObject();
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
                mGetSyswareObject();
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
                mGetSyswareObject();
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
                mGetSyswareObject();
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
                mGetSyswareObject();
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
                mGetSyswareObject();
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
                Object SyswareObj =
                    Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");

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
                Object SyswareObj =
                    Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");

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
                Object SyswareObj =
                    Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");

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

        #region ���÷���

        public static bool SetParameterGroup(string paraName, string groupName)
        {
            try
            {
                Object SyswareObj =
                    Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");

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
                Object SyswareObj =
                    Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");

                // ����������·��
                String context = _Context;

                // ������Ϣ
                String errorMsg = String.Empty;

                if (SyswareObj != null)
                {
                    // ��ȡ������ֵ�����ַ������������޷�ͨ��ref��ȡ��errorMsg��Ϣ��
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

        #region ɾ������

        public static bool DeleteParameter(String _Context, String _Name)
        {
            try
            {
                Object SyswareObj =
                    Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");

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
    }
}