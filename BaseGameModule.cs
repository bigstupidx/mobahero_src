using System;
/// <summary>
/// ��Ϸģ����࣬�����ӿڿ�ʵ��
/// </summary>
public class BaseGameModule : IGameModule
{
	public virtual void Init()
	{
	}

	public virtual void Uninit()
	{
	}

	public virtual void OnGameStateChange(GameState oldState, GameState newState)
	{
	}
}
