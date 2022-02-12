using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DefaultNamespace;

public class MemoryOrderingTest : MonoBehaviour
{
    public Button StartBtn;
    public InputField RoundField;
    public InputField PairCountField;
    public Text Text;
    public Toggle Toggle;

    private int memoryBarrier;
    private readonly List<TestPair> mTestPairs = new List<TestPair>();

    private void Start()
    {
        StartBtn.onClick.AddListener(OnClick);
    }

    private void Update()
    {
        int curRound = 0;
        int maxRound = 0;
        int error = 0;
        foreach (var testPair in mTestPairs)
        {
            curRound += testPair.curRound;
            maxRound += testPair.maxRound;
            error += testPair.error;
        }
        int progress = maxRound == 0 ? 0 : (int)((double)curRound / maxRound * 100);
        
        Text.text = $"Memory Barrier: {memoryBarrier}\nProgress: {progress}%\nError: {error}";
    }

    private void OnClick()
    {
        memoryBarrier = Toggle.isOn ? 1 : 0;
        
        mTestPairs.Clear();
        for (var i = 0; i < int.Parse(PairCountField.text); ++i)
        {
            var testPair = new TestPair();
            testPair.Start(memoryBarrier, int.Parse(RoundField.text));
            mTestPairs.Add(testPair);
        }
    }
}
