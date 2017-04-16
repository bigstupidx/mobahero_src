using System;
/// <summary>
/// ��Ϸģ��ӿ�
/// </summary>
public interface IGameModule
{
    /// <summary>
    /// ��ʼ��
    /// </summary>
	void Init();
    /// <summary>
    /// ����
    /// </summary>
	void Uninit();
    /// <summary>
    /// ��Ϸ״̬�仯�ص��ӿ�
    /// </summary>
    /// <param name="oldState"></param>
    /// <param name="newState"></param>
	void OnGameStateChange(GameState oldState, GameState newState);
}
