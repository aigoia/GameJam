using System;
using System.Threading.Tasks;
using Arcade;
using UnityEngine;

namespace Script.Arcade
{
    public class ArcadeController : MonoBehaviour
    {
        float speedInput;
        float turnInput;
        float jumpInput;
        bool isGround;
        bool isJumping;
        public bool jumpMode;
        Vector3 targetDirection;
        Rigidbody body;
        Transform groundRayPoint;
        LayerMask groundMask;
        
        Vector3 ResetPosition => new Vector3(0, -10, 0);

        void Awake()
        {
            groundMask = LayerMask.GetMask("Ground");
            body = transform.Find("CharacterCollider").GetComponent<Rigidbody>();
            groundRayPoint = transform.Find("RayPoint");
        }

        void Start()
        {
            body.transform.parent = null;
            
            _ = CheckGroundRoutine();
            isJumping = false;
        }

        async Task CheckGroundRoutine()
        {
            isJumping = false;
            
            while (isGround)
            {
                RayGround();
                for (int i = 0; i < Setting.GroundMistake && !isGround; i++)
                {
                    RayGround();
                    await Task.Yield();
                    isGround = isGround || isGround; 
                    if (isGround) break;
                }
                isJumping = false;
                await Task.Yield();
            }
            
            while (!isGround)
            {
                RayGround();
                await Task.Yield();
            }
			
            await Task.Delay(TimeSpan.FromSeconds(Setting.RayCastWait));
            if (jumpMode) await CheckGroundRoutine();
        }
        
        bool RayGround()
        {
            return Physics.Raycast(groundRayPoint.position, -transform.up, out var hit, Setting.GroundRayLength, groundMask);
        }

        void Update()
        {
            
            speedInput = Input.GetAxis("Vertical") < 0 ? Input.GetAxis("Vertical") * 100f : 
                          Input.GetAxis("Vertical") > 0 ? Input.GetAxis("Vertical") * Setting.ReverseAccelerator * 100f : 0;
            turnInput = Input.GetAxis("Horizontal");
            jumpInput = Input.GetAxis("Jump") * 100f;

            if (isGround) transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3
                                                (0f, turnInput * Setting.TurnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
            
            transform.position = body.transform.position;
            
            SuperJump();
        }

        void FixedUpdate()
        {
            isGround = false;
            isGround = RayGround();
            
            if (isGround)
            {
                if (Math.Abs(speedInput) > 0) body.AddForce(transform.forward * speedInput);
                body.drag = Setting.DragGround;
            }
            else
            {
                body.AddForce(Vector3.up * (Setting.GravityForce * 10f));
                body.drag = Setting.DragAir;
            }
        }
        
        void SuperJump()
        {
            if (jumpInput == 0) return;
            if (isJumping) return;
			
            isJumping = true;
            body.AddForce(transform.forward * speedInput + Vector3.up * (jumpInput * Setting.JumpForce));
        }
    }
}
