//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.13.0
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

/// <summary>
/// Provides programmatic access to <see cref="InputActionAsset" />, <see cref="InputActionMap" />, <see cref="InputAction" /> and <see cref="InputControlScheme" /> instances defined in asset "Assets/inputs/MainInput.inputactions".
/// </summary>
/// <remarks>
/// This class is source generated and any manual edits will be discarded if the associated asset is reimported or modified.
/// </remarks>
/// <example>
/// <code>
/// using namespace UnityEngine;
/// using UnityEngine.InputSystem;
///
/// // Example of using an InputActionMap named "Player" from a UnityEngine.MonoBehaviour implementing callback interface.
/// public class Example : MonoBehaviour, MyActions.IPlayerActions
/// {
///     private MyActions_Actions m_Actions;                  // Source code representation of asset.
///     private MyActions_Actions.PlayerActions m_Player;     // Source code representation of action map.
///
///     void Awake()
///     {
///         m_Actions = new MyActions_Actions();              // Create asset object.
///         m_Player = m_Actions.Player;                      // Extract action map object.
///         m_Player.AddCallbacks(this);                      // Register callback interface IPlayerActions.
///     }
///
///     void OnDestroy()
///     {
///         m_Actions.Dispose();                              // Destroy asset object.
///     }
///
///     void OnEnable()
///     {
///         m_Player.Enable();                                // Enable all actions within map.
///     }
///
///     void OnDisable()
///     {
///         m_Player.Disable();                               // Disable all actions within map.
///     }
///
///     #region Interface implementation of MyActions.IPlayerActions
///
///     // Invoked when "Move" action is either started, performed or canceled.
///     public void OnMove(InputAction.CallbackContext context)
///     {
///         Debug.Log($"OnMove: {context.ReadValue&lt;Vector2&gt;()}");
///     }
///
///     // Invoked when "Attack" action is either started, performed or canceled.
///     public void OnAttack(InputAction.CallbackContext context)
///     {
///         Debug.Log($"OnAttack: {context.ReadValue&lt;float&gt;()}");
///     }
///
///     #endregion
/// }
/// </code>
/// </example>
public partial class @MainInput: IInputActionCollection2, IDisposable
{
    /// <summary>
    /// Provides access to the underlying asset instance.
    /// </summary>
    public InputActionAsset asset { get; }

    /// <summary>
    /// Constructs a new instance.
    /// </summary>
    public @MainInput()
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
                    ""expectedControlType"": """",
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
                    ""expectedControlType"": """",
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
                    ""type"": ""Value"",
                    ""id"": ""7b1737d2-7fd8-4116-ba85-a55f332cfba5"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
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
                    ""path"": ""<Keyboard>/o"",
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
                    ""path"": ""<Keyboard>/j"",
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
                    ""path"": ""<Keyboard>/k"",
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
                    ""name"": ""positive"",
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
                    ""name"": ""negative"",
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

    ~@MainInput()
    {
        UnityEngine.Debug.Assert(!m_mainScene.enabled, "This will cause a leak and performance issues, MainInput.mainScene.Disable() has not been called.");
    }

    /// <summary>
    /// Destroys this asset and all associated <see cref="InputAction"/> instances.
    /// </summary>
    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.bindingMask" />
    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.devices" />
    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.controlSchemes" />
    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.Contains(InputAction)" />
    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.GetEnumerator()" />
    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    /// <inheritdoc cref="IEnumerable.GetEnumerator()" />
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.Enable()" />
    public void Enable()
    {
        asset.Enable();
    }

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.Disable()" />
    public void Disable()
    {
        asset.Disable();
    }

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.bindings" />
    public IEnumerable<InputBinding> bindings => asset.bindings;

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.FindAction(string, bool)" />
    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.FindBinding(InputBinding, out InputAction)" />
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
    /// <summary>
    /// Provides access to input actions defined in input action map "mainScene".
    /// </summary>
    public struct MainSceneActions
    {
        private @MainInput m_Wrapper;

