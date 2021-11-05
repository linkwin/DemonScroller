using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RoadTile : Tile
{
    public Sprite[] m_Sprites;
    public Sprite m_Preview;
    public Sprite[] m_InnerCornerSprites;
    public Sprite[] m_RotationSprites0;
    public Sprite[] m_RotationSprites1;
    public Sprite[] m_RotationSprites2;
    public Sprite[] m_RotationSprites3;

    // This refreshes itself and other RoadTiles that are orthogonally and diagonally adjacent
    public override void RefreshTile(Vector3Int location, ITilemap tilemap)
    {
        for (int yd = -1; yd <= 1; yd++)
            for (int xd = -1; xd <= 1; xd++)
            {
                Vector3Int position = new Vector3Int(location.x + xd, location.y + yd, location.z);
                if (HasRoadTile(tilemap, position))
                    tilemap.RefreshTile(position);
            }
    }

    // This determines which sprite is used based on the RoadTiles that are adjacent to it and rotates it to fit the other tiles.
    // As the rotation is determined by the RoadTile, the TileFlags.OverrideTransform is set for the tile.
    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        int mask = HasRoadTile(tilemap, location + new Vector3Int(0, 1, 0)) ? 1 : 0;
        mask += HasRoadTile(tilemap, location + new Vector3Int(1, 0, 0)) ? 2 : 0;
        mask += HasRoadTile(tilemap, location + new Vector3Int(0, -1, 0)) ? 4 : 0;
        mask += HasRoadTile(tilemap, location + new Vector3Int(-1, 0, 0)) ? 8 : 0;

        // check diagonal tiles
        int mask2 = HasRoadTile(tilemap, location + new Vector3Int(1, 1, 0)) ? 1 : 0;
        mask2 += HasRoadTile(tilemap, location + new Vector3Int(-1, 1, 0)) ? 2 : 0;
        mask2 += HasRoadTile(tilemap, location + new Vector3Int(1, -1, 0)) ? 4 : 0;
        mask2 += HasRoadTile(tilemap, location + new Vector3Int(-1, -1, 0)) ? 8 : 0;

        int index = GetIndex((byte)mask);
        int index2 = GetRotationIndex((byte)mask2);

        if (index >= 0 && index < m_Sprites.Length)
        {
            tileData.sprite = m_Sprites[index];//set sprite
            tileData.sprite = GetSprite(GetRotationIndex((byte)mask), index);//override with proper side
            if (tileData.sprite == m_Sprites[0] && mask2 != 15 && index != 0)
                tileData.sprite = m_InnerCornerSprites[index2];//override with inner corner
                //tileData.sprite = m_RotationSprites[index][GetSprite((byte) mask)];
            tileData.color = Color.white;
            var m = tileData.transform;
            m.SetTRS(Vector3.zero, tileData.transform.rotation,  // GetRotation((byte)mask)
                 Vector3.one);
            tileData.transform = m;
            tileData.flags = TileFlags.LockTransform;
            tileData.colliderType = ColliderType.Sprite;
        }
        else
        {
            Debug.LogWarning("Not enough sprites in RoadTile instance");
        }
    }

    private Sprite GetSprite(int rotationIndex, int spriteIndex)
    {
        switch (spriteIndex)
        {
            case 0:
                return m_RotationSprites0[rotationIndex];
            case 1:
                return m_RotationSprites1[rotationIndex];
            case 2:
                return m_RotationSprites2[rotationIndex];
            case 3:
                return m_RotationSprites3[rotationIndex];
        }
        return m_Sprites[spriteIndex];
    }

    // This determines if the Tile at the position is the same RoadTile.
    private bool HasRoadTile(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }
    // The following determines which sprite to use based on the number of adjacent RoadTiles
    private int GetIndex(byte mask)
    {
        switch (mask)
        {
            case 0: return 0;
            case 3:
            case 6:
            case 9:
            case 12: return 1;
            case 1:
            case 2:
            case 4:
            case 5:
            case 10:
            case 8: return 2;
            case 7:
            case 11:
            case 13:
            case 14: return 3;
            case 15: return 4;
        }
        return -1;
    }
    // The following determines which rotation to use based on the positions of adjacent RoadTiles
    private int GetRotationIndex(byte mask)
    {
        switch (mask)
        {
            case 9:
            case 10:
            case 7:
            case 2:
            case 8:
                return 1;
            case 3:
            case 14:
                return 2;
            case 6:
            case 13:
                return 3;
        }

        return 0;

    }
    
    private Quaternion GetRotation(byte mask)
    {
        switch (mask)
        {
            case 9:
            case 10:
            case 7:
            case 2:
            case 8:
                return Quaternion.Euler(0f, 0f, -90f);
            case 3:
            case 14:
                return Quaternion.Euler(0f, 0f, -180f);
            case 6:
            case 13:
                return Quaternion.Euler(0f, 0f, -270f);
        }

        return Quaternion.Euler(0f, 0f, 0f);
    }
#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/RoadTile")]
    public static void CreateRoadTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Road Tile", "New Road Tile", "Asset", "Save Road Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<RoadTile>(), path);
    }
#endif
}