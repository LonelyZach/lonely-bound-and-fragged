using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class LazerBehaviour : NetworkBehaviour
{

  public float LineDrawWidth = 0.1f;

  public int SpringDistance = 3;
  public float SpringFrequency = 0.5f;
  public float SpringDampingRatio = 1.0f;

  public GameObject Avatar0;
  public GameObject Avatar1;
  private NetworkIdentity _networkIdentity;

  #region Client variables for local drawing
  //Individual x and y because that's how SyncVar works
  [SyncVar]
  private float Avatar0_x;
  [SyncVar]
  private float Avatar0_y;

  [SyncVar]
  private float Avatar1_x;
  [SyncVar]
  private float Avatar1_y;

  private Vector3 Avatar0Position = Vector3.zero;
  private Vector3 Avatar1Position = Vector3.zero;

  private float updateInterval;
  #endregion

  // Use this for initialization
  void Start()
  {
    _networkIdentity = GetComponent<NetworkIdentity>();
  }

  // Update is called once per frame
  void Update()
  {
    Draw();
    if(_networkIdentity.isServer)
    {
      Avatar0_x = Avatar0.gameObject.transform.position.x;
      Avatar0_y = Avatar0.gameObject.transform.position.y;
      Avatar1_x = Avatar1.gameObject.transform.position.x;
      Avatar1_y = Avatar1.gameObject.transform.position.y;

      Avatar0Position = new Vector3(Avatar0_x, Avatar0_y);
      Avatar1Position = new Vector3(Avatar1_x, Avatar1_y);
      KillAvatarsInLazer();
    }
    //Attempt to do some smoothing on the client side
    else if(_networkIdentity.isClient)
    {
      if(Avatar0Position == Vector3.zero || Avatar1Position == Vector3.zero || updateInterval > 0.11f) //9 times a second
      {
        Avatar0Position = new Vector3(Avatar0_x, Avatar0_y);
        Avatar1Position = new Vector3(Avatar1_x, Avatar1_y);
        updateInterval = 0;
      }
      else
      {
        updateInterval += Time.deltaTime;
        Avatar0Position = Vector3.Lerp(Avatar0Position, new Vector3(Avatar0_x, Avatar0_y), 0.1f);
        Avatar1Position = Vector3.Lerp(Avatar1Position, new Vector3(Avatar1_x, Avatar1_y), 0.1f);
      }
    }
  }


  private void Draw()
  {
    var renderer = gameObject.GetComponent<LineRenderer>();
    renderer.startWidth = LineDrawWidth;
    renderer.endWidth = LineDrawWidth;
    renderer.SetPosition(0, Avatar0Position);
    renderer.SetPosition(1, Avatar1Position);
  }

  /// <summary>
  /// Sets the two avatars to be tethered together by this laser
  /// </summary>
  /// <param name="avatar0"></param>
  /// <param name="avatar1"></param>
  public void Tether(GameObject avatar0, GameObject avatar1)
  {
    Avatar0 = avatar0;
    Avatar1 = avatar1;

    // Spring joints need to be on one of the objects being joined, so, we'll create it here.
    CreateSpringJoint();
  }

  private void CreateSpringJoint()
  {
    var springJoint = Avatar0.AddComponent<SpringJoint2D>();
    springJoint.connectedBody = Avatar1.gameObject.GetComponent<Rigidbody2D>();
    springJoint.autoConfigureDistance = false;
    springJoint.autoConfigureConnectedAnchor = false;
    springJoint.enableCollision = true;
    springJoint.distance = SpringDistance;
    springJoint.frequency = SpringFrequency;
    springJoint.dampingRatio = SpringDampingRatio;
  }

  private void KillAvatarsInLazer()
  {
    foreach (var avatar in FindAvatarsInLazer())
    {
      if (avatar.IsAlive)
      {
        avatar.Kill();
        Avatar0.GetComponent<AvatarBehaviour>().ScoredKill();
        Avatar1.GetComponent<AvatarBehaviour>().ScoredKill();
      }
    }
  }

  private IEnumerable<AvatarBehaviour> FindAvatarsInLazer()
  {
    return Physics2D.LinecastAll(Avatar0.transform.position, Avatar1.transform.position)
      .Select(x => x.collider.gameObject)
      .Where(x => x != Avatar0 && x != Avatar1)
      .Select(x => x.GetComponent<AvatarBehaviour>())
      .Where(x => x != null);
  }
}
