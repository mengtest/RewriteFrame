﻿using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;
using UnityObject = UnityEngine.Object;

namespace LeyoutechEditor.Core.Util
{
    public static class SpriteAtlasUtil
    {
        /// <summary>
        /// 查找Atlas引用的所有的资源
        /// </summary>
        /// <param name="atlas"></param>
        /// <returns></returns>
        public static string[] GetDependAssets(SpriteAtlas atlas)
        {
            if (atlas == null) return null;

            List<string> spriteAssetPathList = new List<string>();
            UnityObject[] objs = atlas.GetPackables();
            foreach (var obj in objs)
            {
                if (obj.GetType() == typeof(Sprite))
                {
                    spriteAssetPathList.Add(AssetDatabase.GetAssetPath(obj));
                }
                else if (obj.GetType() == typeof(DefaultAsset))//如果是文件夹
                {
                    string folderPath = AssetDatabase.GetAssetPath(obj);
                    string[] assets = AssetDatabaseUtil.FindAssetInFolder<Sprite>(folderPath);
                    spriteAssetPathList.AddRange(assets);
                }
            }
            return spriteAssetPathList.Distinct().ToArray();
        }
    }
}
