package com.android.web;

public class DataPair {
	private String name;
	private String value;
	public void setName(String nameString){
		this.name=nameString;
	}
	public void setValue(String valueString){
		this.value=valueString;
	}
	public String getName(){
		return this.name;
	}
	public String getValue(){
		return this.value;
	}
	
}
