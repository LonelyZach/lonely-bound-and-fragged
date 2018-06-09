using UnityEngine;
using UnityEngine.Networking;

public class TeamBehaviour : NetworkBehaviour
{
  public GameObject LazerPrefab;

  public GameObject Avatar0;
  public GameObject Avatar1;

  private AvatarBehaviour avatar0Behaviour;
  private AvatarBehaviour avatar1Behaviour;

  private bool isTeamAlive = true;

  public bool IsTeamAlive { get { return isTeamAlive; } }

  // Use this for initialization
  void Start()
  {
    //Doing this works because the lazer doesn't have 'start' called until the first game 
    //update after the one that initializes this one

    LazerBehaviour lazerBehaviour = LazerPrefab.GetComponent<LazerBehaviour>();
    lazerBehaviour.Avatar0 = Avatar0;
    lazerBehaviour.Avatar1 = Avatar1;
    lazerBehaviour.Tether(Avatar0, Avatar1);

    var lazer = (GameObject)Instantiate(LazerPrefab, new Vector3(0, 0), Quaternion.identity);
    NetworkServer.Spawn(lazer);

    avatar0Behaviour = Avatar0.GetComponent<AvatarBehaviour>();
    avatar1Behaviour = Avatar1.GetComponent<AvatarBehaviour>();
  }

  // Update is called once per frame
  void Update()
  {
    //Update status of the avatar being alive or dead.
    //This can probably be updated with something more elegant like a listener system
    //if we decide we need it.
    if (!avatar0Behaviour.IsAlive && !avatar1Behaviour.IsAlive)
    {
      isTeamAlive = false;
    }
  }

  public void ResetGame()
  {
    isTeamAlive = true;
    avatar0Behaviour.ReturnToStartPosition();
    avatar1Behaviour.ReturnToStartPosition();
    avatar0Behaviour.Rpc_Resurect();
    avatar1Behaviour.Rpc_Resurect();
  }
}
