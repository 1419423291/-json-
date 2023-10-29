using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public Button btnSure;//ȷ����ť
    public Text txtInfo;//��ʾ����
    public override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            //�����Լ�
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }
    /// <summary>
    /// �޸���ʾ
    /// </summary>
    /// <param name="info">�ַ�������</param>
    public void ChangeInfo(string info)
    {
        txtInfo.text = info;
    }
}
