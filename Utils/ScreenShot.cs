using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScreenShot : MonoBehaviour
{
    enum Mode {
        SINGLE,
        ARRAY
    }

    Camera camera;

    [SerializeField] string path = "Assets/Resources/Items/Icons/";
    [SerializeField] string name;
    [SerializeField] Mode mode;
    [SerializeField] GameObject[] Array;


    [ContextMenu("Take ScreenShot")]
    void Take()
    {
        if(mode == Mode.SINGLE)
            SS();
        if(mode == Mode.ARRAY)
        {
            int i = 0;

            foreach(var obj in Array)
            {
                i++;
                name = "obj_"+i;
                var o = Instantiate(obj, Vector3.zero, Quaternion.identity);
                SS();
                DestroyImmediate(o);
            }
        }
    }


    void SS()
    {
        if(camera == null)
        {
            camera = GetComponent<Camera>();
        }

        camera.clearFlags = CameraClearFlags.Depth;

        RenderTexture rt = new RenderTexture(256, 256, 24);
        camera.targetTexture = rt;
        Texture2D sc = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        camera.Render();
        RenderTexture.active = rt;
        sc.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null;

        if(Application.isEditor)
        {
            DestroyImmediate(rt);
        }
        else
        {
            Destroy(rt);
        }

        byte[] bytes = sc.EncodeToPNG();
        System.IO.File.WriteAllBytes(path + name +".png", bytes);
        #if UNITY_EDITOR
        AssetDatabase.Refresh();
        #endif
    }
}
