using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ServerRightItem :MonoBehaviour
{
    /// <summary>
    /// ��������
    /// </summary>
    public Button btnSelf;
    /// <summary>
    /// �Ƿ��·�
    /// </summary>
    public Image imgNew;
    /// <summary>
    /// �� ά�� ���� ��æ ��
    /// </summary>
    public Image imgState;
    /// <summary>
    /// �ı���Ϣ 31��  ������˫
    /// </summary>
    public Text txtName;
    /// <summary>
    /// ��ǰѡ�з�����
    /// </summary>
    public ServerInfo nowServerInfo;
    void Start()
    {
        btnSelf.onClick.AddListener(() =>
        {
            //��¼��ǰѡ��ķ�����
            LoginMgr.Instance.LoginData.frontServerID = nowServerInfo.id;

            //����ѡ�����
            UIManager.Instance.HidePanel<ChooseServerPanel>(); 
            //��ʾ���������
            UIManager.Instance.ShowPanel<ServerPanel>();
        });
    }
    
    /// <summary>
    /// ������������ʼ����ť(�ⲿ����)
    /// </summary>
    /// <param name="info">��Ӧ������</param>
    public void InitInfo(ServerInfo info)
    {
        //������Ӧ������
        nowServerInfo = info;

        txtName.text = info.id + "��  " + info.name;
        imgNew.gameObject.SetActive(info.isNew);
        imgState.gameObject.SetActive(true);
        //����ͼ��
        SpriteAtlas sa = Resources.Load<SpriteAtlas>("Login");
        switch (info.state)
        {
            case 0:
                imgState.gameObject.SetActive(false);
                break;
            case 1:
                imgState.sprite = sa.GetSprite("ui_DL_liuchang_01");
                break;
            case 2:
                imgState.sprite = sa.GetSprite("ui_DL_fanhua_01");
                break;
            case 3:
                imgState.sprite = sa.GetSprite("ui_DL_huobao_01");
                break;
            case 4:
                imgState.sprite = sa.GetSprite("ui_DL_weihu_01");
                break;
        }
    }
}
