using System;
/// <summary>
/// unit��������
/// </summary>
public enum UnitControlType
{
	None = -1,
    /// <summary>
    /// ����
    /// </summary>
	Free,
    /// <summary>
    /// �ط�
    /// </summary>
	Replay,
    /// <summary>
    /// pvp�Լ�����
    /// </summary>
	PvpMyControl,
    /// <summary>
    /// pvp AI����
    /// </summary>
	PvpAIControl,
    /// <summary>
    /// PVP�������
    /// </summary>
	PvpNetControl
}
