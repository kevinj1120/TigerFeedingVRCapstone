using System.Collections;
using System.Collections.Generic;
using GPUInstancer;
using UnityEngine;


public class GrassGenerator : MonoBehaviour
{
    public Camera camera;
    public Texture2D groundTexture;
    public Texture2D detailTexture;
    public GameObject spawnPosition;
    private int terrainSize = 128;
    private int terrainCounter;
    private Vector3 terrainShiftX;
    private Vector3 terrainShiftZ;
    private Terrain[] terrainArray;
    private bool isCurrentCameraFixed = true;
    private float[,,] alphaMap;

    #region Prototype settings

    // color settings
    private Color _healthyColor = new Color(0.263f, 0.976f, 0.165f, 1f); // default unity terrain prototype healthy color
    private Color _dryColor = new Color(0.804f, 0.737f, 0.102f, 1f); // default unity terrain prototype dry color
    private float _noiseSpread = 0.2f;
    private float _ambientOcclusion = 0.5f;
    private float _gradientPower = 0.5f;

    // wind settings
    private float _windIdleSway = 0.6f;
    private bool _windWavesOn = false;
    private float _windWaveTint = 0.5f;
    private float _windWaveSize = 0.5f;
    private float _windWaveSway = 0.5f;
    private Color _windWaveTintColor = new Color(160f / 255f, 82f / 255f, 45f / 255f, 1f); // sienna
        
    // mesh settings
    private bool _isBillboard = true;
    private bool _useCrossQuads = false;
    private int _crossQuadCount = 2;
    private float _crossQuadBillboardDistance = 50f;
    private Vector4 _scale = new Vector4(0.5f, 3.0f, 0.5f, 3.0f);
        
    // GPU Instancer settings
    private bool _isShadowCasting = false;
    private bool _isFrustumCulling = true;
    private float _frustumOffset = 0.2f;
    private float _maxDistance = 250f;

    // GPU Instancer terrain Settings
    private Vector2 _windVector = new Vector2(0.4f, 0.8f);

