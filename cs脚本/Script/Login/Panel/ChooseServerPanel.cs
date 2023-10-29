using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ChooseServerPanel : BasePanel
{
    //左右滚动视图
    public ScrollRect svLeft;
    public ScrollRect svRight;
    //上次登录服务器信息
    public Text txtName;
    public Image imgState;
    //当前选择服务器区间
    public Text txtRange;
    /// <summary>
    /// 存储选服面板右侧动态按钮(分析:面板左侧不需要动态更新右侧需要动态更新)
    /// </summary>
    private List<GameObject> itemList=new List<GameObject>();

    public override void Init()
    {
        //动态创建左侧按钮:获取服务器数据
        List<ServerInfo> infoList = LoginMgr.Instance.ServerData;
        int num = (infoList.Count / 5) + 1;//区间数
        for (int i = 0; i < num; i++)
        {
            GameObject item = Instantiate(Resources.Load<GameObject>("UI/ServerLeftItem"));//创建单个服务器预设体
            item.transform.SetParent(svLeft.content, false);//设置父对象
            ServerLeftItem serverLeft = item.GetComponent<ServerLeftItem>();
            int beginIndex = i * 5 + 1;
            int endIndex = i * 5 + 5;
            if (endIndex > infoList.Count)
                endIndex = infoList.Count;
            serverLeft.InitInfo(beginIndex, endIndex);
        }
    }

    public override void ShowMe()
    {
        base.ShowMe();
        #region 更新：上次选择的服务器数据
        //显示自己的时候更新：上次选择的服务器数据
        //更新当前选择的服务器
        int id = LoginMgr.Instance.LoginData.frontServerID;
        if( id <= 0 ) 
        {
            //没有选择
            txtName.text = "无";
            imgState.gameObject.SetActive(false);
        }
        else
        {
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            txtName.text = info.id + "区 " + info.name;
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
        #endregion
        #region 更新：服务器区间提示
        //判断是否小于5个服务器
        UpdatePanel(1, 5 > LoginMgr.Instance.ServerData.Count ? LoginMgr.Instance.ServerData.Count : 5);
        #endregion
    }

    /// <summary>
    /// ChoosePanel内更新当前选择左侧区间的右侧按钮(外部可用)
    /// </summary>
    /// <param name="beginIndex"></param>
    /// <param name="endIndex"></param>
    public void UpdatePanel(int beginIndex,int endIndex)
    {
        #region 更新服务器区间提示
        //更新服务器区间提示
        txtRange.text = "服务器 " + beginIndex + "-" + endIndex;
        //删除之前的单个按钮（注：itemList是当前存储的服务器列表）
        for (int i = 0; i < itemList.Count; i++)
        {
            //删除之前的服务器对象
            Destroy(itemList[i]);
        }
        //清空列表
        itemList.Clear();
        #endregion
        #region 创建新按钮
        //创建新按钮
        for (int i = beginIndex; i <= endIndex; i++)
        {
            //获取服务器信息
            ServerInfo nowInfo = LoginMgr.Instance.ServerData[i - 1];
            //动态创建预设体
            GameObject serverItem = Instantiate(Resources.Load<GameObject>("UI/ServerRightItem"));
            serverItem.transform.SetParent(svRight.content, false);
            //使用ServerRightItem的InitInfo方法初始化
            ServerRightItem rightItem=serverItem.GetComponent<ServerRightItem>();
            rightItem.InitInfo(nowInfo);
            //创建成功后将其记录到itemList列表中
            itemList.Add(serverItem);
        }
        #endregion
    }
}
