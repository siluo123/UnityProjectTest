using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
public class BlockMove : MonoBehaviour
{
    // private static MapSize size = ScreneManage.size;
   private  float lastTime;
    private void Start()
    {
        lastTime = Time.time;
       // StartCoroutine(gogogo());
    }
    private void Update()
    {
        if (!BlockManage.touchBottom)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Vector2 v2 = transform.position;
                transform.position += Vector3.left;
                if (checkOver())
                {
                    transform.position = v2;
                }
                else
                {
                    updateBlocks();
                }
                Debug.Log("左移");
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Vector2 v2 = transform.position;
                transform.position += Vector3.right;
                if (checkOver())
                {
                    transform.position = v2;
                }
                else
                {
                    updateBlocks();
                }
                Debug.Log("右移");
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("旋转");
                transform.Rotate(0, 0, -90);
                if (checkOver())
                {
                    transform.Rotate(0, 0, 90);
                }
                else
                {
                    updateBlocks();
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Debug.Log("下降");
                Vector3 currentPosition = transform.position;
                while (true)
                {
                    if (checkOver())
                    {
                        break;
                    }
                    updateBlocks();
                    currentPosition = transform.position;
                    transform.position += Vector3.down;
                }
                transform.position = currentPosition;
                updateBlocks();
                deleteFulls();
                BlockManage.touchBottom = true;
                gameObject.GetComponent<BlockMove>().enabled = false;
             //   transform.gameObject.

            }
            if (Time.time- lastTime>=1)
            {
                Vector3 position = transform.position;
                transform.position += Vector3.down;
                if (!checkOver())
                {
                    updateBlocks();
                    lastTime = Time.time;
                }
                else
                {
                    transform.position = position;
                    updateBlocks();
                    deleteFulls();
                    BlockManage.touchBottom = true;
                    enabled = false;
                  //  gameObject.GetComponent<BlockMove>().enabled = false;
                }
            }
        }
    }
    private void updateBlocks() {
        for (int i = 1; i <= ScreneManage.size.x; i++)
        {
            for (int j =1; j <= ScreneManage.size.y+1; j++)
            {
                //去掉方块组在 数组中的原有子方块
                if (ScreneManage.blocks[i, j]!=null&& ScreneManage.blocks[i,j].parent==this.transform)
                {
                    ScreneManage.blocks[i,j] = null;
                }
            }
        }
        Debug.Log("准备放入数组中");
        //将新的子方块放入数组中
        foreach (Transform item in transform)
        {
           
            Vector2 v2 = BlockManage.Round(item);
            Debug.Log("坐标为:" + v2 + " ...." + Time.time);
            ScreneManage.blocks[(int)v2.x, (int)v2.y] = item;
            
        }
        for (int i = 0; i <= ScreneManage.size.x; i++)
        {
            for (int j = 0; j <= ScreneManage.size.y+1; j++)
            {
                if (ScreneManage.blocks[i, j] != null)
                {
                    Debug.Log(i + "," + j + "=" + ScreneManage.blocks[i, j] + Time.time);
                }
              
            }
        }
        
    }
    public  bool checkOver()
    {
        foreach (Transform item in transform)
        {
            if (!(item.position.x > 0 && item.position.x <= ScreneManage.size.x && item.position.y > 0))
            {
                Debug.Log("越界");
                return true;
            }
        }
        if (!checkBlock(this.transform))
        {
           
            return true;
        }
        return false;
       
    }
    public  bool checkBlock(Transform transform)
    {
        foreach (Transform item in transform)
        {
            Vector2 v2 = BlockManage.Round(item);
            if (ScreneManage.blocks[(int)v2.x, (int)v2.y] != null)
            {
                //判断
                if (ScreneManage.blocks[(int)v2.x, (int)v2.y].parent != this.transform)
                {
                    Debug.Log(v2+ "已经存在方块");
                    return false;
                }
            }
            
        }
        return true;
    }
    private void deleteFulls()
    { 
            for (int j = 1; j < ScreneManage.size.y; j++)
            {
                if (isFull(j))
                 {
                   Debug.Log("准备删除");
                   deleteFull(j);
                   deCreseBlock(j+1);
                   j--;
                 }
            }
       
    }
    private void deleteFull(int j) {
        for (int i = 1; i < ScreneManage.size.x; i++)
        {
                Destroy(ScreneManage.blocks[i, j].gameObject);
                ScreneManage.blocks[i, j] = null;
        }
    }
    private bool isFull(int j) 
    {
        for (int i = 1; i <= ScreneManage.size.x; i++)
         {
            Debug.Log("检测是否存在空"+ ScreneManage.blocks[i, j]+","+i+":"+j);
            if (ScreneManage.blocks[i, j] == null)
            {
                return false;
            }
        }
        Debug.Log(j+"行满了");
      
        return true;
    }
    //IEnumerator gogogo() {
    //    while (!BlockManage.touchBottom)
    //    {   
    //        Vector3 currentPosition = transform.position;
    //        transform.position += Vector3.down;
    //        if (!checkOver())
    //        {
    //            updateBlocks();
    //        }
    //        else
    //        {
    //            transform.position = currentPosition;
    //            updateBlocks();
    //            deleteFulls();
    //            BlockManage.touchBottom = true;
    //            gameObject.GetComponent<BlockMove>().enabled = false;
    //        }
    //        yield return new WaitForSeconds(0.5f);
    //    }
    //}
    private static void deCreseBlock(int index)
    {
        for (int i = 1; i < ScreneManage.size.x; i++)
        {
            for (int j = index; j < ScreneManage.size.y + 1; j++)
            {
                if (ScreneManage.blocks[i, j] != null)
                {
                    ScreneManage.blocks[i, j - 1] = ScreneManage.blocks[i, j];
                    ScreneManage.blocks[i, j] = null;
                    // Update Block position
                    ScreneManage.blocks[i, j - 1] .position+= new Vector3(0, -1, 0);
                }
            }
        }
    }
}
