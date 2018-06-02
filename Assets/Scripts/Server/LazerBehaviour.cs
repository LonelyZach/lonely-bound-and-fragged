using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class LazerBehaviour : NetworkBehaviour {

  public int SpringDistance = 3;
  public int SpringFrequency = 1;

  private GameObject _avatar0;
  private GameObject _avatar1;

	// Use this for initialization
	void Start () {
  }
	
	// Update is called once per frame
	void Update () {
    KillAvatarsInLazer();
  }

  /// <summary>
  /// Sets the two avatars to be tethered together by this laser
  /// </summary>
  /// <param name="avatar0"></param>
  /// <param name="avatar1"></param>
  public void Tether(GameObject avatar0, GameObject avatar1)
  {
    _avatar0 = avatar0;
    _avatar1 = avatar1;

    // Spring joints need to be on one of the objects being joined, so, we'll create it here.
    CreateSpringJoint();
  }

  private void CreateSpringJoint()
  {
    var springJoint = _avatar0.AddComponent<SpringJoint2D>();
    springJoint.connectedBody = _avatar1.gameObject.GetComponent<Rigidbody2D>();
    springJoint.autoConfigureDistance = false;
    springJoint.autoConfigureConnectedAnchor = false;
    springJoint.enableCollision = true;
    springJoint.distance = SpringDistance;
    springJoint.frequency = SpringFrequency;
  }

  private void KillAvatarsInLazer()
  {
    foreach(var avatar in FindAvatarsInLazer())
    {
      avatar.Kill();
    }
  }

  private IEnumerable<AvatarBehaviour> FindAvatarsInLazer()
  {
    return Physics2D.LinecastAll(_avatar0.transform.position, _avatar1.transform.position)
      .Select(x => x.collider.gameObject)
      .Where(x => x != _avatar0 && x != _avatar1)
      .Select(x => x.GetComponent<AvatarBehaviour>())
      .Where(x => x != null);
  }
}
