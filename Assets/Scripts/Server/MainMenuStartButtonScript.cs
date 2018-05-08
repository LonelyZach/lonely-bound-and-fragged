using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuStartButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
    var button = this.GetComponent<Button>();
    button.onClick.AddListener(TaskOnClick);

  }
	
  /// <summary>
  /// Execute the scene change
  /// </summary>
  void TaskOnClick()
  {
    Debug.Log("Clicked Start Button!");
    SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
  }
}
