using UnityEditor;
using UnityEngine;
public class CopyTransform : MonoBehaviour
{
    // Add a menu item named "Do Something" to MyMenu in the menu bar.
    //[MenuItem("MyMenu/Do Something")]
    //static void DoSomething()
    //{
    //    Debug.Log("Doing Something...");
    //}

    // Validated menu item.
    // Add a menu item named "Log Selected Transform Name" to MyMenu in the menu bar.
    // We use a second function to validate the menu item
    // so it will only be enabled if we have a transform selected.
    //[MenuItem("MyMenu/Log Selected Transform Name")]
    //static void LogSelectedTransformName()
    //{
    //    Debug.Log("Selected Transform is on " + Selection.activeTransform.gameObject.name + ".");
    //}

    // Validate the menu item defined by the function above.
    // The menu item will be disabled if this function returns false.
    [MenuItem("CustomCopy/Log Selected Transform Name", true)]
    static bool ValidateLogSelectedTransformName()
    {
        // Return false if no transform is selected.
        return Selection.activeTransform != null;
    }

    // Add a menu item named "Do Something with a Shortcut Key" to MyMenu in the menu bar
    // and give it a shortcut (ctrl-g on Windows, cmd-g on macOS).
    [MenuItem("CustomCopy/Copy RectTransform _c")]
    static void DoSomethingWithAShortcutKey()
    {
     //   Debug.Log("Doing something with a Shortcut Key...");
        UnityEditorInternal.ComponentUtility.CopyComponent(
            Selection.activeTransform.GetComponent<RectTransform>() ?
                Selection.activeTransform.GetComponent<RectTransform>()
                : Selection.activeTransform.GetComponent<Transform>());
       // Debug.Log("Doing something with a Shortcut Key..."+ Selection.activeTransform.position);
    }


    [MenuItem("CustomCopy/ImageReplace _l")]
    static void DoSomethingWithAShortcutKey2()
    {
        //   Debug.Log("Doing something with a Shortcut Key...");
        if (Selection.activeTransform != null)
        {
          
            UnityEditorInternal.ComponentUtility.PasteComponentValues(
                Selection.activeTransform.GetComponent<RectTransform>() ?
                Selection.activeTransform.GetComponent<RectTransform>()
                : Selection.activeTransform.GetComponent<Transform>()
                );
        }
        else
        {
            Debug.Log("Select A Transform First ");
        }
        // Debug.Log("Doing something with a Shortcut Key..." + Selection.activeTransform.position);
    }



    [MenuItem("CustomCopy/Paste RectTransform _v")]
    static void DoSomethingWithAShortcutKey1()
    {
        //   Debug.Log("Doing something with a Shortcut Key...");
        if (Selection.activeTransform != null)
        {

            UnityEditorInternal.ComponentUtility.PasteComponentValues(
                Selection.activeTransform.GetComponent<RectTransform>()?
                Selection.activeTransform.GetComponent<RectTransform>()
                : Selection.activeTransform.GetComponent<Transform>()
                );
        }
        else {
            Debug.Log("Select A Transform First ");
        }
        // Debug.Log("Doing something with a Shortcut Key..." + Selection.activeTransform.position);
    }


    // Add a menu item called "Double Mass" to a Rigidbody's context menu.
    [MenuItem("CONTEXT/Rigidbody/Double Mass")]
    static void DoubleMass(MenuCommand command)
    {
        Rigidbody body = (Rigidbody)command.context;
        body.mass = body.mass * 2;
        Debug.Log("Doubled Rigidbody's Mass to " + body.mass + " from Context Menu.");
    }

    // Add a menu item to create custom GameObjects.
    // Priority 1 ensures it is grouped with the other menu items of the same kind
    // and propagated to the hierarchy dropdown and hierarchy context menus.
    [MenuItem("GameObject/MyCategory/Custom Game Object", false, 10)]
    static void CreateCustomGameObject(MenuCommand menuCommand)
    {
        // Create a custom game object
        GameObject go = new GameObject("Custom Game Object");
        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
}