using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerLeftItem : MonoBehaviour
{
    public Button btnSelf;
    public Text txtInfo;
    //区间范围
    private int beginIndex;
    private int endIndex;
    private void Start()
    {
        btnSelf.onClick.AddListener(() =>
        {
            //选服面板改变右侧内容
            //获取选服面板
            ChooseServerPanel panel = UIManager.Instance.GetPanel<ChooseServerPanel>();
            panel.UpdatePanel(beginIndex, endIndex);
        });
    }
    /// <summary>
    /// ServerLeftItem按钮初始化(区号区间)
    /// </summary>
    /// <param name="beginIndex"></param>
    /// <param name="endIndex"></param>
    public void InitInfo(int beginIndex, int endIndex)
    {
        this.beginIndex = beginIndex;
        this.endIndex = endIndex;
        txtInfo.text = beginIndex.ToString() + "-" + endIndex.ToString() + "区";
    }
}
