using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainManage : MonoBehaviour 
{
    private Button btn;
    public static string LoadScreneName;
	private void Start()
	{
        btn = GameObject.Find("Start").GetComponent<Button>();
        btn.onClick.AddListener(() =>{
            LoadScreneName = "GameScrene";
            SceneManager.LoadScene("LoadingScrene");
        });
	}

}
