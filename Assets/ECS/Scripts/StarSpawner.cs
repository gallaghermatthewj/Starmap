using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using System.Collections.Generic;

namespace StarMap
{
    // ReSharper disable once InconsistentNaming
    [AddComponentMenu("DOTS Samples/SpawnFromMonoBehaviour/Spawner")]
    public class StarSpawner : MonoBehaviour
    {
        public GameObject starPrefab_A;
        public GameObject starPrefab_B;
        public GameObject starPrefab_F;
        public GameObject starPrefab_G;
        public GameObject starPrefab_K;
        public GameObject starPrefab_M;
        public GameObject starPrefab_O;

        public GameObject constellationRendererPrefab;

        public StarDataFileReader starDataFileReader;
        public List<StarValue> values;

        void Start()
        {
            values = starDataFileReader.readStarDataFile();

            // Create entity prefab from the game object hierarchy once
            var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);

            var prefabA = GameObjectConversionUtility.ConvertGameObjectHierarchy(starPrefab_A, settings);
            var prefabB = GameObjectConversionUtility.ConvertGameObjectHierarchy(starPrefab_B, settings);
            var prefabF = GameObjectConversionUtility.ConvertGameObjectHierarchy(starPrefab_F, settings);
            var prefabG = GameObjectConversionUtility.ConvertGameObjectHierarchy(starPrefab_G, settings);
            var prefabK = GameObjectConversionUtility.ConvertGameObjectHierarchy(starPrefab_K, settings);
            var prefabM = GameObjectConversionUtility.ConvertGameObjectHierarchy(starPrefab_M, settings);
            var prefabO = GameObjectConversionUtility.ConvertGameObjectHierarchy(starPrefab_O, settings);

            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            
            for (int i = 0; i < values.Count; i++)
            {
                // Efficiently instantiate a bunch of entities from the already converted entity prefab
                if (values[i].spect.Length > 0)
                {
                    switch (values[i].spect.Substring(0, 1))
                    {
                        case "A":
                            CreateEntityInstance(entityManager, values[i], entityManager.Instantiate(prefabA));
                            break;
                        case "B":
                            CreateEntityInstance(entityManager, values[i], entityManager.Instantiate(prefabB));
                            break;
                        case "F":
                            CreateEntityInstance(entityManager, values[i], entityManager.Instantiate(prefabF));
                            break;
                        case "G":
                            CreateEntityInstance(entityManager, values[i], entityManager.Instantiate(prefabG));
                            break;
                        case "K":
                            CreateEntityInstance(entityManager, values[i], entityManager.Instantiate(prefabK));
                            break;
                        case "M":
                            CreateEntityInstance(entityManager, values[i], entityManager.Instantiate(prefabM));
                            break;
                        case "O":
                            CreateEntityInstance(entityManager, values[i], entityManager.Instantiate(prefabO));
                            break;
                        default:
                            CreateEntityInstance(entityManager, values[i], entityManager.Instantiate(prefabA));
                            break;
                    }
                }

                
            }

