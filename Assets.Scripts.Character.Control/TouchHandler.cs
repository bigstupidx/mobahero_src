using System;
using UnityEngine;

namespace Assets.Scripts.Character.Control
{
    /// <summary>
    /// �����¼�������
    /// </summary>
	public class TouchHandler : IControlHandler
	{
		private ControlEventManager tEventManager;
        /// <summary>
        /// ��ָ����
        /// </summary>
		private int fingerID;
        /// <summary>
        /// �Ƿ���
        /// </summary>
		private bool hasPressed;
        /// <summary>
        /// ����ʱ���
        /// </summary>
		private float downTimeStamp;

		private Touch touchInfo;

		public int ControlID
		{
			get
			{
				return this.fingerID;
			}
		}

		public TouchHandler(int fingerIndex, ControlEventManager teManager)
		{
			this.tEventManager = teManager;
			this.fingerID = fingerIndex;
			this.Init();
		}

		private void Init()
		{
			this.hasPressed = false;
			this.downTimeStamp = 0f;
		}
        /// <summary>
        /// ���¿���
        /// </summary>
        /// <param name="deltaTime"></param>
		public override void updateControl(float deltaTime)
		{
			this.AnalysisState(deltaTime);
		}
        /// <summary>
        /// ���·����¼������߼�
        /// </summary>
        /// <param name="deltaTime"></param>
		private void AnalysisState(float deltaTime)
		{
			for (int i = 0; i < Input.touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				if (touch.fingerId == this.ControlID)
				{
					this.touchInfo = touch;
					this.DidpatchEvent(this.touchInfo.phase, deltaTime);
				}
			}
		}
        /// <summary>
        /// �ɷ�ָ�����Ե��¼�
        /// </summary>
        /// <param name="phase"></param>
        /// <param name="deltaTime"></param>
		private void DidpatchEvent(TouchPhase phase, float deltaTime)
		{
			switch (phase)
			{
			case TouchPhase.Began:
				this.Down(deltaTime);
				break;
			case TouchPhase.Moved:
				if (base.IsDrag(this.touchInfo.position))
				{
					this.Press(deltaTime);
				}
				break;
			case TouchPhase.Stationary:
				if (base.IsDrag(this.touchInfo.position))
				{
					this.Press(deltaTime);
				}
				break;
			case TouchPhase.Ended:
				this.Up(deltaTime);
				break;
			}
		}

		private void Down(float deltaTime)
		{
			this.hasPressed = false;
			base.InitPressPos(this.touchInfo.position);
			this.downTimeStamp = Time.realtimeSinceStartup;
			this.CreateTouchEvent(EControlType.eDown);
		}

		private void Up(float deltaTime)
		{
			if (this.hasPressed)
			{
				this.hasPressed = false;
				this.CreateTouchEvent(EControlType.eMoveEnd);
			}
			else
			{
				this.CreateTouchEvent(EControlType.eUp);
			}
			this.downTimeStamp = 0f;
		}

		private void Press(float deltaTime)
		{
			this.hasPressed = true;
			this.CreateTouchEvent(EControlType.ePress);
		}
        /// <summary>
        /// ����ָ�����͵��¼�,��������¼�������
        /// </summary>
        /// <param name="tType"></param>
		private void CreateTouchEvent(EControlType tType)
		{
			this.tEventManager.AddEvent(new ControlEvent
			{
				type = tType,
				id = this.ControlID,
				touchDownTimeStamp = this.downTimeStamp,
				position = this.touchInfo.position,
				createTimeStamp = Time.realtimeSinceStartup
			});
		}
	}
}
