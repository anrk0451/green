using DevExpress.XtraEditors;
using green.Misc;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace green.Action
{
	public class C_detail
	{
		public string xh;       //序号
		public string fphxz;    //发票行性质 0 正常行1 折扣行2 被折扣行
		public string spmc;     //商品名称
		public string ggxh;     //规格型号(可空)
		public string dw;       //单位(可空)
		public string spsl;     //商品数量(可空)
		public string dj;       //单价(可空)
		public string je;       //金额
		public string sl;       //税率
		public string se;       //税额
		public string hsbz;     //含税标志 0-不含税 1-含税
		public string spbm;     //商品编码 税收分类编码
		public string zxbm;     //自行编码(可空)
		public string yhzcbs;   //优惠政策标识 0是不使用，1是使用
		public string slbs;     //税率标识(可空)  空，是正常税率 1-免税 2-是不征税 3-普通零税率
		public string zzstsgl;  //增值税特殊管理(可空)

	}
	/// <summary>
	/// 税务发票处理类
	/// </summary>
	class TaxInvoice
    {
		/// <summary>
		/// 包装业务数据并返回结果
		/// </summary>
		/// <param name="bdata"></param>
		/// <returns></returns>
		public static string WrapData(string serviceId, string sid, string bdata)
		{
			//1.加密业务数据
			string s_inputmi = Tools.AesEncrypt(bdata, Envior.TAX_PRIVATE_KEY);

			//2.打包发送请求数据
			Dictionary<string, object> sendmsg = new Dictionary<string, object>();
			sendmsg.Add("async", "true");
			sendmsg.Add("input", s_inputmi);
			sendmsg.Add("nsrsbh", Envior.TAX_ID);     //纳税人识别号
			sendmsg.Add("appid", Envior.TAX_APPID);   //appID	
			sendmsg.Add("serviceid", serviceId);
			sendmsg.Add("sid", sid);                  //请求流水号	

			//3.将请求报文转换为Json 字符串 并整体用公钥 加密
			string s_msg_json = Tools.ConvertObjectToJson(sendmsg);

			string s_fullmi = Tools.AesEncrypt(s_msg_json, Envior.TAX_PUBLIC_KEY);

			//4.加密后的字符串用 urlencode 编码,生产最终发送数据!!!
			string s_post = HttpUtility.UrlEncode(s_fullmi);

			//5.发送数据
			var client = new RestClient(Envior.TAX_SERVER_URL);
			client.Timeout = -1;
			var request = new RestRequest(Method.POST);
			request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			request.AddParameter("json", s_post);
 
			IRestResponse response = client.Execute(request);
			string retstr = response.Content;
			return retstr;
		}


		/// <summary>
		/// 获取下一张票号
		/// </summary>
		/// <returns></returns>
		public static int GetNextInvoiceNo()
		{
			//组装业务数据
			Dictionary<string, string> bdata = new Dictionary<string, string>();
			bdata.Add("fplxdm", Envior.TAX_INVOICE_TYPE);   //发票类型代码

			//将业务数据转换为Json字符串
			string s_json = Tools.ConvertObjectToJson(bdata);
			string s_req_sid = MiscAction.GetEntityPK("TAXREQ");            //报文请求ID

 
			Tools.msg(MessageBoxIcon.Information, "获取票号debug", s_json);
			Envior.NEXT_BILL_CODE = "000000000001";  //发票代码
			Envior.NEXT_BILL_NUM = "9999999999999";   //发票号码
			return 1;
 


			string s_retstr = WrapData("HQDQFPDMHM", s_req_sid, s_json);

			if (string.IsNullOrEmpty(s_retstr))
			{
				XtraMessageBox.Show("未获取到票号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return -1;
			}

			//分析返回结果
			Object obj = JsonConvert.DeserializeObject(s_retstr);
			Newtonsoft.Json.Linq.JObject js = obj as Newtonsoft.Json.Linq.JObject;
			if (js["code"].ToString() == "00000")   //成功
			{
				string data = js["data"].ToString();
				//解密 返回数据
				string resultText = Tools.AesDecrypt(data, Envior.TAX_PRIVATE_KEY);

				//解析真正的业务数据
				Object obj2 = JsonConvert.DeserializeObject(resultText);
				Newtonsoft.Json.Linq.JObject js2 = obj2 as Newtonsoft.Json.Linq.JObject;

				Envior.NEXT_BILL_CODE = js2["fpdm"].ToString();  //发票代码
				Envior.NEXT_BILL_NUM = js2["fphm"].ToString();   //发票号码
				return 1;
			}
			else
			{
				Tools.msg(MessageBoxIcon.Error, "提示", "获取税票号错误!\r\n" + js["msg"].ToString());
				return -1;
			}
		}

		/// <summary>
		/// 打印发票
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static int PrintInvoice(string fp001, string dylx)
		{
			string s_fphm = string.Empty;
			OracleParameter op_fp001 = new OracleParameter("fp001", OracleDbType.Varchar2, 10);
			op_fp001.Direction = ParameterDirection.Input;
			op_fp001.Value = fp001;
			OracleDataReader reader = SqlAssist.ExecuteReader("select * from fp01 where fp001 = :fp001", new OracleParameter[] { op_fp001 });
			reader.Read();
			if (reader.HasRows)
			{
				//查询发票代码和发票号码
				string s_fpdm = reader["INVOICECODE"].ToString();
				s_fphm = reader["INVOICENUM"].ToString();
				return PrintInvoice(s_fpdm, s_fphm, dylx);
			}
			else
			{
				XtraMessageBox.Show("未找到开票记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return -1;
			}
		}

		/// <summary>
		/// 打印发票
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static int PrintInvoice(string fpdm, string fphm, string dylx)
		{
			//组装业务数据
			Dictionary<string, string> bdata = new Dictionary<string, string>();
			bdata.Add("fplxdm", Envior.TAX_INVOICE_TYPE);   //发票类型代码
			bdata.Add("fpdm", fpdm);                        //发票代码
			bdata.Add("fphm", fphm);                        //发票号码
			bdata.Add("dylx", dylx);                        //打印类型  0：发票打印，1：清单打印
			bdata.Add("dyjmc", "");                         //打印机名称

			//将业务数据转换为Json字符串
			string s_json = Tools.ConvertObjectToJson(bdata);
			string s_req_sid = MiscAction.GetEntityPK("TAXREQ"); //报文请求ID
			string s_retstr = WrapData("FPDY", s_req_sid, s_json);

			//分析返回结果
			Object obj = JsonConvert.DeserializeObject(s_retstr);
			Newtonsoft.Json.Linq.JObject js = obj as Newtonsoft.Json.Linq.JObject;
			if (js["code"].ToString() == "00000")   //成功
			{
				XtraMessageBox.Show("打印成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return 1;
			}
			else
			{
				Tools.msg(MessageBoxIcon.Error, "错误", "打印错误!\r\n" + js["msg"]);			 
				return -1;
			}
		}

		/// <summary>
		/// 发票开具
		/// </summary>
		/// <returns></returns>
		public static int Invoice(string fp001, TaxClientInfo taxClient)
		{
			//组装业务数据
			Dictionary<string, object> bdata = new Dictionary<string, object>();

			//判断是正数负数发票
			decimal dec_sum = Convert.ToDecimal(SqlAssist.ExecuteScalar("select je from v_fp01_nopen where fp001='" + fp001 + "'"));

			//退费红冲 和 正数发票分开 
			if (dec_sum < 0) return InvoiceRefund(fp001, taxClient);

			bdata.Add("fplxdm", Envior.TAX_INVOICE_TYPE);             //发票类型代码
			bdata.Add("kplx", dec_sum > 0 ? "0" : "1");               //开票类型 0-正数发票 1-负数
			bdata.Add("tspz", "00");                                  //特殊票种 00-不是 01-农产品销售 02-农产品收购
			bdata.Add("xhdwdzdh", Envior.TAX_ADDR_TELE);              //销货单位地址电话
			bdata.Add("xhdwyhzh", Envior.TAX_BANK_ACCOUNT);           //销货单位银行账号
			bdata.Add("ghdwsbh", taxClient.InfoClientTaxCode);        //购货单位纳税识别号
			bdata.Add("ghdwmc", taxClient.InfoClientName);            //购货单位名称
			bdata.Add("ghdwdzdh", taxClient.infoclientaddressphone);  //购货单位地址电话
			bdata.Add("ghdwyhzh", taxClient.infoclientbankaccount);   //购货单位银行账号

			OracleParameter op_fp001 = new OracleParameter("fp001", OracleDbType.Varchar2, 10);
			op_fp001.Direction = ParameterDirection.Input;
			op_fp001.Value = fp001;
			OracleDataReader reader_sa01 = SqlAssist.ExecuteReader("select * from v_invoice_detail where fp001 = :fp001 order by sa002,sa001", new OracleParameter[] { op_fp001 });

			int i_order = 0;
			List<C_detail> detaildata = new List<C_detail>();
			decimal dec_rate = new decimal();
			decimal dec_price = new decimal();         //含税价格
			decimal dec_price_notax = new decimal();   //不含税价格
			decimal dec_je_notax = new decimal();      //不含税金额
			decimal dec_tax = new decimal();           //税额

			decimal dec_sum_notax = new decimal(0);    //合计金额(不含税)
			decimal dec_sum_tax = new decimal(0);      //合计税额
			decimal dec_sum_sum = new decimal(0);      //加税合计
			decimal dec_nums = new decimal(0);		   //数量

			bool b_split = false;					   //项目是否分割

			while (reader_sa01.Read())
			{
				if (reader_sa01["SA020"].ToString() != "T") continue;   //不是税务项目 忽略
				i_order++;

				if (Convert.ToDecimal(reader_sa01["SA007"]) != Convert.ToDecimal(reader_sa01["FP022"]))
					b_split = true;
				else
					b_split = false;

				C_detail c_detail = new C_detail();
				dec_rate = Convert.ToDecimal(reader_sa01["SA025"]);                      //获取税率

				
				dec_price = b_split ? Convert.ToDecimal(reader_sa01["FP022"]) : Convert.ToDecimal(reader_sa01["PRICE"]);               //含税价格
				dec_nums = b_split ? 1 : Convert.ToDecimal(reader_sa01["NUMS"]);													   //数量

				dec_price_notax = Math.Round(dec_price / (1 + dec_rate), 2);             //不含税价格
				dec_je_notax = Math.Round((dec_price / (1 + dec_rate)) * dec_nums, 2);   //不含税金额
				dec_tax = Math.Round((dec_price / (1 + dec_rate)) * dec_rate * dec_nums , 2);  //税额

				c_detail.xh = i_order.ToString();                                        //序号 
				c_detail.fphxz = "0";                                                    //发票行性质	0 正常行1 折扣行2 被折扣行
				c_detail.ggxh = BusinessAction.GetItemGGXH(reader_sa01["SA004"].ToString()); //规格型号
				c_detail.dw = BusinessAction.GetItemDW(reader_sa01["SA004"].ToString());     //计量单位
				c_detail.spmc = reader_sa01["SA003"].ToString();                         //商品名称
				c_detail.spsl = dec_nums.ToString();									 //数量
				c_detail.dj = dec_price_notax.ToString();                                //单价(不含税)
				c_detail.je = dec_je_notax.ToString();                                   //金额(不含税)
				c_detail.sl = dec_rate.ToString();                                       //税率
				c_detail.se = dec_tax.ToString();                                        //税额						
				c_detail.hsbz = "0";                                                     //含税标志 0 不含税1 含税
				c_detail.spbm = BusinessAction.GetItemInvoiceCode(reader_sa01["SA002"].ToString(), reader_sa01["SA004"].ToString());  //商品编码(税务发票编码)
				c_detail.zxbm = "";                                                      //自行编码	
				c_detail.yhzcbs = "0";                                                   //优惠政策标识   

				c_detail.slbs = dec_rate == 0 ? "1" : "";                                //税率标识： 空，是正常税率  1-免税 2-不征税 3-普通零税率

				c_detail.zzstsgl = "";                                                   //增值税特殊管理
				detaildata.Add(c_detail);

				dec_sum_notax += dec_je_notax;              //合计金额(不含税)
				dec_sum_tax += dec_tax;                     //合计税额
				dec_sum_sum += dec_je_notax + dec_tax;      //价税合计
			}

			reader_sa01.Dispose();

			bdata.Add("mx", detaildata);
			bdata.Add("hjje", dec_sum_notax.ToString());     //合计金额(不含税)
			bdata.Add("hjse", dec_sum_tax.ToString());       //合计税额
			bdata.Add("jshj", dec_sum_sum.ToString());       //价税合计
			bdata.Add("bz", "");                             //备注
			bdata.Add("skr", Envior.TAX_CASHIER);            //收款人
			bdata.Add("fhr", Envior.TAX_CHECKER);            //复核人
			bdata.Add("kpr", Envior.cur_userName);           //开票人
			bdata.Add("tzdbh", "");                          //通知单编号 专票红字必填

			 
			bdata.Add("yfphm", "");							 //原发票号码 负数发票必填
			bdata.Add("yfpdm", "");							 //原发票代码 负数发票必填
			bdata.Add("gmf_dzyx", "");                       //购买方电子邮箱 推送使用，电子发票，购买方电子邮箱和手机号码微信id三个必填一
			bdata.Add("gmf_sjhm", "1");                      //购买方手机号码
			bdata.Add("gmf_openid", "");                     //购买方微信id

			//将业务数据转换为Json字符串
			string s_json = Tools.ConvertObjectToJson(bdata);
			string s_req_sid = MiscAction.GetEntityPK("TAXREQ"); //报文请求ID

			Tools.msg(MessageBoxIcon.Information, "debug", s_json);
			return 1;

			string s_retstr = WrapData("FPKJ", s_req_sid, s_json);
			 
			//分析返回结果
			Object obj = JsonConvert.DeserializeObject(s_retstr);
			Newtonsoft.Json.Linq.JObject js = obj as Newtonsoft.Json.Linq.JObject;
			if (js["code"].ToString() == "00000")   //成功
			{
				string data = js["data"].ToString();
				//解密 返回数据
				string resultText = Tools.AesDecrypt(data, Envior.TAX_PRIVATE_KEY);

				//解析真正的业务数据
				Object obj2 = JsonConvert.DeserializeObject(resultText);
				Newtonsoft.Json.Linq.JObject js2 = obj2 as Newtonsoft.Json.Linq.JObject;

				string s_fpdm = js2["fpdm"].ToString();   //发票代码
				string s_fphm = js2["fphm"].ToString();   //发票号码
				string s_mw = js2["mw"].ToString();       //密文
				string s_jym = js2["jym"].ToString();     //校验码
				decimal d_hjje = Convert.ToDecimal(js2["hjje"].ToString());  //合计金额
				decimal d_jshj = Convert.ToDecimal(js2["jshj"].ToString());  //价税合计

				//记录发票日志
				int result = InvoiceLog(fp001, Envior.TAX_INVOICE_TYPE, Envior.cur_userId, Envior.cur_userName, Envior.cur_userName,
					taxClient.InfoClientName, taxClient.InfoClientTaxCode, taxClient.infoclientbankaccount, taxClient.infoclientaddressphone,
					s_fpdm, s_fphm, s_mw, s_jym, d_hjje, d_jshj);

				if (result > 0)
					Tools.msg(MessageBoxIcon.Information, "提示", "发票开具成功!\r\n" + "发票代码:" + s_fpdm + "\r\n" + "发票号码:" + s_fphm);					 
				else
					Tools.msg(MessageBoxIcon.Error, "提示", "发票开具成功!但记录开票日志失败!\r\n" + "发票代码:" + s_fpdm + "\r\n" + "发票号码:" + s_fphm);				 

				////////// 打印发票和清单 //////////
				if (XtraMessageBox.Show("现在打印【发票】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					PrintInvoice(s_fpdm, s_fphm, "0");
				}
				if (i_order > AppInfo.TAXITEMCOUNT && XtraMessageBox.Show("现在打印【发票清单】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					PrintInvoice(s_fpdm, s_fphm, "1");
				}
				return 1;
			}
			else
			{
				Tools.msg(MessageBoxIcon.Error, "错误", "发票开具失败!\r\n" + js["msg"].ToString()); 
				return -1;
			}
		}

		/// <summary>
		/// 负数发票开具
		/// </summary>
		/// <returns></returns>
		public static int InvoiceRefund(string fp001, TaxClientInfo taxClient)
		{
			//组装业务数据
			Dictionary<string, object> bdata = new Dictionary<string, object>();

			int i_order = 0;
			List<C_detail> detaildata = new List<C_detail>();

			decimal dec_rate = new decimal();
			decimal dec_price = new decimal();         //含税价格
			decimal dec_price_notax = new decimal();   //不含税价格
			decimal dec_je_notax = new decimal();      //不含税金额
			decimal dec_tax = new decimal();           //税额

			decimal dec_sum_notax = new decimal(0);    //合计金额(不含税)
			decimal dec_sum_tax = new decimal(0);      //合计税额
			decimal dec_sum_sum = new decimal(0);      //价税合计
			decimal dec_nums = new decimal(0);

			string s_ggxh = string.Empty;              //规格型号
			string s_jldw = string.Empty;              //计量单位
			string s_spmc = string.Empty;              //商品名称
			string s_spbm = string.Empty;              //商品编码
			string s_yfpdm = string.Empty;             //原发票代码
			string s_yfphm = string.Empty;             //原发票号码

			//发票明细数量
			int itemCount = Convert.ToInt32(SqlAssist.ExecuteScalar("select count(*) from v_invoice_detail where fp001 ='" + fp001 + "'"));
			//退费总金额
			decimal dec_sum = Convert.ToDecimal(SqlAssist.ExecuteScalar("select sum(fp022) from v_invoice_detail where fp001='" + fp001 + "'"));

			bdata.Add("fplxdm", Envior.TAX_INVOICE_TYPE);             //发票类型代码
			bdata.Add("kplx", "1");                                   //开票类型 0-正数发票 1-负数
			bdata.Add("tspz", "00");                                  //特殊票种 00-不是 01-农产品销售 02-农产品收购
			bdata.Add("xhdwdzdh", Envior.TAX_ADDR_TELE);              //销货单位地址电话
			bdata.Add("xhdwyhzh", Envior.TAX_BANK_ACCOUNT);           //销货单位银行账号
			bdata.Add("ghdwsbh", taxClient.InfoClientTaxCode);        //购货单位纳税识别号
			bdata.Add("ghdwmc", taxClient.InfoClientName);            //购货单位名称
			bdata.Add("ghdwdzdh", taxClient.infoclientaddressphone);  //购货单位地址电话
			bdata.Add("ghdwyhzh", taxClient.infoclientbankaccount);   //购货单位银行账号

			OracleParameter op_fp001 = new OracleParameter("fp001", OracleDbType.Varchar2, 10);
			op_fp001.Direction = ParameterDirection.Input;
			op_fp001.Value = fp001;
			OracleDataReader reader_sa01 = SqlAssist.ExecuteReader("select * from v_invoice_detail where fp001 = :fp001", new OracleParameter[] { op_fp001 });

			bool b_split = false;                      //项目是否分割

			if (itemCount > AppInfo.TAXITEMCOUNT)      //超出清单阈值
			{
				while (reader_sa01.Read())
				{
					i_order++;

					if (Convert.ToDecimal(reader_sa01["SA007"]) != Convert.ToDecimal(reader_sa01["FP022"]))
						b_split = true;
					else
						b_split = false;

					dec_rate = Convert.ToDecimal(reader_sa01["SA025"]);                      //获取税率
					dec_price = b_split ? Convert.ToDecimal(reader_sa01["FP022"]) : Convert.ToDecimal(reader_sa01["PRICE"]);                //含税价格
					dec_nums = b_split ? 1 : Convert.ToDecimal(reader_sa01["NUMS"].ToString());

					dec_price_notax = Math.Round(dec_price / (1 + dec_rate), 2);					//不含税价格
					dec_je_notax += Math.Round((dec_price / (1 + dec_rate)) * dec_nums , 2);		//不含税金额
					dec_tax += Math.Round((dec_price / (1 + dec_rate)) * dec_rate * dec_nums , 2);  //税额

					if (i_order == 1) s_ggxh = BusinessAction.GetItemGGXH(reader_sa01["SA004"].ToString());                                     //规格型号	
					if (i_order == 1) s_jldw = BusinessAction.GetItemDW(reader_sa01["SA004"].ToString());                                       //计量单位	
					if (i_order == 1) s_spmc = reader_sa01["SA003"].ToString();																	//商品名称	
					if (i_order == 1) s_spbm = BusinessAction.GetItemInvoiceCode("", reader_sa01["SA004"].ToString());                          //商品编码
				}
				reader_sa01.Dispose();

				C_detail c_detail = new C_detail();
				c_detail.xh = "1";                                                       //序号 
				c_detail.fphxz = "0";                                                    //发票行性质	0 正常行1 折扣行2 被折扣行
				c_detail.ggxh = s_ggxh;                                                  //规格型号

				c_detail.dw = s_jldw;                                                    //计量单位
				c_detail.spmc = "详见对应正数发票及清单";                                //商品名称 s_spmc;
				c_detail.spsl = "";                                                      //数量
				c_detail.dj = "";                                                        //单价(不含税)
				c_detail.je = dec_je_notax.ToString();                                   //金额(不含税)
				c_detail.sl = "";                                                        //税率
				c_detail.se = dec_tax.ToString();                                        //税额						
				c_detail.hsbz = "0";                                                     //含税标志 0 不含税1 含税
				c_detail.spbm = ""; //s_spbm;											 //商品编码(税务发票编码)
				c_detail.zxbm = "";                                                      //自行编码	
				c_detail.yhzcbs = "0";                                                   //优惠政策标识   
				c_detail.slbs = "";

				c_detail.zzstsgl = "";                                                   //增值税特殊管理
				detaildata.Add(c_detail);

				dec_sum_notax = dec_je_notax;              //合计金额(不含税)
				dec_sum_tax = dec_tax;                     //合计税额
				dec_sum_sum = dec_je_notax + dec_tax;      //价税合计

				bdata.Add("mx", detaildata);

				bdata.Add("zhsl", "99.01");                      //综合税率 固定传 99.01
				bdata.Add("hjje", dec_sum_notax.ToString());     //合计金额(不含税)
				bdata.Add("hjse", dec_sum_tax.ToString());       //合计税额
				bdata.Add("jshj", dec_sum_sum.ToString());       //价税合计
				bdata.Add("bz", "");                             //备注
				bdata.Add("skr", Envior.TAX_CASHIER);            //收款人
				bdata.Add("fhr", Envior.TAX_CHECKER);            //复核人
				bdata.Add("kpr", Envior.cur_userName);           //开票人
				bdata.Add("tzdbh", "");                          //通知单编号 专票红字必填
			}
			else
			{
				while (reader_sa01.Read())
				{
					if (reader_sa01["SA020"].ToString() != "T") continue;   //不是税务项目 忽略
					i_order++;

					if (Convert.ToDecimal(reader_sa01["SA007"]) != Convert.ToDecimal(reader_sa01["FP022"]))
						b_split = true;
					else
						b_split = false;

					C_detail c_detail = new C_detail();
					dec_rate = Convert.ToDecimal(reader_sa01["SA025"]);																		//获取税率
					dec_price = b_split ? Convert.ToDecimal(reader_sa01["FP022"]) : Convert.ToDecimal(reader_sa01["PRICE"]);                //含税价格
					dec_nums = b_split ? 1 : Convert.ToDecimal(reader_sa01["NUMS"].ToString());

					dec_price_notax = Math.Round(dec_price / (1 + dec_rate), 2);             //不含税价格
					dec_je_notax = Math.Round((dec_price / (1 + dec_rate)) * dec_nums, 2);   //不含税金额
					dec_tax = Math.Round((dec_price / (1 + dec_rate)) * dec_rate * dec_nums, 2);  //税额

					c_detail.xh = i_order.ToString();                                        //序号 
					c_detail.fphxz = "0";                                                    //发票行性质	0 正常行1 折扣行2 被折扣行
					c_detail.ggxh = BusinessAction.GetItemGGXH(reader_sa01["SA004"].ToString()); //规格型号
					c_detail.dw = BusinessAction.GetItemDW(reader_sa01["SA004"].ToString());     //计量单位
					c_detail.spmc = reader_sa01["SA003"].ToString();                         //商品名称
					c_detail.spsl = dec_nums.ToString(); ;									 //数量 
					c_detail.dj = dec_price_notax.ToString();                                //单价(不含税) 
					c_detail.je = dec_je_notax.ToString();                                   //金额(不含税)
					c_detail.sl = dec_rate.ToString();                                       //税率
					c_detail.se = dec_tax.ToString();                                        //税额						
					c_detail.hsbz = "0";                                                     //含税标志 0 不含税1 含税
					c_detail.spbm = BusinessAction.GetItemInvoiceCode("", reader_sa01["SA004"].ToString());    //商品编码(税务发票编码)
					c_detail.zxbm = "";                                                      //自行编码	
					c_detail.yhzcbs = "0";                                                   //优惠政策标识   

					c_detail.slbs = dec_rate == 0 ? "1" : "";                                //税率标识： 空，是正常税率  1-免税 2-不征税 3-普通零税率

					c_detail.zzstsgl = "";                                                   //增值税特殊管理
					detaildata.Add(c_detail);

					dec_sum_notax += dec_je_notax;              //合计金额(不含税)
					dec_sum_tax += dec_tax;                     //合计税额
					dec_sum_sum += dec_je_notax + dec_tax;      //价税合计
				}

				bdata.Add("mx", detaildata);
				bdata.Add("hjje", dec_sum_notax.ToString());     //合计金额(不含税)
				bdata.Add("hjse", dec_sum_tax.ToString());       //合计税额
				bdata.Add("jshj", dec_sum_sum.ToString());       //价税合计
				bdata.Add("bz", "");                             //备注
				bdata.Add("skr", Envior.TAX_CASHIER);            //收款人
				bdata.Add("fhr", Envior.TAX_CHECKER);            //复核人
				bdata.Add("kpr", Envior.cur_userName);           //开票人
				bdata.Add("tzdbh", "");                          //通知单编号 专票红字必填
			}

			///检索原发票代码、号码
			string s_log_sql = String.Format(@"select * from tax_log where settleId = 
												 (select rf300 from refund where rf001 = '" + fp001 + "')");
			using (OracleDataReader reader_log = SqlAssist.ExecuteReader(s_log_sql))
			{
				if (reader_log.Read())
				{
					s_yfpdm = reader_log["INVOICECODE"].ToString();   //原发票代码
					s_yfphm = reader_log["INVOICENUM"].ToString();    //原发票号码
				}
				else
				{
					Tools.msg(MessageBoxIcon.Error, "错误", "读取原发票信息错误!");				 
					return -1;
				}
			}

			bdata.Add("yfphm", s_yfphm);                     //原发票号码 负数发票必填
			bdata.Add("yfpdm", s_yfpdm);                     //原发票代码 负数发票必填
			bdata.Add("gmf_dzyx", "");                       //购买方电子邮箱 推送使用，电子发票，购买方电子邮箱和手机号码微信id三个必填一
			bdata.Add("gmf_sjhm", "1");                      //购买方手机号码
			bdata.Add("gmf_openid", "");                     //购买方微信id

			//将业务数据转换为Json字符串
			string s_json = Tools.ConvertObjectToJson(bdata);
			 
			string s_req_sid = string.Empty;
			string s_retstr = string.Empty;

			s_req_sid = MiscAction.GetEntityPK("TAXREQ"); //报文请求ID	
			LogUtils.Debug("报文请求Id:" + s_req_sid);

			s_retstr = WrapData("FPKJ", s_req_sid, s_json);

			//分析返回结果
			Object obj = JsonConvert.DeserializeObject(s_retstr);
			Newtonsoft.Json.Linq.JObject js = obj as Newtonsoft.Json.Linq.JObject;

			if (js["code"].ToString() == "00000")   //成功
			{
				string data = js["data"].ToString();
				//解密 返回数据
				string resultText = Tools.AesDecrypt(data, Envior.TAX_PRIVATE_KEY);

				//解析真正的业务数据
				Object obj2 = JsonConvert.DeserializeObject(resultText);
				Newtonsoft.Json.Linq.JObject js2 = obj2 as Newtonsoft.Json.Linq.JObject;

				string s_fpdm = js2["fpdm"].ToString();   //发票代码
				string s_fphm = js2["fphm"].ToString();   //发票号码
				string s_mw = js2["mw"].ToString();       //密文
				string s_jym = js2["jym"].ToString();     //校验码
				decimal d_hjje = Convert.ToDecimal(js2["hjje"].ToString());  //合计金额
				decimal d_jshj = Convert.ToDecimal(js2["jshj"].ToString());  //价税合计

				//记录发票日志
				int result = InvoiceLog(fp001, Envior.TAX_INVOICE_TYPE, Envior.cur_userId, Envior.cur_userName, Envior.cur_userName,
					taxClient.InfoClientName, taxClient.InfoClientTaxCode, taxClient.infoclientbankaccount, taxClient.infoclientaddressphone,
					s_fpdm, s_fphm, s_mw, s_jym, d_hjje, d_jshj);

				if (result > 0)
				{
					XtraMessageBox.Show("发票开具成功!\r\n" + "发票代码:" + s_fpdm + "\r\n" + "发票号码:" + s_fphm, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					XtraMessageBox.Show("发票开具成功!但记录开票日志失败!\r\n" + "发票代码:" + s_fpdm + "\r\n" + "发票号码:" + s_fphm, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

				////////// 打印发票和清单 //////////
				if (XtraMessageBox.Show("现在打印【发票】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					PrintInvoice(s_fpdm, s_fphm, "0");
				}
				if (i_order > AppInfo.TAXITEMCOUNT && XtraMessageBox.Show("现在打印【发票清单】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					PrintInvoice(s_fpdm, s_fphm, "1");
				}
				return 1;
			}
			else
			{
				Tools.msg(MessageBoxIcon.Error, "错误", "发票开具失败!\r\n" + js["msg"].ToString());			 
				return -1;
			}
		}


		/// <summary>
		/// 记录发票日志
		/// </summary>
		/// <returns></returns>
		public static int InvoiceLog(string fp001, string infoKind, string INFOINVOICER, string INFOCASHIER, string INFOCHECKER, string INFOCLIENTNAME,
			string INFOCLIENTTAXCODE, string INFOCLIENTBANKACCOUNT, string INFOCLIENTADDRESSPHONE, string INVOICECODE, string INVOICENUM, string mw,
			string jym, decimal hjje, decimal jshj)
		{
			//结算流水号
			OracleParameter op_fp001 = new OracleParameter("ic_fp001", OracleDbType.Varchar2, 10);
			op_fp001.Direction = ParameterDirection.Input;
			op_fp001.Value = fp001;

			//发票类别
			OracleParameter op_kind = new OracleParameter("ic_infoKind", OracleDbType.Varchar2, 3);
			op_kind.Direction = ParameterDirection.Input;
			op_kind.Value = infoKind;

			//开票人
			OracleParameter op_invoicer = new OracleParameter("ic_INFOINVOICER", OracleDbType.Varchar2, 20);
			op_invoicer.Direction = ParameterDirection.Input;
			op_invoicer.Value = INFOINVOICER;

			//收款人
			OracleParameter op_cashier = new OracleParameter("ic_INFOCASHIER", OracleDbType.Varchar2, 20);
			op_cashier.Direction = ParameterDirection.Input;
			op_cashier.Value = INFOCASHIER;

			//复核人
			OracleParameter op_checker = new OracleParameter("ic_INFOCHECKER", OracleDbType.Varchar2, 20);
			op_checker.Direction = ParameterDirection.Input;
			op_checker.Value = INFOCHECKER;

			//客户名称
			OracleParameter op_clientName = new OracleParameter("ic_INFOCLIENTNAME", OracleDbType.Varchar2, 80);
			op_clientName.Direction = ParameterDirection.Input;
			op_clientName.Value = INFOCLIENTNAME;

			//客户税号
			OracleParameter op_clientTaxCode = new OracleParameter("ic_INFOCLIENTTAXCODE", OracleDbType.Varchar2, 50);
			op_clientTaxCode.Direction = ParameterDirection.Input;
			op_clientTaxCode.Value = INFOCLIENTTAXCODE;

			//客户银行账号
			OracleParameter op_bankAccount = new OracleParameter("ic_INFOCLIENTBANKACCOUNT", OracleDbType.Varchar2, 80);
			op_bankAccount.Direction = ParameterDirection.Input;
			op_bankAccount.Value = INFOCLIENTBANKACCOUNT;

			//客户地址电话
			OracleParameter op_addrTele = new OracleParameter("ic_INFOCLIENTADDRESSPHONE", OracleDbType.Varchar2, 80);
			op_addrTele.Direction = ParameterDirection.Input;
			op_addrTele.Value = INFOCLIENTADDRESSPHONE;

			//发票代码
			OracleParameter op_invcode = new OracleParameter("ic_INVOICECODE", OracleDbType.Varchar2, 20);
			op_invcode.Direction = ParameterDirection.Input;
			op_invcode.Value = INVOICECODE;

			//发票号码
			OracleParameter op_invnum = new OracleParameter("ic_INVOICENUM", OracleDbType.Varchar2, 20);
			op_invnum.Direction = ParameterDirection.Input;
			op_invnum.Value = INVOICENUM;

			//密文
			OracleParameter op_mw = new OracleParameter("ic_mw", OracleDbType.Varchar2, 80);
			op_mw.Direction = ParameterDirection.Input;
			op_mw.Value = mw;

			//校验码
			OracleParameter op_jym = new OracleParameter("ic_jym", OracleDbType.Varchar2, 50);
			op_jym.Direction = ParameterDirection.Input;
			op_jym.Value = jym;

			//合计金额
			OracleParameter op_hjje = new OracleParameter("in_hjje", OracleDbType.Decimal);
			op_hjje.Direction = ParameterDirection.Input;
			op_hjje.Value = hjje;

			//价税合计
			OracleParameter op_jshj = new OracleParameter("in_jshj", OracleDbType.Decimal);
			op_jshj.Direction = ParameterDirection.Input;
			op_jshj.Value = jshj;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_TaxInvoiceLog", new OracleParameter[]
			{op_fp001,op_kind,op_invoicer,op_cashier,op_checker,op_clientName,op_clientTaxCode,op_bankAccount,op_addrTele,op_invcode,
			 op_invnum,op_mw,op_jym,op_hjje,op_jshj});
		}

	}
}
