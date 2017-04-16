using System;

namespace GUIFramework
{
    /// <summary>
    /// �򿪴����¼�����
    /// </summary>
	public class OpenWindowEventArgs : EventArgs
	{
        /// <summary>
        /// �Ƿ�ɹ�
        /// </summary>
		public bool IsSuccess
		{
			get;
			private set;
		}
        /// <summary>
        /// ��������
        /// </summary>
		public string WinName
		{
			get;
			private set;
		}
        /// <summary>
        /// ��ص�TUIWindow
        /// </summary>
		public TUIWindow UiWindow
		{
			get;
			private set;
		}

		public OpenWindowEventArgs(bool success, string name, TUIWindow window)
		{
			this.IsSuccess = success;
			this.WinName = name;
			this.UiWindow = window;
		}
	}
}