        /// <summary>
        /// Construct a new instance of the input action map wrapper class.
        /// </summary>
        public MainSceneActions(@MainInput wrapper) { m_Wrapper = wrapper; }
        /// <summary>
        /// Provides access to the underlying input action "mainScene/NewNode".
        /// </summary>
        public InputAction @NewNode => m_Wrapper.m_mainScene_NewNode;
        /// <summary>
        /// Provides access to the underlying input action "mainScene/Select".
        /// </summary>
        public InputAction @Select => m_Wrapper.m_mainScene_Select;
        /// <summary>
        /// Provides access to the underlying input action "mainScene/Load".
        /// </summary>
        public InputAction @Load => m_Wrapper.m_mainScene_Load;
        /// <summary>
        /// Provides access to the underlying input action "mainScene/Save".
        /// </summary>
        public InputAction @Save => m_Wrapper.m_mainScene_Save;
        /// <summary>
        /// Provides access to the underlying input action "mainScene/NewChildNode".
        /// </summary>
        public InputAction @NewChildNode => m_Wrapper.m_mainScene_NewChildNode;
        /// <summary>
        /// Provides access to the underlying input action "mainScene/MoveCamera".
        /// </summary>
        public InputAction @MoveCamera => m_Wrapper.m_mainScene_MoveCamera;
        /// <summary>
        /// Provides access to the underlying input action "mainScene/ChangeCameraSize".
        /// </summary>
        public InputAction @ChangeCameraSize => m_Wrapper.m_mainScene_ChangeCameraSize;
        /// <summary>
        /// Provides access to the underlying input action "mainScene/ConnectSelectedNodes".
        /// </summary>
        public InputAction @ConnectSelectedNodes => m_Wrapper.m_mainScene_ConnectSelectedNodes;
        /// <summary>
        /// Provides access to the underlying input action "mainScene/ConnectToSelectedNodes".
        /// </summary>
        public InputAction @ConnectToSelectedNodes => m_Wrapper.m_mainScene_ConnectToSelectedNodes;
        /// <summary>
        /// Provides access to the underlying input action map instance.
        /// </summary>
        public InputActionMap Get() { return m_Wrapper.m_mainScene; }
        /// <inheritdoc cref="UnityEngine.InputSystem.InputActionMap.Enable()" />
        public void Enable() { Get().Enable(); }
        /// <inheritdoc cref="UnityEngine.InputSystem.InputActionMap.Disable()" />
        public void Disable() { Get().Disable(); }
        /// <inheritdoc cref="UnityEngine.InputSystem.InputActionMap.enabled" />
        public bool enabled => Get().enabled;
        /// <summary>
        /// Implicitly converts an <see ref="MainSceneActions" /> to an <see ref="InputActionMap" /> instance.
        /// </summary>
        public static implicit operator InputActionMap(MainSceneActions set) { return set.Get(); }
        /// <summary>
        /// Adds <see cref="InputAction.started"/>, <see cref="InputAction.performed"/> and <see cref="InputAction.canceled"/> callbacks provided via <param cref="instance" /> on all input actions contained in this map.
        /// </summary>
        /// <param name="instance">Callback instance.</param>
        /// <remarks>
        /// If <paramref name="instance" /> is <c>null</c> or <paramref name="instance"/> have already been added this method does nothing.
        /// </remarks>
        /// <seealso cref="MainSceneActions" />
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

        /// <summary>
        /// Removes <see cref="InputAction.started"/>, <see cref="InputAction.performed"/> and <see cref="InputAction.canceled"/> callbacks provided via <param cref="instance" /> on all input actions contained in this map.
        /// </summary>
        /// <remarks>
        /// Calling this method when <paramref name="instance" /> have not previously been registered has no side-effects.
        /// </remarks>
        /// <seealso cref="MainSceneActions" />
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

