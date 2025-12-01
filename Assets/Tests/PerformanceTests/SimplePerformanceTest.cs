using component;
using entity;
using System;
using System.Reflection;
using UnityEngine;

namespace Tests.PerformanceTests
{
    public class SimplePerformanceTest : MonoBehaviour
    {
        private Entity _testEntity;
        private ConstructorInfo _constructorInfo;
        private Type _componentType;
        
        private const int IterationCount = 100000;
        private const int WarmupCount = 1000;

        void Start()
        {
            RunPerformanceTest();
        }

        [ContextMenu("Run Performance Test")]
        public void RunPerformanceTest()
        {
            Debug.Log("Setting up performance test...");
            Setup();
            
            Debug.Log("Starting performance test...");
            
            // 预热
            Warmup();
            
            // 执行性能测试
            var directCreationTime = TestDirectCreation();
            var reflectionCreationTime = TestReflectionCreationWithConstructorInfo();
            var activatorCreationTime = TestActivatorCreation();
            
            // 输出结果
            PrintResults(directCreationTime, reflectionCreationTime, activatorCreationTime);
            
            Debug.Log("Performance test completed.");
        }

        private void Setup()
        {
            _testEntity = new Entity(1);
            _componentType = typeof(TestComponent);
            _constructorInfo = _componentType.GetConstructor(new[] { typeof(Entity) });
        }

        private void Warmup()
        {
            Debug.Log("Warming up...");
            for (int i = 0; i < WarmupCount; i++)
            {
                new TestComponent(_testEntity);
                _constructorInfo.Invoke(new object[] { _testEntity });
                Activator.CreateInstance(_componentType, _testEntity);
            }
            Debug.Log("Warmup completed.");
        }

        private long TestDirectCreation()
        {
            Debug.Log("Testing direct creation...");
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            for (int i = 0; i < IterationCount; i++)
            {
                var component = new TestComponent(_testEntity);
            }
            
            stopwatch.Stop();
            Debug.Log($"Direct creation test completed in {stopwatch.ElapsedMilliseconds} ms");
            return stopwatch.ElapsedMilliseconds;
        }

        private long TestReflectionCreationWithConstructorInfo()
        {
            Debug.Log("Testing reflection creation with ConstructorInfo...");
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            for (int i = 0; i < IterationCount; i++)
            {
                var component = (TestComponent)_constructorInfo.Invoke(new object[] { _testEntity });
            }
            
            stopwatch.Stop();
            Debug.Log($"Reflection creation with ConstructorInfo test completed in {stopwatch.ElapsedMilliseconds} ms");
            return stopwatch.ElapsedMilliseconds;
        }

        private long TestActivatorCreation()
        {
            Debug.Log("Testing Activator creation...");
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            for (int i = 0; i < IterationCount; i++)
            {
                var component = (TestComponent)Activator.CreateInstance(_componentType, _testEntity);
            }
            
            stopwatch.Stop();
            Debug.Log($"Activator creation test completed in {stopwatch.ElapsedMilliseconds} ms");
            return stopwatch.ElapsedMilliseconds;
        }

        private void PrintResults(long directCreationTime, long reflectionCreationTime, long activatorCreationTime)
        {
            Debug.Log("=== Performance Test Results ===");
            Debug.Log($"Iterations per test: {IterationCount}");
            Debug.Log($"Direct Creation: {directCreationTime} ms");
            Debug.Log($"Reflection (ConstructorInfo): {reflectionCreationTime} ms");
            Debug.Log($"Activator.CreateInstance: {activatorCreationTime} ms");
            Debug.Log("===============================");
            
            // 计算相对性能
            double reflectionVsDirect = (double)reflectionCreationTime / directCreationTime;
            double activatorVsDirect = (double)activatorCreationTime / directCreationTime;
            
            Debug.Log($"Performance comparison (relative to direct creation):");
            Debug.Log($"Reflection is {reflectionVsDirect:F2}x slower than direct creation");
            Debug.Log($"Activator is {activatorVsDirect:F2}x slower than direct creation");
        }
    }
}