using System;
using UnityEngine;

namespace MobaFrame.SkillAction
{
    /// <summary>
    /// Ч����������
    /// </summary>
	public class EffectPlayer : BasePlayer
	{
        /// <summary>
        /// ���ܽڵ�id
        /// </summary>
		public int skillNodeId;
        /// <summary>
        /// ִ��id
        /// </summary>
		public string performId;
        /// <summary>
        /// �˳�ʱ��
        /// </summary>
		public float exitTime;

		[SerializeField]
		private ParticleSystem[] m_particleSystems;

		[SerializeField]
		private LineRenderer[] m_lineRender;

		[SerializeField]
		private Renderer[] m_Render;

		private Animator[] m_Animator;

		public ParticleSystem[] mParticleSystem
		{
			get
			{
				return this.m_particleSystems;
			}
		}

		public LineRenderer[] mLineRender
		{
			get
			{
				return this.m_lineRender;
			}
		}

		public Animator[] mAnimator
		{
			get
			{
				return this.m_Animator;
			}
		}
        /// <summary>
        /// ��ʼ����ȡ��ض�������
        /// </summary>
		private void Awake()
		{
			if (this.m_particleSystems == null || this.m_particleSystems.Length == 0)
			{
				this.m_particleSystems = base.GetComponentsInChildren<ParticleSystem>();
			}
			if (this.m_lineRender == null || this.m_lineRender.Length == 0)
			{
				this.m_lineRender = base.GetComponentsInChildren<LineRenderer>();
			}
			this.m_Render = base.GetComponentsInChildren<Renderer>();
			if (this.mAnimator == null || this.mAnimator.Length == 0)
			{
				this.m_Animator = base.GetComponentsInChildren<Animator>();
			}
		}

		private void OnDestroy()
		{
		}
        /// <summary>
        /// ��ʼ����Ч��
        /// </summary>
		public override void Play()
		{
			if (this.OnPlayCallback != null)
			{
				this.OnPlayCallback(this.skillNodeId);
			}
		}
        /// <summary>
        /// ֹͣ����Ч��
        /// </summary>
		public override void Stop()
		{
			if (this.OnStopCallback != null)
			{
				this.OnStopCallback(this.skillNodeId);
			}
		}
        /// <summary>
        /// �����Ч������
        /// </summary>
        /// <param name="effect"></param>
		public void AddEffect(GameObject effect)
		{
			if (effect != null)
			{
				effect.transform.parent = base.transform;
			}
		}
        /// <summary>
        /// ������Ⱦ����Ⱦ�����
        /// </summary>
        /// <param name="bIsShow"></param>
		public void ShowRenders(bool bIsShow)
		{
			if (this.m_Render != null)
			{
				for (int i = 0; i < this.m_Render.Length; i++)
				{
					if (this.m_Render[i] != null)
					{
						this.m_Render[i].enabled = bIsShow;
					}
				}
			}
		}
	}
}
