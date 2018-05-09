using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBehaviour : NetworkBehaviour {

  public GameObject AvatarToControl;

  protected AvatarBehaviour _associatedAvatarBehaviour;

	// Use this for initialization
	void Start ()
  {
    FindAssociatedAvatarBehaviour();
	}
	
	// Update is called once per frame
	void Update () {
  }

  protected void FindAssociatedAvatarBehaviour()
  {
    _associatedAvatarBehaviour = AvatarToControl.GetComponent<AvatarBehaviour>();
  }

  // [Command] - enable for netwokring
  protected void Cmd_SetPlayerDrivenMovement(float angleOfForce)
  {
    _associatedAvatarBehaviour.SetPlayerDrivenMovement(angleOfForce);
  }
}
