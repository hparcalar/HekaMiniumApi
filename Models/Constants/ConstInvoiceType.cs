using HekaMiniumApi.Models;

namespace HekaMiniumApi.Models.Constants
{
  public class ConstInvoiceType{
    public static InvoiceTypeModel[] Purchasing{
			get{
				return new InvoiceTypeModel[]{
					new InvoiceTypeModel{ TypeNo = 1, TypeText = "Alış Faturası" },
					new InvoiceTypeModel{ TypeNo = 2, TypeText = "Satıştan İade Faturası" },
					new InvoiceTypeModel{ TypeNo = 3, TypeText = "Üretimden Giriş Faturası" },
				};
			}
    }

    public static InvoiceTypeModel[] Sales{
			get{
				return new InvoiceTypeModel[]{
					new InvoiceTypeModel{ TypeNo = 105, TypeText = "Satış Faturası" },
					new InvoiceTypeModel{ TypeNo = 106, TypeText = "Alıştan İade Faturası" },
					new InvoiceTypeModel{ TypeNo = 107, TypeText = "Sarf Fişi" },
				};
			}
    }

		public static string GetDesc(int invoiceType){
			try
			{
				var typeText = Purchasing.Concat(Sales).ToArray().Where(d => d.TypeNo == invoiceType)
						.Select(d => d.TypeText).FirstOrDefault();

				return typeText ?? string.Empty;
			}
			catch (System.Exception)
			{
			}
			return string.Empty;
		}
  }
}