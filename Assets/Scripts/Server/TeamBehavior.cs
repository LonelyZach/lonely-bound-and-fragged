using UnityEngine;
using System.Collections;

public class TeamBehavior : MonoBehaviour
{
  public GameObject Avatar0;
  public GameObject Avatar1;

  private bool isTeamAlive = true;

  // Use this for initialization
  void Start()
  {
    //Doing this works because the lazer doesn't have 'start' called until the first game 
    //update after the one that initializes this one
    var lazer = (GameObject)Instantiate(Resources.Load("Lazer"));
    var lazerBehavior = lazer.GetComponent<LazerBehaviour>();
    lazerBehavior.Tether(Avatar0, Avatar1);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
