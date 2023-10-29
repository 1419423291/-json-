using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// LoginMgr内存放临时注册数据容器(LoginPanel渲染)。
/// 记录玩家操作数据：账密、记住密码、自动登录、选择服务器。
/// Data都用于记录临时内存中的存储数据
/// </summary>
public class LoginData 
{
    public string userName;
    public string passWord;
    public bool rememberPw;
    public bool autoLogin;
    //服务器相关(-1代表未选择过服务器)
    public int frontServerID = 0;
}
