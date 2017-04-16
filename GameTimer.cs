using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��Ϸ��ʱ��ģ��
/// </summary>
public class GameTimer : BaseGameModule
{
    /// <summary>
    /// ��Ϸ�������͵�ʱ���ʱ������ϵ���б�
    /// </summary>
	private static readonly Dictionary<string, float> _timeScales = new Dictionary<string, float>();
    /// <summary>
    /// ��¼��Ϸ���еĿ�ʼʱ��
    /// </summary>
	private float _startTimestamp;
    /// <summary>
    /// ��¼��Ϸֹͣ�Ŀ�ʼʱ��
    /// </summary>
	private float _stopTimestamp;
    /// <summary>
    /// ��Ϸ�����еĳ���ʱ��
    /// </summary>
	public float TotalPlayingSeconds
	{
		get
		{
			if (GameManager.IsPlaying())
			{
				return RealTime.time - this._startTimestamp;
			}
			return this._stopTimestamp - this._startTimestamp;
		}
	}
    /// <summary>
    /// ��Ϸ״̬�����֪ͨ��Ϸ��ʱ�����������ֵ״̬
    /// </summary>
    /// <param name="oldState"></param>
    /// <param name="newState"></param>
	public override void OnGameStateChange(GameState oldState, GameState newState)
	{
		if (newState == GameState.Game_Playing)  //�������״̬Ϊ���У���¼���еĿ�ʼʱ��
		{
			this._startTimestamp = RealTime.time;
			this._stopTimestamp = 0f;
		}
		else if (newState == GameState.Game_Pausing) //�������״̬Ϊ��ͣ����¼ֹͣ�Ŀ�ʼʱ��
		{
			if (this._stopTimestamp == 0f)
			{
				this._stopTimestamp = RealTime.time;
			}
		}
		else if (newState == GameState.Game_Resume) //�������״̬Ϊ��ͣ���¿�ʼ������ʱ��Ҫ��ȥ��ͣ��ʱ��
		{
			this._startTimestamp += RealTime.time - this._stopTimestamp;
			this._stopTimestamp = 0f;
		}
		else if (newState == GameState.Game_Over && this._stopTimestamp == 0f) //���������Ϸ������������ͣʱ��0������ͣ��ʼʱ��Ϊ��ǰ
		{
			this._stopTimestamp = RealTime.time;
		}
	}
    /// <summary>
    /// ������Ϸ��ָ�����ͼ�ʱ����ʱ������ϵ��
    /// </summary>
    /// <param name="useType"></param>
    /// <param name="scale"></param>
	public static void SetTimeScale(string useType, float scale)
	{
		GameTimer._timeScales[useType] = scale;
		float num = 1f;
		foreach (KeyValuePair<string, float> current in GameTimer._timeScales)
		{
			num *= current.Value; //�ۼƼ�������ʱ���������ӵĵ���
		}
		Time.timeScale = num;  //����ʱ���������Ӷ�ʱ���������Ӱ��
	}
    /// <summary>
    /// ����ʱ���������ӣ������Ϸ��������ʱ���������ӵ�Ӱ��
    /// </summary>
	public static void NormalTimeScale()
	{
		Time.timeScale = 1f;
		GameTimer._timeScales.Clear();
	}
    /// <summary>
    /// ֹͣʱ������????????
    /// </summary>
	public static void StopTimeScale()
	{
	}
}
