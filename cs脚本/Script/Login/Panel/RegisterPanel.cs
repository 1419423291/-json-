using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    public Button btnSure;
    public Button btnCancel;
    public InputField inputUN;
    public InputField inputPW;
    public override void Init()
    {
        btnCancel.onClick.AddListener(() => //ȡ����ť�߼�
        {
            //�����Լ�
            UIManager.Instance.HidePanel<RegisterPanel>();
            //��ʾ��¼���
            UIManager.Instance.ShowPanel<LoginPanel>();
        });
        btnSure.onClick.AddListener(() =>   //ȷ����ť�߼�
        {
            if (inputUN.text.Length <= 6 || inputPW.text.Length <= 6)
            {
                //��ʾ���Ϸ�
                TipPanel panel = UIManager.Instance.ShowPanel<TipPanel>();
                //�ı���ʾ�������
                panel.ChangeInfo("�˺Ż����벻������7λ");
                return;
            }
            //ע���˺�����
            if (LoginMgr.Instance.RegisterUser(inputUN.text, inputPW.text))
            {
                //�����¼���� ������ע���˺ŵ��������� ��ֹʹ��LoginMgr��ǰһ����¼�˺ŵ�LoginData����
                LoginMgr.Instance.ClearLoginData(); 
                //��ʾ��¼��� ��������ϵĶ�Ӧ����
                LoginPanel loginPanel = UIManager.Instance.ShowPanel<LoginPanel>();
                loginPanel.SetInfo(inputUN.text, inputPW.text);
                //�����Լ�
                UIManager.Instance.HidePanel<RegisterPanel>();
            }
            else
            {
                //��ʾ�û�������   
                TipPanel tipPanel=UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("�û����Ѵ���");
                //���ע��������� ������ע��
                inputUN.text = "";
                inputPW.text = "";
            }
        });
    }
}

