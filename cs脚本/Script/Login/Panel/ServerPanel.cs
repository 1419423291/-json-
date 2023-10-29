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
            //置否自动登录勾选,避免进入LoginPanel后重复进行自动登录逻辑
            if (LoginMgr.Instance.LoginData.autoLogin)
                LoginMgr.Instance.LoginData.autoLogin = false;  
            UIManager.Instance.ShowPanel<LoginPanel>();
            UIManager.Instance.HidePanel<ServerPanel>();
        });
        btnStart.onClick.AddListener(() =>
        {
            //隐藏ServerPanel 存储选择的服务器数据
            UIManager.Instance.HidePanel<ServerPanel>();
            //隐藏登录背景图面板
            UIManager.Instance.HidePanel<LoginBKPanel>();
            //让下次进入游戏场景时能够使用LoginMgr中的LoginData数据
            LoginMgr.Instance.SaveLoginData();
            //进入游戏
            SceneManager.LoadScene("GameScene");
        });
        btnChange.onClick.AddListener(() =>
        {
            //进入选服面板
            UIManager.Instance.ShowPanel<ChooseServerPanel>();
            //隐藏自己
            UIManager.Instance.HidePanel<ServerPanel>();
        });
    }
    public override void ShowMe()
    {
        base.ShowMe();
        //更新当前服务器名字(通过上一次记录的frontServerID服务器ID更新内容)
        int id = LoginMgr.Instance.LoginData.frontServerID;
        if (id <= 0)
            txtName.text = "无";
        else
        {
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            txtName.text = info.id + "区  " + info.name;
        }
    }
}
