using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct SpawnerSystem : ISystem
{
	public void OnCreate(ref SystemState state) { }

	public void OnDestroy(ref SystemState state) { }

	[BurstCompile]
	public void OnUpdate(ref SystemState state)
	{
		EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(state);

		// Creates a new instance of the job, assigns the necessary data, and schedules the job in parallel.
		new ProcessSpawnerJob
		{
			ElapsedTime = SystemAPI.Time.ElapsedTime,
			Ecb = ecb
		}.ScheduleParallel();
	}

	private EntityCommandBuffer.ParallelWriter GetEntityCommandBuffer(SystemState state)
	{
		var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
		var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
		return ecb.AsParallelWriter();
	}
}

