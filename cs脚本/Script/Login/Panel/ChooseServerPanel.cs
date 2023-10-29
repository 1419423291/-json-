using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ChooseServerPanel : BasePanel
{
    //���ҹ�����ͼ
    public ScrollRect svLeft;
    public ScrollRect svRight;
    //�ϴε�¼��������Ϣ
    public Text txtName;
    public Image imgState;
    //��ǰѡ�����������
    public Text txtRange;
    /// <summary>
    /// �洢ѡ������Ҳද̬��ť(����:�����಻��Ҫ��̬�����Ҳ���Ҫ��̬����)
    /// </summary>
    private List<GameObject> itemList=new List<GameObject>();

    public override void Init()
    {
        //��̬������ఴť:��ȡ����������
        List<ServerInfo> infoList = LoginMgr.Instance.ServerData;
        int num = (infoList.Count / 5) + 1;//������
        for (int i = 0; i < num; i++)
        {
            GameObject item = Instantiate(Resources.Load<GameObject>("UI/ServerLeftItem"));//��������������Ԥ����
            item.transform.SetParent(svLeft.content, false);//���ø�����
            ServerLeftItem serverLeft = item.GetComponent<ServerLeftItem>();
            int beginIndex = i * 5 + 1;
            int endIndex = i * 5 + 5;
            if (endIndex > infoList.Count)
                endIndex = infoList.Count;
            serverLeft.InitInfo(beginIndex, endIndex);
        }
    }

    public override void ShowMe()
    {
        base.ShowMe();
        #region ���£��ϴ�ѡ��ķ���������
        //��ʾ�Լ���ʱ����£��ϴ�ѡ��ķ���������
        //���µ�ǰѡ��ķ�����
        int id = LoginMgr.Instance.LoginData.frontServerID;
        if( id <= 0 ) 
        {
            //û��ѡ��
            txtName.text = "��";
            imgState.gameObject.SetActive(false);
        }
        else
        {
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            txtName.text = info.id + "�� " + info.name;
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
        #endregion
        #region ���£�������������ʾ
        //�ж��Ƿ�С��5��������
        UpdatePanel(1, 5 > LoginMgr.Instance.ServerData.Count ? LoginMgr.Instance.ServerData.Count : 5);
        #endregion
    }

    /// <summary>
    /// ChoosePanel�ڸ��µ�ǰѡ�����������Ҳఴť(�ⲿ����)
    /// </summary>
    /// <param name="beginIndex"></param>
    /// <param name="endIndex"></param>
    public void UpdatePanel(int beginIndex,int endIndex)
    {
        #region ���·�����������ʾ
        //���·�����������ʾ
        txtRange.text = "������ " + beginIndex + "-" + endIndex;
        //ɾ��֮ǰ�ĵ�����ť��ע��itemList�ǵ�ǰ�洢�ķ������б�
        for (int i = 0; i < itemList.Count; i++)
        {
            //ɾ��֮ǰ�ķ���������
            Destroy(itemList[i]);
        }
        //����б�
        itemList.Clear();
        #endregion
        #region �����°�ť
        //�����°�ť
        for (int i = beginIndex; i <= endIndex; i++)
        {
            //��ȡ��������Ϣ
            ServerInfo nowInfo = LoginMgr.Instance.ServerData[i - 1];
            //��̬����Ԥ����
            GameObject serverItem = Instantiate(Resources.Load<GameObject>("UI/ServerRightItem"));
            serverItem.transform.SetParent(svRight.content, false);
            //ʹ��ServerRightItem��InitInfo������ʼ��
            ServerRightItem rightItem=serverItem.GetComponent<ServerRightItem>();
            rightItem.InitInfo(nowInfo);
            //�����ɹ������¼��itemList�б���
            itemList.Add(serverItem);
        }
        #endregion
    }
}
