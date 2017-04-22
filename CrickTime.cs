using Com.Game.Module;
using MobaHeros.Pvp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
/// <summary>
/// ��ȴʱ�����
/// </summary>
public class CrickTime : StaticUnitComponent
{
    /// <summary>
    /// �˶�������ȴ��ʱ
    /// </summary>
	public float moveAnimCool;
    /// <summary>
    /// ������ȴ��ʱ
    /// </summary>
	public float actionCool;
    /// <summary>
    /// ������ȴ��ʱ
    /// </summary>
	public float skillCool;
    /// <summary>
    /// ������ȴ��ʱ
    /// </summary>
	public float attackCool;
    /// <summary>
    /// �ȴ���ȴ��ʱ
    /// </summary>
	public float waitCool;
    /// <summary>
    /// ai��ȴ��ʱ
    /// </summary>
	public float aiCool;
    /// <summary>
    /// ����ʱ�䳤����ȴ��ʱ
    /// </summary>
	public float attackTimeLenghCool;
    /// <summary>
    /// ����id�б�
    /// </summary>
	public List<string> skillID = new List<string>();
    /// <summary>
    /// ����CD�б�
    /// </summary>
	public Dictionary<string, float> skillCD = new Dictionary<string, float>();

	public Dictionary<string, float> publicCD = new Dictionary<string, float>();

	public Dictionary<string, float> chargeCD = new Dictionary<string, float>();

	public List<string> buffID = new List<string>();
    /// <summary>
    /// buffʱ���б�
    /// </summary>
	public Dictionary<string, float> buffTime = new Dictionary<string, float>();
    /// <summary>
    /// �Ƿ��ʼ��
    /// </summary>
	private bool _hasInit;

	private Task _updateTask;
    /// <summary>
    /// �Ƿ���Բ��Ŷ����ƶ�????
    /// </summary>
	public bool canMoveAnim
	{
		get
		{
			return this.moveAnimCool <= 0f;
		}
	}
    /// <summary>
    /// �Ƿ�ִ��Action
    /// </summary>
	public bool canAction
	{
		get
		{
			return this.actionCool <= 0f;
		}
	}
    /// <summary>
    /// �Ƿ�����ƶ�
    /// </summary>
	public bool canMove
	{
		get
		{
			return this.waitCool <= 0f;
		}
	}
    /// <summary>
    /// �Ƿ������ת
    /// </summary>
	public bool canRotate
	{
		get
		{
			return this.waitCool <= 0f;
		}
	}
    /// <summary>
    /// �Ƿ����ִ��AI
    /// </summary>
	public bool canAI
	{
		get
		{
			return this.aiCool <= 0f;
		}
	}
    /// <summary>
    /// �Ƿ�����ͷż���
    /// </summary>
	public bool canSkill
	{
		get
		{
			return this.skillCool <= 0f;
		}
	}
    /// <summary>
    /// �Ƿ���Խ��й���
    /// </summary>
	public bool canAttack
	{
		get
		{
			return this.attackCool <= 0f;
		}
	}
    /// <summary>
    /// �Ƿ��ڹ���ʱ����
    /// </summary>
	public bool isInAttackTimeLengh
	{
		get
		{
			return this.attackTimeLenghCool <= 0f;
		}
	}
    /// <summary>
    /// ��������״̬
    /// </summary>
	private void Clear()
	{
		this.skillCD.Clear();
		this.publicCD.Clear();
		this.buffTime.Clear();
		this.actionCool = 0f;
		this.skillCool = 0f;
		this.attackCool = 0f;
		this.waitCool = 0f;
		this.aiCool = 0f;
		this.attackTimeLenghCool = 0f;
		this.moveAnimCool = 0f;
		this._hasInit = false;
		this._updateTask = null;
		if (this.m_CoroutineManager != null)
		{
			this.m_CoroutineManager.StopAllCoroutine();
		}
	}

