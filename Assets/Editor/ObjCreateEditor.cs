using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjCreateEditor : EditorWindow {

    private float x_poz_;
    private float y_poz_;
    private float z_poz_;
    private Vector3 instantiate_poz_;
    private Object instantiated_object_;
    private Texture tex_;

    private GameObject selected_object_;
    private Texture selected_tex_;
    private int count_of_grid_x_;
    private int count_of_grid_y_;

    /*
     * グリッド数を選択するポップアップUI用の配列
     */
    private string[] grid_value_str_list_ = new string[]{"2","4","6","8","10"};
    private int[] grid_value_list_ = new int[] {2,4,6,8,10};

    /*
     * 初期画面で選択した画像を保持しておく変数（MapCreate画面でグリッドに挿入する画像）
     */
    public Texture SelectedTex{
        get{return selected_tex_;}
    }

    public int CountOfGridX    
    {
        get
        {
            return count_of_grid_x_;
        }
    }

    public int CountOfGridY
    {
        get
        {
            return count_of_grid_y_;
        }
    }

    [UnityEditor.MenuItem("Editor/ObjCreateEditor")]
    private static void Create()
    {
        EditorWindow.GetWindow<ObjCreateEditor>("ObjectCreateEditor");
    }

    private void OnGUI()
    {
        //ObjCreate();

        DrawImgs();

        EditorGUILayout.Space();

        /*
         * グリッド数を選択するポップアップ
         */
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Label("グリッド数（X）:");
            count_of_grid_x_ = EditorGUILayout.IntPopup(count_of_grid_x_, grid_value_str_list_, grid_value_list_);
        }
        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.Label("グリッド数（Y）:");
            count_of_grid_y_ = EditorGUILayout.IntPopup(count_of_grid_y_, grid_value_str_list_, grid_value_list_);
        }

        /*
         * マップエディター 
         */
        if (GUILayout.Button("マップエディター"))
        {
            MapCreateEditor.ShowWindow(this);
        }
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

