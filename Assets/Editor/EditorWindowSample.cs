using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class EditorWindowSample : EditorWindow {

    private const string ASSET_PATH = "Assets/Resources/ScriptableObjectSample.asset";

    [MenuItem("Editor/Sample01")]
    private static void Create()
    {
        // 生成
        GetWindow<EditorWindowSample>("サンプル");
    }

    /// <summary>
    /// ScriptableObjectSampleの変数
    /// </summary>
    private ScriptableObjectSample _sample;

    private void OnGUI()
    {
        if (_sample == null)
        {
            _sample = ScriptableObject.CreateInstance<ScriptableObjectSample>();
        }

        using (new GUILayout.HorizontalScope())
        {
            _sample.SampleIntValue = EditorGUILayout.IntField("サンプル", _sample.SampleIntValue);
        }

        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("書き込み"))
            {
                Export();
            }
            if (GUILayout.Button("読み込み"))
            {
            }
        }

        using (new EditorGUILayout.VerticalScope())
        {
            GUILayout.Button("ボタン１");
            GUILayout.Button("ボタン２");
        }

    }

    private void Export()
    {
        // 新規の場合は作成
        if (!AssetDatabase.Contains(_sample as UnityEngine.Object))
        {
            string directory = Path.GetDirectoryName(ASSET_PATH);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // アセット作成
            AssetDatabase.CreateAsset(_sample, ASSET_PATH);
        }
        // インスペクターから設定できないようにする
        _sample.hideFlags = HideFlags.NotEditable;
        // 更新通知
        EditorUtility.SetDirty(_sample);
        // 保存
        AssetDatabase.SaveAssets();
        // エディタを最新の状態にする
        AssetDatabase.Refresh();
    }

    private void Import()
    {
        ScriptableObjectSample sample = AssetDatabase.LoadAssetAtPath<ScriptableObjectSample>(ASSET_PATH);
        if (sample == null)
            return;

        _sample = sample;
    }
}
