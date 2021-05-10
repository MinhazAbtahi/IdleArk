using UnityEngine;
using UnityEngine.UI;
 
public class RadialProgress : MonoBehaviour
{
	public GameObject LoadingText;
	public Text ProgressIndicator;
	public Image LoadingBar;
	float currentValue;
	public float duration;

	public bool done;

	// Use this for initialization
	void Start()
	{

	}
	int i = 0;
	// Update is called once per frame
	void Update()
	{
		transform.LookAt(Camera.main.transform);
		if (currentValue < duration)
		{
			currentValue += /*duration **/ Time.deltaTime;
			//ProgressIndicator.text = ((int)currentValue).ToString() + "%";
			//LoadingText.SetActive(true);
		}
		else
		{
            if (!done)
            {
				done = true;
				Debug.Log(i);
				GameManager.instance.playerCon.FinishBuild(i);
				currentValue = 0f;
				++i;
            }
			//LoadingText.SetActive(false);
			//ProgressIndicator.text = "Done";
		}

		LoadingBar.fillAmount = currentValue / duration;
	}
}