        /// <summary>
        /// Unregisters <param cref="instance" /> and unregisters all input action callbacks via <see cref="MainSceneActions.UnregisterCallbacks(IMainSceneActions)" />.
        /// </summary>
        /// <seealso cref="MainSceneActions.UnregisterCallbacks(IMainSceneActions)" />
        public void RemoveCallbacks(IMainSceneActions instance)
        {
            if (m_Wrapper.m_MainSceneActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        /// <summary>
        /// Replaces all existing callback instances and previously registered input action callbacks associated with them with callbacks provided via <param cref="instance" />.
        /// </summary>
        /// <remarks>
        /// If <paramref name="instance" /> is <c>null</c>, calling this method will only unregister all existing callbacks but not register any new callbacks.
        /// </remarks>
        /// <seealso cref="MainSceneActions.AddCallbacks(IMainSceneActions)" />
        /// <seealso cref="MainSceneActions.RemoveCallbacks(IMainSceneActions)" />
        /// <seealso cref="MainSceneActions.UnregisterCallbacks(IMainSceneActions)" />
        public void SetCallbacks(IMainSceneActions instance)
        {
            foreach (var item in m_Wrapper.m_MainSceneActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MainSceneActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    /// <summary>
    /// Provides a new <see cref="MainSceneActions" /> instance referencing this action map.
    /// </summary>
    public MainSceneActions @mainScene => new MainSceneActions(this);
    private int m_PcSchemeIndex = -1;
    /// <summary>
    /// Provides access to the input control scheme.
    /// </summary>
    /// <seealso cref="UnityEngine.InputSystem.InputControlScheme" />
    public InputControlScheme PcScheme
    {
        get
        {
            if (m_PcSchemeIndex == -1) m_PcSchemeIndex = asset.FindControlSchemeIndex("Pc");
            return asset.controlSchemes[m_PcSchemeIndex];
        }
    }
    /// <summary>
    /// Interface to implement callback methods for all input action callbacks associated with input actions defined by "mainScene" which allows adding and removing callbacks.
    /// </summary>
    /// <seealso cref="MainSceneActions.AddCallbacks(IMainSceneActions)" />
    /// <seealso cref="MainSceneActions.RemoveCallbacks(IMainSceneActions)" />
    public interface IMainSceneActions
    {
        /// <summary>
        /// Method invoked when associated input action "NewNode" is either <see cref="UnityEngine.InputSystem.InputAction.started" />, <see cref="UnityEngine.InputSystem.InputAction.performed" /> or <see cref="UnityEngine.InputSystem.InputAction.canceled" />.
        /// </summary>
        /// <seealso cref="UnityEngine.InputSystem.InputAction.started" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.performed" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.canceled" />
        void OnNewNode(InputAction.CallbackContext context);
        /// <summary>
        /// Method invoked when associated input action "Select" is either <see cref="UnityEngine.InputSystem.InputAction.started" />, <see cref="UnityEngine.InputSystem.InputAction.performed" /> or <see cref="UnityEngine.InputSystem.InputAction.canceled" />.
        /// </summary>
        /// <seealso cref="UnityEngine.InputSystem.InputAction.started" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.performed" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.canceled" />
        void OnSelect(InputAction.CallbackContext context);
        /// <summary>
        /// Method invoked when associated input action "Load" is either <see cref="UnityEngine.InputSystem.InputAction.started" />, <see cref="UnityEngine.InputSystem.InputAction.performed" /> or <see cref="UnityEngine.InputSystem.InputAction.canceled" />.
        /// </summary>
        /// <seealso cref="UnityEngine.InputSystem.InputAction.started" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.performed" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.canceled" />
        void OnLoad(InputAction.CallbackContext context);
        /// <summary>
        /// Method invoked when associated input action "Save" is either <see cref="UnityEngine.InputSystem.InputAction.started" />, <see cref="UnityEngine.InputSystem.InputAction.performed" /> or <see cref="UnityEngine.InputSystem.InputAction.canceled" />.
        /// </summary>
        /// <seealso cref="UnityEngine.InputSystem.InputAction.started" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.performed" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.canceled" />
        void OnSave(InputAction.CallbackContext context);
        /// <summary>
        /// Method invoked when associated input action "NewChildNode" is either <see cref="UnityEngine.InputSystem.InputAction.started" />, <see cref="UnityEngine.InputSystem.InputAction.performed" /> or <see cref="UnityEngine.InputSystem.InputAction.canceled" />.
        /// </summary>
        /// <seealso cref="UnityEngine.InputSystem.InputAction.started" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.performed" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.canceled" />
        void OnNewChildNode(InputAction.CallbackContext context);
        /// <summary>
        /// Method invoked when associated input action "MoveCamera" is either <see cref="UnityEngine.InputSystem.InputAction.started" />, <see cref="UnityEngine.InputSystem.InputAction.performed" /> or <see cref="UnityEngine.InputSystem.InputAction.canceled" />.
        /// </summary>
        /// <seealso cref="UnityEngine.InputSystem.InputAction.started" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.performed" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.canceled" />
        void OnMoveCamera(InputAction.CallbackContext context);
        /// <summary>
        /// Method invoked when associated input action "ChangeCameraSize" is either <see cref="UnityEngine.InputSystem.InputAction.started" />, <see cref="UnityEngine.InputSystem.InputAction.performed" /> or <see cref="UnityEngine.InputSystem.InputAction.canceled" />.
        /// </summary>
        /// <seealso cref="UnityEngine.InputSystem.InputAction.started" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.performed" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.canceled" />
        void OnChangeCameraSize(InputAction.CallbackContext context);
        /// <summary>
        /// Method invoked when associated input action "ConnectSelectedNodes" is either <see cref="UnityEngine.InputSystem.InputAction.started" />, <see cref="UnityEngine.InputSystem.InputAction.performed" /> or <see cref="UnityEngine.InputSystem.InputAction.canceled" />.
        /// </summary>
        /// <seealso cref="UnityEngine.InputSystem.InputAction.started" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.performed" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.canceled" />
        void OnConnectSelectedNodes(InputAction.CallbackContext context);
        /// <summary>
        /// Method invoked when associated input action "ConnectToSelectedNodes" is either <see cref="UnityEngine.InputSystem.InputAction.started" />, <see cref="UnityEngine.InputSystem.InputAction.performed" /> or <see cref="UnityEngine.InputSystem.InputAction.canceled" />.
        /// </summary>
        /// <seealso cref="UnityEngine.InputSystem.InputAction.started" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.performed" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.canceled" />
        void OnConnectToSelectedNodes(InputAction.CallbackContext context);
    }
}
