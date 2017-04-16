using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUIFramework
{
	public class UIUtils
	{
        /// <summary>
        /// �Ա�UIPanel���
        /// </summary>
		private class CompareSubPanels : IComparer<UIPanel>
		{
			public int Compare(UIPanel left, UIPanel right)
			{
				return left.depth - right.depth;
			}
		}
        /// <summary>
        /// ����������ֵ����ָ��������ص�UIPanel�����
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="depth">��С���</param>
		public static void SetTargetMinPanel(GameObject obj, int depth)
		{
			List<UIPanel> panelSorted = UIUtils.GetPanelSorted(obj, true);
			if (panelSorted != null)
			{
				for (int i = 0; i < panelSorted.Count; i++)
				{
					panelSorted[i].depth = depth + i;
				}
			}
		}
        /// <summary>
        /// ��ȡָ��������ص�UIPanel�е�������
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="includeInactive">�Ƿ����û�м����</param>
        /// <returns></returns>
		public static int GetMaxTargetDepth(GameObject obj, bool includeInactive = false)
		{
			int result = -1;
			List<UIPanel> panelSorted = UIUtils.GetPanelSorted(obj, includeInactive);
			if (panelSorted != null)
			{
				return panelSorted[panelSorted.Count - 1].depth;
			}
			return result;
		}
        /// <summary>
        /// ��ȡָ��������ص�UIPanel�е������Ȼ�����С���
        /// </summary>
        /// <param name="target"></param>
        /// <param name="maxDepth">�����Ȼ�����С���</param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
		public static GameObject GetPanelDepthMaxMin(GameObject target, bool maxDepth, bool includeInactive)
		{
			List<UIPanel> panelSorted = UIUtils.GetPanelSorted(target, includeInactive);
			if (panelSorted == null)
			{
				return null;
			}
			if (maxDepth)
			{
				return panelSorted[panelSorted.Count - 1].gameObject;
			}
			return panelSorted[0].gameObject;
		}
        /// <summary>
        /// ��ȡָ������㼶�е�UIPanel�����б�
        /// </summary>
        /// <param name="target"></param>
        /// <param name="includeInactive"></param>
        /// <returns></returns>
		private static List<UIPanel> GetPanelSorted(GameObject target, bool includeInactive = false)
		{
			UIPanel[] componentsInChildren = target.transform.GetComponentsInChildren<UIPanel>(includeInactive);
			if (componentsInChildren.Length > 0)
			{
				List<UIPanel> list = new List<UIPanel>(componentsInChildren);
				list.Sort(new UIUtils.CompareSubPanels());
				return list;
			}
			return null;
		}
	}
}
