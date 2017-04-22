using System;
/// <summary>
/// ʱ��ͬ��ϵͳ���
/// </summary>
public class UnitsTimeSyncSystem : UnitComponent
{
    /// <summary>
    /// unit��ʱ������
    /// </summary>
	public float unitsTimeScale = 1f;
    /// <summary>
    /// �ͻ���֮ǰͬ���ķ�����ʱ�䣿��������
    /// </summary>
	private long unitsSvrTime;
    /// <summary>
    /// unit�Ŀͻ��˼�ʱ
    /// </summary>
	public long unitsClientSvrTime;

	private int tmpCnt;

	public UnitsTimeSyncSystem()
	{
		this.donotUpdateByMonster = true;  //Monster��update
	}
    /// <summary>
    /// ���ÿͻ��˼�ʱ
    /// </summary>
    /// <param name="inSvrTime">ʱ���������ͬ��������ʱ��</param>
	public void ResetClientSvrTime(long inSvrTime)
	{
		if (inSvrTime < this.unitsSvrTime)
		{
			this.unitsClientSvrTime = inSvrTime;
		}
	}

	public override void OnInit()
	{
	}
    /// <summary>
    /// ����update�����ӿ�
    /// </summary>
    /// <param name="deltaTime"></param>
	public override void OnUpdate(float deltaTime)
	{
		if (this.unitsSvrTime == 0L)
		{
			Units player = PlayerControlMgr.Instance.GetPlayer();
			if (player != null)
			{
				this.unitsSvrTime = NetWorkHelper.Instance.client.GetSvrTime();
				this.unitsClientSvrTime = NetWorkHelper.Instance.client.GetClientTime();
			}
		}
		else
		{
			this.unitsClientSvrTime += (long)(deltaTime * 1000f);
			this.unitsSvrTime = NetWorkHelper.Instance.client.GetSvrTime();
			float num = (float)(this.unitsSvrTime - this.unitsClientSvrTime);
			if (num <= 0f) //�ͻ����ܿ��֣�Ҫ������
			{
				this.unitsTimeScale = 0.9f;
				return;
			}
			if (num < 600f)//�ͻ���������׷����
			{
				this.unitsTimeScale = 1f + num / 1000f;
			}
			else if (num < 2000f)   //���3������
			{
				this.unitsTimeScale = 3f;
			}
			else
			{
				this.unitsTimeScale = 1f;
				this.unitsClientSvrTime = this.unitsSvrTime;
			}
			if (this.tmpCnt++ % 2 == 0)
			{
			}
			this.ProcessMsgAfterUpdateTime();
		}
	}
    /// <summary>
    /// ������ʱ�䣬������Ϣpvp server msg����
    /// </summary>
	private void ProcessMsgAfterUpdateTime()
	{
		while (true)
		{
			MobaMessage mobaMessage = this.self.FetchPvpServerMsg();
			if (mobaMessage == null)
			{
				break;
			}
			MobaMessageManager.ExecuteMsg(mobaMessage);
		}
	}

	public override void OnStop()
	{
	}

	public override void OnExit()
	{
	}
}
