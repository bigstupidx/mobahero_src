using GUIFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Game.Module
{
	public class BaseView<T> : Singleton<T>, IView where T : new()
	{
		private GameObject mGameObject;

		protected object intiData;

		public OpenViewGuideDelegate AfterOpenGuideDelegate;

		protected List<string> prefabCacheKeys = new List<string>();

		private GameObject animationRoot;

		public bool IsForceReset = true;
        /// <summary>
        /// ���ڱ���
        /// </summary>
		public string WindowTitle
		{
			get;
			set;
		}
        /// <summary>
        /// ������Դ������
        /// </summary>
		public WinResurceCfg WinResCfg
		{
			get;
			set;
		}

		public TUIWindow uiWindow
		{
			get;
			set;
		}
        /// <summary>
        /// ����ID
        /// </summary>
		public WindowID WinId
		{
			get;
			set;
		}

		public Transform transform
		{
			get
			{
				if (this.uiWindow == null)
				{
					return null;
				}
				return this.uiWindow.trans;
			}
		}

		public bool IsOpen
		{
			get
			{
				return null != this.gameObject && this.gameObject.activeInHierarchy;
			}
		}

		public GameObject gameObject
		{
			get
			{
				return (!this.uiWindow) ? null : this.uiWindow.gameObj;
			}
			set
			{
				this.mGameObject = value;
			}
		}

		public GameObject AnimationRoot
		{
			get
			{
				return this.animationRoot;
			}
			set
			{
				this.animationRoot = value;
			}
		}

		public bool IsOpened
		{
			get;
			set;
		}

		public virtual void Init()
		{
		}
        /// <summary>
        /// �򿪴�����ͼ��Ҫ���еĴ���
        /// </summary>
		public virtual void HandleAfterOpenView()
		{
		}

		public virtual void HandleBeforeCloseView()
		{
			MobaMessageManagerTools.SendClientMsg(ClientV2C.signView_close, null, false);
		}
        /// <summary>
        /// ע�������صĴ���ص�
        /// </summary>
		public virtual void RegisterUpdateHandler()
		{
		}
        /// <summary>
        /// ע��������صĴ���ص�
        /// </summary>
		public virtual void CancelUpdateHandler()
		{
		}
        /// <summary>
        /// ˢ��UI
        /// </summary>
		public virtual void RefreshUI()
		{
		}
        /// <summary>
        /// ��������
        /// </summary>
		public virtual void OnRestart()
		{
		}
        /// <summary>
        /// ���ٴ��ڽӿ�
        /// </summary>
		public virtual void Destroy()
		{
			this.UnloadPrefabCache();
			this.uiWindow = null;
		}
        /// <summary>
        /// ���ݸ��½ӿ�
        /// </summary>
        /// <param name="data"></param>
		public virtual void DataUpdated(object data)
		{
			this.intiData = data;
		}
        /// <summary>
        /// ����Ԥ����Դ������key
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
		protected GameObject LoadPrefabCache(string name)
		{
			GameObject result = ResourceManager.Load<GameObject>(name, true, this.WinResCfg.IsAssetbundle, null, 0, false);
			if (!this.prefabCacheKeys.Contains(name))
			{
				this.prefabCacheKeys.Add(name);
			}
			return result;
		}
        /// <summary>
        /// ж��Ԥ�Ʋ���ջ���key�б�
        /// </summary>
		protected void UnloadPrefabCache()
		{
			if (this.prefabCacheKeys != null)
			{
				string[] names = this.prefabCacheKeys.ToArray();
				ResourceManager.UnLoadBundle(names, this.WinResCfg.IsAssetbundle);
				this.prefabCacheKeys.Clear();
			}
		}
        /// <summary>
        /// �����߼�ִ�нӿ�
        /// </summary>
        /// <returns></returns>
		public virtual bool DoReturnLogic()
		{
			return true;
		}
        /// <summary>
        /// ��ʼ���ӿ�
        /// </summary>
		public virtual void Initialize()
		{
			if (this.IsForceReset)//ǿ������
			{
				this.BindObject();
				this.IsForceReset = false;
			}
			this.RegisterListener();
		}
        /// <summary>
        /// ���������¼��ӿ�
        /// </summary>
		public virtual void HandleHideEvent()
		{
			this.UnRegisterListener();
		}
        /// <summary>
        /// ���������¼��ӿ�
        /// </summary>
		public virtual void HandleDestroyEvent()
		{
			this.HandleHideEvent();
			this.IsForceReset = true;
		}
        /// <summary>
        /// ���°���Ϸ����//////
        /// </summary>
		protected virtual void BindObject()
		{
		}
        /// <summary>
        /// ע���¼���������
        /// </summary>
		protected virtual void RegisterListener()
		{
		}
        /// <summary>
        /// ע���¼���������
        /// </summary>
		protected virtual void UnRegisterListener()
		{
		}
	}
}
