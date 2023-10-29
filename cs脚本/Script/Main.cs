using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 测试代码
        //TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
        //tipPanel.ChangeInfo("测试提示内容");
        //显示背景图面板
        UIManager.Instance.ShowPanel<LoginBKPanel>();
        //显示登录面板
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
