using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class LazerBehaviour : NetworkBehaviour
{

  public int SpringDistance = 3;
  public int SpringFrequency = 1;

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
      KillAvatarsInLazer();
    }
  }


  private void Draw()
  {
    var renderer = gameObject.GetComponent<LineRenderer>();
    renderer.SetPosition(0, new Vector3(Avatar0_x, Avatar0_y));
    renderer.SetPosition(1, new Vector3(Avatar1_x, Avatar1_y));
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
  }

  private void KillAvatarsInLazer()
  {
    foreach (var avatar in FindAvatarsInLazer())
    {
      //avatar.Kill();
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
