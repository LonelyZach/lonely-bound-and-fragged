using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameMasterBehaviour : NetworkBehaviour
{
  public GameObject DefaultTeam;

  public GameObject DefaultPlayerAvatar;

  public int PlacementRadius;

  private List<TeamBehaviour> _teamBehaviorList = new List<TeamBehaviour>();

  private List<GameObject> _networkPlayers = new List<GameObject>();

  private List<GameObject> _avatars = new List<GameObject>();

  private int numberOfLobbyPlayers = 0;

  private bool isInitialized = false;

  // Use this for initialization
  void Start()
  {
    numberOfLobbyPlayers = LobbyManager.singleton.numPlayers;
  }

  void Initialize()
  {
    int numberOfCreatedPlayers = 0;
    NetworkPlayerBehaviour[] allNetworkPlayers = new NetworkPlayerBehaviour[0];

    allNetworkPlayers = GameObject.FindObjectsOfType<NetworkPlayerBehaviour>();
    numberOfCreatedPlayers = allNetworkPlayers.Length;

    if(numberOfCreatedPlayers < numberOfLobbyPlayers)
    {
      //We can't initialize yet!
      return;
    }

    if (allNetworkPlayers.Length % 2 != 0)
    {
      Debug.LogError("WTF, why is there an uneven number of players? There are: " + allNetworkPlayers.Length.ToString());
    }

    int count = allNetworkPlayers.Length;

    float segment = (Mathf.PI * 2) / count;

    int iteration = 0;


    //Spawn an avatar for each player in a circular pattern
    foreach (NetworkPlayerBehaviour player in allNetworkPlayers)
    {
      float x = PlacementRadius * Mathf.Cos(segment * iteration);
      float y = PlacementRadius * Mathf.Sin(segment * iteration);
      var avatarBehaviour = DefaultPlayerAvatar.GetComponent<AvatarBehaviour>();
      avatarBehaviour.startColor = player.avatarColor;
      GameObject newAvatar = (GameObject)Instantiate(DefaultPlayerAvatar, new Vector3(x, y), Quaternion.identity);
      NetworkServer.Spawn(newAvatar);
      
      player.AssociatedAvatarBehaviour = newAvatar.GetComponent<AvatarBehaviour>();
      _avatars.Add(newAvatar);
      player.IsPlayerReady = true;

      ++iteration;
    }

    for (int i = 0; i < _avatars.Count; i += 2)
    {
      var teamBehaviour = DefaultTeam.GetComponent<TeamBehaviour>();
      teamBehaviour.Avatar0 = _avatars[i];
      teamBehaviour.Avatar1 = _avatars[i + 1];

      GameObject team = (GameObject)Instantiate(DefaultTeam, new Vector3(0,0), Quaternion.identity);
      NetworkServer.Spawn(team);

      _teamBehaviorList.Add(team.GetComponent<TeamBehaviour>());
    }

    isInitialized = true;
  }

  // Update is called once per frame
  void Update()
  {
    //Check to see if we are actually running yet, and initialize if not
    if (!isInitialized)
    {
      Initialize();
      return;
    }

    //Check for how many teams are still alive
    int livingTeamCount = 0;
    foreach (var teamBehavior in _teamBehaviorList)
    {
      if (teamBehavior.IsTeamAlive)
      {
        livingTeamCount++;
      }
    }

    //If only 1 or fewer teams left, reset the game.
    //This will be a spot for future expansion for a victory screen or something
    if (livingTeamCount <= 1)
    {
      StartCoroutine(ReturnToLobby());
    }
  }

  IEnumerator ReturnToLobby()
  {
    //Then we need to change the scene back to the main menu
    yield return new WaitForSeconds(3.0f);
    foreach(var lobbyPlayer in LobbyManager.s_Singleton.lobbySlots)
    {
      lobbyPlayer.readyToBegin = false;
    }
    LobbyManager.s_Singleton.ServerReturnToLobby();
  }
}

