using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMasterBehavior : MonoBehaviour
{
  public List<GameObject> Teams;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    foreach (var Team in Teams)
    {
      if (!Team.GetComponent<TeamBehavior>().IsTeamAlive())
      {
        //Then we need to change the scene back to the main menu

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
      }
    }
  }
}
