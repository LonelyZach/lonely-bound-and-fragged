using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerBehaviour : NetworkBehaviour {

  public string HorizontalJoystickAxisName;
  public string VerticalJoystickAxisName;

  public KeyCode Up;
  public KeyCode Down;
  public KeyCode Left;
  public KeyCode Right;
  
  /// <summary>
  /// This is only going to be set on the server side, the client is totally agnostic to this
  /// </summary>
  private AvatarBehaviour _associatedAvatarBehaviour;
    
  public AvatarBehaviour AssociatedAvatarBehaviour { get { return _associatedAvatarBehaviour; } set { _associatedAvatarBehaviour = value; Debug.Log("Assigned avatar!"); } }

  private NetworkIdentity _networkIdentity;

  public PersistentPlayerData playerData = new PersistentPlayerData();

  /// <summary>
  /// This flag lets the client know that the server has it properly handled
  /// </summary>
  [SyncVar]
  private bool isPlayerReady = false;

  public bool IsPlayerReady { get { return isPlayerReady; }  set { isPlayerReady = value; } }

  private void Start()
  {
    _networkIdentity = GetComponent<NetworkIdentity>();
  }

  // Update is called once per frame
  void Update()
  {
    if(!_networkIdentity.hasAuthority || !IsPlayerReady)
    {
      return;
    }

    //Keyboard > joystick. Try this first
    var keyboardForce = GetKeyboardAngleOfForce();

    if(!float.IsNaN(keyboardForce))
    {
      Cmd_SetPlayerDrivenMovement(keyboardForce, 1.0f);
      return;
    }

    //If no keyboard, then run the joystick checks
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

  /// <summary>
  /// Copy pasted from the keyboardcontrollerbehaviour script. One will have to die eventually
  /// </summary>
  /// <returns></returns>
  float GetKeyboardAngleOfForce()
  {
    var isUp = Input.GetKey(Up);
    var isDown = Input.GetKey(Down);
    var isLeft = Input.GetKey(Left);
    var isRight = Input.GetKey(Right);

    //Angle in degrees
    float angleOfForce = float.NaN;

    //First cancel out opposing key presses
    if (isUp && isDown)
    {
      isUp = false;
      isDown = false;
    }
    if (isLeft && isRight)
    {
      isLeft = false;
      isRight = false;
    }

    if (isRight)
    {
      if (isDown)
      {
        angleOfForce = 315.0f;
      }
      else if (isUp)
      {
        angleOfForce = 45.0f;
      }
      else
      {
        angleOfForce = 0.0f;
      }
    }
    else if (isLeft)
    {
      if (isDown)
      {
        angleOfForce = 215.0f;
      }
      else if (isUp)
      {
        angleOfForce = 135.0f;
      }
      else
      {
        angleOfForce = 180.0f;
      }
    }
    else if (isDown)
    {
      angleOfForce = 270.0f;
    }
    else if (isUp)
    {
      angleOfForce = 90.0f;
    }
    return angleOfForce;
  }


  //[Command]
  //protected void Cmd_LinkToAvatar()
  //{
  //  var networkPlayers = FindObjectsOfType<NetworkPlayerBehaviour>();
  //  var avatars = FindObjectsOfType<AvatarBehaviour>();

  //  var uncontrolledAvatars = avatars.Where(a => !networkPlayers.Any(p => p.AssociatedAvatarBehaviour == a));

  //  if (!uncontrolledAvatars.Any())
  //  {
  //    Debug.LogError("Found no uncontrolled avatar for the player.");
  //  }

  //  AssociatedAvatarBehaviour = uncontrolledAvatars.First();
  //}

  [Command]
  protected void Cmd_SetPlayerDrivenMovement(float angleOfForce, float intensity)
  {
    Debug.Log(string.Format("aof={0} intensity={1}", angleOfForce, intensity));
    intensity = Mathf.Clamp(intensity, 0.0f, 1.0f);
    _associatedAvatarBehaviour.SetPlayerDrivenMovement(angleOfForce, intensity);
  }

  public override int GetHashCode()
  {
    return playerData.playerId;
  }
}
