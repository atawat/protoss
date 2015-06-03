package com.android.hal.printer;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.UnsupportedEncodingException;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.List;
import java.util.ResourceBundle.Control;

import android.content.Context;
import android.content.res.AssetManager;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Environment;
import android.util.Log;

import com.ctrl.gpio.Ioctl;

@SuppressWarnings("all")
public class JBInterface {

	public static final byte HT = 0x9; // 姘村钩鍒惰�??
	public static final byte LF = 0x0A; // 鎵撳嵃骞舵崲琛�
	public static final byte CR = 0x0D; // 鎵撳嵃鍥炶溅
	public static final byte ESC = 0x1B;
	public static final byte DLE = 0x10;
	public static final byte GS = 0x1D;
	public static final byte FS = 0x1C;
	public static final byte STX = 0x02;
	public static final byte US = 0x1F;
	public static final byte CAN = 0x18;
	public static final byte CLR = 0x0C;
	public static final byte EOT = 0x04;

	/* 榛樿棰滆壊瀛椾綋鎸囦护 */
	public static final byte[] ESC_FONT_COLOR_DEFAULT = new byte[] { ESC, 'r',
			0x00 };
	/* 鏍囧噯澶у�? */
	public static final byte[] FS_FONT_ALIGN = new byte[] { FS, 0x21, 1, ESC,
			0x21, 1 };
	/* 闈犲乏鎵撳嵃鍛戒�? */
	public static final byte[] ESC_ALIGN_LEFT = new byte[] { 0x1b, 'a', 0x00 };
	/* 灞呬腑鎵撳嵃鍛戒�? */
	public static final byte[] ESC_ALIGN_CENTER = new byte[] { 0x1b, 'a', 0x01 };
	/* 鍙栨秷�?�椾綋鍔犵矖 */
	public static final byte[] ESC_CANCEL_BOLD = new byte[] { ESC, 0x45, 0 };

	// 杩涚�?
	public static final byte[] ESC_ENTER = new byte[] { 0x1B, 0x4A, 0x40 };
	public static final byte[] ENTER = new byte[]{0x0D,0x0A};

	// 鑷�?
	public static final byte[] PRINTE_TEST = new byte[] { 0x1D, 0x28, 0x41 };
	public static final byte[] SET_RIGHT = new byte[]{0x1B, 0x61, 0x02};
	public static final byte[] SET_LEFT = new byte[]{0x1B,0x61,0x00};
	
	

	// 娴嬭瘯杈撳嚭Unicode Pirit Message
	public static final byte[] UNICODE_TEXT = new byte[] {0x00, 0x50, 0x00,
			0x72, 0x00, 0x69, 0x00, 0x6E, 0x00, 0x74, 0x00, 0x20, 0x00, 0x20,
			0x00, 0x20, 0x00, 0x4D, 0x00, 0x65, 0x00, 0x73, 0x00, 0x73, 0x00,
			0x61, 0x00, 0x67, 0x00, 0x65};
	public static final byte[] huidu = new byte[]{0x1B,0x6D,0x04};

