﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MapCreateEditor : EditorWindow {

    float default_x = 0.0f;     //並んでいるグリッドの幅の合計の計算用（初期値）
    float w = 50.0f;            //グリッドの幅
    float mapsize_x_ = 300.0f;  //グリッドの横幅の最大値
    float h = 50.0f;            //グリッドの高さ
    int number_of_grid_ = 35;   //グリッドの総数
    Rect[] rect_list_;          //生成したグリッドを格納
    string[] map_data_list_;    //ファイル出力用のstring型配列
    static readonly string path = "Assets/Resources/Output/output_file.txt";
    public Vector3 spaceScale;                      //スケール
    private static EditorWindow parent_window_;
    Dictionary<string, GameObject> obj_dict_ 
        = new Dictionary<string, GameObject>();    //生成用オブジェクトのディクショナリ

    public static void ShowWindow(EditorWindow parent_window)
    {
        MapCreateEditor window = EditorWindow.GetWindow<MapCreateEditor>("MapWindow");
        parent_window_ = parent_window;
        window.Init();
    }

    void Init()
    {
        //グリッド数を計算
        number_of_grid_ = ((ObjCreateEditor)parent_window_).CountOfGridX
            * ((ObjCreateEditor)parent_window_).CountOfGridY;
        //スケールを初期化
        spaceScale = new Vector3(((ObjCreateEditor)parent_window_).SelectedScale,0,((ObjCreateEditor)parent_window_).SelectedScale);
        //ファイルに出力するstringデータを格納する配列を初期化
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

        EditorGUILayout.Space();
        if (GUILayout.Button("ステージ生成"))
        {
            CreateObjectFromMapData();
        }
        EditorGUILayout.Space();
        if (GUILayout.Button("ファイル出力"))
        {
            OutputFile();
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
             * グリッドの列数を数え、指定した列数で改行
             */
            if (i != 0 && i % ((ObjCreateEditor)parent_window_).CountOfGridX == 0)
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
        //if (e.type == EventType.MouseDown)
        if (e.type == EventType.MouseDrag || e.type == EventType.MouseDown)
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
                        /*
                         * マップデータを格納している配列に選択している画像名を格納
                         */ 
                        if (((ObjCreateEditor)parent_window_).SelectedTex != null)
                        {
                            //Debug.Log("Rect:" + i + "をクリックしたよ！");
                            map_data_list_[i] = ((ObjCreateEditor)parent_window_).SelectedTex.name;
                        }
                    }
                }
            }
        }
    }

    private void OutputFile()
    {
        //※後で、ファイル名を指定できるように修正する
        StreamWriter sw = new StreamWriter(path,false);
        sw.WriteLine(GetMapStrFormat());
        sw.Flush();
        sw.Close();

        // 完了ポップアップ
        EditorUtility.DisplayDialog("MapCreater", "ファイルを出力しました。\n" + path, "ok");
    }

    //map_data_list_に格納している文字列をファイル出力用の配列に格納
    //ファイル出力のためにカンマや改行を追加していく
    //※後程検討：最初からファイル出力用のFMTにしておけばよいのでは？
    private string GetMapStrFormat()
    {
        string result = "";
        for (int i = 0; i < number_of_grid_; i++)
        {
            result += map_data_list_[i];
            //カンマを追記
            if ((i + 1 )% ((ObjCreateEditor)parent_window_).CountOfGridX != 0)
            {
                result += ",";
            }

            //改行
            if ((i + 1 )% ((ObjCreateEditor)parent_window_).CountOfGridX == 0)
            {
                result += "\n";
            }
        }
        return result;
    }

    /*
     * マップデータからオブジェクトをビューに生成
     * ※後でファイルから吸い出す処理と、生成する処理を分けること
     */ 
    private void CreateObjectFromMapData()
    {
        //ファイルに出力
        OutputFile();
        
        //テキストファイルからreaderにデータを取得
        TextAsset text_asset = new TextAsset();
        text_asset = Resources.Load("Output/output_file") as TextAsset;
        //string str_map_data = text_asset.text;
        var reader = new StringReader(text_asset.text);

        GameObject parent_empty_obj = new GameObject("ParentObj");  //生成したオブジェクトを格納しておく空オブジェクトを生成
        float poz_x = 0;                                            //オブジェクト生成の初期位置
        float poz_z = 0;                                            //オブジェクト生成の初期位置

        //readerから取得したデータをリストに格納する
        //一行ずつ順番実行：文字列データを吸い出し⇒オブジェクトを生成を行う
        while (reader.Peek() >  -1)
        {
            /*
             * 行を取り出し、カンマで区切られた文字列をリストに格納
             */
            //一行取り出し
            var line_data = reader.ReadLine();
            //取り出した行からカンマで区切られた文字列を１つずつ配列に格納
            var obj_str = line_data.Split(',');

            //x 座標を初期化
            poz_x = 0;    //オブジェクト生成の初期位置

            foreach (var s in obj_str)
            {
                if (s != "")
                {
                    Vector3 obj_poz = new Vector3(poz_x,0,poz_z);
                    if (!obj_dict_.ContainsKey(s))
                    {
                        obj_dict_[s] = Resources.Load("Prefab/" + s) as GameObject;
                    }
                    GameObject obj = Instantiate(obj_dict_[s], obj_poz, Quaternion.identity);
                    //まとめて管理用に空のオブジェクトに格納
                    obj.transform.parent = parent_empty_obj.transform;
                }
                //x座標をインクリメント
                poz_x += spaceScale.x;
            }
            //z座標をインクリメント（次の行の処理に移る準備）
            //マップエディター上での手前のグリッド・行はゲームビュー上のZ軸ではマイナス方向のため減算
            poz_z -= spaceScale.z;
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
