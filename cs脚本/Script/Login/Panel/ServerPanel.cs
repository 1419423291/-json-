using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerPanel : BasePanel
{
    public Button btnChange;
    public Button btnStart;
    public Button btnBack;
    public Text txtName;
    public override void Init()
    {
        btnBack.onClick.AddListener(() =>
        {
            //�÷��Զ���¼��ѡ,�������LoginPanel���ظ������Զ���¼�߼�
            if (LoginMgr.Instance.LoginData.autoLogin)
                LoginMgr.Instance.LoginData.autoLogin = false;  
            UIManager.Instance.ShowPanel<LoginPanel>();
            UIManager.Instance.HidePanel<ServerPanel>();
        });
        btnStart.onClick.AddListener(() =>
        {
            //����ServerPanel �洢ѡ��ķ���������
            UIManager.Instance.HidePanel<ServerPanel>();
            //���ص�¼����ͼ���
            UIManager.Instance.HidePanel<LoginBKPanel>();
            //���´ν�����Ϸ����ʱ�ܹ�ʹ��LoginMgr�е�LoginData����
            LoginMgr.Instance.SaveLoginData();
            //������Ϸ
            SceneManager.LoadScene("GameScene");
        });
        btnChange.onClick.AddListener(() =>
        {
            //����ѡ�����
            UIManager.Instance.ShowPanel<ChooseServerPanel>();
            //�����Լ�
            UIManager.Instance.HidePanel<ServerPanel>();
        });
    }
    public override void ShowMe()
    {
        base.ShowMe();
        //���µ�ǰ����������(ͨ����һ�μ�¼��frontServerID������ID��������)
        int id = LoginMgr.Instance.LoginData.frontServerID;
        if (id <= 0)
            txtName.text = "��";
        else
        {
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            txtName.text = info.id + "��  " + info.name;
        }
    }
}
