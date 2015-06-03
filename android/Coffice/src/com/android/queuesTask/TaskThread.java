package com.android.queuesTask;
/**
 * 任务线程；
 * 
 * @author 贾豆
 * 
 */
public class TaskThread extends Thread
{
    @Override
    public void run()
    {
        while (true)
        {
            synchronized (Queues.queue)
            {
                while (Queues.queue.isEmpty())
                { //
                    try
                    {
                        Queues.queue.wait(); // 队列为空时，使线程处于等待状态
                    }
                    catch (InterruptedException e)
                    {
                        e.printStackTrace();
                    }
                    System.out.println("wait...");
                }
                Queues.Task t = Queues.queue.remove(0); // 得到第一个
                t.RunTask(); // 执行该任务
                System.out.println("end");
            }
        }
    }

}
