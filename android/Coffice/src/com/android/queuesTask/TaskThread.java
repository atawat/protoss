package com.android.queuesTask;
/**
 * �����̣߳�
 * 
 * @author �ֶ�
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
                        Queues.queue.wait(); // ����Ϊ��ʱ��ʹ�̴߳��ڵȴ�״̬
                    }
                    catch (InterruptedException e)
                    {
                        e.printStackTrace();
                    }
                    System.out.println("wait...");
                }
                Queues.Task t = Queues.queue.remove(0); // �õ���һ��
                t.RunTask(); // ִ�и�����
                System.out.println("end");
            }
        }
    }

}
