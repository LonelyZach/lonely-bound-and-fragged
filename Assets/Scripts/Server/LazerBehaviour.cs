using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LazerBehaviour : MonoBehaviour {

  public int SpringDistance = 3;
  public int SpringFrequency = 1;

  public GameObject Avatar0;
  public GameObject Avatar1;

	// Use this for initialization
	void Start () {
    // Spring joints need to be on one of the objects being joined, so, we'll creat it programatically.
    CreateSpringJoint();

  }
	
	// Update is called once per frame
	void Update () {
    KillAvatarsInLazer();
    Draw();
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

  private void Draw()
  {
    var renderer = gameObject.GetComponent<LineRenderer>();
    renderer.SetPosition(0, Avatar0.gameObject.transform.position);
    renderer.SetPosition(1, Avatar1.gameObject.transform.position);
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
    return Physics2D.LinecastAll(Avatar0.transform.position, Avatar1.transform.position)
      .Select(x => x.collider.gameObject)
      .Where(x => x != Avatar0 && x != Avatar1)
      .Select(x => x.GetComponent<AvatarBehaviour>())
      .Where(x => x != null);
  }
}
