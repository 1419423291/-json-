using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// LoginMgr�ڴ����ʱע����������(LoginPanel��Ⱦ)��
/// ��¼��Ҳ������ݣ����ܡ���ס���롢�Զ���¼��ѡ���������
/// Data�����ڼ�¼��ʱ�ڴ��еĴ洢����
/// </summary>
public class LoginData 
{
    public string userName;
    public string passWord;
    public bool rememberPw;
    public bool autoLogin;
    //���������(-1����δѡ���������)
    public int frontServerID = 0;
}
