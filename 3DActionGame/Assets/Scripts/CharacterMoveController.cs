using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    public float Speed = 6f;

    private Animator m_animator = null;
    private Rigidbody m_rigidbody = null;
    private Vector3 m_direction = Vector3.zero;
    private CharacterController m_characterController = null;
    private float m_turnSmoothVerocity = 0f;
    private float m_turnSmoothTime = 0.1f;
    // Start is called before the first frame update
    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Move(h, v);
    }
    private void Move(float horizontal, float vertical)
    {
        m_direction = new Vector3(horizontal, 0, vertical).normalized;
        if (m_direction.magnitude >= 0.1f)
        {
            var targetAngle = Mathf.Atan2(m_direction.x, m_direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_turnSmoothVerocity, m_turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            m_characterController.Move(m_direction * Speed * Time.deltaTime);
        }
        m_animator.SetFloat("FrontVelocity", m_direction.magnitude);
    }
    public void FootR()
    {

    }
    public void footL()
    {

    }
}