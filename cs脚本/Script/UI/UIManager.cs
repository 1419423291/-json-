using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance = instance;
    /// <summary>
    /// ���δ洢��������,���ڶ�̬�������
    /// </summary>
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    private Transform canvasTrans;
    private UIManager() //�������ⲿʵ����
    {
        canvasTrans = GameObject.Find("Canvas").transform;
        GameObject.DontDestroyOnLoad(canvasTrans.gameObject);//����������ɾ��Canvas,����̬������ɾ������ʾ�������
    }

    /// <summary>
    /// ��ʾ��� ����д����panelDic����߼������ȡ�̳�BasePanel������
    /// </summary>
    /// <param name="name">�������</param>
    /// <returns></returns>
    public T ShowPanel<T>() where T : BasePanel
    {
        //��MainCamera����
        
        //���뱣֤�������������������һ��
        string panelName = typeof(T).Name;

        //������������ֱ�ӷ��� ��֮��ʵ��������
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        #region �˴�������ShowMe����ԭ��
        //�˴�������ShowMe��������Ϊ�Ѵ��ھ������,BasePanel��Update�������Զ�����ShowMe����
        //��δ��ʾ��������ʱ�������isShowĬ����false�����Բ���Ҫʹ��ShowMe�������е���
        #endregion

        //���������,��̬�������(��Ҳ�ǵ���ShowMe������ԭ��)
        //��ʾ��弴��̬�������Ԥ���� ���ø�����
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        panelObj.transform.SetParent(canvasTrans, false);
        //�õ���Ӧ�����ű��洢
        T panel = panelObj.GetComponent<T>();
        panelDic.Add(panelName, panel);
        panel.ShowMe();
        return panel;
    }

    /// <summary>
    /// ������� isFadeΪĬ�ϵ���Ч��
    /// </summary>
    /// <typeparam name="T">�����������</typeparam>
    /// <param name="isFade">�Ƿ񵭳�</param>
    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        //���ݷ������͵õ��������
        string panelName = typeof(T).Name;
        //�ж��Ƿ����
        if (panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {
                //�����ص�����,�����þ������ִ��һ��HideMe����
                panelDic[panelName].HideMe(() =>
                {
                    //��嵭���ɹ���ɾ�����
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    //ɾ��������ֵ����Ƴ�
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                //������(��ִ��HideMe����)
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }
    }

    /// <summary>
    /// ������
    /// </summary>
    /// <typeparam name="T">�������</typeparam>
    /// <returns></returns>
    /// //�˴���Ϊ��ShowMe������д����panelDic����߼����Կ���ֱ��ʹ�ø÷�����ȡ�������
    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        return null;//�������򷵻ؿ�
    }
}
