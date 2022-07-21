using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;

namespace SpaceGraphicsToolkit
{
	/// <summary>This component works like <b>SgtTerrainHeightmap</b>, but the heightmap will apply to one small area of the planet. This is useful if you want to add a specific feature to your planet at a specific location (e.g. a mountain).</summary>
	[ExecuteInEditMode]
	[RequireComponent(typeof(SgtTerrain))]
	[HelpURL(SgtHelper.HelpUrlPrefix + "SgtTerrainFeature")]
	[AddComponentMenu(SgtHelper.ComponentMenuPrefix + "Terrain Feature")]
	public class SgtTerrainFeature : MonoBehaviour
	{
		public enum ChannelType
		{
			Red,
			Green,
			Blue,
			Alpha
		}

		/// <summary>The heightmap texture used to displace the mesh.</summary>
		public Texture2D Heightmap { set { if (heightmap != value) { heightmap = value; PrepareTexture(); } } get { return heightmap; } } [SerializeField] private Texture2D heightmap;

		/// <summary>This allows you to choose which color channel from the heightmap texture will be used.
		/// NOTE: If your texture uses a 1 byte per channel format like Alpha8/R8, then this setting will be ignored.</summary>
		public ChannelType Channel { set { if (channel != value) { channel = value; MarkAsDirty(); } } get { return channel; } } [SerializeField] private ChannelType channel;

		/// <summary>This allows you to control the maximum height displacement applied to the terrain.</summary>
		public double Displacement { set { if (displacement != value) { displacement = value; MarkAsDirty(); } } get { return displacement; } } [SerializeField] private double displacement = 0.25;

		/// <summary>This allows you to specify the local direction on the planet where the feature should appear.</summary>
		public Vector3 Rotation { set { if (rotation != value) { rotation = value; MarkAsDirty(); } } get { return rotation; } } [SerializeField] private Vector3 rotation;

		/// <summary>This allows you to specify the local scale of the feature.</summary>
		public float Scale { set { if (scale != value) { scale = value; MarkAsDirty(); } } get { return scale; } } [SerializeField] private float scale = 1.0f;

		private SgtTerrain cachedTerrain;

		public void MarkAsDirty()
		{
			if (cachedTerrain != null)
			{
				cachedTerrain.MarkAsDirty();
			}
		}

		protected virtual void OnEnable()
		{
			cachedTerrain = GetComponent<SgtTerrain>();

			cachedTerrain.OnScheduleHeights         += HandleScheduleHeights;
			cachedTerrain.OnScheduleCombinedHeights += HandleScheduleHeights;

			PrepareTexture();
		}

		protected virtual void OnDisable()
		{
			cachedTerrain.OnScheduleHeights         -= HandleScheduleHeights;
			cachedTerrain.OnScheduleCombinedHeights -= HandleScheduleHeights;

			cachedTerrain.MarkAsDirty();
		}

		protected virtual void OnDidApplyAnimationProperties()
		{
			MarkAsDirty();
		}

		private void PrepareTexture()
		{
#if UNITY_EDITOR
			SgtHelper.MakeTextureReadable(heightmap);
#endif
			MarkAsDirty();
		}

		private void HandleScheduleHeights(NativeArray<double3> points, NativeArray<double> heights, ref JobHandle handle)
		{
			if (heightmap != null)
			{
				var job = new HeightsJob();

				job.Size         = new int2(heightmap.width, heightmap.height);
				job.Displacement = displacement;
				job.Stride       = SgtHelper.GetStride(heightmap.format);
				job.Offset       = SgtHelper.GetOffset(heightmap.format, (int)channel);
				job.Data         = heightmap.GetRawTextureData<byte>();
				job.Points       = points;
				job.Heights      = heights;
				job.Rotation     = float3x3.Euler(rotation) * float3x3.Scale(scale);

				handle = job.Schedule(heights.Length, 32, handle);
			}
		}

		[BurstCompile]
		public struct HeightsJob : IJobParallelFor
		{
			public double    Displacement;
			public int2      Size;
			public int       Stride;
			public int       Offset;
			public double3x3 Rotation;

			[ReadOnly] public NativeArray<byte> Data;

			[ReadOnly] public NativeArray<double3> Points;

			public NativeArray<double> Heights;

			public void Execute(int i)
			{
				if (double.IsNegativeInfinity(Heights[i]) == false)
				{
					var point = math.mul(Rotation, Points[i]);

					if (math.length(point.xy) < 0.1f)
					{
						Heights[i] += Displacement;
					}
					//Heights[i] += SgtTerrainTopology.Sample_Cubic(Data, Stride, Offset, Size, point.xy) * Displacement;
				}
			}
		}
	}
}

#if UNITY_EDITOR
namespace SpaceGraphicsToolkit
{
	using TARGET = SgtTerrainFeature;

	[UnityEditor.CanEditMultipleObjects]
	[UnityEditor.CustomEditor(typeof(TARGET))]
	public class SgtTerrainFeature_Editor : SgtEditor
	{
		protected override void OnInspector()
		{
			TARGET tgt; TARGET[] tgts; GetTargets(out tgt, out tgts);

			var markAsDirty = false;

			BeginError(Any(tgts, t => t.Heightmap == null));
				Draw("heightmap", ref markAsDirty, "The heightmap texture used to displace the mesh.\n\nNOTE: The height data should be stored in the alpha channel.\n\nNOTE: This should use the equirectangular cylindrical projection.");
			EndError();
			Draw("channel", ref markAsDirty, "This allows you to choose which color channel from the heightmap texture will be used.");
			BeginError(Any(tgts, t => t.Displacement == 0.0));
				Draw("displacement", ref markAsDirty, "This allows you to control the maximum height displacement applied to the terrain.");
			EndError();
			Draw("rotation", ref markAsDirty, "");
			Draw("scale", ref markAsDirty, "");

			if (markAsDirty == true)
			{
				Each(tgts, t => t.MarkAsDirty());
			}
		}
	}
}
#endif