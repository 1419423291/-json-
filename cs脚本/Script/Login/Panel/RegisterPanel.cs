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
        btnCancel.onClick.AddListener(() => //取消按钮逻辑
        {
            //隐藏自己
            UIManager.Instance.HidePanel<RegisterPanel>();
            //显示登录面板
            UIManager.Instance.ShowPanel<LoginPanel>();
        });
        btnSure.onClick.AddListener(() =>   //确定按钮逻辑
        {
            if (inputUN.text.Length <= 6 || inputPW.text.Length <= 6)
            {
                //提示不合法
                TipPanel panel = UIManager.Instance.ShowPanel<TipPanel>();
                //改变提示面板内容
                panel.ChangeInfo("账号或密码不得少于7位");
                return;
            }
            //注册账号密码
            if (LoginMgr.Instance.RegisterUser(inputUN.text, inputPW.text))
            {
                //清理登录数据 用于新注册账号的数据重置 防止使用LoginMgr中前一个登录账号的LoginData数据
                LoginMgr.Instance.ClearLoginData(); 
                //显示登录面板 更新面板上的对应数据
                LoginPanel loginPanel = UIManager.Instance.ShowPanel<LoginPanel>();
                loginPanel.SetInfo(inputUN.text, inputPW.text);
                //隐藏自己
                UIManager.Instance.HidePanel<RegisterPanel>();
            }
            else
            {
                //提示用户名存在   
                TipPanel tipPanel=UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("用户名已存在");
                //清空注册面板数据 方便再注册
                inputUN.text = "";
                inputPW.text = "";
            }
        });
    }
}

