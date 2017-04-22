using System;
using UnityEngine;

namespace MobaFrame.SkillAction
{
    /// <summary>
    /// ����������
    /// </summary>
	public abstract class BasePlayer : MonoBehaviour, IPlayer
	{
        /// <summary>
        /// ��ʼ���Żص�
        /// </summary>
		public Callback<int> OnPlayCallback;
        /// <summary>
        /// ֹͣ���Żص�
        /// </summary>
		public Callback<int> OnStopCallback;
        /// <summary>
        /// ���ٲ��Żص�
        /// </summary>
		public Callback<int> OnDestroyCallback;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void OnDestroy()
		{
			this.OnPlayCallback = null;
			this.OnStopCallback = null;
			this.OnDestroyCallback = null;
		}
        /// <summary>
        /// ��ʼ���Žӿ�
        /// </summary>
		public abstract void Play();
        /// <summary>
        /// ֹͣ���Žӿ�
        /// </summary>
		public abstract void Stop();
	}
}
