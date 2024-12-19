//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/inputs/MainInput.inputactions
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

public partial class MainInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public MainInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainInput"",
    ""maps"": [
        {
            ""name"": ""mainScene"",
            ""id"": ""50758617-850f-4d5e-9890-7727dd642a44"",
            ""actions"": [
                {
                    ""name"": ""NewNode"",
                    ""type"": ""Button"",
                    ""id"": ""4f645a60-c0b8-4801-8a87-2506b5bf3fb8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""eda6efd3-28af-4a8f-8cb0-a32da59f3704"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Load"",
                    ""type"": ""Button"",
                    ""id"": ""23472ed6-2f18-4021-a9b4-7797377218ad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Save"",
                    ""type"": ""Button"",
                    ""id"": ""d4a8231d-53da-4e52-b556-5036ad7f97f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""NewChildNode"",
                    ""type"": ""Button"",
                    ""id"": ""eb9fa6c8-508f-4d63-9442-3c2542a00176"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveCamera"",
                    ""type"": ""Value"",
                    ""id"": ""4d556fe7-3e16-4ee1-b1fd-76a6d9ac0cf6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ChangeCameraSize"",
                    ""type"": ""Button"",
                    ""id"": ""7b1737d2-7fd8-4116-ba85-a55f332cfba5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ConnectSelectedNodes"",
                    ""type"": ""Button"",
                    ""id"": ""a7414ba0-b688-46ba-af3e-4a2916326ccd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ConnectToSelectedNodes"",
                    ""type"": ""Button"",
                    ""id"": ""6c520d2e-688d-423e-ae40-3aaf0621f630"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fb020ec3-fa62-40f7-8ce0-8b6e33bb21a1"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pc"",
                    ""action"": ""NewNode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3444824-b7f6-44cb-93d5-0807a985984c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pc"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cfa93652-949e-4b85-8c36-de0daa0cac33"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Load"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac505fdd-6872-40be-91c2-77edc4fa8614"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""3261362f-1385-4b4c-9d27-4c7cdfc47946"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NewChildNode"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Modifier"",
                    ""id"": ""bf90c115-f6eb-40c1-884d-1d8c0c585dc3"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pc"",
                    ""action"": ""NewChildNode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Button"",
                    ""id"": ""ca5a5d39-aa7d-4e91-b1b2-381765186822"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pc"",
                    ""action"": ""NewChildNode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""c6454f71-9a69-47bd-a751-844f0152b8c2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5b301848-e65f-4334-b9a1-d08fa0ccc48b"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pc"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0cb2ec46-8ea8-45a6-9199-162eefd357ac"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pc"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f1237084-14e6-4544-8c12-9b8a3cf2bad1"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pc"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1f9141ec-3590-47fd-8987-6d3e6d908fe1"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pc"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""One Modifier"",
                    ""id"": ""c8cd4629-945d-4430-b40f-7c10720df826"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConnectSelectedNodes"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""ad446764-ada9-46ae-add9-1a272770bf3a"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConnectSelectedNodes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""41b487fd-c136-44a9-abfa-5852d03d802d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConnectSelectedNodes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""One Modifier"",
                    ""id"": ""501900c1-841b-4322-8c90-7b5dddd28d42"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConnectToSelectedNodes"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Two Modifiers"",
                    ""id"": ""f231514c-f08f-4d2f-bab2-4eff12df2347"",
                    ""path"": ""TwoModifiers"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConnectToSelectedNodes"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier1"",
                    ""id"": ""a0c63b65-062b-4cfd-ab7b-4ab2664263d1"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pc"",
                    ""action"": ""ConnectToSelectedNodes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""modifier2"",
                    ""id"": ""17b86fde-13c0-4106-98c9-cf56606d92cc"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pc"",
                    ""action"": ""ConnectToSelectedNodes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""31dd6d96-f421-48b9-8c4b-42fc5cab219a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pc"",
                    ""action"": ""ConnectToSelectedNodes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""cae324cc-8f69-4f4c-b2e4-fb6fdc767345"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCameraSize"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9953f52d-e3a3-444b-bbc5-79758620000c"",
                    ""path"": ""<Keyboard>/numpadMinus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pc"",
                    ""action"": ""ChangeCameraSize"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9d47a8fb-7588-4547-ad1c-5c361c76a1d3"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Pc"",
                    ""action"": ""ChangeCameraSize"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Mouse"",
                    ""id"": ""fcb05c23-47b1-4fb4-b10c-8242bdc1961a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCameraSize"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""751e228d-05d3-4bdf-b4ae-ba796b329afa"",
                    ""path"": ""<Mouse>/scroll/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCameraSize"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6ec5cb79-bacf-4b6a-ab80-eb74b34b9fc2"",
                    ""path"": ""<Mouse>/scroll/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCameraSize"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Pc"",
            ""bindingGroup"": ""Pc"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // mainScene
        m_mainScene = asset.FindActionMap("mainScene", throwIfNotFound: true);
        m_mainScene_NewNode = m_mainScene.FindAction("NewNode", throwIfNotFound: true);
        m_mainScene_Select = m_mainScene.FindAction("Select", throwIfNotFound: true);
        m_mainScene_Load = m_mainScene.FindAction("Load", throwIfNotFound: true);
        m_mainScene_Save = m_mainScene.FindAction("Save", throwIfNotFound: true);
        m_mainScene_NewChildNode = m_mainScene.FindAction("NewChildNode", throwIfNotFound: true);
        m_mainScene_MoveCamera = m_mainScene.FindAction("MoveCamera", throwIfNotFound: true);
        m_mainScene_ChangeCameraSize = m_mainScene.FindAction("ChangeCameraSize", throwIfNotFound: true);
        m_mainScene_ConnectSelectedNodes = m_mainScene.FindAction("ConnectSelectedNodes", throwIfNotFound: true);
        m_mainScene_ConnectToSelectedNodes = m_mainScene.FindAction("ConnectToSelectedNodes", throwIfNotFound: true);
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

    // mainScene
    private readonly InputActionMap m_mainScene;
    private List<IMainSceneActions> m_MainSceneActionsCallbackInterfaces = new List<IMainSceneActions>();
    private readonly InputAction m_mainScene_NewNode;
    private readonly InputAction m_mainScene_Select;
    private readonly InputAction m_mainScene_Load;
    private readonly InputAction m_mainScene_Save;
    private readonly InputAction m_mainScene_NewChildNode;
    private readonly InputAction m_mainScene_MoveCamera;
    private readonly InputAction m_mainScene_ChangeCameraSize;
    private readonly InputAction m_mainScene_ConnectSelectedNodes;
    private readonly InputAction m_mainScene_ConnectToSelectedNodes;
    public struct MainSceneActions
    {
        private MainInput m_Wrapper;
        public MainSceneActions(MainInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @NewNode => m_Wrapper.m_mainScene_NewNode;
        public InputAction @Select => m_Wrapper.m_mainScene_Select;
        public InputAction @Load => m_Wrapper.m_mainScene_Load;
        public InputAction @Save => m_Wrapper.m_mainScene_Save;
        public InputAction @NewChildNode => m_Wrapper.m_mainScene_NewChildNode;
        public InputAction @MoveCamera => m_Wrapper.m_mainScene_MoveCamera;
        public InputAction @ChangeCameraSize => m_Wrapper.m_mainScene_ChangeCameraSize;
        public InputAction @ConnectSelectedNodes => m_Wrapper.m_mainScene_ConnectSelectedNodes;
        public InputAction @ConnectToSelectedNodes => m_Wrapper.m_mainScene_ConnectToSelectedNodes;
        public InputActionMap Get() { return m_Wrapper.m_mainScene; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainSceneActions set) { return set.Get(); }
        public void AddCallbacks(IMainSceneActions instance)
        {
            if (instance == null || m_Wrapper.m_MainSceneActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MainSceneActionsCallbackInterfaces.Add(instance);
            @NewNode.started += instance.OnNewNode;
            @NewNode.performed += instance.OnNewNode;
            @NewNode.canceled += instance.OnNewNode;
            @Select.started += instance.OnSelect;
            @Select.performed += instance.OnSelect;
            @Select.canceled += instance.OnSelect;
            @Load.started += instance.OnLoad;
            @Load.performed += instance.OnLoad;
            @Load.canceled += instance.OnLoad;
            @Save.started += instance.OnSave;
            @Save.performed += instance.OnSave;
            @Save.canceled += instance.OnSave;
            @NewChildNode.started += instance.OnNewChildNode;
            @NewChildNode.performed += instance.OnNewChildNode;
            @NewChildNode.canceled += instance.OnNewChildNode;
            @MoveCamera.started += instance.OnMoveCamera;
            @MoveCamera.performed += instance.OnMoveCamera;
            @MoveCamera.canceled += instance.OnMoveCamera;
            @ChangeCameraSize.started += instance.OnChangeCameraSize;
            @ChangeCameraSize.performed += instance.OnChangeCameraSize;
            @ChangeCameraSize.canceled += instance.OnChangeCameraSize;
            @ConnectSelectedNodes.started += instance.OnConnectSelectedNodes;
            @ConnectSelectedNodes.performed += instance.OnConnectSelectedNodes;
            @ConnectSelectedNodes.canceled += instance.OnConnectSelectedNodes;
            @ConnectToSelectedNodes.started += instance.OnConnectToSelectedNodes;
            @ConnectToSelectedNodes.performed += instance.OnConnectToSelectedNodes;
            @ConnectToSelectedNodes.canceled += instance.OnConnectToSelectedNodes;
        }

        private void UnregisterCallbacks(IMainSceneActions instance)
        {
            @NewNode.started -= instance.OnNewNode;
            @NewNode.performed -= instance.OnNewNode;
            @NewNode.canceled -= instance.OnNewNode;
            @Select.started -= instance.OnSelect;
            @Select.performed -= instance.OnSelect;
            @Select.canceled -= instance.OnSelect;
            @Load.started -= instance.OnLoad;
            @Load.performed -= instance.OnLoad;
            @Load.canceled -= instance.OnLoad;
            @Save.started -= instance.OnSave;
            @Save.performed -= instance.OnSave;
            @Save.canceled -= instance.OnSave;
            @NewChildNode.started -= instance.OnNewChildNode;
            @NewChildNode.performed -= instance.OnNewChildNode;
            @NewChildNode.canceled -= instance.OnNewChildNode;
            @MoveCamera.started -= instance.OnMoveCamera;
            @MoveCamera.performed -= instance.OnMoveCamera;
            @MoveCamera.canceled -= instance.OnMoveCamera;
            @ChangeCameraSize.started -= instance.OnChangeCameraSize;
            @ChangeCameraSize.performed -= instance.OnChangeCameraSize;
            @ChangeCameraSize.canceled -= instance.OnChangeCameraSize;
            @ConnectSelectedNodes.started -= instance.OnConnectSelectedNodes;
            @ConnectSelectedNodes.performed -= instance.OnConnectSelectedNodes;
            @ConnectSelectedNodes.canceled -= instance.OnConnectSelectedNodes;
            @ConnectToSelectedNodes.started -= instance.OnConnectToSelectedNodes;
            @ConnectToSelectedNodes.performed -= instance.OnConnectToSelectedNodes;
            @ConnectToSelectedNodes.canceled -= instance.OnConnectToSelectedNodes;
        }

        public void RemoveCallbacks(IMainSceneActions instance)
        {
            if (m_Wrapper.m_MainSceneActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMainSceneActions instance)
        {
            foreach (var item in m_Wrapper.m_MainSceneActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MainSceneActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MainSceneActions @mainScene => new MainSceneActions(this);
    private int m_PcSchemeIndex = -1;
    public InputControlScheme PcScheme
    {
        get
        {
            if (m_PcSchemeIndex == -1) m_PcSchemeIndex = asset.FindControlSchemeIndex("Pc");
            return asset.controlSchemes[m_PcSchemeIndex];
        }
    }
    public interface IMainSceneActions
    {
        void OnNewNode(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnLoad(InputAction.CallbackContext context);
        void OnSave(InputAction.CallbackContext context);
        void OnNewChildNode(InputAction.CallbackContext context);
        void OnMoveCamera(InputAction.CallbackContext context);
        void OnChangeCameraSize(InputAction.CallbackContext context);
        void OnConnectSelectedNodes(InputAction.CallbackContext context);
        void OnConnectToSelectedNodes(InputAction.CallbackContext context);
    }
}
