using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

/// <summary>
/// UI管理器
/// </summary>
public class UICon : HelperXML
{
    public GameObject UI_Info; //游戏结束图片//计分板
    public Text scoreText;     //得分版
    public Text countText;     //计分板
    public static int nowScore;
    private void Start()
    {
        UIGameVoer();
    }
  
    // 游戏结束显示相关UI
   
    public void UIGameVoer()
    {
        //显示记分板
        UI_Info.SetActive(true);
       
        //遍历获取XML
        List<GameInfoModel> list = base.VCLoadXMLManage();
       
        //当前分数
        //int nowScore = GameManager.score;
        //添加当前分数信息
        var mod = new GameInfoModel
        {
            Score = nowScore,
            Time = DateTime.Now.ToString("MM月dd日")
        };
        list.Add(mod);
        
        //倒序
        list = list.OrderByDescending(p => p.Score).ToList();
        
        //删除最后一个最少的
        if (list.Count > 5)
            list.RemoveAt(list.Count - 1);
        
        //显示最新排名版
        StringBuilder sb = new StringBuilder();
        int index = 1;

        list.ForEach(p => sb.Append(p.Score == mod.Score ?
            ("<color=#ff0000ff>" + index++ + "    " + p.Score + "分    " + p.Time + "</color>\n") :
            (index++ + "    " + p.Score + "分    " + p.Time + "\n\n\n"))
            );

        countText.text = sb.ToString();
        //更新XML
        base.VCRemoveXmlElement();
        base.VCAddXml(list);
    }

    //跳转到主界面
    public void BackButton()
    {
   
        SceneManager.LoadScene("Index");
    }
}
