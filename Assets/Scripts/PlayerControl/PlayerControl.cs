using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    enum State
    {
        None,
        Idle,
        Rotation_Left,
        Rotation_Right,
        MoveFornt,
        MoveBack
    }

    Rigidbody m_rigidbody;

    State playerState = State.None;

    public float rotationSpeed=1f;
    public float moveSpeed = 1f;

    private string m_MovementAxisName = "Vectical01";
    private string m_TurnAxisName = "Horizontal01";

    private float m_MovementInputValue;
    private float m_TurnInputValue;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 moveMent = transform.forward * m_MovementInputValue * moveSpeed*Time.deltaTime;
        m_rigidbody.MovePosition(transform.position+moveMent);
    }

    private void Turn()
    {
        float turn = m_TurnInputValue * rotationSpeed * Time.deltaTime;
        Quaternion quaternion = Quaternion.Euler(0f,turn,0f);
        m_rigidbody.MoveRotation(m_rigidbody.rotation*quaternion);
    }

    void LaunchBullet()
    {

    }
}
