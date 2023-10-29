using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// LoginMgr单例模式 私有静态变量
/// </summary>
public class LoginMgr 
{
    //单例模式LoginMgr
    private static LoginMgr instance = new LoginMgr();
    public static LoginMgr Instance => instance;

    //登录数据
    private LoginData loginData = new LoginData();
    public LoginData LoginData => loginData;

    //注册数据
    private RegisterData registerData = new RegisterData();
    public RegisterData RegisterData => registerData;

    private List<ServerInfo> serverData = new List<ServerInfo>();
    /// <summary>
    /// 存储的服务器数据(列表)
    /// </summary>
    public List<ServerInfo> ServerData => serverData;
    private LoginMgr() 
    {
        //直接通过json管理器 获取对应数据
        loginData = JsonMgr.Instance.LoadData<LoginData>("LoginData");
        registerData = JsonMgr.Instance.LoadData<RegisterData>("RegisterData");
        serverData = JsonMgr.Instance.LoadData<List<ServerInfo>>("ServerInfo");
    }

    #region LoginData存储登录数据
    /// <summary>
    /// LoginData存储登录数据
    /// </summary>
    public void SaveLoginData()
    {
        JsonMgr.Instance.SaveData(loginData, "LoginData");
    }
    /// <summary>
    /// 注册账号后自动清除LoginMgr中的LoginData。
    /// 否则新账号会使用旧的LoginMgr中的LoginData数据进入ServerPanel面板而不是进入选服面板
    /// </summary>
    public void ClearLoginData()
    {
        loginData.frontServerID = 0;
        loginData.autoLogin = false;
        loginData.rememberPw = false;
    }
    #endregion

    #region RegisterData存储注册数据

    /// <summary>
    /// RegisterData存储注册数据
    /// </summary>
    public void SaveRegisterData()
    {
        JsonMgr.Instance.SaveData(registerData, "RegisterData");
    }

    /// <summary>
    /// RegisterPanel面板注册逻辑
    /// </summary>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    /// <returns></returns>
    public bool RegisterUser(string userName, string passWord)
    {
        //账号存在？
        if (registerData.registerInfo.ContainsKey(userName))
            return false;
        //注册
        registerData.registerInfo.Add(userName, passWord);
        //本地存储
        SaveRegisterData();
        return true;
        
    }
    /// <summary>
    /// LoginPanel面板登录逻辑
    /// </summary>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    /// <returns></returns>
    public bool CheckInfo(string userName, string passWord)
    {
        //账号存在？
        if (registerData.registerInfo.ContainsKey(userName))
        {
            //验证成功
            if (registerData.registerInfo[userName] == passWord)
            {
                return true;
            }
        }
        //验证失败
        return false;
    }
    #endregion
}
