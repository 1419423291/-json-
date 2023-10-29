using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ServerRightItem :MonoBehaviour
{
    /// <summary>
    /// 按键本身
    /// </summary>
    public Button btnSelf;
    /// <summary>
    /// 是否新服
    /// </summary>
    public Image imgNew;
    /// <summary>
    /// 无 维护 流畅 繁忙 火爆
    /// </summary>
    public Image imgState;
    /// <summary>
    /// 文本信息 31区  天下无双
    /// </summary>
    public Text txtName;
    /// <summary>
    /// 当前选中服务器
    /// </summary>
    public ServerInfo nowServerInfo;
    void Start()
    {
        btnSelf.onClick.AddListener(() =>
        {
            //记录当前选择的服务器
            LoginMgr.Instance.LoginData.frontServerID = nowServerInfo.id;

            //隐藏选服面板
            UIManager.Instance.HidePanel<ChooseServerPanel>(); 
            //显示服务器面板
            UIManager.Instance.ShowPanel<ServerPanel>();
        });
    }
    
    /// <summary>
    /// 单个服务器初始化按钮(外部可用)
    /// </summary>
    /// <param name="info">对应服务器</param>
    public void InitInfo(ServerInfo info)
    {
        //关联对应服务器
        nowServerInfo = info;

        txtName.text = info.id + "区  " + info.name;
        imgNew.gameObject.SetActive(info.isNew);
        imgState.gameObject.SetActive(true);
        //加载图集
        SpriteAtlas sa = Resources.Load<SpriteAtlas>("Login");
        switch (info.state)
        {
            case 0:
                imgState.gameObject.SetActive(false);
                break;
            case 1:
                imgState.sprite = sa.GetSprite("ui_DL_liuchang_01");
                break;
            case 2:
                imgState.sprite = sa.GetSprite("ui_DL_fanhua_01");
                break;
            case 3:
                imgState.sprite = sa.GetSprite("ui_DL_huobao_01");
                break;
            case 4:
                imgState.sprite = sa.GetSprite("ui_DL_weihu_01");
                break;
        }
    }
}