	[DebuggerHidden]
	private IEnumerator Update_Coroutine()
	{
		CrickTime.<Update_Coroutine>c__Iterator1D2 <Update_Coroutine>c__Iterator1D = new CrickTime.<Update_Coroutine>c__Iterator1D2();
		<Update_Coroutine>c__Iterator1D.<>f__this = this;
		return <Update_Coroutine>c__Iterator1D;
	}
    /// <summary>
    /// ���¸�����ȴʱ�估buffʱ��״̬
    /// </summary>
    /// <param name="deltaTime"></param>
	private void DoUpdate(float deltaTime)
	{
		if (GameManager.IsPausing())
		{
			return;
		}
		if (this.actionCool > 0f)
		{
			this.actionCool -= deltaTime;
		}
		if (this.skillCool > 0f)
		{
			this.skillCool -= deltaTime;
		}
		if (this.attackCool > 0f)
		{
			this.attackCool -= deltaTime;
		}
		if (this.waitCool > 0f)
		{
			this.waitCool -= deltaTime;
		}
		if (this.aiCool > 0f)
		{
			this.aiCool -= deltaTime;
		}
		if (this.attackTimeLenghCool > 0f)
		{
			this.attackTimeLenghCool -= deltaTime;
		}
		if (this.moveAnimCool > 0f)
		{
			this.moveAnimCool -= deltaTime;
		}
        //���ǹ۲���Ҳ���ǵ�ǰ��ҽ�ɫ
		if (!Singleton<PvpManager>.Instance.IsObserver && !this.self.isPlayer)
		{
			return;
		}
        //���ؽ�ɫ�Ž��������߼�,ˢ�¸��ּ��ܵ�CD״̬,buffʱ��״̬��
		for (int i = 0; i < this.skillID.Count; i++)
		{
			string text = this.skillID[i];
			float num;
			if (this.skillCD.TryGetValue(text, out num) && num > 0f)
			{
				if (num <= deltaTime)
				{
					this.skillCD[text] = 0f;
					MessageEventArgs messageEventArgs = new MessageEventArgs();
					messageEventArgs.AddMessage("type", 256.ToString());
					messageEventArgs.AddMessage("id", text);
					Singleton<SkillView>.Instance.GetMessages(messageEventArgs);
				}
				else
				{
					this.skillCD[text] = num - deltaTime;
				}
			}
			float num2 = 0f;
			if (this.publicCD.TryGetValue(text, out num2) && num2 > 0f)
			{
				if (num2 <= deltaTime)
				{
					this.publicCD[text] = 0f;
					MessageEventArgs messageEventArgs2 = new MessageEventArgs();
					messageEventArgs2.AddMessage("type", 256.ToString());
					messageEventArgs2.AddMessage("id", text);
					Singleton<SkillView>.Instance.GetMessages(messageEventArgs2);
				}
				else
				{
					this.publicCD[text] = num2 - deltaTime;
				}
			}
			float num3;
			if (this.chargeCD.TryGetValue(text, out num3) && num3 > 0f)
			{
				if (num3 <= deltaTime)
				{
					this.chargeCD[text] = 0f;
				}
				else
				{
					this.chargeCD[text] = num3 - deltaTime;
				}
			}
		}
		if (this.buffTime != null && this.buffTime.Count > 0)
		{
			for (int j = 0; j < this.buffID.Count; j++)
			{
				string text2 = this.buffID[j];
				if (this.buffTime.ContainsKey(text2) && this.buffTime[text2] > 0f && this.buffTime[text2] >= 0f)
				{
					Dictionary<string, float> dictionary;
					Dictionary<string, float> expr_2FA = dictionary = this.buffTime;
					string key;
					string expr_2FF = key = text2;
					float num4 = dictionary[key];
					if ((expr_2FA[expr_2FF] = num4 - deltaTime) <= 0f)
					{
						this.buffTime[text2] = 0f;
						MessageEventArgs messageEventArgs3 = new MessageEventArgs();
						messageEventArgs3.AddMessage("type", 257.ToString());
						messageEventArgs3.AddMessage("id", text2);
						Singleton<SkillView>.Instance.GetMessages(messageEventArgs3);
					}
				}
				float num5;
				if (this.buffTime.TryGetValue(text2, out num5) && num5 > 0f)
				{
					if (num5 <= deltaTime)
					{
						this.buffTime[text2] = 0f;
						MessageEventArgs messageEventArgs4 = new MessageEventArgs();
						messageEventArgs4.AddMessage("type", 257.ToString());
						messageEventArgs4.AddMessage("id", text2);
						Singleton<SkillView>.Instance.GetMessages(messageEventArgs4);
					}
					else
					{
						this.buffTime[text2] = num5 - deltaTime;
					}
				}
			}
		}
	}

