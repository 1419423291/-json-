using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public Button btnSure;//确定按钮
    public Text txtInfo;//提示内容
    public override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            //隐藏自己
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }
    /// <summary>
    /// 修改提示
    /// </summary>
    /// <param name="info">字符串内容</param>
    public void ChangeInfo(string info)
    {
        txtInfo.text = info;
    }
}