	public static final DateFormat formatw = new SimpleDateFormat(
			"yyyy-MM-dd HH:mm:ss");
	/**print test 鎵撳嵃鏈鸿嚜妫�/
	 * 
	 */
	public static void printTest() {
		try {
//		print(ESC_ALIGN_CENTER);
		if (allowTowrite())
			C.printSerialPortTools.write(PRINTE_TEST);		
			//Thread.sleep(2000);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
//		ENTER(2);
		
		writeEnterLine(2);	
	}
	
	
	/** 打印文字GB2312 */
	public static void printText(String text) {
		Log.i("info", "text == "+text);
		PrintTools_58mm.printText(text);
	}
	
	
	public static boolean getState(){
		
		return C.printSerialPortTools.getState();
	}
	/** print text 鎵撳嵃鏂囧瓧 */
	public static void printText(String text,boolean test,String Persian) {
//		ENTER(4);
		writeEnterLine(2);
		print_Unicode(text,test,Persian);
//		ENTER(4);		
		writeEnterLine(2);
		
	}
	public static void setBold(){
		print(huidu);
	}
	public static void setRight(){
		print(SET_RIGHT);
	}
	public static void ENTER(int k){
		for(int i=0;i<k;i++){
		print(ENTER);
		}
	}
	/**
	 * print photo with path 鏍规嵁鍥剧墖璺緞鎵撳嵃鍥剧�?
	 * 
	 * @param 鍥剧墖鍦⊿D鍗¤矾寰勶紝濡�photo/pic.bmp
	 * */
	public static void printPhotoWithPath(String filePath) {

		String SDPath = Environment.getExternalStorageDirectory() + "/";
		String path = SDPath + filePath;

		// 鏍规嵁璺緞鑾峰彇鍥剧墖
		File mfile = new File(path);
		if (mfile.exists()) {// 鑻ヨ鏂囦欢瀛樺�?
			Bitmap bmp = BitmapFactory.decodeFile(path);
			byte[] command = decodeBitmap(bmp);
			printPhoto(command);
		}else{
			Log.e("PrintTools_58mm", "the file isn't exists");
		}
	}

	/**
	 * print photo in assets 鎵撳嵃assets閲岀殑鍥剧墖
	 * 
	 * @param 鍥剧墖鍦╝ssets鐩綍锛屽:pic.bmp
	 * */
	public static void printPhotoInAssets(Context context,String fileName) {

		AssetManager asm=context.getResources().getAssets();
		InputStream is;
		try {
			is = asm.open(fileName);
			Bitmap bmp = BitmapFactory.decodeStream(is);  
			is.close();  
			if(bmp!=null){
				byte[] command = decodeBitmap(bmp);
				printPhoto(command);
			}else{
				Log.e("PrintTools", "the file isn't exists");
			}
		} catch (IOException e) {
			e.printStackTrace();
			Log.e("PrintTools", "the file isn't exists");
		}
	}
	
	/**
	 * decode bitmap to bytes 瑙ｇ爜Bitmap涓轰綅鍥惧瓧鑺傛�?
	 * */
	public static byte[] decodeBitmap(Bitmap bmp){
		int bmpWidth = bmp.getWidth();
		int bmpHeight = bmp.getHeight();

		List<String> list = new ArrayList<String>(); //binaryString list
		StringBuffer sb;

		// 姣忚�?�楄妭鏁�闄や互8锛屼笉瓒宠�?0)
		int bitLen = bmpWidth / 8;
		int zeroCount = bmpWidth % 8;
		// 姣忚闇�琛ュ厖鐨�?
		String zeroStr = "";
		if (zeroCount > 0) {
			bitLen = bmpWidth / 8 + 1;
			for (int i = 0; i < (8 - zeroCount); i++) {
				zeroStr = zeroStr + "0";
			}
		}
		// 閫愪釜璇诲彇鍍忕礌棰滆壊锛屽皢闈炵櫧鑹叉敼涓洪粦鑹�
		for (int i = 0; i < bmpHeight; i++) {
			sb = new StringBuffer();
			for (int j = 0; j < bmpWidth; j++) {
				int color = bmp.getPixel(j, i); // 鑾峰緱Bitmap 鍥剧墖涓瘡涓�釜鐐圭殑color棰滆壊鍊�?
				//棰滆壊鍊肩殑R G B
				int r = (color >> 16) & 0xff;
				int g = (color >> 8) & 0xff;
				int b = color & 0xff;

				// if color close to white锛宐it='0', else bit='1'
				if (r > 160 && g > 160 && b > 160)
					sb.append("0");
				else
					sb.append("1");
			}
			// 姣忎竴琛�?粨鏉熸椂锛岃ˉ鍏呭墿浣欑殑0
			if (zeroCount > 0) {
				sb.append(zeroStr);
			}
			list.add(sb.toString());
		}
		// binaryStr姣�浣嶈皟鐢ㄤ竴娆¤浆鎹㈡柟娉曪紝鍐嶆嫾鍚�?
		List<String> bmpHexList = ConvertUtil.binaryListToHexStringList(list);
		String commandHexString = "1D763000";
		// 瀹藉害鎸囦护
		String widthHexString = Integer
				.toHexString(bmpWidth % 8 == 0 ? bmpWidth / 8
						: (bmpWidth / 8 + 1));
		if (widthHexString.length() > 2) {
			Log.e("decodeBitmap error", "瀹藉害瓒呭嚭 width is too large");
			return null;
		} else if (widthHexString.length() == 1) {
			widthHexString = "0" + widthHexString;
		}
		widthHexString = widthHexString + "00";

		// 楂樺害鎸囦护
		String heightHexString = Integer.toHexString(bmpHeight);
		if (heightHexString.length() > 2) {
			Log.e("decodeBitmap error", "楂樺害瓒呭嚭 height is too large");
			return null;
		} else if (heightHexString.length() == 1) {
			heightHexString = "0" + heightHexString;
		}
		heightHexString = heightHexString + "00";
		
		List<String> commandList = new ArrayList<String>();
		commandList.add(commandHexString+widthHexString+heightHexString);
		commandList.addAll(bmpHexList);
		
		return ConvertUtil.hexList2Byte(commandList);
	}
	
	/**
	 * print photo with bytes 鏍规嵁鎸囦护鎵撳嵃鍥剧墖
	 * */
	public static void printPhoto(byte[] bytes) {
		print(ESC_ALIGN_CENTER);
		writeEnterLine(1);
		print(bytes);
		writeEnterLine(3);
	}

	/**reset 閲嶇疆鏍煎紡*/
	public static void resetPrint() {

		print(ESC_FONT_COLOR_DEFAULT);
		print(FS_FONT_ALIGN);
		print(ESC_ALIGN_LEFT);
		print(ESC_CANCEL_BOLD);
		print(LF);
	}

	/**涓插彛鏄惁灏辩�?*/
	public static boolean allowTowrite() {
		return C.printSerialPortTools != null;
	}

	/**
	 * 杈撳�?
	 * @param String鍐呭�?
	 * */
	public static void print(String msg) {
		if (allowTowrite())
			C.printSerialPortTools.write(msg);
	}

	public static void print_unicode(String msg) {
		if (allowTowrite())
			C.printSerialPortTools.write_unicode(msg);
	}
	
	public static void print_Unicode(String msg,boolean test,String Persian) {
		if (allowTowrite())
			System.out.println("msg == "+msg);
			System.out.println("Persian == "+Persian);
			C.printSerialPortTools.write_Unicode(msg,Persian);
	}
	

	
	/**
	 * 杈撳�?
	 * @param  byte[]鎸囦�?
	 * */
	public static void print(byte[] b) {
		if (allowTowrite())
			C.printSerialPortTools.write(b);
		try {
			Thread.sleep(80);
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	/**
	 * 杈撳�?
	 * 
	 * @param int鎸囦�?
	 * */
	public static void print(int oneByte) {
		if (allowTowrite())
			C.printSerialPortTools.write(oneByte);
	}

	/**
	 * EnterLine 杩涚�?
	 * 
	 * @param 杩涚焊琛屾暟
	 * */
	public static void writeEnterLine(int count) {
		for (int i = 0; i < count; i++) {
			print(ESC_ENTER);
		}
	}
//	public static void setEnter(int count){
//		for(int )
//	}

	public static String getEnterLine(int count) {
		StringBuilder sBuilder = new StringBuilder();
		sBuilder.append(ESC_ENTER);
		return sBuilder.toString();
	}
	

	public static void initPrinter(){
		openPrinter();
		convertPrinterControl();
		setBold();
	}

	public static boolean openPrinter() {
//		int result = GpioControl.activate(GpioControl.printer_o, true);
		int result = Ioctl.activate(20, 1);
//		Log.i("info", "open printer == "+Ioctl.get_status(8192));
		if (result == 1)
			return true;
		else
			return false;
	}

	public static boolean closePrinter() {
		int result = Ioctl.activate(20, 0);
//		Log.i("info", "close printer == "+Ioctl.get_status(8192));
		C.printSerialPortTools.closeSerialPort();
		if (result == 0)
			return true;
		else
			return false;
	}

	public static boolean convertPrinterControl() {
		int result = Ioctl.convertPrinter();

		C.printSerialPortTools = new SerialPortTools(C.printPort_58mm,
				C.printBaudrate_58mm);
		if (result == 0)
			return true;
		else
			return false;
	}

	/** 自检 */
	public static void testPrinter() {
		JBInterface.printTest();
	}

	/*传入图片路径，打印二维码图片*/
	public static void printQRCodeWithPath(String qrcodeImagePath) {
		JBInterface.printPhotoWithPath(qrcodeImagePath);
	}
	
	/*传入图片路径，打印图�?*/
	public static void printImageWithPath(String iamgePath) {
		JBInterface.printPhotoWithPath(iamgePath);
	}
	
	/*传入Bitmap对象，打印二维码图片*/
	public static void printQRCode(Bitmap bitmap) {
		byte[] command = JBInterface.decodeBitmap(bitmap);
		JBInterface.printPhoto(command);
	}
	
	/*传入Bitmap对象，打印图�?*/
	public static void printImage(Bitmap bitmap) {
		byte[] command = JBInterface.decodeBitmap(bitmap);
		JBInterface.printPhoto(command);
	}
	
	/*传入Assets文件夹里面的图片文件名，打印二维码图�?*/
	public static void printQRCodeImageInAssets(Context context,String fileName){
		JBInterface.printPhotoInAssets(context, fileName);
	}
	
	/*传入Assets文件夹里面的图片文件名，打印图片*/
	public static void printImageInAssets(Context context,String fileName){
		JBInterface.printPhotoInAssets(context, fileName);
	}
	
	
	// 完整的判断中文汉字和符号
			public static boolean isChinese(String strName) {
				char[] ch = strName.toCharArray();
				for (int i = 0; i < ch.length; i++) {
					char c = ch[i];
					if (isChinese(c)) {
						return true;
					}
				}
				return false;
			}
			
			
			// 根据Unicode编码完美的判断中文汉字和符号
			private static boolean isChinese(char c) {
				Character.UnicodeBlock ub = Character.UnicodeBlock.of(c);
				if (ub == Character.UnicodeBlock.CJK_UNIFIED_IDEOGRAPHS
						|| ub == Character.UnicodeBlock.CJK_COMPATIBILITY_IDEOGRAPHS
						|| ub == Character.UnicodeBlock.CJK_UNIFIED_IDEOGRAPHS_EXTENSION_A
						|| ub == Character.UnicodeBlock.CJK_UNIFIED_IDEOGRAPHS_EXTENSION_B
						|| ub == Character.UnicodeBlock.CJK_SYMBOLS_AND_PUNCTUATION
						|| ub == Character.UnicodeBlock.HALFWIDTH_AND_FULLWIDTH_FORMS
						|| ub == Character.UnicodeBlock.GENERAL_PUNCTUATION) {
					return true;
				}
				return false;
			}
			
			  public static String stringToUnicode(String s) {
				  
		          String str = "";
		  
		          for (int i = 0; i < s.length(); i++) {		  
		              int ch = (int) s.charAt(i);		  
		              if (ch > 255)		 
		                  str +=  Integer.toHexString(ch);		 
		              else		  
		                  str +=  Integer.toHexString(ch);		  
		          }	  
		          Log.i("info", "str ==  "+str);
		          return str;
		  
		      }

	  public static byte uniteBytes(byte src0, byte src1) {  
		    byte _b0 = Byte.decode("0x" + new String(new byte[]{src0})).byteValue();  
		    _b0 = (byte)(_b0 << 4);  
		    byte _b1 = Byte.decode("0x" + new String(new byte[]{src1})).byteValue();  
		    byte ret = (byte)(_b0 ^ _b1);  
		    return ret;  
		    }   
		  
		      /** 
		       * 将指定字符串src，以每两个字符分割转换为16进制形式 
		       * 如："2B44EFD9" �?> byte[]{0x2B, 0×44, 0xEF, 0xD9} 
		       * @param src String 
		       * @return byte[] 
		       */  
		      public static byte[] HexString2Bytes(String src){  
		        byte[] ret = new byte[src.length()/2];  
		        byte[] tmp = src.getBytes();  
		        for(int i=0; i< tmp.length/2; i++){  
		          ret[i] = uniteBytes(tmp[i*2], tmp[i*2+1]);  
		        }  
		        return ret;  
		      }     
	public static byte[] getStringToHexBytes(String str){
		return HexString2Bytes(stringToUnicode(str));
	}
	
	//打印英文重做byte[]
	public static byte[] printerENByte(String msg){
		byte[] b = msg.getBytes();
		 byte[] writebytes = new byte[b.length *2];
			for(int i=0;i<b.length;i++){
				writebytes[i*2] = 0x00;
				writebytes[i*2+1] = msg.getBytes()[i];
			}
			return writebytes;
	}

}