    // Start is called before the first frame update
    void Start()
    {
        terrainCounter = 0;
        terrainShiftX = new Vector3(terrainSize, 0, 0);
        terrainShiftZ = new Vector3(0, 0, -terrainSize);
        terrainArray = new Terrain[9];
        GPUInstancerAPI.SetCamera(camera);
        AddTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddTerrain()
    {
        //if (terrainCounter == 9)
        //    return;

        GenerateTerrain();
        AddInstancer(terrainArray[terrainCounter]);

        //terrainCounter++;
    }
    
    private void GenerateTerrain()
    {
        SplatPrototype[] splatPrototypes = new SplatPrototype[1];
        splatPrototypes[0] = new SplatPrototype();
        splatPrototypes[0].texture = groundTexture;

        DetailPrototype[] detailPrototypes = new DetailPrototype[1];
        detailPrototypes[0] = new DetailPrototype();
        detailPrototypes[0].prototypeTexture = detailTexture;
        detailPrototypes[0].renderMode = DetailRenderMode.GrassBillboard;

        Vector3 terrainPosition = spawnPosition.transform.position + (terrainShiftX * (terrainCounter % 3)) + (terrainShiftZ * (Mathf.FloorToInt(terrainCounter / 3f)));

        terrainArray[terrainCounter] = InitializeTerrainObject(terrainPosition, terrainSize, terrainSize / 2f, 16, 16, splatPrototypes, detailPrototypes);

        terrainArray[terrainCounter].transform.SetParent(transform);

        SetDetailMap(terrainArray[terrainCounter]);
    }
    
    private void AddInstancer(Terrain terrain)
    {
        GPUInstancerDetailManager detailManager = terrain.gameObject.AddComponent<GPUInstancerDetailManager>();
        GPUInstancerAPI.SetupManagerWithTerrain(detailManager, terrain);

        detailManager.terrainSettings.windVector = _windVector;
            
        // Can change prototype properties here
        if(detailManager.prototypeList.Count > 0)
        {
            GPUInstancerDetailPrototype detailPrototype = (GPUInstancerDetailPrototype)detailManager.prototypeList[0];

            detailPrototype.detailHealthyColor = _healthyColor;
            detailPrototype.detailDryColor = _dryColor;
            detailPrototype.noiseSpread = _noiseSpread;
            detailPrototype.ambientOcclusion = _ambientOcclusion;
            detailPrototype.gradientPower = _gradientPower;

            detailPrototype.windIdleSway = _windIdleSway;
            detailPrototype.windWavesOn = _windWavesOn;
            detailPrototype.windWaveTint = _windWaveTint;
            detailPrototype.windWaveSize = _windWaveSize;
            detailPrototype.windWaveSway = _windWaveSway;
            detailPrototype.windWaveTintColor = _windWaveTintColor;
                
            detailPrototype.isBillboard = _isBillboard;
            detailPrototype.useCrossQuads = _useCrossQuads;
            detailPrototype.quadCount = _crossQuadCount;
            detailPrototype.billboardDistance = _crossQuadBillboardDistance;
            detailPrototype.detailScale = _scale;

            detailPrototype.isShadowCasting = _isShadowCasting;
            detailPrototype.isFrustumCulling = _isFrustumCulling;
            detailPrototype.frustumOffset = _frustumOffset;
            detailPrototype.maxDistance = _maxDistance;


        }

        GPUInstancerAPI.InitializeGPUInstancer(detailManager);
    }
    
    private Terrain InitializeTerrainObject(Vector3 position, int terrainSize, float terrainHeight, int baseTextureResolution = 16, int detailResolutionPerPatch = 16, SplatPrototype[] splatPrototypes = null, DetailPrototype[] detailPrototypes = null)
    {
        GameObject terrainGameObject = new GameObject("GenratedTerrain");
        terrainGameObject.transform.position = position;

        Terrain terrain = terrainGameObject.AddComponent<Terrain>();
        TerrainCollider terrainCollider = terrainGameObject.AddComponent<TerrainCollider>();

#if UNITY_2019_2_OR_NEWER || UNITY_2018_4
        if (GPUInstancerConstants.gpuiSettings.isURP)
            terrain.materialTemplate = new Material(Shader.Find("Universal Render Pipeline/Terrain/Lit"));
        else if (GPUInstancerConstants.gpuiSettings.isHDRP)
            terrain.materialTemplate = new Material(Shader.Find("HDRP/TerrainLit"));
        else
            terrain.materialTemplate = new Material(Shader.Find("Nature/Terrain/Standard"));
#endif
        TerrainData terrainData = CreateTerrainData(terrainSize, terrainHeight, baseTextureResolution, detailResolutionPerPatch, splatPrototypes, detailPrototypes);

        terrainCollider.terrainData = terrainData;
        terrain.terrainData = terrainData;

#if UNITY_2019_2_OR_NEWER || UNITY_2018_4
        if (alphaMap == null)
        {
            alphaMap = new float[terrainSize, terrainSize, 1];
            for (int i = 0; i < terrainSize; i++)
            {
                for (int j = 0; j < terrainSize; j++)
                {
                    alphaMap[i, j, 0] = 1;
                }
            }
        }
        terrain.terrainData.SetAlphamaps(0, 0, alphaMap);
#endif

        return terrain;
    }
    
    private TerrainData CreateTerrainData(int terrainSize, float terrainHeight, int baseTextureResolution = 16, int detailResolutionPerPatch = 16, SplatPrototype[] splatPrototypes = null, DetailPrototype[] detailPrototypes = null)
        {
            TerrainData terrainData = new TerrainData();

            terrainData.heightmapResolution = terrainSize + 1;
            terrainData.baseMapResolution = baseTextureResolution; //16 is enough.
            terrainData.alphamapResolution = terrainSize;
            terrainData.SetDetailResolution(terrainSize, detailResolutionPerPatch);
#if UNITY_2018_3_OR_NEWER
            terrainData.terrainLayers = SplatPrototypesToTerrainLayers(splatPrototypes);
#else
            terrainData.splatPrototypes = splatPrototypes;
#endif
            terrainData.detailPrototypes = detailPrototypes;

            //terrain size must be set after setting terrain resolution.
            terrainData.size = new Vector3(terrainSize, terrainHeight, terrainSize);

            return terrainData;
        }

#if UNITY_2018_3_OR_NEWER
        private TerrainLayer[] SplatPrototypesToTerrainLayers(SplatPrototype[] splatPrototypes)
        {
            if (splatPrototypes == null)
                return null;
            TerrainLayer[] terrainLayers = new TerrainLayer[splatPrototypes.Length];
            for (int i = 0; i < splatPrototypes.Length; i++)
            {
                terrainLayers[i] = new TerrainLayer() { diffuseTexture = splatPrototypes[i].texture, normalMapTexture = splatPrototypes[i].normalMap };
            }
            return terrainLayers;
        }
#endif

        private void SetDetailMap(Terrain terrain)
        {
        int[,] detailMap = new int[terrain.terrainData.detailResolution, terrain.terrainData.detailResolution];
      
        for (int i = 0; i < terrain.terrainData.detailPrototypes.Length; i++)
            {
                for (int x = 0; x < terrain.terrainData.detailResolution; x++)
                {
                    for (int y = 0; y < terrain.terrainData.detailResolution; y++)
                    {
                        detailMap[x, y] = Random.Range(1,2);
                    }
                }
                terrain.terrainData.SetDetailLayer(0, 0, i, detailMap);
            }
            terrain.detailObjectDistance = 250;
        }

        #endregion Unity Terrain Generation
}