            //if constellations
            if (starDataFileReader.starDisplaySettings.ShowConstellationLines)
            {
                float ds = starDataFileReader.starDisplaySettings.DistanceScalar;

                createSimpleLineRenderer(new Vector3(-17.298705f * ds, 4.33488f * ds, 33.191332f * ds), new Vector3(-13.103033f * ds, 3.398358f * ds, 20.360601f * ds), "54061", "53910");
                createSimpleLineRenderer(new Vector3(-17.298705f * ds, 4.33488f * ds, 33.191332f * ds), new Vector3(-13.402309f * ds, -0.903455f * ds, 20.710384f * ds), "54061", "59774");
                createSimpleLineRenderer(new Vector3(-13.103033f * ds, 3.398358f * ds, 20.360601f * ds), new Vector3(-15.094867f * ds, 0.406425f * ds, 20.552678f * ds), "53910", "58001");
                createSimpleLineRenderer(new Vector3(-13.402309f * ds, -0.903455f * ds, 20.710384f * ds), new Vector3(-15.094867f * ds, 0.406425f * ds, 20.552678f * ds), "59774", "58001");
                createSimpleLineRenderer(new Vector3(-13.402309f * ds, -0.903455f * ds, 20.710384f * ds), new Vector3(-13.775959f * ds, -3.309169f * ds, 20.972944f * ds), "59774", "62956");
                createSimpleLineRenderer(new Vector3(-13.775959f * ds, -3.309169f * ds, 20.972944f * ds), new Vector3(-14.115808f * ds, -5.413302f * ds, 21.531272f * ds), "62956", "65378");
                createSimpleLineRenderer(new Vector3(-14.115808f * ds, -5.413302f * ds, 21.531272f * ds), new Vector3(-18.529548f * ds, -9.394541f * ds, 24.164506f * ds), "65378", "67301");
                createSimpleLineRenderer(new Vector3(1.3431f * ds, 1.047629f * ds, 132.614909f * ds), new Vector3(-0.379972f * ds, -3.119005f * ds, 52.676703f * ds), "11767", "85822");
                createSimpleLineRenderer(new Vector3(-0.379972f * ds, -3.119005f * ds, 52.676703f * ds), new Vector3(-4.098102f * ds, -12.242736f * ds, 92.297935f * ds), "85822", "82080");
                createSimpleLineRenderer(new Vector3(-4.098102f * ds, -12.242736f * ds, 92.297935f * ds), new Vector3(-13.368655f * ds, -19.830791f * ds, 110.565025f * ds), "82080", "77055");
                createSimpleLineRenderer(new Vector3(-8.058164f * ds, -7.429707f * ds, 38.619407f * ds), new Vector3(-29.797327f * ds, -35.741213f * ds, 141.814461f * ds), "72607", "75097");
                createSimpleLineRenderer(new Vector3(-29.797327f * ds, -35.741213f * ds, 141.814461f * ds), new Vector3(-3.16421f * ds, -6.597182f * ds, 28.821068f * ds), "75097", "79822");
                createSimpleLineRenderer(new Vector3(-3.16421f * ds, -6.597182f * ds, 28.821068f * ds), new Vector3(-13.368655f * ds, -19.830791f * ds, 110.565025f * ds), "79822", "77055");
                createSimpleLineRenderer(new Vector3(-13.368655f * ds, -19.830791f * ds, 110.565025f * ds), new Vector3(-8.058164f * ds, -7.429707f * ds, 38.619407f * ds), "77055", "72607");
                createSimpleLineRenderer(new Vector3(51.601106f * ds, 256.709905f * ds, -37.740051f * ds), new Vector3(10.444137f * ds, 195.314937f * ds, -33.326707f * ds), "24436", "27366");
                createSimpleLineRenderer(new Vector3(62.775122f * ds, 602.66691f * ds, -12.712683f * ds), new Vector3(3.189296f * ds, 151.364387f * ds, 19.682142f * ds), "26311", "27989");
                createSimpleLineRenderer(new Vector3(11.658576f * ds, 76.036136f * ds, 8.56012f * ds), new Vector3(2.413721f * ds, 7.63677f * ds, 0.977904f * ds), "25336", "22449");
                createSimpleLineRenderer(new Vector3(2.413721f * ds, 7.63677f * ds, 0.977904f * ds), new Vector3(94.925539f * ds, 306.683598f * ds, 31.506969f * ds), "22449", "22549");
                createSimpleLineRenderer(new Vector3(3.189296f * ds, 151.364387f * ds, 19.682142f * ds), new Vector3(-0.487031f * ds, 46.83194f * ds, 7.961214f * ds), "27989", "28614");
                createSimpleLineRenderer(new Vector3(-5.054138f * ds, 152.917263f * ds, 40.334489f * ds), new Vector3(0.199145f * ds, 8.12391f * ds, 3.002187f * ds), "29038", "27913");
                createSimpleLineRenderer(new Vector3(-9.400594f * ds, 180.27811f * ds, 45.708737f * ds), new Vector3(-3.127264f * ds, 207.361101f * ds, 74.215924f * ds), "29426", "28691");
                createSimpleLineRenderer(new Vector3(-0.487031f * ds, 46.83194f * ds, 7.961214f * ds), new Vector3(-9.400594f * ds, 180.27811f * ds, 45.708737f * ds), "28614", "29426");
                createSimpleLineRenderer(new Vector3(-9.400594f * ds, 180.27811f * ds, 45.708737f * ds), new Vector3(-5.054138f * ds, 152.917263f * ds, 40.334489f * ds), "29426", "29038");
                createSimpleLineRenderer(new Vector3(-5.054138f * ds, 152.917263f * ds, 40.334489f * ds), new Vector3(-0.487031f * ds, 46.83194f * ds, 7.961214f * ds), "29038", "28614");
                createSimpleLineRenderer(new Vector3(94.925539f * ds, 306.683598f * ds, 31.506969f * ds), new Vector3(51.872397f * ds, 173.39311f * ds, 7.928048f * ds), "22549", "22730");
                createSimpleLineRenderer(new Vector3(51.872397f * ds, 173.39311f * ds, 7.928048f * ds), new Vector3(76.757549f * ds, 279.372806f * ds, 8.669789f * ds), "22730", "23123");
                createSimpleLineRenderer(new Vector3(2.413721f * ds, 7.63677f * ds, 0.977904f * ds), new Vector3(20.273136f * ds, 64.902011f * ds, 10.64789f * ds), "22449", "22509");
                createSimpleLineRenderer(new Vector3(20.273136f * ds, 64.902011f * ds, 10.64789f * ds), new Vector3(9.83882f * ds, 33.698243f * ds, 6.285324f * ds), "22509", "22845");
                createSimpleLineRenderer(new Vector3(3.189296f * ds, 151.364387f * ds, 19.682142f * ds), new Vector3(35.907504f * ds, 329.702779f * ds, 58.086365f * ds), "27989", "26207");
                createSimpleLineRenderer(new Vector3(35.907504f * ds, 329.702779f * ds, 58.086365f * ds), new Vector3(11.658576f * ds, 76.036136f * ds, 8.56012f * ds), "26207", "25336");
                createSimpleLineRenderer(new Vector3(11.658576f * ds, 76.036136f * ds, 8.56012f * ds), new Vector3(25.868117f * ds, 210.729667f * ds, -1.108306f * ds), "25336", "25930");
                createSimpleLineRenderer(new Vector3(10.444137f * ds, 195.314937f * ds, -33.326707f * ds), new Vector3(18.918488f * ds, 224.809409f * ds, -7.651875f * ds), "27366", "26727");
                createSimpleLineRenderer(new Vector3(18.918488f * ds, 224.809409f * ds, -7.651875f * ds), new Vector3(62.775122f * ds, 602.66691f * ds, -12.712683f * ds), "26727", "26311");
                createSimpleLineRenderer(new Vector3(62.775122f * ds, 602.66691f * ds, -12.712683f * ds), new Vector3(25.868117f * ds, 210.729667f * ds, -1.108306f * ds), "26311", "25930");
                createSimpleLineRenderer(new Vector3(25.868117f * ds, 210.729667f * ds, -1.108306f * ds), new Vector3(51.601106f * ds, 256.709905f * ds, -37.740051f * ds), "25930", "24436");
                createSimpleLineRenderer(new Vector3(14.082378f * ds, 5.53413f * ds, 26.457567f * ds), new Vector3(79.836717f * ds, 20.168018f * ds, 146.837047f * ds), "6686", "4427");
                createSimpleLineRenderer(new Vector3(79.836717f * ds, 20.168018f * ds, 146.837047f * ds), new Vector3(37.984817f * ds, 6.784483f * ds, 58.379619f * ds), "4427", "3179");
                createSimpleLineRenderer(new Vector3(37.984817f * ds, 6.784483f * ds, 58.379619f * ds), new Vector3(8.600014f * ds, 0.344589f * ds, 14.409503f * ds), "3179", "746");
                createSimpleLineRenderer(new Vector3(49.169644f * ds, 26.806887f * ds, 113.163436f * ds), new Vector3(14.082378f * ds, 5.53413f * ds, 26.457567f * ds), "8886", "6686");
                createSimpleLineRenderer(new Vector3(-35.773586f * ds, 4.486959f * ds, 95.570558f * ds), new Vector3(-51.400013f * ds, -7.563147f * ds, 141.115978f * ds), "56211", "61281");
                createSimpleLineRenderer(new Vector3(-51.400013f * ds, -7.563147f * ds, 141.115978f * ds), new Vector3(-34.416017f * ds, -20.758849f * ds, 83.796391f * ds), "61281", "68756");
                createSimpleLineRenderer(new Vector3(-34.416017f * ds, -20.758849f * ds, 83.796391f * ds), new Vector3(-10.015969f * ds, -12.471787f * ds, 26.585776f * ds), "68756", "75458");
                createSimpleLineRenderer(new Vector3(-10.015969f * ds, -12.471787f * ds, 26.585776f * ds), new Vector3(-5.406623f * ds, -9.545392f * ds, 17.947647f * ds), "75458", "78527");
                createSimpleLineRenderer(new Vector3(-5.406623f * ds, -9.545392f * ds, 17.947647f * ds), new Vector3(-5.477258f * ds, -12.300885f * ds, 24.814526f * ds), "78527", "80331");
                createSimpleLineRenderer(new Vector3(-5.477258f * ds, -12.300885f * ds, 24.814526f * ds), new Vector3(-9.178442f * ds, -40.388128f * ds, 91.793254f * ds), "80331", "83895");
                createSimpleLineRenderer(new Vector3(-9.178442f * ds, -40.388128f * ds, 91.793254f * ds), new Vector3(0.219434f * ds, -2.381656f * ds, 7.694536f * ds), "83895", "89937");
                createSimpleLineRenderer(new Vector3(0.219434f * ds, -2.381656f * ds, 7.694536f * ds), new Vector3(6.964788f * ds, -13.643765f * ds, 42.707954f * ds), "89937", "97433");
                createSimpleLineRenderer(new Vector3(6.964788f * ds, -13.643765f * ds, 42.707954f * ds), new Vector3(3.534238f * ds, -10.788301f * ds, 27.627312f * ds), "97433", "94376");
                createSimpleLineRenderer(new Vector3(3.534238f * ds, -10.788301f * ds, 27.627312f * ds), new Vector3(-0.532395f * ds, -18.850278f * ds, 28.897629f * ds), "94376", "87585");
                createSimpleLineRenderer(new Vector3(-0.532395f * ds, -18.850278f * ds, 28.897629f * ds), new Vector3(-0.436154f * ds, -29.451035f * ds, 37.014357f * ds), "87585", "87833");
                createSimpleLineRenderer(new Vector3(-0.436154f * ds, -29.451035f * ds, 37.014357f * ds), new Vector3(-9.169333f * ds, -70.678899f * ds, 92.218765f * ds), "87833", "85670");
                createSimpleLineRenderer(new Vector3(-9.169333f * ds, -70.678899f * ds, 92.218765f * ds), new Vector3(-2.101791f * ds, -17.284211f * ds, 25.026701f * ds), "85670", "85829");
                createSimpleLineRenderer(new Vector3(-2.101791f * ds, -17.284211f * ds, 25.026701f * ds), new Vector3(-0.532395f * ds, -18.850278f * ds, 28.897629f * ds), "85829", "87585");
                createSimpleLineRenderer(new Vector3(-16.048583f * ds, -138.749793f * ds, -105.649898f * ds), new Vector3(-8.785101f * ds, -114.747994f * ds, -93.292778f * ds), "85927", "86670");
                createSimpleLineRenderer(new Vector3(-8.785101f * ds, -114.747994f * ds, -93.292778f * ds), new Vector3(-24.497404f * ds, -451.772701f * ds, -381.351324f * ds), "86670", "87073");
                createSimpleLineRenderer(new Vector3(-24.497404f * ds, -451.772701f * ds, -381.351324f * ds), new Vector3(-6.654055f * ds, -67.016643f * ds, -62.796539f * ds), "87073", "86228");
                createSimpleLineRenderer(new Vector3(-6.654055f * ds, -67.016643f * ds, -62.796539f * ds), new Vector3(-3.401345f * ds, -16.05484f * ds, -15.432267f * ds), "86228", "84143");
                createSimpleLineRenderer(new Vector3(-3.401345f * ds, -16.05484f * ds, -15.432267f * ds), new Vector3(-8.440449f * ds, -28.763155f * ds, -27.33475f * ds), "84143", "82729");
                createSimpleLineRenderer(new Vector3(-8.440449f * ds, -28.763155f * ds, -27.33475f * ds), new Vector3(-35.43289f * ds, -115.66204f * ds, -94.671563f * ds), "82729", "82514");
                createSimpleLineRenderer(new Vector3(-35.43289f * ds, -115.66204f * ds, -94.671563f * ds), new Vector3(-4.842163f * ds, -15.39568f * ds, -11.006616f * ds), "82514", "82396");
                createSimpleLineRenderer(new Vector3(-4.842163f * ds, -15.39568f * ds, -11.006616f * ds), new Vector3(-45.960032f * ds, -119.546841f * ds, -68.7205f * ds), "82396", "81266");
                createSimpleLineRenderer(new Vector3(-45.960032f * ds, -119.546841f * ds, -68.7205f * ds), new Vector3(-58.542602f * ds, -140.307591f * ds, -75.574766f * ds), "81266", "80763");
                createSimpleLineRenderer(new Vector3(-58.542602f * ds, -140.307591f * ds, -75.574766f * ds), new Vector3(-55.881427f * ds, -102.320756f * ds, -41.98604f * ds), "80763", "78820");
                createSimpleLineRenderer(new Vector3(-58.542602f * ds, -140.307591f * ds, -75.574766f * ds), new Vector3(-69.332609f * ds, -120.492107f * ds, -57.928452f * ds), "80763", "78401");
                createSimpleLineRenderer(new Vector3(-58.542602f * ds, -140.307591f * ds, -75.574766f * ds), new Vector3(-81.301517f * ds, -139.203202f * ds, -79.023364f * ds), "80763", "78265");
                createSimpleLineRenderer(new Vector3(-3.444569f * ds, 17.203451f * ds, 4.0169f * ds), new Vector3(-9.909037f * ds, 27.946222f * ds, 8.805756f * ds), "32362", "35350");
                createSimpleLineRenderer(new Vector3(-9.909037f * ds, 27.946222f * ds, 8.805756f * ds), new Vector3(-5.888392f * ds, 16.151258f * ds, 6.939513f * ds), "35350", "35550");
                createSimpleLineRenderer(new Vector3(-5.888392f * ds, 16.151258f * ds, 6.939513f * ds), new Vector3(-109.067807f * ds, 379.683873f * ds, 148.25163f * ds), "35550", "34088");
                createSimpleLineRenderer(new Vector3(-109.067807f * ds, 379.683873f * ds, 148.25163f * ds), new Vector3(-5.266162f * ds, 31.714389f * ds, 9.461403f * ds), "34088", "31681");
                createSimpleLineRenderer(new Vector3(-5.888392f * ds, 16.151258f * ds, 6.939513f * ds), new Vector3(-30.104979f * ds, 67.678521f * ds, 37.572052f * ds), "35550", "36962");
                createSimpleLineRenderer(new Vector3(-30.104979f * ds, 67.678521f * ds, 37.572052f * ds), new Vector3(-17.374205f * ds, 35.446541f * ds, 17.905243f * ds), "36962", "37740");
                createSimpleLineRenderer(new Vector3(-30.104979f * ds, 67.678521f * ds, 37.572052f * ds), new Vector3(-4.055465f * ds, 8.19518f * ds, 4.867171f * ds), "36962", "37826");
                createSimpleLineRenderer(new Vector3(-4.055465f * ds, 8.19518f * ds, 4.867171f * ds), new Vector3(-11.927106f * ds, 30.38497f * ds, 17.208794f * ds), "37826", "36046");
                createSimpleLineRenderer(new Vector3(-11.927106f * ds, 30.38497f * ds, 17.208794f * ds), new Vector3(-25.971151f * ds, 80.964075f * ds, 49.576976f * ds), "36046", "34693");
                createSimpleLineRenderer(new Vector3(-25.971151f * ds, 80.964075f * ds, 49.576976f * ds), new Vector3(-5.311938f * ds, 12.130011f * ds, 8.238737f * ds), "34693", "36850");
                createSimpleLineRenderer(new Vector3(-25.971151f * ds, 80.964075f * ds, 49.576976f * ds), new Vector3(-10.97736f * ds, 46.812348f * ds, 32.384569f * ds), "34693", "33018");
                createSimpleLineRenderer(new Vector3(-25.971151f * ds, 80.964075f * ds, 49.576976f * ds), new Vector3(-44.684834f * ds, 230.247724f * ds, 110.023705f * ds), "34693", "32246");
                createSimpleLineRenderer(new Vector3(-44.684834f * ds, 230.247724f * ds, 110.023705f * ds), new Vector3(-19.745914f * ds, 155.415378f * ds, 57.67907f * ds), "32246", "30883");
                createSimpleLineRenderer(new Vector3(-44.684834f * ds, 230.247724f * ds, 110.023705f * ds), new Vector3(-6.562116f * ds, 65.281443f * ds, 27.194957f * ds), "32246", "30343");
                createSimpleLineRenderer(new Vector3(-6.562116f * ds, 65.281443f * ds, 27.194957f * ds), new Vector3(-7.067151f * ds, 108.713368f * ds, 45.140745f * ds), "30343", "29655");
                createSimpleLineRenderer(new Vector3(-7.067151f * ds, 108.713368f * ds, 45.140745f * ds), new Vector3(-0.785331f * ds, 43.678266f * ds, 18.780748f * ds), "29655", "28734");
                createSimpleLineRenderer(new Vector3(12.391949f * ds, 126.637551f * ds, 49.207351f * ds), new Vector3(7.02722f * ds, 18.287604f * ds, 5.806661f * ds), "26451", "21421");
                createSimpleLineRenderer(new Vector3(7.02722f * ds, 18.287604f * ds, 5.806661f * ds), new Vector3(17.209733f * ds, 40.871602f * ds, 12.608233f * ds), "21421", "20894");
                createSimpleLineRenderer(new Vector3(17.209733f * ds, 40.871602f * ds, 12.608233f * ds), new Vector3(20.197375f * ds, 43.211662f * ds, 13.342572f * ds), "20894", "20205");
                createSimpleLineRenderer(new Vector3(20.197375f * ds, 43.211662f * ds, 13.342572f * ds), new Vector3(18.695965f * ds, 41.472036f * ds, 14.380508f * ds), "20205", "20455");
                createSimpleLineRenderer(new Vector3(18.695965f * ds, 41.472036f * ds, 14.380508f * ds), new Vector3(17.36483f * ds, 39.694452f * ds, 14.017394f * ds), "20455", "20648");
                createSimpleLineRenderer(new Vector3(17.36483f * ds, 39.694452f * ds, 14.017394f * ds), new Vector3(16.488448f * ds, 39.136798f * ds, 14.772771f * ds), "20648", "20889");
                createSimpleLineRenderer(new Vector3(16.488448f * ds, 39.136798f * ds, 14.772771f * ds), new Vector3(37.416474f * ds, 106.020957f * ds, 47.623853f * ds), "20889", "21881");
                createSimpleLineRenderer(new Vector3(37.416474f * ds, 106.020957f * ds, 47.623853f * ds), new Vector3(5.281578f * ds, 35.6504f * ds, 19.655456f * ds), "21881", "25428");
                createSimpleLineRenderer(new Vector3(18.695965f * ds, 41.472036f * ds, 14.380508f * ds), new Vector3(61.664507f * ds, 94.489136f * ds, 50.483636f * ds), "20455", "17702");
                createSimpleLineRenderer(new Vector3(20.197375f * ds, 43.211662f * ds, 13.342572f * ds), new Vector3(72.055648f * ds, 125.663969f * ds, 32.088342f * ds), "20205", "18724");
                createSimpleLineRenderer(new Vector3(72.055648f * ds, 125.663969f * ds, 32.088342f * ds), new Vector3(55.200207f * ds, 68.663325f * ds, 13.999272f * ds), "18724", "15900");
                createSimpleLineRenderer(new Vector3(7.02722f * ds, 18.287604f * ds, 5.806661f * ds), new Vector3(16.488448f * ds, 39.136798f * ds, 14.772771f * ds), "21421", "20889");
                createSimpleLineRenderer(new Vector3(118.013398f * ds, 39.343134f * ds, 56.911417f * ds), new Vector3(78.942697f * ds, 28.524892f * ds, 43.257068f * ds), "5742", "6193");
                createSimpleLineRenderer(new Vector3(78.942697f * ds, 28.524892f * ds, 43.257068f * ds), new Vector3(108.793748f * ds, 30.58991f * ds, 70.082353f * ds), "6193", "4889");
                createSimpleLineRenderer(new Vector3(108.793748f * ds, 30.58991f * ds, 70.082353f * ds), new Vector3(118.013398f * ds, 39.343134f * ds, 56.911417f * ds), "4889", "5742");
                createSimpleLineRenderer(new Vector3(118.013398f * ds, 39.343134f * ds, 56.911417f * ds), new Vector3(95.233975f * ds, 40.171366f * ds, 28.364911f * ds), "5742", "7097");
                createSimpleLineRenderer(new Vector3(95.233975f * ds, 40.171366f * ds, 28.364911f * ds), new Vector3(75.808974f * ds, 37.546943f * ds, 13.637807f * ds), "7097", "8198");
                createSimpleLineRenderer(new Vector3(75.808974f * ds, 37.546943f * ds, 13.637807f * ds), new Vector3(39.728779f * ds, 23.413015f * ds, 2.226137f * ds), "8198", "9487");
                createSimpleLineRenderer(new Vector3(39.728779f * ds, 23.413015f * ds, 2.226137f * ds), new Vector3(48.236339f * ds, 26.069281f * ds, 3.053521f * ds), "9487", "8833");
                createSimpleLineRenderer(new Vector3(48.236339f * ds, 26.069281f * ds, 3.053521f * ds), new Vector3(100.168039f * ds, 47.473101f * ds, 10.649285f * ds), "8833", "7884");
                createSimpleLineRenderer(new Vector3(100.168039f * ds, 47.473101f * ds, 10.649285f * ds), new Vector3(85.579481f * ds, 35.529252f * ds, 9.974354f * ds), "7884", "7007");
                createSimpleLineRenderer(new Vector3(85.579481f * ds, 35.529252f * ds, 9.974354f * ds), new Vector3(53.144471f * ds, 14.974142f * ds, 7.651854f * ds), "7007", "4906");
                createSimpleLineRenderer(new Vector3(53.144471f * ds, 14.974142f * ds, 7.651854f * ds), new Vector3(172.283739f * ds, 36.848083f * ds, 22.568981f * ds), "4906", "3760");
                createSimpleLineRenderer(new Vector3(172.283739f * ds, 36.848083f * ds, 22.568981f * ds), new Vector3(123.225843f * ds, 11.104763f * ds, 17.807629f * ds), "3760", "1645");
                createSimpleLineRenderer(new Vector3(123.225843f * ds, 11.104763f * ds, 17.807629f * ds), new Vector3(31.760437f * ds, -0.095415f * ds, 3.822824f * ds), "1645", "118268");
                createSimpleLineRenderer(new Vector3(31.760437f * ds, -0.095415f * ds, 3.822824f * ds), new Vector3(13.595494f * ds, -1.192399f * ds, 1.34449f * ds), "118268", "116771");
                createSimpleLineRenderer(new Vector3(13.595494f * ds, -1.192399f * ds, 1.34449f * ds), new Vector3(44.814124f * ds, -6.304546f * ds, 5.059407f * ds), "116771", "115830");
                createSimpleLineRenderer(new Vector3(44.814124f * ds, -6.304546f * ds, 5.059407f * ds), new Vector3(41.496215f * ds, -7.847237f * ds, 2.421966f * ds), "115830", "114971");
                createSimpleLineRenderer(new Vector3(41.496215f * ds, -7.847237f * ds, 2.421966f * ds), new Vector3(46.55861f * ds, -6.764661f * ds, 1.031187f * ds), "114971", "115738");
                createSimpleLineRenderer(new Vector3(46.55861f * ds, -6.764661f * ds, 1.031187f * ds), new Vector3(32.57453f * ds, -2.556983f * ds, 1.015452f * ds), "115738", "116928");
                createSimpleLineRenderer(new Vector3(32.57453f * ds, -2.556983f * ds, 1.015452f * ds), new Vector3(13.595494f * ds, -1.192399f * ds, 1.34449f * ds), "116928", "116771");









            }
        }

