using System;

namespace GUIFramework
{
    /// <summary>
    /// ���ڴ�ģʽ��
    /// </summary>
	public enum WindowShowMode
	{
        /// <summary>
        /// ��ֱͨ�Ӵ�
        /// </summary>
		DoNothing,
        /// <summary>
        /// ��ʱ������������
        /// </summary>
		HideOther,
        /// <summary>
        /// �򿪴�����Ҫ����֧��
        /// </summary>
		NeedReturn,
        /// <summary>
        /// �򿪴��ڲ���Ҫ����
        /// </summary>
		UnneedReturn,
        /// <summary>
        /// ��������
        /// </summary>
		HideAll
	}
}
