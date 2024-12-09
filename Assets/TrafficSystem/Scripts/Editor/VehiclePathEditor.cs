using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(VehiclePath))]
public class VehiclePathEditor : Editor
{
    // Start is called before the first frame update
    bool placing;
    VehiclePath pathVisual;
    Vector3 offSet = new Vector3(0,0.05f,0);
    public override void OnInspectorGUI()
    {
        SceneView view = GetSceneView();
        DrawDefaultInspector();
         pathVisual = (VehiclePath)target;




        EditorGUILayout.Space();
        if (!placing && GUILayout.Button("Start PathEditing",GUILayout.Height(30)))
        {

            placing = true;
            //focus sceneview for placement
            view.Focus();
        }
        GUI.backgroundColor = Color.yellow;
        if (placing && GUILayout.Button("Finish PathEditing", GUILayout.Height(30)))
        {
            placing = false;
        }
        GUI.backgroundColor = Color.white;
        EditorGUILayout.Space();
        //if (GUILayout.Button("GenerateArrows", GUILayout.Height(30)))
        //{

        //    pathVisual.GenerateArrows();
        //}
    }

    public void OnSceneGUI()
    {
        //with creation mode enabled, place new waypoints on keypress
        if (Event.current.type != EventType.KeyDown || !placing) return;


        if (Event.current.keyCode == KeyCode.P)
        {
            //cast a ray against mouse position
            Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(worldRay, out hitInfo))
            {
                Event.current.Use();

                // pathVisual.GenerateNewPoint(hitInfo.point);
                if (pathVisual == null)
                    return;
                GameObject NewPoint = new GameObject(pathVisual.transform.childCount + "");
                NewPoint.transform.position = hitInfo.point+offSet;
                NewPoint.transform.rotation = Quaternion.identity;
                NewPoint.AddComponent<Node>();
                NewPoint.AddComponent<BoxCollider>();
                NewPoint.GetComponent<BoxCollider>().isTrigger=true;
                NewPoint.transform.SetParent(pathVisual.transform);

            }
        }


    }


    public static SceneView GetSceneView()
    {
        SceneView view = SceneView.lastActiveSceneView;
        if (view == null)
            view = EditorWindow.GetWindow<SceneView>();

        return view;
    }
}
