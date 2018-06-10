using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TeamBehaviour : NetworkBehaviour
{
  public GameObject LazerPrefab;

  public GameObject Avatar0;
  public GameObject Avatar1;

  private AvatarBehaviour avatar0Behaviour;
  private AvatarBehaviour avatar1Behaviour;

  public IEnumerable<AvatarBehaviour> AvatarBehaviours
  {
    get
    {
      yield return avatar0Behaviour;
      yield return avatar1Behaviour;
    }
  }

  public bool IsTeamAlive { get { return avatar0Behaviour.IsAlive || avatar1Behaviour.IsAlive; } }

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
  }

  public void ResetGame()
  {
    avatar0Behaviour.ReturnToStartPosition();
    avatar1Behaviour.ReturnToStartPosition();
    avatar0Behaviour.Rpc_Resurect();
    avatar1Behaviour.Rpc_Resurect();
  }
}
