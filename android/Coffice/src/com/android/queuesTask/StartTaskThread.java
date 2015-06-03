package com.android.queuesTask;

/**
 * 启动指定的任务队列；
 * 
 * @author 贾豆
 * 
 */
public class StartTaskThread
{
    //开启多个线程执行队列中的任务，那就是先到先得，先处理；
    public static void RunAllThread(int ThreadNumber){
      TaskThread thread = new TaskThread();
        for (int i = 0; i < ThreadNumber; i++) {
            Thread th=new Thread(thread); //开始执行时，队列为空，处于等待状态
            th.setName("异步队列线程"+i);
            th.start();
        }
    }
    
}