        void createSimpleLineRenderer(Vector3 point1, Vector3 point2)
        {
            GameObject constellationInstance = GameObject.Instantiate(constellationRendererPrefab);
            LineRenderer constellationLineRenderer = constellationInstance.GetComponent<LineRenderer>();
            constellationLineRenderer.SetPosition(0, point1);
            constellationLineRenderer.SetPosition(1, point2);
        }
        void createSimpleLineRenderer(Vector3 point1, Vector3 point2, string starInd1, string starInd2)
        {
            GameObject constellationInstance = GameObject.Instantiate(constellationRendererPrefab);
            LineRenderer constellationLineRenderer = constellationInstance.GetComponent<LineRenderer>();
            constellationLineRenderer.SetPosition(0, point1);
            constellationLineRenderer.SetPosition(1, point2);
            constellationInstance.name = starInd1 + "_" + starInd2;
        }

        private void CreateEntityInstance(EntityManager entityManager, StarValue starValue, Entity instance)
        {
            // Place the instantiated entity in a grid with some noise
            var position = transform.TransformPoint(new Vector3(starValue.x * starDataFileReader.starDisplaySettings.DistanceScalar , starValue.y * starDataFileReader.starDisplaySettings.DistanceScalar, starValue.z * starDataFileReader.starDisplaySettings.DistanceScalar));
            entityManager.SetComponentData(instance, new Translation { Value = position });
            entityManager.AddComponents(instance, new ComponentTypes(
            new ComponentType[]
            {
                typeof(NonUniformScale),
            }));
            float magnitude = starValue.lum;
            if (!starDataFileReader.starDisplaySettings.ScaleByAbsoluteMagnitude)
            {
                magnitude = starValue.absmag;
            }
            float sc = Mathf.Abs(magnitude > 0 ? magnitude * 2.512f : 1 / (magnitude * 2.512f)) * starDataFileReader.starDisplaySettings.SizeScalar;
            entityManager.SetComponentData(instance, new NonUniformScale { Value = sc});
        }
        private void OnDisable()
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            entityManager.DestroyAndResetAllEntities();
        }
    }
}