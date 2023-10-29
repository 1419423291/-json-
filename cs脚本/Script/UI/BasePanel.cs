using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    //整体控制淡入淡出的画布组件 
    private CanvasGroup canvasGroup;
    //淡入淡出速度
    private float alphaSpeed = 10;
    private bool isShow;//是否开始显示
    public UnityAction hideCallBack;//淡出成功时执行的委托函数

    protected virtual void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
        if(canvasGroup == null)
            canvasGroup=this.gameObject.AddComponent<CanvasGroup>();
    }

    //子类可重写 
    protected virtual void Start()
    {
        Init();
    }

    /// <summary>
    /// 主要用于初始化 按钮事件监听等内容
    /// </summary>
    public abstract void Init();

    public virtual void ShowMe()
    {
        isShow=true;
        canvasGroup.alpha = 0;
    }
    /// <summary>
    /// BasePanel基类中的隐藏函数
    /// </summary>
    /// <param name="callBack">UIManager中 
    /// callBack委托回调函数作为需要淡出的"成立"逻辑</param>
    public virtual void HideMe(UnityAction callBack)
    {
        isShow = false;
        canvasGroup.alpha = 1;
        //记录传入淡出的委托函数(删除自己)
        hideCallBack = callBack;
    }
    void Update()
    {
        if( isShow && canvasGroup.alpha != 1 )
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
                //UI管理器创建自己
            }
        }
        else if (!isShow)
        {
            canvasGroup.alpha -=alphaSpeed * Time.deltaTime;
            if(canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                //UI管理器删除自己(不为空时执行)
                hideCallBack?.Invoke();
            }
        }
    }

}
