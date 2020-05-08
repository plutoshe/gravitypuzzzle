using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class GravityController : MonoBehaviour, ITriggerComparison
{
    // Start is called before the first frame update
    public float m_NormalGravityAccleration = 9.8f;
    public float m_UnitMovement = 1.0f;
    private Rigidbody m_rigidbody;
    private GravityDirectionRoot m_rotationRoot;
    private float camPitch = 0;
    public float m_SpeedH = 2;
    private float camYaw = 0;
    public float m_SpeedV = 2;
    public Camera m_Camera;
    private Transform downDetection;
    LayerMask m_CollisionMask;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rotationRoot = transform.parent.GetComponent<GravityDirectionRoot>();
        m_Camera = GetComponentInChildren<Camera>();
        downDetection = transform.Find("Down");
        m_IsInteracting = false;
        m_CollisionMask = LayerMask.GetMask("Level") | LayerMask.GetMask("Default") | LayerMask.GetMask("Interaction");
    }

    // Update is called once per frame
    Vector3 UpdateByGravity(float i_deltaTime)
    {
        return i_deltaTime * transform.up * -m_NormalGravityAccleration;
    }
    public bool CompareIdentifier(string identifier)
    {
        return "player" == identifier;
    }
    private Vector3 TackleInput()
    {
        Vector3 movement = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            movement.z += m_UnitMovement;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.z -= m_UnitMovement;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x -= m_UnitMovement;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x += m_UnitMovement;
        }
        return movement;
    }

    private bool isGrounded()
    {
        RaycastHit hit;
        var direction = downDetection.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit, direction.magnitude + 10, m_CollisionMask))
        {
            if (hit.distance < direction.magnitude + 0.1f)
            {
                return true;
            }
        }
        return false;
    }

    private void UpdateCharacterPosition(Vector3 direction, Vector3 startPosition, float innerDist)
    {
        RaycastHit hit;
        //Debug.DrawLine(startPosition, startPosition + 100 * direction, Color.red, 100);
        if (Physics.Raycast(startPosition, direction, out hit, direction.magnitude + innerDist + 1, m_CollisionMask))
        {
            if (hit.distance < direction.magnitude + innerDist)
            {
                transform.parent.position += direction.normalized * (hit.distance - innerDist);
            }
            else
            {
                transform.parent.position += direction;
            }
        }
        else
        {
            transform.parent.position += direction;
        }
    }


    public float m_InteractionDist = 100;
    public InteractionPoint m_InteractionPoint;
    private IInteractionCube m_PreviousInteractionCube;
    public IInteractionCube m_InteractionCube;
    bool m_IsInteracting;
    private void HandleInteraction()
    {
        m_PreviousInteractionCube = m_InteractionCube;
        if (m_Camera)
        {
            m_InteractionCube = null;
            RaycastHit hit;
            int x = Screen.width / 2;
            int y = Screen.height / 2;
            var point = m_Camera.ScreenToWorldPoint(new Vector3(x, y, 1.0f));
            var direction = (point - m_Camera.transform.position).normalized;
            //Debug.DrawLine(m_Camera.transform.position, m_Camera.transform.position + direction * m_InteractionDist, Color.red, 100);
            if (Physics.Raycast(m_Camera.transform.position, direction, out hit, m_InteractionDist, LayerMask.GetMask("Trigger") | LayerMask.GetMask("Level") | LayerMask.GetMask("Interaction")))
            {
                
                m_InteractionCube = hit.collider.gameObject.GetComponent<IInteractionCube>();
                if (m_InteractionCube != m_PreviousInteractionCube && m_PreviousInteractionCube != null)
                {
                    m_PreviousInteractionCube.EndInteraction();
                }
                if (m_InteractionCube != null && m_InteractionPoint)
                {
                    m_InteractionPoint.OnFocus();
                }
            }
            else
            {
                if (m_IsInteracting && m_PreviousInteractionCube != null)
                {
                    m_PreviousInteractionCube.EndInteraction();
                }
                m_IsInteracting = false;
                if (m_InteractionPoint)
                {
                    m_InteractionPoint.OnLeaveFocus();
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && m_InteractionCube != null)
        {
            m_InteractionCube.StartInteraction(this);
            m_IsInteracting = true;
        }
        if (Input.GetMouseButtonUp(0) && m_InteractionCube != null)
        {
            m_InteractionCube.EndInteraction();
            m_IsInteracting = false;
        }
    }

    private void Update()
    {
        if (m_rotationRoot == null || !m_rotationRoot.IsRotating)
        {
            if (!isGrounded())
            {
                UpdateCharacterPosition(UpdateByGravity(Time.deltaTime) * m_rigidbody.mass, transform.position, (downDetection.position - transform.position).magnitude);
            }
            Vector3 desiredMove = TackleInput() * Time.deltaTime;
            
            camYaw += m_SpeedH * Input.GetAxis("Mouse X");
            camPitch -= m_SpeedV * Input.GetAxis("Mouse Y");
            camPitch = Mathf.Clamp(camPitch, -90, 90);

            if (m_Camera)
            {
                m_Camera.transform.localEulerAngles = new Vector3(camPitch, 0, 0);
                transform.localEulerAngles = new Vector3(0, camYaw, 0);
                //RaycastHit hitInfo;
                //Physics.SphereCast(transform.position, 0.5, Vector3.down, out hitInfo,
                //                   m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
                //desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

                //desiredMove.x = movement.x * m_UnitMovement;
                //desiredMove.z = movement.z * m_UnitMovement;

               
            }
            var ZdesiredMove = desiredMove.z * transform.forward;
            var XdesiredMove = desiredMove.x * transform.right;
            UpdateCharacterPosition(ZdesiredMove, transform.position, 0.92f);
            UpdateCharacterPosition(XdesiredMove, transform.position, 0.92f);

        }
        HandleInteraction();
    }
}
