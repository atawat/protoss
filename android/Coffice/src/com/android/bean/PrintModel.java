package com.android.bean;

import java.util.ArrayList;

public class PrintModel {
	public int OrderCode;
	public String type;
	public String tel;
	public String creatTime;
	public ArrayList<OrderDetail> detailList;
	public int count;
	public double amount;

	public void setOrderCode(int orderCode) {
		this.OrderCode = orderCode;
	}

	public void setType(String type) {
		this.type = type;
	}

	public void setTel(String tel) {
		this.tel = tel;
	}

	public void setCreatTime(String creatTime) {
		this.creatTime = creatTime;
	}

	public void setDetailList(ArrayList<OrderDetail> detailList) {
		this.detailList = (ArrayList<OrderDetail>) detailList.clone();
	}

	public void setCount(int count) {
		this.count = count;
	}

	public void setAmount(double amount) {
		this.amount = amount;
	}
	
	public int getOrderCode() {
		return this.OrderCode ;
	}

	public String getType() {
		return this.type;
	}

	public String getTel() {
		return this.tel;
	}

	public String getCreatTime() {
		return this.creatTime ;
	}

	public ArrayList<OrderDetail> getDetailList() {
		return this.detailList ;
	}

	public int getCount() {
		return this.count;
	}

	public double getAmount() {
		return this.amount ;
	}
}
