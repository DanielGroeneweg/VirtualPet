using TMPro;
using Unity.Mathematics;
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
    public FuelPickup fuelPickupPrefab;
    public FuelPickup spawnedFuel;

    [Header("Run Stats")]
    public float distanceTraveled => car.transform.position.x;

    [Header("Spline settings")]
    public float spawnDistance;
    public Vector2 randomHeightRange;
    public int startingPoints;
    void AddPoint(float xPos)
    {
        Vector3 position = new Vector3(xPos, UnityEngine.Random.Range(randomHeightRange.x, randomHeightRange.y), 0);
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
        car.canMove = false;
        car.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        car.maxSpeed = 0;
        
        gameOverScreen.alpha = 1;
        gameOverScreen.blocksRaycasts = true;
        gameOverScreen.interactable = true;

        distanceText.text = $"Distance: {distanceTraveled}m";
    }
    

    private void Update()
    {
        if (Mathf.Abs(car.transform.position.x - raceTrackSpline.Spline[raceTrackSpline.Spline.Count - 1].Position.x) < spawnDistance)
        {
            AddPoint(car.transform.position.x + spawnDistance + 5f);
            RemoveOldPoint();
        }

        if(car.fuel < car.maxFuel / 4 && spawnedFuel == null)
        {
            float3 position;
            float3 tangent;
            float3 upvector;
            raceTrackSpline.Spline.Evaluate(distanceTraveled + spawnDistance * .8f, out position, out tangent, out upvector);

            Vector3 targetPosition = position + tangent + upvector;
            float3 targetPositionSplinePosition;
            raceTrackSpline.Spline.Evaluate(targetPosition.x, out targetPositionSplinePosition, out _, out _);

            targetPosition.y = Mathf.Max(targetPosition.y, targetPositionSplinePosition.y);

            spawnedFuel = Instantiate(fuelPickupPrefab, targetPosition, quaternion.identity);
        }
    }
}
