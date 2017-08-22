using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class ScreneManage : MonoBehaviour 
{
    private GameObject WallPrefeb;
    public static bool isPause;
    [SerializeField] public static MapSize size;
    public static ScreneManage gameManager;
    public static Transform[,] blocks;
    public event Action onUpdateTime;
    public static float delay;

    private void Awake()
    {
        delay = 1;
        size.x = 10;
        size.y = 20;
        isPause = false;
        blocks = new Transform[size.x+1, size.y+2];
        WallPrefeb = Resources.Load<GameObject>("Prefabs/Wall");
        gameManager = this;
        initMap();
    }
    private void Update()
    {
        if (!isPause && onUpdateTime != null)
        {
            onUpdateTime();
        }
    }
    private void initMap()
    {
        GameObject Walls = new GameObject();
        Walls.name = "Walls";
        for (int i = 0; i < size.x+2; i++)
        {
            for (int j = 0; j < size.y+1; j++)
            {
                if (j==0||i==0||i==size.x+1)
                {
                    GameObject copy = Instantiate(WallPrefeb);
                    copy.name = String.Format("Wall({0},{1})",i,j);
                    copy.transform.position = new Vector3(i,j);
                    if (j==0)
                    {
                        copy.transform.tag = "Bottom";
                    }
                    else
                    {
                        copy.transform.tag = "Wall";
                    }
                    copy.transform.parent = Walls.transform;
                }
            }
        }
    }
    public static void PasueGame()
    {
        Debug.Log("游戏暂停");
        isPause = true;
        Time.timeScale = 0;
    }
    public static void Continue()
    {
        Debug.Log("游戏继续");
        isPause = false;
        Time.timeScale = 1;
    }
  

}
