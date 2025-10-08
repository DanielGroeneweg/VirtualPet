using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
public class RacingGameManager : MonoBehaviour
{
    public static RacingGameManager instance;
    SplineContainer raceTrackSpline;
    EdgeCollider2D edgeCollider;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;


        // set up spline
        raceTrackSpline = GetComponent<SplineContainer>();

        if (raceTrackSpline.Spline == null) raceTrackSpline.AddSpline();

        for (int i = 0; i < startingPoints; i++)
        {
            AddPoint(spawnDistance * i);
        }

        // set up edge collider
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    public CarController car;

    [Header("Run Stats")]
    public float distanceTraveled => car.transform.position.x;

    [Header("Spline settings")]
    public float spawnDistance;
    public Vector2 randomHeightRange;
    public int startingPoints;

    private void Update()
    {
        if (Mathf.Abs(car.transform.position.x - raceTrackSpline.Spline[raceTrackSpline.Spline.Count - 1].Position.x) < spawnDistance)
        {
            AddPoint(car.transform.position.x + spawnDistance + 5f);
            RemoveOldPoint();
        }
    }

    void AddPoint(float xPos)
    {
        Vector3 position = new Vector3(xPos, Random.Range(randomHeightRange.x, randomHeightRange.y), 0);
        BezierKnot knot = new BezierKnot(position);

        raceTrackSpline.Spline.Add(knot);

        raceTrackSpline.Spline.SetTangentMode(TangentMode.AutoSmooth);
    }
    void RemoveOldPoint()
    {
        raceTrackSpline.Spline.RemoveAt(0);
    }

    [Header("Game Over Settings")]
    public CanvasGroup gameOverScreen;
    public TMP_Text distanceText;

    public void EndRun()
    {
        gameOverScreen.alpha = 1;
        gameOverScreen.blocksRaycasts = true;
        gameOverScreen.interactable = true;

        distanceText.text = $"Distance: {distanceTraveled}m";
    }
}
