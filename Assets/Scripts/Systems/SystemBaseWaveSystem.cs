using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class SystemBaseWaveSystem : SystemBase
{
    protected override void OnUpdate()
    {
        // Cannot access reference types in jobs, must cache this value before diving into jobs
        float elapsedTime = (float)Time.ElapsedTime;
        
        Entities.ForEach((ref Translation trans, in MoveSpeedData moveSpeedData, in WaveData waveData) =>
        {
            float zPosition = waveData.Amplitude * math.sin(elapsedTime * moveSpeedData.Value
                                                            + trans.Value.x * waveData.XOffset + trans.Value.y * waveData.YOffset);
            trans.Value = new float3(trans.Value.x, trans.Value.y, zPosition);
        }).ScheduleParallel();
    }
}