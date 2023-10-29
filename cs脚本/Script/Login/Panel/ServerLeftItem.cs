using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerLeftItem : MonoBehaviour
{
    public Button btnSelf;
    public Text txtInfo;
    //���䷶Χ
    private int beginIndex;
    private int endIndex;
    private void Start()
    {
        btnSelf.onClick.AddListener(() =>
        {
            //ѡ�����ı��Ҳ�����
            //��ȡѡ�����
            ChooseServerPanel panel = UIManager.Instance.GetPanel<ChooseServerPanel>();
            panel.UpdatePanel(beginIndex, endIndex);
        });
    }
    /// <summary>
    /// ServerLeftItem��ť��ʼ��(��������)
    /// </summary>
    /// <param name="beginIndex"></param>
    /// <param name="endIndex"></param>
    public void InitInfo(int beginIndex, int endIndex)
    {
        this.beginIndex = beginIndex;
        this.endIndex = endIndex;
        txtInfo.text = beginIndex.ToString() + "-" + endIndex.ToString() + "��";
    }
}
