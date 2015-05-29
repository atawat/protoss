using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Protoss.Entity.Model;

namespace Protoss.Models
{
	public class ProductDetailModel
	{

		/// <summary>
        /// Id
        /// </summary>
		public int Id {get;set;}


		/// <summary>
        /// 详细
        /// </summary>
		public string Detail {get;set;}


		/// <summary>
        /// 图片链接1
        /// </summary>
		public string ImgUrl1 {get;set;}


		/// <summary>
        /// 图片链接2
        /// </summary>
		public string ImgUrl2 {get;set;}


		/// <summary>
        /// 图片链接3
        /// </summary>
		public string ImgUrl3 {get;set;}


		/// <summary>
        /// 图片链接4
        /// </summary>
		public string ImgUrl4 {get;set;}


		/// <summary>
        /// 图片链接5
        /// </summary>
		public string ImgUrl5 {get;set;}


		/// <summary>
        /// 商品主体
        /// </summary>
		public Product Product {get;set;}



	}
}