using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMasterBehaviour : MonoBehaviour
{
  public List<GameObject> Teams;

  private List<TeamBehaviour> _teamBehaviorList = new List<TeamBehaviour>();

  // Use this for initialization
  void Start()
  {
    foreach (var team in Teams)
    {
      _teamBehaviorList.Add(team.GetComponent<TeamBehaviour>());
    }
  }

  // Update is called once per frame
  void Update()
  {
    //Check for how many teams are still alive
    int livingTeamCount = 0;
    foreach (var teamBehavior in _teamBehaviorList)
    {
      if (teamBehavior.IsTeamAlive())
      {
        livingTeamCount++;
      }
    }

    //If only 1 or fewer teams left, reset the game.
    //This will be a spot for future expansion for a victory screen or something
    if(livingTeamCount <= 1)
    {
      //Then we need to change the scene back to the main menu
      SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
  }
}
