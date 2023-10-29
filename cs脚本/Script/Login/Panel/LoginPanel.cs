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
    public Toggle togPW;//记住密码
    public Toggle togAuto;//自动登陆
    public override void Init()
    {
        //注册按钮逻辑
        btnRegister.onClick.AddListener(() =>
        {
            //动态添加注册面板
            UIManager.Instance.ShowPanel<RegisterPanel>();
            //隐藏自己
            UIManager.Instance.HidePanel<LoginPanel>();
        });
        //登录按钮逻辑
        btnSure.onClick.AddListener(() =>
        {
            if (inputUN.text.Length <= 3 || inputPW.text.Length <= 3)
            {
                //提示不合法
                TipPanel panel = UIManager.Instance.ShowPanel<TipPanel>();
                //改变提示面板内容
                panel.ChangeInfo("账号或密码不合理");
                return;
            }
            //验证账号密码
            if(LoginMgr.Instance.CheckInfo(inputUN.text,inputPW.text))
            {
                //登录成功
                //记录数据
                LoginMgr.Instance.LoginData.userName = inputUN.text;
                LoginMgr.Instance.LoginData.passWord = inputPW.text;
                LoginMgr.Instance.LoginData.rememberPw = togPW.isOn;
                LoginMgr.Instance.LoginData.autoLogin= togAuto.isOn;
                LoginMgr.Instance.SaveLoginData();//LoginMgr会自动写入Json
                #region 是否打开选服面板逻辑
                //根据服务器信息进行判断显示面板
                if (LoginMgr.Instance.LoginData.frontServerID <= 0)
                {
                    //如果没选择过服务器直接进入选服面板
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //反之打开服务器面板
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }
                //隐藏自己
                UIManager.Instance.HidePanel<LoginPanel>();
                #endregion
            }
            else
            {
                //登录失败
                //提示不合法 改变提示面板内容
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("账号或密码不存在");
            }

        });
        //记住密码逻辑
        togPW.onValueChanged.AddListener((IsOn) =>
        {
            //取消记住密码时取消勾选自动登录
            if (!IsOn) 
            { 
                togAuto.isOn = false;//isOn是toggle组件的bool
            }
        });
        //自动登录逻辑
        togAuto.onValueChanged.AddListener((IsOn) =>
        {
            //选中自动登录时自动勾选记住密码
            if (IsOn) 
            {
                //此处的isOn不是参数
                togPW.isOn = true;//isOn是toggle组件的bool
            }
        });
    }
    /// <summary>
    /// 自动登录标识：ServerPanel中按下返回按钮后不能直接自动登录
    /// </summary>

    public override void ShowMe()
    {
        base.ShowMe();
        //显示自己时 读取xml文件更新面板数据
        //获取数据
        LoginData loginData = LoginMgr.Instance.LoginData;
        //初始化面板显示
        togPW.isOn = loginData.rememberPw;
        inputUN.text = loginData.userName;
        togAuto.isOn = loginData.autoLogin;
        if(togPW.isOn)
            inputPW.text = loginData.passWord;
        if(togAuto.isOn)
        {
            //自动验证账号相关
            //验证用户名密码
            if (LoginMgr.Instance.CheckInfo(inputUN.text, inputPW.text))
            {
                #region 是否打开选服面板逻辑
                //根据服务器信息进行判断显示面板
                if (LoginMgr.Instance.LoginData.frontServerID <= 0)
                {
                    //如果没选择过服务器直接进入选服面板
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //反之打开服务器面板
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }
                //隐藏自己
                UIManager.Instance.HidePanel<LoginPanel>(false);
                #endregion
            }
            else
            {
                TipPanel panel=UIManager.Instance.ShowPanel<TipPanel>();
                panel.ChangeInfo("账号或密码错误");
            }
        }
    }
    /// <summary>
    /// LoginPanel注册成功后改变面板数据方法(外部可用)
    /// </summary>
    /// <param name="userName">账号</param>
    /// <param name="password">密码</param>
    public void SetInfo(string userName,string password)
    {
        inputUN.text = userName;
        inputPW.text = password;
    }
}
