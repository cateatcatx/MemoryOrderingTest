# 简介
用来测试在Unity Mono环境下的多线程内存顺序问题
# 使用说明
![image](https://user-images.githubusercontent.com/2322181/153714613-26d04daf-516a-418f-84fc-d02be77282d7.png)
![image](https://user-images.githubusercontent.com/2322181/153714602-6ca0c1bf-cb03-43e7-bee4-084c9f7f3fcb.png)
输入：  
Enter TestPair：输入测试对儿的数量，测试时如果占用线程太少则无法复现内存乱序问题，增加测试对儿来增加线程数量。  
Enter round：每个测试对儿的最大测试回合数。  
Memory Barrier：是否开启内存屏障。  
输出：  
Memory Barrier：标识是否开启了内存屏障。  
Progress：测试进度（百分比）。  
Error：发生的内存乱序次数。  
# 测试结果
![image](https://user-images.githubusercontent.com/2322181/153716272-9b7a7f6c-e5bc-4cb3-8d08-d59953c0fee6.png)   
在arm架构的Android手机上，开启10个测试对儿每个测试对儿1000回合，关闭内存屏障的情况下发生了214次内存乱序错误。
如果开启内存屏障则不会发生内存乱序。
