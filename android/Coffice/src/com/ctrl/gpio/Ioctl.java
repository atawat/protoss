package com.ctrl.gpio;

import android.util.Log;

public class Ioctl {
//	eDISP_PWR	= 11,
//	eFINGER_PWR=12,
//	eUSBKEY_PWR=13,
//	eMAGCARD_PWR=14,
//	eLDTONG_PWR=15,
//	eQX_PWR=16,
//	eRFID_PWR=17,
//	eSCAN_PWR=18,
//	eSCAN_TRIG=19,
//	ePRINT_PWR=20,
//	eBEEP=21,
//	eLAISER=22,
//	eFLASH=23,
//* new port   ttyS1, RFID, 1D, 2D, MSR, ID, 
//* 			  ttyS2: RS2322
//			  ttyS3, FINGER, PRINTER, LED, IDREADER,   
	
	 static  
	    {  
		 try{
	        System.loadLibrary("ctrl_gpio");  
	    }catch(UnsatisfiedLinkError ule){
	    	System.err.println("WARNING: Could not load library!"); 
	    	Log.i("info", "error ===  "+ule.getMessage().toString());
	    }
	    }
	 //鑾峰彇鍚勬ā鍧楃姸鎬佸嚱鏁�
	    public native static int convertRfid();
	    public native static int convertScanner();
	    public native static int convertLed();
	    public native static int convertMagcard();
	    public native static int convertFinger();
	    public native static int convertPrinter();
	    public native static int convertIdReader();
	    public native static int convertPSAM();
	    public native static int convertRS232_1();
	    public native static int convertRS232_2();
	    
	//鍚勬ā鍧椾笂鐢靛嚱鏁�
	    public native static int activate(int type,int open);//鍙傛暟锛氫笂鐢垫垨涓嬬數IO鍙�
	    public native static int get_status(int type);//鏀诲彇IO鐘舵��(0涓轰笅鐢碉紝1涓轰笂鐢碉紝璐熷�间负涓嶆甯�)
	    
}
