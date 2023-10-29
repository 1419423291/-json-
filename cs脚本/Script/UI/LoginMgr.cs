using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// LoginMgr����ģʽ ˽�о�̬����
/// </summary>
public class LoginMgr 
{
    //����ģʽLoginMgr
    private static LoginMgr instance = new LoginMgr();
    public static LoginMgr Instance => instance;

    //��¼����
    private LoginData loginData = new LoginData();
    public LoginData LoginData => loginData;

    //ע������
    private RegisterData registerData = new RegisterData();
    public RegisterData RegisterData => registerData;

    private List<ServerInfo> serverData = new List<ServerInfo>();
    /// <summary>
    /// �洢�ķ���������(�б�)
    /// </summary>
    public List<ServerInfo> ServerData => serverData;
    private LoginMgr() 
    {
        //ֱ��ͨ��json������ ��ȡ��Ӧ����
        loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");
        registerData = JsonMgr.Instance.LoadData<RegisterData>("RegisterData");
        serverData = JsonMgr.Instance.LoadData<List<ServerInfo>>("ServerInfo");
    }

    #region LoginData�洢��¼����
    /// <summary>
    /// LoginData�洢��¼����
    /// </summary>
    public void SaveLoginData()
    {
        JsonMgr.Instance.SaveData(loginData, "LoginData");
    }
    /// <summary>
    /// ע���˺ź��Զ����LoginMgr�е�LoginData��
    /// �������˺Ż�ʹ�þɵ�LoginMgr�е�LoginData���ݽ���ServerPanel�������ǽ���ѡ�����
    /// </summary>
    public void ClearLoginData()
    {
        loginData.frontServerID = 0;
        loginData.autoLogin = false;
        loginData.rememberPw = false;
    }
    #endregion

    #region RegisterData�洢ע������

    /// <summary>
    /// RegisterData�洢ע������
    /// </summary>
    public void SaveRegisterData()
    {
        JsonMgr.Instance.SaveData(registerData, "RegisterData");
    }

    /// <summary>
    /// RegisterPanel���ע���߼�
    /// </summary>
    /// <param name="username">�û���</param>
    /// <param name="password">����</param>
    /// <returns></returns>
    public bool RegisterUser(string userName, string passWord)
    {
        //�˺Ŵ��ڣ�
        if (registerData.registerInfo.ContainsKey(userName))
            return false;
        //ע��
        registerData.registerInfo.Add(userName, passWord);
        //���ش洢
        SaveRegisterData();
        return true;
        
    }
    /// <summary>
    /// LoginPanel����¼�߼�
    /// </summary>
    /// <param name="username">�û���</param>
    /// <param name="password">����</param>
    /// <returns></returns>
    public bool CheckInfo(string userName, string passWord)
    {
        //�˺Ŵ��ڣ�
        if (registerData.registerInfo.ContainsKey(userName))
        {
            //��֤�ɹ�
            if (registerData.registerInfo[userName] == passWord)
            {
                return true;
            }
        }
        //��֤ʧ��
        return false;
    }
    #endregion
}
