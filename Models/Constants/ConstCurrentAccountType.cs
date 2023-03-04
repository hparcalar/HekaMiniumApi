using HekaMiniumApi.Models;

namespace HekaMiniumApi.Models.Constants
{
  public class ConstCurrentAccountType{
    public static CurrentAccountTypeModel[] Purchasing{
			get{
				return new CurrentAccountTypeModel[]{
					new CurrentAccountTypeModel{ TypeNo = 1, TypeText = "Nakit Tahsilat" },
					new CurrentAccountTypeModel{ TypeNo = 2, TypeText = "Alacak Dekontu" },
					new CurrentAccountTypeModel{ TypeNo = 3, TypeText = "Alış Faturası" },
				};
			}
    }

    public static CurrentAccountTypeModel[] Sales{
			get{
				return new CurrentAccountTypeModel[]{
					new CurrentAccountTypeModel{ TypeNo = 105, TypeText = "Nakit Ödeme" },
					new CurrentAccountTypeModel{ TypeNo = 106, TypeText = "Borç Dekontu" },
					new CurrentAccountTypeModel{ TypeNo = 107, TypeText = "Satış Faturası" },
				};
			}
    }

    public static string GetDesc(int currentAccountType){
			try
			{
				var typeText = Purchasing.Concat(Sales).ToArray().Where(d => d.TypeNo == currentAccountType)
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