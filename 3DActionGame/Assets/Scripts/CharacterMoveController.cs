using System.Collections;
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

    private float m_gravityValue = -9.81f;
    private Vector3 m_playerGravityVelocity = Vector3.zero;
    private bool m_jumpFlag = false;
    private bool m_PunchFlag = false;
    private bool m_PunchFlag2 = false;
    private bool m_PunchFlag3 = false;

    private bool m_canWalk = true;

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
        m_jumpFlag = Input.GetKeyDown(KeyCode.V);
        m_PunchFlag = Input.GetKeyDown(KeyCode.Z);
        m_PunchFlag2 = Input.GetKeyDown(KeyCode.X);
        m_PunchFlag3 = Input.GetKeyDown(KeyCode.C);

        Move(h, v);
    }
    private void Move(float horizontal, float vertical)
    {
        if (m_PunchFlag)
        {
            m_animator.SetTrigger("Punch");
            StartCoroutine(StopWalk("Punch"));
        }
        if (m_PunchFlag2)
        {
            m_animator.SetTrigger("Punch2");
            StartCoroutine(StopWalk("Punch2"));
        }
        if (m_PunchFlag3)
        {
            m_animator.SetTrigger("Punch3");
            StartCoroutine(StopWalk("Punch3"));
        }


        if (m_PunchFlag3 && m_characterController.isGrounded)
        {
            m_animator.SetTrigger("Punch3");
            m_PunchFlag3 = false;
        }
        if (m_PunchFlag2 && m_characterController.isGrounded)
        {
            m_animator.SetTrigger("Punch2");
            m_PunchFlag2 = false;
        }
        if (m_PunchFlag && m_characterController.isGrounded)
        {

            m_animator.SetTrigger("Punch");

            m_PunchFlag = false;
        }
        if (m_jumpFlag && m_characterController.isGrounded)
        {
            m_playerGravityVelocity.y = 6;
            m_animator.SetTrigger("Jump");
            m_characterController.Move(m_playerGravityVelocity * Time.deltaTime);
            m_jumpFlag = false;
        }
        if (m_canWalk)
        {


            var cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            cameraForward = cameraForward.normalized;
            if (cameraForward.sqrMagnitude < 0.01f)
                return;

            Quaternion inputFrame = Quaternion.LookRotation(cameraForward, Vector3.up);
            var input = new Vector3(horizontal, 0, vertical);
            var cameraFromPlayer = inputFrame * input;

            if (cameraFromPlayer.sqrMagnitude >= 0.1f)
            {
                var targetAngle = Mathf.Atan2(cameraFromPlayer.x, cameraFromPlayer.z) * Mathf.Rad2Deg;
                var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_turnSmoothVerocity, m_turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                m_characterController.Move(cameraFromPlayer * Speed * Time.deltaTime);
            }
            if (!m_jumpFlag && !m_characterController.isGrounded)
            {
                m_playerGravityVelocity.y += m_gravityValue * Time.deltaTime * 2;
                m_characterController.Move(m_playerGravityVelocity * Time.deltaTime);

            }
            m_playerGravityVelocity.y += m_gravityValue * Time.deltaTime;
            m_characterController.Move(m_playerGravityVelocity * Time.deltaTime);

            m_animator.SetFloat("FrontVelocity", cameraFromPlayer.magnitude);
        }
    }

    public void FootR()
    {

    }
    public void footL()
    {

    }
    private IEnumerator StopWalk(string animatorName)
    {
        m_canWalk = false;
        yield return new WaitWhile(() => !m_animator.GetCurrentAnimatorStateInfo(0).IsName(animatorName));
        yield return new WaitWhile(() => m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f);
        m_canWalk = true;
    }




}