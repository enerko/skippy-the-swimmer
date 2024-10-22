//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""CharacterMovement"",
            ""id"": ""f7895585-4671-418e-b77d-40c4e27c639a"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""06e6ad83-20e0-4a03-a48a-c1401ab8785c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""27d66705-9f26-4896-80b8-9c2515b2ebcb"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""2e2ecddc-4ff5-4b2b-aaf4-3b3df16cc7db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CameraRotate"",
                    ""type"": ""Value"",
                    ""id"": ""5bafad3c-9a58-423e-8c8a-e8ac357c2dba"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Talk"",
                    ""type"": ""Button"",
                    ""id"": ""f5204983-3ab5-4a56-8915-128cd0a0f3be"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ViewChecklist"",
                    ""type"": ""Value"",
                    ""id"": ""87a64561-b0d6-4a57-a9f0-2f06720d3a74"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CameraRotateJoystick"",
                    ""type"": ""Value"",
                    ""id"": ""250d5a2e-600c-4d6f-8139-b32103cdde4e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""50d3e704-8724-471f-a2b7-98fec126587b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cb54ea4d-e31e-43f0-af0f-ab8533d30b4b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c12ea725-0d5a-4bf0-af80-2b9e1016489e"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""3D Vector"",
                    ""id"": ""6a713f31-6e95-46ff-8eeb-167de6b87a42"",
                    ""path"": ""3DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8733d99b-42a6-4ed0-8d3a-1c209b64e410"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a507aabb-1805-4547-a032-6a415ef5947e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""forward"",
                    ""id"": ""bcaa7620-71fa-4614-85e6-73eba3a2fbc2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""backward"",
                    ""id"": ""3802864b-4ba7-47c8-90d6-7905d22f868b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""3D Vector"",
                    ""id"": ""3a8d80ff-978c-4589-a8b1-4192eb89bad6"",
                    ""path"": ""3DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""462e3764-72a9-4da2-868b-516cb05977bb"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6d36abbe-0f1c-4897-ace9-bfb5f16a48b8"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""forward"",
                    ""id"": ""2a5014f1-c987-4b2c-ae7d-e83cb466c7f0"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""backward"",
                    ""id"": ""2a2fb5ed-752f-4416-b73c-5c7db02f2e5e"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""4b4564e0-e50c-4ea0-a663-c9cfc188662f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a72d1b6d-79b7-4d44-874d-26697c698f9f"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""cb591a2b-3c5c-4219-90e3-9a521b719e21"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""76e751ad-49b3-4b9d-8c84-72084450455e"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c4c57f2b-112b-45dd-9b0e-9bac8abf34da"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.3,y=0.3)"",
                    ""groups"": """",
                    ""action"": ""CameraRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6552d7b8-6529-45a0-b63a-5a39f058825f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Talk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a261687b-48c6-4d7e-bae0-6d952eb0af89"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Talk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""631b01b0-339d-4263-ab01-b588abf2a873"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ViewChecklist"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0b24bbc-3f89-48a0-9120-3486ca137a9c"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ViewChecklist"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""bf2ea7c2-9505-49a3-b500-9c5518a2a0fe"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.3,y=0.3)"",
                    ""groups"": """",
                    ""action"": ""CameraRotateJoystick"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f82cc5e0-aa94-4083-b6f8-4069f7b84302"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotateJoystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""98118934-2498-4898-8e1c-68f4a37649e5"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotateJoystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""485218a6-5a56-4c5b-b38a-31c5c0976f8c"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotateJoystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""215bc7a0-0aa7-4846-aa3b-e887abcb8415"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotateJoystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7cebd546-6972-42b1-9c3f-491744248ec5"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e8bcfe4-c57c-4a68-b7fc-6fa5346a50bd"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterMovement
        m_CharacterMovement = asset.FindActionMap("CharacterMovement", throwIfNotFound: true);
        m_CharacterMovement_Jump = m_CharacterMovement.FindAction("Jump", throwIfNotFound: true);
        m_CharacterMovement_Move = m_CharacterMovement.FindAction("Move", throwIfNotFound: true);
        m_CharacterMovement_Attack = m_CharacterMovement.FindAction("Attack", throwIfNotFound: true);
        m_CharacterMovement_CameraRotate = m_CharacterMovement.FindAction("CameraRotate", throwIfNotFound: true);
        m_CharacterMovement_Talk = m_CharacterMovement.FindAction("Talk", throwIfNotFound: true);
        m_CharacterMovement_ViewChecklist = m_CharacterMovement.FindAction("ViewChecklist", throwIfNotFound: true);
        m_CharacterMovement_CameraRotateJoystick = m_CharacterMovement.FindAction("CameraRotateJoystick", throwIfNotFound: true);
        m_CharacterMovement_Pause = m_CharacterMovement.FindAction("Pause", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // CharacterMovement
    private readonly InputActionMap m_CharacterMovement;
    private List<ICharacterMovementActions> m_CharacterMovementActionsCallbackInterfaces = new List<ICharacterMovementActions>();
    private readonly InputAction m_CharacterMovement_Jump;
    private readonly InputAction m_CharacterMovement_Move;
    private readonly InputAction m_CharacterMovement_Attack;
    private readonly InputAction m_CharacterMovement_CameraRotate;
    private readonly InputAction m_CharacterMovement_Talk;
    private readonly InputAction m_CharacterMovement_ViewChecklist;
    private readonly InputAction m_CharacterMovement_CameraRotateJoystick;
    private readonly InputAction m_CharacterMovement_Pause;
    public struct CharacterMovementActions
    {
        private @PlayerControls m_Wrapper;
        public CharacterMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_CharacterMovement_Jump;
        public InputAction @Move => m_Wrapper.m_CharacterMovement_Move;
        public InputAction @Attack => m_Wrapper.m_CharacterMovement_Attack;
        public InputAction @CameraRotate => m_Wrapper.m_CharacterMovement_CameraRotate;
        public InputAction @Talk => m_Wrapper.m_CharacterMovement_Talk;
        public InputAction @ViewChecklist => m_Wrapper.m_CharacterMovement_ViewChecklist;
        public InputAction @CameraRotateJoystick => m_Wrapper.m_CharacterMovement_CameraRotateJoystick;
        public InputAction @Pause => m_Wrapper.m_CharacterMovement_Pause;
        public InputActionMap Get() { return m_Wrapper.m_CharacterMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterMovementActions set) { return set.Get(); }
        public void AddCallbacks(ICharacterMovementActions instance)
        {
            if (instance == null || m_Wrapper.m_CharacterMovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CharacterMovementActionsCallbackInterfaces.Add(instance);
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
            @CameraRotate.started += instance.OnCameraRotate;
            @CameraRotate.performed += instance.OnCameraRotate;
            @CameraRotate.canceled += instance.OnCameraRotate;
            @Talk.started += instance.OnTalk;
            @Talk.performed += instance.OnTalk;
            @Talk.canceled += instance.OnTalk;
            @ViewChecklist.started += instance.OnViewChecklist;
            @ViewChecklist.performed += instance.OnViewChecklist;
            @ViewChecklist.canceled += instance.OnViewChecklist;
            @CameraRotateJoystick.started += instance.OnCameraRotateJoystick;
            @CameraRotateJoystick.performed += instance.OnCameraRotateJoystick;
            @CameraRotateJoystick.canceled += instance.OnCameraRotateJoystick;
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
        }

        private void UnregisterCallbacks(ICharacterMovementActions instance)
        {
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
            @CameraRotate.started -= instance.OnCameraRotate;
            @CameraRotate.performed -= instance.OnCameraRotate;
            @CameraRotate.canceled -= instance.OnCameraRotate;
            @Talk.started -= instance.OnTalk;
            @Talk.performed -= instance.OnTalk;
            @Talk.canceled -= instance.OnTalk;
            @ViewChecklist.started -= instance.OnViewChecklist;
            @ViewChecklist.performed -= instance.OnViewChecklist;
            @ViewChecklist.canceled -= instance.OnViewChecklist;
            @CameraRotateJoystick.started -= instance.OnCameraRotateJoystick;
            @CameraRotateJoystick.performed -= instance.OnCameraRotateJoystick;
            @CameraRotateJoystick.canceled -= instance.OnCameraRotateJoystick;
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
        }

        public void RemoveCallbacks(ICharacterMovementActions instance)
        {
            if (m_Wrapper.m_CharacterMovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICharacterMovementActions instance)
        {
            foreach (var item in m_Wrapper.m_CharacterMovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CharacterMovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CharacterMovementActions @CharacterMovement => new CharacterMovementActions(this);
    public interface ICharacterMovementActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnCameraRotate(InputAction.CallbackContext context);
        void OnTalk(InputAction.CallbackContext context);
        void OnViewChecklist(InputAction.CallbackContext context);
        void OnCameraRotateJoystick(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
