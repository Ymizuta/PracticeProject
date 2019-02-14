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
    private Texture tex_;

    private GameObject selected_object_;
    public Texture selected_tex_;

    [UnityEditor.MenuItem("Editor/ObjCreateEditor")]
    private static void Create()
    {
        EditorWindow.GetWindow<ObjCreateEditor>("ObjectCreateEditor");
    }

    private void OnGUI()
    {
        //ObjCreate();

        DrawImgs();

        /*
         * マップエディター 
         */
        if (GUILayout.Button("マップエディター"))
        {
            MapCreateEditor.ShowWindow(this);
        }

        //EditorGUILayout.Space();

    }

    private void ObjCreate()
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
            GameObject new_object = Instantiate(instantiated_object_, instantiate_poz_, Quaternion.identity) as GameObject;
        }
        EditorGUILayout.Space();
    }

    void DrawImgs()
    {
        /*
         * 設置するオブジェクを画像で表示する
         */
        float default_x_ = 0.0f;             //並んでいるグリッドの幅の合計の計算用（初期値）
        float x_ = 0.0f;                     //並んでいるグリッドの幅の合計の計算用
        float y_ = 0.0f;                     //並んでいるグリッドの高さの合計の計算用
        float img_width_ = 50.0f;           //グリッドの幅
        float max_size_x_ = 300.0f;         //グリッドの横幅の最大値
        float img_height_ = 50.0f;          //グリッドの高さ
        Object[] tex_list_;

        tex_list_ = Resources.LoadAll("Textures", typeof(Texture));
        x_ = default_x_;

        GUILayout.BeginHorizontal();
        //foreachのほうが良い？
        for (int i = 0; i < tex_list_.Length; i++)
        {
            if (x_ > max_size_x_)
            {
                x_ = default_x_;
                y_ += img_height_;
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
            }
            if (GUILayout.Button((Texture)tex_list_[i], GUILayout.MaxWidth(img_width_), GUILayout.MaxHeight(img_height_)))
            {                
                selected_object_ = Resources.Load("Prefab/"+tex_list_[i].name) as GameObject;
                selected_tex_ = ((Texture)Resources.Load("Textures/"+tex_list_[i].name));
            }
            //次の画像の位置を計算
            x_ += img_width_;
        }
        GUILayout.EndHorizontal();
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

