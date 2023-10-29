using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance = instance;
    /// <summary>
    /// 单次存储面板的容器,用于动态显隐面板
    /// </summary>
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    private Transform canvasTrans;
    private UIManager() //不允许外部实例化
    {
        canvasTrans = GameObject.Find("Canvas").transform;
        GameObject.DontDestroyOnLoad(canvasTrans.gameObject);//过场景不能删除Canvas,仅动态创建或删除来显示隐藏面板
    }

    /// <summary>
    /// 显示面板 里面写入了panelDic相关逻辑方便获取继承BasePanel的子类
    /// </summary>
    /// <param name="name">面板名字</param>
    /// <returns></returns>
    public T ShowPanel<T>() where T : BasePanel
    {
        //给MainCamera挂载
        
        //必须保证泛型类名和其面板类型一致
        string panelName = typeof(T).Name;

        //如果存在面板则直接返回 反之则实例化即可
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        #region 此处不调用ShowMe函数原因
        //此处不调用ShowMe函数是因为已存在具体面板,BasePanel的Update函数中自动调用ShowMe函数
        //且未显示具体面板的时候该面板的isShow默认是false的所以不需要使用ShowMe函数进行淡入
        #endregion

        //不存在面板,动态创建面板(这也是调用ShowMe函数的原因)
        //显示面板即动态创建面板预设体 设置父对象
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        panelObj.transform.SetParent(canvasTrans, false);
        //得到对应的面板脚本存储
        T panel = panelObj.GetComponent<T>();
        panelDic.Add(panelName, panel);
        panel.ShowMe();
        return panel;
    }

    /// <summary>
    /// 隐藏面板 isFade为默认淡出效果
    /// </summary>
    /// <typeparam name="T">具体面板类型</typeparam>
    /// <param name="isFade">是否淡出</param>
    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        //根据泛型类型得到面板名字
        string panelName = typeof(T).Name;
        //判断是否存在
        if (panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {
                //匿名回调函数,会先让具体面板执行一次HideMe函数
                panelDic[panelName].HideMe(() =>
                {
                    //面板淡出成功后删除面板
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    //删除面板后从字典中移除
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                //不淡出(不执行HideMe函数)
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }
    }

    /// <summary>
    /// 获得面板
    /// </summary>
    /// <typeparam name="T">具体面板</typeparam>
    /// <returns></returns>
    /// //此处因为在ShowMe函数中写好了panelDic相关逻辑所以可以直接使用该方法获取具体面板
    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        return null;//不存在则返回空
    }
}
