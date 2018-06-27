using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody2D), typeof(EdgeCollider2D), typeof(LineRenderer))]
public class SuperEllipseEdgeCollider2D : MonoBehaviour
{
  public float SuperellipseFactor = 4.0f;
  public float Width = 0f;
  public float Height = 0f;
  public int NumPoints = 32;

  EdgeCollider2D EdgeCollider;
  LineRenderer lineRenderer;
  float currentSuperellipseFactor = 0.0f;
  float currentWidth = 0.0f;
  float currentHeight = 0.0f;

  /// <summary>
  /// Start this instance.
  /// </summary>
  void Start()
  {
    CreateRoundedRectangle();
  }

  /// <summary>
  /// Update this instance.
  /// </summary>
  void Update()
  {
    // If the radius or point count has changed, update the circle
    if (NumPoints != EdgeCollider.pointCount || currentSuperellipseFactor != SuperellipseFactor || currentWidth != Width || currentHeight != Height)
    {
      CreateRoundedRectangle();
    }
  }

  /// <summary>
  /// Creates the rounded rectangle.
  /// </summary>
  void CreateRoundedRectangle()
  {
    Vector2[] edgePoints = new Vector2[NumPoints + 1];
    Vector3[] drawPoints = new Vector3[NumPoints + 1];
    EdgeCollider = GetComponent<EdgeCollider2D>();
    lineRenderer = GetComponent<LineRenderer>();

    var transform = GetComponent<Transform>();

    for (int loop = 0; loop <= NumPoints; loop++)
    {
      float angle = (Mathf.PI * 2.0f / NumPoints) * loop;
      var newVector = new Vector2(Mathf.Pow(Mathf.Abs(Mathf.Cos(angle)), 2f / SuperellipseFactor) * Width * Mathf.Sign(Mathf.Cos(angle)),
                                  Mathf.Pow(Mathf.Abs(Mathf.Sin(angle)), 2f / SuperellipseFactor) * Height * Mathf.Sign(Mathf.Sin(angle)));

      edgePoints[loop] = newVector;
      drawPoints[loop] = new Vector3(newVector.x + transform.position.x, newVector.y + transform.position.y);
    }

    EdgeCollider.points = edgePoints;
    lineRenderer.positionCount = NumPoints;
    lineRenderer.SetPositions(drawPoints);
    currentSuperellipseFactor = SuperellipseFactor;
  }
}