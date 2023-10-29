using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单个服务器
/// </summary>
public class ServerInfo 
{
    /// <summary>
    /// 区号 1区
    /// </summary>
    public int id;
    /// <summary>
    /// 服务器名 天下无双
    /// </summary>
    public string name;
    /// <summary>
    /// 服务器状态: 无 流畅 繁忙 火爆 维护 
    /// </summary>
    public int state;
    /// <summary>
    /// 是否是新服
    /// </summary>
    public bool isNew;
}
