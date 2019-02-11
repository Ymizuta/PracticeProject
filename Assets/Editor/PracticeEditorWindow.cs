using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PracticeEditorWindow : EditorWindow {

    private Object obj;

    [UnityEditor.MenuItem("Editor/PracticeEdiotr")]
    private static void Create()
    {
        EditorWindow.GetWindow<PracticeEditorWindow>("さんぷる");
    }

    private void OnGUI()
    {
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.Label("登録データ:", GUILayout.Width(100));
            System.Type objType = typeof(Object);
            //パラメータの意味は…？（objtype⇒どのタイプを許容するか？）
            EditorGUILayout.ObjectField(obj, objType);
        }

        EditorGUILayout.Space();

        using (new GUILayout.VerticalScope())
        {
            if (GUILayout.Button("インポート")){
                Debug.Log("インポート！");
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("エクスポート"))
            {
                Debug.Log("エクスポート！");
            }
        }

        EditorGUILayout.Space();

        using (new GUILayout.VerticalScope())
        {
            float default_x = 0.0f;
            float x = 0.0f;
            float w = 50.0f;
            float maxw = 300.0f;
            float h = 50.0f;

            GUILayout.BeginHorizontal();
            for (int i=0; i<=10;i++)
            {
                x += w;
                if (x > maxw)
                {
                    x = default_x;
                    GUILayout.EndHorizontal();
                }
                if (x == default_x)
                {
                    GUILayout.BeginHorizontal();                    
                }
                GUILayout.Button("ボタン" + i, GUILayout.Width(w), GUILayout.Height(h));

                //using (new EditorGUILayout.HorizontalScope())
                //{
                //    GUILayout.Button("ボタン" + i, GUILayout.Width(w), GUILayout.Height(h));
                //}
                //ボタンの幅の合計ウィンドウの幅を超えていないかを検知
                //x += w;
                //if (w > maxw)
                //{
                //    EditorGUILayout.EndHorizontal();

                //}
                //超えていたら下の行からスタート
                //超えていないなら普通に描画
            }
            GUILayout.EndHorizontal();
        }
    }

}
