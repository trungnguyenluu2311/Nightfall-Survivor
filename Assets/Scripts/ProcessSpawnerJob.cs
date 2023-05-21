using System;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

public partial struct ProcessSpawnerJob : IJobEntity
{
	public EntityCommandBuffer.ParallelWriter Ecb;
	public double ElapsedTime;

	// IJobEntity generates a component data query based on the parameters of its `Execute` method.
	// This example queries for all Spawner components and uses `ref` to specify that the operation
	// requires read and write access. Unity processes `Execute` for each entity that matches the
	// component data query.
	public void Execute([ChunkIndexInQuery] int chunkIndex, ref Spawner spawner)
	{
		// If the next spawn time has passed.
		if (spawner.NextSpawnTime < ElapsedTime)
		{
			// Spawns a new entity and positions it at the spawner.
			Entity newEntity = Ecb.Instantiate(chunkIndex, spawner.Prefab);
			Ecb.SetComponent(chunkIndex, newEntity, LocalTransform.FromPosition(spawner.SpawnPosition));

			// Resets the next spawn time.
			spawner.NextSpawnTime = (float)ElapsedTime + spawner.SpawnRate;
		}
	}
}