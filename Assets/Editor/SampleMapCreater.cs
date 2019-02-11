using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SampleMapCreater : EditorWindow {

    private float x_poz_;
    private float y_poz_;
    private float z_poz_;
    private Object instantiated_object_;

    [UnityEditor.MenuItem("Editor/SampleMapCreater")]
    private static void Create()
    {
        EditorWindow.GetWindow<SampleMapCreater>("SampleMapCreater");
    }

    private void OnGUI()
    {
        float default_x = 0.0f;
        float x = 0.0f;
        float y = 0.0f;
        float w = 50.0f;        
        float maxw = 300.0f;
        float h = 50.0f;

        //後で関数化する
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Label("生成オブジェクト:", GUILayout.Width(100));
            instantiated_object_ = EditorGUILayout.ObjectField(instantiated_object_,typeof(GameObject)) as GameObject;
        }
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Label("X:",GUILayout.Width(100));
            x_poz_ = EditorGUILayout.FloatField(0);
        }
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Label("y:",GUILayout.Width(100));
            y_poz_ = EditorGUILayout.FloatField(0);
        }
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Label("y:", GUILayout.Width(100));
            z_poz_ = EditorGUILayout.FloatField(0);
        }

        GUILayout.Button("オブジェクト生成");
              
        //GUILayout.BeginHorizontal();
        //for (int i = 1; i<10; i++)
        //{
        //    x += w;
        //    if (x > maxw)
        //    {
        //        x = default_x;
        //        GUILayout.EndHorizontal();
        //    }
        //    if (x == default_x)
        //    {
        //        GUILayout.BeginHorizontal();
        //    }
        //    GUILayout.Button("ボタン" + i, GUILayout.Width(w), GUILayout.Height(h));
        //    Rect r = new Rect(new Vector2(x,y),new Vector2(w,h));
        //}
    }

}
