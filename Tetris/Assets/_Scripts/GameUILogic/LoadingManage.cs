using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadingManage : MonoBehaviour 
{
    private Button Continue;
    private Text Loading;
    private Slider slider;
    private AsyncOperation ao;
    
    private void Start()
	{
        Continue = GameObject.Find("Continue").GetComponent<Button>();
        Continue.onClick.AddListener(LoadingScrene);
        Continue.gameObject.SetActive(false);
        Loading = GameObject.Find("Loading").GetComponent<Text>();
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        ao = SceneManager.LoadSceneAsync(MainManage.LoadScreneName);
        ao.allowSceneActivation = false;
    }
	
	private void Update()
	{
        if (ao!=null)
        {
            if (ao.progress<0.9f)
            {
                slider.value = ao.progress;
            }
            else if (ao.progress==0.9f)
            {
                slider.value = 1;
                Continue.gameObject.SetActive(true);
            }
            Loading.text = string.Format("{0} %",slider.value*100);
        }
	}
    private void LoadingScrene() {
        ao.allowSceneActivation = true;
    }
}
