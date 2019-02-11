using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjCreateEditor : EditorWindow{
    private float x_poz_;
    private float y_poz_;
    private float z_poz_;
    private Vector3 instantiate_poz_;
    private Object instantiated_object_;

    //private int number_of_instances_;
    //private float min_x_;
    //private float max_x_;
    //private float min_y_;
    //private float max_y_;
    //private float min_z_;
    //private float max_z_;

    [UnityEditor.MenuItem("Editor/ObjCreateEditor")]
    private static void Create()
    {
        EditorWindow.GetWindow<ObjCreateEditor>("ObjectCreateEditor");
    }

    private void OnGUI()
    {
        GUILayout.Label("個別オブジェクト生成");
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Label("生成オブジェクト:", GUILayout.Width(100));
            instantiated_object_ = EditorGUILayout.ObjectField(instantiated_object_, typeof(GameObject)) as GameObject;
        }
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Label("X:", GUILayout.Width(100));
            x_poz_ = EditorGUILayout.FloatField(x_poz_);
        }
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Label("y:", GUILayout.Width(100));
            y_poz_ = EditorGUILayout.FloatField(y_poz_);
        }
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Label("z:", GUILayout.Width(100));
            z_poz_ = EditorGUILayout.FloatField(z_poz_);
        }

        if (GUILayout.Button("オブジェクト生成"))
        {
            instantiate_poz_.x = x_poz_;
            instantiate_poz_.y = y_poz_;
            instantiate_poz_.z = z_poz_; 
            GameObject new_object = Instantiate(instantiated_object_,instantiate_poz_,Quaternion.identity) as GameObject;
        }
        EditorGUILayout.Space();

        
        ///*
        // *マップ 
        // */
        //float default_x = 0.0f;
        //float x = 0.0f;
        //float y = 0.0f;
        //float w = 50.0f;
        //float maxw = 300.0f;
        //float h = 50.0f;

        //GUILayout.BeginHorizontal();
        //for (int i = 0; i <= 10; i++)
        //{
        //    x += w;
        //    if (x > maxw)
        //    {
        //        x = default_x;
        //        y += h;
        //        GUILayout.EndHorizontal();
        //    }
        //    if (x == default_x)
        //    {
        //        GUILayout.BeginHorizontal();
        //    }
        //    Rect r = new Rect(new Vector2(x,y),new Vector2(w,h));
        //    DrawGridLine(r);
        //}
        //GUILayout.EndHorizontal();    
}

private void DrawGridLine(Rect r)
    {
        // grid
        Handles.color = new Color(1f, 1f, 1f, 0.5f);
        // upper line
        Handles.DrawLine(
            new Vector2(r.position.x, r.position.y),
            new Vector2(r.position.x + r.size.x, r.position.y));
        // bottom line
        Handles.DrawLine(
            new Vector2(r.position.x, r.position.y + r.size.y),
            new Vector2(r.position.x + r.size.x, r.position.y + r.size.y));
        // left line
        Handles.DrawLine(
            new Vector2(r.position.x, r.position.y),
            new Vector2(r.position.x, r.position.y + r.size.y));
        // right line
        Handles.DrawLine(
            new Vector2(r.position.x + r.size.x, r.position.y),
            new Vector2(r.position.x + r.size.x, r.position.y + r.size.y));
    }
}

///*
// *ランダムオブジェクト生成 
// */
//GUILayout.Label("ランダムオブジェクト生成");
//using (new EditorGUILayout.HorizontalScope())
//{
//    GUILayout.Label("生成オブジェクト:", GUILayout.Width(100));
//    instantiated_object_ = EditorGUILayout.ObjectField(instantiated_object_, typeof(GameObject)) as GameObject;
//}
//using (new EditorGUILayout.HorizontalScope())
//{
//    GUILayout.Label("生成個数:", GUILayout.Width(100));
//    number_of_instances_ = EditorGUILayout.IntField(number_of_instances_);
//}
//using (new EditorGUILayout.HorizontalScope())
//{
//    GUILayout.Label("生成範囲X:", GUILayout.Width(100));
//    min_x_ = EditorGUILayout.FloatField(min_x_);
//    max_x_ = EditorGUILayout.FloatField(max_x_);
//}
//using (new EditorGUILayout.HorizontalScope())
//{
//    GUILayout.Label("生成範囲y:", GUILayout.Width(100));
//    min_y_ = EditorGUILayout.FloatField(min_y_);
//    max_y_ = EditorGUILayout.FloatField(max_y_);
//}
//using (new EditorGUILayout.HorizontalScope())
//{
//    GUILayout.Label("生成範囲z:", GUILayout.Width(100));
//    min_z_ = EditorGUILayout.FloatField(min_z_);
//    max_z_ = EditorGUILayout.FloatField(max_z_);
//}
//if (GUILayout.Button("ランダムオブジェクト生成"))
//{

//}

