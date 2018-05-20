using UnityEngine;
using UnityEngine.Networking;

public class TeamBehaviour : NetworkBehaviour
{
  public GameObject LazerPrefab;
  public GameObject LazerRendererPrefab;

  public GameObject Avatar0;
  public GameObject Avatar1;

  private AvatarBehaviour avatar0Behavior;
  private AvatarBehaviour avatar1Behavior;

  private bool isTeamAlive = true;

  public bool IsTeamAlive { get { return isTeamAlive; } }

  // Use this for initialization
  void Start()
  {
    //Doing this works because the lazer doesn't have 'start' called until the first game 
    //update after the one that initializes this one
    var lazer = Instantiate(LazerPrefab);
    var lazerBehaviour = lazer.GetComponent<LazerBehaviour>();
    lazerBehaviour.Tether(Avatar0, Avatar1);

    var lazerRenderer = Instantiate(LazerRendererPrefab);
    var lazerRendererBehaviour = lazerRenderer.GetComponent<LazerRendererBehaviour>();
    lazerRendererBehaviour.Avatar0 = Avatar0;
    lazerRendererBehaviour.Avatar1 = Avatar1;

    avatar0Behavior = Avatar0.GetComponent<AvatarBehaviour>();
    avatar1Behavior = Avatar1.GetComponent<AvatarBehaviour>();
  }

  // Update is called once per frame
  void Update()
  {
    //Update status of the avatar being alive or dead.
    //This can probably be updated with something more elegant like a listener system
    //if we decide we need it.
    if (!avatar0Behavior.IsAlive && !avatar1Behavior.IsAlive)
    {
      isTeamAlive = false;
    }
  }
}
