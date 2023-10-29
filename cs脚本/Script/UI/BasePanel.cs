using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    //������Ƶ��뵭���Ļ������ 
    private CanvasGroup canvasGroup;
    //���뵭���ٶ�
    private float alphaSpeed = 10;
    private bool isShow;//�Ƿ�ʼ��ʾ
    public UnityAction hideCallBack;//�����ɹ�ʱִ�е�ί�к���

    protected virtual void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
        if(canvasGroup == null)
            canvasGroup=this.gameObject.AddComponent<CanvasGroup>();
    }

    //�������д 
    protected virtual void Start()
    {
        Init();
    }

    /// <summary>
    /// ��Ҫ���ڳ�ʼ�� ��ť�¼�����������
    /// </summary>
    public abstract void Init();

    public virtual void ShowMe()
    {
        isShow=true;
        canvasGroup.alpha = 0;
    }
    /// <summary>
    /// BasePanel�����е����غ���
    /// </summary>
    /// <param name="callBack">UIManager�� 
    /// callBackί�лص�������Ϊ��Ҫ������"����"�߼�</param>
    public virtual void HideMe(UnityAction callBack)
    {
        isShow = false;
        canvasGroup.alpha = 1;
        //��¼���뵭����ί�к���(ɾ���Լ�)
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
                //UI�����������Լ�
            }
        }
        else if (!isShow)
        {
            canvasGroup.alpha -=alphaSpeed * Time.deltaTime;
            if(canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                //UI������ɾ���Լ�(��Ϊ��ʱִ��)
                hideCallBack?.Invoke();
            }
        }
    }

}
