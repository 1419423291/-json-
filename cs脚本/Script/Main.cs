using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region ���Դ���
        //TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
        //tipPanel.ChangeInfo("������ʾ����");
        //��ʾ����ͼ���
        UIManager.Instance.ShowPanel<LoginBKPanel>();
        //��ʾ��¼���
        UIManager.Instance.ShowPanel<LoginPanel>();
        print(Application.persistentDataPath);
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
