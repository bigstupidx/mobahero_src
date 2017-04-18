using System;

namespace GUIFramework
{
    /// <summary>
    /// ������Դ������
    /// </summary>
	public class WinResurceCfg
	{
        /// <summary>
        /// �Ƿ�AssetBundle
        /// </summary>
		public bool IsAssetbundle
		{
			get;
			private set;
		}

        /// <summary>
        /// �Ƿ�����ü���
        /// </summary>
		public bool IsLoadFromConfig
		{
			get;
			private set;
		}

        /// <summary>
        /// �������ӻ�·��????
        /// </summary>
		public string Url
		{
			get;
			private set;
		}

		public WinResurceCfg(bool isAssetBundle, bool isLoadFromcfg, string url)
		{
			this.IsAssetbundle = isAssetBundle;
			this.IsLoadFromConfig = isLoadFromcfg;
			this.Url = url;
		}
	}
}
