using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// LoginMgr内存放临时注册数据容器(RegisterPanel渲染)。
/// 记录玩家注册成功的账号密码
/// Data都用于记录临时内存中的存储数据
/// </summary>
public class RegisterData 
{
    /// <summary>
    ///注册数据字典(键账号 值密码)。
    /// </summary>
    public Dictionary<string, string> registerInfo = new Dictionary<string, string>();
}
