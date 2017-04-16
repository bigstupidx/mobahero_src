using System;

namespace GUIFramework
{
    /// <summary>
    /// �رմ����¼�������
    /// </summary>
	public class CloseWindowEventArgs : EventArgs
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
        /// �Ƿ����ٴ���
        /// </summary>
		public bool IsDestroy
		{
			get;
			private set;
		}
        /// <summary>
        /// �Ƿ����¿�ʼ
        /// </summary>
		public bool IsRestart
		{
			get;
			private set;
		}
        /// <summary>
        /// �¼���ص�TUIWindow����
        /// </summary>
		public TUIWindow UiWindow
		{
			get;
			private set;
		}

		public CloseWindowEventArgs(bool success, string name, bool isDestroy, bool isRestart, TUIWindow win)
		{
			this.IsSuccess = success;
			this.WinName = name;
			this.IsDestroy = isDestroy;
			this.IsRestart = isRestart;
			this.UiWindow = win;
		}
	}
}
