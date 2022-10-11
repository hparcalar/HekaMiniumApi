using HekaMiniumApi.Models;

namespace HekaMiniumApi.Models.Constants{
    public class ConstReceiptType{
        public static ReceiptTypeModel[] Purchasing{
            get{
                return new ReceiptTypeModel[]{
                    new ReceiptTypeModel{ TypeNo = 1, TypeText = "Alış İrsaliyesi" },
                    new ReceiptTypeModel{ TypeNo = 2, TypeText = "Satıştan İade İrsaliyesi" },
                    new ReceiptTypeModel{ TypeNo = 3, TypeText = "Üretimden Giriş İrsaliyesi" },
                };
            }
        }

        public static ReceiptTypeModel[] Sales{
            get{
                return new ReceiptTypeModel[]{
                    new ReceiptTypeModel{ TypeNo = 105, TypeText = "Satış İrsaliyesi" },
                    new ReceiptTypeModel{ TypeNo = 106, TypeText = "Alıştan İade İrsaliyesi" },
                    new ReceiptTypeModel{ TypeNo = 107, TypeText = "Sarf Fişi" },
                };
            }
        }

        public static string GetDesc(int receiptType){
            try
            {
                var typeText = Purchasing.Concat(Sales).ToArray().Where(d => d.TypeNo == receiptType)
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