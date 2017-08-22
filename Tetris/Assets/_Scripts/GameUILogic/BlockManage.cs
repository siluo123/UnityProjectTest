using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BlockManage : MonoBehaviour
{
    enum Blocks
    {
        square, TBlock, Long, LeftZ, RightZ, LeftL, RightL
    }
    public static bool touchBottom;
    private GameObject square;
    private GameObject TBlock;
    private GameObject Long;
    private GameObject LeftZ;
    private GameObject RightZ;
    private GameObject LeftL;
    private GameObject RightL;
    private GameObject copy;
    private void Start()
    {
        touchBottom = true;
        square = Resources.Load<GameObject>("Prefabs/square");
        TBlock = Resources.Load<GameObject>("Prefabs/TBlock");
        Long = Resources.Load<GameObject>("Prefabs/Long");
        LeftZ = Resources.Load<GameObject>("Prefabs/LeftZ");
        RightZ = Resources.Load<GameObject>("Prefabs/RightZ");
        LeftL = Resources.Load<GameObject>("Prefabs/LeftL");
        RightL = Resources.Load<GameObject>("Prefabs/RightL");
        ScreneManage.gameManager.onUpdateTime += createBlock;
    }
    private void createBlock()
    {
        if (touchBottom)
        {

            switch (Random.Range(0,7))
            {
                case 0: copy = Instantiate(square); break;
                case 1: copy = Instantiate(TBlock); break;
                case 2: copy = Instantiate(Long); break;
                case 3: copy = Instantiate(LeftZ); break;
                case 4: copy = Instantiate(RightZ); break;
                case 5: copy = Instantiate(LeftL); break;
                case 6: copy = Instantiate(RightL); break;
                default:
                    break;
            }
            foreach (Transform item in copy.transform)
            {
                //确保子方块的坐标都为整数
                Vector2 v2= Round(item);
                item.position = v2;
                Debug.Log(v2);
                ScreneManage.blocks[(int)v2.x, (int)v2.y] = item;
            }
            touchBottom = false;
        }
    }
    public static Vector2 Round(Transform t) {
        return new Vector2(Mathf.RoundToInt(t.position.x), Mathf.RoundToInt(t.position.y));
    }

   
}
