using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    public Button btnRegister;
    public Button btnSure;
    public InputField inputUN;
    public InputField inputPW;
    public Toggle togPW;//��ס����
    public Toggle togAuto;//�Զ���½
    public override void Init()
    {
        //ע�ᰴť�߼�
        btnRegister.onClick.AddListener(() =>
        {
            //��̬���ע�����
            UIManager.Instance.ShowPanel<RegisterPanel>();
            //�����Լ�
            UIManager.Instance.HidePanel<LoginPanel>();
        });
        //��¼��ť�߼�
        btnSure.onClick.AddListener(() =>
        {
            if (inputUN.text.Length <= 3 || inputPW.text.Length <= 3)
            {
                //��ʾ���Ϸ�
                TipPanel panel = UIManager.Instance.ShowPanel<TipPanel>();
                //�ı���ʾ�������
                panel.ChangeInfo("�˺Ż����벻����");
                return;
            }
            //��֤�˺�����
            if(LoginMgr.Instance.CheckInfo(inputUN.text,inputPW.text))
            {
                //��¼�ɹ�
                //��¼����
                LoginMgr.Instance.LoginData.userName = inputUN.text;
                LoginMgr.Instance.LoginData.passWord = inputPW.text;
                LoginMgr.Instance.LoginData.rememberPw = togPW.isOn;
                LoginMgr.Instance.LoginData.autoLogin= togAuto.isOn;
                LoginMgr.Instance.SaveLoginData();//LoginMgr���Զ�д��Json
                #region �Ƿ��ѡ������߼�
                //���ݷ�������Ϣ�����ж���ʾ���
                if (LoginMgr.Instance.LoginData.frontServerID <= 0)
                {
                    //���ûѡ���������ֱ�ӽ���ѡ�����
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //��֮�򿪷��������
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }
                //�����Լ�
                UIManager.Instance.HidePanel<LoginPanel>();
                #endregion
            }
            else
            {
                //��¼ʧ��
                //��ʾ���Ϸ� �ı���ʾ�������
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("�˺Ż����벻����");
            }

        });
        //��ס�����߼�
        togPW.onValueChanged.AddListener((IsOn) =>
        {
            //ȡ����ס����ʱȡ����ѡ�Զ���¼
            if (!IsOn) 
            { 
                togAuto.isOn = false;//isOn��toggle�����bool
            }
        });
        //�Զ���¼�߼�
        togAuto.onValueChanged.AddListener((IsOn) =>
        {
            //ѡ���Զ���¼ʱ�Զ���ѡ��ס����
            if (IsOn) 
            {
                //�˴���isOn���ǲ���
                togPW.isOn = true;//isOn��toggle�����bool
            }
        });
    }
    /// <summary>
    /// �Զ���¼��ʶ��ServerPanel�а��·��ذ�ť����ֱ���Զ���¼
    /// </summary>

    public override void ShowMe()
    {
        base.ShowMe();
        //��ʾ�Լ�ʱ ��ȡxml�ļ������������
        //��ȡ����
        LoginData loginData = LoginMgr.Instance.LoginData;
        //��ʼ�������ʾ
        togPW.isOn = loginData.rememberPw;
        inputUN.text = loginData.userName;
        togAuto.isOn = loginData.autoLogin;
        if(togPW.isOn)
            inputPW.text = loginData.passWord;
        if(togAuto.isOn)
        {
            //�Զ���֤�˺����
            //��֤�û�������
            if (LoginMgr.Instance.CheckInfo(inputUN.text, inputPW.text))
            {
                #region �Ƿ��ѡ������߼�
                //���ݷ�������Ϣ�����ж���ʾ���
                if (LoginMgr.Instance.LoginData.frontServerID <= 0)
                {
                    //���ûѡ���������ֱ�ӽ���ѡ�����
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //��֮�򿪷��������
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }
                //�����Լ�
                UIManager.Instance.HidePanel<LoginPanel>(false);
                #endregion
            }
            else
            {
                TipPanel panel=UIManager.Instance.ShowPanel<TipPanel>();
                panel.ChangeInfo("�˺Ż��������");
            }
        }
    }
    /// <summary>
    /// LoginPanelע��ɹ���ı�������ݷ���(�ⲿ����)
    /// </summary>
    /// <param name="userName">�˺�</param>
    /// <param name="password">����</param>
    public void SetInfo(string userName,string password)
    {
        inputUN.text = userName;
        inputPW.text = password;
    }
}
