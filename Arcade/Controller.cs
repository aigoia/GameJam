using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Arcade
{
    public class Controller : MonoBehaviour
    {
        float _speedInput;
        float _turnInput;
        float _jumpInput;
        bool _isGround;
        bool _isJumping;
        public bool jumpMode;
        Vector3 _targetDirection;
        Rigidbody _body;
        Transform _groundRayPoint;
        LayerMask _groundMask;

        void Awake()
        {
            _groundMask = LayerMask.GetMask("Ground");
            _body = transform.Find("CharacterCollider").GetComponent<Rigidbody>();
            _groundRayPoint = transform.Find("RayPoint");
        }

        void Start()
        {
            _body.transform.parent = null;
            
            _ = CheckGroundRoutine();
            _isJumping = false;
        }

        async Task CheckGroundRoutine()
        {
            _isJumping = false;
            
            while (_isGround)
            {
                RayGround();
                for (int i = 0; i < Setting.GroundMistake && !_isGround; i++)
                {
                    RayGround();
                    await Task.Yield();
                    _isGround = _isGround || _isGround; 
                    if (_isGround) break;
                }
                _isJumping = false;
                await Task.Yield();
            }
            
            while (!_isGround)
            {
                RayGround();
                await Task.Yield();
            }
			
            await Task.Delay(TimeSpan.FromSeconds(Setting.RayCastWait));
            if (jumpMode) await CheckGroundRoutine();
        }
        
        bool RayGround()
        {
            return Physics.Raycast(_groundRayPoint.position, -transform.up, out var hit, Setting.GroundRayLength, _groundMask);
        }

        void Update()
        {
            _speedInput = Input.GetAxis("Vertical") < 0 ? Input.GetAxis("Vertical") * 100f : 
                          Input.GetAxis("Vertical") > 0 ? Input.GetAxis("Vertical") * Setting.ReverseAccelerator * 100f : 0;
            _turnInput = Input.GetAxis("Horizontal");
            _jumpInput = Input.GetAxis("Jump") * 100f;

            if (_isGround) transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3
                                                (0f, _turnInput * Setting.TurnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
            
            transform.position = _body.transform.position;
            
            SuperJump();
        }

        void FixedUpdate()
        {
            _isGround = false;
            _isGround = RayGround();
            
            if (_isGround)
            {
                if (Math.Abs(_speedInput) > 0) _body.AddForce(transform.forward * _speedInput);
                _body.drag = Setting.DragGround;
            }
            else
            {
                _body.AddForce(Vector3.up * (Setting.GravityForce * 10f));
                _body.drag = Setting.DragAir;
            }
        }
        
        void SuperJump()
        {
            if (_jumpInput == 0) return;
            if (_isJumping) return;
			
            _isJumping = true;
            _body.AddForce(transform.forward * _speedInput + Vector3.up * (_jumpInput * Setting.JumpForce));
        }
    }
}
