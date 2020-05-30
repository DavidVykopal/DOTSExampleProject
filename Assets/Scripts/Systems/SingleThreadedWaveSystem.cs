using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

// public class SingleThreadedWaveSystem : ComponentSystem
// {
//     protected override void OnUpdate()
//     {
//         Entities.ForEach((ref Translation trans, ref MoveSpeedData moveSpeedData, ref WaveData waveData) =>
//         {
//             float zPosition = waveData.Amplitude * math.sin((float) Time.ElapsedTime * moveSpeedData.Value
//                 + trans.Value.x * waveData.XOffset + trans.Value.y * waveData.YOffset);
//             trans.Value = new float3(trans.Value.x, trans.Value.y, zPosition);
//         });
//     }
// }