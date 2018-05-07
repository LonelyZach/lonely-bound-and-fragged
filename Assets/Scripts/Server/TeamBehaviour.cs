using UnityEngine;
using System.Collections;

public class TeamBehaviour : MonoBehaviour
{
  public GameObject Avatar0;
  public GameObject Avatar1;

  private AvatarBehaviour avatar0Behavior;
  private AvatarBehaviour avatar1Behavior;

  private bool isTeamAlive = true;

  // Use this for initialization
  void Start()
  {
    //Doing this works because the lazer doesn't have 'start' called until the first game 
    //update after the one that initializes this one
    var lazer = (GameObject)Instantiate(Resources.Load("Lazer"));
    var lazerBehavior = lazer.GetComponent<LazerBehaviour>();

    lazerBehavior.Tether(Avatar0, Avatar1);

    avatar0Behavior = Avatar0.GetComponent<AvatarBehaviour>();
    avatar1Behavior = Avatar1.GetComponent<AvatarBehaviour>();
  }

  // Update is called once per frame
  void Update()
  {
    //Update status of the avatar being alive or dead.
    //This can probably be updated with something more elegant like a listener system
    //if we decide we need it.
    if(!avatar0Behavior.IsAlive() && !avatar1Behavior.IsAlive())
    {
      isTeamAlive = false;
    }
  }

  public bool IsTeamAlive()
  {
    return isTeamAlive;
  }
}
