using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

struct SimpleJob : IJob
{
    public float a;
    public NativeArray<float> result;
    
    public void Execute()
    {
        result[0] = a;
    }
}

struct AnotherJob : IJob
{
    public NativeArray<float> result;
    
    public void Execute()
    {
        result[0] = result[0] + 1;
    }
}

public class JobsExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DoExample();
    }

    private void DoExample()
    {
        NativeArray<float> resultArray = new NativeArray<float>(1, Allocator.TempJob);
        
        // Instantiate and initialize
        SimpleJob firstJob = new SimpleJob
        {
            a = 5f,
            result = resultArray
        };

        AnotherJob secondJob = new AnotherJob
        {
            result = resultArray
        };

        // Schedule
        JobHandle firstHandle = firstJob.Schedule();
        JobHandle secondHandle = firstJob.Schedule(firstHandle);
        
        // other tasks to run in Main Thread in parallel
        
        // Confirm that jobs are completed
        firstHandle.Complete();
        secondHandle.Complete();

        float resultingValue = resultArray[0];
        Debug.LogError("Result = " + resultingValue);

        resultArray.Dispose();
    }
}
