package com.android.queuesTask;

/**
 * ����ָ����������У�
 * 
 * @author �ֶ�
 * 
 */
public class StartTaskThread
{
    //��������߳�ִ�ж����е������Ǿ����ȵ��ȵã��ȴ���
    public static void RunAllThread(int ThreadNumber){
      TaskThread thread = new TaskThread();
        for (int i = 0; i < ThreadNumber; i++) {
            Thread th=new Thread(thread); //��ʼִ��ʱ������Ϊ�գ����ڵȴ�״̬
            th.setName("�첽�����߳�"+i);
            th.start();
        }
    }
    
}
