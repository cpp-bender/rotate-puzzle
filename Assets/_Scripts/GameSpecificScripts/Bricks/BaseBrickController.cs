using DG.Tweening;
using UnityEngine;

public class BaseBrickController : MonoBehaviour
{
    [SerializeField] MeshRenderer[] stickRenderers;
    [SerializeField] GameObject turnRightArrow;
    [SerializeField] GameObject turnLeftArrow;

    protected Vector3 startRotation;
    protected Vector3 nextRotation;
    protected Vector3 currentRotation;

    private ReferenceManager referenceManager;
    private BrickRotation startBrickRotation = BrickRotation.Up;
    private BrickRotation nextBrickRotation = BrickRotation.Right;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        referenceManager = FindObjectOfType<ReferenceManager>();
    }

    public Vector3 BrickRotationToEulerAngles(BrickRotation brickRotation)
    {
        if (brickRotation == BrickRotation.Right)
        {
            return new Vector3(0f, 90f, 0f);
        }

        else if (brickRotation == BrickRotation.Left)
        {
            return new Vector3(0f, 270f, 0f);
        }

        else if (brickRotation == BrickRotation.Up)
        {
            return new Vector3(0f, 0f, 0f);
        }

        else if (brickRotation == BrickRotation.Down)
        {
            return new Vector3(0f, 180f, 0f);
        }

        else
        {
            return Vector3.zero;
        }
    }

    public virtual void InitialSetup(Vector3 initialRot, Vector3 nextRot, Material material, Transform parent, BrickRotation start, BrickRotation next)
    {
        SetInitialRotation(initialRot);
        SetNextRotation(nextRot);
        SetCurrentRotation(initialRot);
        SetMaterial(material);
        SetParent(parent);
        SetBrickRotationStart(start);
        SetBrickRotationNext(next);
        FindTurnDirection();
    }

    private void ChangeActiveArrow()
    {
        if (turnLeftArrow.activeSelf)
        {
            turnRightArrow.SetActive(true);
            turnLeftArrow.SetActive(false);
        }
        else
        {
            turnRightArrow.SetActive(false);
            turnLeftArrow.SetActive(true);
        }
    }

    private void FindTurnDirection()
    {
        if (turnRightArrow == null)
        {
            return;
        }

        if (startRotation.y == 0f)
        {
            if (nextRotation.y < 180f)
            {
                turnRightArrow.SetActive(true);
                turnLeftArrow.SetActive(false);
            }
            else
            {
                turnRightArrow.SetActive(false);
                turnLeftArrow.SetActive(true);
            }
        }
        else if (nextRotation.y == 0f)
        {
            if (startRotation.y > 180f)
            {
                turnRightArrow.SetActive(true);
                turnLeftArrow.SetActive(false);
            }
            else
            {
                turnRightArrow.SetActive(false);
                turnLeftArrow.SetActive(true);
            }
        }
        else
        {
            if (startRotation.y < nextRotation.y)
            {
                turnRightArrow.SetActive(true);
                turnLeftArrow.SetActive(false);
            }
            else
            {
                turnRightArrow.SetActive(false);
                turnLeftArrow.SetActive(true);
            }
        }
    }

    private void SetBrickRotationStart(BrickRotation start)
    {
        startBrickRotation = start;
    }

    private void SetBrickRotationNext(BrickRotation next)
    {
        nextBrickRotation = next;
    }

    public void SetInitialRotation(Vector3 rotation)
    {
        startRotation = rotation;
    }

    public void SetNextRotation(Vector3 rotation)
    {
        nextRotation = rotation;
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void SetMaterial(Material material)
    {
        for (int i = 0; i < stickRenderers.Length; i++)
        {
            stickRenderers[i].material = material;
        }
    }

    private void SetCurrentRotation(Vector3 rotation)
    {
        currentRotation = rotation;
    }

    public virtual void OnSelected()
    {
        if (referenceManager.brickState == BrickState.Stop)
        {
            return;
        }

        if (currentRotation == startRotation)
        {
            transform.DOLocalRotate(nextRotation, .5f, RotateMode.Fast)
                .SetEase(Ease.Linear)
                .OnStart(delegate
                {
                    referenceManager.brickState = BrickState.Stop;
                })
                .OnComplete(delegate
                {
                    referenceManager.brickState = BrickState.Rotate;

                    currentRotation = nextRotation;

                    ChangeActiveArrow();

                    GridManager.Instance.PlayEffects();
                    GridManager.Instance.CheckGameOver();
                })
                .Play();
        }

        else if (currentRotation == nextRotation)
        {
            transform.DOLocalRotate(startRotation, .5f, RotateMode.Fast)
                .SetEase(Ease.Linear)
                .OnStart(delegate
                {
                    referenceManager.brickState = BrickState.Stop;
                })
                .OnComplete(delegate
                {
                    referenceManager.brickState = BrickState.Rotate;

                    currentRotation = startRotation;

                    ChangeActiveArrow();

                    GridManager.Instance.PlayEffects();
                    GridManager.Instance.CheckGameOver();
                })
                .Play();
        }
    }
}
