
using UnityEngine;
using UnityEditor;
using UnityEngine.XR.Interaction.Toolkit;

public class CreateGrabInteractableWindow : EditorWindow
{
    public enum InteractorOptions { XRGrabInteractable, OffsetGrabInteractable, XRSimpleInteractable }
    public InteractorOptions interactorType; 
    
    public enum ColliderOptions { MeshCollider, BoxCollider, SphereCollider, CapsuleCollider}
    public ColliderOptions colliderType;  
    
    public enum MovementOptions { VelocityTracking, Kinematic, Instantaneous}
    public MovementOptions movementType;

    [MenuItem("Window/Create VR Ineractable")]
    private static void ShowWindow() //opens the editor window
    {
        GetWindow<CreateGrabInteractableWindow>("Create VR Interactable");
    }
    private void OnGUI() // this method draws the UI
    {
        GUILayout.Label("Select your GameObjects in the scene to change into a VR interactable!", EditorStyles.wordWrappedLabel);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        GUILayout.Label("Selection Count: " + Selection.gameObjects.Length, EditorStyles.boldLabel);
        EditorGUILayout.Space();

        GUILayout.Label("Collider Type");
        colliderType = (ColliderOptions)EditorGUILayout.EnumPopup(colliderType);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Interactor Type");
        interactorType = (InteractorOptions)EditorGUILayout.EnumPopup(interactorType);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Movement Type");
        movementType = (MovementOptions)EditorGUILayout.EnumPopup(movementType);
        EditorGUILayout.Space();

        if (GUILayout.Button("Create Interactable"))
        {
            CreateInteractable();
        }
    }

    private void CreateInteractable()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            AddCollider(obj);
            AddInteractable(obj);
        }
    }

    private void AddCollider(GameObject obj)
    {
        MeshCollider meshCollider = obj.GetComponent<MeshCollider>();
        BoxCollider boxCollider = obj.GetComponent<BoxCollider>();
        SphereCollider sphereCollider = obj.GetComponent<SphereCollider>();
        CapsuleCollider capsuleCollider = obj.GetComponent<CapsuleCollider>();
        switch (colliderType)
        {
            case ColliderOptions.MeshCollider:
                if (meshCollider == null)
                {
                    DestroyImmediate(boxCollider);
                    DestroyImmediate(sphereCollider);
                    DestroyImmediate(capsuleCollider);
                    meshCollider = obj.AddComponent<MeshCollider>();
                    meshCollider.convex = true;
                }
                break;

            case ColliderOptions.BoxCollider:
                if (boxCollider == null)
                {
                    DestroyImmediate(meshCollider);
                    DestroyImmediate(sphereCollider);
                    DestroyImmediate(capsuleCollider);
                    boxCollider = obj.AddComponent<BoxCollider>();
                }
                break;

            case ColliderOptions.SphereCollider:
                if (sphereCollider == null)
                {
                    DestroyImmediate(meshCollider);
                    DestroyImmediate(boxCollider);
                    DestroyImmediate(capsuleCollider);
                    sphereCollider = obj.AddComponent<SphereCollider>();
                }
                break;

            case ColliderOptions.CapsuleCollider:
                if (capsuleCollider == null)
                {
                    DestroyImmediate(meshCollider);
                    DestroyImmediate(sphereCollider);
                    DestroyImmediate(boxCollider);
                    capsuleCollider = obj.AddComponent<CapsuleCollider>();
                }
                break;
        }
    }

    private void AddInteractable(GameObject obj)
    {
        OffsetInteractable offsetInteractable = obj.GetComponent<OffsetInteractable>();
        XRGrabInteractable xrInteractable = obj.GetComponent<XRGrabInteractable>();
        XRSimpleInteractable xrSimpleInteractable = obj.GetComponent<XRSimpleInteractable>();
        switch (interactorType)
        {
            case InteractorOptions.XRGrabInteractable:
                if (xrInteractable == null || offsetInteractable != null)
                {
                    DestroyImmediate(offsetInteractable);
                    DestroyImmediate(xrSimpleInteractable);
                    xrInteractable = obj.AddComponent<XRGrabInteractable>();
                }

                switch (movementType)
                {
                    case MovementOptions.VelocityTracking:
                        xrInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking;
                        break;
                    case MovementOptions.Kinematic:
                        xrInteractable.movementType = XRBaseInteractable.MovementType.Kinematic;
                        break;
                    case MovementOptions.Instantaneous:
                        xrInteractable.movementType = XRBaseInteractable.MovementType.Instantaneous;
                        break;
                }
                break;

            case InteractorOptions.OffsetGrabInteractable:
                if (offsetInteractable == null)
                {
                    DestroyImmediate(xrInteractable);
                    DestroyImmediate(xrSimpleInteractable);
                    offsetInteractable = obj.AddComponent<OffsetInteractable>();
                }

                switch (movementType)
                {
                    case MovementOptions.VelocityTracking:
                        offsetInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking;
                        break;
                    case MovementOptions.Kinematic:
                        offsetInteractable.movementType = XRBaseInteractable.MovementType.Kinematic;
                        break;
                    case MovementOptions.Instantaneous:
                        offsetInteractable.movementType = XRBaseInteractable.MovementType.Instantaneous;
                        break;
                }
                break;

            case InteractorOptions.XRSimpleInteractable:
                if (xrSimpleInteractable == null)
                {
                    DestroyImmediate(xrInteractable);
                    DestroyImmediate(offsetInteractable);
                    obj.AddComponent<XRSimpleInteractable>();
                }
                break;
        }
    }
}
