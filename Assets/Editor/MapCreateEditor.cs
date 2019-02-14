using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapCreateEditor : EditorWindow {

    float default_x = 0.0f;     //並んでいるグリッドの幅の合計の計算用（初期値）
    float w = 50.0f;            //グリッドの幅
    float mapsize_x_ = 300.0f;  //グリッドの横幅の最大値
    float h = 50.0f;            //グリッドの高さ
    int number_of_grid_ = 35;
    Rect[] rect_list_;
    string[] map_data_list_;    //位置情報を格納


    private static EditorWindow parent_window_;

    public static void ShowWindow(EditorWindow parent_window)
    {
        MapCreateEditor window = EditorWindow.GetWindow<MapCreateEditor>("MapWindow");
        parent_window_ = parent_window;
        window.Init();
    }

    void Init()
    {
        map_data_list_ = new string[number_of_grid_];
        DrawGrid();
    }

    private void OnGUI()
    {
        //Initでは描画されないのはなぜか？
        foreach (var r in rect_list_)
        {
            DrawGridLine(r);
        }

        InsertTexture();

        for(int i = 0;i<number_of_grid_;i++)
        {
            if (map_data_list_[i] != "")
            {
                Texture2D tex = ((Texture2D)Resources.Load("Textures/"+map_data_list_[i]));
                GUI.DrawTexture(rect_list_[i], tex, ScaleMode.ScaleToFit, true, 0);
            }
        }

        GUILayout.FlexibleSpace();
        if (GUILayout.Button("リセット"))
        {
            DrawGrid();
        }
    }

    private void DrawGrid()
    {
        ///*
        // *マップ 
        // */
        float x = 0.0f;             //並んでいるグリッドの幅の合計の計算用
        float y = 0.0f;             //並んでいるグリッドの高さの合計の計算用

        /*
         * Rectを格納するリストを初期化
         */ 
        rect_list_ = new Rect[number_of_grid_];

        GUILayout.BeginHorizontal();
        for (int i = 0; i < number_of_grid_; i++)
        {
            /*
             * Rectの生成位置が上限幅を超過している場合にグリッドの描画する行をリセット（次の行へ）
             */
            if (x > mapsize_x_)
            {
                x = default_x;
                y += h;
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
            }

            /*
             * Rectを配列に格納・mapデータ配列に格納
             */ 
            rect_list_[i] = new Rect(new Vector2(x, y), new Vector2(w, h));
            map_data_list_[i] = "";

            //次のRectの位置を計算
            x += w;
        }
        GUILayout.EndHorizontal();
    }

    /*
     * 画像をグリッドに挿入
     */
    void InsertTexture()
    {
        /*
         * クリックしたRectの位置を検索
         */
        Event e = Event.current;
        if (e.type == EventType.MouseDown)
        {
            Vector2 mouse_poz = Event.current.mousePosition;
            for (int i = 0; i < number_of_grid_; i++)
            {
                //クリックした位置のx座標の位置がRectの位置に該当するかを検索
                if (rect_list_[i].x <= mouse_poz.x && mouse_poz.x < rect_list_[i].x + w)
                {
                    //クリックした位置のy座標の位置がRectの位置に該当するかを検索
                    if (rect_list_[i].y <= mouse_poz.y && mouse_poz.y < rect_list_[i].y + h)
                    {
                        //画像をRectに挿入
                        if (((ObjCreateEditor)parent_window_).selected_tex_ != null)
                        {
                            Debug.Log("Rect:" + i + "をクリックしたよ！");
                            map_data_list_[i] = ((ObjCreateEditor)parent_window_).selected_tex_.name;
                        }
                    }
                }
            }
        }
    }

    /*
     * グリッド線の描画
     */
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
