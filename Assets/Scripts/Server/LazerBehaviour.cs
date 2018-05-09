using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LazerBehaviour : MonoBehaviour {

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
    Draw();
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

  private void Draw()
  {
    var renderer = gameObject.GetComponent<LineRenderer>();
    renderer.SetPosition(0, new Vector3(_avatar0.gameObject.transform.position.x, _avatar0.gameObject.transform.position.y, -1));
    renderer.SetPosition(1, new Vector3(_avatar1.gameObject.transform.position.x, _avatar1.gameObject.transform.position.y, -1));
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
