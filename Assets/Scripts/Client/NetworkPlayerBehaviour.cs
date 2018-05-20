﻿using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerBehaviour : NetworkBehaviour {

  public string HorizontalJoystickAxisName;
  public string VerticalJoystickAxisName;

  public AvatarBehaviour AssociatedAvatarBehaviour;

  public override void OnStartServer()
  {
    base.OnStartServer();

    Cmd_LinkToAvatar();
  }

  // Update is called once per frame
  void Update()
  {
    if(Network.isServer)
    {
      return;
    }

    float x = Input.GetAxis(HorizontalJoystickAxisName);
    float y = Input.GetAxis(VerticalJoystickAxisName);


    var unit = new Vector2(x, y);

    if (unit.magnitude > 1.0f)
    {
      unit.Normalize();
    }

    if (Mathf.Abs(x) <= 0.01f && Mathf.Abs(y) <= 0.01f)
    {
      Cmd_SetPlayerDrivenMovement(float.NaN, 0.0f);
    }
    else
    {
      var degrees = Mathf.Rad2Deg * Mathf.Atan2(unit.y, unit.x);
      if (degrees < 0)
      {
        degrees += 360.0f;
      }

      if (unit.magnitude >= 0.01f)
      {
        Cmd_SetPlayerDrivenMovement(degrees, unit.magnitude);
      }
    }
  }

  [Command]
  protected void Cmd_LinkToAvatar()
  {
    var networkPlayers = FindObjectsOfType<NetworkPlayerBehaviour>();
    var avatars = FindObjectsOfType<AvatarBehaviour>();

    var uncontrolledAvatars = avatars.Where(a => !networkPlayers.Any(p => p.AssociatedAvatarBehaviour == a));

    if (!uncontrolledAvatars.Any())
    {
      Debug.LogError("Found no uncontrolled avatar for the player.");
    }

    AssociatedAvatarBehaviour = uncontrolledAvatars.First();
  }

  [Command]
  protected void Cmd_SetPlayerDrivenMovement(float angleOfForce, float intensity)
  {
    Debug.Log(string.Format("aof={0} intensity={1}", angleOfForce, intensity));
    intensity = Mathf.Clamp(intensity, 0.0f, 1.0f);
    AssociatedAvatarBehaviour.SetPlayerDrivenMovement(angleOfForce, intensity);
  }
}