
using System;
using System.Windows.Forms;

namespace XCommon
{
	/// <summary>
	/// ��־��
	/// </summary>
	public class XLog
	{
		/// <summary>
		/// �Ƿ����ʱ�䣬"HH:mm:ss"
		/// </summary>
		public static bool OutputTime = true;

		private delegate void AppendMessage(string strMsg);

		// ������Ҽ�����˵�
		public static TextBox TextBoxLog = null;

		public static void Write(string strInfo)
		{
			if (TextBoxLog == null)
				return;

			if (string.IsNullOrEmpty(strInfo))
				return;

			//if (InfoDockContainerItem == null)
			//   return;

			//InfoDockContainerItem.Selected = true;

			string strNewLine = "";

			if (TextBoxLog.Text.Length > 0)
				strNewLine = Environment.NewLine;

			if (TextBoxLog.InvokeRequired)
			{
				AppendMessage appendMsg = delegate(string msg)
				{
					Write(msg);
				};
				TextBoxLog.Invoke(appendMsg, new object[] { strInfo });
			}
			else
			{
				try
				{
					string strPrefix = "";
					if (OutputTime)
						strPrefix = DateTime.Now.ToString("HH:mm:ss") + " ";

					XStrings xstrsInfoLines = strInfo.Split('\n');
					for (int i = 1; i < xstrsInfoLines.Count; i++)
					{
						xstrsInfoLines[i] = string.Empty.PadLeft(strPrefix.Length) + xstrsInfoLines[i];
					}

					TextBoxLog.AppendText(strNewLine + strPrefix + xstrsInfoLines.ToLongString(false));
					TextBoxLog.ScrollToCaret();
				}
				catch (Exception)
				{
					// ���������˳�ʱ���������쳣
					XLog.TextBoxLog = null;
					return;
				}
			}
		}

        public static void log()
        {
            throw new NotImplementedException();
        }
    }
}
