using System;
/// <summary>
/// ��̬unit���
/// </summary>
public class StaticUnitComponent : UnitComponent
{
    /// <summary>
    /// �Ƿ���Ҫ����---����Ҫ
    /// </summary>
    /// <returns></returns>
	public override bool needUpdate()
	{
		return false;
	}
}