	public override void OnInit()
	{
		if (!this._hasInit)
		{
			if (this._updateTask == null)
			{
				this._updateTask = this.m_CoroutineManager.StartCoroutine(this.Update_Coroutine(), true);
			}
			this._hasInit = true;
		}
	}

	public override void OnStop()
	{
	}

	public override void OnExit()
	{
		this.Clear();
	}
    /// <summary>
    /// ���ü���cdʱ��
    /// </summary>
    /// <param name="skill_id"></param>
    /// <param name="time"></param>
	public void SetSkillCDTime(string skill_id, float time)
	{
		if (!this.skillID.Contains(skill_id))
		{
			this.skillID.Add(skill_id);
			this.skillCD.Add(skill_id, time);
		}
		else
		{
			this.skillCD[skill_id] = time;
		}
	}
    /// <summary>
    /// ��ȡ����cdʱ��
    /// </summary>
    /// <param name="skill_id"></param>
    /// <returns></returns>
	public float GetSkillCDtime(string skill_id)
	{
		if (this.skillCD.ContainsKey(skill_id))
		{
			return this.skillCD[skill_id];
		}
		return 0f;
	}
    /// <summary>
    /// ���ü�����ȴ
    /// </summary>
	public void ResertAllSkillCool()
	{
		this.skillCD.Clear();
	}
    /// <summary>
    /// ����ָ��id�ļ�����ȴ
    /// </summary>
    /// <param name="skill_id"></param>
	public void ResertSkillCool(string skill_id)
	{
		if (this.skillCD.ContainsKey(skill_id))
		{
			this.skillCD[skill_id] = 0f;
		}
	}
    /// <summary>
    /// ����buff��ʱ��
    /// </summary>
    /// <param name="buff_id"></param>
    /// <param name="time"></param>
	public void SetBuffTime(string buff_id, float time)
	{
		if (!this.buffID.Contains(buff_id))
		{
			this.buffID.Add(buff_id);
			this.buffTime.Add(buff_id, time);
		}
		else
		{
			this.buffTime[buff_id] = time;
		}
	}
    /// <summary>
    /// ��ȡbuff��ʱ��
    /// </summary>
    /// <param name="buff_id"></param>
    /// <returns></returns>
	public float GetBuffTime(string buff_id)
	{
		if (this.buffTime.ContainsKey(buff_id))
		{
			return this.buffTime[buff_id];
		}
		return 0f;
	}
    /// <summary>
    /// ���ü�������ʱ��
    /// </summary>
    /// <param name="skill_id"></param>
    /// <param name="time"></param>
	public void SetChargeTime(string skill_id, float time)
	{
		if (!this.skillID.Contains(skill_id))
		{
			this.skillID.Add(skill_id);
			this.chargeCD.Add(skill_id, time);
		}
		else
		{
			this.chargeCD[skill_id] = time;
		}
	}
    /// <summary>
    /// ��ȡ��������ʱ��
    /// </summary>
    /// <param name="skill_id"></param>
    /// <returns></returns>
	public float GetChargeTime(string skill_id)
	{
		if (this.chargeCD.ContainsKey(skill_id))
		{
			return this.chargeCD[skill_id];
		}
		return 0f;
	}
}
