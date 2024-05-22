using System;
using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class SOPDCharacter : MonoBehaviour
{
    [Header("AI")]
    public NavMeshAgent agent;
    public NavMeshSurface navMeshSurface;

    [FormerlySerializedAs("haveHammer")] [Header("World State")] 
    public bool hasHammer = false;
    [FormerlySerializedAs("haveDrill")] public bool hasDrill = false;
    [FormerlySerializedAs("haveMoney")] public bool hasMoney = false;
    public bool brokeDoor = false;
    public bool openedVault = false;
    public bool successfullyEscaped = false;
    
    [Header("Animations")] 
    public Animator animatorController;

    [Header("Debug/Internal State")]
    [SerializeField] private float interactTime = 0.0f;
    [SerializeField] private float hammerTime = 0.0f;
    [SerializeField] private bool interactCoroutineRunning = false;
    [SerializeField] private bool isInteracting = false;
    [SerializeField] private float defaultSpeed = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = moveGoal.position;
        defaultSpeed = agent.speed;
        UpdateAnimationClipTimes();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (testAnims)
        {
            animatorController.Play("Interact");
            testAnims = false;
        }*/
        
        animatorController.SetFloat("movementSpeed", agent.velocity.magnitude);
    }

    public void MoveToLocation(Vector3 location)
    {
        agent.SetDestination(location);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (!interactCoroutineRunning)
            {
                StartCoroutine(WaitAndInteract(other));
            }
        }
    }

    IEnumerator WaitAndInteract(Collider other)
    {
        interactCoroutineRunning = true;
        
        yield return new WaitForSeconds(0.1f);
        
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable)
        {
            InteractableType type = interactable.GetInteractType();
            
            switch (type)
            {
                case InteractableType.Default:
                {
                    break;
                }
                case InteractableType.Drill:
                {
                    if (!hasDrill && !isInteracting)
                    {
                        StartCoroutine(InteractWithDrill(interactable));
                    }
                    break;
                }
                case InteractableType.Hammer:
                {
                    if (!hasHammer && !isInteracting)
                    {
                        StartCoroutine(InteractWithHammer(interactable));
                    }
                    
                    break;
                }
                case InteractableType.Door:
                {
                    if (hasHammer && !isInteracting)
                    {
                        StartCoroutine(InteractWithDoor(interactable));
                    }
                    break;
                }
                case InteractableType.Vault:
                {
                    if (hasDrill && !isInteracting)
                    {
                        StartCoroutine(InteractWithVault(interactable));
                    }
                    break;
                }
                case InteractableType.Money:
                {
                    if (!hasMoney && !isInteracting)
                    {
                        StartCoroutine(InteractWithMoney(interactable));
                    }
                    break;
                }
                case InteractableType.GetawayVan:
                {
                    if (hasMoney && !isInteracting)
                    {
                        StartCoroutine(InteractWithVan(interactable));
                    }
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        interactCoroutineRunning = false;
    }

    IEnumerator InteractWithDrill(Interactable interactable)
    {
        StartInteract("Interact");
        yield return new WaitForSeconds(interactTime);
        EndInteract(interactable);
        hasDrill = true;
    }

    IEnumerator InteractWithHammer(Interactable interactable)
    {
        StartInteract("Interact");
        yield return new WaitForSeconds(interactTime);
        EndInteract(interactable);
        hasHammer = true;
    }
    
    IEnumerator InteractWithDoor(Interactable interactable)
    {
        StartInteract("Hammer");
        yield return new WaitForSeconds(hammerTime);
        EndInteract(interactable);
        brokeDoor = true;
        navMeshSurface.BuildNavMesh();
    }
    
    IEnumerator InteractWithVault(Interactable interactable)
    {
        StartInteract("Interact");
        yield return new WaitForSeconds(interactTime);
        EndInteract(interactable);
        openedVault = true;
        navMeshSurface.BuildNavMesh();
    }
    
    IEnumerator InteractWithMoney(Interactable interactable)
    {
        StartInteract("Interact");
        yield return new WaitForSeconds(interactTime);
        EndInteract(interactable);
        hasMoney = true;
    }

    IEnumerator InteractWithVan(Interactable interactable)
    {
        StartInteract("Interact");
        yield return new WaitForSeconds(interactTime);
        EndInteract(interactable);
        hasMoney = false;
        successfullyEscaped = true;
    }

    private void StartInteract(string animationName)
    {
        isInteracting = true;
        agent.speed = 0.0f;
        animatorController.Play(animationName);
    }
    
    private void EndInteract(Interactable interactable)
    {
        interactable.Interact();
        agent.speed = defaultSpeed;
        isInteracting = false;
    }
    
    private void UpdateAnimationClipTimes()
    {
        AnimationClip[] clips = animatorController.runtimeAnimatorController.animationClips;
        foreach (var clip in clips)
        {
            switch (clip.name)
            {
                case "Interact":
                    interactTime = clip.length;
                    break;
                case "Hammer":
                    hammerTime = clip.length;
                    break;
            }
        }
    }
}
